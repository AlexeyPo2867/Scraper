using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace WEBScraping_c
{
    public class HttpClientScraper : Component
    {
        public string responseBody = String.Empty;
        string moduleName = "HttpClientScarper";
        string filePath = "index.html";
        bool isDispose;

        List<int> index_tag = new List<int>();
        List<string> href = new List<string>();

        public int code_error = 0;

        public async Task ScrapePage(string URI)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // HttpResponseMessage response = await client.GetAsync(URI);
                    //  response.EnsureSuccessStatusCode();
                    //   responseBody = await response.Content.ReadAsStringAsync();

                    // Above three lines can be replaced with new helper method below
                    responseBody = await client.GetStringAsync(URI);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"\n Произошло исключение в модуле {moduleName}!");
                    Console.WriteLine("Сообщение об ОШИБКИ !!! :{0} ", e.Message);

                    responseBody = e.Message;
                    code_error = 1;
                }
            }
        }

        public List<string> TakeTableRow(string tag1, string tag2) // Вытаскивает сообщения из таблицы
        {
            List<string> rows = new List<string>();
            string pattern = // @$"{tag1}[А-Яф-я0-9](.*?){tag2}";
                @$"{tag1}[А БВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя](.*?){tag2}";
            responseBody = "<div> <span class='jknbj'>gbgb шгтищгщтгщш yhyy</span></div>";

            //    Console.WriteLine($"TakeTableRow {pattern}");
            string res = String.Empty;
            MatchCollection matches = Regex.Matches(responseBody, pattern);
            Console.WriteLine($"{responseBody}");
            int i = 0;
            foreach (Match match in matches)
            {
                res = match.Value; //.ToString();
                res = res.Replace($"{tag1}", "");
                res = res.Replace($"{tag2}", "");
                rows.Add(res + "\r\n");
                Console.WriteLine($"{i++}   {res}");
            }

            // for(;;){


            //     int start = responseBody.IndexOf(tag1);
            //     int end   = responseBody.IndexOf(tag2);

            //     Console.WriteLine($"{start}  {end} {end - start}");
            //     if((start == -1) & (end == -1)) break;

            //     if (end - start <= 0) break;

            //     responseBody = responseBody.Remove(start, end - start + tag2.Length);// + tag2.Length);
            //    // Console.WriteLine(responseBody);
            // }
            // // Console.WriteLine($"TakeTableRow {res}");

            return rows;
        }

        public void SaveFile()
        {
            //  Console.WriteLine(responseBody);
            // Console.WriteLine(responseBody.Length);
            // Regex regex = new Regex(
            //     @"[А-Яа-я ](.*?)"
            // );
            // MatchCollection matches = regex.Matches(responseBody);
            // string context = String.Empty;
            // foreach (Match match in matches)
            // {
            //     context += match.Value;
            // }

            File.WriteAllTextAsync(filePath, responseBody);
            //.AppendAllTextAsync(filePath, responseBody);//.Replace("\r\n","").Trim());
        }

        public void SaveFile(string filePath)
        {
            File.AppendAllTextAsync(filePath, responseBody);
        }

        public void SaveFile(string filePath, List<string> data_save)
        {
            //  for (int i = 0; i < data_save.Count; i++)
            File.WriteAllLines(filePath, data_save);
        }

        public void SaveFile(string filePath, string data_save)
        {
            //  for (int i = 0; i < data_save.Count; i++)
            File.WriteAllText(filePath, data_save);
        }

        public void FindLink()
        {
            var link = new List<string>();
            int i;
            int start = 0,
                end;
            string low = responseBody.ToLower();

            for (; ; )
            {
                i = low.IndexOf("href=\"http", start);
                if (i != -1)
                {
                    start = low.IndexOf('"', i) + 1;
                    end = low.IndexOf('"', start);
                    var str = low.Substring(start, end - start);
                    if (!href.Contains(str))
                        href.Add(str);
                    low = low.Remove(start, end - start);
                }
                else
                    break;
            }
        }

        public List<string> FindTag(string tag1, string tag2)
        {
            var link = new List<string>();
            int i = 0;
            int start = 0,
                end;
            string low = responseBody.ToLower().Replace("\r\n", "");

            for (; ; )
            {
                start = low.IndexOf(tag1);
                end = low.IndexOf(tag2);

                if (start > end)
                {
                    var t = start;
                    start = end;
                    end = t;
                }

                //   Console.WriteLine($"{low} ->   {start} - {end}");

                if ((start != -1) && (end != -1))
                {
                    var str = low.Substring(start + tag1.Length + 1, end - start - tag1.Length - 1);
                    link.Add(str); // + "\r\n");
                    low = low.Remove(start, end - start + tag2.Length);
                    i++;
                }
                else
                    break;
                //    Console.WriteLine($"----->>>>>{low}");
            }

            return link;
        }

        public string CopyTag(string tag1, string tag2)
        {
            int start = responseBody.IndexOf(tag1);
            int end = responseBody.IndexOf(tag2);

            return responseBody.Substring(start, end - start + tag2.Length);
        }

        public void FindLink(string pattern)
        {
            var link = new List<string>();
            int i;
            int start = 0,
                end;
            string low = responseBody.ToLower();

            for (; ; )
            {
                i = low.IndexOf(pattern, start);
                if (i != -1)
                {
                    start = low.IndexOf('"', i) + 1;
                    end = low.IndexOf('"', start);
                    var str = low.Substring(start, end - start);
                    if (!href.Contains(str))
                        href.Add(str);
                    low = low.Remove(start, end - start);
                }
                else
                    break;
            }
        }

        public void SaveLink(string filepath)
        {
            File.WriteAllLinesAsync(filepath, href);
        }

        public int ArrayPosition(string parse)
        {
            int indexOfString = 0;
            int index = 0;
            responseBody = responseBody.ToLower();

            indexOfString = responseBody.IndexOf(parse);
            index_tag.Add(indexOfString);

            for (; ; )
            {
                index = indexOfString + parse.Length + 1;
                indexOfString = responseBody.IndexOf(parse, index, responseBody.Length - index);

                if (indexOfString == -1)
                    break;

                index_tag.Add(indexOfString);
            }

            return index_tag.Count;
        }

        public void Print()
        {
            for (int i = 0; i < index_tag.Count; i++)
                Console.WriteLine(index_tag[i]);
        }

        public void Print(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);
        }

        protected override void Dispose(bool dispose)
        {
            if (!isDispose)
            {
                if (dispose)
                {
                    isDispose = true;
                }
                base.Dispose(dispose);
            }
        }

        ~HttpClientScraper()
        {
            Dispose(false);
        }

        public int CharCount(string str, char c)
        {
            int counter = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c) counter++;
            }

            return counter;
        }
    }
}
