using System;

namespace CarrollChains
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\tДополнительное задание по ООП.\n" +
                "\tРешение задачи \"Метаграмма\" для поиска оптимального пути цепочек Кэрролла\n" +
                "\t\tВыполнил: студент гр.20ВП1 Кудашов Александр\n");
            Console.Write("Введите размер словаря: ");
            int matrixSize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите словарь (каждое новое слово на новой строке):\n");
            string[] dictionary = new string[matrixSize];
           
            // Считывание словаря
            for (int i = 0; i < matrixSize; i++)
            {
                //Console.Write($"Введите {i}-ое слово словаря: ");
                dictionary[i] = Console.ReadLine();
            }

            int[,] matrix = GetMatrix(dictionary, matrixSize);
            Console.WriteLine();
            ShortestPathFinder test = new(matrix, matrixSize);
            test.MatrixPrint();
            test.Dijkstra(dictionary);
        }

        /// <summary>
        /// Матрица смежности графа возможных преобразований слов
        /// </summary>
        /// <param name="dictionary">Словарь слов</param>
        /// <param name="matrixSize">Длина словаря</param>
        /// <returns></returns>
        static int[,] GetMatrix(string[] dictionary, int matrixSize)
        {
            int[,] matrix = new int[matrixSize, matrixSize];

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    // Заполнение весов переходов из вершины в саму себя
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                        continue;
                    }
                    // Заполнение весов переходов из вершины во все другие вершины
                    // Если переход за 1 шаг возможен - вес = 1, если не возможен, вес = 0
                    matrix[i, j] = (HasOneDiff(dictionary[i], dictionary[j])) ? 1 : 0;
                }
            }

            return matrix;
        }

        /// <summary>
        /// Проверка на то, имеет ли 2 строку разницу лишь в одном символе
        /// </summary>
        /// <param name="BaseString">Исходная строка</param>
        /// <param name="StringToCountDiff">Строка, разница с которой проверяется</param>
        /// <returns></returns>
        static bool HasOneDiff(string BaseString, string StringToCountDiff)
        {
            int diffCount = 0;

            if (BaseString.Length == StringToCountDiff.Length)
            {
                for (int i = 0; i < BaseString.Length; i++)
                {
                    if (BaseString[i] != StringToCountDiff[i])
                    {
                        diffCount++;
                    }
                }
                return diffCount == 1;
            }

            return false;
        }
    }
}