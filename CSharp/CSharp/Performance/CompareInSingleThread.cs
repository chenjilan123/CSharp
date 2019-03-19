﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSharp.Performance
{
    public class CompareInSingleThread
    {
        const string Dictionary = "Dictionary";
        const string ConcurrentDictionary = "ConcurrentDictionary";

        const int Iter = 1000000;
        const string Item = "Item";
        static string CurrentItem;

        public void Compare()
        {
            Console.WriteLine("Write in single thread");

            var dic = new Dictionary<int, string>();
            var cdic = new ConcurrentDictionary<int, string>();

            Work(Dictionary, i =>
            {
                lock (dic)
                {
                    dic[i] = Item;
                }
            });

            Work(ConcurrentDictionary, i =>
            {
                cdic[i] = Item;
            });

            Console.WriteLine("Read in single thread");
            Work(Dictionary, i =>
            {
                lock (dic)
                {
                    CurrentItem = dic[i];
                }
            });

            Work(ConcurrentDictionary, i =>
            {
                CurrentItem = cdic[i];
            });
            //var sw = new Stopwatch();

            //Console.WriteLine("  Dictionary...");
            //sw.Start();
            //for (int i = 0; i < Iter; i++)
            //{
            //    lock(dic)
            //    {
            //        dic[i] = Item;
            //    }
            //}
            //sw.Stop();
            //Console.WriteLine($"  Elapsed: {sw.Elapsed}");

            //Console.WriteLine("  ConcurrentDictionary...");
            //sw.Restart();
            //for (int i = 0; i < Iter; i++)
            //{
            //    cdic[i] = Item;
            //}
            //sw.Stop();
            //Console.WriteLine($"  Elapsed: {sw.Elapsed}");
        }

        private void Work(string actionName, Action<int> action)
        {
            var sw = new Stopwatch();
            Console.WriteLine($"  {actionName}...");
            sw.Start();
            for (int i = 0; i < Iter; i++)
            {
                action(i);
            }
            sw.Stop();
            Console.WriteLine($"  Elapsed: {sw.Elapsed}");
        }
    }
}
