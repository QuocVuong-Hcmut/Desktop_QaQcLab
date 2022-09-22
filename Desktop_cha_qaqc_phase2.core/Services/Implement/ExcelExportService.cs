using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using ProductVertificationDesktopApp.Core.Domain;
using OfficeOpenXml.Drawing;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class ExcelExportService : IExcelExportService
    {
        private ExcelPackage package;
        private ExcelWorksheet worksheet;
        //private readonly IRegularExpressionService _regularExpression;

        public ExcelExportService()
        {
            //this._regularExpression = regularExpression;
        }

        private ServiceResponse ReadExcelFile(string path)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                // mở file excel
                package = new ExcelPackage(new FileInfo(path));

                // lấy ra sheet đầu tiên để thao tác
                worksheet = package.Workbook.Worksheets[0];
                return ServiceResponse.Successful();
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    ErrorCode = "ReadExcel",
                    Message = ex.ToString()
                };
                return ServiceResponse.Failed(error);
            }

        }

        private ServiceResponse EditExcelSoftClose(SoftCloseTest testingMachine, bool mode)
        {
            try
            {
                worksheet.Cells["D6"].Value = testingMachine.ProductName;
                worksheet.Cells["D8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["D8"].Value = testingMachine.StartDate;
                worksheet.Cells["K8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["K8"].Value = testingMachine.EndDate;
                worksheet.Cells["K9"].Value = testingMachine.Note;
                switch (((Domain.Models.Resource.TestPurpose)testingMachine.TestPurpose).ToString())
                {
                    case "Scheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(1);
                        break;
                    case "Unscheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(2);
                        break;
                    case "NewProduct":
                        worksheet.Cells["P9"].Value = Convert.ToString(3);
                        break;
                    case "Other":

                        worksheet.Cells["P9"].Value = Convert.ToString(4);
                        break;
                }
                if (mode)
                {
                    int i = 1;
                    foreach (var sheet in testingMachine.Testsheet)
                    {
                        worksheet.Cells["B" + Convert.ToString(15 + i)].Value = sheet.FallTimeLid;
                        worksheet.Cells["C" + Convert.ToString(15 + i)].Value = sheet.IsBumperLidIntact ? "Đạt" : "Không Đạt";
                        worksheet.Cells["D" + Convert.ToString(15 + i)].Value = sheet.IsBumperLidUnleaked ? "Đạt" : "Không Đạt";
                        worksheet.Cells["E" + Convert.ToString(15 + i)].Value = sheet.IsLidPassed ? "Đạt" : "Không Đạt";
                        worksheet.Cells["F" + Convert.ToString(15 + i)].Value = sheet.FallTimeRing;
                        worksheet.Cells["G" + Convert.ToString(15 + i)].Value = sheet.IsBumperRingIntact ? "Đạt" : "Không Đạt";
                        worksheet.Cells["H" + Convert.ToString(15 + i)].Value = sheet.IsBumperRingUnleaked ? "Đạt" : "Không Đạt";
                        worksheet.Cells["I" + Convert.ToString(15 + i)].Value = sheet.IsRingPassed ? "Đạt" : "Không Đạt";
                        worksheet.Cells["J" + Convert.ToString(15 + i)].Value = sheet.NumberOfError;
                        worksheet.Cells["K" + Convert.ToString(15 + i)].Value = sheet.Note;
                    //    worksheet.Cells["N" + Convert.ToString(15 + i)].Value = sheet.Tester;
                        i++;
                    }
                }
                else
                {
                    for (int k = 0; k < 20; k++)
                    {
                        worksheet.Cells["A" + Convert.ToString(16 + k)].Value = "";
                    }
                    int i = 1;
                    foreach (var sheet in testingMachine.Testsheet)
                    {
                        worksheet.Cells["A" + Convert.ToString(15 + i)].Value = sheet.NumberOfClosing;
                        worksheet.Cells["B" + Convert.ToString(15 + i)].Value = sheet.FallTimeLid;
                        worksheet.Cells["C" + Convert.ToString(15 + i)].Value = sheet.IsBumperLidIntact ? "Đạt" : "Không Đạt";
                        worksheet.Cells["D" + Convert.ToString(15 + i)].Value = sheet.IsBumperLidUnleaked ? "Đạt" : "Không Đạt";
                        worksheet.Cells["E" + Convert.ToString(15 + i)].Value = sheet.IsLidPassed ? "Đạt" : "Không Đạt";
                        worksheet.Cells["F" + Convert.ToString(15 + i)].Value = sheet.FallTimeRing;
                        worksheet.Cells["G" + Convert.ToString(15 + i)].Value = sheet.IsBumperRingIntact ? "Đạt" : "Không Đạt";
                        worksheet.Cells["H" + Convert.ToString(15 + i)].Value = sheet.IsBumperRingUnleaked ? "Đạt" : "Không Đạt";
                        worksheet.Cells["I" + Convert.ToString(15 + i)].Value = sheet.IsRingPassed ? "Đạt" : "Không Đạt";
                        worksheet.Cells["J" + Convert.ToString(15 + i)].Value = sheet.NumberOfError;
                        worksheet.Cells["K" + Convert.ToString(15 + i)].Value = sheet.Note;
                      //  worksheet.Cells["N" + Convert.ToString(15 + i)].Value = sheet.Tester;
                        i++;
                    }
                }

                return ServiceResponse.Successful();
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    ErrorCode = "EditExcel",
                    Message = ex.ToString()
                };
                return ServiceResponse.Failed(error);
            }
        }
        private ServiceResponse EditExcelDeformation(ForcedCloseTest testingMachine)
        {
            try
            {
                worksheet.Cells["D6"].Value = testingMachine.ProductName;
                worksheet.Cells["D8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["D8"].Value = testingMachine.StartDate;
                worksheet.Cells["K8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["K8"].Value = testingMachine.EndDate;
                worksheet.Cells["K9"].Value = testingMachine.Note;
                switch (
                    (testingMachine.TestPurpose))
                {
                    case 0:
                        worksheet.Cells["P9"].Value = Convert.ToString(1);
                        break;
                    case 1:
                        worksheet.Cells["P9"].Value = Convert.ToString(2);
                        break;
                    case 2:
                        worksheet.Cells["P9"].Value = Convert.ToString(3);
                        break;
                    case 3:

                        worksheet.Cells["P9"].Value = Convert.ToString(4);
                        break;
                }
                int i = 1;
                foreach (var sheet in testingMachine.Testsheet)
                {
                    worksheet.Cells["B" + Convert.ToString(15 + i)].Value = sheet.FallTimeLid;
                    worksheet.Cells["C" + Convert.ToString(15 + i)].Value = sheet.IsLidIntact ? "Đạt" : "Không Đạt";
                    worksheet.Cells["D" + Convert.ToString(15 + i)].Value = sheet.IsLidUnleaked ? "Đạt" : "Không Đạt";
                    worksheet.Cells["E" + Convert.ToString(15 + i)].Value = sheet.IsLidPassed ? "Đạt" : "Không Đạt";
                    worksheet.Cells["F" + Convert.ToString(15 + i)].Value = sheet.FallTimeRing;
                    worksheet.Cells["G" + Convert.ToString(15 + i)].Value = sheet.IsRingIntact ? "Đạt" : "Không Đạt";
                    worksheet.Cells["H" + Convert.ToString(15 + i)].Value = sheet.IsRingUnleaked ? "Đạt" : "Không Đạt";
                    worksheet.Cells["I" + Convert.ToString(15 + i)].Value = sheet.IsRingPassed ? "Đạt" : "Không Đạt";
                    worksheet.Cells["J" + Convert.ToString(15 + i)].Value = sheet.NumberOfError;
                    worksheet.Cells["K" + Convert.ToString(15 + i)].Value = sheet.Note;
                //    worksheet.Cells["N" + Convert.ToString(15 + i)].Value = sheet.Tester;
                    i++;
                }

                return ServiceResponse.Successful();
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    ErrorCode = "EditExcel",
                    Message = ex.ToString()
                };
                return ServiceResponse.Failed(error);
            }
        }
        private ServiceResponse EditExcelStaticLoad(StaticLoadTest report)
        {
            try
            {
                worksheet.Cells["D6"].Value = report.ProductName;
                worksheet.Cells["D8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["D8"].Value = report.StartDate;
                worksheet.Cells["K8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["K8"].Value = report.EndDate;
                worksheet.Cells["K9"].Value = report.Note;
                switch (((Domain.Models.Resource.TestPurpose)report.TestPurpose).ToString())
                {
                    case "Scheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(1);
                        break;
                    case "Unscheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(2);
                        break;
                    case "NewProduct":
                        worksheet.Cells["P9"].Value = Convert.ToString(3);
                        break;
                    case "Other":

                        worksheet.Cells["P9"].Value = Convert.ToString(4);
                        break;
                }
                int i = 0;
                foreach (var sheet in report.TestSheets)
                {
                    worksheet.Cells["B" + Convert.ToString(14 + i)].Value = sheet.Status;
                    worksheet.Cells["J" + Convert.ToString(14 + i)].Value = sheet.NumberOfError;
                    worksheet.Cells["K" + Convert.ToString(14 + i)].Value = sheet.Note;
             //       worksheet.Cells["N" + Convert.ToString(14 + i)].Value = sheet.Tester;
                    i++;
                }

                return ServiceResponse.Successful();
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    ErrorCode = "EditExcel",
                    Message = ex.ToString()
                };
                return ServiceResponse.Failed(error);
            }
        }

        private ServiceResponse EditExcelRockTest(RockTest report)
        {
            try
            {
                worksheet.Cells["D6"].Value = report.ProductName;
                worksheet.Cells["D8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["D8"].Value = report.StartDate;
                worksheet.Cells["K8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["K8"].Value = report.EndDate;
                worksheet.Cells["K9"].Value = report.Note;
                switch (((Domain.Models.Resource.TestPurpose)report.TestPurpose).ToString())
                {
                    case "Scheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(1);
                        break;
                    case "Unscheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(2);
                        break;
                    case "NewProduct":
                        worksheet.Cells["P9"].Value = Convert.ToString(3);
                        break;
                    case "Other":

                        worksheet.Cells["P9"].Value = Convert.ToString(4);
                        break;
                }
                int i = 0;
                foreach (var sheet in report.TestSheets)
                {
                    worksheet.Cells["B" + Convert.ToString(14 + i)].Value = sheet.Load;
                    worksheet.Cells["D" + Convert.ToString(14 + i)].Value = sheet.TestedTimes;
                    worksheet.Cells["G" + Convert.ToString(14 + i)].Value = sheet.Passed ? "Đạt" : "Không Đạt";
                    worksheet.Cells["J" + Convert.ToString(14 + i)].Value = sheet.NumberOfError;
                    worksheet.Cells["K" + Convert.ToString(14 + i)].Value = sheet.Note;
                   // worksheet.Cells["N" + Convert.ToString(14 + i)].Value = sheet.Tester;
                    i++;
                }

                return ServiceResponse.Successful();
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    ErrorCode = "EditExcel",
                    Message = ex.ToString()
                };
                return ServiceResponse.Failed(error);
            }
        }

        private ServiceResponse EditExcelCurlingForce(CurlingForceTest report)
        {
            try
            {
                worksheet.Cells["D6"].Value = report.ProductName;
                worksheet.Cells["D8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["D8"].Value = report.StartDate;
                worksheet.Cells["K8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["K8"].Value = report.EndDate;

                worksheet.Cells["K9"].Value = report.Note;
                switch (((Domain.Models.Resource.TestPurpose)(report.TestPurpose)).ToString())
                {
                    case "Scheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(1);
                        break;
                    case "Unscheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(2);
                        break;
                    case "NewProduct":
                        worksheet.Cells["P9"].Value = Convert.ToString(3);
                        break;
                    case "Other":
                        worksheet.Cells["P9"].Value = Convert.ToString(4);
                        break;
                }
                int i = 0;
                foreach (var sheet in report.TestSheet)
                {
                    worksheet.Cells["B" + Convert.ToString(14 + i)].Value = sheet.Load;
                    worksheet.Cells["D" + Convert.ToString(14 + i)].Value = sheet.Duration;
                    worksheet.Cells["G" + Convert.ToString(14 + i)].Value = sheet.DeformationDegree;
                    worksheet.Cells["J" + Convert.ToString(14 + i)].Value = sheet.NumberOfError;
                    worksheet.Cells["K" + Convert.ToString(14 + i)].Value = sheet.Note;
          //          worksheet.Cells["N" + Convert.ToString(14 + i)].Value = sheet.Tester;
                    i++;
                }

                return ServiceResponse.Successful();
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    ErrorCode = "EditExcel",
                    Message = ex.ToString()
                };
                return ServiceResponse.Failed(error);
            }
        }
        private ServiceResponse EditExcelWaterProofing(WaterProofingTest report)
        {
            try
            {
                worksheet.Cells["D6"].Value = report.ProductName;
                worksheet.Cells["D8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["D8"].Value = report.StartDate;
                worksheet.Cells["K8"].Style.Numberformat.Format = "dd-MM-yyyy";
                worksheet.Cells["K8"].Value = report.EndDate;

                worksheet.Cells["K9"].Value = report.Note;
                switch (((Domain.Models.Resource.TestPurpose)(report.TestPurpose)).ToString())
                {
                    case "Scheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(1);
                        break;
                    case "Unscheduled":
                        worksheet.Cells["P9"].Value = Convert.ToString(2);
                        break;
                    case "NewProduct":
                        worksheet.Cells["P9"].Value = Convert.ToString(3);
                        break;
                    case "Other":
                        worksheet.Cells["P9"].Value = Convert.ToString(4);
                        break;
                }
                int i = 0;
                foreach (var sheet in report.TestSheets)
                {
                    worksheet.Cells["B" + Convert.ToString(14 + i)].Value = sheet.Temperature;
                    worksheet.Cells["D" + Convert.ToString(14 + i)].Value = sheet.Duration;
                    worksheet.Cells["G" + Convert.ToString(14 + i)].Value = sheet.Passed? "Đạt" : "Không Đạt";
                    worksheet.Cells["J" + Convert.ToString(14 + i)].Value = sheet.NumberOfError;
                    worksheet.Cells["K" + Convert.ToString(14 + i)].Value = sheet.Note;
          //          worksheet.Cells["N" + Convert.ToString(14 + i)].Value = sheet.Tester;
                    i++;
                }

                return ServiceResponse.Successful();
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    ErrorCode = "EditExcel",
                    Message = ex.ToString()
                };
                return ServiceResponse.Failed(error);
            }
        }

        private async Task<ServiceResponse> ExportExcelFile()
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();
            // chỉ lọc ra các file có định dạng Excel
            //dialog.Filter = "Excel (Excel 2003 (*.xls;*.xlsm)";
            //Excel 2003 | *.xls
            //dialog.Filter="Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls;*.xlsm)|*.xls;*.xlsm";
            dialog.Filter="Excel 2003 | *.xls";
            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
                var file = new FileInfo(filePath);
                try
                {
                    using (package)
                    {
                        //Lưu file lại
                        await package.SaveAsAsync(file);
                    }
                    return ServiceResponse.Successful();
                }
                catch (Exception ex)
                {
                    Error error = new Error
                    {
                        ErrorCode = "ExportExcel",
                        Message = ex.ToString()
                    };
                    return ServiceResponse.Failed(error);
                }
            }
            else
            {
                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                    Error error = new Error
                    {
                        ErrorCode = "ExportExcel",
                        Message = "Đường dẫn không hợp lệ"
                    };
                    return ServiceResponse.Failed(error);
                }
                else
                {
                    Error error = new Error
                    {
                        ErrorCode = "ExportExcel",
                        Message = "Lỗi Lưu file"
                    };
                    return ServiceResponse.Failed(error);
                }
            }

        }

        public async Task<ServiceResponse> ExportSoftClose(string path, SoftCloseTest testingMachine, bool mode)
        {

            var step1 = ReadExcelFile(path);
            var step2 = EditExcelSoftClose(testingMachine, mode);
            var step3 = await ExportExcelFile();
            if (step1.Success != true)
            {
                return step1;
            }
            else
            {
                if (step2.Success != true)
                {
                    return step2;
                }
                else
                {
                    if (step3.Success != true)
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful();
                    }
                }
            }
        }

        public async Task<ServiceResponse> ExportDeformation(string path, Domain.Models.Resource.ForcedCloseTest testingMachine)
        {
            var step1 = ReadExcelFile(path);
            var step2 = EditExcelDeformation(testingMachine);
            var step3 = await ExportExcelFile();
            if (step1.Success != true)
            {
                return step1;
            }
            else
            {
                if (step2.Success != true)
                {
                    return step2;
                }
                else
                {
                    if (step3.Success != true)
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful();
                    }
                }
            }
        }

        public async Task<ServiceResponse> ExportCurlingForce(string path, CurlingForceTest report)
        {
            var step1 = ReadExcelFile(path);
            var step2 = EditExcelCurlingForce(report);
            var step3 = await ExportExcelFile();
            if (step1.Success != true)
            {
                return step1;
            }
            else
            {
                if (step2.Success != true)
                {
                    return step2;
                }
                else
                {
                    if (step3.Success != true)
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful();
                    }
                }
            }
        }
        public async Task<ServiceResponse> ExportStaticLoad(string path, StaticLoadTest report)
        {
            var step1 = ReadExcelFile(path);
            var step2 = EditExcelStaticLoad(report);
            var step3 = await ExportExcelFile();
            if (step1.Success != true)
            {
                return step1;
            }
            else
            {
                if (step2.Success != true)
                {
                    return step2;
                }
                else
                {
                    if (step3.Success != true)
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful();
                    }
                }
            }
        }

        public async Task<ServiceResponse> ExportRockTest(string path, RockTest report)
        {
            var step1 = ReadExcelFile(path);
            var step2 = EditExcelRockTest(report);
            var step3 = await ExportExcelFile();
            if (step1.Success != true)
            {
                return step1;
            }
            else
            {
                if (step2.Success != true)
                {
                    return step2;
                }
                else
                {
                    if (step3.Success != true)
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful();
                    }
                }
            }
        }

        public async Task<ServiceResponse> ExportWaterProofing(string path, WaterProofingTest report)
        {
            var step1 = ReadExcelFile(path);
            var step2 = EditExcelWaterProofing(report);
            var step3 = await ExportExcelFile();
            if (step1.Success != true)
            {
                return step1;
            }
            else
            {
                if (step2.Success != true)
                {
                    return step2;
                }
                else
                {
                    if (step3.Success != true)
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful();
                    }
                }
            }
        }

    }
}


