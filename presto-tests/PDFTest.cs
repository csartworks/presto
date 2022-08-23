using presto;
namespace presto_tests;

public class PDFTest
{
    private const string EXPECTED_PDFS_PATH = @"..\..\..\expected-pdfs\";
    private const string TEST_FILE_TITLE = "testbydeveopler";
    private string _testPdfName = TEST_FILE_TITLE + ".pdf";

    private string GetTestPDFPath(string fileName) => EXPECTED_PDFS_PATH + fileName;
    [Fact]
    public void PDFCreationTest()
    {
        File.Delete(_testPdfName);
        Assert.False(File.Exists(_testPdfName));
        Presto.ToPDF("c", TEST_FILE_TITLE);
        Assert.True(File.Exists(_testPdfName));
    }
    [Fact]
    public void GetRdfIndexTest()
    {
        RawPDF rawPDF = RawPDF.GetRaw(GetTestPDFPath("only-c.pdf"));
        Assert.Equal(23268, rawPDF.RdfIndex);
        rawPDF = RawPDF.GetRaw(GetTestPDFPath("eq-test.pdf"));
        Assert.Equal(23799, rawPDF.RdfIndex);
    }
    [Fact]
    public void PDFEqualityTest()
    {
        RawPDF onlyC = RawPDF.GetRaw(GetTestPDFPath("only-c.pdf"));
        RawPDF onlyCSame = RawPDF.GetRaw(GetTestPDFPath("only-c-same-content.pdf"));
        RawPDF eqTest = RawPDF.GetRaw(GetTestPDFPath("eq-test.pdf"));
        Assert.NotEqual(onlyC, eqTest);
        Assert.Equal(onlyC, onlyCSame);
    }
    [Fact]
    public void SimpleMelodyTest()
    {
        Presto.ToPDF("e e f g", TEST_FILE_TITLE);
        PDFAssert.PDFEqual(_testPdfName, GetTestPDFPath("simple-melody.pdf"));
    }
}

public static class PDFAssert
{
    public static void PDFEqual(string pathToActual, string pathToExpected)
    {
        RawPDF actual = RawPDF.GetRaw(pathToActual);
        RawPDF expected = RawPDF.GetRaw(pathToExpected);
        Assert.Equal(actual, expected);
    }
}