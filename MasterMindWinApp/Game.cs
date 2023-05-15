using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWinApp
{
    public class Game
    {
        public int _dimension { get; set; }
        private int temp;

        public Game(int dimension)
        {
            _dimension = dimension;
        }


        public int[] GenerateRandomNum()
        {
            int[] str = new int[_dimension];
            for (int i = 0; i < _dimension; i++)
            {
                int num;
                Random rnd = new Random();
                if (i == 0)
                    num = rnd.Next(1, 10);
                else
                    num = rnd.Next(0, 10);
                str[i] = num;
                System.Threading.Thread.Sleep(10);
            }

            return str;
        }

        public int[] CheckNumbers(int[] userNum,int[] sysNum)
        {
            int[] result = new int[_dimension];
            int[] tempArray = new int[_dimension];
            userNum.CopyTo(tempArray, 0);


            for (int i = 0; i < tempArray.Length; i++)
            {
                if (tempArray[i]==sysNum[i])
                {
                    result[i] = 1;
                }
            }
            
            for (int i = 0; i < result.Length; i++)
            {
                if(result[i]==1)
                {
                     temp= tempArray[i];
                    for (int j = 0; j < tempArray.Length; j++)
                    {
                        if (tempArray[j] == temp)
                            tempArray[j] = -1;
                    }
                }

            }
            for(int i = 0; i < tempArray.Length; i++)
            {
                if (sysNum.Contains(tempArray[i]))
                {
                    result[i] = 2;
                }else if(tempArray[i]!=-1)
                {
                    result[i] = 0;
                }
                  
            }
            return result;
        }



    }
}
