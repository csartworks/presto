public class PDFUtils
{
    public static int GetRdfIndex(string pathToPdf)
    {
        var file = File.ReadAllText(pathToPdf);
        return file.IndexOf("<rdf");
    }

    public static bool AreEqual(string filePath1, string filePath2)
    {
        var file1 = File.ReadAllText(filePath1);
        var file2 = File.ReadAllText(filePath2);
        int file1RdfIndex = GetRdfIndex(filePath1);
        int file2RdfIndex = GetRdfIndex(filePath2);
        return file1[..file1RdfIndex] == file2[..file2RdfIndex];
    }
}