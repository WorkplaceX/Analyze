namespace Parse
{
    using System;
    using System.Collections.Generic;
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

            private void ListAll(Component component, List<Component> result)
            {
                if (component != this)
                {
                    result.Add(component);
                }
                foreach (var item in component.List)
                {
                    ListAll(item, result);
                }
            }

            /// <summary>
            /// Returns all components (hierarchical).
            /// </summary>
            public List<Component> ListAll()
            {
                List<Component> result = new List<Component>();
                ListAll(this, result);
                return result;
            }

            /// <summary>
            /// Gets Owner. Owner of this component.
            /// </summary>
            public Component Owner { get; private set; }

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
                        result = result.ListAll().Last();
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
                        result = result.ListAll().Last();
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
            public Syntax(Component owner, Syntax referenceBegin, Syntax referenceEnd) 
                : base(owner)
            {
                ReferenceBegin = referenceBegin;
                ReferenceEnd = referenceEnd;
            }

            /// <summary>
            /// Constructor factory.
            /// </summary>
            public Syntax() 
                : base(null)
            {

            }

            public bool IsFactory => Owner == null;

            public readonly Syntax ReferenceBegin;

            public int ReferenceBeginIndex
            {
                get
                {
                    int result = -1;
                    if (ReferenceBegin != null)
                    {
                        result = ReferenceBegin.Index;
                    }
                    return result;
                }
            }

            public readonly Syntax ReferenceEnd;

            public int ReferenceEndIndex
            {
                get
                {
                    int result = -1;
                    if (ReferenceEnd != null)
                    {
                        result = ReferenceEnd.Index;
                    }
                    return result;
                }
            }

            public Syntax ReferenceEndAll
            {
                get
                {
                    var result = ReferenceEnd;
                    if (List.Count > 0)
                    {
                        result = ((Syntax)ListAll().Last()).ReferenceEnd;
                    }
                    return result;
                }
            }

            /// <summary>
            /// Create syntax.
            /// </summary>
            /// <param name="owner">Owner of new syntax.</param>
            /// <param name="referenceList">Reference list to loop through</param>
            /// <param name="reference">Reference item to start with.</param>
            public static void Create(Component owner, List<Component> referenceList, Syntax reference, List<Syntax> syntaxFactoryList, List<Syntax> syntaxFactoryStopList)
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
                        syntaxFactoryItem.Create(owner, referenceList, item, syntaxFactoryList);

                        // Create after
                        Syntax syntaxNewLast = (Syntax)owner.Last;
                        int syntaxNewLength = owner.List.Count;

                        // Create result get
                        UtilFramework.Assert(syntaxNewLength - syntaxLength == 0 || syntaxNewLength - syntaxLength == 1); // Zero or one token added.
                        if (syntaxNewLength - syntaxLength == 1)
                        {
                            syntax = syntaxNewLast;
                        }
                        else
                        {
                            if (syntaxLast != syntaxNewLast)
                            {
                                syntax = syntaxNewLast;
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
                        UtilFramework.Assert(syntaxPrevious.ReferenceEndIndex + 1 == syntax.ReferenceBeginIndex);
                    }
                    int referenceEndIndex = syntax.ReferenceEndAll.Index;
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

            /// <summary>
            /// Create one new Syntax component or replace last Syntax component.
            /// </summary>
            public virtual void Create(Component owner, List<Component> referenceList, Syntax reference, List<Syntax> syntaxFactoryList)
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

        public class FileText : Tree.Component
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
                : base(owner, null, null)
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
                syntaxFactoryList.Add(new NewLine());
                syntaxFactoryList.Add(new Comment());
                syntaxFactoryList.Add(new Space());
                syntaxFactoryList.Add(new Header());
                syntaxFactoryList.Add(new Content());

                List<Tree.Syntax> syntaxFactoryStopList = new List<Tree.Syntax>();

                foreach (Storage.FileText referenceFileText in storageDocument.List)
                {
                    var fileText = new FileText(this);
                    Tree.Syntax.Create(fileText, referenceFileText.List, null, syntaxFactoryList, syntaxFactoryStopList);
                }
            }
        }

        public class FileText : Tree.Component
        {
            public FileText(Document owner)
                : base(owner)
            {

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

            public static void Create(FileText owner, Storage.Character reference, Action<FileText, string, Storage.Character, Storage.Character> createSyntax, params string[] tokenTextList)
            {
                var referenceCharacter = (Storage.Character)reference;
                foreach (var tokenText in tokenTextList)
                {
                    if (referenceCharacter.Owner.Text.Length > referenceCharacter.TextIndex + tokenText.Length - 1)
                    {
                        if (referenceCharacter.Owner.Text.Substring(referenceCharacter.TextIndex, tokenText.Length) == tokenText)
                        {
                            var referenceEnd = (Storage.Character)reference.Owner.List[referenceCharacter.TextIndex + tokenText.Length - 1];

                            // Create before
                            int length = owner.List.Count;

                            // Create
                            createSyntax(owner, tokenText, reference, referenceEnd);

                            // Create after
                            int lengthNew = owner.List.Count;

                            // Create validate
                            UtilFramework.Assert(lengthNew - length == 1); // Created one token
                            break;
                        }
                    }
                }
            }

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, Tree.Syntax reference, List<Tree.Syntax> syntaxFactoryList)
            {
                Create((FileText)owner, referenceList, (Storage.Character)reference);
            }

            public virtual void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference)
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

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference)
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

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference)
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

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference)
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

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference)
            {
                Create(owner, reference, (owner, tokenText, referenceBegin, referenceEnd) => new Comment(owner, referenceBegin, referenceEnd, tokenText == "-->"), "<!--", "-->");
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

            public override void Create(FileText owner, List<Tree.Component> referenceList, Storage.Character reference)
            {
                Create(owner, reference, (owner, tokenText, referenceBegin, referenceEnd) => new NewLine(owner, referenceBegin, referenceEnd), "\r\n", "\r", "\n");
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
                syntaxFactoryList.Add(new Space());
                syntaxFactoryList.Add(new NewLine());
                syntaxFactoryList.Add(new Comment());
                syntaxFactoryList.Add(new Header());
                syntaxFactoryList.Add(new Content());

                List<Tree.Syntax> syntaxFactoryStopList = new List<Tree.Syntax>();
                syntaxFactoryStopList.Add(new NewLine());

                foreach (var fileText in mdLexerDocument.List.OfType<MarkdownLexer.FileText>())
                {
                    var page = new Page(this);
                    Tree.Syntax.Create(page, fileText.List, null, syntaxFactoryList, syntaxFactoryStopList);
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
                : base(null, null, null)
            {

            }

            public new MarkdownLexer.Token ReferenceBegin => (MarkdownLexer.Token)base.ReferenceBegin;

            public new MarkdownLexer.Token ReferenceEnd => (MarkdownLexer.Token)base.ReferenceEnd;

            public string Text
            {
                get
                {
                    var tokenBegin = ReferenceBegin;
                    var characterBegin = tokenBegin.ReferenceBegin;

                    var tokenEnd = ReferenceEnd;
                    var characterEnd = tokenEnd.ReferenceEnd;

                    var fileText = characterBegin.Owner;

                    return fileText.Text.Substring(characterBegin.TextIndex, characterEnd.TextIndex - characterBegin.TextIndex + 1);
                }
            }

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, Tree.Syntax reference, List<Tree.Syntax> syntaxFactoryList)
            {
                List<Tree.Syntax> syntaxFactoryStopList = new List<Tree.Syntax>(syntaxFactoryList.Where(item => item is NewLine || item is Comment));
                Create(owner, referenceList, (MarkdownLexer.Token)reference, syntaxFactoryList, syntaxFactoryStopList);
            }

            public virtual void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList, List<Tree.Syntax> syntaxFactoryStopList)
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

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList, List<Tree.Syntax> syntaxFactoryStopList)
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

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList, List<Tree.Syntax> syntaxFactoryStopList)
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

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList, List<Tree.Syntax> syntaxFactoryStopList)
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

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList, List<Tree.Syntax> syntaxFactoryStopList)
            {
                if (reference is MarkdownLexer.Header tokenHeader)
                {
                    if (tokenHeader.Previous == null || (tokenHeader.Previous is MarkdownLexer.Space && (tokenHeader.Previous.Previous == null || tokenHeader.Previous.Previous is MarkdownLexer.NewLine)))
                    {
                        var header = new Header(owner, reference, reference);
                        Tree.Syntax.Create(header, referenceList, reference.Next, syntaxFactoryList, syntaxFactoryStopList);
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

            public override void Create(Tree.Component owner, List<Tree.Component> referenceList, MarkdownLexer.Token reference, List<Tree.Syntax> syntaxFactoryList, List<Tree.Syntax> syntaxFactoryStopList)
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