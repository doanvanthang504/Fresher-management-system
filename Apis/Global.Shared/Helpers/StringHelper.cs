using System.Linq;

namespace Global.Shared.Helpers
{
    public static class StringHelper
    {
        public static string[] Split(string stringToSplit)
            => stringToSplit.Split(".");

        public static string ToPluralFormName(this string name)
        {
            var PLURAL_FORM_ES = new string[] { "o", "s", "ch", "x", "sh", "z" };

            var lastCharsInName = name[^2..];

            var correctPluralForm = PLURAL_FORM_ES.Any(e => lastCharsInName.EndsWith(e)) ? "es" : "s";

            return name + correctPluralForm;
        }
    }
}
