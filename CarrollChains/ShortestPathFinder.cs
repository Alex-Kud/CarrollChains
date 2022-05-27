using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrollChains
{
    /// <summary>
    /// Класс для поиска кратчайшего пути между 0 и 1 вршинами графа с помощью алгоритма Дейкстры
    /// </summary>
    public class ShortestPathFinder
    {
        /// <summary>
        /// Конструтор класса
        /// </summary>
        /// <param name="matrix">Матрица смежности</param>
        /// <param name="matrixSize">Размер матрицы смежности</param>
        public ShortestPathFinder(int[,] matrix, int matrixSize)
        {
            Matrix = matrix;
            MatrixSize = matrixSize;
        }
        private int[,] Matrix { get; }
        private int MatrixSize { get; }

        /// <summary>
        /// Вывод матрицы смежности графа
        /// </summary>
        public void MatrixPrint()
        {
            Console.WriteLine("Матрица смежности графа ппреобразований слов\n(0 - переход за 1 шаг не возможен,\n1 - переход за 1 шаг возможен): ");
            Console.WriteLine();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write("{0,4}", Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private int MinDistance(int[] dist, bool[] sptSet)
        {
            var min = int.MaxValue;
            var minIndex = -1;

            for (int i = 0; i < MatrixSize; i++)
            {
                if (sptSet[i] == false && dist[i] <= min)
                {
                    min = dist[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        /// <summary>
        /// Алгоритм Дейкстры
        /// </summary>
        /// <param name="dic">Словарь</param>
        public void Dijkstra(string[] dic)
        {
            var dist = new int[MatrixSize];

            var path = new int[MatrixSize];

            var checkPoint = new bool[MatrixSize];

            for (int i = 0; i < MatrixSize; i++) // Заполняем масив кратчайших путей из посещенных точек
            {
                dist[i] = int.MaxValue;
                checkPoint[i] = false;
            }

            dist[0] = 0;

            for (int i = 1; i < MatrixSize; i++)
            {
                var minDist = MinDistance(dist, checkPoint);

                checkPoint[minDist] = true;

                for (int j = 0; j < MatrixSize; j++)
                {
                    if (!checkPoint[j] && Matrix[minDist, j] != 0 && dist[minDist] != int.MaxValue && dist[minDist] + Matrix[minDist, j] < dist[j])
                    {
                        dist[j] = dist[minDist] + Matrix[minDist, j];
                        path[j] = minDist; //Заполняется массив предков
                    }
                }
            }

            Console.WriteLine("Цепочка преобразований:");

            int k = 1;
            if (path[k] == 0)
            {
                Console.WriteLine(0);
            }
            else
            {
                var stack = new Stack<int>(); // Используем стэк для сохранения пути. Так как мы идем в обратном порядке, путь по итогу должен выводиться перевернтный
                stack.Push(path[k]);

                Console.Write($"{dic[0]} -> ");

                for (int j = path[k]; j != 0; j = path[j])
                {
                    if (path[j] == 0)
                    {
                        break;
                    }
                    else
                    {
                        stack.Push(path[j]);
                        j = path[j];
                    }
                }

                for (int j = 0; j <= stack.Count; j++)
                {
                    if (j == stack.Count)
                    {
                        Console.WriteLine($"{dic[k]}\n\nКоличество преобразований: {dist[k]}");
                    }
                    else
                    {
                        Console.Write(dic[stack.Pop()] + " -> ");
                        j = -1;
                    }
                }
            }
        }
    }
}
