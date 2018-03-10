using System;
using heitech.LinqXt.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Example
{
    class Program
    {
        static Dictionary<string, Action> dictionary;

        static Program()
        {
            dictionary = new Dictionary<string, Action>()
            {
                ["convert"] = Convert,
                ["slice_start"] = AllStartSlices,
                ["slice_end"] = AllEndSlices,
                ["slice_step"] = AllStepSlices,
                ["extend"] = Extend,
                ["combine"] = Combine,
                ["flatten"] = Flatten
            };
        }

        static string[] array = new string[] { "1", "2", "3", "4", "5" };
        static string[] sndArray = new string[] { "a", "b", "c" };
        static string help()
        {
            string result =
            "put in one of following functions to get a display:" + Environment.NewLine;
            foreach (var item in dictionary.Keys)
                result += item + Environment.NewLine;
            return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("simple demonstration of LinqExt:" + Environment.NewLine);
            Console.WriteLine(help());

            Console.WriteLine("first array: " + "[1, 2, 3, 4, 5]");
            var sndArray = new string[] { "a", "b", "c" };
            Console.WriteLine("snd array: " + "[a,b,c]");
            Console.WriteLine(Environment.NewLine);

            string input = "";
            while (input != "exit")
            {
                input = Console.ReadLine();
                if (dictionary.TryGetValue(input, out Action action) && input != "exit")
                    action();
                else
                    Console.WriteLine(help());
            }
        }

        private static void Flatten()
            => Console.WriteLine("to be completed");

        private static void Combine()
        {
            WaitOne("Combine<TSource, TSndSource> two collections to one of tuples, with default values for the smaller one");
            ShowResult(array.CombineToOne(sndArray, fallbackSnd: "defaulted"));
        }

        private static void Extend()
        {
            IEnumerable<string> experiment = array.Extend(sndArray);
            WaitOne("Simple Extend<TSource>");
            ShowResult(experiment, $"Extended: {array.Length} and {sndArray.Length}");

            WaitOne("Extend<TSource> with predicate");
            ShowResult(array.Extend(x => x == "a", sndArray), "extendend wit Predicate only a should be included");
        }

        private static void Convert()
        {
            WaitOne("Convert<TSource, TConvert> from string to int");
            Console.WriteLine("converted string to int and multiplied by 10");
            foreach (int item in array.Convert(x => int.Parse(x) * 10))
                Console.WriteLine(item);
            Console.WriteLine(Environment.NewLine);
        }

        private static void AllStartSlices() 
            => Slicer(i => ShowSlice(array.Slice(i), i), "start");

        private static void AllEndSlices()
            => Slicer(i => ShowSlice(array.Slice(null, i), i), "end");

        private static void Slicer(Action<int> action, string argName)
        {
            WaitOne("Slice<TSource>(int? start, int? end, int? step");
            WaitOne($"slices from 0 to 6 for {argName} argument --> Slice(n)");
            foreach (int index in Enumerable.Range(0, 6))
                action(index);

            WaitOne("slcies from -1 to -6 for start argument --> Slice(-n)");
            foreach (var item in Enumerable.Range(1, 6))
                action(-item);
        }

        private static void AllStepSlices()
        {
            WaitOne($"step for slices from 1 to 3");
            foreach (var item in Enumerable.Range(1, 4))
            {
                Console.Write($"positive {item}: ");
                ShowSlice(array.Slice(null, null, item), item);
                Console.Write($"negative {item}: ");
                ShowSlice(array.Slice(null, null, -item), -item);
            }
        }

        private static void WaitOne(string display = null)
        {
            Console.WriteLine(display);
            Console.ReadKey();
        }

        private static void ShowResult(IEnumerable<string> _array, string description)
        {
            Console.WriteLine(description);
            foreach (var item in _array)
                Console.WriteLine(item);

            Console.WriteLine(Environment.NewLine);
        }

        private static void ShowResult(IEnumerable<(string s, string t)> tuples)
        {
            Console.WriteLine("Combined");
            foreach (var (s,t) in tuples)
                Console.WriteLine(s + "," + t);
            Console.WriteLine(Environment.NewLine);
        }

        private static void ShowSlice(IEnumerable<string> slice, int sliceValue)
        {
            
            string result = "[";
            foreach (var item in slice)
                result += item + ",";

            result += "]";
            Console.WriteLine($"{sliceValue} : {result}");
        }
    }
}
