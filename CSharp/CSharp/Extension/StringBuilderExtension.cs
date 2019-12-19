using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text
{
    //Severity	Code	Description	Project	File	Line	Suppression State
    //Error CS1109  Extension methods must be defined in a top level static class; StringBuilderExtension is a nested class CSharp  H:\C#\CSharp\CSharp\CSharp\Extension\StringBuilderExtension.cs	11	Active

    //public class C0
    //{
    //Severity	Code	Description	Project	File	Line	Suppression State
    //Error CS1106  Extension method must be defined in a non-generic static class CSharp  H:\C#\CSharp\CSharp\CSharp\Extension\StringBuilderExtension.cs	12	Active
    //  public class StringBuilderExtension
    public static class StringBuilderExtension
    {
        public static int IndexOf(this StringBuilder sb, char c)
        {
            for (int i = 0; i < sb.Length; i++)
                if (sb[i] == c) return i;
            return -1;
        }

        //public static void Append(this StringBuilder sb, string text)
        //{
        //    sb.Append(text);
        //}

        public static void Hello()
        {
            Console.WriteLine("Hello");
        }
    }
    //}
}
