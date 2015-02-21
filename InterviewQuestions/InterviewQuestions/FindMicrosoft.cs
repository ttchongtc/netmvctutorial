using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    public class FindMicrosoft
    {
        /*
         In a matrix of characters, find an string. String can be in any way (all 8 neighbors to be considered), like find Microsoft in below matrix. 
            A-C-P-R-C 

            X-S-O-P-C 

            V-O-V-N-I 

            W-G-F-M-N 

            Q-A-T-I-T 

            String Microsoft is present in the matrix above ? 

            There also a slight variation where a diagonal neighbor is not considered.
         */
        public string[,] Wordist = new string[,]
        {
            {"A", "C", "P", "R", "C"},
            {"X", "S", "O", "P", "C"},
            {"V", "O", "V", "N", "I"},
            {"W", "G", "F", "M", "N"},
            {"Q", "A", "T", "I", "T"}
        };

        public int[,] AdjacencyMatrix;

        public Node[] Vertices;

        private List<string> positions = new List<string>();

        public enum State
        {
            Black,
            Gray,
            White
        }

        private string word;

        public class Node
        {
            public State State;
            public string Value;
            public int index;
        }

        public FindMicrosoft(string word)
        {
             ConstructVertices();
            ConstructAdjMatrix();
            this.word = word;
            FindWord();

            foreach (string x in positions)
            {
                Console.WriteLine(x);
            }
        }

        public void FindWord()
        {
            DFS(Vertices[0], 0);
        }

        public bool IsEdge(Node a, Node b)
        {
            return Convert.ToBoolean(AdjacencyMatrix[a.index, b.index]);
        }

        public void DFS(Node node, int pos)
        {
            if (node == null)
            {
                return;
            }

            if (node.Value.Equals(word[pos].ToString()))
            {
                positions.Add(node.index.ToString());
                pos = pos + 1;
            }
            else
            {
                if (positions.Count > 1)
                {
                    positions.RemoveAt(positions.Count - 1);
                    pos = pos - 1;
                }
            }

            node.State = State.Gray;
            foreach (Node n in Vertices)
            {
                if (IsEdge(node, n) && n.State == State.White)
                {
                    DFS(n, pos);
                }

                node.State = State.Black;
            }


        }

        public void ConstructVertices()
        {
            int verticesCount = Wordist.GetLength(0) * Wordist.GetLength(1);
            Vertices = new Node[verticesCount];
            for (int i = 0; i < Wordist.GetLength(0); i++)
            {
                for (int j = 0; j < Wordist.GetLength(1); j++)
                {
                    Vertices[i * Wordist.GetLength(1) + j] = new Node
                    {
                        State = State.White,
                        Value = Wordist[i, j],
                        index = GetPos(Wordist[i, j])
                    };
                }
            }
        }

        public void ConstructAdjMatrix()
        {
            AdjacencyMatrix = new int[26, 26];

            for (int i = 0; i < Wordist.GetLength(0); i++)
            {
                for (int j = 0; j < Wordist.GetLength(1); j++)
                {
                    FillMatrix(i, j);
                }
            }
        }

        private void FillMatrix(int i, int j)
        {
            int[] values = { -1, 0, 1 };
            int pos = GetPos(Wordist[i, j]);

            for(int x = 0; x < values.Length; x++)
            {
                for(int y = 0; y < values.Length; y++)
                {
                    if (values[x] == values[y] && values[x] == 0)
                    {
                        continue;
                    }

                    if (i + values[x] >= 0 && i + values[x] < Wordist.GetLength(0) && values[y] + j >= 0 && values[y] + j < Wordist.GetLength(1)
                       
                        )
                    {
                        AdjacencyMatrix[pos, GetPos(Wordist[i + values[x], values[y] + j])] = 1;
                    }
                    
                }
            }
        }

        public int GetPos(string letter)
        {
            string[] letters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "Z"};
            int pos = letter[0] - 'A';

            return pos;
        }

    }
}
