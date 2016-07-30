using System;
using System.Collections;

class A{

    static IEnumerator GetValue(){
        yield return 3;
        yield return 8;
        yield return 11;
        yield break;
    }
    
    public static void Main()
        {
            foreach( var i in GetValue() ){
                Console.WriteLine( "i:" + i );
            }
        }

}
