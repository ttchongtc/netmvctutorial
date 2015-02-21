using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string str = "1235";

            int m = str[2] - '0';

            double t = Math.Pow(2, 4);

            int[] array = { 94,95,97,101,5,100,1,20 };

            Pair p = GetMaxDiffPair(array);

            Console.WriteLine("Min: {0} Max: {1}.", p.A, p.B);
             */

            //FindMicrosoft fm = new FindMicrosoft("MICROSOFT");
            WordFinder wf = new WordFinder("MICROSOFT");
            Console.WriteLine("End");
        }

        public static Pair GetMaxDiffPair(int[] array)
        {
            int min = array[0];
            int delta = array[1] - array[0];
            for (int i = 2; i < array.Length; i++)
            {
                // Reset delta
                if (array[i] - min > delta)
                {
                    delta = array[i] - min;
                }

                // Reset min
                if (array[i] - array[i-1] > delta)
                {
                    min = array[i-1];
                    delta = array[i] - min;
                }
            }

            return new Pair
            {
                A = min,
                B = delta + min
            };
        }
    }
}
