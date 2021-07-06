using System;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime endDate = startDate.AddDays(dayCount - 1);

            if (weekEnds == null)
                return endDate;

            foreach(WeekEnd weekend in weekEnds)
            {
                if (startDate >= weekend.EndDate && startDate <= weekend.StartDate)
                    continue;
                if (endDate < weekend.StartDate)
                    continue;
                if(startDate <= weekend.EndDate)
                    if (startDate >= weekend.StartDate)
                        endDate = endDate.AddDays(weekend.EndDate.Day - startDate.Day+1);
                    else
                        endDate = endDate.AddDays(weekend.EndDate.Day - weekend.StartDate.Day+1);
            }

            return endDate;
        }
    }
}
