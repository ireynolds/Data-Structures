using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Set
{
    public enum Direction {
	    Left,
	    Right
    }

    /**
     * A splay tree.
     * 
     * @author Isaac
     * @date 10/26/2012
     */
    public class SplayTree {

	    private Node overallRoot;

	    /**
	     * Number of single rotations performed on this tree. Public for testing
	     * purposes.
	     */
	    public long NumRotations;

	    public SplayTree() 
            : this(null) { }

	    private SplayTree(Node root) {
		    overallRoot = root;
	    }

	    /**
	     * Performs a single rotation at node in the given direction (a left
	     * rotation will move node left and replace node with its right child).
	     * 
	     * @param root
	     * @param direction
	     */
	    // Returns a reference to the new root in this tree (what replaced node).
	    // After this operation, you MUST reassign the parents' subtree to the
	    // return value.
	    private Node Rotate(Node node, Direction direction) {
		    Node n = null;
		    switch (direction) {
		    case Direction.Right:
			    n = node.left;
			    node.left = n.right;
			    n.right = node;
			    break;
		    case Direction.Left:
			    n = node.right;
			    node.right = n.left;
			    n.left = node;
			    break;
		    }
		    NumRotations++;
		    return n;
	    }

	    /**
	     * If key is in this, brings it to the root. Otherwise brings to the root a
	     * key in this that would neighbor key in the dictionary ordering.
	     * 
	     * @param key
	     * @return the new root of this
	     */
	    private void Splay(int key) {
		    Tuple ret = Splay(key, overallRoot);
		    overallRoot = ret.rootNode;

		    // If the key was left one down from the root, move it up.
		    Node r = overallRoot;
		    if (r == null)
			    return;
		    if (r.left != null && r.left.key == ret.splayedKey) {
			    overallRoot = Rotate(overallRoot, Direction.Right);
		    } else if (r.right != null && r.right.key == ret.splayedKey) {
			    overallRoot = Rotate(overallRoot, Direction.Left);
		    }
	    }

	    // Private recursive companion to splay(int)
	    private Tuple Splay(int key, Node current) {
		    // If you couldn't find key in the tree, return null
		    if (current == null)
			    return new Tuple(null, 0);

		    // Recurse down to find key
		    Tuple ret = null;
		    if (key == current.key) {
			    return new Tuple(current, current.key);

		    } else if (key < current.key) {
			    ret = Splay(key, current.left);
			    current.left = ret.rootNode;

		    } else { // key > current.key
			    ret = Splay(key, current.right);
			    current.right = ret.rootNode;

		    }

		    // If the returned node is null, then key isn't in this tree, so we
		    // should splay around the current node instead.
		    if (ret.rootNode == null)
			    return new Tuple(current, current.key);

		    // newCurrent will store a reference to the node that will replace
		    // current in the tree.
		    Node newCurrent = null;

		    // Perform the appropriate rotation if at depth 2, or return if at
		    // depth 1.
		    Node ll = (current.left != null) ? current.left.left : null;
		    Node lr = (current.left != null) ? current.left.right : null;
		    Node rr = (current.right != null) ? current.right.right : null;
		    Node rl = (current.right != null) ? current.right.left : null;
		    if (ll != null && ll.key == ret.splayedKey) {
			    // first rotate around current, then rotate around whatever took
			    // current's place (rotate grandparent of splayedKey, then parent).
			    newCurrent = Rotate(current, Direction.Right);
			    newCurrent = Rotate(newCurrent, Direction.Right);

		    } else if (lr != null && lr.key == ret.splayedKey) {
			    // first rotate around current's left child, then rotate around
			    // current.
			    current.left = Rotate(current.left, Direction.Left);
			    newCurrent = Rotate(current, Direction.Right);

		    } else if (rr != null && rr.key == ret.splayedKey) {
			    newCurrent = Rotate(current, Direction.Left);
			    newCurrent = Rotate(newCurrent, Direction.Left);

		    } else if (rl != null && rl.key == ret.splayedKey) {
			    current.right = Rotate(current.right, Direction.Right);
			    newCurrent = Rotate(current, Direction.Left);

		    } else { // the splayedNode is at depth one
			    newCurrent = current;
		    }

		    return new Tuple(newCurrent, ret.splayedKey);
	    }
	
	    /**
	     * Returns true iff key is in this.
	     * 
	     */
	    // Splay the tree around key and then check if key is at the root.
	    public bool Lookup(int key) {
		    Splay(key);
		    return overallRoot != null && overallRoot.key == key;
	    }

	    /**
	     * Inserts the given key into this if it not already in this.
	     * 
	     */
	    // Splay the tree around key and then place key at the root.
	    public void Insert(int key) {
		    Splay(key);
		    Node newRoot = new Node(key);
		    if (overallRoot == null) {
			    overallRoot = newRoot;
		    } else if (overallRoot.key != key) {
			    int rootKey = overallRoot.key;
			    // Replace the root with the new key and move old root's subtrees
			    // to maintain the search tree property.
			    if (rootKey < key) {
				    newRoot.left = overallRoot;
				    newRoot.right = overallRoot.right;
				    overallRoot.right = null;
				    overallRoot = newRoot;
			    } else if (rootKey > key) {
				    newRoot.right = overallRoot;
				    newRoot.left = overallRoot.left;
				    overallRoot.left = null;
				    overallRoot = newRoot;
			    }
		    }
	    }

	    /**
	     * If every key in t1 is less than every key in t2, creates a binary search
	     * tree containing all keys in t1 and t2, then returns it.
	     * 
	     * @return the new SplayTree
	     */
	    // Get n2, the root of t2. Splay t1 around n2 so that the root of t1 has
	    // only a left subtree.
	    private SplayTree Concat(SplayTree t1, SplayTree t2) {
		    if (t2.overallRoot != null && t1.overallRoot != null) {
			    t1.Splay(t2.overallRoot.key);
			    t1.overallRoot.right = t2.overallRoot;
			    return t1;
		    } else if (t2.overallRoot != null) {
			    return t2;
		    } else if (t1.overallRoot != null) {
			    return t1;
		    } else { // both empty
			    return new SplayTree();
		    }
	    }

	    /**
	     * Removes key from this.
	     * 
	     */
	    // Splay the tree around K, then remove root and concatenate its two
	    // subtrees.
	    public void Delete(int key) {
		    Splay(key);
		    if (overallRoot != null && overallRoot.key == key) {
			    SplayTree t1 = new SplayTree(overallRoot.left);
			    SplayTree t2 = new SplayTree(overallRoot.right);
			    overallRoot = Concat(t1, t2).overallRoot;
		    }
	    }

	    /**
	     * Prints a string representation of this to the console, and returns that
	     * string.
	     * 
	     */
	    public String Display() {
		    StringBuilder sb = new StringBuilder();
		    Display(overallRoot, 0, sb);
		    return sb.ToString();
	    }

	    // Private companion to display()
	    private void Display(Node current, int level, StringBuilder sb) {
		    String output = "";
		    for (int i = 0; i < level; i++)
			    output += "  ";
		    output += (current == null) ? "-" : current.key.ToString();
		    output += "\n";
		    sb.Append(output);

		    if (current != null && (current.left != null || current.right != null)) {
			    Display(current.left, level + 1, sb);
			    Display(current.right, level + 1, sb);
		    }
	    }
	    private class Node {
		    public Node left;
		    public Node right;
		    public readonly int key;

		    public Node(Node left, Node right, int key) {
			    this.left = left;
			    this.right = right;
			    this.key = key;
		    }

		    public Node(int key) 
                : this(null, null, key) { }
	    }

	    private class Tuple {
		    public readonly Node rootNode;
		    public readonly int splayedKey;

		    public Tuple(Node parentNode, int splayedKey) {
			    this.rootNode = parentNode;
			    this.splayedKey = splayedKey;
		    }
	    }
    }
}
