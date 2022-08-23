namespace presto;
public class Parser
{
    private static string[] NoteLengths = new string[] { "2", "2.", "1" };
    private const string NOTE_NAMES = "abcdefg";
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
                    Foo(i);
                    break;
            }
        }
        return result;

        void Foo(int i)
        {

            result += notes[i];
        }

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
}