using presto;
namespace presto_tests;

public class PDFTest
{
    private const string EXPECTED_PDFS_PATH = @"..\..\..\expected-pdfs\";
    private const string TEST_FILE_NAME = "testbydeveopler";
    private string _testPdfName = TEST_FILE_NAME + ".pdf";

    private string GetTestPDFPath(string fileName) => EXPECTED_PDFS_PATH + fileName;
    [Fact]
    public void PDFCreationTest()
    {
        File.Delete(_testPdfName);
        Assert.False(File.Exists(_testPdfName));
        Presto.ToPDF("c", TEST_FILE_NAME);
        Assert.True(File.Exists(_testPdfName));
    }
    [Fact]
    public void TestPDFEquality()
    {
        bool equals = PDFUtils.AreEqual(GetTestPDFPath("only-c.pdf"), GetTestPDFPath("only-c-same-content.pdf"));
        Assert.True(equals);

        equals = PDFUtils.AreEqual(GetTestPDFPath("only-c.pdf"), GetTestPDFPath("only-c-identical.pdf"));
        Assert.True(equals);

        equals = PDFUtils.AreEqual(GetTestPDFPath("only-c.pdf"), GetTestPDFPath("eq-test.pdf"));
        Assert.False(equals);
    }
    [Fact]
    public void GetRdfIndexTest()
    {
        string rawPDF = File.ReadAllText(GetTestPDFPath("only-c.pdf"));
        int index = PDFUtils.GetRdfIndex(rawPDF);
        Assert.Equal(23268, index);

        rawPDF = File.ReadAllText(GetTestPDFPath("eq-test.pdf"));
        index = PDFUtils.GetRdfIndex(rawPDF);
        Assert.Equal(23799, index);
    }
}