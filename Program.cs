
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ConsoleAppfilecount
{
    class Program
    {

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            string path, path1;
            Console.WriteLine("Enter the path:");
            path = Console.ReadLine();
            
            // to fetch the file paths
            var fileCount = (from file in System.IO.Directory.EnumerateFiles(@path, "*.zip", SearchOption.AllDirectories)
                             select file).Count();
            string[] filePaths = Directory.GetFiles(@path, "*.zip", SearchOption.AllDirectories);

            Console.WriteLine(fileCount);

            List<long> filesize = new System.Collections.Generic.List<long>();
            List<String> filepathlist = new System.Collections.Generic.List<String>();
            List<String> filesizelist = new System.Collections.Generic.List<String>();
            String join1;

            foreach (string file in Directory.GetFiles(@path, "*.zip", SearchOption.AllDirectories))
            {


                FileInfo finfo = new FileInfo(file);
                filesize.Add(finfo.Length);
                join1=string.Join("", filePaths);
                filepathlist.Append(join1);


            }
            
            


            


            string result = string.Join(", ", filesize.Select(i => i.ToString()).ToArray());

            string[] filesizearray = new string[] { "" };

            filesizearray = result.Split(',');

            long[] filesizearrayint = filesizearray.Select(long.Parse).ToArray();



            Dictionary<string, long> dictionary = new Dictionary<string, long>();
            string[] outputarray = new string[filePaths.Length + 10];
            for (int k = 0; k < filePaths.Length; k++)
            {
                //Console.WriteLine("size of files");
                //displying thr filesize and its memory
                outputarray[k] = filePaths[k] + "  Memory  " + filesizearray[k] + "Bytes";
                dictionary.Add(filePaths[k].ToString(), filesizearrayint[k]);
                Console.WriteLine(outputarray[k]);

            }
           
            var ordered = dictionary.OrderBy(x => x.Key);
            Console.WriteLine(" Files present and its length");
            foreach (var data in ordered)
            {
                Console.WriteLine("{0} {1}", data.Key, data.Value);

            }

            double[] mytotalsizearray = Array.ConvertAll(filesizearray, double.Parse);
            double[] mysizeless500 = new double[10000];
            double[] mysizeless1gb = new double[10000];
            double[] mysizeless2gb = new double[10000];
            double[] mysizeless3gb = new double[10000];
            int countless500 = 0;
            int countless1gb = 0;
            int countless2gb = 0;
            int countless3gb = 0;
            //files checking for size of memory and storing them in its respective array
            System.Collections.Generic.List<double[]> outputless500 = new System.Collections.Generic.List<double[]>();
            for (int c = 0; c < filePaths.Length; c++)
            {
                if (mytotalsizearray[c] < 500000000 && mytotalsizearray[c]!=0)
                {
                    ++countless500;

                    mysizeless500[countless500] = mytotalsizearray[c];
                }
                if (mytotalsizearray[c] > 500000000 && mytotalsizearray[c] < 1000000000)
                {
                    ++countless1gb;

                    mysizeless1gb[countless1gb] = mytotalsizearray[c];
                }
                if (mytotalsizearray[c] > 1000000000 && mytotalsizearray[c] < 2000000000)
                {
                    ++countless2gb;

                    mysizeless2gb[countless2gb] = mytotalsizearray[c];
                }
                if (mytotalsizearray[c] > 2000000000 && mytotalsizearray[c] < 3000000000)
                {
                    ++countless3gb;

                    mysizeless3gb[countless3gb] = mytotalsizearray[c];
                }
            }  //for
            mysizeless500[countless500 + 1] = countless500;
            mysizeless1gb[countless1gb + 1] = countless1gb;
            mysizeless2gb[countless2gb + 1] = countless2gb;
            mysizeless3gb[countless3gb + 1] = countless3gb;


            Console.WriteLine("Enter the path to save the CSV:");
            string fPath = Console.ReadLine();
            Console.WriteLine("Enter the filename:");
            string fileName = Console.ReadLine();
            //string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = fPath + "\\" + fileName + ".csv";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            string delimter = ",";
            System.Collections.Generic.List<string[]> output = new System.Collections.Generic.List<string[]>();
            System.Collections.Generic.List<int[]> output1 = new System.Collections.Generic.List<int[]>();
            string[] resultlessthan500 = mysizeless500.Select(x => x.ToString()).ToArray();
            string[] resultlessthan1gb = mysizeless1gb.Select(x => x.ToString()).ToArray();
            string[] resultlessthan2gb = mysizeless2gb.Select(x => x.ToString()).ToArray();
            string[] resultlessthan3gb = mysizeless3gb.Select(x => x.ToString()).ToArray();
            resultlessthan500[0] = "countless500";
            resultlessthan1gb[0] = "countless1gb";
            resultlessthan2gb[0] = "countless2gb";
            resultlessthan3gb[0] = "countless3gb";
            string[] finalresultlessthan500 = resultlessthan500.Select(x => x.Replace("0", " ")).ToArray();
            string[] finalresultlessthan1gb = resultlessthan1gb.Select(x => x.Replace("0", " ")).ToArray();
            string[] finalresultlessthan2gb = resultlessthan2gb.Select(x => x.Replace("0", " ")).ToArray();
            string[] finalresultlessthan3gb = resultlessthan3gb.Select(x => x.Replace("0", " ")).ToArray();


            //var newresultlessthan500=metrics

            //flexible part ... add as many object as you want based on your app logic
            output.Add(filePaths);
            output.Add(finalresultlessthan500);
            output.Add(finalresultlessthan1gb);
            output.Add(finalresultlessthan2gb);
            output.Add(finalresultlessthan3gb);



            int length = output.Count;

            using (System.IO.TextWriter writer = File.CreateText(filePath))
            {
                for (int index = 0; index < length; index++)
                {  
                    writer.WriteLine(string.Join(delimter, output[index]));

                }
            }


        }
    }
}



