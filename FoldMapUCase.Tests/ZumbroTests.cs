    using System;
    using Microsoft.FSharp.Collections;
    using Microsoft.FSharp.Core;
    using NUnit.Framework;

namespace FoldMapUCase.Tests
{
    [TestFixture]
    public class TestFoldMapUCase
    {
        [Test]
        public void CheckAlwaysTwo()
        {
            // simple example to show how to access F# function from C#
            int n = Zumbro.AlwaysTwo;
            Assert.AreEqual(2, n);
        }

        public static FSharpList<T> mkList<T>(params T[] ar)
        {
            FSharpList<T> foo = FSharpList<T>.Empty;
            for (int n = ar.Length - 1; n >= 0; n--)
                foo = FSharpList<T>.Cons(ar[n], foo);
            return foo;
        }


        [Test]
        public void foldl1()
        {
            const int seed = 64;
            var values = mkList(4, 2, 4);
            var fn = FuncConvert.ToFSharpFunc((int a) => FuncConvert.ToFSharpFunc((int b) => a / b));

            int result = Zumbro.foldl( fn, seed, values);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void foldl0()
        {
            const string seed = "hi mom";
            FSharpList<string> values = mkList<string>();
            var fn = FuncConvert.ToFSharpFunc<string, FSharpFunc<string, string>>(x => { throw new Exception("should never be invoked"); } );

            string result = Zumbro.foldl(fn, seed, values);
            Assert.AreEqual(seed, result);
        }

        [Test]
        public void map()
        {
            FSharpFunc<int, int> fn = FuncConvert.ToFSharpFunc((int a) => a*a);

            FSharpList<int> vals = mkList(1, 2, 3);
            FSharpList<int> res = Zumbro.map(fn, vals);

            Assert.AreEqual(res.Length, 3);
            Assert.AreEqual(1, res.Head);
            Assert.AreEqual(4, res.Tail.Head);
            Assert.AreEqual(9, res.Tail.Tail.Head);
        }

        [Test]
        public void ucase()
        {
            FSharpList<string> vals = mkList("arnold", "BOB", "crAIg");
            FSharpList<string> exp = mkList("ARNOLD", "BOB", "CRAIG");
            FSharpList<string> res = Zumbro.ucase(vals);
            Assert.AreEqual(exp.Length, res.Length);
            Assert.AreEqual(exp.Head, res.Head);
            Assert.AreEqual(exp.Tail.Head, res.Tail.Head);
            Assert.AreEqual(exp.Tail.Tail.Head, res.Tail.Tail.Head);
        }

    }
}