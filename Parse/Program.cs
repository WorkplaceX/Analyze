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
            # Hello2 
            ";

                // text = "# T<!-- # T -->itle\r\n# Title2";



                // text = "# Title\r\n# Tit<!-- # D -->le2";

                text = "# Abc [Node.js](https://nodejs.org/en/) (LTS Version)\r # Title2";
                

                new Storage.FileText(storageDocument, text);

                var lexerDocument = new MarkdownLexer.Document(storageDocument);
                var d = lexerDocument.List[0].List;

                var markDownDocument = new Markdown.Document(lexerDocument);
                var d2 = (Markdown.Node)markDownDocument.List[0].List[0];

                var x = lexerDocument.TextTree;
                var x2 = markDownDocument.TextTree;

            }
        }
    }
}
