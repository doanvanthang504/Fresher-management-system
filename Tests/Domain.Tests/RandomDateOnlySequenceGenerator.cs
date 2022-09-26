using AutoFixture;
using AutoFixture.Kernel;
using Global.Shared.Extensions;
using System;
using System.Reflection;

namespace Domain.Tests
{
    internal class RandomDateOnlySequenceGenerator : ISpecimenBuilder
    {
        private readonly RandomDateTimeSequenceGenerator _dateTimeGenerator;

        public RandomDateOnlySequenceGenerator()
        {
            _dateTimeGenerator = new RandomDateTimeSequenceGenerator();
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propertyInfo = request as PropertyInfo;

            if (propertyInfo?.PropertyType == typeof(DateOnly))
                return ((DateTime)_dateTimeGenerator.Create(typeof(DateTime), context)).ToDateOnly();

            return new NoSpecimen();
        }
    }
}
