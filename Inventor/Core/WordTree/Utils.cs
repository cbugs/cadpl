using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadbury.Inventor.Core.WordTree
{
    public static class Utils
    {
        /// <summary>
        /// Checks if a stringbuilder instance contains a char
        /// </summary>
        /// <param name="s">Stringbuilder instance to check</param>
        /// <param name="c">Character whose existence is checked within stringbuilder instance</param>
        /// <returns>True if the stringbuilder contains a character, false otherwise</returns>
        public static bool StringBuilderContains(StringBuilder s, char c)
        {
            for (int i = 0; i < s.Length; i++)
                if (s[i] == c)
                    return true;

            return false;
        }


        /// <summary>
        /// Gets a list of unique characters figuring in a string array
        /// </summary>
        /// <param name="lines">String array representing the text to scan</param>
        /// <param name="caseSensitive">Whether the characters need to be added to the charset in case-sensitive mode</param>
        /// <returns>A list of unique characters in the text provided, formatted into a string</returns>
        public static string GetUniqueCharsInLines(string[] lines, bool caseSensitive)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //i is the line j is the character
            for (int i = 0; i < lines.Length; i++)
            {
                //convert to lowercase first
                if (!caseSensitive)
                    lines[i] = lines[i].ToLower();

                //find if char exists in stringbuilder
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char c = lines[i][j];
                    if (!StringBuilderContains(stringBuilder, c))
                    {
                        stringBuilder.Append(c);
                    }
                }
            }

            return stringBuilder.ToString();
        }


        /// <summary>
        /// Gets a list of unique characters figuring in a string array
        /// </summary>
        /// <param name="text">String representing the text to scan</param>
        /// <param name="caseSensitive">Whether the characters need to be added to the charset in case-sensitive mode</param>
        /// <returns>A list of unique characters in the text provided, formatted into a string</returns>
        public static string GetUniqueCharsInString(string text, bool caseSensitive)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //convert to lowercase first
            if (!caseSensitive)
                text = text.ToLower();

            //find if char exists in stringbuilder
            for (int j = 0; j < text.Length; j++)
            {
                char c = text[j];
                if (!StringBuilderContains(stringBuilder, c))
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }


        /// <summary>
        /// Removes sequences of characters from the string provided.
        /// E.g. "assasin" -> "asasin"
        /// </summary>
        /// <param name="s">String to scan and remove the sequential characters from</param>
        /// <returns>A string without consecutive similar characters.</returns>
        public static string RemoveSequentialChars(string s)
        {
            if (s.Length <= 1)
                return s;

            StringBuilder output = new StringBuilder();
            int outputCount = 0;
            output.Append(s[0]);

            for(int i=1;i<s.Length;i++)
            {
                if (s[i] != output[outputCount])
                {
                    output.Append(s[i]);
                    outputCount++;
                }
            }

            return output.ToString();
        }
    }
}
