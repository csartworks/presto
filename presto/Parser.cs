namespace presto;
public static class Parser
{
    private static string[] NoteLengths = new string[] { "2", "2.", "1" };
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
            bool is8th = false;
            if (IsNoteName(i) && IsNoteName(i + 1))
            {
                is8th = true;
            }

            s_result += s_notes[i];
            if (is8th)
            {
                s_result += '8';
            }
        }
    }
    private static bool IsNoteName(int index)
    {
        return NOTE_NAMES.Contains(s_notes.ElementAtOrDefault(index));
    }
    private static void ParseDashes()
    {
        int x = 0;
        SearchForDashes(ref x);
        s_result += NoteLengths[x - 1];
        s_head += 2 * (x - 1);

    }
    private static void SearchForDashes(ref int x)
    {
        if (s_notes.ElementAtOrDefault(s_head + 2 * x) == '-')
        {
            x++;
            SearchForDashes(ref x);
        }
    }
}