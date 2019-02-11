using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using Wox.Infrastructure.UserSettings;
using Wox.Plugin;

namespace FakePlugin
{
    public class Main : IPlugin//, ISettingProvider
    {
        Random random = new Random();
        SoundPlayer player = new SoundPlayer();
        private int _actionKeywordLength = 5; //med mellemrum efter

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();
            //Tager vores query og opdeler den i en array af strings udefra mellemrum.
            string[] splitQuery = query.RawQuery.Split(' ');

            Result result = new Result();

            if (string.IsNullOrEmpty(query.Search))
            {
                results.Add(ResultForListCommandAutoComplete());
                results.Add(ResultForInstallCommandAutoComplete());
                results.Add(ResultForUninstallCommandAutoComplete());
                return results;
            }

            for (int i = 0; i < 25; i++)
            {
                result = new Result();
                result.Title = query.ActionKeyword;
                result.SubTitle = "Copy to clipboard: " + GeneratePassword(Convert.ToInt16(splitQuery[1]), splitQuery[2], splitQuery[3]);
                result.IcoPath = "Images\\logo.png";
                results.Add(result);
                result.Action = context =>
                {
                    Clipboard.SetText(result.SubTitle.Substring(19)); //Lucas was here
                    string sound = SoundEffects();
                    string[] username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\');
                    player.SoundLocation = @"C:\Users\" + username[1] + @"\AppData\Roaming\Wox\Plugins\pass\Sounds\" + sound + ".wav";

                    player.Play();
                    return true;  //false lukker ikke wox, true lukker wox
                };
            }
            
            return results;
        }

        private Result ResultForListCommandAutoComplete()
        {
            string title = "PasswordLength(int) yes yes";
            string subtitle = "list password of given length";
            return ResultForCommand(title, subtitle);
        }

        private Result ResultForInstallCommandAutoComplete()
        {
            string title = "PasswordLength(int) yes no";
            string subtitle = "list password of given length";
            return ResultForCommand(title, subtitle);
        }

        private Result ResultForUninstallCommandAutoComplete()
        {
            string title = "PasswordLength(int) no yes";
            string subtitle = "list password of given length";
            return ResultForCommand(title, subtitle);
        }

        private Result ResultForCommand(string title, string subtitle)
        {
            var result = new Result
            {
                Title = title,
                IcoPath = "Images\\logo.png",
                SubTitle = subtitle,
                Action = e =>
                {
                    return false;
                }
            };
            return result;
        }

        public string SoundEffects()
        {
            int RN = random.Next(4);
            switch (RN)
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
        //    return new View2();
        //}
    }
}
