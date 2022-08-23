using Xunit;
namespace rawpdflib;
public static class PDFAssert
{
    public static void PDFEqual(string pathToActual, string pathToExpected)
    {
        RawPDF actual = RawPDF.GetRaw(pathToActual);
        RawPDF expected = RawPDF.GetRaw(pathToExpected);
        Assert.Equal(actual, expected);
    }
}