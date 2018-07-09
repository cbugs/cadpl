using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadbury.Inventor.Core.WordTree
{
    public class WordTree
    {
        /// <summary>
        /// Topmost letter in wordtree
        /// </summary>
        public Letter Root { get; set; }


        /// <summary>
        /// Wordtree constructor. Initialize with a charset and list of good words and bad ones.
        /// </summary>
        /// <param name="charSet">String representing allowed characters</param>
        /// <param name="separators">List of separators</param>
        /// <param name="wildcard">List of wildcard characters</param>
        /// <param name="goodWords">String array of good words.</param>
        /// <param name="badWords">String array of bad wrods.</param>
        public WordTree(string charSet, string separators, string wildcard, string[] goodWords, string[] badWords)
        {
            Charset.Initialize(charSet, separators, wildcard, false);
            //Charset.IntializeLines(goodWords);
            //Charset.ExtendCharset(badWords);

            Root = new Letter(' ', null);
            foreach (string word in goodWords)
            {
                AddWord(word);
            }

            foreach (string profanity in badWords)
            {
                AddBadWord(profanity);
            }
        }


        /// <summary>
        /// Wordtree constructor. Initialize with list of good words and bad words.
        /// </summary>
        /// <param name="goodWords">String array of good words.</param>
        /// <param name="badWords">String array of bad wrods.</param>
        public WordTree(string[] goodWords, string[] badWords)
        {
            Charset.IntializeLines(goodWords);
            Charset.ExtendCharset(badWords);

            Root = new Letter(' ', null);
            foreach(string word in goodWords)
            {
                AddWord(word);
            }

            foreach(string profanity in badWords)
            {
                AddBadWord(profanity);
            }
        }


        /// <summary>
        /// Add a word to the list of good words.
        /// </summary>
        /// <param name="word">Word to add</param>
        public void AddWord(string word)
        {
            word = word.ToLower();
            Letter lastLetter = Root.AddWord(word);
            lastLetter.IsWord = true;
        }


        /// <summary>
        /// Add a word to the list of bad words.
        /// </summary>
        /// <param name="word">Bad word to add</param>
        public void AddBadWord(string word)
        {
            word = word.ToLower();
            Letter lastLetter = Root.AddWord(word);
            lastLetter.IsBadWord = true;
        }


        /// <summary>
        /// Look up an the exact match of a word
        /// </summary>
        /// <param name="word">Word to match exactly</param>
        /// <returns>The (last) letter that represents that word</returns>
        public Letter FindExact(string word)
        {
            return Root.GetWord(word);
        }


        /// <summary>
        /// Look up a specified word and return a SearchResultType object
        /// </summary>
        /// <param name="word">Word to search</param>
        /// <returns>The type of result emanating from that search</returns>
        public SearchResultType Find(string word)
        {
            bool partIsBadWord = false;

            Letter letterFound = Root.GetWordFuzzyQuickLetter(word, ref partIsBadWord);
            if (letterFound == null)
            {
                if (partIsBadWord)
                    return SearchResultType.PartIsBadWord;
                else
                    return SearchResultType.None;
            }
            else if (letterFound.IsBadWord)
            {
                return SearchResultType.Match;
            }
            else
            {
                if (partIsBadWord)
                    return SearchResultType.PartIsBadWord;
                else
                    return SearchResultType.PartialMatch;
            }
        }


        /// <summary>
        /// Looks up a word while considering the character map in Charset
        /// </summary>
        /// <param name="word">Word to match using character map</param>
        /// <returns>The (last) letters of each words that represent that word being searched</returns>
        public List<Letter> FindFuzzy(string word)
        {
            return Root.GetWordFuzzy(word);
        }


        /// <summary>
        /// Faster implementation of FindFuzzy - stops when the first match is found.
        /// </summary>
        /// <param name="word">Word to search</param>
        /// <returns>True if a match is found, false if otherwise</returns>
        public bool FindFuzzyQuick(string word)
        {
            return Root.GetWordFuzzyQuick(word);
        }


        /// <summary>
        /// Rebuilds a word given a letter
        /// </summary>
        /// <param name="letter">Letter from which word is reconstructed.</param>
        /// <returns>Word that ends with specified letter.</returns>
        public string RetrieveWord(Letter letter)
        {
            StringBuilder outputString = new StringBuilder();
            while(letter.Parent != null)
            {
                outputString.Insert(0, letter.Character);
                letter = letter.Parent;
            }
            return outputString.ToString();
        }

    }
}
