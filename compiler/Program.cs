using System;

namespace Microsoft
{
    class Program
    {
        static void Main(string[] args)
        {
        
            while(true)
            {
                Console.WriteLine(">");
                var line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                {
                    return;
                }

                var lexer = new Lexer(line);
                while(true)
                {
                   var token = lexer.NextToken();
                   if (token.Kind == SyntaxKind.EndOfFileToken)
                        break;
                    Console.Write($"{token.Kind}: '{token.Text}'");
                    if (token.Value != null)
                    {
                        Console.Write($"{token.Value}");
                    }
                    Console.WriteLine();

                }
            }
        }
    }

    enum SyntaxKind
    {
        EndOfFileToken,
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenToken,
        CloseToken,
        BadToken
    }
    class SyntaxToken
    {   
        public SyntaxKind Kind{get;}
        public int Position{get;}
        public string Text{get;}
        
        public object Value{get;}


        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
    }
    class Lexer
    {
        private readonly string text;
        private int position;
        public Lexer(string text)
        {
            this.text=text;
        }

        private char Current
        {
            get
            {
                if(position>=text.Length)
                return '\0';

                return text[position];
            }
        }

        private void Next()
        {
            position++;
        }

        public SyntaxToken NextToken()
        {
            // Identify EOF, numbers, operations, brackets and white space

            if (position >= text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, position, "\0", null);

            
            if (char.IsDigit(Current))
            {
                var start = position;

                while (char.IsDigit(Current))
                Next();

                var length = position - start;
                var msg = text.Substring(start,length);
                int.TryParse(msg, out var value);
                return new SyntaxToken(SyntaxKind.NumberToken, start, msg, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = position - start;
                var msg = text.Substring(start,length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, msg, null);
            }

            if (Current == '+')
            {
                return new SyntaxToken(SyntaxKind.PlusToken, position++, "+", null);
            }

            else if (Current == '-')
            {
                return new SyntaxToken(SyntaxKind.MinusToken, position++, "-", null);
            }

            else if (Current == '*')
            {
                return new SyntaxToken(SyntaxKind.StarToken, position++, "*", null);
            }

            else if (Current == '/')
            {
                return new SyntaxToken(SyntaxKind.SlashToken, position++, "/", null);
            }

            else if (Current == '(')
            {
                return new SyntaxToken(SyntaxKind.OpenToken, position++, "(", null);
            }

            else if (Current == ')')
            {
                return new SyntaxToken(SyntaxKind.CloseToken, position++, ")", null);
            }
            
            return new SyntaxToken(SyntaxKind.BadToken, position++, text.Substring(position-1,1), null);
        }

    }
    class parser
    {
        
    }

}