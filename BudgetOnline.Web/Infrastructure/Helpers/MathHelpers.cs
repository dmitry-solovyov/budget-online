using System;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
    public static class MathHelpers
    {
        public static int Nearest(int value, int[] validValues)
        {
            int diff = -1;
            int foundIndex = 0;
            for (int i = 0; i < validValues.Length; i++)
            {
                if (Math.Abs(value - validValues[i]) < diff || diff == -1)
                {
                    diff = Math.Abs(value - validValues[i]);
                    foundIndex = i;
                }
            }

            return validValues[foundIndex];
        }
    }
}