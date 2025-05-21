using System;
using System.Collections.Generic;
using System.IO;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> logsFormatingList = new List<string>();
            List<string> logsFormatingErrorList = new List<string>();
            LogRead reader = new LogRead(); // объект, который читает лог-файл
            LogFormatting formatting = new LogFormatting(); // объект, который форматирует список логов из объекта LogRead

            /*
             * простейшая реализация интерфейса, можно ввести путь до необходимого файла или использовать файл по умолчанию.
             * есть проверка на корректность ввода (существует ли файл), 
             * также в конце уточняется есть ли ещё какой-нибудь файл для форматирования.
             * Этот функционал реализован в main т.к. это простое управление классами.
            */
            while (true)
            {
                endWile:
                Console.WriteLine("Введите путь до файла " + 
                    @"(Если файл не выбран, то будет использоваться файл по умолчанию ""input_log.txt"" из корня проекта):");
                string path = Console.ReadLine();
                if (path == "")
                    path = "input_log.txt";
                if (!File.Exists(path))
                {
                    Console.WriteLine("Данного файла не существует или он не найден.\r\n");
                    goto endWile;
                }

                // чтение лог-файла и получение списка значений
                reader.ReadFile(path);
                //передача всех необходимых параметров для обработки списка логов
                Formatting(formatting, reader, logsFormatingList, logsFormatingErrorList);

                //вызов методов для итоговой записи в txt. 2 вызова потому что по т.з. необходимо записывать в 2 txt файла
                TxtWriteResult("output_log.txt", logsFormatingList);
                TxtWriteResult("problems.txt", logsFormatingErrorList);

                //дополнительный вывод результатов работы программы в консоле
                ConsoleWriteResult(logsFormatingList, "Отформатированные логи:");
                if (logsFormatingErrorList.Count > 0) ConsoleWriteResult(logsFormatingErrorList, "Ошибка форматирования:");

                //уточнение для завершения работы программы.
                Console.Write("Отформатировать другой файл y/n: ");
                string chrEnding = Console.ReadLine();
                if (chrEnding == "N" || chrEnding == "n") break;

                //очистка всех списков перед новой итерацией цикла.
                reader.Logs.Clear();
                logsFormatingList.Clear();
                logsFormatingErrorList.Clear();
            }
        }
        public static void Formatting(LogFormatting formatting, LogRead reader, List<string> logsFormatingList, List<string> logsFormatingErrorList)
        {
            /*
             * основной рабочий метод.
             * Метод "Start" объекта класса "LogFormatting" принимает в себя строку списка(один из логов)
             * в случае какой либо ошибки или некорректности самого лога, метод возвращает значение error,
             * который используется как иднетификатор того, куда помещать лог (в результат или в список ошибок)
             */
            foreach (var log in reader.Logs)
            {
                string logFormating = formatting.Start(log);
                if (logFormating != "error")
                    logsFormatingList.Add(logFormating);
                else
                    logsFormatingErrorList.Add(log);
            }
        }
        public static void TxtWriteResult(string fileName, List<string> logsList )
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                foreach (var log in logsList) writer.WriteLine(log);
                writer.WriteLine("-----------------------------------------" +
                    "---------------------------------------------------------------------------------");
            }
        }
        public static void ConsoleWriteResult(List<string> logsList, string statusLogs)
        {
            Console.WriteLine(statusLogs);
            foreach (var log in logsList)
            {
                Console.WriteLine(log);
            }
        }
    }
}
