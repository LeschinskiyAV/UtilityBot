using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace UtilityBot.Services
{
    public static class Worker
    {
        public static string Work(string message, string botMode)
        {
            string res = string.Empty;
            if (botMode == "stringLength")
            {
                res = message.Length.ToString();
            }
            else if (botMode == "sumOfNumbers")
            {
                res = StringOfNumbersToArray(message);
            }
            else
            {
                res = "Введите /start для начала";
            }
            return res;
        }

        public static string StringOfNumbersToArray(string text)
        {
            string[] parts = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                sum += Convert.ToInt32(parts[i]);
            }
            return sum.ToString();
        }
    }
}
