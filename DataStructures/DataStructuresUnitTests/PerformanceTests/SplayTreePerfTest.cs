using DataStructures.Set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresUnitTests.PerformanceTests
{
    public class SplayTreeTimeTests
    {
        public static void main(String[] args) {
		    for (int i = 0; i <= 20; i++) {
			    SplayTree t = new SplayTree();
			
			    int n = (int)Math.Pow(2, i);	// number of elements at max size
			    long m = 3 * n;					// number of operations (insert + lookup)
			    long T = 0;						// number of single rotations
			
			    if (n > Int32.MaxValue)
				    throw new Exception();
			
			    for (int m1 = 0; m1 < n; m1++) 
				    t.insert(m1);
			
			    lookupRand(t, n, m);
			    lookupRand(t, n, m);
			
			    T = t.numRotations;
			    double tm = (double)T / m ;				// T / m
			    double ln = Math.Log(n) / Math.Log(2);	// log_2(n)
			    Console.WriteLine(i + ": ");
                Console.WriteLine(String.Format("({0}, {1})", ln, tm));
		    }
	    }

        private static void lookupRand(SplayTree t, int max, long numOps)
        {
            Random r = new Random();
            for (int i = 0; i < numOps; i++)
            {
                t.lookup(r.Next(max));
            }
        }
    }
}
