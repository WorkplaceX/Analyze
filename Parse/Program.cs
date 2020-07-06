using System;

namespace Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string text = @"
            # Hello World
            This is the <!--
            --> paragraph
            ";

            // text = "7\r\n# R  <!--";

            var storageDocument = new Storage.Document();
            new Storage.FileText(storageDocument, text);

            var mdTokenDocument = new MdToken.Document(storageDocument);

            var d = mdTokenDocument.List[0].List;

            var mdDocument = new Md.Document(mdTokenDocument);

        }
    }
}
