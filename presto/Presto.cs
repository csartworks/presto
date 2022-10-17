using System.Diagnostics;
using Manufaktura.Controls.Model;
namespace presto;
public class Presto
{
    Score score = new();
    public static void Main(string[] args)
    {
        if (args.Length == 2) ToPDF(args[0], args[1]);
        else if (args.Length == 1) ToPDF(args[0]);
    }
    public static void ToPDF(string prestoScore, string title = "untitled")
    {
        string lyFileName = title + ".ly";
        string lyScore = Parser.ToLyNotes(prestoScore);

        string head = @"\version ""2.20.0"" \relative";
        using (StreamWriter streamWriter = File.CreateText(lyFileName))
            streamWriter.WriteLine($"{head}{{{lyScore}}}");
        ConvertLyToPDF(lyFileName);
    }
    private const string HORIZONTAL_BAR = "!!!!!!!!!!!!!!!!!!!!!!";
    private static void ConvertLyToPDF(string filename)
    {
        Process lilypond = new Process();
        lilypond.StartInfo = new ProcessStartInfo
        {
            FileName = "lilypond",
            Arguments = filename,
            RedirectStandardError = true
        };
        lilypond.Start();
        lilypond.WaitForExit();
        if (lilypond.ExitCode != 0)
        {
            string errorMessage = lilypond.StandardError.ReadToEnd();
            throw new Exception($"Lilypond threw an error.\n{HORIZONTAL_BAR}\n{errorMessage}Exit code : {lilypond.ExitCode}\n{HORIZONTAL_BAR}");
        }
        else Console.Write("Conversion was successful");
    }
    public static string filePath = "";
    public static string pwd = "";
    public static string title = "";
    public static void ToPng(string prestoScore, string title = "untitled")
    {
        Presto.title = title;
        string lyFileName = title + ".ly";
        string lyScore = Parser.ToLyNotes(prestoScore);

        string head = @"\version ""2.20.0"" \relative";
        pwd = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        filePath = Path.Combine(pwd, lyFileName);
        using (StreamWriter streamWriter = File.CreateText(filePath))
            streamWriter.WriteLine($"{head}{{{lyScore}}}");
        PNG(lyFileName);
    }
    private static void PNG(string filename)
    {
        Process lilypond = new Process();
        lilypond.StartInfo = new ProcessStartInfo
        {
            FileName = "lilypond",
            Arguments = $"{filePath}",
            WorkingDirectory = pwd,
            RedirectStandardError = true
        };
        lilypond.Start();
        lilypond.WaitForExit();
        if (lilypond.ExitCode != 0)
        {
            string errorMessage = lilypond.StandardError.ReadToEnd();
            throw new Exception($"Lilypond threw an error.\n{HORIZONTAL_BAR}\n{errorMessage}Exit code : {lilypond.ExitCode}\n{HORIZONTAL_BAR}");
        }
        else Console.Write("Conversion was successful");
    }
}