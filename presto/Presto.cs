using System.Diagnostics;
namespace presto;
public class Presto
{
    public static void Main(string[] args)
    {
        ToPDF("d a c");
    }
    public static void ToPDF(string value, string title = "untitled")
    {
        string lyFileName = title + ".ly";
        using (StreamWriter streamWriter = File.CreateText(lyFileName))
        {
            string content = @"\version ""2.22.2"" \relative";
            string notes = $"{{{value}'}}";
            streamWriter.WriteLine(content + notes);
        }

        Process lilypond = new Process();
        lilypond.StartInfo.FileName = "lilypond";
        lilypond.StartInfo.Arguments = lyFileName;
        lilypond.Start();
        lilypond.WaitForExit();
    }
}