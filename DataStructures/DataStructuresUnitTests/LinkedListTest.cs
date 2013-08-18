using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.LinkedList;
using System.Collections.Generic;

namespace DataStructuresUnitTests
{
    [TestClass]
    public class LinkedListTest
    {
        DataStructures.LinkedList.LinkedList<int> l;

        [TestInitialize]
        public void Initialize()
        {
            l = new DataStructures.LinkedList.LinkedList<int>();
        }

        //===============================================
        // Count

        [TestMethod]
        public void TestCountOnEmpty()
        {
            Assert.AreEqual(0, l.Count);
        }

        //===============================================
        // First

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestFirstOnEmpty()
        {
            var a = l.First;
        }

        [TestMethod]
        public void TestFirstOnSingle()
        {
            l.AddFirst(1);
            var el = l.First;
            Assert.AreEqual(1, el);
            Assert.AreEqual(1, l.Count);
        }

        [TestMethod]
        public void TestFirstOnMultiple()
        {
            l.AddFirst(1);
            l.AddFirst(2);

            var ell = l.Last;
            var elf = l.First;

            Assert.AreEqual(1, ell);
            Assert.AreEqual(2, elf);
            Assert.AreEqual(2, l.Count);
        }

        //===============================================
        // Last

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestLastOnEmpty()
        {
            var a = l.Last;
        }

        [TestMethod]
        public void TestLastOnSingle()
        {
            l.AddLast(1);
            var el = l.Last;
            Assert.AreEqual(1, el);
            Assert.AreEqual(1, l.Count);
        }

        [TestMethod]
        public void TestLastOnMultiple()
        {
            l.AddLast(1);
            l.AddLast(2);

            var ell = l.Last;
            var elf = l.First;

            Assert.AreEqual(2, ell);
            Assert.AreEqual(1, elf);
            Assert.AreEqual(2, l.Count);
        }

        //===============================================
        // RemoveFirst

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRemoveFirstOfEmpty()
        {
            try
            {
                l.RemoveFirst();
            }
            finally
            {
                Assert.AreEqual(0, l.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRemoveFirstOfSingle()
        {
            l.AddFirst(1);
            l.RemoveFirst();
            Assert.AreEqual(0, l.Count);
            var a = l.First;
        }

        public void TestRemoveFirstOfMultiple()
        {
            l.AddFirst(2);
            l.AddFirst(1);

            l.RemoveFirst();
            Assert.AreEqual(2, l.First);
            Assert.AreEqual(1, l.Count);
        }

        //===============================================
        // RemoveLast

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRemoveLastOfEmpty()
        {
            try
            {
                l.RemoveLast();
            }
            finally
            {
                Assert.AreEqual(0, l.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRemoveLastOfSingle()
        {
            l.AddLast(1);
            l.RemoveLast();
            Assert.AreEqual(0, l.Count);
            var a = l.Last;
        }

        public void TestRemoveLastOfMultiple()
        {
            l.AddLast(2);
            l.AddLast(1);

            l.RemoveLast();
            Assert.AreEqual(2, l.Last);
            Assert.AreEqual(1, l.Count);
        }

        //===============================================
        // RemoveElementAt

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRemoveAtOnEmpty()
        {
            try
            {
                l.RemoveElementAt(0);
            }
            finally
            {
                Assert.AreEqual(0, l.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRemoveAtOutOfBoundsPositive()
        {
            try
            {
                l.AddFirst(1);
                l.RemoveElementAt(1);
            }
            finally
            {
                Assert.AreEqual(1, l.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRemoveAtOutOfBoundsNegative()
        {
            try
            {
                l.AddFirst(1);
                l.RemoveElementAt(-1);
            }
            finally
            {
                Assert.AreEqual(1, l.Count);
            }
        }

        [TestMethod]
        public void TestRemoveAtFirst()
        {
            l.AddFirst(1);
            l.AddFirst(2);
            l.RemoveElementAt(0);

            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(1, l.First);
            Assert.AreEqual(1, l.Last);
        }

        [TestMethod]
        public void TestRemoveAtLast()
        {
            l.AddFirst(1);
            l.AddFirst(2);
            l.RemoveElementAt(1);

            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(2, l.First);
            Assert.AreEqual(2, l.Last);
        }

        [TestMethod]
        public void TestRemoveAtMiddle()
        {
            l.AddFirst(3);
            l.AddFirst(2);
            l.AddFirst(1);
            l.RemoveElementAt(1);

            Assert.AreEqual(2, l.Count);
            Assert.AreEqual(1, l.First);
            Assert.AreEqual(3, l.Last);
        }

        //===============================================
        // GetElementAt

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetAtOnEmpty()
        {
            l.GetElementAt(0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetAtOutOfBoundsPositive()
        {
            l.AddFirst(0);
            l.GetElementAt(1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetAtOutOfBoundsNegative()
        {
            l.AddFirst(0);
            l.GetElementAt(-1);
        }

        [TestMethod]
        public void TestGetFirst()
        {
            l.AddFirst(1);
            l.AddFirst(0);
            Assert.AreEqual(0, l.GetElementAt(0));
        }

        [TestMethod]
        public void TestGetLast()
        {
            l.AddFirst(1);
            l.AddFirst(0);
            Assert.AreEqual(1, l.GetElementAt(1));
        }

        [TestMethod]
        public void TestGetMiddle()
        {
            l.AddFirst(3);
            l.AddFirst(2);
            l.AddFirst(1);
            Assert.AreEqual(2, l.GetElementAt(1));
        }

        //===============================================
        // GetElementAt

        [TestMethod]
        public void TestContainsOnEmpty()
        {
            Assert.IsFalse(l.Contains(0));
        }

        [TestMethod]
        public void TestContainsNotIn()
        {
            l.AddFirst(0);
            l.AddFirst(0);
            l.AddFirst(0);
            Assert.IsFalse(l.Contains(1));
        }

        [TestMethod]
        public void TestContainsFirst()
        {
            l.AddFirst(1);
            l.AddFirst(0);
            Assert.IsTrue(l.Contains(0));
        }

        [TestMethod]
        public void TestContainsLast()
        {
            l.AddFirst(1);
            l.AddFirst(0);
            Assert.IsTrue(l.Contains(1));
        }

        [TestMethod]
        public void TestContainsMiddle()
        {
            l.AddFirst(3);
            l.AddFirst(2);
            l.AddFirst(1);
            Assert.IsTrue(l.Contains(2));
        }

        //===============================================
        // IndexOf

        [TestMethod]
        public void TestIndexOfOnEmpty()
        {
            Assert.AreEqual(-1, l.IndexOf(0));
        }

        [TestMethod]
        public void TestIndexOfFirst()
        {
            l.AddFirst(2);
            l.AddFirst(1);
            Assert.AreEqual(0, l.IndexOf(1));
        }

        [TestMethod]
        public void TestIndexOfLast()
        {
            l.AddFirst(2);
            l.AddFirst(1);
            Assert.AreEqual(1, l.IndexOf(2));
        }

        [TestMethod]
        public void TestIndexOfMiddle()
        {
            l.AddLast(1);
            l.AddLast(2);
            l.AddLast(3);
            Assert.AreEqual(1, l.IndexOf(2));
        }

        [TestMethod]
        public void TestIndexOfMultipleInstances()
        {
            l.AddLast(1);
            l.AddLast(1);
            Assert.AreEqual(0, l.IndexOf(1));
        }

        //===============================================
        // Enumerator

        [TestMethod]
        public void TestEnumeratorOnEmpty()
        {
            IEnumerator<int> e = l.GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void TestEnumeratorOnSingle()
        {
            l.AddFirst(1);
            IEnumerator<int> e = l.GetEnumerator();

            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual(1, e.Current);

            Assert.IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void TestForEach()
        {
            l.AddFirst(1);
            l.AddFirst(2);
            l.AddFirst(3);

            int i = 0;
            foreach (int el in l)
            {
                Assert.AreEqual(l.GetElementAt(i), el);
                i++;
            }
        }

        [TestMethod]
        public void TestEnumeratorMultipleElements()
        {
            l.AddFirst(1);
            l.AddFirst(2);
            l.AddFirst(3);

            IEnumerator<int> e = l.GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual(3, e.Current);

            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual(2, e.Current);

            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual(1, e.Current);

            Assert.IsFalse(e.MoveNext());
        }
    }
}
