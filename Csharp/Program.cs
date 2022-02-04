using System.Text;

namespace CodePageConverter;

public class Program
{
    public static void Main(string[] args)
    {
        string inputFile;
        if (args.Length < 1)
        {
            Console.WriteLine("Choose File to convert:");
            inputFile = Console.ReadLine() ?? "";
            if (String.IsNullOrWhiteSpace(inputFile)) return;
        }
        else
        {
            inputFile = args[0];
        }
        Console.WriteLine(inputFile);
        Console.WriteLine("Which Encoding has the Inputfile?  (1) UTF8 (2) Latin1/ISO-8859-1 (3) Windows-1252 Western Europe");
        string? readLine = Console.ReadLine();
        Encoding inputEncoding = readLine switch
        {
            "1" => Encoding.UTF8,
            "2" => Encoding.Latin1,
            "3" => Encoding.GetEncoding(1252),
            _ => throw new Exception("supports only UTF8, Latin1/ISO-8859-1 or Windows-1252"),
        };
        Console.WriteLine("To which Encoding should it be converted? (1) UTF8 (2) Latin1/ISO-8859-1 (3) Windows-1252 Western Europe");
        string? readLineOutput = Console.ReadLine();
        Encoding outputEncoding = readLineOutput switch
        {
            "1" => Encoding.UTF8,
            "2" => Encoding.Latin1,
            "3" => Encoding.GetEncoding(1252),
            _ => throw new Exception("supports only UTF8, Latin1/ISO-8859-1 or Windows-1252"),
        };
        FileInfo file = new(inputFile);
        var fullNameWithoutExtension = file.FullName[..file.FullName.LastIndexOf('.')].Replace("_"+inputEncoding.HeaderName,"");
        string input = File.ReadAllText(inputFile, inputEncoding);
        string newName = String.Concat(fullNameWithoutExtension,"_", outputEncoding.HeaderName,file.Extension);
        File.WriteAllText(newName,input,outputEncoding);

    }
}
