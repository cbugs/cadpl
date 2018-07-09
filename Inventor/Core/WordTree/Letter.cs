using System.Collections.Generic;

namespace Cadbury.Inventor.Core.WordTree
{
    /// <summary>
    /// Represents a node in the WordTree. May connect to an array of 1..charset_size other nodes.
    /// IsWord = true signifies that the current node is a word.
    /// </summary>
    public class Letter
    {
        /// <summary>
        /// Character this Letter node represents
        /// </summary>
        public byte CharacterByte { get; set; }


        /// <summary>
        /// Set the character
        /// </summary>
        public char Character
        {
            get
            {
                return Charset.AllowedChars[(int)CharacterByte];
            }
            set
            {
                int index = Charset.GetCode(value);
                if (index > -1)
                    CharacterByte = (byte)index;
                else
                    throw new System.ArgumentException("Sorry but the specified character: " + value + " is not within the charset.");
            }
        }


        /// <summary>
        /// If IsWord=true this node represents a complete word
        /// </summary>
        public bool IsWord { get; set; }


        /// <summary>
        /// Set IsBadWord to true if this word is a disallowed word
        /// </summary>
        public bool IsBadWord { get; set; }


        /// <summary>
        /// The parent node above
        /// </summary>
        public Letter Parent { get; set; }


        /// <summary>
        /// Array representing child nodes under this letter
        /// </summary>
        public Letter[] Children { get; set; }


        /// <summary>
        /// Constructor for a non-separator letter
        /// </summary>
        /// <param name="character">The letter this node represents</param>
        /// <param name="isWord">True if this letter is a word</param>
        /// <param name="parent">Parent node</param>
        public Letter(char character, Letter parent)
        {
            Character = character;
            Parent = parent;
            Children = new Letter[Charset.Length];             
        }


        /// <summary>
        /// Recursive method. Adds a word from this Letter down to its children.
        /// </summary>
        /// <param name="word">Word to add - new children Letters will be created to make this word available.</param>
        public Letter AddWord(string word)
        {
            return AddWord(word, -1);
        }


        /// <summary>
        /// Recursive method. Adds a word from this letter down to its children.
        /// </summary>
        /// <param name="word">The word to add.</param>
        /// <param name="count">Which letter we have reached to in the recursion.</param>
        /// <returns>The last letter added representing the word that was added.</returns>
        private Letter AddWord(string word, int count)
        {
            count++;
            if (count < word.Length)
            {
                int childIndex = Charset.GetCode(word[count]);
                if (childIndex > -1)
                {
                    Letter nextLetter = this.Children[childIndex];
                    if (nextLetter == null)
                    {
                        nextLetter = new Letter(Charset.AllowedChars[childIndex], this);
                        this.Children[childIndex] = nextLetter;
                        return nextLetter.AddWord(word, count);
                    }
                    else
                    {
                        return this.Children[childIndex].AddWord(word, count);
                    }
                }
                else
                {
                    throw new System.ArgumentException("Sorry but the word you are trying to add contains disallowed characters.");
                }
            }
            else
            {
                return this;
            }
        }


        /// <summary>
        /// Recursive method. Retrieves a word from the WordTree
        /// </summary>
        /// <param name="word">Word to retrieve</param>
        /// <returns>A letter object</returns>
        public Letter GetWord(string word)
        {
            return GetWord(word, -1);
        }


        /// <summary>
        /// Recursive method. Retrieves a word from the WordTree
        /// </summary>
        /// <param name="word">Word to retrieve</param>
        /// <returns>A letter object</returns>
        public Letter GetWord(string word, int count)
        {
            count++;
            if (count < word.Length)
            {
                byte code = Charset.GetCode(word[count]);
                if (Children[code] == null)
                    return null;
                else
                    return Children[code].GetWord(word, count);
            }
            else
            {
                return this;
            }
        }


        /// <summary>
        /// Recursive method. Retrives a word from the WordTree while considering its possible
        /// variations set within the FuzzyMap
        /// </summary>
        /// <param name="word">Word to look for.</param>
        /// <returns>Contains a list of letters found that match the word being searched.</returns>
        public List<Letter> GetWordFuzzy(string word)
        {
            List<Letter> letterList = new List<Letter>();
            GetWordFuzzy(word, -1, ref letterList);
            return letterList;
        }


        /// <summary>
        /// Returns the last letter of the first instance found.
        /// </summary>
        /// <param name="word">Word to search</param>
        /// <returns>Returns the last letter of the first instance found.</returns>
        public Letter GetWordFuzzyQuickLetter(string word, ref bool partIsBadWord)
        {
            Letter nullLetter = null;
            partIsBadWord = false;
            GetWordFuzzy(word, -1, ref nullLetter, ref partIsBadWord);
            return nullLetter;
        }
        

        /// <summary>
        /// Same as GetWordFuzzy(string), except that it stops at the first result found.
        /// </summary>
        /// <param name="word">Word to scan.</param>
        /// <returns>True if a match is found, false if no matches are found.</returns>
        public bool GetWordFuzzyQuick(string word)
        {
            bool found = false;
            GetWordFuzzy(word, -1, ref found);
            return found;
        }

        


        /// <summary>
        /// Recursive method. Retrives a word from the WordTree while considering its possible
        /// variations set within the FuzzyMap.
        /// </summary>
        /// <param name="word">Word to look for.</param>
        /// <param name="count">Depth of recursion.</param>
        /// <param name="letterList">Contains list of letters found that match the word being searched.</param>
        private void GetWordFuzzy(string word, int count, ref List<Letter> letterList)
        {
            count++;
            if (count < word.Length)
            {
                //wildcard work
                if (Charset.IsCharWildcard(word[count]))
                {
                    for (int i=0; i<Children.Length; i++)
                    {
                        if (Children[i] != null)
                            Children[i].GetWordFuzzy(word, count, ref letterList);
                    }
                }

                //branch into a new recursion with the next char if the current char is a separator
                if (Charset.IsCharSeparator(word[count]))
                {
                    GetWordFuzzy(word, count, ref letterList);
                }


                //branch off recursion with the current char if the current char is the same as the next one
                if (count + 1 < word.Length)
                    if (word[count + 1] == word[count])
                        GetWordFuzzy(word, count, ref letterList);


                //variation work
                string possibleVariations = word[count] + Charset.Map(word[count]);
                for (int i=0;i<possibleVariations.Length;i++)
                {
                    byte code = Charset.GetCode(possibleVariations[i]);
                    if (Children[code] != null)
                    {
                        Children[code].GetWordFuzzy(word, count, ref letterList);
                    }
                }
            }
            else
            {
                if (this.IsBadWord)
                    letterList.Add(this);
            }
        }


        /// <summary>
        /// Recursive method. Retrives a word from the WordTree while considering its possible
        /// variations set within the FuzzyMap.
        /// </summary>
        /// <param name="word">Word to look for.</param>
        /// <param name="count">Depth of recursion.</param>
        /// <param name="found">Value by ref indicating whether a result has been found. When set to true exits all branches of the recursion</param>
        private bool GetWordFuzzy(string word, int count, ref bool found)
        {
            if (found)
                return found;

            count++;
            if (count < word.Length)
            {
                //variation work
                string possibleVariations = word[count] + Charset.Map(word[count]);
                for (int i = 0; i < possibleVariations.Length; i++)
                {
                    byte code = Charset.GetCode(possibleVariations[i]);
                    if (Children[code] != null)
                    {
                        Children[code].GetWordFuzzy(word, count, ref found);
                    }
                }

                //wildcard work
                if (Charset.IsCharWildcard(word[count]))
                {
                    for (int i = 0; i < Children.Length; i++)
                    {
                        if (Children[i] != null)
                            Children[i].GetWordFuzzy(word, count, ref found);
                    }
                }

                //branch into a new recursion with the next char if the current char is a separator
                if (Charset.IsCharSeparator(word[count]))
                {
                    GetWordFuzzy(word, count, ref found);
                }


                //branch off recursion with the current char if the current char is the same as the next one
                if (count + 1 < word.Length)
                    if (word[count + 1] == word[count])
                        GetWordFuzzy(word, count, ref found);
            }
            else
            {
                if (this.IsBadWord)
                {
                    found = true;
                }
            }

            return found;
        }


        /// <summary>
        /// Recursive method. Retrives a word from the WordTree while considering its possible
        /// variations set within the FuzzyMap.
        /// </summary>
        /// <param name="word">Word to look for.</param>
        /// <param name="count">Depth of recursion.</param>
        /// <param name="found">Letter found. When null exits all branches of the recursion</param>
        private Letter GetWordFuzzy(string word, int count, ref Letter found, ref bool partIsBadWord)
        {
            if (found != null)
                return found;

            count++;
            if (count < word.Length)
            {
                if (this.IsBadWord)
                    partIsBadWord = true;

                //variation work
                string possibleVariations = word[count] + Charset.Map(word[count]);
                for (int i = 0; i < possibleVariations.Length; i++)
                {
                    byte code = Charset.GetCode(possibleVariations[i]);
                    if (Children[code] != null)
                    {
                        Children[code].GetWordFuzzy(word, count, ref found, ref partIsBadWord);
                    }
                }

                //wildcard work
                if (Charset.IsCharWildcard(word[count]))
                {
                    for (int i = 0; i < Children.Length; i++)
                    {
                        if (Children[i] != null)
                            Children[i].GetWordFuzzy(word, count, ref found, ref partIsBadWord);
                    }
                }

                //branch into a new recursion with the next char if the current char is a separator
                if (Charset.IsCharSeparator(word[count]))
                {
                    GetWordFuzzy(word, count, ref found, ref partIsBadWord);
                }


                //branch off recursion with the current char if the current char is the same as the next one
                if (count + 1 < word.Length)
                    if (word[count + 1] == word[count])
                        GetWordFuzzy(word, count, ref found, ref partIsBadWord);
            }
            else
            {
                found = this;
            }

            return found;
        }


    }
}
