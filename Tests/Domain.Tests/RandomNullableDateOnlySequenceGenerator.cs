using AutoFixture;
using AutoFixture.Kernel;
using Global.Shared.Extensions;
using System;
using System.Reflection;

namespace Domain.Tests
{
    internal class RandomNullableDateOnlySequenceGenerator : ISpecimenBuilder
    {
        private readonly RandomDateTimeSequenceGenerator _dateTimeGenerator;

        public RandomNullableDateOnlySequenceGenerator()
        {
            _dateTimeGenerator = new RandomDateTimeSequenceGenerator();
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propertyInfo = request as PropertyInfo;

            if (propertyInfo?.PropertyType == typeof(DateOnly?))
            {
                var randomizer = new Random();

                // 1 identify that this method will generate null value
                var willGenerateNullValue = randomizer.Next(0, 2) % 2 == 1;

                return willGenerateNullValue
                            ? null
                            : ((DateTime)_dateTimeGenerator.Create(typeof(DateTime), context)).ToDateOnly();
            }

            return new NoSpecimen();
        }
    }
}
