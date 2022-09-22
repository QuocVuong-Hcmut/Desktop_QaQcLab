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
    public class S71200Endurance : IS71200EnduranceService
    {
        private readonly Plc _s7Client;
        private readonly System.Timers.Timer _timer1;
        private readonly System.Timers.Timer _timer2;
        private readonly int _memory;
        private readonly int _DB;
        private bool preConnectionStatus = true;
        public readonly string _address;
        public event Action<EnduranceMachineRawData> DataReceived;
        public event Action PlcNotConnected;
        private EnduranceMachineRawData testingStruct = new EnduranceMachineRawData();
        private bool _lock = true;

        public S71200Endurance(int memory, string address, int DB)
        {
            _s7Client = new Plc(CpuType.S71200, address, 0, 1);
            _memory = memory;
            _address = address;
            _DB = DB;
            _timer1 = new System.Timers.Timer
            {
                Interval = 200,
                Enabled = false
            };
            _timer1.Elapsed += Timer1_Tick;
            _s7Client.ReadTimeout = 600;
            _s7Client.WriteTimeout = 400;
        }
        #region control
        public void SetMemoryBit(string s)
        {
            var buffer = new byte[1];
            if ((s == "start") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 1, true);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, 0, buffer);
                _timer1.Enabled = true;

            }
            if ((s == "stop") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 2, true);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, 0, buffer);
                _timer1.Enabled = true;
            }
            if ((s == "reset") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 3, true);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, 0, buffer);
                _timer1.Enabled = true;
            }
            if ( (s=="resetnumber")&&(_s7Client.IsConnected) )
            {
                Sharp7.S7.SetBitAt(buffer,0,0,true);
                _s7Client.WriteBytes(DataType.DataBlock,_DB,80,buffer);
                Thread.Sleep(1000);
                Sharp7.S7.SetBitAt(buffer,0,0,false);
                _s7Client.WriteBytes(DataType.DataBlock,_DB,80,buffer);
                _timer1.Enabled=true;
            }
            if ((s == "confirm") && (_s7Client.IsConnected))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 6, true);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, 0, buffer);
                _timer1.Enabled = true;
            }
            if ((s == "cancel") && (_s7Client.IsConnected))
            {
                //_timer.Enabled = false;
                Sharp7.S7.SetBitAt(buffer, 0, 6, false);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, 0, buffer);
                _timer1.Enabled = true;
            }

            buffer[0] = 0;
        }
        public void SetMemoryBitData(int offset, int bit, bool status, int StatusSelect)
        {
            try
            {
                var buffer = new byte[1];
                Sharp7.S7.SetBitAt(buffer, 0, bit, status);
                if (StatusSelect == 1)
                {
                    Sharp7.S7.SetBitAt(buffer, 0, 4, true);
                    Sharp7.S7.SetBitAt(buffer, 0, 5, false);
                }
                if (StatusSelect == 2)
                {
                    Sharp7.S7.SetBitAt(buffer, 0, 4, false);
                    Sharp7.S7.SetBitAt(buffer, 0, 5, true);
                }
                if (StatusSelect == 3)
                {
                    Sharp7.S7.SetBitAt(buffer, 0, 4, true);
                    Sharp7.S7.SetBitAt(buffer, 0, 5, true);
                }
                if (StatusSelect == 4)
                {
                    Sharp7.S7.SetBitAt(buffer, 0, 4, false);
                    Sharp7.S7.SetBitAt(buffer, 0, 5, false);
                }
                Sharp7.S7.SetBitAt(buffer, 0, bit, status);
                _s7Client.WriteBytes(S7.Net.DataType.DataBlock, _DB, 0, buffer);
                _timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public async void SetMemMoryBitStatus(int offset, int bit, bool status)
        {
            var buffer = new byte[1];
            Sharp7.S7.SetBitAt(buffer, 0, bit, status);
            await _s7Client.WriteBytesAsync(DataType.DataBlock, _DB, 0, buffer);
        }
        public void SendDataUint(int offset, Int16 data)
        {
            if (_s7Client.IsConnected)
            {
                byte[] _data;
                _data = BitConverter.GetBytes(data);
                var data_send = MyConvert2Byte(_data);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, _memory + offset, data_send);
            }
            else
            {
                PlcNotConnected?.Invoke();
            }
        }
        public void SendDataReal(int offset, Single data)
        {
            if (_s7Client.IsConnected)
            {
                byte[] _data = new byte[4];
                _data = BitConverter.GetBytes(data);
                var data_send = MyConvert4Byte(_data);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, _memory + offset, data_send);
            }
            else
            {
                PlcNotConnected?.Invoke();
            }
        }
        public void SendTime(int offset, int data)
        {
            if (_s7Client.IsConnected)
            {
                byte[] _data = new byte[4];
                _data = BitConverter.GetBytes(data * 1000);
                var data_send = MyConvert4Byte(_data);
                _s7Client.WriteBytes(DataType.DataBlock, _DB, _memory + offset, data_send);
            }
            else
            {
                PlcNotConnected?.Invoke();
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
        private byte[] MyConvert2Byte(byte[] buffer_data)
        {
            byte temp = buffer_data[0];
            buffer_data[0] = buffer_data[1];
            buffer_data[1] = temp;
            return buffer_data;
        }
        private async void Timer1_Tick(object sender, EventArgs args)
        {
            if (_s7Client.IsConnected)
            {
                var buffer_send1 = new byte[1];
                buffer_send1 = await _s7Client.ReadBytesAsync(DataType.DataBlock, _DB, 0, 1);
                Sharp7.S7.SetBitAt(buffer_send1, 0, 1, false);
                Sharp7.S7.SetBitAt(buffer_send1, 0, 2, false);
                Sharp7.S7.SetBitAt(buffer_send1, 0, 3, false);
                Sharp7.S7.SetBitAt(buffer_send1, 0, 6, false);
                Sharp7.S7.SetBitAt(buffer_send1, 0, 7, false);
                // Không được chạy bất đồng bộ ở đây 
                // Tác dụng là dùng để reset các biến ĐK
                _s7Client.WriteBytes(DataType.DataBlock, _DB, 0, buffer_send1);
                _timer1.Enabled = false;
            }
            else
            {
                PlcNotConnected?.Invoke();
                _timer1.Enabled = false;
            }
        }

        #endregion
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
                        var data = (EnduranceMachineRawData)await _s7Client.ReadStructAsync(typeof(EnduranceMachineRawData), _DB, 22);
                        testingStruct = data;
                        DataReceived?.Invoke(testingStruct);
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
                    //var data = new EnduranceMachineRawData();
                    //DataReceived.Invoke(data);
                }
                _lock = true;
            }
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
    }
}
