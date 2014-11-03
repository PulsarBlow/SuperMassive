using System;
using System.Globalization;

namespace SuperMassive.Fakers
{
    /// <summary>
    /// Name fakes
    /// </summary>
    public static class Name
    {
        /// <summary>
        /// A random First name
        /// </summary>
        /// <returns></returns>
        public static string FirstName()
        {
            return DataStore.FirstNames.RandPick();
        }
        /// <summary>
        /// A random Last name
        /// </summary>
        /// <returns></returns>
        public static string LastName()
        {
            return DataStore.LastNames.RandPick();
        }
        /// <summary>
        /// A random full name (First name + Last name)
        /// </summary>
        /// <returns></returns>
        public static string FullName()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0} {1}", FirstName(), LastName());
        }

        /// <summary>
        /// Taxonomy name is based on product taxonomy. Can be used for categories or labels
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string TaxonomyName(int words = 2)
        {
            return DataStore.Taxonomies.RangeRandPick(words).Join(" ");
        }

        /// <summary>
        /// A random name based on action verbs.
        /// </summary>
        /// <param name="minActionVerbs"></param>
        /// <param name="maxActionVerbs"></param>
        /// <returns></returns>
        public static string VerbBasedName(int minActionVerbs = 1, int maxActionVerbs = 3)
        {
            return DataStore.ActionVerbs.RangeRandPick(RandomNumberGenerator.Int(minActionVerbs, maxActionVerbs)).Join(" ");
        }
    }
}
