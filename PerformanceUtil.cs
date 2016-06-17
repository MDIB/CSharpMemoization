using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support
{
    public class PerformanceUtil
    {
        private static PerformanceUtil INSTANCE;

        private Dictionary<String, Object> MemoizedFunctionsCache = new Dictionary<string, object>();

        private PerformanceUtils()
        {

        }

        public static PerformanceUtil GetInstance()
        {
            if (INSTANCE == null)
                INSTANCE = new PerformanceUtil();
            return INSTANCE;
        }

        public Func<P1, R> Memoize<P1, R>(Func<P1, R> originalFunction)
        {
            Object cached;

            if (MemoizedFunctionsCache.TryGetValue(originalFunction.Method.Name, out cached))
                return (Func<P1, R>)cached;

            var InternDictionary = new Dictionary<P1, R>();

            Func<P1, R> wrappedFunction = (a) =>
            {
                R r;
                lock (InternDictionary)
                {
                    if (!InternDictionary.TryGetValue(a, out r))
                    {
                        r = originalFunction(a);
                        InternDictionary.Add(a, r);
                    }
                }
                return r;
            };

            MemoizedFunctionsCache.Add(originalFunction.Method.Name, wrappedFunction);

            return wrappedFunction;
        }
        
        public Func<P1, P2, R> Memoize<P1, P2, R>(Func<P1, P2, R> original)
        {
            Object cached;

            if (MemoizedFunctionsCache.TryGetValue(original.Method.Name, out cached))
                return (Func<P1, P2, R>)cached;

            var d = new Dictionary<string, R>();

            Func<P1, P2, R> fn = (p1, p2) =>
            {
                R r;
                lock (d)
                {
                    if (!d.TryGetValue(p1.ToString() + p2.ToString(), out r))
                    {
                        r = original(p1, p2);
                        d.Add(p1.ToString() + p2.ToString(), r);
                    }
                }
                return r;
            };

            MemoizedFunctionsCache.Add(original.Method.Name, fn);

            return fn;
        }

        public Func<P1, P2, P3, R> Memoize<P1, P2, P3, R>(Func<P1, P2, P3, R> original)
        {
            Object cached;

            if (MemoizedFunctionsCache.TryGetValue(original.Method.Name, out cached))
                return (Func<P1, P2, P3, R>)cached;

            var d = new Dictionary<string, R>();

            Func<P1, P2, P3, R> fn = (p1, p2, p3) =>
            {
                R r;
                lock (d)
                {
                    if (!d.TryGetValue(p1.ToString() + p2.ToString() + p3.ToString(), out r))
                    {
                        r = original(p1, p2, p3);
                        d.Add(p1.ToString() + p2.ToString() + p3.ToString(), r);
                    }
                }
                return r;
            };

            MemoizedFunctionsCache.Add(original.Method.Name, fn);

            return fn;
        }
    }
}
