using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;

namespace Parse
{
    public static class UtilFramework
    {
        public static void Assert(bool value)
        {
            if (value == false)
            {
                throw new Exception("Assert!");
            }
        }
    }

    public static class Tree
    {
        public class Component
        {
            public Component(Component owner)
            {
                Owner = owner;
                if (owner != null)
                {
                    owner.List.Add(this);
                }
            }

            public readonly List<Component> List = new List<Component>();

            public readonly Component Owner;
        }
    }

    public static class Storage
    {
        public class Document : Tree.Component
        {
            public Document() 
                : base(null)
            {

            }
        }

        public class FileText : Tree.Component
        {
            public FileText(Document owner, string text)
                : base(owner)
            {
                Text = text;
            }

            public readonly string Text;

            /// <summary>
            /// Returns last token in file.
            /// </summary>
            /// <param name="indexOffset">If 1, last token before last token is returned.</param>
            public Token TokenLast(int indexOffset)
            {
                Token result = null;
                var list = List.OfType<Token>().ToList();
                if ((list.Count - 1) - indexOffset >= 0)
                {
                    result = list[(list.Count - 1) - indexOffset];
                }
                return result;
            }

            /// <summary>
            /// Returns last token in the file.
            /// </summary>
            public Token TokenLast()
            {
                return TokenLast(0);
            }
        }

        public class Token : Tree.Component
        {
            public Token(FileText owner, int indexStart, int indexEnd)
                : base(owner)
            {
                IndexStart = indexStart;
                IndexEnd = indexEnd;
            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Token()
                : base(null)
            {

            }

            public new FileText Owner => (FileText)base.Owner;

            /// <summary>
            /// Gets IsFactory. Returns true, if instance is used to create tokens.
            /// </summary>
            public bool IsFactory => Owner == null;

            /// <summary>
            /// Gets IndexStart. Token starts at this index position.
            /// </summary>
            public readonly int IndexStart;

            /// <summary>
            /// Gets IndexEnd. Token ends at this index position.
            /// </summary>
            public readonly int IndexEnd;

            /// <summary>
            /// Gets Length. This is the token length.
            /// </summary>
            public int Length
            {
                get
                {
                    return IndexEnd - IndexStart + 1;
                }
            }

            /// <summary>
            /// Gets Text. This is the text from IndexStart to IndexEnd.
            /// </summary>
            public string Text
            {
                get
                {
                    return Owner.Text.Substring(IndexStart, IndexEnd - IndexStart + 1);
                }
            }

            /// <summary>
            /// Returns token if text at possition matches.
            /// </summary>
            /// <param name="index">Returns new index position</param>
            public virtual Token Create(FileText owner, int index)
            {
                return null;
            }

            /// <summary>
            /// Returns true if tokenText matches.
            /// </summary>
            public static bool Create(FileText owner, int index, string tokenText)
            {
                bool result = false;
                if (index + tokenText.Length <= owner.Text.Length)
                {
                    if (owner.Text.Substring(index, tokenText.Length) == tokenText)
                    {
                        result = true;
                    }
                }
                return result;
            }

            /// <summary>
            /// Returns token if tokenText matches.
            /// </summary>
            public static TToken Create<TToken>(FileText owner, int index, Func<FileText, int, int, string, TToken> create, string tokenText) where TToken : Token
            {
                TToken result = null;
                if (Create(owner, index, tokenText))
                {
                    result = create(owner, index, index + tokenText.Length - 1, tokenText);
                    UtilFramework.Assert(result != null);
                }
                return result;
            }

            /// <summary>
            /// Returns token if tokenText matches.
            /// </summary>
            public static TToken Create<TToken>(FileText owner, int index, Func<FileText, int, int, string, TToken> create, params string[] tokenTextList) where TToken : Token
            {
                TToken result = null;
                foreach (string tokenText in tokenTextList)
                {
                    result = Create(owner, index, create, tokenText);
                    if (result != null)
                    {
                        break;
                    }
                }
                return result;
            }
        }
    }

    public static class MdToken
    {
        public class Document : Storage.Document
        {
            public Document(Storage.Document storageDocument)
                : base()
            {
                // Copy FileText components
                foreach (var item in storageDocument.List)
                {
                    if (item is Storage.FileText fileText)
                    {
                        new Storage.FileText(this, fileText.Text);
                    }
                }

                // Populate token factory
                List<Storage.Token> tokenFactoryList = new List<Storage.Token>();
                tokenFactoryList.Add(new Space());
                tokenFactoryList.Add(new NewLine());
                tokenFactoryList.Add(new Comment());
                tokenFactoryList.Add(new Header());

                // Loop over Document components
                foreach (var item in List)
                {
                    // Loop over FileText
                    if (item is Storage.FileText fileText)
                    {
                        Content content = null;
                        // Loop over each character
                        for (int index = 0; index < fileText.Text.Length; index++)
                        {
                            // Loop over each token factory.
                            Storage.Token token = null;
                            foreach (var tokenFactory in tokenFactoryList)
                            {
                                token = tokenFactory.Create(fileText, index);
                                if (token != null)
                                {
                                    UtilFramework.Assert(token.IndexStart <= token.IndexEnd);
                                    var tokenLastLast = fileText.TokenLast(1);
                                    if (tokenLastLast != null)
                                    {
                                        UtilFramework.Assert(tokenLastLast.IndexEnd + 1 == token.IndexStart);
                                    }
                                    UtilFramework.Assert(token.IndexEnd < fileText.Text.Length);
                                    index = index + token.IndexEnd - token.IndexStart; // Move index if token length greater one.
                                    content = null;
                                    break;
                                }
                            }
                            // Fill gap with content token.
                            if (token == null)
                            {
                                if (content == null)
                                {
                                    content = new Content(fileText, index, index);
                                }
                                else
                                {
                                    UtilFramework.Assert(fileText.List.Remove(content));
                                    content = new Content(fileText, content.IndexStart, index);
                                }
                            }
                        }
                    }
                }
            }
        }

        public class Space : Storage.Token
        {
            public Space(Storage.FileText owner, int indexStart, int indexEnd)
                : base(owner, indexStart, indexEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Space()
                : base()
            {

            }

            public override Storage.Token Create(Storage.FileText owner, int index)
            {
                Space result = null;
                int indexEnd;
                for (indexEnd = index; indexEnd < owner.Text.Length; indexEnd++)
                {
                    if (owner.Text[indexEnd] != ' ')
                    {
                        break;
                    }
                }
                indexEnd -= 1;
                if (index <= indexEnd)
                {
                    result = new Space(owner, index, indexEnd);
                }
                return result;
            }
        }

        public class NewLine : Storage.Token
        {
            public NewLine(Storage.FileText owner, int indexStart, int indexEnd) 
                : base(owner, indexStart, indexEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public NewLine() 
                : base()
            {

            }

            public override Storage.Token Create(Storage.FileText owner, int index)
            {
                return Create(owner, index, (owner, indexStart, indexEnd, tokenText) => new NewLine(owner, indexStart, indexEnd), "\r\n", "\r", "\n");
            }
        }

        public class Comment : Storage.Token
        {
            public Comment(Storage.FileText owner, int indexStart, int indexEnd, bool isEnd)
                : base(owner, indexStart, indexEnd)
            {
                IsEnd = isEnd;
            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Comment() 
                : base()
            {

            }

            /// <summary>
            /// Gets IsEnd. If true, this is the end comment token.
            /// </summary>
            public readonly bool IsEnd;

            public override Storage.Token Create(Storage.FileText owner, int index)
            {
                return Create(owner, index, (owner, indexStart, indexEnd, tokenText) => new Comment(owner, indexStart, indexEnd, tokenText == "-->"), "<!--", "-->");
            }
        }

        public class Header : Storage.Token
        {
            public Header(Storage.FileText owner, int indexStart, int indexEnd)
                : base(owner, indexStart, indexEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Header() 
                : base()
            {

            }

            public override Storage.Token Create(Storage.FileText owner, int index)
            {
                Header result = null;
                if (Create(owner, index, "# "))
                {
                    var tokenLast = owner.TokenLast();
                    if (tokenLast == null || tokenLast is NewLine)
                    {
                        result = new Header(owner, index, index);
                    }
                    else
                    {
                        var tokenLastLast = owner.TokenLast(1);
                        if (tokenLast is Space)
                        {
                            if (tokenLastLast == null || tokenLastLast is NewLine)
                            {
                                result = new Header(owner, index, index);
                            }
                        }
                    }
                }
                return result;
            }
        }

        public class Content : Storage.Token
        {
            public Content(Storage.FileText owner, int indexStart, int indexEnd)
                : base(owner, indexStart, indexEnd)
            {

            }
        }
    }

    public static class Md
    {
        public class Document : Tree.Component
        {
            public Document(MdToken.Document mdTokenDocument) 
                : base(null)
            {
                var elementFactoryList = new List<Element>();
                List.Add(new Comment());

                // Loop through document components
                foreach (var item in mdTokenDocument.List)
                {
                    // Loop through FileText components
                    if (item is Storage.FileText fileText)
                    {
                        var page = new Page(this);
                        // Loop through token list
                        var tokenList = fileText.List.OfType<Storage.Token>().ToList();
                        for (int i = 0; i < tokenList.Count; i++)
                        {
                            var token = tokenList[i];
                            // Loop through element factory
                            Element element = null;
                            foreach (var elementFactory in elementFactoryList)
                            {
                                element = elementFactory.Create(page, token);
                                if (element != null)
                                {
                                    break;
                                }
                            }
                            // Fill the gap
                            if (element == null)
                            {

                            }
                        }
                    }
                }
            }
        }

        public class Page : Tree.Component
        {
            public Page(Document owner) 
                : base(owner)
            {

            }
        }

        public class Element : Tree.Component
        {
            public Element(Tree.Component owner)
                : base(owner)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Element()
                : base(null)
            {

            }

            public bool IsFactory => Owner == null;

            public virtual Element Create(Tree.Component owner, Storage.Token token)
            {
                return null;
            }
        }

        public class Comment : Element
        {
            public Comment(Page owner, string text) 
                : base(owner)
            {
                Text = text;
            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Comment() 
                : base()
            {

            }

            public readonly string Text;
        }

        public class Content : Element
        {
            public Content(Page owner, string text)
                : base(owner)
            {
                Text = text;
            }

            public readonly string Text;
        }
    }

    public static class Cms
    {
        public class Document : Tree.Component
        {
            public Document()
                : base(null)
            {

            }
        }

        public class Page
        {

        }

        public class Paragraph
        {
            public string Title;
        }

        public class Text
        {

        }

        public class Link
        {

        }
    }

    public static class Html
    {
        public class Document : Tree.Component
        {
            public Document()
                : base(null)
            {

            }
        }

        public class Page
        {

        }

        public class Header
        {

        }

        public class Paragraph
        {

        }

        public class Text
        {

        }
    }
}