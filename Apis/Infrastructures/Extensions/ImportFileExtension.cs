using Ganss.Excel;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.ImportViewModels;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructures.Extensions
{
    public static class ImportFileExtension
    {
        /// <summary>
        /// Get a list of string with name of the InputTypeSupport
        /// </summary>
        /// <returns></returns>
        public static List<string> GetListExcelFileInputTypeSupport()
        {
            List<string> supportedExcelInputFileType = new()
            {
                ".xlsx",
                ".xlsm",
                ".xlsb",
                ".xltx",
                ".xltm",
                ".xls",
                ".xlt",
                ".xls",
                ".xla",
                ".xlw",
                ".xlr",
                ".xlam"
            };
            return supportedExcelInputFileType;
        }

        /// <summary>
        /// Return true if File Input Type is valid. False if it not.
        /// </summary>
        /// <param name="fileExcel"></param>
        /// <returns></returns>
        public static bool CheckImportRECFileTypeInput(this IFormFile? fileExcel)
        {
            if (fileExcel != null)
            {
                var ListExcelFileInputSupport = GetListExcelFileInputTypeSupport();
                foreach (var inputtype in ListExcelFileInputSupport)
                {
                    if (fileExcel.FileName.EndsWith(inputtype))
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public static IEnumerable<AccountImportViewModel>? GetAccountFromImportFile(this ExcelMapper instanceMapper)
        {
            var listAccount_ImportVM = instanceMapper.Fetch<AccountImportViewModel>();
            if (listAccount_ImportVM.Any()) return listAccount_ImportVM;
            return null;
        }

        public static IEnumerable<ClassImportViewModel>? GetClassFromImportFile(this ExcelMapper instanceMapper)
        {
            var listClass_ImportVM = instanceMapper.Fetch<ClassImportViewModel>().
                    DistinctBy(p => p.RRCode);
            if (listClass_ImportVM.Any()) return listClass_ImportVM;
            return null;
        }


        public static IEnumerable<FresherImportViewModel>? GetFresherFromImportFile(this ExcelMapper instanceMapper)
        {
            var listFresher_ImportVM = instanceMapper.Fetch<FresherImportViewModel>();
            if (listFresher_ImportVM.Any()) return listFresher_ImportVM;
            return null;
        }

        private static string[] VietnameseChars = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string VietnameseConvertToUTF8(this string accountName)
        {
            //Replace và fillter sign of vietnamese chars      
            for (int i = 1; i < VietnameseChars.Length; i++)
            {
                for (int j = 0; j < VietnameseChars[i].Length; j++)
                {
                    accountName = accountName.Replace(VietnameseChars[i][j], VietnameseChars[0][i - 1]);
                }
            }
            return accountName;
        }

        public static void ValidateFreshersFromRECExcelFile(this IEnumerable<FresherImportViewModel> freshersListFromExcelFile)
        {
            foreach (var fresher in freshersListFromExcelFile)
            {
                /* 
                    nameOfFresher = {"Pham","Tran","Huy","Bao"}
                    Get the Last and the first char of other names.
                    Example : BaoPTH
                    Example2: Đinh Minh Thiện => ThienDM
                */
                var nameOfFresher = fresher.FullName.Split(" ");
                var firstNameOfFresher = nameOfFresher.LastOrDefault();
                string firstNameOfAccount = (firstNameOfFresher == null) ? "" : firstNameOfFresher;
                string accountName = firstNameOfAccount;
                foreach (var name in nameOfFresher)
                {
                    if (!name.Equals(firstNameOfFresher))
                    {
                        accountName += name.FirstOrDefault();
                    }
                }
                accountName = accountName.VietnameseConvertToUTF8();
                if (!(fresher.AccountName.Contains(accountName))) 
                    throw new AppException(Constant.EXCEPTION_ACCOUNT_NAME_IS_NOT_VALID + $"Error at Fresher Account: {fresher.AccountName}");
                var propertiesOfRRCode = fresher.RRCode.Split(".");
                /*RRCode : FSO.HCM.FHO.FA.G0.SG_2022.57_4 
                * => FSO [0]
                * => HCM (Get Location from here) [1]
                * => FHO [2]
                * => FA [3]
                * => GO [4]
                * => SG_2022 [5]
                * => 57_4 [6]
                * Count: 7
                */
                if (propertiesOfRRCode.Length != 7 ||
                    propertiesOfRRCode[0].Length > 3 || propertiesOfRRCode[1].Length > 3 || propertiesOfRRCode[2].Length > 3 ||
                    propertiesOfRRCode[3].Length > 2 || propertiesOfRRCode[4].Length > 2 || propertiesOfRRCode[5].Length > 8 ||
                    propertiesOfRRCode[6].Length > 5)
                    throw new AppException(Constant.EXCEPTION_RRCODE_IS_NOT_VALID + $"Error at Fresher Name: {fresher.FullName}");
            }
        }



        public static void ValidateDataImportFromFileExcel(this PackageReponseFromRECFileImportViewModel package)
        {
            package.ListFresherImportViewModel.ValidateFreshersFromRECExcelFile();
        }

        public static string GetVal(this ExcelRange @this)
        {
            if (@this.Merge)
            {
                var cellIdx = @this.Worksheet.GetMergeCellId(@this.Start.Row, @this.Start.Column);
                string mergedCellAddress = @this.Worksheet.MergedCells[cellIdx - 1];
                string firstCellAddress = @this.Worksheet.Cells[mergedCellAddress].Start.Address;
                return @this.Worksheet.Cells[firstCellAddress].Value?.ToString() ?? "";
            }
            else
            {
                return @this.Value?.ToString() ?? "";
            }
        }
        public static string CellsValue(this ExcelWorksheet workSheet, int row, int column)
        {
            return workSheet.Cells[row, column].GetVal();
        }
    }


}
