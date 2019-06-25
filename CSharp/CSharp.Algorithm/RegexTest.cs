using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharp.Algorithm
{
    public class RegexTest
    {
        [Theory]
        [InlineData("dfssxx12345", "[12345]", true)]
        [InlineData("dfssxx12345", "fd+", false)]
        //[InlineData("dfssxx1235", "[12345]", false)] //??
        ///匹配空格
        [InlineData("  ", "\\s+", true)] 
        [InlineData("aa b", "\\s+", true)]
        [InlineData("aab", "\\s+", false)]
        [InlineData("  ", "\\ +", true)] 
        [InlineData("aa b", "\\ +", true)]
        [InlineData("aab", "\\ +", false)] 
        //匹配字符
        [InlineData("aab", "\\w+", true)] 
        [InlineData("123", "\\w+", true)] //数字是字符
        [InlineData(" ", "\\w+", false)] //空格不是字符
        public void TestPattern(string input, string pattern, bool except)
        {
            Assert.Equal(except, Regex.IsMatch(input, pattern));
        }

        [Theory]
        [InlineData("dfssxx12345", "\\d+", "12345")]
        [InlineData("dfssxx123456", "\\d+", "123456")]
        [InlineData("dfssxx1235", "\\d+", "1235")]
        [InlineData("   -1235", "\\-+\\d+", "-1235")] //匹配数字
        [InlineData("   1235", "\\-?\\d+", "1235")]   //匹配数字
        [InlineData("   +1235", "[\\+-]?\\d+", "+1235")] //'+'是转义字符, 若要用, 做好前面加斜杠
        [InlineData("   -1235", "[\\+-]?\\d+", "-1235")]
        [InlineData("   +-1235", "[\\+-]?\\d+", "-1235")]
        [InlineData("   -+1235", "[\\+-]?\\d+", "+1235")]
        [InlineData("   +-1235", "[+-]?\\d+", "-1235")]
        [InlineData("   -+1235", "[+-]?\\d+", "+1235")]
        public void GetSubString(string input, string pattern, string except)
        {
            var match = Regex.Match(input, pattern);
            Assert.Equal(except, match.Value);
        }

        [Theory]
        [InlineData("aaaaa  12345", @"^\w*\s*(?<Num>[\d]*)", "12345")]
        [InlineData(" 12345", @"^\w*\s*(?<Num>[\d]*)", "12345")]
        [InlineData("12345", @"^\w*\s*(?<Num>[\d]*)", "")]//为什么/w不行? => 数字全被/w匹配过去了(/w放前面,故优先匹配)
        [InlineData("12345", @"^\w*(?<Num>[\d]*)", "")]   //
        [InlineData("12345", @"^\s*(?<Num>[\d]*)", "12345")]
        //[InlineData("12345", @"^\w*\s*([\d]*){0}", "12345")]
        //[InlineData("aaaaa  12345", @"^\w*\s*[\d]*", "aaaaa  12345", "12345")]
        public void GetMatchGroup(string input, string pattern, string except)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(input);
            Assert.Equal(match.Groups["Num"].Value, except);
            //RegexOptions.Compiled
        }

        [Theory]
        [InlineData("   12345", "12345", "12345")]
        [InlineData("   00012345", "12345", "12345")]
        [InlineData("   +00012345", "12345", "+12345")]
        [InlineData("   -00012345", "12345", "-12345")]
        public void FindNumber(string input, string exceptNum, string exceptFull)
        {
            //命名捕获组
            var pattern = @"^\s*(?<Sig>[\+-]?)0*(?<Num>\d+)"; // 0和\0的区别？
            var regex = new Regex(pattern);
            var match = regex.Match(input);
            Assert.Equal(match.Groups["Num"].Value, exceptNum);
            Assert.Equal(match.Groups["Sig"].Value + match.Groups["Num"].Value, exceptFull);
        }
    }
}

//Demo: ref - https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.getgroupnames?view=netframework-4.8
//using System;
//using System.Text.RegularExpressions;

//public class Example
//{
//    public static void Main()
//    {
//        string pattern = @"\b(?<FirstWord>\w+)\s?((\w+)\s)*(?<LastWord>\w+)?(?<Punctuation>\p{Po})";
//        string input = "The cow jumped over the moon.";
//        Regex rgx = new Regex(pattern);
//        Match match = rgx.Match(input);
//        if (match.Success)
//            ShowMatches(rgx, match);
//    }

//    private static void ShowMatches(Regex r, Match m)
//    {
//        string[] names = r.GetGroupNames();
//        Console.WriteLine("Named Groups:");
//        foreach (var name in names)
//        {
//            Group grp = m.Groups[name];
//            Console.WriteLine("   {0}: '{1}'", name, grp.Value);
//        }
//    }
//}
//// The example displays the following output:
////       Named Groups:
////          0: 'The cow jumped over the moon.'
////          1: 'the '
////          2: 'the'
////          FirstWord: 'The'
////          LastWord: 'moon'
////          Punctuation: '.'
///

/// 查找连续的重复字符串
//// Define a regular expression for repeated words.
//Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b",
//  RegexOptions.Compiled | RegexOptions.IgnoreCase);

//// Define a test string.        
//string text = "The the quick brown fox  fox jumps over the lazy dog dog.";

//// Find matches.
//MatchCollection matches = rx.Matches(text);

//// Report the number of matches found.
//Console.WriteLine("{0} matches found in:\n   {1}", 
//                          matches.Count, 
//                          text);

//        // Report on each match.
//        foreach (Match match in matches)
//        {
//            GroupCollection groups = match.Groups;
//Console.WriteLine("'{0}' repeated at positions {1} and {2}",  
//                              groups["word"].Value, 
//                              groups[0].Index, 
//                              groups[1].Index);
//        }

// The example produces the following output to the console:
//       3 matches found in:
//          The the quick brown fox  fox jumps over the lazy dog dog.
//       'The' repeated at positions 0 and 4
//       'fox' repeated at positions 20 and 25
//       'dog' repeated at positions 50 and 54