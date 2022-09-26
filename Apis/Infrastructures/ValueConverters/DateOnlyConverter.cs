using Global.Shared.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Infrastructures.ValueConverters
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter()
            : base(d => d.ToDateTime(), d => d.ToDateOnly())
        {
        }
    }
}
