using DataStructures.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresUnitTests
{
    [TestClass]
    public class AdjacencyListGraphTest {
    
        // Custom strings
        private const string n1  = "lorem";
        private const string n2 = "ipsum";
        private const string n3  = "dolor";
        private const string n4  = "sit";
    
        // Custom Edge<string, string>s
        private readonly Edge<string, string> e1  = new Edge<string, string>(n1, n2, "lorem");
        private readonly Edge<string, string> e2  = new Edge<string, string>(n2, n3, "ipsum");
        private readonly Edge<string, string> e3  = new Edge<string, string>(n3, n4, "dolor");
        private readonly Edge<string, string> e4  = new Edge<string, string>(n1, n4, "sit");
    
        // convenient way to get a new graph with m strings.
        private static AdjacencyListGraph<string, string> graph(params string[] args) {
            AdjacencyListGraph<string, string> g = new AdjacencyListGraph<string, string>();
            foreach (string arg in args) 
                g.AddNode(arg);
            return g;
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // AddNode(string) Test
    
        [TestMethod] 
        [ExpectedException(typeof(ArgumentNullException))]
        public void testAddstringNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.AddNode(null);
        }
    
        [TestMethod]
        public void testAddstring() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsTrue(g.AddNode(n1));
        }
    
        [TestMethod]
        public void testAddstringMultiple() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsTrue(g.AddNode(n1));
            Assert.IsTrue(g.AddNode(n2));
        }
    
        [TestMethod]
        public void testAddstringDuplicate() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsTrue(g.AddNode(n1));
            Assert.IsFalse(g.AddNode(n1));
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // Containsstring(string) Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testContainsstringNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.ContainsNode(null);
        }
    
        [TestMethod]
        public void testNotContainsstring() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsFalse(g.ContainsNode(n2));
        }
    
        [TestMethod]
        public void testContainsOnestring() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsTrue(g.ContainsNode(n1));
        }
    
        [TestMethod]
        public void testContainsstringMultiple() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Assert.IsTrue(g.ContainsNode(n2));
            Assert.IsTrue(g.ContainsNode(n1));
        }
    
        [TestMethod]
        public void testContainsstringDuplicate() {
            AdjacencyListGraph<string, string> g = graph(n1, n1);
            Assert.IsTrue(g.ContainsNode(n1));
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // AddEdge(Edge<string, string>) Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testAddEdgeNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.AddEdge<string, string>(null);
        }
    
        [TestMethod]
        public void testAddEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e = new Edge<string, string>(n1, n2, "label");
            Assert.IsTrue(g.AddEdge(e));
        }
    
        [TestMethod]
        public void testAddBadEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e = new Edge<string, string>(n1, n3, "label");
            Assert.IsFalse(g.AddEdge(e));
        }
    
        [TestMethod]
        public void testAddDuplicateEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e = new Edge<string, string>(n1, n2, "label");
            Assert.IsTrue(g.AddEdge(e));
            Assert.IsFalse(g.AddEdge(e));
        }
    
        [TestMethod]
        public void testAddMultigraph() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e1 = new Edge<string, string>(n1, n2, "label");
            Edge<string, string> e2 = new Edge<string, string>(n2, n1, "label");
            Edge<string, string> e3 = new Edge<string, string>(n1, n2, "label2");
            Edge<string, string> e4 = new Edge<string, string>(n2, n1, "label2");
            Assert.IsTrue(g.AddEdge(e1));
            Assert.IsTrue(g.AddEdge(e2));
            Assert.IsTrue(g.AddEdge(e3));
            Assert.IsTrue(g.AddEdge(e4));
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // ContainsEdge(Edge<string, string>) Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testContainsEdgeNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.ContainsEdge<string, string>(null);
        }
    
        [TestMethod]
        public void testContainsSingleEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e = new Edge<string, string>(n1, n2, "label");
            g.AddEdge(e);
            Assert.IsTrue(g.ContainsEdge(e));
        }
    
        [TestMethod]
        public void testContainsNoEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e = new Edge<string, string>(n1, n3, "label");
            Assert.IsFalse(g.ContainsEdge(e));
        }
    
        [TestMethod]
        public void testContainsDuplicateEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e = new Edge<string, string>(n1, n2, "label");
            g.AddEdge(e);
            g.AddEdge(e);
            Assert.IsTrue(g.ContainsEdge(e));
        }
    
        [TestMethod]
        public void testContainsMultipleEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4);
            Assert.IsTrue(g.ContainsEdge(e1));
            Assert.IsTrue(g.ContainsEdge(e2));
            Assert.IsTrue(g.ContainsEdge(e3));
            Assert.IsTrue(g.ContainsEdge(e4));
        }
    
        [TestMethod]
        public void testContainsMultigraphEdge() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e1 = new Edge<string, string>(n1, n2, "label");
            Edge<string, string> e2 = new Edge<string, string>(n2, n1, "label");
            Edge<string, string> e3 = new Edge<string, string>(n1, n2, "label2");
            Edge<string, string> e4 = new Edge<string, string>(n2, n1, "label2");
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4);
            Assert.IsTrue(g.ContainsEdge(e1));
            Assert.IsTrue(g.ContainsEdge(e2));
            Assert.IsTrue(g.ContainsEdge(e3));
            Assert.IsTrue(g.ContainsEdge(e4));
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // Children Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testChildrenNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.Children(null);
        }
    
        [TestMethod]
        public void testChildrenWithEmptyGraph() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsNull(g.Children(n1));
        }
    
        [TestMethod]
        public void testChildrenWithChildlessParent() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsTrue(g.Children(n1).Count == 0);
        }
    
        [TestMethod]
        public void testChildrenSimple() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            g.AddEdge(e1); g.AddEdge(e4);
        
            ISet<string> Children1 = g.Children(n1);
            Assert.IsTrue(Children1.Contains(n2));
            Assert.IsTrue(Children1.Contains(n4));
            Assert.AreEqual(2, Children1.Count);
        }
    
        [TestMethod]
        public void testChildrenMultiGraph() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            Edge<string, string> e5 = new Edge<string, string>(n2, n1, "lorem");
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4); g.AddEdge(e5);
        
            ISet<string> Children1 = g.Children(n1);
            Assert.IsTrue(Children1.Contains(n2));
            Assert.IsTrue(Children1.Contains(n4));
            Assert.AreEqual(2, Children1.Count);
        
            ISet<string> Children2 = g.Children(n2);
            Assert.IsTrue(Children2.Contains(n3));
            Assert.IsTrue(Children2.Contains(n1));
            Assert.AreEqual(2, Children2.Count);
        
            ISet<string> Children3 = g.Children(n3);
            Assert.IsTrue(Children3.Contains(n4));
            Assert.AreEqual(1, Children3.Count);
        
            ISet<string> Children4 = g.Children(n4);
            Assert.AreEqual(0, Children4.Count);
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // Parents Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testParentsNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.Parents(null);
        }
    
        [TestMethod]
        public void testParentWithEmptyGraph() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsNull(g.Parents(n1));
        }
    
        [TestMethod]
        public void testParentWithParentlessChild() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsTrue(g.Children(n1).Count == 0);
        }
    
        [TestMethod]
        public void testParentsimple() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4);
        
            ISet<string> Parents4 = g.Parents(n4);
            Assert.IsTrue(Parents4.Contains(n1));
            Assert.IsTrue(Parents4.Contains(n3));
            Assert.AreEqual(2, Parents4.Count);
        }
    
        [TestMethod]
        public void testParentsMultiGraph() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            Edge<string, string> e5 = new Edge<string, string>(n2, n1, "lorem");
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4); g.AddEdge(e5);
        
            ISet<string> Parents1 = g.Parents(n1);
            Assert.IsTrue(Parents1.Contains(n2));
            Assert.AreEqual(1, Parents1.Count);
        
            ISet<string> Parents2 = g.Parents(n2);
            Assert.IsTrue(Parents2.Contains(n1));
            Assert.AreEqual(1, Parents2.Count);
        
            ISet<string> Parents3 = g.Parents(n3);
            Assert.IsTrue(Parents3.Contains(n2));
            Assert.AreEqual(1, Parents3.Count);
        
            ISet<string> Parents4 = g.Parents(n4);
            Assert.IsTrue(Parents4.Contains(n1));
            Assert.IsTrue(Parents4.Contains(n3));
            Assert.AreEqual(2, Parents4.Count);
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // GetEdges Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testGetEdgesOneNull() {
            AdjacencyListGraph<string, string> g = graph(n1);
            g.GetEdges(null, n1);
        }
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testGetEdgesBothNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.GetEdges(null, null);
        }
    
        [TestMethod]
        public void testGetEdgesWithUncontainedstrings() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsNull(g.GetEdges(n1, n2));
            Assert.IsNull(g.GetEdges(n2, n1));
            Assert.IsNull(g.GetEdges(n3, n4));
        }
    
        [TestMethod]
        public void testGetEdgesSimple() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
        
            ISet<string> s1 = g.GetEdges(n1, n2);
            Assert.IsTrue(s1.Contains(e1.Label));
            Assert.AreEqual(1, s1.Count);
        
            ISet<string> s2 = g.GetEdges(n2, n1);
            Assert.IsFalse(s2.Contains(e1.Label));
            Assert.AreEqual(0, s2.Count);
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // Edge<string, string>Exists Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testEdgeExistsNull() {
            AdjacencyListGraph<string, string> g = graph(n1);
            g.EdgeExists(null, n1);
        }
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testEdgeExistsBothNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.EdgeExists(null, null);
        }
    
        [TestMethod]
        public void testEdgeExistsWithUncontainedstrings() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsFalse(g.EdgeExists(n1, n2));
            Assert.IsFalse(g.EdgeExists(n2, n1));
            Assert.IsFalse(g.EdgeExists(n3, n4));
        }
    
        [TestMethod]
        public void testEdgeExistsSimple() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
        
            Assert.IsTrue(g.EdgeExists(n1, n2));
            Assert.IsFalse(g.EdgeExists(n2, n1));
        }
    
        [TestMethod]
        public void testGetEdgesMultiGraph() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            Edge<string, string> e5 = new Edge<string, string>(n2, n1, "lorem");
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4); g.AddEdge(e5);
        
            Assert.IsTrue(g.EdgeExists(n1, n2));
            Assert.IsTrue(g.EdgeExists(n2, n3));
            Assert.IsTrue(g.EdgeExists(n3, n4));
            Assert.IsTrue(g.EdgeExists(n1, n4));
            Assert.IsTrue(g.EdgeExists(n2, n1));
        
            Assert.IsFalse(g.EdgeExists(n4, n1));
            Assert.IsFalse(g.EdgeExists(n4, n3));
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // RemoveEdge(Edge<string, string>) Test
   
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testRemoveEdgeNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.RemoveEdge<string, string>(null);
        }
    
        [TestMethod]
        public void testRemoveEdgeNotExists() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsFalse(g.RemoveEdge(e1));
        }
    
        [TestMethod]
        public void testRemoveEdgeSimple() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
            Assert.IsTrue(g.ContainsEdge(e1));
            Assert.IsTrue(g.RemoveEdge(e1));
            Assert.IsFalse(g.ContainsEdge(e1));
            Assert.IsFalse(g.EdgeExists(n1, n2));
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // RemoveNode(string) Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testRemovestringNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.RemoveEdge<string, string>(null);
        }
    
        [TestMethod]
        public void testRemovestringNotExists() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsFalse(g.RemoveNode(n1));
        }
    
        [TestMethod]
        public void testRemovestringNoParents() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsTrue(g.RemoveNode(n1));
            Assert.IsFalse(g.ContainsNode(n1));
        }
    
        [TestMethod]
        public void testRemovestringWithChildren() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
        
            Assert.IsTrue(g.RemoveNode(n1));
            Assert.IsFalse(g.ContainsNode(n1));
            Assert.IsFalse(g.EdgeExists(n1, n2));
            Assert.IsNull(g.GetEdges(n1, n2));
        }
    
        [TestMethod]
        public void testRemovestringWithParents() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
        
            Assert.IsTrue(g.RemoveNode(n2));
            Assert.IsFalse(g.ContainsNode(n2));
            Assert.IsFalse(g.EdgeExists(n1, n2));
            Assert.IsNull(g.GetEdges(n1, n2));
            Assert.IsFalse(g.ContainsEdge(e1));
        }
    
        [TestMethod]
        public void testRemovestringWithParentsAndAddAgain() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
        
            g.RemoveNode(n2);
            g.AddNode(n2);
            Assert.IsTrue(g.ContainsNode(n2));
            Assert.IsFalse(g.ContainsEdge(e1));
            g.AddEdge(e1);
            Assert.IsTrue(g.ContainsEdge(e1));
        }
    
    
        // /////////////////////////////////////////////////////////////////////////
        // // setEdge<string, string>Data(Edge<string, string>) Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testSetEdgeDataNull() {
            AdjacencyListGraph<object, string> g = new AdjacencyListGraph<object, string>();
            g.AddNode(n1);
            g.AddNode(n2);
            g.AddEdge(e1);
            g.ReplaceEdge<object, string, string>(null, "");
        }
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testSetEdgeDataBothNull() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
            g.ReplaceEdge<string, string, string>(null, null);
        }
    
        [TestMethod]
        public void testSetEdgeDataNoDuplicate() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            g.AddEdge(e1);
        
            g.ReplaceEdge(e1, "new");
            Assert.IsFalse(g.ContainsEdge(e1));
            Assert.IsTrue(g.ContainsEdge(new Edge<string, string>(e1.Source, e1.Destination, "new")));
        }
    
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testSetEdgeDataDuplicate() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            Edge<string, string> e1 = new Edge<string, string>(n1, n2, "one");
            Edge<string, string> e2 = new Edge<string, string>(n1, n2, "two");
            g.AddEdge(e1); g.AddEdge(e2);
            g.ReplaceEdge(e1, "two");
        }
    
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testSetEdgeDataNoEdge() {
            AdjacencyListGraph<string, string> g = graph();
            g.ReplaceEdge(e1, "new_data");
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // ReplaceNode(string) Test
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testReplaceNodeNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.ReplaceNode(null, "new_label");
        }
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testReplaceNodeBothNull() {
            AdjacencyListGraph<string, string> g = graph();
            g.ReplaceNode(null, null);
        }
    
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testReplaceNodeNostring() {
            AdjacencyListGraph<string, string> g = graph();
            g.ReplaceNode(n1, "new_label");
        }
    
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testReplaceNodeDuplicate() {
            AdjacencyListGraph<string, string> g = graph();
            g.ReplaceNode(n1, "new_label");
        }
    
        [TestMethod]
        public void testReplaceNodeNoDuplicate() {
            AdjacencyListGraph<string, string> g = graph(n1);
        
            g.ReplaceNode(n1, "new_label");
            Assert.IsTrue(g.ContainsNode("new_label"));
            Assert.IsFalse(g.ContainsNode(n1));
        }
    
        [TestMethod]
        public void testReplaceNodeWithChildren() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e1 = new Edge<string, string>(n1, n2, "one");
            g.AddEdge(e1);
        
            g.ReplaceNode(n1, "new_label");
            Assert.IsFalse(g.ContainsNode(n1));
            Assert.IsTrue(g.ContainsNode("new_label"));
        
            Assert.IsFalse(g.EdgeExists(n1, n2));
            Assert.IsTrue(g.EdgeExists("new_label", n2));
        }
    
        [TestMethod]
        public void testReplaceNodeSelfAsChild() {
            AdjacencyListGraph<string, string> g = graph(n1);
            g.AddEdge(new Edge<string, string>(n1, n1, "label"));
        
            g.ReplaceNode(n1, "new_label");
            Assert.IsFalse(g.ContainsNode(n1));
            Assert.IsTrue(g.ContainsNode("new_label"));
            Assert.IsTrue(g.EdgeExists("new_label", "new_label"));
        }
    
        [TestMethod]
        public void testReplaceNodeWithParents() {
            AdjacencyListGraph<string, string> g = graph(n1, n2);
            Edge<string, string> e1 = new Edge<string, string>(n2, n1, "one");
            g.AddEdge(e1);
            Assert.IsTrue(g.EdgeExists(n2, n1));
        
            string n2_post = "new_label";
            g.ReplaceNode(n2, n2_post);
            Assert.IsFalse(g.ContainsNode(n2));
            Assert.IsTrue(g.ContainsNode(n2_post));
        
            Assert.IsFalse(g.EdgeExists(n2, n1));
            Assert.IsTrue(g.EdgeExists(n2_post, n1));
            Assert.AreEqual(1, g.GetEdges(n2_post, n1).Count);
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // stringSet Test
    
        [TestMethod]
        public void testNodeSetEmptystrings() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.AreEqual(0, g.NodeSet().Count);
        }
    
        [TestMethod]
        public void testNodeSetMultiplestrings() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            Assert.IsTrue(g.NodeSet().Contains(n1));
            Assert.IsTrue(g.NodeSet().Contains(n2));
            Assert.IsTrue(g.NodeSet().Contains(n3));
            Assert.IsTrue(g.NodeSet().Contains(n4));
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // size Test
    
        [TestMethod]
        public void testSizeZerostrings() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.AreEqual(0, g.Count);
        }
    
        [TestMethod]
        public void testSizeMultiplestrings() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3);
            Assert.AreEqual(3, g.Count);
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // sizeEdge<string, string>s Test
    
        [TestMethod]
        public void testSizeEdgesZeroEdges() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.AreEqual(0, g.CountEdges);
        }
    
        [TestMethod]
        public void testSizeEdgesMultigraphEdges() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4);
        
            Assert.AreEqual(4, g.CountEdges);
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // IsEmpty Test
    
        [TestMethod]
        public void testIsEmptyZerostrings() {
            AdjacencyListGraph<string, string> g = graph();
            Assert.IsTrue(g.IsEmpty());
        }
    
        [TestMethod]
        public void testIsEmptyOnestring() {
            AdjacencyListGraph<string, string> g = graph(n1);
            Assert.IsFalse(g.IsEmpty());
        }
    
        // /////////////////////////////////////////////////////////////////////////
        // // Clear Test
    
        [TestMethod]
        public void testClear() {
            AdjacencyListGraph<string, string> g = graph(n1, n2, n3, n4);
            g.AddEdge(e1); g.AddEdge(e2); g.AddEdge(e3); g.AddEdge(e4);
        
            g.Clear();
            Assert.IsTrue(g.IsEmpty());
        }
    }

}
