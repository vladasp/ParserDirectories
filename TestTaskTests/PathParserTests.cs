using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Tests
{
    [TestClass()]
    public class PathParserTests
    {
        [TestMethod()]
        public void GetCCPPathAsyncTest()
        {
            // arrange
            Program.path = "C:\\Test";
            string mainPuth = "C:\\Test";
            List<string> resultList = new List<string>();
            resultList.Add("testcpp1.cpp");
            resultList.Add("\\Test1\\testcpp2.cpp");
            PathParser pp = new PathParser();

            // act
            pp.GetCCPPathAsync(mainPuth).GetAwaiter().GetResult();
            var actual = pp.Result;

            // assert
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(resultList[i], actual[i]);
            }
        }

        [TestMethod()]
        public void GetAllPathAsyncTestAll()
        {
            // arrange
            Program.path = "C:\\Test";
            string mainPuth = "C:\\Test";
            bool r1 = false;
            bool r2 = false;
            List<string> resultList = new List<string>();
            resultList.Add("\\Test1");
            resultList.Add("\\Test2");
            resultList.Add("\\Test2\\Test3");
            PathParser pp = new PathParser();

            // act
            pp.GetAllPathAsync(mainPuth, r1, r2).GetAwaiter().GetResult();
            var actual = pp.Result;

            // assert
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(resultList[i], actual[i]);
            }
        }

        [TestMethod()]
        public void GetAllPathAsyncTestR1()
        {
            // arrange
            Program.path = "C:\\Test";
            string mainPuth = "C:\\Test";
            bool r1 = true;
            bool r2 = false;
            List<string> resultList = new List<string>();
            resultList.Add("Test1");
            resultList.Add("Test2");
            resultList.Add("Test3\\Test2");
            PathParser pp = new PathParser();

            // act
            pp.GetAllPathAsync(mainPuth, r1, r2).GetAwaiter().GetResult();
            var actual = pp.Result;

            // assert
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(resultList[i], actual[i]);
            }
        }

        [TestMethod()]
        public void GetAllPathAsyncTestR2()
        {
            // arrange
            Program.path = "C:\\Test";
            string mainPuth = "C:\\Test";
            bool r1 = false;
            bool r2 = true;
            List<string> resultList = new List<string>();
            resultList.Add("1tseT\\");
            resultList.Add("2tseT\\");
            resultList.Add("3tseT\\2tseT\\");
            PathParser pp = new PathParser();

            // act
            pp.GetAllPathAsync(mainPuth, r1, r2).GetAwaiter().GetResult();
            var actual = pp.Result;

            // assert
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(resultList[i], actual[i]);
            }
        }
    }
}