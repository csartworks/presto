namespace presto_tests;

public class PDFTest
{
    protected const string EXPECTED_PDFS_PATH = @"..\..\..\expected-pdfs\";
    protected const string TEST_FILE_TITLE = "testbydeveopler";
    protected string _testPdfName = TEST_FILE_TITLE + ".pdf";

    protected string GetPath(string fileName) => EXPECTED_PDFS_PATH + fileName;
    [Fact]
    private void PDFCreationTest()
    {
        File.Delete(_testPdfName);
        Assert.False(File.Exists(_testPdfName));
        Presto.ToPDF("c", TEST_FILE_TITLE);
        Assert.True(File.Exists(_testPdfName));
    }
    [Fact]
    private void PDFEquality()
    {
        RawPDF pdf1 = RawPDF.Get(GetPath("only-c.pdf"));
        RawPDF pdf2 = RawPDF.Get(GetPath("only-c-same-content.pdf"));
        RawPDF pdf3 = RawPDF.Get(GetPath("eq-test.pdf"));
        Assert.Equal(pdf1, pdf2);
        Assert.NotEqual(pdf1, pdf3);
    }
    protected void Test(string input, string resultPDFName)
    {
        Presto.ToPDF(input, TEST_FILE_TITLE);
        PDFAssert.PDFEqual(_testPdfName, GetPath(resultPDFName));
    }
}
