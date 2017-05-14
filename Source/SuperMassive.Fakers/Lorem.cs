namespace SuperMassive.Fakers
{
    using System.Collections.Generic;
    using System.Linq;
    using SuperMassive.Extensions;

    public static class Lorem
    {
        public static string Word()
        {
            return DataStore.Words.RandPick();
        }
        public static IEnumerable<string> Words(int length = 2)
        {
            return DataStore.Words.RangeRandPick(length);
        }
        public static string Sentence(int minWords = 4)
        {
            var result = Words(minWords + RandomNumberGenerator.Int(6)).Join(" ");
            return StringHelper.Capitalize(result) + ".";
        }
        public static IEnumerable<string> Sentences(int sentenceCount = 3)
        {
            return 1.To(sentenceCount).Select(item => Sentence());
        }
        public static string Paragraph(int sentenceCount = 3)
        {
            return Sentences(sentenceCount + RandomNumberGenerator.Int(3)).Join(" ");
        }
        public static IEnumerable<string> Paragraphs(int paragraphCount = 3)
        {
            return 1.To(paragraphCount).Select(item => Paragraph());
        }
    }
}
