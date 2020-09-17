namespace SuperMassive
{
    using System;

    public class SonarHelper
    {
        // just for the sake of testing sonar quality gate (uncovered)
        public static void TestMethod()
        {
            Console.WriteLine($"{nameof(SonarHelper)}.{nameof(TestMethod)} has been called");
        }
    }
}
