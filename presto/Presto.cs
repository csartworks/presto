using System.Diagnostics;
namespace presto;
public class Presto
{
    public static void Main(string[] args)
    {
        ToPDF("E e f g");
    }
    public static void ToPDF(string value, string title = "untitled")
    {
        string lyFileName = title + ".ly";
        using (StreamWriter streamWriter = File.CreateText(lyFileName))
        {
            string content = @"\version ""2.22.2"" \relative";
            if (char.IsUpper(value[0]))
            {
                value = value.Insert(1, "'");
                value = value.ToLower();
            }
            value = value.Insert(1, "'");
            string notes = $"{{{value}}}";
            streamWriter.WriteLine(content + notes);
        }

        Process lilypond = new Process();
        lilypond.StartInfo.FileName = "lilypond";
        lilypond.StartInfo.Arguments = lyFileName;
        lilypond.Start();
        lilypond.WaitForExit();
    }
}