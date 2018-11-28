using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrillTagger
{
    class Program
    {
        static Dictionary<string, Dictionary<string, int>> posOcurrences;
        static void Main(string[] args)
        {
            posOcurrences = new Dictionary<string, Dictionary<string, int>>();
            ReadFiles();
            TagWords(posOcurrences);
            Console.ReadKey();
        }

        static void ReadFiles()
        {
            using (StreamReader stream = File.OpenText("C:\\Users\\Delia\\Desktop\\ANUL IV\\Licenta\\BrillTagger\\brown\\cats.txt"))
            {
                string line = string.Empty;
                while ((line = stream.ReadLine()) != null)
                {
                    var splitLine = line.Split(' ');
                    //Console.WriteLine(splitLine[0]);

                    ReadFileContent(splitLine[0]);
                    //break;

                }
            }

        }

        static void ReadFileContent(string fileName)
        {
            
            using (StreamReader stream = File.OpenText($"C:\\Users\\Delia\\Desktop\\ANUL IV\\Licenta\\BrillTagger\\brown\\{fileName}"))
            {
                string line = string.Empty;
                while ((line = stream.ReadLine()) != null)
                {

                    var trimmedLine = line.Trim();
                    //Console.WriteLine(trimmedLine);
                    var spliTrimmedtLine = trimmedLine.Split(' ');
                    foreach (var word in spliTrimmedtLine)
                    {
                        if(string.IsNullOrWhiteSpace(word))
                        {
                            continue;
                        }
                        //Console.WriteLine(word);
                        var splitWord = word.Split('/');
                        
                        if(! posOcurrences.Keys.Contains(splitWord[0]))
                        {
                            posOcurrences.Add(splitWord[0], new Dictionary<string, int>());
                            posOcurrences[splitWord[0]].Add(splitWord[1], 1);
                        }
                        else
                        {
                            if(! posOcurrences[splitWord[0]].Keys.Contains(splitWord[1]))
                            {
                                posOcurrences[splitWord[0]].Add(splitWord[1], 1);
                            }
                            else
                            {
                                posOcurrences[splitWord[0]][splitWord[1]]++;
                            }
                        }
                    }
                }

                // ListOccurences(posOcurrences);
               

            }
           
        }

        static void ListOccurences(Dictionary<string, Dictionary<string, int>> dict)
        {
            foreach( var key in dict.Keys)
            {
                foreach( var pos in dict[key].Keys)
                {
                    Console.WriteLine($"{key} - {pos} : {dict[key][pos]}");
                }
            }
        }

        static void TagWords(Dictionary<string, Dictionary<string, int>> dict)
        {
            var text = "A bird is on the fence";
            var splitText = text.Split(' ');
            foreach( var word in splitText)
            {
                var pos = "?";
                if(dict.Keys.Contains(word))
                {
                    var maxValue = dict[word].Max(x => x.Value);
                    pos = dict[word].FirstOrDefault(x => x.Value == maxValue).Key;
                }
                Console.WriteLine($"{word} - {pos}");
            }


        }
    }
}
