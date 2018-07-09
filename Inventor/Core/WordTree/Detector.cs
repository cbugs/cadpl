using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadbury.Inventor.Core.WordTree
{
    public class Detector
    {
        public WordTree Tree;
        private string lastWord = "";


        public Detector(WordTree tree)
        {
            Tree = tree;
        }


        public bool IsValid(string text)
        {
            text = text.ToLower();
            string[] values = text.Split(Charset.SeparatorChars, StringSplitOptions.RemoveEmptyEntries);

            if (Tree.Find(text) == SearchResultType.Match)
                return false;
            else
            {
                foreach (string word in values)
                {
                    SearchResultType searchResult = Tree.Find(word);
                    if (searchResult == SearchResultType.Match)
                    {
                        lastWord = "";
                        return false;
                    }

                    if (lastWord != "")
                    {
                        SearchResultType searchCombined = Tree.Find(lastWord + word);
                        if (searchCombined == SearchResultType.Match)
                        {
                            return false;
                        }
                        else if (searchCombined == SearchResultType.PartialMatch)
                        {
                            lastWord += word;
                            continue;
                        }
                        else if (searchCombined == SearchResultType.None)
                        {
                            if (searchResult == SearchResultType.PartialMatch)
                            {
                                lastWord = word;
                            }

                            continue;
                        }
                    }
                    else
                    {
                        if (searchResult == SearchResultType.Match)
                        {
                            lastWord = "";
                            return false;
                        }
                        else if (searchResult == SearchResultType.PartialMatch)
                        {
                            lastWord = lastWord + " " + word;
                            SearchResultType deepSearch = Tree.Find(lastWord);
                            if (deepSearch == SearchResultType.Match)
                                return false;
                            else if (deepSearch == SearchResultType.None)
                                lastWord = "";
                        }
                        else if (searchResult == SearchResultType.None)
                        {
                            lastWord = "";
                            continue;
                        }
                    }

                }
                return true;
            }
        }
    }
}
