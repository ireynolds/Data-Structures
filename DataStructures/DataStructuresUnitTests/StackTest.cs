using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.Stack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresUnitTests
{
    [TestClass]
    public class StackTest
    {

        // T Top { get; }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestTopOfEmptyThrowsException()
        {
            var s = new Stack<int>();

            var t = s.Top;

            Assert.AreEqual(null, t);
        }

        [TestMethod]
        public void TestTopOnSizeOneReturnsElement()
        {
            var s = new Stack<int>();

            s.Push(1);

            var t = s.Top;

            Assert.AreEqual(1, t);
        }

        [TestMethod]
        public void TestTopOnSizeTwoReturnsTop()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Push(2);

            var t = s.Top;

            Assert.AreEqual(2, t);
        }

        [TestMethod]
        public void TestTopOnSizeTwoDoesNotRemoveAny()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Push(2);

            var t = s.Top;
            var t2 = s.Top;
            var ct = s.Count;

            Assert.AreEqual(2, t);
            Assert.AreEqual(2, t2);
            Assert.AreEqual(2, ct);
        }

        [TestMethod]
        public void TestTopDoesNotChangeOrder()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Push(2);

            var t = s.Top;

            var first = s.Pop();
            var second = s.Pop();

            Assert.AreEqual(2, t);
            Assert.AreEqual(2, first);
            Assert.AreEqual(1, second);
        }

        [TestMethod]
        public void TestTopReturnsCorrectValueAfterRemove()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Push(2);
            s.Pop();

            var t = s.Top;

            Assert.AreEqual(1, t);
        }



        // int Count { get; }

        [TestMethod]
        public void TestCountOfEmptyIsZero()
        {
            var s = new Stack<int>();

            var ct = s.Count;

            Assert.AreEqual(0, ct);
        }


        [TestMethod]
        public void TestAddElementIncrementsCount()
        {
            var s = new Stack<int>();

            s.Push(1);

            var ct = s.Count;

            Assert.AreEqual(1, ct);
        }


        [TestMethod]
        public void TestRemoveElementDecrementsCount()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Pop();

            var ct = s.Count;

            Assert.AreEqual(0, ct);
        }



        // T Pop();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestPopFromEmptyThrowsException()
        {
            var s = new Stack<int>();

            s.Pop();
        }


        [TestMethod]
        public void TestPopFromEmptyDoesNotDecrementCount()
        {
            var s = new Stack<int>();

            try
            {
                s.Pop();
            }
            catch
            {
                // Expected -- do nothing
            }

            var ct = s.Count;

            Assert.AreEqual(0, ct);
        }


        [TestMethod]
        public void TestPopRemovesAndReturnsLastElement()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Push(2);

            var t = s.Pop();

            Assert.AreEqual(2, t);
        }


        [TestMethod]
        public void TestPopDoesNotChangeOrder()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Push(2);
            s.Push(3);

            var t = s.Pop();
            var m = s.Pop();
            var b = s.Pop();

            Assert.AreEqual(3, t);
            Assert.AreEqual(2, m);
            Assert.AreEqual(1, b);
        }


        // void Clear();

        [TestMethod]
        public void TestClearSetsCountToZero()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Push(2);
            s.Clear();

            var ct = s.Count;

            Assert.AreEqual(0, ct);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAfterClearTopThrowsException()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Clear();

            var t = s.Top;

            Assert.AreEqual(null, t);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAfterClearPopThrowsException()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Clear();

            s.Pop();
        }


        [TestMethod]
        public void TestCanPushAfterClear()
        {
            var s = new Stack<int>();

            s.Push(1);
            s.Clear();
            s.Push(2);

            var t = s.Top;

            Assert.AreEqual(2, t);
        }


    }
}
