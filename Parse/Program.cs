using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            {
                var storageDocument = new Storage.Document();
                string text = "# Hello  123 <!-- Comment\r\n -->";

                text = @"
            # Hello World
            This is the <!-- My comment
            # Comment
            --> paragraph
            ";

                text = "# Title\r\n# Tit<!---->le2";

                new Storage.FileText(storageDocument, text);

                var lexerDocument = new MarkdownLexer.Document(storageDocument);
                var d = lexerDocument.List[0].List;

                var document = new Markdown.Document(lexerDocument);
                var d2 = document.List[0].List;

            }
        }
    }
}
