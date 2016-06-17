Memoization is a simple technique for making pure functions that has a computational cost be more performatic. This is achievied by a cached version of the function that will lookup the arguments in a HashTable (c# Dictionary) before it really call the function. If the arguments exists it will return the value associated with that argurments, if not it will call the function and then cache the result and value in the HashTable.

Simple demonstration on how to use it ->



static void Main(String[] args){

	Stopwatch pureCall = Stopwatch.StartNew();	
	BigInteger a = factorial(100);
	
	pureCall.Stop();
	Stopwatch firstCall = Stopwatch.StartNew();
    
    BigInteger l = Utils.GetInstance().Memoize<BigInteger, BigInteger>(factorial)(100);
    firstCall.Stop();
    
    Stopwatch secondCall = Stopwatch.StartNew();
    
    BigInteger z = Utils.GetInstance().Memoize<BigInteger, BigInteger>(factorial)(100);
    secondCall.Stop();

    Console.WriteLine(String.Format("Runtime of the function with memoize with not cached params: {0}", firstCall.ElapsedMilliseconds));

    Console.WriteLine(String.Format("Runtime of the function with memoize with cached params : {0}", secondCall.ElapsedMilliseconds));
}


static BigInteger factorial(BigInteger n) 
{
	if (n < 2) return n;
	return n* factorial(n - 1);
}