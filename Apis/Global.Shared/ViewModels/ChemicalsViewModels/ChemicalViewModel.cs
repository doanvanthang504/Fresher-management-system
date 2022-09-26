using System;

namespace Global.Shared.ViewModels.ChemicalsViewModels
{
    public class ChemicalViewModel
    {
        public string _Id { get; set; }

        public string ChemicalType { get; set; }

        public int PreHarvestIntervalInDays { get; set; }

        public string ActiveIngredient { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset? ModificationDate { get; set; }

        public DateTimeOffset? DeletionDate { get; set; }
    }
}
