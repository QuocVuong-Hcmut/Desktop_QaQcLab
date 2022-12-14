using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp7;
using System.Timers;
using S7.Net;
using System.Threading;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using System.Windows;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class LogoForcedClose : ILogoForceCloseService
    {
        private readonly Plc _s7Client;
        private readonly int _memory;
        private readonly string _address;
        private System.Timers.Timer _timer1;
        private bool _lock = true;

        private bool preConnectionStatus = true;
        public LogoForcedClose(string address, int memory)
        {
            _address = address;
            _memory = memory;
            _s7Client = new Plc(CpuType.Logo0BA8, address, 0, 1);
            _timer1 = new System.Timers.Timer
            {
                Interval = 100,
                Enabled = false
            };
            _timer1.Elapsed += Timer1_Tick;
        }
        public event Action<ForcedCloseMachineRawData, byte[], byte[], byte[]> DataReceived;
        public event Action PlcNotConnected;
        // Send1Byte
        public void ControlActive(string s)
        {
            var buffer = new byte[1];
            if ((s == "start") && (_s7Client.IsConnected == true))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 6, true);
                _s7Client.WriteBytes(DataType.DataBlock, 1, 1105, buffer);
                _timer1.Enabled = true;
            }
            if ((s == "stop") && (_s7Client.IsConnected == true))
            {
                Sharp7.S7.SetBitAt(buffer, 0, 6, false);
                Sharp7.S7.SetBitAt(buffer, 0, 7, true);
                _s7Client.WriteBytes(DataType.DataBlock, 1, 1105, buffer);
                _timer1.Enabled = true;
            }
            if ((s == "resetnumber") && (_s7Client.IsConnected == true))
            {
                
                Sharp7.S7.SetBitAt(buffer, 0, 0, true);
                _s7Client.WriteBytes(DataType.DataBlock, 1, 1107, buffer);
                Thread.Sleep(1000);
                Sharp7.S7.SetBitAt(buffer, 0, 0, false);
                _s7Client.WriteBytes(DataType.DataBlock, 1, 1107, buffer);
                _timer1.Enabled = true;
            }
            if (_s7Client.IsConnected == false)
            {
                PlcNotConnected?.Invoke();
            }
        }
        // Send data with2 Bytes
        public void SendData2Byte(int offset, Int16 data)
        {
            if (_s7Client.IsConnected == true)
            {
                byte[] _data = new byte[2];
                _data = BitConverter.GetBytes(data * 100);
                var data_send = MyConvert2Byte(_data);
                _s7Client.WriteBytes(DataType.DataBlock, 1, _memory + offset, data_send);
            }
        }
        public void SendData4Byte(int offset, Int32 data)
        {
            if (_s7Client.IsConnected == true)
            {
                byte[] _data = new byte[4];
                _data = BitConverter.GetBytes(data);
                var data_send = MyConvert4Byte(_data);
                _s7Client.WriteBytes(DataType.DataBlock, 1, _memory + offset, data_send);
            }
            else
            {
                PlcNotConnected?.Invoke();
            }
        }
        public byte[] MyConvert4Byte(byte[] buffer_data)
        {
            byte[] buffer = new byte[4];
            buffer[0] = buffer_data[3];
            buffer[1] = buffer_data[2];
            buffer[2] = buffer_data[1];
            buffer[3] = buffer_data[0];
            return buffer;
        }
        public byte[] MyConvert2Byte(byte[] buffer_data)
        {
            byte temp = buffer_data[0];
            buffer_data[0] = buffer_data[1];
            buffer_data[1] = temp;
            return buffer_data;
        }
        public ForcedCloseMachineRawData Converttodata(byte[] data)
        {
            byte[] TimeCloseSP = new byte[2];
            byte[] TimeClose = new byte[2];
            byte[] TimeOpenSP = new byte[2];
            byte[] TimeOpen = new byte[2];
            byte[] NumberClosingSmooth = new byte[4];
            byte[] NumberClosingSP = new byte[4];
            if (data != null)
            {
                Array.Copy(data, 0, TimeCloseSP, 0, 2);
                Array.Copy(data, 2, TimeClose, 0, 2);
                Array.Copy(data, 4, TimeOpenSP, 0, 2);
                Array.Copy(data, 6, TimeOpen, 0, 2);
                Array.Copy(data, 8, NumberClosingSmooth, 0, 4);
                Array.Copy(data, 14, NumberClosingSP, 0, 4);
                ForcedCloseMachineRawData componentResult = new ForcedCloseMachineRawData();
                componentResult.TimeCloseSP = Convert2Byte(TimeCloseSP);
                componentResult.TimeClosePV = Convert2Byte(TimeClose);
                componentResult.TimeOpenSP = Convert2Byte(TimeOpenSP);
                componentResult.TimeOpenPV = Convert2Byte(TimeOpen);
                componentResult.ClosingSmoothTime = (float)(Convert4ByteToFloat(NumberClosingSmooth));
                componentResult.NumberOfClosingSP = Convert4ByteToInt(NumberClosingSP);
                return componentResult;
            }
            else
            {
                ForcedCloseMachineRawData componentResult = new ForcedCloseMachineRawData();
                return componentResult;
            }
        }
        private int Convert4ByteToInt(byte[] buffer_data)
        {
            byte[] buffer = new byte[4];
            buffer[0] = buffer_data[3];
            buffer[1] = buffer_data[2];
            buffer[2] = buffer_data[1];
            buffer[3] = buffer_data[0];
            int data = BitConverter.ToInt32(buffer, 0);
            return data;
        }
        private float Convert4ByteToFloat(byte[] buffer_data)
        {
            byte[] buffer = new byte[4];
            buffer[0] = buffer_data[3];
            buffer[1] = buffer_data[2];
            buffer[2] = buffer_data[1];
            buffer[3] = buffer_data[0];
            float data = (float)(BitConverter.ToUInt32(buffer, 0)) / 100;
            return data;
        }
        private int Convert2Byte(byte[] buffer_data)
        {
            byte temp = buffer_data[0];
            buffer_data[0] = buffer_data[1];
            buffer_data[1] = temp;
            Int16 data = BitConverter.ToInt16(buffer_data, 0);
            int _data = (int)data / 100;
            return _data;
        }
        // nhận sự kiện đọc LOGO đọc 16 BIT auto
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

                        var bufferdata = await _s7Client.ReadBytesAsync(DataType.DataBlock, 1, 0, 18);
                        var data = Converttodata(bufferdata);
                        byte[] CurrentNumberClosingBuffer = await _s7Client.ReadBytesAsync(DataType.DataBlock, 1, 100, 4);
                        data.NumberOfClosingPV = Convert4ByteToInt(CurrentNumberClosingBuffer);

                        var bufferI = await _s7Client.ReadBytesAsync(DataType.Input, 1, 0, 1); // Read I 
                        var bufferQ = await _s7Client.ReadBytesAsync(DataType.Output, 1, 0, 2);// read Q 
                        var bufferM = await _s7Client.ReadBytesAsync(DataType.DataBlock, 1, 1104, 1);// read M 

                        DataReceived?.Invoke(data, bufferM, bufferQ, bufferI);

                    }
                    catch
                    {
                        //PlcNotConnected?.Invoke();
                    }
                }
                else
                {
                    //goi lai ham connect de chac chan no disconnected
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
                }
                _lock = true;
            }
        }
        private async void Timer1_Tick(object sender, EventArgs args)
        {
            byte[] buffer = new byte[1];
            buffer[0] = 0;
            await _s7Client.WriteBytesAsync(DataType.DataBlock, 1, 1105, buffer);
            _timer1.Enabled = false;
        }
    }
}

