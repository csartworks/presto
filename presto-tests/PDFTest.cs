using presto;
using rawpdflib;
namespace presto_tests;

public class PDFTest
{
    protected const string EXPECTED_PDFS_PATH = @"..\..\..\expected-pdfs\";
    protected const string TEST_FILE_TITLE = "testbydeveopler";
    protected string _testPdfName = TEST_FILE_TITLE + ".pdf";

    protected string GetTestPDFPath(string fileName) => EXPECTED_PDFS_PATH + fileName;
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
    public void CheckPdfLines()
    {
        Assert.Equal(GetFilteredRaw(GetTestPDFPath("only-c.pdf")), GetFilteredRaw(GetTestPDFPath("only-c-same-content.pdf")));

    }
    public string[] GetFilteredRaw(string path)
    {
        string[] lines = File.ReadAllLines(path);
        List<string> filteredLines = new List<string>();
        foreach (string line in lines)
        {
            if (line.StartsWith(RDF_MARK)) break;
            if (line.StartsWith(URI_MARK)) continue;
            filteredLines.Add(line);
        }
        return filteredLines.ToArray();
    }
    private const string RDF_MARK = "<rdf";
    private const string URI_MARK = "/URI";
}
