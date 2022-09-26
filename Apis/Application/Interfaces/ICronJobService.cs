using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICronJobService
    {
        public Task AutoGetChemicalAsync();

        public Task AutoGetChemicalPagingsionAsync();

        public Task AutoCreateReminderAsync();
    }
}
