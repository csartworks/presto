namespace presto;
public static class Parser
{
    private static string[] NoteLengths = new string[] { "2", "2.", "1" };
    private static string[] ShortNoteLengths = new string[] { "", "", "8", "16", "32" };
    private const string NOTE_NAMES = "abcdefg";
    private static string s_result = string.Empty;
    private static string s_notes = string.Empty;
    private static int s_head = 0;
    public static string ToLyNotes(in string prestoNotes)
    {
        s_head = 0;
        s_result = string.Empty;
        s_notes = prestoNotes;


        for (s_head = 0; s_head < s_notes.Length; s_head++)
        {
            switch (s_notes[s_head])
            {
                case '|':
                    s_result += @"\bar""|""";
                    break;
                case ',':
                    s_result += "r";
                    break;
                case '-':
                    s_result = s_result.Remove(s_result.Length - 1);
                    ParseDashes();
                    break;
                case ' ':
                    s_result += ' ';
                    break;
                default:
                    Foo(s_head);
                    break;
            }
        }
        if (char.IsUpper((char)s_notes[0]))
        {
            s_result = s_result.Insert(1, "'");
        }
        s_result = s_result.Insert(1, "'");
        s_result = s_result.ToLower();
        return s_result;

        void Foo(int i)
        {
            int x = 0;
            SearchFoward(ref x, 1, NOTE_NAMES);

            s_result += s_notes[i];
            s_result += ShortNoteLengths[x];
            if(x >= 2) s_result += " ";
        }
    }
    private static bool IsNoteName(int index)
    {
        return NOTE_NAMES.Contains(s_notes.ElementAtOrDefault(index));
    }
    private static void ParseDashes()
    {
        int x = 0;
        SearchFoward(ref x, 2, '-');
        s_result += NoteLengths[x - 1];
        s_head += 2 * (x - 1);
    }
    private static void SearchFoward(ref int count, int step, char toSearch)
    {
        if (s_notes.ElementAtOrDefault(s_head + step * count) == toSearch)
        {
            count++;
            SearchFoward(ref count, step, toSearch);
        }
    }
    private static void SearchFoward(ref int count, int step, string searchIn)
    {
        int nextSearchIndex = s_head + step * count;
        if (searchIn.Contains(s_notes.ElementAtOrDefault(nextSearchIndex)))
        {
            count++;
            SearchFoward(ref count, step, searchIn);
        }
    }
}