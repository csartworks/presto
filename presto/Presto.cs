using System.Diagnostics;
namespace presto;
public class Presto
{
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
        else Console.WriteLine("Conversion was successful");
    }
}