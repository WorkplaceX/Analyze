namespace Parse
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

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
        [DebuggerDisplay("{TextGet()}")]
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

            /// <summary>
            /// Gets Owner. Owner of this component.
            /// </summary>
            public Component Owner { get; private set; }

            public readonly List<Component> List = new List<Component>();

            protected virtual string TextGet()
            {
                return GetType().Name;
            }

            private void TextTreeGet(int level, StringBuilder result)
            {
                for (int i = 0; i < level; i++)
                {
                    result.Append("    ");
                }
                result.Append("- ");
                result.Append(TextGet());
                result.AppendLine();
                foreach (var item in List)
                {
                    item.TextTreeGet(level + 1, result);
                }
            }

            /// <summary>
            /// Gets TextTree. Hierarchical representation.
            /// </summary>
            public string TextTree
            {
                get
                {
                    var result = new StringBuilder();
                    TextTreeGet(0, result);
                    return result.ToString();
                }
            }

            /// <summary>
            /// Gets Index. This is the index of this component in owners list.
            /// </summary>
            public int Index
            {
                get
                {
                    int result = -1;
                    if (Owner != null)
                    {
                        result = Owner.List.IndexOf(this);
                    }
                    return result;
                }
            }

            private void ListAllGet(Component component, List<Component> result)
            {
                if (component != this)
                {
                    result.Add(component);
                }
                foreach (var item in component.List)
                {
                    ListAllGet(item, result);
                }
            }

            /// <summary>
            /// Returns all components (hierarchical).
            /// </summary>
            public List<Component> ListAll
            {
                get
                {
                    List<Component> result = new List<Component>();
                    ListAllGet(this, result);
                    return result;
                }
            }

            /// <summary>
            /// Returns last child component.
            /// </summary>
            public Component Last
            {
                get
                {
                    Component result = null;
                    if (List.Count - 1 >= 0)
                    {
                        result = List[List.Count - 1];
                    }
                    return result;
                }
            }

            /// <summary>
            /// Returns last child component (hierarchical).
            /// </summary>
            public Component LastAll
            {
                get
                {
                    Component result = Last;
                    if (result?.List.Count > 0)
                    {
                        result = result.ListAll.Last();
                    }
                    return result;
                }
            }

            /// <summary>
            /// Gets Next. This is the next sibling component.
            /// </summary>
            public Component Next
            {
                get
                {
                    Component result = null;
                    if (Owner != null)
                    {
                        var index = Owner.List.IndexOf(this) + 1;
                        if (index < Owner.List.Count)
                        {
                            result = Owner.List[index];
                        }
                    }
                   return result;
                }
            }

            /// <summary>
            /// Gets NextAll. 
            /// </summary>
            public Component NextAll
            {
                get
                {
                    Component result;
                    if (List.Count > 0)
                    {
                        result = List[0];
                    }
                    else
                    {
                        result = Next;
                    }
                    return result;
                }
            }


            /// <summary>
            /// Gets Previous. This is the previous sibling component.
            /// </summary>
            public Component Previous
            {
                get
                {
                    Component result = null;
                    if (Owner != null)
                    {
                        var index = Owner.List.IndexOf(this) - 1;
                        if (index >= 0)
                        {
                            result = Owner.List[index];
                        }
                    }
                    return result;
                }
            }

            /// <summary>
            /// Gets Previous. This is the previous sibling component (hierarchical).
            /// </summary>
            public Component PreviousAll
            {
                get
                {
                    var result = Previous;
                    if (result?.List.Count > 0)
                    {
                        result = result.ListAll.Last();
                    }
                    return result;
                }
            }

            /// <summary>
            /// Removes this component from owners list.
            /// </summary>
            public void Remove()
            {
                if (Owner != null)
                {
                    UtilFramework.Assert(Owner.List.Remove(this));
                    Owner = null;
                }
            }
        }

        /// <summary>
        /// Syntax tree.
        /// </summary>
        public class Syntax : Component
        {
            /// <summary>
            /// Constructor for syntax component with reference range.
            /// </summary>
            public Syntax(Component owner, Syntax referenceBegin, Syntax referenceEnd) 
                : base(owner)
            {
                UtilFramework.Assert(!(referenceBegin == null ^ referenceEnd == null));
                ReferenceBegin = referenceBegin;
                ReferenceEnd = referenceEnd;
            }

            /// <summary>
            /// Constructor for syntax component with reference.
            /// </summary>
            public Syntax(Component owner, Syntax reference) 
                : this(owner, reference, reference)
            {

            }

            /// <summary>
            /// Constructor for syntax component without reference.
            /// </summary>
            public Syntax(Component owner) 
                : this(owner, null, null)
            {

            }

            /// <summary>
            /// Constructor for syntax component factory.
            /// </summary>
            public Syntax() 
                : this(null, null, null)
            {
                IsFactory = true;
            }

            public readonly bool IsFactory;

            public new Syntax NextAll => (Syntax)base.NextAll;

            public readonly Syntax ReferenceBegin;

            public readonly Syntax ReferenceEnd;

            public Syntax ReferenceEndAll
            {
                get
                {
                    var result = ReferenceEnd;
                    if (List.Count > 0)
                    {
                        result = ((Syntax)ListAll.Last()).ReferenceEnd;
                    }
                    return result;
                }
            }

            /// <summary>
            /// Create syntax tree.
            /// </summary>
            /// <param name="owner">Owner of new syntax tree.</param>
            /// <param name="referenceList">Reference list to walk through to create new syntax components.</param>
            /// <param name="reference">Reference component in referenceList to start with or null to start with first component.</param>
            /// <param name="syntaxFactoryList">Factories to create syntax components.</param>
            /// <param name="syntaxFactoryStopList">If referenceList hits one of this factories, walk through stops.</param>
            public static void CreateSyntaxTree(Component owner, List<Component> referenceList, Syntax reference, List<Syntax> syntaxFactoryList, List<Syntax> syntaxFactoryStopList)
            {
                int referenceIndex = 0;
                if (reference != null)
                {
                    UtilFramework.Assert(referenceList.Contains(reference));
                    referenceIndex = referenceList.IndexOf(reference);
                }

                // Loop through reference list starting at item.
                for (int index = referenceIndex; index < referenceList.Count; index++)
                {
                    Syntax item = (Syntax)referenceList[index];

                    // Create syntax from factory
                    Syntax syntax = null;
                    // Syntax factory used to create syntax
                    Syntax syntaxFactory = null;
                    foreach (var syntaxFactoryItem in syntaxFactoryList)
                    {
                        // Create before
                        int syntaxLength = owner.List.Count;
                        Syntax syntaxLast = (Syntax)owner.Last;

                        // Create
                        syntaxFactoryItem.CreateSyntax(owner, referenceList, item, syntaxFactoryList);

                        // Create after
                        int syntaxLengthNew = owner.List.Count;
                        Syntax syntaxLastNew = (Syntax)owner.Last;

                        // Create result get
                        UtilFramework.Assert(syntaxLengthNew - syntaxLength == 0 || syntaxLengthNew - syntaxLength == 1); // Zero or one token added.
                        if (syntaxLengthNew - syntaxLength == 1)
                        {
                            syntax = syntaxLastNew;
                        }
                        else
                        {
                            if (syntaxLast != syntaxLastNew)
                            {
                                syntax = syntaxLastNew;
                            }
                        }

                        // Create result
                        if (syntax != null)
                        {
                            UtilFramework.Assert(syntax.GetType() == syntaxFactoryItem.GetType());
                            syntaxFactory = syntaxFactoryItem;
                            break;
                        }
                    }

                    // Created syntax
                    UtilFramework.Assert(syntax != null);
                    var syntaxPrevious = (Syntax)syntax.PreviousAll;
                    if (syntaxPrevious != null)
                    {
                        // Make sure every reference component is covered.
                        UtilFramework.Assert(syntaxPrevious.ReferenceEnd.Index + 1 == syntax.ReferenceBegin.Index);
                    }
                    int referenceEndIndex = referenceList.IndexOf(syntax.ReferenceEndAll);
                    UtilFramework.Assert(index <= referenceEndIndex && referenceEndIndex < referenceList.Count);
                    // Move index to new end
                    index = referenceEndIndex;
                    if (syntaxFactoryStopList.Contains(syntaxFactory))
                    {
                        syntax.Remove();
                        break;
                    }
                }
            }

            public static void CreateSyntaxTree(Component owner, List<Component> referenceList, Syntax reference, List<Syntax> syntaxFactoryList)
            {
                CreateSyntaxTree(owner, referenceList, reference, syntaxFactoryList, new List<Syntax>());
            }

            /// <summary>
            /// Create one new Syntax component or replace owners last Syntax component.
            /// </summary>
            public virtual void CreateSyntax(Component owner, List<Component> referenceList, Syntax reference, List<Syntax> syntaxFactoryList)
            {

            }
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

        public class FileText : Tree.Syntax
        {
            public FileText(Document owner, string text)
                : base(owner)
            {
                Text = text;
                for (int index = 0; index < Text.Length; index++)
                {
                    new Character(this, index);
                }
            }

            public readonly string Text;
        }

        public class Character : Tree.Syntax
        {
            public Character(FileText owner, int textIndex)
                : base(owner)
            {
                TextIndex = textIndex;
            }

            public new FileText Owner => (FileText)base.Owner;

            public readonly int TextIndex;

            public char Text
            {
                get
                {
                    return Owner.Text[TextIndex];
                }
            }
        }
    }

    public static class MarkdownLexer
    {
        public class Document : Tree.Component
        {
            public Document(Storage.Document storageDocument)
                : base(null)
            {
                List<Tree.Syntax> syntaxFactoryList = new List<Tree.Syntax>();
                syntaxFactoryList.Add(new FileText());
                syntaxFactoryList.Add(new NewLine());
                syntaxFactoryList.Add(new Comment());
                syntaxFactoryList.Add(new Space());
                syntaxFactoryList.Add(new Header());
                syntaxFactoryList.Add(new Content());

                Tree.Syntax.CreateSyntaxTree(this, storageDocument.ListAll, null, syntaxFactoryList);
            }
        }

        public class FileText : Tree.Syntax
        {
            public FileText(Document owner, Storage.FileText reference)
                : base(owner, reference)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public FileText() 
                : base()
            {

            }

            public override void CreateSyntax(Tree.Component owner, List<Tree.Component> referenceList, Tree.Syntax reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (reference is Storage.FileText referenceFileText)
                {
                    var fileText = new FileText((Document)owner, referenceFileText);
                    CreateSyntaxTree(fileText, referenceList, reference.NextAll, syntaxFactoryList);
                }
            }
        }

        public class Token : Tree.Syntax
        {
            public Token(FileText owner, Storage.Character referenceBegin, Storage.Character referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            public new FileText Owner => (FileText)base.Owner;

            public new Storage.Character ReferenceBegin => (Storage.Character)base.ReferenceBegin;

            public new Storage.Character ReferenceEnd => (Storage.Character)base.ReferenceEnd;

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Token()
                : base()
            {

            }

            public new Token Next
            {
                get
                {
                    return (Token)base.Next;
                }
            }

            protected override string TextGet()
            {
                string result = "Token." + GetType().Name;
                string text = Text;
                if (!string.IsNullOrEmpty(text.Replace(" ", "").Replace("\r", "").Replace("\n", "")))
                {
                    result += " (" + text + ")";
                }
                return result;
            }

            public string Text
            {
                get
                {
                    Storage.FileText fileText = ReferenceBegin.Owner;
                    int indexBegin = ReferenceBegin.TextIndex;
                    int indexEnd = ReferenceEnd.TextIndex;
                    return fileText.Text.Substring(indexBegin, indexEnd - indexBegin + 1);
                }
            }

            /// <summary>
            /// Create one token if one tokenTextList matches.
            /// </summary>
            /// <param name="owner">Owner of new token to create.</param>
            /// <param name="reference">Character to start match.</param>
            /// <param name="createToken">Call back method to create one token.</param>
            /// <param name="tokenTextList">One of these tokens has to match.</param>
            public static void CreateToken(FileText owner, Storage.Character reference, Action<FileText, string, Storage.Character, Storage.Character> createToken, params string[] tokenTextList)
            {
                foreach (var tokenText in tokenTextList)
                {
                    if (reference.Owner.Text.Length > reference.TextIndex + tokenText.Length - 1)
                    {
                        if (reference.Owner.Text.Substring(reference.TextIndex, tokenText.Length) == tokenText)
                        {
                            var referenceEnd = (Storage.Character)reference.Owner.List[reference.TextIndex + tokenText.Length - 1];

                            // Create before
                            int length = owner.List.Count;

                            // Create
                            createToken(owner, tokenText, reference, referenceEnd);

                            // Create after
                            int lengthNew = owner.List.Count;

                            // Create validate
                            UtilFramework.Assert(lengthNew - length == 1); // Created one token
                            UtilFramework.Assert(owner.Last is Token); // Create of type token

                            break;
                        }
                    }
                }
            }

            public override void CreateSyntax(Tree.Component owner, List<Tree.Component> referenceList, Tree.Syntax reference, List<Tree.Syntax> syntaxFactoryList)
            {
                Create((FileText)owner, referenceList, (Storage.Character)reference, syntaxFactoryList);
            }

            public virtual void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference, List<Tree.Syntax> syntaxFactoryList)
            {

            }
        }

        public class Space : Token
        {
            public Space(FileText owner, Storage.Character referenceBegin, Storage.Character referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Space() 
                : base()
            {

            }

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference, List<Tree.Syntax> syntaxFactoryList)
            {
                var character =reference;
                if (character.Text == ' ')
                {
                    if (owner.Last is Space space)
                    {
                        space.Remove();
                        new Space(owner, space.ReferenceBegin, reference);
                    }
                    else
                    {
                        new Space(owner, reference, reference);
                    }
                }
            }
        }

        public class Header : Token
        {
            public Header(FileText owner, Storage.Character referenceBegin, Storage.Character referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Header()
            {

            }

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (reference.Text == '#')
                {
                    new Header(owner, reference, reference);
                }
            }
        }

        public class Content : Token
        {
            public Content(FileText owner, Storage.Character referenceBegin, Storage.Character referenceEnd) 
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Content()
            {

            }

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (owner.Last is Content content)
                {
                    content.Remove();
                    new Content((FileText)owner, content.ReferenceBegin, reference);
                }
                else
                {
                    new Content((FileText)owner, reference, reference);
                }
            }
        }

        public class Comment : Token
        {
            public Comment(FileText owner, Storage.Character referenceBegin, Storage.Character referenceEnd, bool isEnd)
                : base(owner, referenceBegin, referenceEnd)
            {
                IsEnd = isEnd;
            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Comment()
            {

            }

            public readonly bool IsEnd;

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference, List<Tree.Syntax> syntaxFactoryList)
            {
                CreateToken(owner, reference, (owner, tokenText, referenceBegin, referenceEnd) => new Comment(owner, referenceBegin, referenceEnd, tokenText == "-->"), "<!--", "-->");
            }
        }

        public class NewLine : Token
        {
            public NewLine(FileText owner, Storage.Character referenceBegin, Storage.Character referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public NewLine()
            {

            }

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference, List<Tree.Syntax> syntaxFactoryList)
            {
                CreateToken(owner, reference, (owner, tokenText, referenceBegin, referenceEnd) => new NewLine(owner, referenceBegin, referenceEnd), "\r\n", "\r", "\n");
            }
        }
    }

    public static class Markdown
    {
        public class Document : Tree.Component
        {
            public Document(MarkdownLexer.Document mdLexerDocument) 
                : base(null)
            {
                List<Tree.Syntax> syntaxFactoryList = new List<Tree.Syntax>();
                syntaxFactoryList.Add(new Page());
                syntaxFactoryList.Add(new Space());
                syntaxFactoryList.Add(new NewLine());
                syntaxFactoryList.Add(new Comment());
                syntaxFactoryList.Add(new Header());
                syntaxFactoryList.Add(new Content());

                Tree.Syntax.CreateSyntaxTree(this, mdLexerDocument.ListAll, null, syntaxFactoryList);
            }
        }

        public class Page : Tree.Syntax
        {
            public Page(Document owner, MarkdownLexer.FileText reference) 
                : base(owner, reference)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Page() 
                : base()
            {

            }

            public override void CreateSyntax(Tree.Component owner, List<Tree.Component> referenceList, Tree.Syntax reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (reference is MarkdownLexer.FileText referenceFileText)
                {
                    var page = new Page((Document)owner, referenceFileText);
                    CreateSyntaxTree(page, referenceList, reference.NextAll, syntaxFactoryList);
                }
            }
        }

        public class Node : Tree.Syntax
        {
            public Node(Tree.Component owner, MarkdownLexer.Token referenceBegin, MarkdownLexer.Token referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Node() 
                : base()
            {

            }

            public new MarkdownLexer.Token ReferenceBegin => (MarkdownLexer.Token)base.ReferenceBegin;

            public new MarkdownLexer.Token ReferenceEnd => (MarkdownLexer.Token)base.ReferenceEnd;

            public string Text
            {
                get
                {
                    string result = null;
                    if (IsFactory == false)
                    {
                        var tokenBegin = ReferenceBegin;
                        var characterBegin = tokenBegin.ReferenceBegin;

                        var tokenEnd = ReferenceEnd;
                        var characterEnd = tokenEnd.ReferenceEnd;

                        var fileText = characterBegin.Owner;

                        result = fileText.Text.Substring(characterBegin.TextIndex, characterEnd.TextIndex - characterBegin.TextIndex + 1);
                    }
                    return result;
                }
            }

            protected override string TextGet()
            {
                string result = "Markdown." + GetType().Name;
                if (!IsFactory)
                {
                    var text = Text;
                    if (!string.IsNullOrEmpty(text.Replace(" ", "").Replace("\r", "").Replace("\n", "")))
                    {
                        result += " " + "(\"" + text.Replace("\r", "").Replace("\n", "") + "\")";
                    }
                }
                return result;
            }

            public static List<Tree.Syntax> SyntaxFactoryStopList(List<Tree.Syntax> syntaxFactoryList)
            {
                return syntaxFactoryList.Where(item => item is NewLine).ToList();
            }

            public override void CreateSyntax(Tree.Component owner, List<Tree.Component> referenceList, Tree.Syntax reference, List<Tree.Syntax> syntaxFactoryList)
            {
                Create(owner, referenceList, (MarkdownLexer.Token)reference, syntaxFactoryList);
            }

            public virtual void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList)
            {

            }
        }

        public class Space : Node
        {
            public Space(Tree.Component owner, MarkdownLexer.Token referenceBegin, MarkdownLexer.Token referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Space()
                : base(null, null, null)
            {

            }

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (reference is MarkdownLexer.Space)
                {
                    new Space(owner, reference, reference);
                }
            }

        }

        public class NewLine : Node
        {
            public NewLine(Tree.Component owner, MarkdownLexer.Token referenceBegin, MarkdownLexer.Token referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public NewLine()
                : base(null, null, null)
            {

            }

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (reference is MarkdownLexer.NewLine)
                {
                    new NewLine(owner, reference, reference);
                }
            }
        }

        public class Comment : Node
        {
            public Comment(Tree.Component owner, MarkdownLexer.Token referenceBegin, MarkdownLexer.Token referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Comment() 
                : base(null, null, null)
            {

            }

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (reference is MarkdownLexer.Comment comment && comment.IsEnd == false)
                {
                    MarkdownLexer.Token referenceNext = reference;
                    while ((referenceNext = referenceNext.Next) != null)
                    {
                        if (referenceNext is MarkdownLexer.Comment commentNext && commentNext.IsEnd)
                        {
                            new Comment(owner, reference, referenceNext);
                            break;
                        }
                    }
                }
            }
        }

        public class Header : Node
        {
            public Header(Tree.Component owner, MarkdownLexer.Token referenceBegin, MarkdownLexer.Token referenceEnd)
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Header()
                : base(null, null, null)
            {

            }

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (reference is MarkdownLexer.Header tokenHeader)
                {
                    // Previous
                    bool isNull = tokenHeader.Previous == null;
                    bool isSpace = tokenHeader.Previous is MarkdownLexer.Space && (tokenHeader.Previous.Previous == null || tokenHeader.Previous.Previous is MarkdownLexer.NewLine);
                    bool isNewLine = tokenHeader.Previous is MarkdownLexer.NewLine;
                    if (isNull || isSpace || isNewLine)
                    {
                        var header = new Header(owner, reference, reference);
                        CreateSyntaxTree(header, referenceList, reference.Next, syntaxFactoryList, SyntaxFactoryStopList(syntaxFactoryList));
                    }
                }
            }
        }


        public class Content : Node
        {
            public Content(Tree.Component owner, MarkdownLexer.Token referenceBegin, MarkdownLexer.Token referenceEnd) 
                : base(owner, referenceBegin, referenceEnd)
            {

            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Content() 
                : base(null, null, null)
            {

            }

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList)
            {
                if (owner.Last is Content content)
                {
                    content.Remove();
                    new Content(owner, content.ReferenceBegin, reference);
                }
                else
                {
                    new Content(owner, reference, reference);
                }
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