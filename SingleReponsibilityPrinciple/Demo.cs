using System;
using System.Collections.Generic;
using System.IO;

/**
 * 单一职责原则，一个类尽量只做一系列相关的功能
 * 
 * 
 * 
 * 
 * 
 * 
 * ***/
namespace SingleReponsibilityPrinciple
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}:{text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

       
    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string fileName, bool overwrite = false)
        {
            if (overwrite || !File.Exists(fileName))
            {
                File.WriteAllText(fileName, j.ToString());
            }
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("How are you");
            j.AddEntry("I'M fine thank you");
            Console.WriteLine(j);

            var p = new Persistence();
           
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameof(Journal) + ".txt");
            p.SaveToFile(j, fileName);
        }
    }
}
