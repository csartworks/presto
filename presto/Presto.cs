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
                    ParseDashes(ref i);
                    break;
                default:
                    result += notes[i];
                    break;
            }
        }
        return result;

        void ParseDashes(ref int i)
        {
            int x = 0;
            SearchForDashes(ref i, ref x);
            result += NoteLengths[x - 1];
            i += 2 * (x - 1);

            void SearchForDashes(ref int i, ref int x)
            {
                if (notes.ElementAtOrDefault(i + 2 * x) == '-')
                {
                    x++;
                    SearchForDashes(ref i, ref x);
                }
            }
        }
    }
    private static string[] NoteLengths = new string[] { "2", "2.", "1" };

    public static void ToPDF(string prestoScore, string title = "untitled")
    {
        string lyFileName = title + ".ly";
        string lyScore = ToLyNotes(prestoScore);

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