using AutoMapper;
using Newtonsoft.Json;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using ProductVertificationDesktopApp.Core.Domain;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using System.Collections.ObjectModel;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{

    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        //private const string serverUrl = "https://localhost:7116"; // Local host
        //private const string serverUrl = "https://chaqaqclab.azurewebsites.net/"; // azure
        private const string serverUrl = "http://10.84.50.10:8085"; // Server nhà máy
        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(30);
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }
        
        #region SoftClose
        public async Task<ServiceResponse> PostSoftCloseReport(SoftCloseTestPOST settingMachine)
        {
            ServiceResponse result;
            var json = JsonConvert.SerializeObject(settingMachine);

            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                string url = $"{serverUrl}/api/SoftCloseTests";

                var response = await _httpClient.PostAsync(url, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.SettingMachine.Post", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<Tester>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                result = ServiceResponse.Failed(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResourceResponse<QueryResult<SoftCloseTestGET>>> GetSoftCloseReport(DateTime? startTime, DateTime? stopTime)
        {
            ServiceResourceResponse<QueryResult<SoftCloseTestGET>> result;
            string queryString = "";
            if (startTime != null)
            {
                string startTimestring = startTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StartTime=" + startTimestring;
            }
            if (stopTime != null && startTime.Value.Date != stopTime.Value.Date)
            {
                string stopTimestring = stopTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StopTime=" + stopTimestring;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/SoftCloseTests/?" + queryString);
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var reportRiliability = JsonConvert.DeserializeObject<QueryResult<SoftCloseTestGET>>(responseBody);
                        result = new ServiceResourceResponse<QueryResult<SoftCloseTestGET>>(reportRiliability);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<QueryResult<SoftCloseTestGET>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<SoftCloseTestGET>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<SoftCloseTestGET>>(error);
                return result;
            }
            return result;
        }

        #endregion
        #region ForcedClose
        public async Task<ServiceResponse> PostForcedCloseReport(ForcedCloseTestPOST settingMachine)
        {
            ServiceResponse result;
            var json = JsonConvert.SerializeObject(settingMachine);

            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string url = $"{serverUrl}/api/ForcedCloseTests";

                var response = await _httpClient.PostAsync(url, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.SettingMachine.Post", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<Tester>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                result = ServiceResponse.Failed(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResourceResponse<QueryResult<ForcedCloseTestGET>>> GetForcedCloseReport(DateTime? startTime, DateTime? stopTime)
        {
            ServiceResourceResponse<QueryResult<ForcedCloseTestGET>> result;
            string queryString = "";
            if (startTime != null)
            {
                string startTimestring = startTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StartTime=" + startTimestring;
            }
            if (stopTime != null && startTime.Value.Date != stopTime.Value.Date)
            {
                string stopTimestring = stopTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StopTime=" + stopTimestring;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/ForcedCloseTests/?" + queryString);
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var reportRiliability = JsonConvert.DeserializeObject<QueryResult<ForcedCloseTestGET>>(responseBody);
                        result = new ServiceResourceResponse<QueryResult<ForcedCloseTestGET>>(reportRiliability);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<QueryResult<ForcedCloseTestGET>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<ForcedCloseTestGET>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<ForcedCloseTestGET>>(error);
                return result;
            }
            return result;
        }
        #endregion
        #region StaticLoad
        public async Task<ServiceResponse> PostStaticLoad(StaticLoadTestPOST report)
        {
            ServiceResponse result;
            var json = JsonConvert.SerializeObject(report);

            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                string url = $"{serverUrl}/api/StaticLoadTests";

                var response = await _httpClient.PostAsync(url, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.SettingMachine.Post", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<Tester>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                result = ServiceResponse.Failed(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResourceResponse<QueryResult<StaticLoadTestGET>>> GetStaticLoadReport(DateTime? startTime, DateTime? stopTime)
        {
            ServiceResourceResponse<QueryResult<StaticLoadTestGET>> result;
            string queryString = "";
            if (startTime != null)
            {
                string startTimestring = startTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StartTime=" + startTimestring;
            }
            if (stopTime != null && startTime.Value.Date != stopTime.Value.Date)
            {
                string stopTimestring = stopTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StopTime=" + stopTimestring;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/StaticLoadTests/?" + queryString);
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var report = JsonConvert.DeserializeObject<QueryResult<StaticLoadTestGET>>(responseBody);
                        result = new ServiceResourceResponse<QueryResult<StaticLoadTestGET>>(report);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<QueryResult<StaticLoadTestGET>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<StaticLoadTestGET>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<StaticLoadTestGET>>(error);
                return result;
            }
            return result;
        }
        #endregion
        #region CurlingForce
        public async Task<ServiceResponse> PostCurlingForce(CurlingForceTestPOST report)
        {
            ServiceResponse result;
            var json = JsonConvert.SerializeObject(report);

            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                string url = $"{serverUrl}/api/CurlingForceTests";

                var response = await _httpClient.PostAsync(url, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.SettingMachine.Post", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<Tester>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                result = ServiceResponse.Failed(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResourceResponse<QueryResult<CurlingForceTestGET>>> GetCurlingForceReport(DateTime? startTime, DateTime? stopTime)
        {
            ServiceResourceResponse<QueryResult<CurlingForceTestGET>> result;
            string queryString = "";
            if (startTime != null)
            {
                string startTimestring = startTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StartTime=" + startTimestring;
            }
            if (stopTime != null && startTime.Value.Date != stopTime.Value.Date)
            {
                string stopTimestring = stopTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StopTime=" + stopTimestring;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/CurlingForceTests/?" + queryString);
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var report = JsonConvert.DeserializeObject<QueryResult<CurlingForceTestGET>>(responseBody);
                        result = new ServiceResourceResponse<QueryResult<CurlingForceTestGET>>(report);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<QueryResult<CurlingForceTestGET>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<CurlingForceTestGET>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<CurlingForceTestGET>>(error);
                return result;
            }
            return result;
        }
        #endregion
        #region Rocktests
        public async Task<ServiceResponse> PostRockTest(RockTestPOST report)
        {
            ServiceResponse result;
            var json = JsonConvert.SerializeObject(report);

            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                string url = $"{serverUrl}/api/RockTests";

                var response = await _httpClient.PostAsync(url, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.SettingMachine.Post", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<Tester>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                result = ServiceResponse.Failed(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResourceResponse<QueryResult<RockTestGET>>> GetRockTestReport(DateTime? startTime, DateTime? stopTime)
        {
            ServiceResourceResponse<QueryResult<RockTestGET>> result;
            string queryString = "";
            if (startTime != null)
            {
                string startTimestring = startTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StartTime=" + startTimestring;
            }
            if (stopTime != null && startTime.Value.Date != stopTime.Value.Date)
            {
                string stopTimestring = stopTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StopTime=" + stopTimestring;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/RockTests/?" + queryString);
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var reportRockTest = JsonConvert.DeserializeObject<QueryResult<RockTestGET>>(responseBody);
                        result = new ServiceResourceResponse<QueryResult<RockTestGET>>(reportRockTest);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<QueryResult<RockTestGET>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<RockTestGET>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<RockTestGET>>(error);
                return result;
            }
            return result;
        }
        #endregion

        #region WaterProofing
        public async Task<ServiceResponse> PostWaterProofing(WaterProofingTestPOST report)
        {
            ServiceResponse result;
            var json = JsonConvert.SerializeObject(report);

            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                string url = $"{serverUrl}/api/WaterProofingTests";

                var response = await _httpClient.PostAsync(url, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.SettingMachine.Post", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<Tester>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                result = ServiceResponse.Failed(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResourceResponse<QueryResult<WaterProofingTestGET>>> GetWaterProofingReport(DateTime? startTime, DateTime? stopTime)
        {
            ServiceResourceResponse<QueryResult<WaterProofingTestGET>> result;
            string queryString = "";
            if (startTime != null)
            {
                string startTimestring = startTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StartTime=" + startTimestring;
            }
            if (stopTime != null && startTime.Value.Date != stopTime.Value.Date)
            {
                string stopTimestring = stopTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StopTime=" + stopTimestring;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/WaterProofingTests/?" + queryString);
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var reportRockTest = JsonConvert.DeserializeObject<QueryResult<WaterProofingTestGET>>(responseBody);
                        result = new ServiceResourceResponse<QueryResult<WaterProofingTestGET>>(reportRockTest);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<QueryResult<WaterProofingTestGET>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<WaterProofingTestGET>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<WaterProofingTestGET>>(error);
                return result;
            }
            return result;
        }
        #endregion
        #region Warning 
        public async Task<ServiceResponse> PostSoftCloseWarning(AlarmCode warningcode)
        {
            //token ....
            if (/*string.IsNullOrEmpty(token) == true*/ false)
            {
                Error error = new Error()
                {
                    Message = null,
                    ErrorCode = null
                };
                return ServiceResponse.Failed(error);
            }
            else
            {
                ServiceResponse result;
                var json = JsonConvert.SerializeObject(warningcode);

                try
                {
                    string url = $"{serverUrl}/api/SoftCloseAlarm";
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(url, content);

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            //EndShiftStatus = true;
                            return ServiceResponse.Successful();
                        case HttpStatusCode.BadRequest:
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                            var error = new Error("Api.SettingMachine.Post", serverError.Message);
                            return ServiceResponse.Failed(error);
                        case HttpStatusCode.Unauthorized:
                            error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                            return ServiceResponse.Failed(error);
                        default:
                            error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                            return ServiceResponse.Failed(error);
                    }
                }

                catch (HttpRequestException ex)
                {
                    var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                    result = new ServiceResourceResponse<Tester>(error);
                }
                catch (Exception ex)
                {
                    var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                    result = ServiceResponse.Failed(error);
                    return result;
                }
                return result;
            }

        }
        public async Task<ServiceResponse> PostForcedCloseWarning(AlarmCode warningcode)
        {
            if (/*string.IsNullOrEmpty(token) == true*/ false)
            {
                Error error = new Error()
                {
                    Message = null,
                    ErrorCode = null
                };
                return ServiceResponse.Failed(error);
            }
            else
            {
                ServiceResponse result;
                var json = JsonConvert.SerializeObject(warningcode);

                try
                {
                    string url = $"{serverUrl}/api/ForcedCloseAlarm";
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(url, content);

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            //EndShiftStatus = true;
                            return ServiceResponse.Successful();
                        case HttpStatusCode.BadRequest:
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                            var error = new Error("Api.SettingMachine.Post", serverError.Message);
                            return ServiceResponse.Failed(error);
                        case HttpStatusCode.Unauthorized:
                            error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                            return ServiceResponse.Failed(error);
                        default:
                            error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                            return ServiceResponse.Failed(error);
                    }
                }

                catch (HttpRequestException ex)
                {
                    var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                    result = new ServiceResourceResponse<Tester>(error);
                }
                catch (Exception ex)
                {
                    var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                    result = ServiceResponse.Failed(error);
                    return result;
                }
                return result;
            }

        }
        public async Task<ServiceResponse> PostEnduranceWarning(AlarmCode warningcode)
        {
            if (/*string.IsNullOrEmpty(token) == true*/ false)
            {
                Error error = new Error()
                {
                    Message = null,
                    ErrorCode = null
                };
                return ServiceResponse.Failed(error);
            }
            else
            {
                ServiceResponse result;
                var json = JsonConvert.SerializeObject(warningcode);
                try
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    string url = $"{serverUrl}/api/EnduranceAlarm";
                    var response = await _httpClient.PostAsync(url, content);

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            //EndShiftStatus = true;
                            return ServiceResponse.Successful();
                        case HttpStatusCode.BadRequest:
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                            var error = new Error("Api.SettingMachine.Post", serverError.Message);
                            return ServiceResponse.Failed(error);
                        case HttpStatusCode.Unauthorized:
                            error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                            return ServiceResponse.Failed(error);
                        default:
                            error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                            return ServiceResponse.Failed(error);
                    }
                }

                catch (HttpRequestException ex)
                {
                    var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                    result = new ServiceResourceResponse<Tester>(error);
                }
                catch (Exception ex)
                {
                    var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                    result = ServiceResponse.Failed(error);
                    return result;
                }
                return result;
            }

        }
        public async Task<ServiceResponse> PostWaterProofingWarning(AlarmCode warningcode)
        {
            if (/*string.IsNullOrEmpty(token) == true*/ false)
            {
                Error error = new Error()
                {
                    Message = null,
                    ErrorCode = null
                };
                return ServiceResponse.Failed(error);
            }
            else
            {
                ServiceResponse result;
                var json = JsonConvert.SerializeObject(warningcode);

                try
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    string url = $"{serverUrl}/api/WaterProofingAlarm";
                    var response = await _httpClient.PostAsync(url, content);

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            //EndShiftStatus = true;
                            return ServiceResponse.Successful();
                        case HttpStatusCode.BadRequest:
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                            var error = new Error("Api.SettingMachine.Post", serverError.Message);
                            return ServiceResponse.Failed(error);
                        case HttpStatusCode.Unauthorized:
                            error = new Error("Api.SettingMachine.Post", "Vui lòng đăng nhập.");
                            return ServiceResponse.Failed(error);
                        default:
                            error = new Error("Api.SettingMachine.Post", "Đã có lỗi xảy ra.");
                            return ServiceResponse.Failed(error);
                    }
                }

                catch (HttpRequestException ex)
                {
                    var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                    result = new ServiceResourceResponse<Tester>(error);
                }
                catch (Exception ex)
                {
                    var error = new Error("Api.SettingMachine.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                    result = ServiceResponse.Failed(error);
                    return result;
                }
                return result;
            }

        }
        public async Task<ServiceResourceResponse<QueryResult<AlarmCode>>> GetWarning(DateTime? startTime, DateTime? stopTime, string option)
        {
            ServiceResourceResponse<QueryResult<AlarmCode>> result;
            string queryString = "";
            if (startTime != null)
            {
                string startTimestring = startTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StartTime=" + startTimestring;
            }
            if (stopTime != null && startTime.Value.Date != stopTime.Value.Date)
            {
                string stopTimestring = stopTime.Value.ToString("yyyy-MM-dd");
                queryString += "&StopTime=" + stopTimestring;
            }
            try
            {
                string url = null;
                if (option.ToLower() == "softclose")
                {
                    url = $"{serverUrl}/api/softclosealarm/?" + queryString;
                }
                if (option.ToLower() == "forcedclose")
                {
                    url = $"{serverUrl}/api/forcedclosealarm/?" + queryString;
                }
                if (option.ToLower() == "endurance")
                {
                    url = $"{serverUrl}/api/endurancealarm/?" + queryString;
                }
                if (option.ToLower() == "waterproofing")
                {
                    url = $"{serverUrl}/api/waterproofingalarm/?" + queryString;
                }
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var warningcode = JsonConvert.DeserializeObject<QueryResult<AlarmCode>>(responseBody);
                        result = new ServiceResourceResponse<QueryResult<AlarmCode>>(warningcode);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<QueryResult<AlarmCode>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<AlarmCode>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<QueryResult<AlarmCode>>(error);
                return result;
            }
            return result;
        }
        #endregion
        #region Product
        public async Task<ServiceResponse> PostProduct(Product product)
        {
            ServiceResponse result;
            var json = JsonConvert.SerializeObject(product);

            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                string url = $"{serverUrl}/api/products";

                var response = await _httpClient.PostAsync(url, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.Product.Post", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.Product.Post", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.Product.Post", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<Tester>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Post", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                result = ServiceResponse.Failed(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResourceResponse<ObservableCollection<Product>>> GetProduct()
        {
            ServiceResourceResponse<ObservableCollection<Product>> result;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Products");
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var productlist = JsonConvert.DeserializeObject<ObservableCollection<Product>>(responseBody);
                        result = new ServiceResourceResponse<ObservableCollection<Product>>(productlist);
                        return result;
                    default:
                        var error = new Error("Api.Product.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<ObservableCollection<Product>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<ObservableCollection<Product>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<ObservableCollection<Product>>(error);
                return result;
            }
            return result;
        }
        public async Task<ServiceResponse> DeleteProductByID(Product product)
        {
            try
            {
                string productid = product.Id;
                string url = $"{serverUrl}/api/Products/?productid={productid}";
                var response = await _httpClient.DeleteAsync(url);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        //EndShiftStatus = true;
                        return ServiceResponse.Successful();
                    case HttpStatusCode.BadRequest:
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var serverError = JsonConvert.DeserializeObject<ServerError>(responseBody);
                        var error = new Error("Api.Product.Delete", serverError.Message);
                        return ServiceResponse.Failed(error);
                    case HttpStatusCode.Unauthorized:
                        error = new Error("Api.Product.Delete", "Vui lòng đăng nhập.");
                        return ServiceResponse.Failed(error);
                    default:
                        error = new Error("Api.Product.Delete", "Đã có lỗi xảy ra.");
                        return ServiceResponse.Failed(error);
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                return ServiceResponse.Failed(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Product.Delete", $"Đã có lỗi xảy ra. Không thể gửi dữ liệu về server được vì: {ex.Message}");
                return ServiceResponse.Failed(error);
                
            }
        }
        #endregion
        #region Tester
        public async Task<ServiceResourceResponse<ObservableCollection<Tester>>> GetTester()
        {
            ServiceResourceResponse<ObservableCollection<Tester>> result;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Testers");
                string responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var testerlist = JsonConvert.DeserializeObject<ObservableCollection<Tester>>(responseBody);
                        result = new ServiceResourceResponse<ObservableCollection<Tester>>(testerlist);
                        return result;
                    default:
                        var error = new Error("Api.Tester.Get", "Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server.");
                        result = new ServiceResourceResponse<ObservableCollection<Tester>>(error);
                        return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var error = new Error("Api.Connection", $"Đã có lỗi xảy ra. Không thể kết nối được với server vì: {ex.Message}");
                result = new ServiceResourceResponse<ObservableCollection<Tester>>(error);
            }
            catch (Exception ex)
            {
                var error = new Error("Api.Tester.Get", $"Đã có lỗi xảy ra. Không thể truy xuất dữ liệu từ server vì: {ex.Message}");
                result = new ServiceResourceResponse<ObservableCollection<Tester>>(error);
                return result;
            }
            return result;
        }
        #endregion
    }
}