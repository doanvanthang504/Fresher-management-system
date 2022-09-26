using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;
using Global.Shared.ViewModels.MailViewModels;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttendanceTokenService _attendanceTokenService;
        private readonly ICurrentTime _currentTime;
        private readonly IMailService _mailService;

        public AttendanceService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAttendanceTokenService attendanceTokenService,
            ICurrentTime currentTime,
            IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attendanceTokenService = attendanceTokenService;
            _currentTime = currentTime;
            _mailService = mailService;
        }

        public async Task<bool> SendAttendanceEmailToFresherAsync(GenerateAttendanceClassTokenViewModel attendanceClassToken)
        {
            var freshers = await _unitOfWork.FresherRepository.GetFresherByClassIdAsync(attendanceClassToken.ClassId);

            if (freshers.Count == 0)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_FRESHER);
            }

            var tasks = new List<Task>();
            foreach (var fresher in freshers)
            {
                var attendance = _mapper.Map<GenerateAttendanceTokenViewModel>(attendanceClassToken);
                attendance.FresherId = fresher.Id;
                var tokenUrl = _attendanceTokenService.GenerateAttendanceTokenURL(attendance);
                //Send mail attendance url to fresher
                var mailRequest = new MailRequestViewModel
                {
                    Subject = "Mail Take Attendance For Fresher",
                    Body = $"<div><p>Click here to attendance!</p><a href='{tokenUrl}'>Take Attendance Link</a></div>",
                    ToAddresses = fresher.Email
                };
                var mailMessage = _mapper.Map<MailMessage>(mailRequest);

                tasks.Add(Task.Run(async () =>
                {
                    var mailResult = await _mailService.SendAsync(mailMessage);

                    if (mailResult.IsError)
                    {
                        throw new AppException(mailResult.ErrorMessage);
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return true;
        }

        public async Task<bool> TakeAttendanceAsync(string token)
        {
            var fresher = _attendanceTokenService.GetDataByToken(token);
            var currentTime = _currentTime.GetCurrentTime().Date;
            var attendance = await _unitOfWork.AttendanceRepository.GetAttendanceByFilterAsync(x => x.FresherId == fresher.Key
                                                                                                    && x.AttendDate1.Date == currentTime);
            if (attendance == null)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_ATTENDANCE);
            }

            var typeAttendance = fresher.Value;

            if (typeAttendance == Constant.FIRST_ATTENDANCE)
            {
                attendance.Status1 = StatusAttendanceEnum.Present;
                attendance.IsAttend1 = true;
                attendance.AttendDate1 = _currentTime.GetCurrentTime();
                _unitOfWork.AttendanceRepository.Update(attendance);
                return await _unitOfWork.SaveChangeAsync() > 0;
            }

            attendance.Status2 = StatusAttendanceEnum.Present;
            attendance.IsAttend2 = true;
            attendance.AttendDate2 = _currentTime.GetCurrentTime();
            _unitOfWork.AttendanceRepository.Update(attendance);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }       

        public async Task<AttendanceViewModel?> UpdateAttendanceAsync(UpdateAttendanceViewModel attendance)
        {
            var attendanceObj = await _unitOfWork.AttendanceRepository.GetByIdAsync(attendance.AttendanceId);

            if (attendanceObj == null)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_ATTENDANCE);
            }

            _mapper.Map(attendance, attendanceObj);
            _unitOfWork.AttendanceRepository.Update(attendanceObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

            if (isSuccess)
            {
                return _mapper.Map<AttendanceViewModel>(attendanceObj);
            }

            throw new AppException(Constant.CAN_NOT_UPDATE_ATTENDANCE);
        }

        public async Task<List<AttendanceViewModel>> GetAllAttendanceByFresherIdAsync(FilterAttendanceViewModel attendance)
        {
            var listAttendance = await _unitOfWork.AttendanceRepository.GetAllAttendanceByFilterAsync(
                                                                            x => x.FresherId == attendance.Id
                                                                                && x.AttendDate1.Month == attendance.Month
                                                                                && x.AttendDate1.Year == attendance.Year);
            var result = _mapper.Map<List<AttendanceViewModel>>(listAttendance);
            return result;
        }

        public async Task<List<FresherAttendancesViewModel>> GetAllAttendanceByClassIdAsync(FilterAttendanceViewModel attendance)
        {
            var classObj = await _unitOfWork.ClassFresherRepository.GetClassIncludeFreshersAttendancesByIdAsync(attendance.Id, attendance.Month, attendance.Year);

            if (classObj == null)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_CLASS_NOT_FOUND);
            }

            var listFresherAttendancesObj = _mapper.Map<List<FresherAttendancesViewModel>>(classObj.Freshers);
            return listFresherAttendancesObj;
        }

        public async Task<string> GenenerateLinkAttendanceAsync(GenerateAttendanceClassTokenViewModel attendanceToken)
        {
            var isExist = await _unitOfWork.ClassFresherRepository.ExistAnyAsync(x => x.Id == attendanceToken.ClassId);

            if (!isExist)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_CLASS_NOT_FOUND);
            }

            await CreateAttendanceListByClassIdAsync(attendanceToken.ClassId);

            var tokenUrl = _attendanceTokenService.GenerateAttendanceClassTokenURL(attendanceToken);
            return tokenUrl;
        }

        public async Task<bool> TakeAttendanceClassAsync(string token, string tokenUser)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokenUser);
            var userId = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            if (userId == null)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_USER);
            }

            //Get info user
            var classFreser = _attendanceTokenService.GetDataByToken(token);
            var user = await _unitOfWork.UserRepository
                                     .GetByIdAsync(Guid.Parse(userId));
            var isExisted = await _unitOfWork.FresherRepository.ExistAnyAsync(x => x.Email.ToLower().Equals(user.Email.ToLower())
                                                                                    && x.ClassFresherId == classFreser.Key);
            if (!isExisted)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_USER);
            }

            //handle attendance status
            var currentDate = _currentTime.GetCurrentTime().DayOfYear;
            var attendance = await _unitOfWork.AttendanceRepository
                                          .GetAttendanceByFilterAsync(
                                              x => x.Fresher.Email == user.Email
                                                  && x.AttendDate1.DayOfYear == currentDate);
            var result = await UpdateStatusAttendanceFresherAsync(attendance, classFreser.Value);
            return result;
        }

        private async Task<bool> UpdateStatusAttendanceFresherAsync(Attendance attendance, int typeAttendance)
        {
            if (attendance == null)
            {
                throw new AppException(Constant.FAILED_ATTENDANCE);
            }

            if (typeAttendance == Constant.FIRST_ATTENDANCE)
            {
                attendance.Status1 = StatusAttendanceEnum.Present;
                attendance.IsAttend1 = true;
                attendance.AttendDate1 = _currentTime.GetCurrentTime();
                _unitOfWork.AttendanceRepository.Update(attendance);
                return await _unitOfWork.SaveChangeAsync() > 0;
            }

            attendance.Status2 = StatusAttendanceEnum.Present;
            attendance.IsAttend2 = true;
            attendance.AttendDate2 = _currentTime.GetCurrentTime();
            _unitOfWork.AttendanceRepository.Update(attendance);
            return await _unitOfWork.SaveChangeAsync() > 0;

        }

        private async Task CreateAttendanceListByClassIdAsync(Guid classId)
        {
            var freshers = await _unitOfWork.FresherRepository.GetFresherByClassIdAsync(classId);

            if (freshers.Count == 0)
            {
                return;
            }

            var attendances = _mapper.Map<List<Attendance>>(freshers);
            attendances.ForEach(attendance =>
            {
                attendance.AttendDate1 = _currentTime.GetCurrentTime().Date;
                attendance.AttendDate2 = _currentTime.GetCurrentTime().Date;
            });

            var isAttendanceExist = await _unitOfWork.AttendanceRepository.ExistAnyAsync(x => attendances.Select(attendance => attendance.FresherId).Contains(x.FresherId)
                                                                                              && attendances.Select(attendance => attendance.AttendDate1.Date).Contains(x.AttendDate1.Date));

            if (isAttendanceExist)
            {
                return;
            }

            await _unitOfWork.AttendanceRepository.AddRangeAsync(attendances);
            var isSuccess = await _unitOfWork.SaveChangeAsync() == attendances.Count;

            if (!isSuccess)
            {
                throw new AppException(Constant.CAN_NOT_CREATE_ATTENDANCE);
            }
        }

        public bool IsValidAttendanceToken(string token)
        {
            return _attendanceTokenService.VerifyAttendanceToken(token);
        }
    }
}
