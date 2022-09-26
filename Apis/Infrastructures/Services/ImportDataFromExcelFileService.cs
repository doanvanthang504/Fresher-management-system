using Application;
using Application.Interfaces;
using Application.SeedData;
using Domain.Entities;
using Ganss.Excel;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.ImportViewModels;
using Infrastructures.Extensions;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Services
{

    public class ImportDataFromExcelFileService : IImportDataFromExcelFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImportDataFromExcelFileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PackageReponseFromRECFileImportViewModel?>
            GetDataFromRECExcelFileAsync(IFormFile? fileExcel)
        {
            if (fileExcel.CheckImportRECFileTypeInput())
            {
                using var file = fileExcel.OpenReadStream();
                var instanceMapper = new ExcelMapper(file);
                if (instanceMapper == null) return null;
                var listAccountImportVM = instanceMapper.GetAccountFromImportFile();
                var listClassImportVM = instanceMapper.GetClassFromImportFile();
                var listFresherImportVM = instanceMapper.GetFresherFromImportFile();

                if (listAccountImportVM == null || listClassImportVM == null ||
               listFresherImportVM == null) return null;

                //encapsule data into a package to reponse back
                var packageDataReponse = new PackageReponseFromRECFileImportViewModel()
                {
                    ListAccountImportViewModel = listAccountImportVM,
                    ListClassImportViewModel = listClassImportVM,
                    ListFresherImportViewModel = listFresherImportVM
                };
                return await CreateClassCodeFromPackageDataReponseAsync(packageDataReponse);
            }
            return null;
        }

        public static int YearWithTwoNumber() => DateTime.Now.Year - 2000;

        public async Task<PackageReponseFromRECFileImportViewModel>
            CreateClassCodeFromPackageDataReponseAsync(PackageReponseFromRECFileImportViewModel package)
        {
            var fresherFromPackageList = package.ListFresherImportViewModel.ToList();
            var classFresherFromPackageList = package.ListClassImportViewModel.ToList();
            var fresherFromDBList = await _unitOfWork.FresherRepository.GetAllAsync();
            var classFresherFromDBList = await _unitOfWork.ClassFresherRepository.GetAllAsync();
            var locationsList = await DataInitializer.SeedDataAsync<ClassLocation>();
            var jobRanksList = await DataInitializer.SeedDataAsync<FreshersJobRank>();
            var majorList = await DataInitializer.SeedDataAsync<FresherMajor>();
            package.ValidateDataImportFromFileExcel();
            var listClassCodeCreateAtThisImport = new Dictionary<string, int>();
            if (jobRanksList == null || majorList == null) return package;
            fresherFromPackageList.ToList().ForEach(s => s.Major = s.Major.Split("-")[1]);
            foreach (var newclass in classFresherFromPackageList)
            {
                var freshersInformationToCreateClass =
                    fresherFromPackageList.FirstOrDefault(p => p.RRCode.Equals(newclass.RRCode));
                if (freshersInformationToCreateClass == null || newclass.RRCode == null) return package;

                var getClassCodeIfClassFresherExistInDB = classFresherFromDBList.FirstOrDefault(p => p.RRCode.Equals(newclass.RRCode));
                if (getClassCodeIfClassFresherExistInDB != null)
                {
                    newclass.ClassCode = getClassCodeIfClassFresherExistInDB.ClassCode;
                    fresherFromPackageList.Where(p => p.RRCode.Equals(newclass.RRCode)).ToList().
                        ForEach(s => s.ClassCode = newclass.ClassCode);
                }
                else
                {
                    /*RRCode : FSO.HCM.FHO.FA.G0.SG_2022.57_4 
                * => FSO 
                * => HCM (Get Location from here)
                * => FHO
                * => FA
                * => GO
                * => SG_2022
                * => 57_4
                */
                    var propertiesOfRRCode = newclass.RRCode.Split(".");
                    var classSkill = freshersInformationToCreateClass.Skill.ToUpper();

                    var getClassJobRankInJson = jobRanksList.
                        Find(p => p.JobRankName.Equals(freshersInformationToCreateClass.JobRank));
                    var classJobRank = (getClassJobRankInJson == null) ? null : getClassJobRankInJson.JobRankCode;

                    //HCM22_FR_NET_
                    // Count if this Class Code has existed in db 
                    string classCode = $"{propertiesOfRRCode[1]}{YearWithTwoNumber()}_" +
                                                             $"{classJobRank}_{classSkill}_";

                    var indexInt = classFresherFromDBList.Count(p => p.ClassCode.Contains(classCode)) + 1 +
                        listClassCodeCreateAtThisImport.Count(p => p.Key.Contains(classCode));
                    var indexString = (indexInt < 9) ? $"0{indexInt}" : $"{indexInt}";

                    classCode += indexString;
                    listClassCodeCreateAtThisImport.Add(classCode, indexInt);
                    var getLocationName = locationsList.
                        Find(p => p.LocationCode.Equals(propertiesOfRRCode[1]));
                    newclass.Location = (getLocationName == null) ? null : getLocationName.LocationName;
                    newclass.ClassCode = classCode;

                    fresherFromPackageList.Where(p => p.RRCode.Equals(newclass.RRCode)).ToList().
                        ForEach(s => s.ClassCode = classCode);
                }
            }
            package.ListFresherImportViewModel = fresherFromPackageList;
            package.ListClassImportViewModel = classFresherFromPackageList;
            return package;
        }
        public List<TrainingScheduleImportViewModel> GetPlansFromScheduleOfExcelFile(ExcelWorksheet workSheet)
        {
            List<TrainingScheduleImportViewModel> trainingScheduleList = new List<TrainingScheduleImportViewModel>();
            for (int row = workSheet.Dimension.Start.Row + 2; row <= workSheet.Dimension.End.Row - 7; row++)
            {
                var trainingUnitColumn = workSheet.Dimension.Start.Column;
                var trainingChapterColumn = workSheet.Dimension.Start.Column + 1;
                var dayColumn = workSheet.Dimension.Start.Column + 2;
                var lectureColumn = workSheet.Dimension.Start.Column + 3;
                var contentColumn = workSheet.Dimension.Start.Column + 4;
                var learningObjectivesColumn = workSheet.Dimension.Start.Column + 5;
                var deliveryTypeColumn = workSheet.Dimension.Start.Column + 6;
                var durationColumn = workSheet.Dimension.Start.Column + 7;
                var generalNotesColumn = workSheet.Dimension.Start.Column + 8;

                var trainingUnitName = (workSheet.Cells[row, trainingUnitColumn].GetVal() == null) ? "" : workSheet.CellsValue(row, trainingUnitColumn);
                var trainingChapterName = (workSheet.Cells[row, trainingChapterColumn + 1].GetVal() == null) ? "" : workSheet.CellsValue(row, trainingChapterColumn);
                var day = (workSheet.Cells[row, dayColumn + 2].GetVal() == null) ? "" : workSheet.CellsValue(row, dayColumn);
                var lecture = (workSheet.Cells[row, lectureColumn + 3].Value == null) ? "" : workSheet.CellsValue(row, lectureColumn);
                var content = (workSheet.Cells[row, contentColumn + 4].Value == null) ? "" : workSheet.CellsValue(row, contentColumn);
                var learningObjectives = (workSheet.Cells[row, learningObjectivesColumn + 5].Value == null) ? "" : workSheet.CellsValue(row, learningObjectivesColumn);
                var deliveryType = (workSheet.Cells[row, deliveryTypeColumn + 6].Value == null) ? "" : workSheet.CellsValue(row, deliveryTypeColumn);
                var duration = (workSheet.Cells[row, durationColumn + 7].Value == null) ? 0 : Double.Parse(workSheet.CellsValue(row, durationColumn));
                var generalNotes = (workSheet.Cells[row, generalNotesColumn + 8].GetVal() == null) ? "" : workSheet.CellsValue(row, generalNotesColumn);

                var trainingSchedule = new TrainingScheduleImportViewModel()
                {
                    TrainingChapterName = trainingChapterName,
                    TrainingUnitName = trainingUnitName,
                    Day = day,
                    Lecture = lecture,
                    Content = content,
                    LearningObjectives = learningObjectives,
                    DeliveryType = deliveryType,
                    Duration = duration,
                    GeneralNotes = generalNotes,
                };

                trainingScheduleList.Add(trainingSchedule);
            }
            return trainingScheduleList;
        }

        public List<TrainingScheduleImportViewModel?>
            GetDataFromTrainingScheduleExcelFile(IFormFile? fileExcel, string scheduleSheetName)
        {
            if (fileExcel.CheckImportRECFileTypeInput())
            {
                using (var file = fileExcel.OpenReadStream())
                {
                    var package = new ExcelPackage(file);

                    ExcelWorksheet workSheet = package.Workbook.Worksheets[scheduleSheetName];
                    if (workSheet == null) throw new AppNotFoundException(Constant.EXCEPTION_SHEETNAME_DOESNT_EXIST);
                    var GetSchedulePlans = GetPlansFromScheduleOfExcelFile(workSheet);
                    return GetSchedulePlans;
                }
            }
            return null;
        }

        private async void ValidateDataFromScoreImportFile(IEnumerable<ScoreImportViewModel> fresherScoreListFromExcelFile)
        {
            var freshers = await _unitOfWork.FresherRepository.GetAllAsync();
            var modules = await _unitOfWork.ModuleRepository.GetAllAsync();
            var classfreshers = await _unitOfWork.ClassFresherRepository.GetAllAsync();


            foreach (var score in fresherScoreListFromExcelFile)
            {
                if (modules.Any(p => p.ModuleName == score.ModuleName)) throw new AppException(Constant.EXCEPTION_MODULE_NAME_IS_NOT_VALID);
                if (score.ModuleScore >= 10 && score.ModuleScore <= 0) throw new AppException(Constant.EXCEPTION_MODULE_SCORE_IS_NOT_VALID);
                if (freshers.Any(p => p.AccountName == score.AccountName) == false ||
                    freshers.Any(p => p.ClassFresherId.Equals(score.ClassId)) == false ||
                    freshers.Any(p => (p.LastName + p.FirstName).Equals(score.FresherName)) == false ||
                    freshers.Any(p => p.Id.Equals(score.FresherId)) == false) throw new AppException(Constant.EXCEPTION_FRESHER_IS_NOT_VALID);


                var isTypeScoreNumber = score.TypeScore.All(p => Char.IsNumber(p));
                if (isTypeScoreNumber == false) throw new AppException(Constant.EXCEPTION_TYPE_SCORE_IS_NOT_NUMBER);
                var typeScore = Convert.ToInt16(score.TypeScore);
                if (typeScore < 0 || typeScore >= 3) throw new AppException(Constant.EXCEPTION_TYPE_SCORE_IS_NOT_EXIST);

            }
        }

        public List<ScoreImportViewModel>? GetScoreFromExcelFile(IFormFile? fileExcel)
        {
            if (fileExcel.CheckImportRECFileTypeInput())
            {
                using var file = fileExcel.OpenReadStream();
                var instanceMapper = new ExcelMapper(file);
                if (instanceMapper != null)
                {
                    var listScore = instanceMapper.Fetch<ScoreImportViewModel>();
                    ValidateDataFromScoreImportFile(listScore);
                    if (listScore.Any()) return listScore.ToList();
                }
            }
            return null;
        }
    }
}
