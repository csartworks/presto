public class PDFUtils
{
    private const string RDF_MARK = "<rdf";
    public static int GetRdfIndex(string rawPDF) => rawPDF.IndexOf(RDF_MARK);
    public static bool AreEqual(string filePath1, string filePath2)
    {
        var rawPDF1 = File.ReadAllText(filePath1);
        var rawPDF2 = File.ReadAllText(filePath2);
        int file1RdfIndex = GetRdfIndex(rawPDF1);
        int file2RdfIndex = GetRdfIndex(rawPDF2);
        return rawPDF1[..file1RdfIndex] == rawPDF2[..file2RdfIndex];
    }
}