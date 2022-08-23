namespace rawpdflib;
public static class PDFAssert
{
    public static void PDFEqual(string pathToActual, string pathToExpected)
    {
        RawPDF actual = RawPDF.Get(pathToActual);
        RawPDF expected = RawPDF.Get(pathToExpected);
        Assert.Equal(actual, expected);
    }
}