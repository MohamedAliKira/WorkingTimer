using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTimer.Shared
{
    public class Utilities
    {
        public static string ConvertIntMonthToString(int intMonth)
        {
            string result = string.Empty;
            switch (intMonth) 
            {
                case 1:
                    result = "Janvier";
                    break;

                case 2:
                    result = "Février";
                    break;

                case 3:
                    result = "Mars";
                    break;

                case 4:
                    result = "Avril";
                    break;

                case 5:
                    result = "Mai";
                    break;

                case 6:
                    result = "Juin";
                    break;

                case 7:
                    result = "Juillet";
                    break;

                case 8:
                    result = "Août";
                    break;

                case 9:
                    result = "Septembre";
                    break;

                case 10:
                    result = "Octobre";
                    break;

                case 11:
                    result = "Novembre";
                    break;

                case 12:
                    result = "Décembre";
                    break;
            }
            return result;
        }
    }
}
