using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
           
            List<Frequency> frequencyList = new List<Frequency>();
            List<Pair> pairList = new List<Pair>(); 
            Console.WriteLine("enter the file path of names and frequency");
            string filenameFrequencyList =   Console.ReadLine();
            ReadingTextfrequencyList(frequencyList, filenameFrequencyList);
            Console.WriteLine("enter the file path of the same names");
            string filenamePairList =   Console.ReadLine();
            ReadingpairList(pairList, filenamePairList);
            List<Frequency>  finalList= FinalList(frequencyList, pairList);
            WritingText(finalList);


           
        }

        public static List<Frequency> FinalList(List<Frequency> frequencyList, List<Pair> pairList)
        {

            int i = 0; int r, f;
            IDictionary<string, int> NamesDictionary = new Dictionary<string, int>();
            List<List<string>> groups = new List<List<string>>();
            foreach (Pair pair in pairList)
            {
                if ((!NamesDictionary.ContainsKey(pair.Name1)) && (!NamesDictionary.ContainsKey(pair.Name2)))//אם שתי השמות לא קיימים במילון
                {
                    List<string> l = new List<string>();
                    l.Add(pair.Name1);
                    l.Add(pair.Name2);
                    groups.Add(l);
                    
                    NamesDictionary.Add(pair.Name1, i);
                    NamesDictionary.Add(pair.Name2, i);
                     
                    i++;
                }
                else
                if ((NamesDictionary.ContainsKey(pair.Name1)) && (!NamesDictionary.ContainsKey(pair.Name2)))// אם רק שם 1 קיים
                {
                    r = NamesDictionary[pair.Name1];
                    groups[r].Add(pair.Name2);
                    NamesDictionary.Add(pair.Name2, r);
                }
                else

                if ((!NamesDictionary.ContainsKey(pair.Name1)) && (NamesDictionary.ContainsKey(pair.Name2)))// אם רק שם 2 קיים
                {
                    r = NamesDictionary[pair.Name2];
                    groups[r].Add(pair.Name1);
                    NamesDictionary.Add(pair.Name1, r);
                }
                else
                if ((NamesDictionary.ContainsKey(pair.Name1)) && (NamesDictionary.ContainsKey(pair.Name2)))// אם  שתיהם נמצאים
                {
                    if (NamesDictionary[pair.Name2] != NamesDictionary[pair.Name1])//אם יש להם אידס שונה(במקרה ששווה אין צורך לשנות שום דבר     

                        if (NamesDictionary[pair.Name2] > NamesDictionary[pair.Name1])
                        {
                            r = NamesDictionary[pair.Name1];
                            f = NamesDictionary[pair.Name2];
                            foreach (string name in groups[f])
                            {
                                groups[r].Add(name);
                                NamesDictionary[name] = r;
                            }
                            groups[f] = null;


                        }
                        else
                        {
                            r = NamesDictionary[pair.Name2];
                            f = NamesDictionary[pair.Name1];
                            foreach (string name in groups[f])
                            {
                                groups[r].Add(name);
                                NamesDictionary[name] = r;
                            }
                            groups[f] = null;
                        }
                }
                
            }

            List<Frequency> final = new List<Frequency>();
            foreach (Frequency frequency in frequencyList)
            {
                final.Add(null);
            }

                int length = groups.Count;
            foreach (Frequency frequency in frequencyList)
            {
                if (NamesDictionary.ContainsKey(frequency.Name))
                    if (final[NamesDictionary[frequency.Name]] == null)
                        final[NamesDictionary[frequency.Name]] = frequency;
                    else
                        final[NamesDictionary[frequency.Name]].Sum = final[NamesDictionary[frequency.Name]].Sum + frequency.Sum;
                else
                {
                    final[length] = frequency;
                    length++;
                }
                    
            }
            return final;



        }
        const string filename = @"..\..\..\final.txt";
        
        
        private static void WritingText(List<Frequency> finalList)
        {
            FileStream fs;

            if (File.Exists(filename))
            {
                fs = new FileStream(filename,
                                    FileMode.Append,
                                    FileAccess.Write);
            }
            else
            {
                fs = new FileStream(filename,
                                    FileMode.Create,
                                    FileAccess.Write);
            }

            StreamWriter writer = new StreamWriter(fs);
            foreach (Frequency item in finalList)
            {
                if (item != null)
                { 
                writer.WriteLine(item.Name+": " +item.Sum);
                Console.WriteLine(item.Name + ": " + item.Sum);
                }
            }
            

            writer.Close();
            fs.Close();
        }

        private static void ReadingTextfrequencyList(List<Frequency> frequencyList,string filenameFrequencyList)
        {
            FileStream fs = new FileStream(filenameFrequencyList,
                                           FileMode.Open,
                                           FileAccess.Read);

            StreamReader reader = new StreamReader(fs);

            string? str = reader.ReadLine();
            str= str.Trim();
            string[] st =  str.Split(new string[] { "," , "-"}, StringSplitOptions.None);
            for (int i = 0; i < st.Length ; i=i+2)
            {
                st[i]= (st[i]).Trim();
                st[i+1] = (st[i+1]).Trim();
                frequencyList.Add(new Frequency(st[i],Convert.ToInt32(st[i+1])));
            }
            Console.WriteLine(str);

            reader.Close();
            fs.Close();
        }

        private static void ReadingpairList(List<Pair> pairList,string filenamePairList)
        {
            FileStream fs = new FileStream(filenamePairList,
                                           FileMode.Open,
                                           FileAccess.Read);

            StreamReader reader = new StreamReader(fs);

            string? str = reader.ReadLine();
            str = str.Trim();
            string[] st = str.Split(new string[] { ",", "-" }, StringSplitOptions.None);
            for (int i = 0; i < st.Length; i = i + 2)
            {
                st[i] = (st[i]).Trim();
                st[i + 1] = (st[i + 1]).Trim();
                pairList.Add(new Pair(st[i], st[i + 1]));
            }
            Console.WriteLine(str);

            reader.Close();
            fs.Close();
        }
    }
}