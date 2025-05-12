namespace Brainfck;

class Program
{
    const int Size = 30000;
    const char NullChar = '\0';
    
    static char[] _array = new char[Size];
    static string _code;
    static int _arrayPosition;
    static int _codePosition;
    static bool _isFile;
    
    static void Main(string[] args)
    {
        if (args.Length == 1)
        {
            _isFile = true;
            ScanFile(args[0]);
        }
        else
        {
            Repl();            
        }
    }

    static void ScanFile(string filename)
    {
        _code = File.ReadAllText(filename);
        Scan(false);
    }

    static void Repl()
    {
        for (;;)
        {
            Console.Write("\t");
            _code = Console.ReadLine();
            Scan(false);
        }
    }

    static bool Scan(bool skip)
    {
        while (_arrayPosition >= 0 && _codePosition < _code.Length)
        {
            if (_arrayPosition >= _array.Length)
            {
                _array = _array.Append(NullChar).ToArray();
            }
            char c = _code[_codePosition];
            switch (c)
            {
                case '[':
                    _codePosition++;
                    int oldJ = _codePosition;
                    while (Scan(_array[_arrayPosition] == NullChar))
                    {
                        _codePosition = oldJ;
                    }
                    break;
                case ']':
                    return _array[_arrayPosition] != NullChar;
                default:
                    if (!skip)
                    {
                        Eval(c);
                    }
                    break;
            }
            _codePosition++;
        }
        return false;
    }

    static void Eval(char c)
    {
        switch (c)
        {
            case '>':
                _arrayPosition++;
                break;
            case '<':
                _arrayPosition--;
                break;
            case '+':
                _array[_arrayPosition]++;
                break;
            case '-':
                _array[_arrayPosition]--;
                break;
            case '.':
                Console.Write(_array[_arrayPosition].ToString());
                break;
            case ',':
                _array[_arrayPosition] = (char)Console.Read();
                break;
        }
    }
}