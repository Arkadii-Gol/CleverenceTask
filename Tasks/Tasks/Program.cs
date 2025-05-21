using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Введите строку для компрессии: ");
            string input = ReadLine();
            if (input == "") input = "aaabbcccdde";

            List<KeyValue> compressionString = StringCompressionVersion1(input);
            string result = StringDecompressionVersion1(compressionString);

            WriteData(input, compressionString, result);

            ReadLine();
        }
        public static List<KeyValue> StringCompressionVersion1(string inputString)
        {
            inputString += "!";// ограничитель, чтобы компрессия посчитала все символы входящей строки

            var resultDictionary = new List<KeyValue>();
            //список с классом принимающим символ и его количетсво повторений.
            //Используется список т.к. в словаре должны быть уникальные ключ - значения
            int startRepeat = 0, endRepeat = 1;
            // счётичики индекса символов строки, endRepeat имеет значение 1, потому что первый индекс имеет значение 0

            for (int indexChr = 0; indexChr < inputString.Length - 1; indexChr++)
            {
                if (inputString[indexChr] != inputString[indexChr + 1])
                {
                    endRepeat -= startRepeat;
                    resultDictionary.Add(new KeyValue(inputString[indexChr], endRepeat));

                    startRepeat = endRepeat;
                }

                endRepeat++;
            }

            return resultDictionary;
        }
        public static string StringDecompressionVersion1(List<KeyValue> compressionString)
        {
            string result = string.Empty;
            foreach (var item in compressionString)
            {
                for (int i = 0; i < item.Value; i++) result += item.Chr;
            }
            return result;
        }

        public static void WriteData(string input, List<KeyValue> compressionString, string result)
        {
            WriteLine($"Входящая строка: {input}");
            WriteLine("Компрессия:");
            string compression = string.Empty;
            foreach (var item in compressionString)
            {
                WriteLine($"{item.Chr} - {item.Value} ");

                if (item.Value == 1) compression += item.Chr;
                else compression += item.Chr + item.Value.ToString();
            }
            WriteLine($"Строка компрессии: {compression}");
            WriteLine($"Декомпрессия: {result}");
        }
    }
    class KeyValue //Класс для хранения символа и количества его повторений
    {
        public char Chr { get; set;}
        public int Value { get; set;}
        public KeyValue(char chr, int countChr)
        {
            Chr = chr;
            Value = countChr;
        }
    }
}
