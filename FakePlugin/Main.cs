using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using Wox.Plugin;

namespace FakePlugin
{
    public class Main : IPlugin//, ISettingProvider
    {
        Random random = new Random();
        SoundPlayer player = new SoundPlayer();
        private int _actionKeywordLength = 5; //med mellemrum efter

        //private PluginInitContext context;

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();
            //Tager vores query og opdeler den i en array af strings udefra mellemrum.
            string[] splitQuery = query.RawQuery.Split(' ');

            Result result;

            if (string.IsNullOrEmpty(query.Search))
            {
                results.Add(ResultsYesYes(query));
                results.Add(ResultsYesNo(query));
                results.Add(ResultsNoYes(query));
                return results;
            }

            for (int i = 0; i < 25; i++)
            {
                result = new Result();
                result.Title = query.ActionKeyword;
                result.SubTitle = "Copy to clipboard: " + GeneratePassword(Convert.ToInt16(splitQuery[1]), splitQuery[2], splitQuery[3]);
                result.IcoPath = "Images\\logo.png";
                result.Action = context =>
                {
                    Clipboard.SetText(result.SubTitle.Substring(19)); //Lucas was here and didn't do shit!
                    string sound = SoundEffects();
                    string[] username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\');
                    player.SoundLocation = @"C:\Users\" + username[1] + @"\AppData\Roaming\Wox\Plugins\pass\Sounds\" + sound + ".wav";
                    AutoClosingMessageBox.Show("Copied to clipboard", "You're welcome", 1000);
                    player.Play();
                    return true;  //false lukker ikke wox, true lukker wox
                };
                results.Add(result);
            }
            
            return results;
        }

        public Result ResultsYesYes(Query query)
        {
            string title = "PasswordLength(int) yes yes";
            string subtitle = "list password of given length with special characters and uppercase";
            string command = "8 yes yes";
            return ResultForCommand(query, command, title, subtitle);
        }

        private Result ResultsYesNo(Query query)
        {
            string title = "PasswordLength(int) yes no";
            string subtitle = "list password of given length with special characters";
            string command = "8 yes no";
            return ResultForCommand(query, command, title, subtitle);
        }

        private Result ResultsNoYes(Query query)
        {
            string title = "PasswordLength(int) no yes";
            string subtitle = "list password of given length with uppercase";
            string command = "8 no yes";
            return ResultForCommand(query, command, title, subtitle);
        }

        public Result ResultForCommand(Query query, string command, string title, string subtitle)
        {
            //const string seperater = Plugin.Query.TermSeperater;
            var result = new Result
            {
                Title = title,
                IcoPath = "Images\\logo.png",
                SubTitle = subtitle,
                Action = e =>
                {
                    //context.API.ChangeQuery($"{query.ActionKeyword}{seperater}{command}{seperater}");
                    return false;
                }
            };
            return result;
        }

        public string SoundEffects()
        {
            int randomNumber = random.Next(4);
            switch (randomNumber)
            {
                case 0:
                    return "Maglegrdsvej 2 2";
                case 1:
                    return "Maglegrdsvej 2 3";
                case 2:
                    return "Maglegrdsvej 2 4";
                default:
                    return "Maglegrdsvej 2 6";
            }
        }

        public void Init(PluginInitContext context)
        {
            
        }

        public string GeneratePassword(int length, string specialCharsString, string upperCaseString)
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

        //public Control CreateSettingPanel()
        //{
        //    return new UserControl1();
        //}
    }
}
