using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

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
        /// <summary>
        /// Component of tree structure.
        /// </summary>
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

            /// <summary>
            /// Returns last component.
            /// </summary>
            /// <param name="indexOffset">If 1, last component befory last component is returned.</param>
            public T Last<T>(int indexOffset) where T : Component
            {
                T result = null;
                var list = List.OfType<T>().ToList();
                if ((list.Count - 1) - indexOffset >= 0)
                {
                    result = list[(list.Count - 1) - indexOffset];
                }
                return result;
            }

            /// <summary>
            /// Returns last child component.
            /// </summary>
            public T Last<T>() where T : Component
            {
                return Last<T>(0);
            }

            /// <summary>
            /// Returns next sibling component.
            /// </summary>
            public Component Next()
            {
                Component result = null;
                var indexNext = Owner.List.IndexOf(this) + 1;
                if (Owner.List.Count > indexNext)
                {
                    result = Owner.List[indexNext];
                }
                return result;
            }
        }
    }

    public static class Storage
    {
        /// <summary>
        /// Storage root component.
        /// </summary>
        public class Document : Tree.Component
        {
            public Document() 
                : base(null)
            {

            }
        }

        /// <summary>
        /// A text file.
        /// </summary>
        public class FileText : Tree.Component
        {
            public FileText(Document owner, string text)
                : base(owner)
            {
                Text = text;
            }

            public readonly string Text;
        }

        /// <summary>
        /// Token emitted by lexer. Lexer emits a list of tokens (not a hierarchical structure). See also: https://www.youtube.com/watch?v=TG0qRDrUPpA and
        /// https://www.youtube.com/watch?v=9-EYWLbmiG0
        /// </summary>
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

    public static class MdLexer
    {
        /// <summary>
        /// MdLexer root component.
        /// </summary>
        public class Document : Storage.Document
        {
            /// <summary>
            /// Constructor. Lexer emmiting tokens.
            /// </summary>
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
                tokenFactoryList.Add(new Content());

                // Loop over Document components
                foreach (var item in List)
                {
                    // Loop over FileText
                    if (item is Storage.FileText fileText)
                    {
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
                                    var tokenLastLast = fileText.Last<Storage.Token>(1);
                                    if (tokenLastLast != null)
                                    {
                                        UtilFramework.Assert(tokenLastLast.IndexEnd + 1 == token.IndexStart);
                                    }
                                    UtilFramework.Assert(token.IndexEnd < fileText.Text.Length);
                                    index = token.IndexEnd; // Move index if token length greater one.
                                    break;
                                }
                            }
                            UtilFramework.Assert(token != null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Space token. One or many space characters.
        /// </summary>
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

        /// <summary>
        /// New line token.
        /// </summary>
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

        /// <summary>
        /// Comment begin or comment end token.
        /// </summary>
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

        /// <summary>
        /// Header token.
        /// </summary>
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
                    var tokenLast = owner.Last<Storage.Token>();
                    if (tokenLast == null || tokenLast is NewLine)
                    {
                        result = new Header(owner, index, index);
                    }
                    else
                    {
                        var tokenLastLast = owner.Last<Storage.Token>(1);
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

        /// <summary>
        /// Content token to fill the gaps.
        /// </summary>
        public class Content : Storage.Token
        {
            public Content(Storage.FileText owner, int indexStart, int indexEnd)
                : base(owner, indexStart, indexEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Content()
            {

            }

            public override Storage.Token Create(Storage.FileText owner, int index)
            {
                Content result;
                if (owner.Last<Storage.Token>() is Content content)
                {
                    UtilFramework.Assert(owner.List.Remove(content));
                    result = new Content(owner, content.IndexStart, index);
                }
                else
                {
                    result = new Content(owner, index, index);
                }
                return result;
            }
        }
    }

    public static class MdParser
    {
        /// <summary>
        /// Md root component.
        /// </summary>
        public class Document : Tree.Component
        {
            /// <summary>
            /// Constructor. Token parser.
            /// </summary>
            public Document(MdLexer.Document mdLexerDocument) 
                : base(null)
            {
                var syntaxFactoryList = new List<Syntax>();
                syntaxFactoryList.Add(new Comment());
                syntaxFactoryList.Add(new Header());
                syntaxFactoryList.Add(new Content());

                // Loop through document components
                foreach (var item in mdLexerDocument.List)
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
                            // Loop through syntax factory
                            Syntax syntax = null;
                            foreach (var syntaxFactory in syntaxFactoryList)
                            {
                                syntax = syntaxFactory.Create(page, token);
                                if (syntax != null)
                                {
                                    break;
                                }
                            }
                            UtilFramework.Assert(syntax != null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Page with md syntax.
        /// </summary>
        public class Page : Tree.Component
        {
            public Page(Document owner) 
                : base(owner)
            {

            }
        }

        /// <summary>
        /// Md syntax tree. Syntax element is created based on one or more tokens.
        /// </summary>
        public class Syntax : Tree.Component
        {
            public Syntax(Tree.Component owner)
                : base(owner)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Syntax()
                : base(null)
            {

            }

            /// <summary>
            /// Gets IsFactory. If true, syntax element is used as factory.
            /// </summary>
            public bool IsFactory => Owner == null;

            /// <summary>
            /// Create syntax element.
            /// </summary>
            /// <param name="owner">Page or syntax element.</param>
            /// <param name="token">Token emitted by lexer.</param>
            public virtual Syntax Create(Tree.Component owner, Storage.Token token)
            {
                return null;
            }
        }

        /// <summary>
        /// Comment syntax.
        /// </summary>
        public class Comment : Syntax
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

            public override Syntax Create(Tree.Component owner, Storage.Token token)
            {
                Comment result = null;
                if (token is MdLexer.Comment comment && comment.IsEnd == false)
                {
                    StringBuilder text = new StringBuilder();
                    while ((token= (Storage.Token)token.Next()) != null)
                    {
                        if (token is MdLexer.Comment commentNext && commentNext.IsEnd)
                        {
                            result = new Comment((Page)owner, text.ToString());
                            break;
                        }
                        text.Append(token.Text);
                    }
                }
                return result;
            }
        }

        public class Header : Syntax
        {
            public Header(Page owner) 
                : base(owner)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Header() 
                : base(null)
            {

            }

            public override Syntax Create(Tree.Component owner, Storage.Token token)
            {
                Header result = null;
                if (token is MdLexer.Header header)
                {
                    // TODO Level0, Level1 Syntax
                }
                return result;
            }
        }

        /// <summary>
        /// Content syntax to fill the gaps.
        /// </summary>
        public class Content : Syntax
        {
            public Content(Tree.Component owner, string text)
                : base(owner)
            {
                Text = text;
            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Content()
            {

            }

            public readonly string Text;

            public override Syntax Create(Tree.Component owner, Storage.Token token)
            {
                Content result;
                if (owner.Last<Syntax>() is Content content)
                {
                    UtilFramework.Assert(owner.List.Remove(content));
                    result = new Content(owner, content.Text + token.Text);
                }
                else
                {
                    result = new Content(owner, token.Text);
                }
                return result;
            }
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