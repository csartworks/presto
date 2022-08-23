namespace presto_tests;
public class MelodyTest : PDFTest
{
    [Fact]
    public void InvalidSyntaxThrowsError()
    {
        Assert.Throws<Exception>(() => Presto.ToPDF("facodjofp", _testPdfName));
    }
    [Fact]
    public void SimpleMelodyTest()
    {
        Test("e e f g", "simple-melody.pdf");
    }
    [Fact]
    public void MelodyThatStartsOctaveUp()
    {
        Test("E e f g", "simple-melody-2.pdf");
    }
    [Fact]
    public void DrawingBarTest()
    {
        Test("E e f g | g f e d |", "test4-drawing-bars.pdf");
        Test("E e | f g | g f e d |", "test4-bartest-2.pdf");
    }
    [Fact]
    public void RestsTest()
    {
        Test("e d d ,", "test5-rests.pdf");
    }
    [Fact]
    public void NoteLengthTest()
    {
        Test("e - d -", "test6-half-notes.pdf");
    }
    [Fact]
    public void WholeNotesTest()
    {
        Test("eieajfeopf - - - | d - - -", "test7-whole-notes.pdf");
    }
}