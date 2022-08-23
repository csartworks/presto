namespace presto_tests;
public class MelodyTest : PDFTest
{
    [Fact]
    public void SimpleMelodyTest()
    {
        TestConvert("e e f g", "simple-melody.pdf");
    }
    [Fact]
    public void MelodyThatStartsOctaveUp()
    {
        TestConvert("E e f g", "simple-melody-2.pdf");
    }
    [Fact]
    public void DrawingBarTest()
    {
        TestConvert("E e f g | g f e d |", "test4-drawing-bars.pdf");
        TestConvert("E e | f g | g f e d |", "test4-bartest-2.pdf");
    }
    [Fact]
    public void RestsTest()
    {
        TestConvert("e d d ,", "test5-rests.pdf");
    }
    [Fact]
    public void NoteLengthTest()
    {
        TestConvert("e - d -", "test6-half-notes.pdf");
    }
    // [Fact]
    // public void WholeNotesTest()
    // {
    //     Shortcut("e - - - | d - - -", "test7-whole-notes.pdf");
    // }

    private void TestConvert(string input, string resultPDFName)
    {
        Presto.ToPDF(input, TEST_FILE_TITLE);
        PDFAssert.PDFEqual(_testPdfName, GetPath(resultPDFName));
    }
}