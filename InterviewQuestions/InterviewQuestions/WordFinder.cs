using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    public class WordFinder
    {

        public string[,] Wordist = new string[,]
        {
            {"A", "C", "P", "R", "C"},
            {"X", "S", "O", "P", "C"},
            {"V", "O", "V", "N", "I"},
            {"W", "G", "F", "M", "N"},
            {"Q", "A", "T", "I", "T"}
        };

        public class Position
        {
            public int Row;
            public int Col;
        }

        public Dictionary<char, List<Position>> CharacterMap = new Dictionary<char, List<Position>>();

        public List<Position> Result = new List<Position>();

        private string word;


        static void Main(string[] args)
        {

            WordFinder wf = new WordFinder("MICROSOFT");
            Console.WriteLine("End");
        }

        public WordFinder(string word)
        {
            /*Algorithm: This is basically a variable of depth first search that uses adjacency list.
             * 
             * 1. Scan the existing character array to build a character hashmap with key: letter (character) and value as a list of Positions where the character appeared in the array
             * 2. Start searching from the beginning letter.
             * 3. Find all instances of the letter
             * 4. For each instance of this letter, find all of its neighbors
             * 5. If a neighbor is equal to the next letter in the word being searched, recursively search starting with the next position of that word.
             * 6. Return true if all letters in the word are found (Word.Length - 1 = SearchPos)
             * Note: Any character being search, if available, must be in the character map that was build in step 1.
            */

            this.word = word;
            BuildCharacterMap();
            FindLetter(0, null);
            foreach (Position pos in Result)
            {
                Console.WriteLine("Letter: {0} - Position: {1}, {2}.", Wordist[pos.Row, pos.Col], pos.Row, pos.Col);
            }
        }
        public void BuildCharacterMap()
        {
            for (int i = 0; i < Wordist.GetLength(0); i++)
            {
                for (int j = 0; j < Wordist.GetLength(1); j++)
                {
                    if (CharacterMap.ContainsKey(Wordist[i, j][0]))
                    {
                        CharacterMap[Wordist[i, j][0]].Add(new Position()
                        {
                            Row = i,
                            Col = j
                        });
                    }
                    else
                    {
                        CharacterMap.Add(Wordist[i, j][0], new List<Position> {new Position()
                        {
                            Row = i,
                            Col = j
                        }});
                    }
                }
            }
        }

        public bool FindLetter(int wordPos, Position position)
        {
            char letter = word[wordPos];

            if (wordPos == word.Length - 1)
            {
                Result.Add(position);
                //Console.WriteLine("Letter: {0} - Position: {1}, {2}.", letter, position.Row, position.Col);
                return true;
            }

            if (word.Length > wordPos && CharacterMap.ContainsKey(letter))
            {
                List<Position> positions = CharacterMap[letter];

                foreach (Position pos in positions)
                {
                    //Console.WriteLine("Letter: {0} - Position: {1}, {2}.", letter, pos.Row, pos.Col);
                    Result.Add(pos);
                    List<Position> neighbors = GetNeighbors(pos);
                    bool foundWord = false;

                    foreach (Position neighbor in neighbors)
                    {
                        if (Wordist[neighbor.Row, neighbor.Col][0] == word[wordPos + 1])
                        {
                            foundWord = true;
                            return FindLetter(wordPos + 1, neighbor);
                        }
                    }

                    if (!foundWord)
                    {
                        Result.RemoveAt(Result.Count - 1);
                    }
                }
            }

            return false;
        }

        public List<Position> GetNeighbors(Position pos)
        {
            List<Position> neighbors = new List<Position>();
            int[] direction = { -1, 0, 1 };

            for (int i = 0; i < direction.Length; i++)
            {
                for (int j = 0; j < direction.Length; j++)
                {
                    if (direction[i] == direction[j] && direction[i] == 0)
                    {
                        continue;
                    }

                    if (pos.Row + direction[i] >= 0 && pos.Row + direction[i] < Wordist.GetLength(0) && pos.Col + direction[j] >= 0 && pos.Col + direction[j] < Wordist.GetLength(1))
                    {
                        neighbors.Add(new Position()
                        {
                            Row = pos.Row + direction[i],
                            Col = pos.Col + direction[j]
                        });
                    }
                }
            }

            return neighbors;
        }
    }
}
