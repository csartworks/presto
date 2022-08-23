using System.Diagnostics;
namespace presto;
public class Presto
{
    public static void Main(string[] args)
    {
        ToPDF("E e f g");
    }
    public static void ToPDF(string notes, string title = "untitled")
    {
        string lyFileName = title + ".ly";
        using (StreamWriter streamWriter = File.CreateText(lyFileName))
        {
            string lyNotes = string.Empty;
            if (char.IsUpper((char)notes[0]))
            {
                notes = notes.Insert(1, "'");
                notes = notes.ToLower();
            }
            notes = notes.Insert(1, "'");

            foreach (char note in notes)
            {
                if (note == '|')
                {
                    lyNotes += @"\bar""|""";
                    continue;
                }
                lyNotes += note;
            }
            string head = @"\version ""2.22.2"" \relative";
            streamWriter.WriteLine($"{head}{{{notes}}}");
        }

        Process lilypond = new Process();
        lilypond.StartInfo.FileName = "lilypond";
        lilypond.StartInfo.Arguments = lyFileName;
        lilypond.Start();
        lilypond.WaitForExit();
    }
}