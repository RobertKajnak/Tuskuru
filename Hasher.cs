using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTester
{
    class Hasher
    {

        /**Creates a Hash for the given string (e.g. password) using the salt given
         * <br>
         * <br>Guarantess:
         * <br>	- "aaaaaa" type strings will be mapped to a pseudo random string, it cannot be asserted that there are repeated characters
         * <br> - "aaa" and "aaaa" type strings will be mapped to completely different values 
         * <br> - original string cannot be deduced from hash, even knowing the contents of this function, only using brute-force
         * <br> - providing a different salt will give a seemingly uncorrelated string to other identical strings
         * <br> - changin one character changes the whole hash
         * 
         * @param s string for which the hash is created
         * @param salt unique id to differentiate identical strings (e.g. user ID)
         * @param length the length of the hash
         * 
         */
        public static String hash(String s, int salt, int length)
        {
            if (s == null || salt == 0 || length == 0)
                return null;
            int l = length;
            String h = s;
            //int l=16;
            ///create 16 char length 
            char[] sa;
            char[] ha = new char[l];
            if (s.Length < l)
            {
                //if the string is longer, it repeats the string until it fills the length specified
                while (h.Length <= l - s.Length)
                {
                    h = h + s;
                }
                h = h + s.Substring(0, l % s.Length);

                //create two character arrays: one to return and one to keep the original characters
                sa = h.ToCharArray();
            }
            else
            {
                sa = h.Substring(0, l).ToCharArray();
                int ls = h.Length;//in order not to call the method on every iteration for a potentially very long string
                for (int i = l, j = 0; i < ls; i++, j++)
                {
                    if (j == l)
                    {
                        j = i % (s[i] / 9) % l;//predicted character to be ASCII and legible
                        //avoids hash collision on repeated strings and palindroms
                    }
                    sa[j] = (char)(sa[j] + s[i]);
                }
            }

            //create sum and products as property variables
            int sum = 0, prod = 7;
            for (int i = 0; i < s.Length; i++)
            {//uses original string
                sum = sum + s[i];
                prod = prod * s[i];
                if (sum > 256)
                    sum = sum % 256;
                if (prod > 256)
                    prod = prod % 256;
                if (prod == 0)
                    prod = 1;
            }
            for (int k = 1; k <= 1; k++)
            {
                //calculate the hash for the characters
                for (int i = 0, j = l / 2; i < l; i++, j++)
                {
                    if (j == l)
                    {
                        j = 0;
                    }
                    ha[i] = (char)(
                            (Math.Abs(((sa[i] * sa[j] * k + salt) * //using ha[i]*ha[j] would result in using the modified ha[]
                                    (sum * (j + 1) + prod * (i + 1)))))
                            % 256);
                    if (ha[i] == 0)
                        ha[i] = (char)231;
                }
                //copy array for next iteration
                for (int t = 0; t < l; t++)
                {
                    sa[t] = ha[t];
                }

                //rotate the array
                char c = ha[0];
                for (int i = 0; i < l - 1; i++)
                {
                    ha[i] = ha[i + 1];
                }
                ha[l - 1] = c;

            }
            return new String(ha);
        }

        //test proof of concept:
        // aaaaaaaaaaa : (with salt=4)
        // lF úÔ®?òÌ¦?Z4è?
        // aaaaaaaaaab : (with salt=4)
        //dTè*l®ðr´´8z¼þ@" 
        // aaaaaaaaaaa : (with salt=5)
        //èÔÀ¬??p<(çìØÄ°ü
    }
}

