// See https://aka.ms/new-console-template for more information
//You can use the
//  following code for web scraping in C#:

using System;
using System.Net;
using System.IO;
using System.ComponentModel;

Container conteiner = new Container();

// using (Scraper sp = new Scraper())
// {
//     conteiner.Add(sp);
//     sp.ScrapePage("https://google.com");
//     sp.SaveFile();
// }

using (HttpClientScraper hcs = new HttpClientScraper())
{
    conteiner.Add(hcs);
    await hcs.ScrapePage("https://yandex.ru");

    if (hcs.code_error == 0)
        hcs.SaveFile();

    Console.WriteLine("End write of file");
    hcs.Parser("href");

    Console.WriteLine("Print");
    hcs.Print();

}

public class Scraper : Component
{
    public string webpageString = String.Empty;
    string filePath = "contentPage1.txt";
    bool isDispose;
    public int code_error { get; set; }

    public void ScrapePage(string url)
    {
        code_error = 0;
        // Create a new webclient instance
        try
        {
            WebClient webClient = new WebClient();

            // Download the webpage
            Stream webpageStream = webClient.OpenRead(url);

            // Read the webpage into a streamreader
            StreamReader webpageReader = new StreamReader(webpageStream);

            // Read the webpage into a string
            webpageString = webpageReader.ReadToEnd();

            // Close the streamreader
            webpageReader.Close();
        }
        catch (WebException webEx)
        {
            Console.WriteLine("Модуль Scraper -> Ошибка !!!");
            Console.WriteLine(webEx.ToString());
            if (webEx.Status == WebExceptionStatus.ConnectFailure)
            {
                Console.WriteLine(
                    "Are you behind a firewall?  If so, go through the proxy server."
                );
            }

            code_error = 1;
        }

        //    File.AppendAllTextAsync(filePath, webpageString);

        // Find the strings that we are looking for
        // string stringToFind = "script";

        // Print out the index of the string
    }

    public void SaveFile()
    {
        Console.WriteLine(webpageString);
        File.AppendAllTextAsync(filePath, webpageString);
    }

    public string[] Parser(string stringToFind)
    {
        int indexOfString = webpageString.IndexOf(stringToFind);
        Console.WriteLine("The string was found at index: " + indexOfString);

        return new string[3];
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

    ~Scraper()
    {
        Dispose(false);
    }
}

public class HttpClientScraper : Component
{
    public string responseBody = String.Empty;
    string filePath = "contentPageYandex.txt";
    bool isDispose;
    static HttpClient client = new HttpClient();

    List<int> href = new List<int>();

    public int code_error = 0;

    public async Task ScrapePage(string URI)
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
            Console.WriteLine("\n Произошло исключение в модуле HttpClientScraper!");
            Console.WriteLine("Сообщение об ОШИБКИ !!! :{0} ", e.Message);

            responseBody = e.Message;
            code_error = 1;
        }
    }

    public void SaveFile()
    {
        Console.WriteLine(responseBody);
        File.AppendAllTextAsync(filePath, responseBody);
    }

    public void SaveFile(string filePath)
    {
        File.AppendAllTextAsync(filePath, responseBody);
    }

    public string[] Parser(string parse)
    {
        int indexOfString = 0;
        
        for(;;)
        {
            indexOfString = responseBody.IndexOf(parse);
            if (indexOfString == -1) break;
            responseBody = responseBody.Remove(indexOfString, 4);
            Console.WriteLine($"{indexOfString}");
            href.Add(indexOfString);
        }

        // Console.WriteLine("The string was found at index: " + indexOfString);

        return new string[3];
    }

    public void Print()
    {
        for (int i = 0; i < href.Count; i++)
            Console.WriteLine(href[i]);
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
}
