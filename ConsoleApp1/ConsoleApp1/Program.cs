using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void StartOrChangeSizeOfField(ref int field_Rows_Size, ref int field_Cols_Size)
        {
            while (true)
            {
                Console.WriteLine("Чтобы начать игру нажмите 1\n Чтобы выбрать размеры поля нажмите 2");
                if (int.TryParse(Console.ReadLine(), out int changeOrNotSizes) && changeOrNotSizes is 1 or 2)
                {
                    if (changeOrNotSizes == 2)
                    {
                        UsersSizeOfField(out field_Rows_Size, out field_Cols_Size);
                    }
                    break;
                }
                Console.WriteLine("Некорректный ввод");
            }
        }
        static void UsersSizeOfField(out int field_Rows_Size, out int field_Cols_Size)
        {
            const int MaxSize = 9;
            const int MinSize = 3;
            while (true)
            {
                Console.WriteLine("Введите размер по rows:");
                if (int.TryParse(Console.ReadLine(), out field_Rows_Size) && field_Rows_Size is >= MinSize and <= MaxSize)
                    break;

                Console.WriteLine("Размер поля может быть минимум 3х3 и максимум 9х9");
            }
            while (true)
            {
                Console.WriteLine("Введите размер по cols:");
                if (int.TryParse(Console.ReadLine(), out field_Cols_Size) && field_Cols_Size is > 2 and < 10)
                    break;

                Console.WriteLine("Размер поля может быть минимум 3х3 и максимум 9х9");
            }
        }
        static char[,] CreateField(int field_Rows_Size, int field_Cols_Size)
        {
            const char EmptyPlace = '?';
            char[,] field = new char[field_Rows_Size, field_Cols_Size];
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    field[i, j] = EmptyPlace;
            return field;
        }
        static void ShowField(char[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write($"{field[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        static bool crossesOrZeros = false;
        static int draw = 0;
        static void Draw(char[,] field)
        {
            if (draw >= field.GetLength(0) * field.GetLength(1))
            {
                Console.WriteLine("Ничья");
                Environment.Exit(0);
            }
        }
        static void VictoryChecker(char[,] field, char Zero, char Cross)
        {
            bool isCrossesWon = false;
            bool isZerosWon = false;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (j + 2 < field.GetLength(1))
                    {
                        if (field[i, j] == Cross && field[i, j + 1] == Cross && field[i, j + 2] == Cross)
                        {
                            isCrossesWon = true;
                        }
                        else if (field[i, j] == Zero && field[i, j + 1] == Zero && field[i, j + 2] == Zero)
                        {
                            isZerosWon = true;
                        }
                    }
                    if (i + 2 < field.GetLength(0))
                    {
                        if (field[i, j] == Cross && field[i + 1, j] == Cross && field[i + 2, j] == Cross)
                        {
                            isCrossesWon = true;
                        }
                        else if (field[i, j] == Zero && field[i + 1, j] == Zero && field[i + 2, j] == Zero)
                        {
                            isZerosWon = true;
                        }
                    }
                    if (i + 2 < field.GetLength(0) && j + 2 < field.GetLength(1))
                    {
                        if (field[i, j] == Cross && field[i + 1, j + 1] == Cross && field[i + 2, j + 2] == Cross)
                        {
                            isCrossesWon = true;
                        }
                        else if (field[i, j] == Zero && field[i + 1, j + 1] == Zero && field[i + 2, j + 2] == Zero)
                        {
                            isZerosWon = true;
                        }
                    }
                    if (i - 2 >= 0 && j + 2 < field.GetLength(1))
                    {
                        if (field[i, j] == Cross && field[i - 1, j + 1] == Cross && field[i - 2, j + 2] == Cross)
                        {
                            isCrossesWon = true;
                        }
                        else if (field[i, j] == Zero && field[i - 1, j + 1] == Zero && field[i - 2, j + 2] == Zero)
                        {
                            isZerosWon = true;
                        }
                    }
                }
            }
            if (isCrossesWon)
            {
                Console.WriteLine("Крестики победили");
                Environment.Exit(0);
            }
            else if (isZerosWon)
            {
                Console.WriteLine("Нолики победили");
                Environment.Exit(0);
            }
        }
        static void CrossesOrZerosMovings(char[,] field, char Cross, char Zero)
        {
            while (true)
            {
                string strIndex = Console.ReadLine().Replace(" ", "");
                if (strIndex.Length == 2 && int.TryParse(strIndex[0].ToString(), out int row) && int.TryParse(strIndex[1].ToString(), out int col))
                {
                    try
                    {
                        if (field[row, col] == '?')
                        {
                            Console.Clear();
                            if (crossesOrZeros == false)
                            {
                                field[row, col] = Cross;
                            }
                            else
                            {
                                field[row, col] = Zero;
                            }
                            crossesOrZeros = !crossesOrZeros;
                            ++draw;
                            TheGameIsOn(field, Zero, Cross);
                        }
                        else
                        {
                            Console.WriteLine("Место занято");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод");
                }
            }
        }
        static void TheGameIsOn(char[,] field, char Zero, char Cross)
        {
            ShowField(field);
            VictoryChecker(field, Zero, Cross);
            if (!crossesOrZeros)
            {
                Console.WriteLine("Ход крестиков:");
            }
            else
            {
                Console.WriteLine("Ход ноликов:");
            }
            CrossesOrZerosMovings(field, Cross, Zero);
        }
        static void Main(string[] args)
        {
            int field_Rows_Size = 3;
            int field_Cols_Size = 3;
            const char Zero = 'O';
            const char Cross = 'X';
            StartOrChangeSizeOfField(ref field_Rows_Size, ref field_Cols_Size);
            char[,] field = CreateField(field_Rows_Size, field_Cols_Size);
            TheGameIsOn(field, Zero, Cross);
        }
    }
}
