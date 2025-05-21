using System;
using System.Text.RegularExpressions;

namespace Task3
{
    class LogFormatting
    {
        /// <summary>
        /// <Start>
        ///     В данном методе строка лога сначала обрабатывается (заменяются | на " ", затем двойные пробелы на обычные, 
        ///     чтобы исключить сплит строки на ненужные элементы массива).
        ///     на каждом этапе формирования итоговой строки происходит проверка на null,
        ///     чтобы более точно определить подходящий ли это формат лога.
        ///     Если значение нулевое, то возвращается строка "error" что было описано в класса Program.
        ///     
        ///     Возвращаемая строка составляется по частям из всех элементов лога, между элементами идёт табуляция.
        /// </Start>
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public string Start(string log)
        {
            try
            {
                string date, time, logStatus, CalledMethod, Message;
                string[] logElement = log.Replace('|', ' ').Replace("  ", " ").Split(' ');

                date = DateFormatting(logElement[0].Replace(".", "-").Split('-'));
                if (date is null) return "error";
                time = logElement[1];
                if (time is null) return "error";
                logStatus = LogStatusCheck(logElement[2]);
                if (logStatus is null) return "error";
                CalledMethod = CalledMethodCheck(logElement);
                if (CalledMethod is null) return "error";
                Message = MessageCheck(logElement);
                if (Message is null) return "error";

                return $"{date}\t{time}\t{logStatus}\t{CalledMethod}\t{Message}";
            }
            catch (Exception)
            {
                return "error";
            }
        }
        //Т.к. формата входящего лога всего 2, то формирование даты происходит вручную
        // В задание был описан формат DD-MM-YYYY (день-месяц-год), но в примере YYYY-MM-DD (пример из файла: 2025-03-10)
        private string DateFormatting(string[] dateElement)
        {
            try
            {
                if (dateElement[0].Length > dateElement[2].Length)
                    return $"{dateElement[2]}-{dateElement[1]}-{dateElement[0]}";
                else
                    return $"{dateElement[0]}-{dateElement[1]}-{dateElement[2]}";
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Массив, который содержит в себе все возможные уровни логировнаия.
        private string[] logStatusTypes = { "INFO", "WARN", "ERROR", "DEBUG" };
        //Поиск происходит через регулярные выражения, чтобы сокращать все подходящие слова INFORMATION => INFO
        private string LogStatusCheck(string logStatus)
        {
            foreach (var type in logStatusTypes)
            {
                if (Regex.IsMatch(logStatus, type, RegexOptions.IgnoreCase)) return type;
            }
            return null;
        }
        /*В обоих форматах вызывающий метод имеет только латинские символы,
        *поэтому в данном методе происходит анализ оставшейся строки
        * на наличие английских букв. метод Возвращает либо найденный вызывающий метод, либо значение "DEFAULT"
        */
        private string CalledMethodCheck(string[] logElement)
        {
            for (int i = 3; i < logElement.Length; i++)
            {
                if (Regex.IsMatch(logElement[i], @"[a-zA-z]"))
                {
                    return logElement[i];
                }
            }

            return "DEFAULT";
        }
        /*
         * Как и в случае вызывающего метода, сообщения имеют только Кириллические символы. Поиск происходит аналогичным образом.
         * только само сообщение разделено на несколько элементов массива(т.к. сплит происходил по пробелам).
         * Поэтому метод возвращает все элементы строки после нахождения элемента массива с кириллицой.
         */
        private string MessageCheck(string[] logElement)
        {
            string result = string.Empty;
            for (int i = 3; i < logElement.Length; i++)
            {
                if (Regex.IsMatch(logElement[i], @"\p{IsCyrillic}", RegexOptions.IgnoreCase))
                {
                    for (int j = i; j < logElement.Length; j++) result += $"{logElement[j]} ";

                    return result;
                }
            }

            return null;
        }
    }
}
