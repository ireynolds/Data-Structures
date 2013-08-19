using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graph
{
    /**
     * Defines a mutable directed labeled multigraph. 
     * 
     * A graph contains nodes and edges that connect two nodes. This graph contains
     * unique nodes as defined by their label, and unique edges as defined by the 
     * nodes they link and their label. It also does not allow null edges or nodes.
     *
     * The type parameter N is the type of the node data. The type parameter E is 
     * the type of the edge data.
     *
     * Note that each method specified by DirectedGraph throws a 
     * ArgumentNullException if any of its parameters is null.
     * 
     * @specfiedl node  : N             // A piece of data representing a node in 
     *                                     the graph.
     * @specfield nodes : Set<node>     // A collection of all vertices in the 
     *                                     graph, each containing some data.
     * @specfield edge  : Edge<N, E>    // A link between two nodes containing some 
     *                                     data.
     * @specfield edges : Set<edge>     // A collection of all edges in the graph.
     * 
     * 
     * @author Isaac Reynolds
     * @date 25 April 2012
     */
    public class AdjacencyListGraph<N, E> {
    
        public static bool IS_DEBUGGING = false;
    
        // Abstraction Function:
        //
        // AF(this) = a directed labeled multigraph g such that
        // 		graph.keySet() = nodes
        // 		graph.get(A).keySet() = A's children
        // 		graph.get(A).get(B) = Set of all edges from A to B
    
	
        // Representation Invariant:
        //
        // graph != null
        // !graph.keySet().contains(null)
        // !graph.valueSet().contains(null)
        // !graph.get(A).keySet().contains(null)
        // !graph.get(A).valueSet().contains(null)
        // !graph.get(A).get(B).contains(null)
        //
        // -OR, IN ENGLISH-
        //
        // There are no nulls in graph or any sub-collection in graph.
	    // 

        /**
         * Returns the number of Nodes in nodes.
         */
        public int Count
        {
            get { return graph.Count; }
        }

        /**
         * Returns the number of Edges in edges.
         */
        public int CountEdges
        {
            get
            {
                int size = 0;
                foreach (N src in graph.Keys)
                {
                    foreach (N dest in graph[src].Keys)
                    {
                        size += graph[src][dest].Count;
                    }
                }
                return size;
            }
        }
	
        // Represents a graph structure
        private readonly IDictionary<N, IDictionary<N, ISet<E>>> graph;
    
        /**
         * Creates a new AdjacencyListGraph with no nodes or edges.
         */
        public AdjacencyListGraph() {
            graph = new Dictionary<N, IDictionary<N, ISet<E>>>();
            CheckRep();
        }
    
        /**
         * If edges does not contain e, adds e (ensures that there are no 
         * duplicates). Returns true if edges was changed as a result of this call. 
         *
         * @param e     the Edge to add
         * @return      true if edges changed as a result of this call
         * @modifies    edges
         * @effects     edges - adds e
         */
        public bool AddEdge<TYPE1, TYPE2>(Edge<TYPE1, TYPE2> Edge) where TYPE1 : N
                                                                  where TYPE2 : E 
        {
            if (Edge == null)
                throw new ArgumentNullException();

            N source = Edge.Source;
            N destination = Edge.Destination;
            if (!ContainsNode(source) || !ContainsNode(destination))
                return false;
            CheckRep();
        
            bool changed = false;
            N src = source;
            N dest = destination;
        
		    // if dest doesn't exist as a child of src, add it, then add edge
            if (!graph[src].ContainsKey(dest))
                graph[src].Add(dest, new HashSet<E>());
            changed = graph[src][dest].Add(Edge.Label);
        
            CheckRep();
            return changed;
        }
    
        /**
         * If nodes does not contain n, adds n (ensuring that there are no
         * duplicates). Returns true if nodes was changed as a result of this call.
         *
         * @param n     the Node to add
         * @return      true if nodes changed as a result of this call
         * @modifies    nodes
         * @effects     nodes - adds n
         */
        public bool AddNode(N Node) {
            if (Node == null)
                throw new ArgumentNullException();
            CheckRep();
		
		    bool needsChange  = !ContainsNode(Node);
		    if (needsChange) {
			    graph.Add(Node, new Dictionary<N, ISet<E>>());
		    }
		
            CheckRep();
            return needsChange;
        }
    
        /**
         * Returns the Set of n's children. That is, returns all nodes y such that 
         * there is an edge from n to y. Returns null if n is not in nodes.
         * 
         * @param n     the node whose children to get
         * @return      a Set of all Nodes y such that there exists an Edge from n 
         *              to y, or null if n is not in nodes.
         */
        public ISet<N> Children(N Parent) {
            if (Parent == null)
                throw new ArgumentNullException();
            if (!ContainsNode(Parent))
                return null;

            return new HashSet<N>(graph[Parent].Keys);
        }
    
        /**
         * Removes all nodes from nodes, and all edges from edges.
         * 
         * @modifies    nodes
         *              edges
         * @effects     nodes - makes nodes empty
         *              edges - makes edges empty
         */
        public void Clear() {
            graph.Clear();
        }
    
        /** 
         * Returns true if edges contains e.
         * 
         * @param n     Edge whose presence in edges is to be tested.
         * @return      true if e is in edges
         */
        public bool ContainsEdge<TYPE1, TYPE2>(Edge<TYPE1, TYPE2> e) where TYPE1 : N
                                                                     where TYPE2 : E 
        {
            if (e == null)
                throw new ArgumentNullException();

            N source = e.Source;
            N destination = e.Destination;
            if (!ContainsNode(source) || !ContainsNode(destination))
                return false;
            if (!EdgeExists(source, destination))
                return false;
        
            return GetEdges(source, destination).Contains(e.Label);
        }
    
        /** 
         * Returns true if nodes contains n.
         * 
         * @param n     Node whose presence in nodes is to be tested.
         * @return      true if n is in nodes
         */
        public bool ContainsNode(N n) {
            if (n == null)
                throw new ArgumentNullException();
        
            return graph.ContainsKey(n);
        }
    
        /**
         * Returns true if there exists an edge from source to destination, or false
         * otherwise.
         * 
         * @param source        the Node from which the edge is directed
         * @param destination   the Node toward which the edge is directed
         * @return              true if there exists an edge from source to
         *                      destination. False if no such edge exists, or if 
         *                      source or destination is not in nodes.
         */
        public bool EdgeExists(N source, N destination) {
            if (source == null || destination == null)
                throw new ArgumentNullException();
            if (!ContainsNode(source) || !ContainsNode(destination))
                return false;
        
            if (graph[source].ContainsKey(destination))
			    // if the last edge were ever deleted, it would leave the empty
			    // set behind.
                return graph[source][destination].Count != 0;
            else
                return false;
        }
    
        /**
         * Returns a Set of the data of all edges e directed from source to 
         * destination. Returns null if one or both of source and destination isn't 
         * in nodes.
         * 
         * @param source        the Node from which e is directed
         * @param destination   the Node to which e is directed
         * @return              a Set of Edges from source to destination, or null
         *                      if one or both of source and destination isn't in
         *                      nodes.
         */
        public ISet<E> GetEdges(N source, N destination) {
            if (source == null || destination == null)
                throw new ArgumentNullException();
            if (!ContainsNode(source) || !ContainsNode(destination))
                return null;
        
            ISet<E> edges;
            if (EdgeExists(source, destination))
                edges = new HashSet<E>(graph[source][destination]);
            else
                edges = new HashSet<E>();
        
            return edges;
        }
    
        /** 
         * Returns true if nodes is empty.
         *
         * @return true if size() == 0.
         */
        public bool IsEmpty() {
            return Count == 0;
        }
    
        /**
         * Returns nodes.
         * 
         * @return a Set of all nodes in nodes
         */
        public ISet<N> NodeSet() {
            return new HashSet<N>(this.graph.Keys);
        }
    
        /** 
         * Returns n's parents. That is, returns a Set of nodes y such that there 
         * exists an Edge from y to n. Returns null if n is not in nodes.
         * 
         * @param n     the node whose parents to get
         * @return      a Set of all Nodes y such that there exists an Edge from y 
         *              to n, or null if n is not in nodes.
         */
        public ISet<N> Parents(N Node) {
            if (Node == null)
                throw new ArgumentNullException();
            if (!ContainsNode(Node))
                return null;
        
            ISet<N> parents = new HashSet<N>();
            foreach (N p in graph.Keys) {
                if (graph[p].ContainsKey(Node))
                    parents.Add(p);
            }
        
            return parents;
        }
    
        /**
         * If e is in edges, removes e.
         * 
         * @param n     Edge to remove
         * @return      true if edges changed as a result of this call
         * @modifies    edges
         * @effects     edges - removes e
         */
        public bool RemoveEdge<TYPE1, TYPE2>(Edge<TYPE1, TYPE2> e) where TYPE1 : N
                                                                   where TYPE2 : E 
        {
            if (e == null)
                throw new ArgumentNullException();

            N source = e.Source;
            N destination = e.Destination;
            if (!ContainsEdge(e))
                return false;
            CheckRep();
        
            bool changed = graph[source][destination].Remove(e.Label);
        
            CheckRep();
            return changed;
        }
    
        /**
         * If n is in nodes, removes n and all Edges to and from n.
         * 
         * @param n     Node to remove
         * @return      true if nodes changed as a result of this call
         * @modifies    nodes
         * @effects     nodes - removes n
         */
        public bool RemoveNode(N n) {
            if (n == null)
                throw new ArgumentNullException();
            if (!ContainsNode(n))
                return false;
            CheckRep();
        
            foreach (N p in Parents(n)) {
                graph[p].Remove(n);
            }
            graph.Remove(n);
        
            CheckRep();
            return true;
        }
    
        /**
         * If e is in edges, sets e's data to the parameter data and returns a new 
         * Edge reflecting the changes.
         * 
         * @param oldEdge   Edge whose data to set
         * @param newLabel  Represents the new data
         * @return      a new Edge reflecting the changes
         * @modifies    edges
         * @effects     edges - !edges.contains(e) & edges.contains(e_post)
         * @throws      InvalidOperationException - if edges does not contain e
         * @throws      InvalidOperationException - if edges contains e_post
         */
        public void ReplaceEdge<T, S, P>(Edge<T, S> oldEdge, P newLabel) where T : N 
                                                                         where S : E 
                                                                         where P : S 
        {
            if (oldEdge == null || newLabel == null)
                throw new ArgumentNullException();

            T source = oldEdge.Source;
            T destination = oldEdge.Destination;
            S oldLabel = oldEdge.Label;
            if (oldLabel == null || newLabel == null)
                throw new ArgumentNullException();
            if (!ContainsNode(source) || !ContainsNode(destination))
                throw new InvalidOperationException();
            if (!ContainsEdge(oldEdge))
                throw new InvalidOperationException();
            CheckRep();
        
            RemoveEdge(oldEdge);
            Edge<T, P> newEdge = new Edge<T, P>(source, destination, newLabel);
            if (ContainsEdge(newEdge))
                throw new InvalidOperationException();
            AddEdge(newEdge);
        
            CheckRep();
        }
    
        /**
         * If n is in nodes, sets n's data to the parameter data and returns a new 
         * Node reflecting the changes.
         * 
         * @param oldLabel  Node whose data to set
         * @param newLabel  String representing the new data
         * @return      a new Node reflecting the changes
         * @modifies    nodes
         * @effects     nodes - !nodes.contains(n_pre) & nodes.contains(n_post)
         * @throws      InvalidOperationException - if nodes does not contain n
         * @throws      InvalidOperationException - if nodes contains n_post
         */
        public void ReplaceNode(N oldLabel, N newLabel) {
            if (oldLabel == null || newLabel == null)
                throw new ArgumentNullException();
            if (!ContainsNode(oldLabel))
                throw new InvalidOperationException();
            CheckRep();
        
            if (ContainsNode(newLabel))
                throw new InvalidOperationException();
        
            AddNode(newLabel);
		    // for every edge <p, n> create a new edge <p, n_post>
            foreach (N parent in Parents(oldLabel)) {
                foreach (E edgeLabel in GetEdges(parent, oldLabel)) {
                    AddEdge(new Edge<N, E>(parent, newLabel, edgeLabel));
                }
            }
		    // for every edge <n, c> create a new edge <n_post, c>
            foreach (N child in Children(oldLabel)) {
                foreach (E edgeLabel in GetEdges(oldLabel, child)) {
                    AddEdge(new Edge<N, E>(newLabel, child, edgeLabel));
                }
            };
            RemoveNode(oldLabel);
        
            CheckRep();
        }
    
        private void CheckRep() {
            if (!IS_DEBUGGING)
                return;
        
            // nodes != null
            Assert(graph != null);
            // ! nodes.containsValue(null)
            Assert(!graph.Values.Contains(null));
            foreach (N src in graph.Keys) {
                IDictionary<N, ISet<E>> children = graph[src];
                // ! nodes.get(A).containsValue(null) if A is in nodes
                Assert(!children.Values.Contains(null));
            }
        }
   
        private void Assert(bool condition) 
        {
            if (!condition) 
            {
                throw new Exception();
            }
        }
    }

    /**
     * Edge represents an immutable directed edge  in a graph structure. This Edge 
     * class is meant to be used with a class that implements the DirectedGraph 
     * interface.
     * 
     * @specfield Label       : String  // A piece of data stored with this edge 
     *                                     which identifies it uniquely among the 
     *                                     edges in the Graph.
     * @specfield Source      : Node    // The Node away from which this edge 
     *                                     points.
     * @specfield Destination : Node    // The Node toward which this edge points.
     * 
     * @author Isaac
     * @date 25 April 2012
     * @see DirectedGraph
     */
    public class Edge<N, E> {
    
        /**
         * Stores this edge's label.
         */
        private readonly E label;
    
        /**
         * Stores this edge's source.
         */
        private readonly N source;
    
        /**
         * Stores this edge's destination.
         */
        private readonly N destination;

        /**
         * Returns label.
         * 
         * @return this edge's label
         */
        public E Label
        {
            get
            {
                return label;
            }
        }

        /**
         * Returns source.
         * 
         * @return this edge's source
         */
        public N Source
        {
            get
            {
                return source;
            }
        }

        /**
         * Returns destination.
         * 
         * @return this edge's destination
         */
        public N Destination
        {
            get
            {
                return destination;
            }
        }
    
        /**
         * Creates a new Edge.
         * 
         * @require             source != null
         * @require             destination != null
         * @param source        this edge's source
         * @param destination   this edge's destination
         * @param label         this Node's label
         */
        public Edge(N source, N destination, E label) {
            if (source == null || destination == null || label == null)
                throw new ArgumentNullException();
        
            this.label = label;
            this.source = source;
            this.destination = destination;
        }
    
        public static Edge<N, E> makeEdge(N source, N destination, E label) {
            return new Edge<N, E>(source, destination, label);
        }
    
        /**
         * Creates a new Edge e2 such that e2.equals(e).
         * 
         * @param e     the edge to copy.
         */
        public Edge(Edge<N, E> e)
            : this(e.source, e.destination, e.label) { }   
    
        /**
         * {@inheritDoc}
         */
        public override int GetHashCode() {
            int result = 17;
            int c = 0;
        
            c = label.GetHashCode();
            result = 31 * result + c;
            c = source.GetHashCode();
            result = 31 * result + c;
            c = destination.GetHashCode();
            result = 31 * result + c;
        
            return result;
        }
    
        /**
         * {@inheritDoc}
         */
        public override bool Equals(Object o) {
            if (o.GetType() == typeof(Edge<N, E>)) {
            
                Edge<N, E> e = (Edge<N, E>) o;
                return source.Equals(e.source) && destination.Equals(e.destination)
                        && label.Equals(e.label);
            }
            return false;
        }
    
        /**
         * {@inheritDoc}
         */
        public override String ToString() {
            return Label + ": " + source + " -> "
                    + destination;
        }
    }


}
