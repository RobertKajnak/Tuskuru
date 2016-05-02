using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTester
{
    class Program
        {
	    public static void Main(String[] args)
        {
		    Console.WriteLine("Introduce password:");
            string s=Console.ReadLine();
		
		    String h=s;
		    Console.WriteLine(Hasher.hash(h,4,16));
            Console.ReadLine();
		}
	    
    }
}
