namespace presto_tests;
public class NoteTest
{
    private bool GENERATE_PDFS = false;
    private void Test(string expectedLy, string prestoNotes, string testName)
    {
        Assert.Equal(expectedLy, Parser.ToLyNotes(prestoNotes));
        if (GENERATE_PDFS)
        {
            Presto.Main(new string[] { prestoNotes, testName });
        }
    }
    [Fact]
    public void BasicNoteInputTest()
    {
        Test("c'", "c", "test_1_singleNote");
        Test("e' e f g", "e e f g", "test_2_seriesOfNotes");
    }
    [Fact]
    public void PitchTest()
    {
        Test("e'' e f g", "E e f g", "test_3_melodyStartsOctaveOver");
    }
    [Fact]
    public void BarTest()
    {
        Test(@"e'' e f g \bar""|"" g f e d", "E e f g | g f e d", "test_4_bars");
        Test(@"e'' e \bar""|"" f g \bar""|"" g f e d", "E e | f g | g f e d", "test_4_a_bars");
    }
    [Fact]
    public void RestTest()
    {
        Test("e' d d r", "e d d ,", "test_5_rests");
    }
    [Fact]
    public void NoteRhythmTest()
    {
        Test("e'2 d2", "e - d -", "test_6_half_note");
        Test(@"e'1 \bar""|"" d1", "e - - - | d - - -", "test_7_whole_note");
        Test("e'2.", "e - -", "test_8_dotted_half_note");
        Test("e'8 e f8 g", "ee fg", "test_9_8th_note");
    }
}