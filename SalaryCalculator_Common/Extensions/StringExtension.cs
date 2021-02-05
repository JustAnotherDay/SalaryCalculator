using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator_Common.Extensions
{
    public static class StringExtension
    {
        public static string RoundAndFormatToTwoDecimalPlace(this double dec)
        {
            return dec.ToString("F");
        }
    }
}
