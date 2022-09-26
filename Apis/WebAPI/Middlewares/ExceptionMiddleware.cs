using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly ICurrentTime _currentTime;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public ExceptionMiddleware(
            ILogger logger,
            ICurrentTime currentTime,
            IConfiguration configuration,
            IMailService mailService)
        {
            _logger = logger;
            _currentTime = currentTime;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var responseDetails = new ExceptionDetails
                {
                    Message = Constant.INTERNAL_SERVER_ERROR_MESSAGE,
                    StatusCode = Constant.INTERNAL_SERVER_ERROR
                };

                if (exception is AppException appException)
                {
                    responseDetails.Message = appException.Message;
                    responseDetails.StatusCode = appException.Status;
                }

                _logger.LogError(exception, exception.Message);
                await Task.Run(async () =>
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = responseDetails.StatusCode;
                    await context.Response.WriteAsJsonAsync(responseDetails);
                    //if (responseDetails.StatusCode == Constant.INTERNAL_SERVER_ERROR)
                    //{
                    //    await SendMailAsync();
                    //}
                });
            }
        }

        private async Task SendMailAsync()
        {
            var sendTo = _configuration["ExceptionMail"];
            var now = $"{_currentTime.GetCurrentTime():dd-MM-yyyy hh:mm:ss}";
            var mailContent = $"{now}  Error was thrown from server";

            var mailMessage = new MailMessage();
            mailMessage.To.Add(sendTo);
            mailMessage.Body = mailContent;
            await _mailService.SendAsync(mailMessage);
        }
    }
}
