using Application.SeedData;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Infrastructures.SeedData
{
    public static class SeedDataForTesting
    {
        public static async Task SeedDataInit(WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

            await SeedDataAsync<Fresher>(context);
            await SeedDataAsync<Reminder>(context);
            await SeedDataAsync<Topic>(context);
            await SeedDataAsync<ChapterSyllabus>(context);
            await SeedDataAsync<LectureChapter>(context);
            await SeedDataAsync<Module>(context);
            await SeedDataAsync<ClassFresher>(context);
            await SeedDataAsync<Feedback>(context);
            await SeedDataAsync<FeedbackQuestion>(context);
            await SeedDataAsync<FeedbackResult>(context);
            await SeedDataAsync<FeedbackAnswer>(context);
            await SeedDataAsync<Plan>(context);
            await SeedDataAsync<User>(context);
            await SeedDataAsync<Auditor>(context);
            await SeedAttendancesDataAsync(context);
            await SeedDataAsync<PlanInformation>(context);

            await context.SaveChangesAsync();
        }

        private static async Task SeedDataAsync<TEntity>(AppDbContext context) where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            var dataExists = await dbSet.AnyAsync();
            if (!dataExists)
            {
                var data = await DataInitializer.SeedDataAsync<TEntity>();

                dbSet.AddRange(data);
            }
        }

        private static async Task SeedAttendancesDataAsync(AppDbContext context)
        {
            var dataExists = await context.Attendances.AnyAsync();
            if (!dataExists)
            {
                var attendances = await DataInitializer.SeedDataAsync<Attendance>();

                context.Attendances.AddRange(attendances);
            }
        }
    }
}
