using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using S7.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using DataType = S7.Net.DataType;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class S71200WaterProofing : IS71200WaterProofingService
    {
        private readonly Plc _s7Client;
        private readonly string _address;
        private readonly int _memory;
        private readonly int _dB;
        private readonly System.Timers.Timer _timer1;
        private WaterProofingMachineRawData DataStruct = new WaterProofingMachineRawData();
        private bool preConnectionStatus = true;
        private bool _lock = true;

        public S71200WaterProofing(int memory, string address, int DB)
        {
            _s7Client = new Plc(CpuType.S71200, address, 0, 1);
            _memory = memory;
            _address = address;
            _dB = DB;
            _s7Client.ReadTimeout = 600;
            _s7Client.WriteTimeout = 400;
            _timer1 = new System.Timers.Timer()
            {
                Interval = 1000,
                Enabled = false
            };
            _timer1.Elapsed += timer1_tick;
        }

        private void timer1_tick(object? sender, ElapsedEventArgs e)
        {
            Byte[] data_reset = new byte[1];
            _s7Client.WriteBytes(DataType.DataBlock, _dB, 0, data_reset);
            _timer1.Stop();
        }

        public event Action<WaterProofingMachineRawData> DataReceived;
        public event Action PlcNotConnected;
        public void SetMemoryBit(string s)
        {
            var buffer = new byte[1];
            if ((s == "start") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 0, true);
                _s7Client.WriteBytes(DataType.DataBlock, _dB, 0, buffer);
                _timer1.Enabled = true;

            }
            if ((s == "stop") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 1, true);
                _s7Client.WriteBytes(DataType.DataBlock, _dB, 0, buffer);
                _timer1.Enabled = true;
            }
            if ((s == "reset") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 2, true);
                _s7Client.WriteBytes(DataType.DataBlock, _dB, 0, buffer);
                _timer1.Enabled = true;
            }
            if ( (s=="resetnumber")&&(_s7Client.IsConnected) )
            {
                Sharp7.S7.SetBitAt(buffer,0,0,true);
                _s7Client.WriteBytes(DataType.DataBlock,_dB,52,buffer);
                Thread.Sleep(1000);
                Sharp7.S7.SetBitAt(buffer,0,0,false);
                _s7Client.WriteBytes(DataType.DataBlock,_dB,52,buffer);
                _timer1.Enabled=true;
            }
            if ((s == "confirm") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 3, true);
                _s7Client.WriteBytes(DataType.DataBlock, _dB, 0, buffer);
                _timer1.Enabled = true;
            }
            else if (!_s7Client.IsConnected)
            {
                Console.WriteLine("PLC chong tham mat ket noi");
                //foreach (var handler in PlcNotConnectedHandler)
                //{
                //    handler.Invoke();
                //}
            }
            buffer[0] = 0;
        }
        public async void SendTime(int offset, int data)
        {
            if (_s7Client.IsConnected)
            {
                byte[] _data = new byte[4];
                _data = BitConverter.GetBytes(data);
                var data_send = MyConvert4Byte(_data);
                await _s7Client.WriteBytesAsync(DataType.DataBlock, _dB, _memory + offset, data_send);
            }

        }
        public async void SendDataReal(int offset, float data)
        {
            if (_s7Client.IsConnected)
            {
                byte[] _data = new byte[4];
                _data = BitConverter.GetBytes(data);
                var data_send = MyConvert4Byte(_data);
                await _s7Client.WriteBytesAsync(DataType.DataBlock, _dB, _memory + offset, data_send);
            }

        }

        public async void SendData2Byte (int offset,int data)
        {
            if ( _s7Client.IsConnected )
            {
                byte[] _data = new byte[2];
                _data=BitConverter.GetBytes(data);
                var data_send = MyConvert2Byte(_data);
                await _s7Client.WriteBytesAsync(DataType.DataBlock,_dB,_memory+offset,data_send);
            }
        }
        private byte[] MyConvert4Byte(byte[] buffer_data)
        {
            byte[] buffer = new byte[4];
            buffer[0] = buffer_data[3];
            buffer[1] = buffer_data[2];
            buffer[2] = buffer_data[1];
            buffer[3] = buffer_data[0];
            return buffer;
        }
        private byte[] MyConvert2Byte (byte[] buffer_data)
        {
            byte[] buffer = new byte[4];
            buffer[0]=buffer_data[1];
            buffer[1]=buffer_data[0];

            return buffer;
        }
        private async Task<bool> Connect()
        {
            try
            {
                if (!_s7Client.IsConnected)
                {
                    await _s7Client.OpenAsync();
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task ReadData()
        {
            if (_lock)
            {
                _lock = false;
                var isConnected = await Connect();
                if (isConnected)
                {
                    try
                    {
                        preConnectionStatus = isConnected;
                        var data = (WaterProofingMachineRawData)await _s7Client.ReadStructAsync(typeof(WaterProofingMachineRawData), _dB, 16);
                        // Không cần chia cho 1000 (kiểu time)  vì đã được xử lý trong chương trình
                        DataStruct = data;
                        DataReceived?.Invoke(DataStruct);
                    }
                    catch
                    {
                        PlcNotConnected?.Invoke();
                    }
                }
                else
                {
                    isConnected = await Connect();
                    if (isConnected)
                    {
                        preConnectionStatus = isConnected;
                    }
                    else
                    {
                        if (preConnectionStatus)
                        {
                            preConnectionStatus = isConnected;
                            PlcNotConnected?.Invoke();
                        }
                        preConnectionStatus = isConnected;
                    }
                    //var data = new WaterProofingMachineRawData();
                    //DataReceived?.Invoke(data);
                }
                _lock = true;
            }

        }


    }
}
