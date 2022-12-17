using Practice_2_Artificial_Intelligence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2_Artificial_Intelligence
{
    internal class Program
    {

        public static List<int[]> Matrix = new List<int[]>();
        public static List<int> SumBrazesh = new List<int>();


        static void Main(string[] args)
        {
            #region Variable
            int Population;
            List<double> Chance_Percent = new List<double>();
            #endregion
            #region Input Population
            Console.Write("Enter the population : ");
            while (true)
            {
                string Input_Population = Console.ReadLine();

                int.TryParse(Input_Population, out Population);
                if (Population == 0)
                {
                    Console.Write("Enter the population again : ");
                }
                else
                    break;
            }
            #endregion
            for (int i = 0; i < Population; i++)
            {
                SumBrazesh.Add(1401);
            }
            Create_Population(Population);
            int Counter = 0;
            while (true)
            {
                Counter++;
                Brazesh_Function(Population);

                foreach (var item in SumBrazesh)
                {
                    if (item >= -50 && item <= 50)
                    {
                        break;
                    }
                }


                Chance_Percent = SetChance_Percent(Chance_Chromosome(Population));

                List<int> order = Select_Chromosome(Chance_Percent).Result;
                for (int i = 0; i < order.Count; i++)
                {
                    Matrix[i] = Matrix[order[i]];
                }
                //Excute Cross Over
                CrossOver(Population);

                // Mutation
                Mutation(Population);
            }

            ShowFunction(Population, Counter);
            Console.ReadKey();
        }

        #region Create Population By Input
        public static void Create_Population(int Population_Number)
        {
            // باید به اندازه جمعیت ورودی کرومزوم بسازیم و هر ژن یک مقدار رندوم است
            for (int i = 0; i < Population_Number; i++)
            {
                Matrix.Add(Random_Array(3).Result);
            }
        }
        #endregion
        #region Random Array
        private static async Task<int[]> Random_Array(int len)
        {
            int[] array = new int[len];
            for (int i = 0; i < len; i++)
            {
                Random random = new Random();
                if (i == 0 || i == 1)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    array[i] = random.Next(-20, 20);
                }
                else
                {
                    array[i] = random.Next(-100, 100);
                }
            }
            return array;
        }
        #endregion
        #region Brazesh Function
        public static void Brazesh_Function(int Population)
        {
            for (int i = 0; i < Population; i++)
            {
                int x = Matrix[i][0];
                int y = Matrix[i][1];
                int z = Matrix[i][2];

                int Sum = 2 * (int)(Math.Pow(x, 2)) + 3 * (int)Math.Pow(y, 2) + z - 2 * x * y + 10 * x * z - 5;
                SumBrazesh[i] = Sum;
            }

        }
        #endregion
        #region Set Chance For evry Chromosome
        public static double[] Chance_Chromosome(int Population)
        {
            double[] array = new double[Population];

            double Sum_Denominator = 0;

            for (int i = 0; i < SumBrazesh.Count; i++)
            {
                Sum_Denominator += (double)1.0 / Math.Abs(SumBrazesh[i]);
            }

            for (int i = 0; i < Population; i++)
            {
                array[i] = ((double)1.0 / Math.Abs(SumBrazesh[i])) / Sum_Denominator;
            }
            var sumarray = array.Sum(); // this sum is equal =  1 
            return array;
        }
        #endregion
        #region Set Chance Percent 
        public static List<double> SetChance_Percent(double[] Chances)
        {
            List<double> Chance_Percent = new List<double>();
            foreach (var item in Chances.ToList())
            {
                Chance_Percent.Add(item * 100);
            }
            return Chance_Percent;
        }

        #endregion
        #region Select Chromosome 
        // انتخاب کروموزم بر اساس شانس
        public static async Task<List<int>> Select_Chromosome(List<double> Chance_Percent)
        {
            List<int> Order_Chromosome = new List<int>();

            Random random = new Random();
            for (int i = 0; i < Chance_Percent.Count; i++)
            {
                double Sum1 = 0, Sum2 = 0;
                await System.Threading.Tasks.Task.Delay(100);
                int Random_Number = random.Next(0, 100);
                for (int j = 0; j < Chance_Percent.Count; j++)
                {
                    Sum1 = Sum2;
                    Sum2 += Chance_Percent[j];
                    if (Random_Number >= Sum1 && Random_Number <= Sum2)
                    {
                        Order_Chromosome.Add(i);
                        break;
                    }
                }
            }

            return Order_Chromosome;
        }
        #endregion
        #region Cross Over Point
        public static async void CrossOver(int Population)
        {
            Random random = new Random();
            int CrossOver_point = random.Next(0, 1);
            await System.Threading.Tasks.Task.Delay(100);
            int Random_Number1 = random.Next(0, Population - 1); 
            int Random_Number2;
            await System.Threading.Tasks.Task.Delay(100);
            while (true)
            {
                Random_Number2 = random.Next(0, Population - 1);
                if (Random_Number2 != Random_Number1)
                    break;
            }
            int[] Chromosom1 = Matrix[Random_Number1];
            int[] Chromosom2 = Matrix[Random_Number2];


            if (CrossOver_point == 0)
            {
                var r = Matrix[Random_Number1][0];
                Chromosom1[0] = Chromosom2[0];
                Chromosom2[0] =r;
                Matrix[Random_Number1] = Chromosom1;
                Matrix[Random_Number2] = Chromosom2;
            }
            else
            {
                Chromosom1[2] = Chromosom2[2];
                Chromosom2[2] = Matrix[Random_Number1][2];
                Matrix[Random_Number1] = Chromosom1;
                Matrix[Random_Number2] = Chromosom2;
            }
        }
        #endregion
        #region Mutation
        public static async void Mutation(int Population)
        {
            int Percent_Population = (int)((Population * 3) / 10.0);
            if(Percent_Population==0)
            {
                Percent_Population = 1;
            }
            Random random = new Random();
            for (int i = 0; i < Percent_Population; i++)
            {
                int Random_Chromosome = random.Next(0, Population - 1);
                int Indext_Chromosome = random.Next(0, 2);

                if (Indext_Chromosome == 2)
                {
                    var t = random.Next(-100, 100);
                    if (t == Matrix[Random_Chromosome][Indext_Chromosome])
                    {
                        i--;
                        continue;
                    }
                    Matrix[Random_Chromosome][Indext_Chromosome] = t;
                }
                else
                {
                    var t = random.Next(-20, 20);
                    if (t == Matrix[Random_Chromosome][Indext_Chromosome])
                    {
                        i--;
                        continue;
                    }
                    Matrix[Random_Chromosome][Indext_Chromosome] = t;
                }
                await System.Threading.Tasks.Task.Delay(100);
            }
        }
        #endregion
        #region Show Function
        public static void ShowFunction(int Population, int Counter)
        {
            // Population + Brazesh

            for (int i = 0; i < Population; i++)
            {
                foreach (var item in Matrix[i])
                {
                    Console.Write(item + "\t");
                }
                Console.WriteLine(" => " + SumBrazesh[i]);

            }
            Console.WriteLine("-------------------------------------------------------------------");
            for (int i = 0; i < Population; i++)
            {
                if (SumBrazesh[i] >= -50 && SumBrazesh[i] <= 50)
                {
                    foreach (var item in Matrix[i])
                    {
                        Console.Write(item + "\t");
                    }
                    Console.WriteLine(" => " + SumBrazesh[i]);

                }
            }
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"number of repetitions = {Counter}");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Error : ");
            for (int i = 0; i < Population; i++)
            {

                if (SumBrazesh[i] >= -50 && SumBrazesh[i] <= 50)
                {
                    foreach (var item in Matrix[i])
                    {
                        Console.Write(item + "\t");
                    }
                    Console.WriteLine("Error = " + SumBrazesh[i]);

                }
            }

        }
        #endregion
    }
}
