namespace presto_tests;
public class MelodyTest : PDFTest
{
    [Fact]
    public void SimpleMelodyTest()
    {
        Presto.ToPDF("e e f g", TEST_FILE_TITLE);
        PDFAssert.PDFEqual(_testPdfName, GetPath("simple-melody.pdf"));
    }
    [Fact]
    public void MelodyThatStartsOctaveUp()
    {
        Presto.ToPDF("E e f g", TEST_FILE_TITLE);
        PDFAssert.PDFEqual(_testPdfName, GetPath("simple-melody-2.pdf"));
    }
    [Fact]
    public void DrawingBarTest()
    {
        Presto.ToPDF("E e f g | g f e d |", TEST_FILE_TITLE);
        PDFAssert.PDFEqual(_testPdfName, GetPath("test4-drawing-bars.pdf"));
    }
}