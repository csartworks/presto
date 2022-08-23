namespace rawpdflib;
public struct RawPDF
{
    private const string RDF_MARK = "<rdf";
    private const string URI_MARK = "/URI";
    public string Raw { get; init; }
    public string RdflessRaw { get; init; }
    public int RdfIndex { get; init; }
    public int URIIndex { get; init; }

    public RawPDF(string raw)
    {
        Raw = raw;
        RdfIndex = raw.IndexOf(RDF_MARK);
        URIIndex = raw.LastIndexOf(URI_MARK);
        RdflessRaw = Raw[URIIndex..RdfIndex];
    }
    public static RawPDF GetRaw(string pathToPDF) => new RawPDF(File.ReadAllText(pathToPDF));

    public override string ToString()
    {
        return Raw;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not RawPDF comparison) return false;
        return this.RdflessRaw == comparison.RdflessRaw;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}