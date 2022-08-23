namespace presto_tests;
public class MelodyTest : PDFTest
{
    [Fact]
    public void SimpleMelodyTest()
    {
        Presto.ToPDF("e e f g", TEST_FILE_TITLE);
        PDFAssert.PDFEqual(_testPdfName, GetTestPDFPath("simple-melody.pdf"));
    }
    [Fact]
    public void MelodyThatStartsOctaveUp()
    {
        Presto.ToPDF("E e f g", TEST_FILE_TITLE);
        // PDFAssert.PDFEqual(_testPdfName, GetTestPDFPath("simple-melody-2.pdf"));
        Assert.Equal(GetFilteredRaw(_testPdfName), GetFilteredRaw(GetTestPDFPath("simple-melody-2.pdf")));
    }
}