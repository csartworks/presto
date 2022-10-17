namespace prestocli;
public static class PrestoCLI
{
    private const string NOTE_ROW = " -asdfghj";
    private const string NOTES = " -cdefgab";
    private static string score = "";
    private static (int left, int top) Original;

    public static void Main()
    {
        Console.Clear();
        WriteToStatusBar("presto CLI v.0.1");
        while (true)
        {
            MainLoop();
        }
    }
    private static void MainLoop()
    {
        ConsoleKeyInfo info = Console.ReadKey(true);
        char keyChar = info.KeyChar;
        int noteRowIndex = NOTE_ROW.IndexOf(keyChar);
        if (noteRowIndex != -1)
        {
            WriteScore(NOTES[noteRowIndex]);

        }
        else if (info.Key == ConsoleKey.Backspace) EraseScore();
        else if (info.Modifiers == ConsoleModifiers.Control)
        {
            if (info.Key == ConsoleKey.S) Save();
            else if (info.Key == ConsoleKey.E) Export();
            else if (info.Key == ConsoleKey.O) Open();
        }
        else
        {
            switch (keyChar)
            {
                case 'n':
                    Console.CursorLeft++;
                    break;
                case 'N':
                    Console.CursorLeft--;
                    break;
            }
        }
    }
    private static void EraseScore()
    {
        Console.CursorLeft--;
        Console.Write(" ");
        Console.CursorLeft--;
        score = score[..^1];
    }
    private static void EraseConsoleLine()
    {
        Console.CursorLeft = 0;
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.CursorLeft = 0;
    }
    private static void WriteToStatusBar(string text)
    {
        Status();
        Console.Write(text);
        Restore();
    }
    private static void Status()
    {
        Original = Console.GetCursorPosition();
        Console.SetCursorPosition(0, Console.BufferHeight - 1);
        EraseConsoleLine();
    }
    private static void Restore()
    {
        Console.SetCursorPosition(Original.left, Original.top);
    }
    private static void WriteScore(char text) => WriteScore(text.ToString());
    private static void WriteScore(string text)
    {
        Console.Write(text);
        CurrentFile.Write(text);
        score += text;
    }

    private static void Save()
    {
        WriteToStatusBar("Saving file...");
        // using (StreamWriter streamWriter = File.CreateText("untitled.presto"))
        // {
        //     streamWriter.Write(score);
        // }
        CurrentFile.Close();
        // CurrentFile = new StreamWriter(CurrentFileName);
        WriteToStatusBar("File saved.");
    }
    private static void Export()
    {
        Status();
        Console.Write("Converting...");
        presto.Presto.ToPDF(score);
        Restore();
    }
    private static StreamWriter CurrentFile;
    private static string CurrentFileName = "untitled.presto";
    private static void Open()
    {
        Status();
        Console.Write("Enter file name : ");
        CurrentFileName = Console.ReadLine() ?? "";
        string fileContent = File.ReadAllText(CurrentFileName);
        Console.Clear();
        WriteToStatusBar($"Loading file... : {CurrentFileName}");
        Console.Write(fileContent);
        WriteToStatusBar("Loaded file : " + CurrentFileName);
        score = fileContent;

        CurrentFile = new StreamWriter(CurrentFileName);
        CurrentFile.Write(fileContent);
    }
}