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
                        field_Rows_Size = UsersSizeOfField("rows");
                        field_Cols_Size = UsersSizeOfField("cols");
                    }
                    break;
                }
                Console.WriteLine("Некорректный ввод");
            }
        }
        static int UsersSizeOfField(string rowOrColl)
        {
            const int MaxSize = 9;
            const int MinSize = 3;
            while (true)
            {
                Console.WriteLine($"Введите размер по rows: {rowOrColl}");
                if (int.TryParse(Console.ReadLine(), out int size) && size is >= MinSize and <= MaxSize)
                    return size;
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
        static void Draw(char[,] field, int draw)
        {
            if (draw >= field.GetLength(0) * field.GetLength(1))
            {
                Console.WriteLine("Ничья");
                Environment.Exit(0);
            }
        }
        static bool VictoryChecker(char[,] field, char Symbol)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (j + 2 < field.GetLength(1) && (field[i, j] == Symbol && field[i, j + 1] == Symbol && field[i, j + 2] == Symbol))
                    {
                        return true;
                    }
                    else if (i + 2 < field.GetLength(0) && (field[i, j] == Symbol && field[i + 1, j] == Symbol && field[i + 2, j] == Symbol))
                    {
                        return true;
                    }
                    else if (i + 2 < field.GetLength(0) && j + 2 < field.GetLength(1) && (field[i, j] == Symbol && field[i + 1, j + 1] == Symbol && field[i + 2, j + 2] == Symbol))
                    {
                        return true;
                    }
                    else if (i - 2 >= 0 && j + 2 < field.GetLength(1) && (field[i, j] == Symbol && field[i - 1, j + 1] == Symbol && field[i - 2, j + 2] == Symbol))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static void CrossesOrZerosMovings(char[,] field, char Symbol)
        {
            while (true)
            {
                string strIndex = Console.ReadLine().Replace(" ", "");
                if (strIndex.Length == 2 && int.TryParse(strIndex[0].ToString(), out int row) && int.TryParse(strIndex[1].ToString(), out int col) && row < field.GetLength(0) && col < field.GetLength(1))
                {
                    if (field[row, col] == '?')
                    {
                        Console.Clear();
                        field[row, col] = Symbol;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Место занято");
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
            bool crossesOrZeros = false;
            int draw = 0;
            while (true)
            {
                Console.WriteLine("Введите 2 цифры сначала значение по row, затем по col");
                    Console.WriteLine(crossesOrZeros ? "Ход ноликов" : "Ход крестиков");
                    CrossesOrZerosMovings(field, crossesOrZeros ? Zero : Cross);
                bool isWon = VictoryChecker(field, crossesOrZeros ? Zero : Cross);
                ShowField(field);
                if (isWon == true)
                {
                    Console.WriteLine(crossesOrZeros ? "нолики победили" : "крестики победили");
                    Environment.Exit(0);
                }
                ++draw;
                Draw(field, draw);
                crossesOrZeros = !crossesOrZeros;
            }
        }
        static void Main(string[] args)
        {
            int field_Rows_Size = 3;
            int field_Cols_Size = 3;
            const char Zero = 'O';
            const char Cross = 'X';
            StartOrChangeSizeOfField(ref field_Rows_Size, ref field_Cols_Size);
            char[,] field = CreateField(field_Rows_Size, field_Cols_Size);
            ShowField(field);
            TheGameIsOn(field, Zero, Cross);
        }
    }
}
