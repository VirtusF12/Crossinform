
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace Crossinform
{
    class Program {


        private static bool isExistsFile(string path) {

            return new FileInfo(path).Exists;
        }

        private static string getTextFromFile(string path) {


            if (isExistsFile(path))

                using (FileStream fstream = File.OpenRead($"{path}")) {

                    byte[] array = new byte[fstream.Length]; // преобразуем строку в байты
                    fstream.Read(array, 0, array.Length); // считываем данные
                    string textFromFile = System.Text.Encoding.Default.GetString(array); // декодируем байты в строку

                    return textFromFile;
                }
            else {
                Console.WriteLine("файла такого нет!");
                return null;
             }
        }

        private static List<string> getListTriplets(string text) {

            List<string> list = new List<string>();
            string[] arr = text.Split(" ");

            for (int i = 0; i < arr.Length; i++) {

                string word = arr[i];

                for (int j = 0; j < word.Length - 2; j++)
                    list.Add(word.Substring(j, 3));
            }

            return list;
        }

        private static void printList(List<string> list) { 
            foreach(string s in list)
                Console.WriteLine(s);
        }

        private static void printTop10Triplets(Dictionary<string, int> dic, int top = 10) {

            
            var filterDic = dic.OrderByDescending(pair => pair.Value);
            for (int i = 0; i < top; i++)
                Console.WriteLine(i+1 + ") " +filterDic.ElementAt(i).Key + " : " + filterDic.ElementAt(i).Value);
        }

        private static Dictionary<string, int> getDictionary(List<string> listTriplets) {

            Dictionary<string, int> dic = new Dictionary<string, int>();

            for (int i = 0; i < listTriplets.Count; i++)
            {

                string triplet = listTriplets[i];
                int count = 0;

                if (dic.ContainsKey(triplet) == false)
                {

                    for (int j = 0; j < listTriplets.Count; j++)
                        if (listTriplets[j].Equals(triplet))
                            count++;

                    dic.Add(triplet, count);
                }

            }
            return dic;
        }

        static void Main(string[] args)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            printTop10Triplets(getDictionary(getListTriplets(getTextFromFile("D://Crossinform//file.txt").ToLower())));
                 
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00} часов, {1:00} минут, {2:00} секунд, {3:000} миллисекунд",
                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Время выполнения программы: " + elapsedTime);

            Console.ReadLine();
        }

       
    }
}
