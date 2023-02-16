// See https://aka.ms/new-console-template for more information
//You can use the
//  following code for web scraping in C#:

using System;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;

//using Extension;


Container conteiner = new Container();

using (WEBScraping_c.HttpClientScraper hcs = new WEBScraping_c.HttpClientScraper())
{
    conteiner.Add(hcs);
    await hcs.ScrapePage("https://hh.ru/vacancy/76124956");

    // if (hcs.code_error == 0)
    //     hcs.SaveFile("index.html");

    Console.WriteLine("End write of file");

    hcs.FindLink("href=\"https://hh.ru/vacancy/");
    hcs.SaveLink("links.link");
    //    Console.WriteLine(hcs.ArrayPosition(@"div class=""serp-item"" data-qa="));

    //      hcs.SaveFile("span.html", hcs.CopyTag("<head>", "</head>"));
    //     hcs.Print(hcs.FindTag("<span", "</span>"));
    string str = "msakcvfklvmmvmggg,tg,,,geg";
    var cn = str.CharCount('g');
    Console.WriteLine($"end --->>> {cn} ");

    string it = "fdsdcdcdc";
    string[] im = { "dcdc", "fd", "saa" };
    var b = it.Ins(im);
    Console.WriteLine($"end --->>> {b} ");

    var k = Extension.cyrilic.Contains('"');
    Console.WriteLine($"end --->>> {k} ");
}

public static class Extension
{
    static string pattern1 = "ЙФЯЦЫЧУВСКАМЕПИНРТГОЬШЛБЩДЮЗЖЭХЪ";
    static string pattern2 = "йфяцычувскамепинртгоьшлбщдюзжэхъЁё";
    static string pattern3 = "`1234567890-=~!@#$%^&*()_+<>?{}:;[].,'\"|/";
    public static string cyrilic = pattern1 + pattern2 + pattern3;

    public static bool Ins<T>(this T item, params T[] items)
    {
        if (items == null)
            throw new ArgumentNullException("items");

        return items.Contains(item);
    }

    public static int CharCount(this string str, char c)
    {
        int counter = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == c)
                counter++;
        }
        return counter;
    }

    public static bool Contains(this string str, char c)
    {
        for (int i = 0; i < str.Length; i++)
            if (str[i] == c)
                return true;
        return false;
    }
}
