// See https://aka.ms/new-console-template for more information
//You can use the
//  following code for web scraping in C#:

using System;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;


    Container conteiner = new Container();
     Console.WriteLine("End write of file");

        using (WEBScraping_c.HttpClientScraper hcs = new WEBScraping_c.HttpClientScraper())
        {
            conteiner.Add(hcs);
            await hcs.ScrapePage("https://hh.ru/vacancy/76124956");

            // if (hcs.code_error == 0)
            //     hcs.SaveFile("index.html");

            Console.WriteLine("End write of file");

            hcs.FindLink("href=\"https://hh.ru/vacancy/");
            hcs.SaveLink("links.link");
            Console.WriteLine(hcs.ArrayPosition(@"div class=""serp-item"" data-qa="));

            hcs.SaveFile("span.html", hcs.CopyTag("<head>", "</head>"));
       //     hcs.Print(hcs.FindTag("<span", "</span>"));
        }

    



