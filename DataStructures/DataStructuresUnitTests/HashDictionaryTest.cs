using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Dictionary;
using System.Collections.Generic;

namespace DataStructuresUnitTests
{
    [TestClass]
    public class HashDictionaryTest : LLDictionaryTest
    {
        [TestInitialize]
        public override void Initialize()
        {
            d = new HashDictionary<int, int>();
        }

        [TestMethod]
        public void TestResize()
        {
            List<int> keys = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            HashSet<int> keysOut = new HashSet<int>();

            foreach (int key in keys)
            {
                d[key] = 0;
            }

            foreach (int key in d)
            {
                keysOut.Add(key);
                Assert.IsTrue(keys.Contains(key));
            }

            Assert.AreEqual(keys.Count, keysOut.Count);
        }
    }
}
