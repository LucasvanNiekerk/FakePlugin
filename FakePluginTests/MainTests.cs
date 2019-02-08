using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakePlugin.Tests
{
    [TestClass()]
    public class MainTests
    {
        private Main main;

        [TestInitialize]
        public void InitMain()
        {
            main = new Main();
        }

        [TestMethod()]
        public void GeneratePasswordTest()
        {
            Assert.IsTrue(main.GeneratePassword(8, "yes", "yes").Length == 8);
            Assert.IsFalse(main.GeneratePassword(8, "no", "yes").Contains("!#¤%&/()=?'@£$€{[]}" + '"'));
            Assert.IsFalse(main.GeneratePassword(8, "yes", "no").Contains("QWERTYUIOPASDFGHJKLZXCVBNM"));
            Assert.IsFalse(main.GeneratePassword(8, "no", "no").Contains("QWERTYUIOPASDFGHJKLZXCVBNM" + "!#¤%&/()=?'@£$€{[]}" + '"'));
        }

        [TestMethod()]
        public void QueryTest()
        {
            
            Assert.
        }
    }
}