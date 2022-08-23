namespace rawpdflib;
public class RawPDF
{
    private const string RDF_MARK = "<rdf";
    private const string URI_MARK = "/URI";
    public string[] RawLines { get; init; }
    public string[] FilteredLines { get; init; }
    public RawPDF(string[] rawLines)
    {
        RawLines = rawLines;
        List<string> filteredLines = new List<string>();
        foreach (string line in rawLines)
        {
            if (line.StartsWith(RDF_MARK)) break;
            if (line.StartsWith(URI_MARK)) continue;
            filteredLines.Add(line);
        }
        FilteredLines = filteredLines.ToArray();
    }
    public static RawPDF Get(string path)
    {
        return new RawPDF(File.ReadAllLines(path));
    }

    public override bool Equals(object? obj)
    {
        if (obj is not RawPDF comparison) return false;
        return Enumerable.SequenceEqual(this.FilteredLines, comparison.FilteredLines);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}