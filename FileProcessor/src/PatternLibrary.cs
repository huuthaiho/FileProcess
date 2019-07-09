using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor
{
    public class PatternLibrary
    {
        //--------------------------------file validation pattern-------------------------------------
        public static string commaPattern = @".+? '|[^']+?(?=,)|(?<=,)[^']+";
        public static string hashPattern = @".+? '|[^']+?(?=#)|(?<=#)[^']+";
        public static string hyphenPattern = @".+? '|[^']+?(?=-)|(?<=,)[^']+";
    }
}
