using System.Text;

namespace CodePageConverter;

public class Program
{
    public static void Main(string[] args)
    {
        Arguments arguments = new(args);
        string inputFile = "";
        if (arguments.Check() == false)
        {
            Console.WriteLine("Choose File to convert:");
            inputFile = Console.ReadLine() ?? "";
            if (String.IsNullOrWhiteSpace(inputFile)) return;
        }
        else
        {
            inputFile = arguments.InputFile;
        }
        Console.WriteLine($"Inputfile:{inputFile}");
        Encoding inputEncoding;
        if (string.IsNullOrWhiteSpace(arguments.InputEncoding))
        {
            Console.WriteLine("Which Encoding has the Inputfile?  (1) UTF8 (2) Latin1/ISO-8859-1/Windows-1252 Western Europe");
            inputEncoding= GetEncodingFromConsoleInput();
        }
        else
        {
            inputEncoding = Encoding.GetEncoding(arguments.InputEncoding);
        }
        Encoding outputEncoding;
        if (string.IsNullOrWhiteSpace(arguments.OutputEncoding))
        {
            Console.WriteLine("To which Encoding should it be converted? (1) UTF8 (2) Latin1/ISO-8859-1/Windows-1252 Western Europe");
            outputEncoding = GetEncodingFromConsoleInput();
        }
        else
        {
            outputEncoding = Encoding.GetEncoding(arguments.OutputEncoding);
        }
        FileInfo file = new(inputFile);
        var fullNameWithoutExtension = file.FullName[..file.FullName.LastIndexOf('.')].Replace("_" + inputEncoding.HeaderName, "");
        string input = File.ReadAllText(inputFile, inputEncoding);
        string newName = String.Concat(fullNameWithoutExtension, "_", outputEncoding.HeaderName, file.Extension);
        File.WriteAllText(newName, input, outputEncoding);

    }

    private static Encoding GetEncodingFromConsoleInput()
    {
        string? readLineOutput = Console.ReadLine();
        return readLineOutput switch
        {
            "1" => Encoding.UTF8,
            "2" => Encoding.Latin1,
            _ => throw new Exception("supports only UTF8, Latin1/ISO-8859-1/Windows-1252"),
        };
    }
}
