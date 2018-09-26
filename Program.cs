using System;
using System.IO;

namespace clines
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = null;
            if (args.Length > 0)
                path = args[0];
            else
            {
                Console.Write("Path (default is current): ");
                path = Console.ReadLine();
            }
            if (string.IsNullOrWhiteSpace(path))
                path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Path not found.");
                return;
            }

            string mask = null;
            if (args.Length > 1)
                mask = args[1];
            else
            {
                Console.Write("Mask (default is '*.cs'): ");
                mask = Console.ReadLine();
            }
            if (string.IsNullOrWhiteSpace(mask))
                mask = "*.cs";

            var files = Directory.GetFiles(path, mask, SearchOption.AllDirectories);
            int lineCount = 0;
            int fileCount = 0;
            foreach (string f in files)
            {
                bool hasLines = false;
                using (var sr = new StreamReader(f))
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Trim();
                        if (line.Length > 4)
                        {
                            hasLines = true;
                            lineCount++;
                        }
                    }
                if (hasLines)
                    fileCount++;
            }
            Console.WriteLine("{0} lines in {1} files.", lineCount, fileCount);
            Console.ReadKey(true);
        }
    }
}
