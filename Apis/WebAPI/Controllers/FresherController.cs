using Application.Interfaces;
using Global.Shared.Commons;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.FresherViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class FresherController : BaseController
    {
        private readonly IFresherService _fresherService;

        public FresherController(IFresherService fresherService)
        {
            _fresherService = fresherService;
        }

        [HttpPut]
        public async Task<IActionResult> ChangStatusFresherAsync([FromBody] List<ChangeStatusFresherViewModel> changeStatusFresherViewModels)
        {
            _ = await _fresherService.ChangeFresherStatusAsync(changeStatusFresherViewModels);
            return Ok(Constant.UPDATE_STATUS_SUCCESS);
        }

        [HttpGet("{fresherId}")]
        public async Task<FresherViewModel> GetFresherByIdAsync(Guid fresherId)
        {
            return await _fresherService.GetFresherByIdAsync(fresherId);
        }
    }
}
