using Global.Shared.Commons;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.ClassFresherViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClassFresherService
    { 
        /// <summary>
        /// Update information of a class after import
        /// </summary>
        /// <param name="updateClassFresherViewModel"></param>
        /// <returns>A <see cref="ClassFresherViewModel"/> if updated successful, message if updated failure</returns>
        Task<ClassFresherViewModel> UpdateClassFresherAfterImportedAsync(UpdateClassFresherInfoViewModel updateClassFresherViewModel);
        /// <summary>
        /// Get a class fresher and fresher in it by class id
        /// </summary>
        /// <param name="classFresherId"></param>
        /// <returns>A <see cref="ClassFresherViewModel"> include freshers if found any class, message if not found any class</returns>
        Task<ClassFresherViewModel> GetClassFresherByClassIdAsync(Guid classFresherId);
        /// <summary>
        /// Get a class fresher but not has fresher by class id
        /// </summary>
        /// <param name="classFresherId"></param>
        /// <returns>A <see cref="ClassFresherViewModel"> if found any class, message if not found any class</returns>
        Task<ClassFresherViewModel> GetClassWithFresherByClassIdAsync(Guid classFresherId);
        /// <summary>
        /// Get all class fresher code
        /// </summary>
        /// <returns>List class code</returns>
        Task<List<string>> GetAllClassCodeAsync();
        /// <summary>
        /// Get class fresher pagingsion by page index and page size
        /// </summary>
        /// <param name="updateClassFresherViewModel"></param>
        /// <returns>A list  <see cref="ClassFresherViewModel"/> was paginated</returns>
        Task<Pagination<ClassFresherViewModel>> GetAllClassFreshersPagingsionAsync(PaginationRequest request);
        /// <summary>
        /// Update information of a class fresher
        /// </summary>
        /// <param name="updateClassFresherViewModel"></param>
        /// <returns>A <see/></returns>
        Task<ClassFresherViewModel> UpdateClassFresher(UpdateClassFresherViewModel updateClassFresherViewModel);

        /// <summary>
        /// Get a list fresher by class code
        /// </summary>     
        /// <returns>A list <see cref="FresherViewModel"/></returns>
        Task<List<FresherViewModel>> GetFreshersByClassCodeAsync(string classCode);
        /// <summary>
        /// Create a new class from file excel after imported
        /// </summary>
        /// <param name="fileExcel"></param>
        /// <returns>A list <see cref="ClassFresherViewModel"/>if imported successful, message if imported failure</returns>
        Task<List<ClassFresherViewModel>> CreateClassFresherFromImportedExcelFile(IFormFile fileExcel);
        /// <summary>
        /// Delete a class fresher by class id
        /// </summary>
        /// <param name="ClassFresherId"></param>
        /// <returns><see cref="Boolean"/> for status delete. True if success, false if fail</returns>
        Task<bool> DeleteClassFresherAsync(Guid ClassFresherId);
    }
}
