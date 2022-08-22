using presto;
namespace presto_tests;

public class PDFTest
{
    private const string TEST_FILE_NAME = "testbydeveopler";
    private const string EXPECTED_PDFS_PATH = @"..\..\..\expected-pdfs\";
    private string _testPdfName = TEST_FILE_NAME + ".pdf";

    private string GetExpectedPDFPath(string fileName) => EXPECTED_PDFS_PATH + fileName;

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
        bool equals = PDFUtils.AreEqual(GetExpectedPDFPath("only-c.pdf"), GetExpectedPDFPath("only-c-same-content.pdf"));
        Assert.True(equals);

        equals = PDFUtils.AreEqual(GetExpectedPDFPath("only-c.pdf"), GetExpectedPDFPath("only-c-identical.pdf"));
        Assert.True(equals);

        equals = PDFUtils.AreEqual(GetExpectedPDFPath("only-c.pdf"), GetExpectedPDFPath("eq-1.pdf"));
        Assert.False(equals);
    }
    [Fact]
    public void GetRdfIndexTest()
    {
        int index = PDFUtils.GetRdfIndex(GetExpectedPDFPath("only-c.pdf"));
        Assert.Equal(23268, index);

        index = PDFUtils.GetRdfIndex(GetExpectedPDFPath("eq-1.pdf"));
        Assert.Equal(23799, index);
    }
}