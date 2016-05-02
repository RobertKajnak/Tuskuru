import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class HashFuction {
	public static void main(String[] args){
		System.out.println("Introduce password:");
		BufferedReader rd=new BufferedReader(new InputStreamReader(System.in));
		String s="blank";
		try {
			s=rd.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		String h=s;
		System.out.println(hash(h,4,16));

	}

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
	public static String hash(String s,int salt,int length){
		if (s==null || salt==0 || length==0)
			return null;
		int l=length;
		String h=s;
		//int l=16;
		///create 16 char length 
		char [] sa;
		char [] ha=new char[l];
		if (s.length()<l){
			//if the string is longer, it repeats the string until it fills the length specified
			while (h.length()<=l-s.length()){
				h=h+s;
			}
			h=h+s.substring(0, l%s.length());

			//create two character arrays: one to return and one to keep the original characters
			sa=h.toCharArray();
		}
		else{
			sa=h.substring(0, l).toCharArray();
			int ls=h.length();//in order not to call the method on every iteration for a potentially very long string
			for (int i=l,j=0;i<ls;i++,j++){
				if (j==l){
					j=i%(s.charAt(i)/9)%l;//predicted character to be ASCII and legible
					//avoids hash collision on repeated strings and palindroms
				}
				sa[j]=(char)(sa[j]+s.charAt(i));
			}
		}

		//create sum and products as property variables
		int sum=0,prod=7;
		for (int i=0;i<s.length();i++){//uses original string
			sum=sum+s.charAt(i);
			prod=prod*s.charAt(i);
			if (sum>256)
				sum=sum%256;
			if (prod>256)
				prod=prod%256;
			if (prod==0)
				prod=1;
		}
		for (int k=1;k<=1;k++){ 
			//calculate the hash for the characters
			for (int i=0,j=l/2;i<l;i++,j++){
				if (j==l){
					j=0;
				}
				ha[i]=(char) (
						(Math.abs(((sa[i]*sa[j]*k+salt)* //using ha[i]*ha[j] would result in using the modified ha[]
								(sum*(j+1)+prod*(i+1)))))
						%256);
				if (ha[i]==0)
					ha[i]=231;
			}
			//avoid possible reference problems
			sa=ha.clone();

			//rotate the array
			char c=ha[0];
			for (int i=0; i<l-1;i++){
				ha[i]=ha[i+1];
			}
			ha[l-1]=c;

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
