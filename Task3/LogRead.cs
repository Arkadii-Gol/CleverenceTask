using System;
using System.Collections.Generic;
using System.IO;

namespace Task3
{
    class LogRead
    {
        /// <summary>
        /// <Logs>
        ///     Свойство используется для того, чтобы исключить добавления в список пустых строк.
        /// </Logs>
        /// <LogRead>
        ///     Реализовано 2 конструктора для большего удобства, в потенциально других случаях применения данного класса
        /// </LogRead>
        /// </summary>
        private List<string> logs = new List<string>();
        public List<string> Logs
        {
            set
            {
                foreach (var log in value)
                {
                    if (log != "") logs.Add(log);
                }
            }
            get { return logs; }
        }
        public LogRead(string pathLogFile) => ReadFile(pathLogFile);
        public LogRead() { }
        //файл читается по строчно для дальнейшей работы.
        public void ReadFile(string pathLogFile)
        {
            Logs = new List<string> (File.ReadLines(pathLogFile));
        }
    }
}
