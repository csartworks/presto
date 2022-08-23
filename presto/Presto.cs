using System.Diagnostics;
namespace presto;
public class Presto
{
    public static void Main(string[] args)
    {
        ToPDF(args[0], args[1]);
    }
    public static void ToPDF(string prestoScore, string title = "untitled")
    {
        string lyFileName = title + ".ly";
        string lyScore = string.Empty;

        if (char.IsUpper((char)prestoScore[0]))
        {
            prestoScore = prestoScore.Insert(1, "'");
            prestoScore = prestoScore.ToLower();
        }
        prestoScore = prestoScore.Insert(1, "'");

        foreach (char note in prestoScore) ParseNote(note, ref lyScore);

        string head = @"\version ""2.20.0"" \relative";
        using (StreamWriter streamWriter = File.CreateText(lyFileName))
            streamWriter.WriteLine($"{head}{{{lyScore}}}");
        ConvertLyToPDF(lyFileName);
    }
    private static void ParseNote(char note, ref string score)
    {
        switch (note)
        {
            case '|':
                score += @"\bar""|""";
                break;
            case ',':
                score += "r";
                break;
            case '-':
                score.Remove(score.Length - 1);
                score += "2";
                break;
            default:
                score += note;
                break;
        }
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
        Console.WriteLine(lilypond.ExitCode);
        if (lilypond.ExitCode != 0)
        {
            string errorMessage = lilypond.StandardError.ReadToEnd();
            throw new Exception($"Lilypond threw an error.\n{HORIZONTAL_BAR}\n{errorMessage}Exit code : {lilypond.ExitCode}\n{HORIZONTAL_BAR}");
        }
    }
}