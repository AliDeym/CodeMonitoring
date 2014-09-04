using System;

using System.Collections.Generic;

using System.IO;

namespace CodeMonitoring
{
    class Program
    {
        static List<string> _files;

        static float LongestLine = 0;

        static float TotalChars = 0;
        static float TotalFiles = 0;
        static float TotalLines = 0;

        static void Main(string[] args)
        {
            _files = new List<string>();

            Run();
        }

        static void Run()
        {
            _files.Clear();

            TotalChars = TotalLines = TotalFiles = LongestLine = 0;

            // Lazy to use environments or some other writelines in new lines of code.
            Console.WriteLine("Please enter the directory you want to start with:\n\n");

            var dir = Console.ReadLine();

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("INVALID DIRECTORY!!!!");
                Run();
                return;
            }

            ReadDir(dir);

            if (_files.Count == 0)
            {
                Console.WriteLine(String.Format("There's no file in \"{0}\"!!!", dir));
                Run();
                return;
            }

            TotalFiles = _files.Count;

            foreach (var db in _files)
            {
                using (var sr = new StreamReader(db))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length > LongestLine)
                            LongestLine = line.Length;

                        TotalChars += line.Length;

                        TotalLines += 1;
                    }
                }
            }

            Console.WriteLine("Statics: \n\n");

            Console.WriteLine("Total characters: " + TotalChars);
            Console.WriteLine("Total files     : " + TotalFiles);
            Console.WriteLine("Total lines     : " + TotalLines);
            Console.WriteLine("Longest line    : " + LongestLine);
            Console.WriteLine("Lines length avr: " + TotalChars / TotalLines);

            Run();
        }

        static void ReadDir(string dir)
        {
            var di = new DirectoryInfo(dir);

            foreach (var db in di.GetFiles())
            {
                _files.Add(db.FullName);
            }

            foreach (var ds in di.GetDirectories())
            {
                ReadDir(ds.FullName);
            }
        }
    }
}
