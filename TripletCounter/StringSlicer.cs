using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripletCounter
{
    public class StringSlicer
    {
        public List<string> SliceString(string inputText)
        {
            string[] separatingStrings = { " ", ".", ",", "/", "\r", "\n", "\t", "!", "?", ":", ";", "`", "%", "*", "(", ")", "-", "#" };

            List<string> words = inputText.Split(separatingStrings,StringSplitOptions.RemoveEmptyEntries).ToList();

            return words;
        }
    }
}
