using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net;

namespace WEBScraping_c
{
    public class Scraper : Component
{
    public string webpageString = String.Empty;
    string filePath = "contentPage1.html";
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

}