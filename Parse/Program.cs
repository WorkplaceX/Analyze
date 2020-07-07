using System;
using System.Linq;

namespace Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string text = @"
            # Hello World
            This is the <!-- My comment
            # Comment
            --> paragraph
            ";

            // text = "7\r\n# Rk  <!--";

            var storageDocument = new Storage.Document();
            new Storage.FileText(storageDocument, text);

            var mdLexerDocument = new MdLexer.Document(storageDocument);

            var d = mdLexerDocument.List[0].List;

            var mdParserDocument = new MdParser.Document(mdLexerDocument);

            var md = mdParserDocument.List[0].List.ToList();

        }
    }
}
