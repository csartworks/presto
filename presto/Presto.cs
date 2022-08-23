using System.Diagnostics;
namespace presto;
public class Presto
{
    public static void Main(string[] args)
    {
        if (args.Length == 2) ToPDF(args[0], args[1]);
        else if (args.Length == 1) ToPDF(args[0]);
    }

    public static string ToLyNotes(string notes)
    {
        if (char.IsUpper((char)notes[0]))
        {
            notes = notes.Insert(1, "'");
            notes = notes.ToLower();
        }
        notes = notes.Insert(1, "'");

        string result = string.Empty;
        foreach (char note in notes)
        {
        }
        for (int i = 0; i < notes.Length; i++)
        {
            switch (notes[i])
            {
                case '|':
                    result += @"\bar""|""";
                    break;
                case ',':
                    result += "r";
                    break;
                case '-':
                    result = result.Remove(result.Length - 1);
                    if (notes.ElementAtOrDefault(i + 2) == '-')
                    {
                        result += "2.";
                        i += 2;
                    }
                    else result += "2";
                    break;
                default:
                    result += notes[i];
                    break;
            }
        }
        return result;
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
                score = score.Remove(score.Length - 1);
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
        if (lilypond.ExitCode != 0)
        {
            string errorMessage = lilypond.StandardError.ReadToEnd();
            throw new Exception($"Lilypond threw an error.\n{HORIZONTAL_BAR}\n{errorMessage}Exit code : {lilypond.ExitCode}\n{HORIZONTAL_BAR}");
        }
        else Console.WriteLine("Conversion was successful");
    }
}