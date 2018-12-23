using CsvHelper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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

        public static string UrlWikipedia = "https://en.wikipedia.org";

        static void Main(string[] args)
        {
            // Load wikipedia website into html string
            string urlAircraft = "https://en.wikipedia.org/wiki/List_of_ICAO_aircraft_type_designators";
            string html;
            using (WebClient client = new WebClient())
            {
                html = client.DownloadString(urlAircraft);
            }
           
            // Parse html string
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Find table with first header column "Code"
            var table = doc.DocumentNode.Descendants("table").Where(item => item.Descendants("th").FirstOrDefault().InnerHtml.StartsWith("ICAO")).First();
            var rowList = table.Descendants("tr").Skip(1); // Skip header row
            List<Record> recordList = new List<Record>();
            foreach (var row in rowList)
            {
                var cellList = row.Descendants("td");
                string icaoCode = cellList.First().InnerHtml;
                string iataCode = cellList.Skip(1).First().InnerHtml;
                string model = cellList.Skip(2).First().Descendants("a").First().InnerHtml;
                string modelUrl = UrlWikipedia + cellList.Skip(2).First().Descendants("a").First().Attributes.First().Value;
                ModelImageUrl(model, modelUrl, out string modelTitle, out string modelImageUrl);

                iataCode = Regex.Replace(iataCode, "<.*?>", String.Empty); // Clean up like "[to be determined]"
                iataCode = WebUtility.HtmlDecode(iataCode);

                recordList.Add(new Record() {
                    IcaoCode = icaoCode,
                    IataCode = iataCode,
                    Model = model,
                    ModelUrl = modelUrl,
                    ModelTitle = modelTitle,
                    ModelImageUrl = modelImageUrl,
                });

                Console.WriteLine(model);
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
            Console.WriteLine("Save file Aircraft.csv");

            // Write csv file
            File.WriteAllText(FolderName + @"Aircraft\Aircraft.csv", csv);

            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        public static void ModelImageUrl(string model, string modelUrl, out string modelTitle, out string modelImageUrl)
        {
            modelTitle = null;
            modelImageUrl = null;
            try
            {
                modelUrl = modelUrl.Substring((UrlWikipedia + "/wiki/").Length);
                if (modelUrl.Contains("#"))
                {
                    modelUrl = modelUrl.Substring(0, modelUrl.IndexOf("#"));
                }

                // Page redirect
                string urlApiRedirect = "https://en.wikipedia.org/w/api.php?action=query&titles={0}&redirects&format=xml&formatversion=2";
                urlApiRedirect = string.Format(urlApiRedirect, modelUrl);
                string xmlApiRedirect;
                using (WebClient client = new WebClient())
                {
                    xmlApiRedirect = client.DownloadString(urlApiRedirect);
                }
                XElement xElement = XElement.Parse(xmlApiRedirect);
                modelTitle = xElement.Descendants("page").Single().Attribute("title").Value;

                // Image
                string urlApiImage = "https://en.wikipedia.org/w/api.php?action=query&titles={0}&prop=pageimages&format=xml&pithumbsize=256";
                urlApiImage = string.Format(urlApiImage, WebUtility.UrlEncode(modelTitle));

                string xmlApiImage;
                using (WebClient client = new WebClient())
                {
                    xmlApiImage = client.DownloadString(urlApiImage);
                }
                xElement = XElement.Parse(xmlApiImage);
                modelImageUrl = xElement.Descendants("thumbnail").Single().Attribute("source").Value;
            }
            catch
            {
                Console.WriteLine("No image for model! ({0})", model);
            }
        }

        public class Record
        {
            public string IcaoCode { get; set; }

            public string IataCode { get; set; }

            public string Model { get; set; }

            public string ModelUrl { get; set; }

            public string ModelTitle { get; set; }

            public string ModelImageUrl { get; set; }
        }
    }
}
