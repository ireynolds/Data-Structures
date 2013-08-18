using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Dictionary;

namespace DataStructuresUnitTests
{
    [TestClass]
    public class LLDictionaryTest
    {
        protected DataStructures.Dictionary.IDictionary<int, int> d;

        [TestInitialize]
        public virtual void Initialize()
        {
            d = new LinkedListDictionary<int, int>();
        }

        //==============================================
        // [] Operator

        [TestMethod]
        public void TestSetNew()
        {
            d[1] = 2;

            Assert.IsTrue(d.ContainsKey(1));
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(2, d[1]);
        }

        [TestMethod]
        public void TestSetExisting()
        {
            d[1] = 2;
            d[1] = 3;

            Assert.IsTrue(d.ContainsKey(1));
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(3, d[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetNotIn()
        {
            try
            {
                var a = d[1];
            }
            finally
            {
                Assert.AreEqual(0, d.Count);
            }
        }

        [TestMethod]
        public void TestGetExisting()
        {
            d[1] = 2;
            Assert.AreEqual(2, d[1]);
            Assert.AreEqual(1, d.Count);
        }

        //==============================================
        // TryGetValue

        [TestMethod]
        public void TestTryGetNotIn()
        {
            d[1] = 2;

            int v;
            Assert.IsFalse(d.TryGetValue(2, out v));
            Assert.AreEqual(1, d.Count);
        }

        [TestMethod]
        public void TestTryGetExisting()
        {
            d[1] = 2;

            int v;
            Assert.IsTrue(d.TryGetValue(1, out v));
            Assert.AreEqual(2, v);
            Assert.AreEqual(1, d.Count);
        }

        //==============================================
        // InsertOrUpdate

        [TestMethod]
        public void TestInsertNew()
        {
            int ov;
            Assert.IsFalse(d.InsertOrUpdate(1, 2, out ov));
            Assert.AreEqual(2, d[1]);
            Assert.AreEqual(1, d.Count);
        }

        [TestMethod]
        public void TestUpdateExisting()
        {
            int ov;
            Assert.IsFalse(d.InsertOrUpdate(1, 2, out ov));
            Assert.IsTrue(d.InsertOrUpdate(1, 3, out ov));

            Assert.AreEqual(2, ov);
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(3, d[1]);
        }

        //==============================================
        // ContainsKey

        [TestMethod]
        public void TestContainsKeyNotIn()
        {
            d[1] = 2;
            Assert.IsFalse(d.ContainsKey(2));
        }

        [TestMethod]
        public void TestContainsExistingKey()
        {
            d[1] = 2;
            Assert.IsTrue(d.ContainsKey(1));
        }

        //==============================================
        // RemoveKey

        [TestMethod]
        public void TestRemoveExisting()
        {
            d[1] = 2;
            d.RemoveKey(1);
            Assert.IsFalse(d.ContainsKey(1));
            Assert.AreEqual(0, d.Count);
        }

        [TestMethod]
        public void TestRemoveAfterUpdate()
        {
            d[1] = 2;
            d[1] = 3;

            d.RemoveKey(1);
            Assert.AreEqual(0, d.Count);
            Assert.IsFalse(d.ContainsKey(1));
        }

        [TestMethod]
        public void TestRemoveNotIn()
        {
            d.RemoveKey(1);
            Assert.AreEqual(0, d.Count);
        }

        //==============================================
        // Clear

        [TestMethod]
        public void TestClear()
        {
            d[1] = 2;
            d[2] = 3;

            Assert.AreEqual(2, d[1]);
            Assert.AreEqual(3, d[2]);
            Assert.AreEqual(2, d.Count);
            d.Clear();

            Assert.AreEqual(0, d.Count);
            Assert.IsFalse(d.ContainsKey(1));
            Assert.IsFalse(d.ContainsKey(2));
        }

        //==============================================
        // Enumerator

        [TestMethod]
        public void TestEnumeratorOnEmpty()
        {
            IEnumerator<int> e = d.GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void TestEnumeratorOnSingle()
        {
            d[1] = 2;
            IEnumerator<int> e = d.GetEnumerator();

            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual(1, e.Current);

            Assert.IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void TestForEach()
        {
            d[1] = 2;
            d[2] = 3;
            d[2] = 4;
            d[3] = 5;

            List<int> l = new List<int>() { 1, 2, 3 };
            List<int> keys = new List<int>();

            foreach (int el in d)
            {
                Assert.IsTrue(l.Contains(el));
                if (!keys.Contains(el)) keys.Add(el);
            }

            Assert.AreEqual(3, keys.Count);
        }

        [TestMethod]
        public void TestEnumeratorMultipleElements()
        {
            d[1] = 2;
            d[2] = 3;
            d[2] = 4;
            d[3] = 5;

            List<int> l = new List<int>() { 1, 2, 3 };
            List<int> keys = new List<int>();

            IEnumerator<int> e = d.GetEnumerator();

            Assert.IsTrue(e.MoveNext());
            Assert.IsTrue(l.Contains(e.Current));
            if (!keys.Contains(e.Current)) keys.Add(e.Current);

            Assert.IsTrue(e.MoveNext());
            Assert.IsTrue(l.Contains(e.Current));
            if (!keys.Contains(e.Current)) keys.Add(e.Current);

            Assert.IsTrue(e.MoveNext());
            Assert.IsTrue(l.Contains(e.Current));
            if (!keys.Contains(e.Current)) keys.Add(e.Current);

            Assert.IsFalse(e.MoveNext());
            Assert.AreEqual(3, keys.Count);
        }
    }
}
