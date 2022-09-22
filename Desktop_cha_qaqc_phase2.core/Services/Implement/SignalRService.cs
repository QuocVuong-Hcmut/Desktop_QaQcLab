using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using ProductVertificationDesktopApp.Core.Domain;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class SignalRService : ISignalRService
    {
        HubConnection connection;
        public SignalRService()
        {
            ConnectAsync();
        }
        private async void ConnectAsync()
        {
            connection = new HubConnectionBuilder()
                //.WithUrl("https://chaqaqclab.azurewebsites.net/hub/")
                .WithUrl("http://10.84.50.10:8085/hub/")
                .WithAutomaticReconnect()
                .Build();
            try
            {
                await connection.StartAsync();
            }
            catch
            { 
            }
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
        public async Task<ServiceResponse> SoftCloseMonitoringData(SoftCloseMonitorData monitoringData)
        {
            try
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    await connection.SendAsync("SendSoftCloseMonitor", monitoringData);
                    
                    return ServiceResponse.Successful();
                }
                else
                {
                    ConnectAsync();
                    var error = new Error
                    {
                        ErrorCode = "SoftCloseMonitoringData.SignalR",
                        Message = "Lỗi chưa kết nối hub"
                    };
                    return ServiceResponse.Failed(error);
                }
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    ErrorCode = "SoftCloseMonitoringData.SignalR",
                    Message = "Lỗi ReliabilityMonitoringData.SignalR."
                };
                return ServiceResponse.Failed(error);
            }
        }
        public async Task<ServiceResponse> ForcedCloseMonitoringData(ForcedCloseMonitorData monitoringData)
        {
            try
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    await connection.SendAsync("SendForcedCloseMonitor", monitoringData);
                    return ServiceResponse.Successful();
                }
                else
                {
                    ConnectAsync();

                    var error = new Error
                    {
                        ErrorCode = "ForcedCloseMonitoringData.SignalR",
                        Message = "Lỗi chưa kết nối hub"
                    };
                    return ServiceResponse.Failed(error);
                }

            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    ErrorCode = "ReliabilityMonitoringData.SignalR",
                    Message = "Lỗi ReliabilityMonitoringData.SignalR."
                };
                return ServiceResponse.Failed(error);
            }
        }
        public async Task<ServiceResponse> EnduranceMonitoringData(EnduranceMonitorData monitoringData)
        {
            try
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    await connection.SendAsync("SendEnduranceMonitor", monitoringData);
                    return ServiceResponse.Successful();
                }
                else
                {
                    ConnectAsync();
                    var error = new Error
                    {
                        ErrorCode = "EnduranceMonitoringData.SignalR",
                        Message = "Lỗi chưa kết nối hub"
                    };
                    return ServiceResponse.Failed(error);

                }
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    ErrorCode = "EnduranceMonitoringData.SignalR",
                    Message = "Lỗi EnduranceMonitoringData.SignalR."
                };
                return ServiceResponse.Failed(error);
            }
        }
        public async Task<ServiceResponse> WaterProofingMonitorData(WaterProofingMonitorData monitoringData)
        {
            try
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    await connection.SendAsync("SendWaterProofingMonitor", monitoringData);
                    return ServiceResponse.Successful();
                }
                else
                {
                    ConnectAsync();
                    var error = new Error
                    {
                        ErrorCode = "WaterProofingMonitoringData.SignalR",
                        Message = "Lỗi chưa kết nối hub"
                    };
                    return ServiceResponse.Failed(error);

                }
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    ErrorCode = "WaterProofingMonitoringData.SignalR",
                    Message = "Lỗi WaterProofingMonitoringData.SignalR."
                };
                return ServiceResponse.Failed(error);
            }
        }

       
    }
}
