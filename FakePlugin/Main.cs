using System;
using System.Collections.Generic;
using Wox.Plugin;

namespace FakePlugin
{
    public class Main : IPlugin
    {
        Random random = new Random();
        private int _actionKeywordLength = 5; //med mellemrum efter

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();
            //Tager vores query og opdeler den i en array af strings udefra mellemrum.
            string[] splitQuery = query.RawQuery.Split(' ');

            for (int i = 0; i < 100; i++)
            {
                Result result = new Result();
                result.Title = query.ActionKeyword;
                result.SubTitle = GeneratePassword(Convert.ToInt16(splitQuery[1]), splitQuery[2], splitQuery[3]);
                results.Add(result);
                result.Action = context =>
                {
                    return false;  //false lukker ikke wox, true lukker wox
                };
            }


            return results;
        }

        public void Init(PluginInitContext context)
        {
            
        }

        private string GeneratePassword(int length, string specialCharsString, string upperCaseString)
        {
            string availableChars = "";
            string passWord = "";
            bool specialChars = false;
            bool upperCase = false;
            if (specialCharsString.ToLower() == "yes") specialChars = true;
            if (upperCaseString.ToLower() == "yes") upperCase = true;

            if (specialChars && upperCase) availableChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890" + "!#¤%&/()=?'@£$€{[]}" + '"';
            else if (specialChars && !upperCase) availableChars = "qwertyuiopasdfghjklzxcvbnm1234567890" + "!#¤%&/()=?'@£$€{[]}" + '"';
            else if (!specialChars && upperCase) availableChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            else availableChars = "qwertyuiopasdfghjklzxcvbnm1234567890";

            for (int i = 0; i < length; i++)
            {
                passWord += availableChars[random.Next(availableChars.Length)];
            }
            return passWord;
        }
    }
}
