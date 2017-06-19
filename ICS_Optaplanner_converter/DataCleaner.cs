using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter
{
    public static class DataCleaner
    {
        public static string ConvertTeacherName(string teacherName)
        {
           
            Regex rgx = new Regex("Geldhof Leonie");
            string result = rgx.Replace(teacherName, "L.Geldhof");

            rgx = new Regex("Benoit Christophe");
            result = rgx.Replace(result, "C.Benoit");

            rgx = new Regex("Gillaerts Herman");
             result = rgx.Replace(result, "H.Gillaerts");

            rgx = new Regex("Dhooge Annick");
             result = rgx.Replace(result, "A.Dhooge");

            rgx = new Regex("Van Ryckegem Kevin");
            result = rgx.Replace(result, "K.V.Ryckegem");

            rgx = new Regex("Weemaels Steve");
            result = rgx.Replace(result, "S.Weemaels");

            rgx = new Regex("Vanneuville Karine");
            result = rgx.Replace(result, "K.Vanneuville");

            rgx = new Regex("Siroyt Danny");
            result = rgx.Replace(result, "D.Siroyt");

            rgx = new Regex("Hambrouck Wim");
            result = rgx.Replace(result, "W.Hambrouck");

            rgx = new Regex("Vanderzijpen Frauke");
            result = rgx.Replace(result, "F.Vanderzijpen");

            rgx = new Regex("Bruylandt Kristien");
            result = rgx.Replace(result, "K.Bruylandt");

            rgx = new Regex("Dejonckheere Ruben");
            result = rgx.Replace(result, "R.Dejonckheere");

            rgx = new Regex("Gerrits Joeri");
            result = rgx.Replace(result, "J.Gerrits");

            rgx = new Regex("Drabbé Bram");
            result = rgx.Replace(result, "B.Drabbé");

            rgx = new Regex("Van Den Broek Johan");
            result = rgx.Replace(result, "J.VanDenBroek");

            rgx = new Regex("Wante Jan");
            result = rgx.Replace(result, "J.Wante");

            rgx = new Regex(", ");
            result = rgx.Replace(result, "_");

            return result;
        }

        public static string CleanRoomNames(string roomName)
        {
            var result = "";
            if (roomName == null)
                result = "defaultRoom";

            else if (roomName.Equals(string.Empty))
                result = "defaultRoom";

            else if (roomName.Equals("EHB - Audi 1"))
                result = "defaultRoom";

            else if (roomName.Equals("Ehb"))
                result = "defaultRoom";

            else
                result = roomName;

            var rgx = new Regex(" pczaal");
            result = rgx.Replace(result, "");

            rgx = new Regex(",.*");
            result = rgx.Replace(result, "");

            return result;
        }

        public static string CleanCourseNames(string summary)
        {
            summary = regex_HoorWerkCollege(summary);
            summary = regex_removeAllBetweenParenthesis(summary);

            var rgx = new Regex(" ");
            summary = rgx.Replace(summary, "_");

            return summary;
        }

        private static string regex_HoorWerkCollege(string input)
        {
            var result = input.Trim();
            if (input.StartsWith("[H]"))
            {
                result = input.Substring(3);
                result += "_HK";
                result = result.Trim();
            }
            else if (input.StartsWith("[W]"))
            {
                result = input.Substring(3);
                result += "_WK";
                result = result.Trim();
            }
            else if (input.StartsWith("[Ex V]"))
            {
                result = input.Substring(6);
                result += "_EXV";
                result = result.Trim();
            }
            else if (input.StartsWith("[Ex M]"))
            {
                result = input.Substring(6);
                result += "_EXM";
                result = result.Trim();
            }

            
            return result;
        }

        private static string regex_removeAllBetweenParenthesis(string input)
        {
            input = input.Trim();
            var regex = "(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))";
            return Regex.Replace(input, regex, "");
        }

    }


}
