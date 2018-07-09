using System;
using System.Collections.Generic;
using System.Text;


namespace Cadbury.Inventor.Core.WordTree
{
    public static class Charset
    {
        /// <summary>
        /// Denotes whether this character set is case-sensitive or not. It is not by default.
        /// </summary>
        public static bool CaseSensitive { get; set; }


        /// <summary>
        /// List of characters in an array that will compose this character set.
        /// </summary>
        public static char[] AllowedChars;


        /// <summary>
        /// List of separators in the system.
        /// </summary>
        public static char[] SeparatorChars;


        /// <summary>
        /// List of wildcard characters
        /// </summary>
        public static char[] WildcardChars;


        /// <summary>
        /// A map of char -> string
        /// The string contains all the possible variations of the key character.
        /// </summary>
        public static Dictionary<char, string> FuzzyMap;


        /// <summary>
        /// Gets the size of allowed chars.
        /// </summary>
        public static int Length
        {
            get
            {
                return AllowedChars.Length;
            }
        }


        public static string Clean(string s)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (ContainsChar(s[i]))
                    builder.Append(s[i]);
            }
            return builder.ToString();
        }


        /// <summary>
        /// Checks whether the character set contains a particular character.
        /// </summary>
        /// <param name="c">Character to check within charset.</param>
        /// <returns>True if the character is present in the charset. False if otherwise.</returns>
        public static bool ContainsChar(char c)
        {
            for (int i = 0; i < AllowedChars.Length; i++)
                if (AllowedChars[i] == c)
                    return true;

            return false;
        }


        /// <summary>
        /// Checks whether the character specified is a separator or not.
        /// </summary>
        /// <param name="c">Character to check</param>
        /// <returns>True if specified character is a separator, false if not.</returns>
        public static bool IsCharSeparator(char c)
        {
            for (int i = 0; i < SeparatorChars.Length; i++)
                if (SeparatorChars[i] == c)
                    return true;

            return false;
        }


        /// <summary>
        /// Checks whether the character specified is a wildcard character or not.
        /// </summary>
        /// <param name="c">Character to check</param>
        /// <returns>True if specified character is a wildcard char, false if not.</returns>
        public static bool IsCharWildcard(char c)
        {
            for (int i = 0; i < WildcardChars.Length; i++)
                if (WildcardChars[i] == c)
                    return true;

            return false;
        }



        /// <summary>
        /// Get the list of characters (packaged in a string) that the specified char maps onto.
        /// </summary>
        /// <param name="c">Character whose map has to be returned</param>
        /// <returns>A string containing the characters upon which the specified char maps onto</returns>
        public static string Map(char c)
        {
            string outValue = "";
            if (FuzzyMap.TryGetValue(c, out outValue))
                return outValue;
            else
                return "";
        }


        /// <summary>
        /// Returns the character represented by the code.
        /// </summary>
        /// <param name="code">0..255 value representing a particular character in this Charset.</param>
        /// <returns>The character value represented by the code passed in params.</returns>
        public static char GetChar(byte code)
        {
            if (AllowedChars.Length > 0)
            {
                if (AllowedChars.Length <= code)
                {
                    return AllowedChars[code];
                }
                else
                {
                    throw new ArgumentException("The charset does not have a character with the specified code.");
                }
            }
            else
            {
                Exception exception = new Exception("Charset.AllowedChars has not been set. Call the Initialize(...) method before using Charset's methods.");
                throw new TypeInitializationException("Charset.AllowedChars", exception);
            }
        }


        /// <summary>
        /// Returns the code represented by the character.
        /// </summary>
        /// <param name="character">Character value whose equivalent code is required.</param>
        /// <returns>The code representing the charater passed in params.</returns>
        public static byte GetCode(char character)
        {
            if (AllowedChars.Length > 0)
            {
                for (int i = 0; i < AllowedChars.Length; i++)
                {
                    if (AllowedChars[i] == character)
                    {
                        return (byte)i;
                    }
                }

                throw new ArgumentException("No code is set for this character. This character does not exist in the charset.");
            }
            else
            {
                Exception exception = new Exception("Charset.AllowedChars has not been set. Call the Initialize(...) method before using Charset's methods.");
                throw new TypeInitializationException("Charset.AllowedChars", exception);
            }
        }


        /// <summary>
        /// Initialize the charset with the list of allowed characters.
        /// </summary>
        /// <param name="allowedChars">List of allowed characters</param>
        /// <param name="separatorChars">List of separator characters</param>
        /// <param name="wildcard">Wildcard chars, such as * or _</param>
        /// <param name="caseSensitive">Whether the charset is case sensitive or not. By default it is not.</param>
        public static void Initialize(char[] allowedChars, char[] separatorChars, char[] wildcard, bool caseSensitive = false)
        {
            AllowedChars = allowedChars;
            SeparatorChars = separatorChars;
            WildcardChars = wildcard;
            CaseSensitive = caseSensitive;
        }


        /// <summary>
        /// Initialize the charset with a string. Charset will be comprised of all unique characters found in the string.
        /// </summary>
        /// <param name="lines">Text to scan to build charset upon. Formatted as a string.</param>
        /// <param name="separatorChars">List of separator characters</param>
        /// <param name="caseSensitive">Whether the charset is case sensitive or not. By default it is not.</param>
        public static void Initialize(string allowedChars, string separatorChars, string wildcard, bool caseSensitive = false)
        {
            AllowedChars = Utils.GetUniqueCharsInString(allowedChars, caseSensitive).ToCharArray();
            SeparatorChars = Utils.GetUniqueCharsInString(separatorChars, caseSensitive).ToCharArray();
            WildcardChars = Utils.GetUniqueCharsInString(wildcard, caseSensitive).ToCharArray();
            CaseSensitive = caseSensitive;
        }


        /// <summary>
        /// Initialize the charset with a text. Charset will be built upon characters found in the text.
        /// </summary>
        /// <param name="lines">Text to scan to build charset upon. Formatted into a string array.</param>
        /// <param name="caseSensitive">Whether the charset is case sensitive or not. By default it is not.</param>
        public static void IntializeLines(string[] lines, bool caseSensitive = false)
        {
            AllowedChars = Utils.GetUniqueCharsInLines(lines, caseSensitive).ToCharArray();
            CaseSensitive = caseSensitive;
        }


        /// <summary>
        /// Extends the current charset with new characters (if any) from a text formatted into a string array
        /// </summary>
        /// <param name="lines">Text to scan to retrieve new characters from</param>
        /// <param name="caseSensitive">Whether the characters need to be added to the charset in case-sensitive mode</param>
        public static void ExtendCharset(string[] lines, bool caseSensitive = false)
        {
            string newChars = Utils.GetUniqueCharsInLines(lines, caseSensitive);
            StringBuilder newAllowedChars = new StringBuilder(new string(AllowedChars));
            foreach (char c in newChars)
            {
                if (!Utils.StringBuilderContains(newAllowedChars, c))
                {
                    newAllowedChars.Append(c);
                }
            }
            AllowedChars = newAllowedChars.ToString().ToCharArray();
        }


        /// <summary>
        /// Extends the current fuzzy map with characters contained
        /// </summary>
        /// <param name="fuzzyMapToAppend">A map of character -> array of variations to append to the current one</param>
        public static void ExtendFuzzyMap(Dictionary<char, string> fuzzyMapToAppend)
        {
            if (FuzzyMap == null)
                FuzzyMap = new Dictionary<char, string>();

            foreach (KeyValuePair<char, string> mapKey in fuzzyMapToAppend)
            {
                AddMap(mapKey.Key, mapKey.Value);
            }
        }


        /// <summary>
        /// Add a map for a single character into the fuzzy map
        /// </summary>
        /// <param name="charKey">Character which is being mapped, e.g. i</param>
        /// <param name="s">List of characters to which the charKey is mapped onto, e.g. 1, i, | etc</param>
        private static void AddMap(char charKey, string s)
        {
            string outValue = "";
            if (!FuzzyMap.TryGetValue(charKey, out outValue))
            {
                outValue = "";
                FuzzyMap.Add(charKey, outValue);
            }

            StringBuilder output = new StringBuilder(outValue);
            foreach (char character in s)
            {
                if (!outValue.Contains(character.ToString()))
                {
                    output.Append(character);
                }
                AddSingleMap(character, charKey);
            }

            FuzzyMap[charKey] = output.ToString();
        }


        /// <summary>
        /// Add a new character for a single key, e.g. extend the map of i to include !
        /// </summary>
        /// <param name="charKey">Key whose map will be extended</param>
        /// <param name="charMap">New character which represents the new mapped character</param>
        private static void AddSingleMap(char charKey, char charMap)
        {
            string outValue = "";
            if (!FuzzyMap.TryGetValue(charKey, out outValue))
            {
                outValue = "";
                FuzzyMap.Add(charKey, charMap.ToString());
            }
            else
            {
                if (!outValue.Contains(charKey.ToString()))
                {
                    FuzzyMap[charKey] = outValue + charKey;
                }
            }
        }


    }
}
