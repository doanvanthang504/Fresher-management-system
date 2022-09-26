using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels.MonthResultViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MonthResultService : IMonthResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const double WeightedNumberAcademark = 0.7;
        private const double WeightedNumberDisciplinary = 0.3;
        private const int WorkingTimeInDay = 8;
        public MonthResultService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<MonthResultViewModel>> GetMonthResultByClassId(Guid classId,
                                                                               int month,
                                                                               int year)
        {

            var moduleResults = await _unitOfWork.ModuleResultRepository
                                          .GetModuleResultsByFillterAsync
                                          (x => x.ClassId == classId 
                                          && x.CreationDate.Month == month 
                                          && x.CreationDate.Year == year);

            var planInformation = await _unitOfWork.PlanInformationRepository
                                                    .GetByClassIdAsync(classId);

            var weightedNumbers = planInformation?.ToDictionary
                                            (x => x.ModuleName, y => (y.Duration * WorkingTimeInDay));

            var bonusAndPenaltyScore = await _unitOfWork.ScoreRepository
                                                        .GetScoresByFillterAsync
                                                        (x => x.ClassId == classId 
                                                        && x.CreationDate.Month == month
                                                        && x.CreationDate.Year == year 
                                                        && x.TypeScore == TypeScoreEnum.Bonus 
                                                        || x.TypeScore == TypeScoreEnum.Penalty);

            var disciplinaryScoreMock = 10d;//get from ReportAttendence
            
            var gpaFreshers = moduleResults
                              .GroupBy(x => x.FresherId)
                              .ToDictionary
                              (x => x.Key,
                               y => CalculateGPA(CalculateAcademark,
                                                 y.ToList(),
                                                 weightedNumbers,
                                                 bonusAndPenaltyScore?.GetScore(y.Key, TypeScoreEnum.Bonus),
                                                 bonusAndPenaltyScore?.GetScore(y.Key, TypeScoreEnum.Penalty),
                                                 disciplinaryScoreMock)
                               );

            return _mapper.Map<IList<MonthResultViewModel>>(gpaFreshers);
        }
        private double CalculateAcademark(List<ModuleResult> moduleResults,
                                          Dictionary<string?, double?> weightedNumbers)
        {
            var totalWeightedNumber = (double)weightedNumbers.Values.Sum();

            var sumScore = 0d;

            foreach (var moduleResult in moduleResults)
            {
                sumScore += (double)(moduleResult.FinalMark *
                                    weightedNumbers?.FirstOrDefault
                                                    (x => x.Key == moduleResult.ModuleName)
                                                    .Value);
            }

            var academark = sumScore / totalWeightedNumber;

            return academark;
        }
        private double CalculateGPA
                        (Func<List<ModuleResult>, Dictionary<string?, double?>, double> academarkFunc,
                         List<ModuleResult> moduleResults,
                         Dictionary<string?, double?> weightedNumbers,
                         double? bonusScore, double? penaltyScore,
                         double disciplinaryScore)
        {
            var gpa =  (academarkFunc(moduleResults, weightedNumbers) * WeightedNumberAcademark)
                      +(disciplinaryScore * WeightedNumberDisciplinary)
                      + bonusScore 
                      - penaltyScore;

            return Math.Round((double)gpa, 2);
        }


    }
}
