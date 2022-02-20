namespace CodePageConverter;

public class Arguments
{
    public string InputFile { get; private set; } = "";
    public string InputEncoding { get; private set; } = "";
    public string OutputEncoding { get; private set; } = "";
    private const int PosInputFile = 0;
    private const int PosInputEncoding = 1;
    private const int PosOutputEncoding = 2;
    private readonly string[] _args = Array.Empty<string>();

    public Arguments(string[] args)
    {
        _args = args;
    }

    public bool Check()
    {
        if (_args.Length == 0)
        {
            return false;
        }
        if (File.Exists(_args[PosInputFile]) == false)
        {
            return false;
        }
        InputFile = _args[PosInputFile];
        if (_args.Length == PosInputEncoding + 1 && string.IsNullOrWhiteSpace(_args[PosInputEncoding]) == false)
        {
            InputEncoding = _args[PosInputEncoding];
        }
        if (_args.Length == PosOutputEncoding + 1 && string.IsNullOrWhiteSpace(_args[PosOutputEncoding]) == false)
        {
            InputEncoding = _args[PosOutputEncoding];
        }
        return true;
    }
}
