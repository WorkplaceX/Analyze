using CsvHelper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class Program
    {
        public static string FolderName
        {
            get
            {
                Uri result = new Uri(typeof(Program).Assembly.CodeBase);
                result = new Uri(result, "../../../../");
                return result.AbsolutePath;
            }
        }

        static void Main(string[] args)
        {
            // Load wikipedia website into html string
            string url = "https://en.wikipedia.org";
            string urlCountry = "https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2";
            string html;
            using (WebClient client = new WebClient())
            {
                html = client.DownloadString(urlCountry);
            }
           
            // Parse html string
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Find table with first header column "Code"
            var table = doc.DocumentNode.Descendants("table").Where(item => item.Descendants("th").FirstOrDefault()?.InnerHtml == "Code\n").First();
            var rowList = table.Descendants("tr").Skip(1); // Skip header row
            List<Record> recordList = new List<Record>();
            foreach (var row in rowList)
            {
                var cellList = row.Descendants("td");
                string code = cellList.First().Descendants("span").First().WriteContentTo();
                string country = cellList.Skip(1).First().Descendants("a").First().WriteContentTo();
                string countryUrl = cellList.Skip(1).First().Descendants("a").First().Attributes.First().Value;
                string year = cellList.Skip(2).First().WriteContentTo();
                string ccTLD = cellList.Skip(3).First().Descendants("a").FirstOrDefault()?.WriteContentTo();
                string ccTLDUrl = cellList.Skip(3).First().Descendants("a").FirstOrDefault()?.Attributes.First().Value;
                string iso = cellList.Skip(4).First().Descendants("a").First().WriteContentTo();
                string isoUrl = cellList.Skip(4).First().Descendants("a").First().Attributes.First().Value;
                string notes = cellList.Skip(5).First().WriteContentTo();
                notes = Regex.Replace(notes, "<.*?>", String.Empty);
                notes = notes.Replace("\r", null).Replace("\n", null);
                recordList.Add(new Record() {
                    Code = code,
                    Country = country,
                    CountryUrl = url + countryUrl,
                    Year = year,
                    CcTLD = ccTLD,
                    CcTLDUrl = url + ccTLDUrl,
                    Iso = iso,
                    IsoUrl = isoUrl,
                    Notes = notes,
                });
            }

            // Convert recordList to csv
            string csv;
            using (var sw = new StringWriter())
            {
                var csvWriter = new CsvWriter(sw);
                csvWriter.WriteRecords(recordList);
                csv = sw.ToString();
            }

            Console.WriteLine();
            Console.WriteLine("Save to file Country.csv");

            // Write csv file
            File.WriteAllText(FolderName + @"Country\Country.csv", csv);
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        public class Record
        {
            public string Code { get; set; }

            public string Country { get; set; }

            public string CountryUrl { get; set; }

            public string Year { get; set; }

            public string CcTLD { get; set; }

            public string CcTLDUrl { get; set; }

            public string Iso { get; set; }

            public string IsoUrl { get; set; }

            public string Notes { get; set; }
        }
    }
}
