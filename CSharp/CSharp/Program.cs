﻿using CSharp.Dictionarys;
using System;
using System.Collections.Generic;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //int j = 5;
            //int k = 6;
            //int count = 0;
            //for (int i = 0; i < k + 1; i++)
            //{
            //    count++;
            //}
            //Console.WriteLine(count);

            RemoveSomeOfDictionary();
        }

        #region RemoveSomeOfDictionary
        private static void RemoveSomeOfDictionary()
        {
            new RemoveSome()
                .Remove();

        }
        #endregion

        #region Jet
        private static void Jet()
        {
            var jetArr = new JetArray()
            {
                new Jet() { Code = "NF5" },
                new Jet() { Code = "NH583"},
                new Jet() { Code = "FL440"},
                new Jet() { Code = "FL441"},
                new JetArray()
                {
                    new Jet() { Code = "FY4" },
                },
            };
            foreach (var jet in jetArr)
            {
                Console.WriteLine(jet.Code);
            }
        }
        #endregion

        #region EnumOrder
        private static void EnumOrder()
        {
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            collection.Add(4);
            foreach (var item in collection)
            {
                //按添加顺序
                Console.WriteLine(item);
            }
            return;
        }
        #endregion
    }
}
