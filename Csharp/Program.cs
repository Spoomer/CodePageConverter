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

        Console.WriteLine("Which Encoding has the Inputfile? (1) UTF8 (2) ANSI/Latin1");
        string? readLine = Console.ReadLine();
        Encoding inputEncoding = readLine switch
        {
            "1" => Encoding.UTF8,
            "2" => Encoding.Latin1,
            _ => throw new Exception("supports only UTF8 and ANSI"),
        };
        Console.WriteLine("To which Encoding should it be converted? (1) UTF8 (2) ANSI/Latin1");
        string? readLineOutput = Console.ReadLine();
        Encoding outputEncoding = readLineOutput switch
        {
            "1" => Encoding.UTF8,
            "2" => Encoding.Latin1,
            _ => throw new Exception("supports only UTF8 and ANSI"),
        };
        FileInfo file = new(inputFile);
        var fullNameWithoutExtension = file.FullName[..file.FullName.LastIndexOf('.')];
        string input = File.ReadAllText(inputFile, inputEncoding);
        string newName = String.Concat(fullNameWithoutExtension,"_", outputEncoding.HeaderName,file.Extension);
        File.WriteAllText(newName,input,outputEncoding);

    }
}
