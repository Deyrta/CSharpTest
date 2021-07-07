using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendBeforeStart()
        {
            DateTime startDate = new DateTime(2021, 3, 19);
            int count = 7;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2021, 3, 15), new DateTime(2021, 3, 16)),
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 3, 25)));
        }

        [TestMethod]
        public void TestWeekendDuringStart()
        {
            DateTime startDate = new DateTime(2021, 1, 12);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 1, 11), new DateTime(2021, 1, 13)),
                new WeekEnd(new DateTime(2021, 1, 15), new DateTime(2021, 1, 15)),
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 1, 24)));
        }

        [TestMethod]
        public void TestOnTheFirstDay()
        {
            DateTime starDate = new DateTime(2021, 4, 28);
            int count = 5;
            WeekEnd[] weeknds = new WeekEnd[3]
            {
                new WeekEnd(new DateTime(2021, 4, 20), new DateTime(2021, 4, 21)),
                new WeekEnd(new DateTime(2021, 4, 28), new DateTime(2021, 4, 28)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29)),
            };

            DateTime result = new WorkDayCalculator().Calculate(starDate, count, weeknds);

            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 4)));
        }

        [TestMethod]
        public void TestOnEverything()
        {
            DateTime starDate = new DateTime(2021, 4, 3);
            int count = 5;
            WeekEnd[] weeknds = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2021, 3, 20), new DateTime(2021, 3, 21)), // Before start
                new WeekEnd(new DateTime(2021, 4, 3), new DateTime(2021, 4, 3)), // on the first day
                new WeekEnd(new DateTime(2021, 4, 4), new DateTime(2021, 4, 6)), // during start
                new WeekEnd(new DateTime(2021, 5, 1), new DateTime(2021, 5, 10)), // afteer end
                new WeekEnd(new DateTime(2021, 4, 9), new DateTime(2021, 4, 11)), // normal path
            };

            DateTime result = new WorkDayCalculator().Calculate(starDate, count, weeknds);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 14)));
        }
    }
}
