using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Descripción breve de PDFDocument
/// </summary>
public class MergePDF
{
    public MergePDF()
    {

    }

    public byte[] Merge(List<string> lstArchivos, OptionMergePDF option)
    {
        List<byte[]> lstArrArchivos = new List<byte[]>();
        foreach (string strArchivo in lstArchivos)
        {
            lstArrArchivos.Add(File.ReadAllBytes(strArchivo));
        }
        return Merge(lstArrArchivos, option);
    }


    public byte[] Merge(List<byte[]> lstArchivos, OptionMergePDF option)
    {
        MemoryStream stMergePDF = new MemoryStream();
        PdfDocument outputDocument = new PdfDocument();

        foreach (byte[] arrArchivo in lstArchivos)
        {
            using (MemoryStream stArchivo = new MemoryStream(arrArchivo))
            {
                try
                {
                    using (PdfDocument inputDocument = Open(stArchivo, PdfDocumentOpenMode.Import, option))
                    {
                        foreach (PdfPage page in inputDocument.Pages)
                        {
                            outputDocument.AddPage(page);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OptionMergePDF option2 = option;
                    if (option == OptionMergePDF.ITextSharp)
                        option2 = OptionMergePDF.PDFSharp;
                    else
                        option2 = OptionMergePDF.ITextSharp;

                    try
                    {
                        using (PdfDocument inputDocument = Open(stArchivo, PdfDocumentOpenMode.Import, option2))
                        {
                            foreach (PdfPage page in inputDocument.Pages)
                            {
                                outputDocument.AddPage(page);
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        // Save the document...
        outputDocument.Save(stMergePDF, true);
        outputDocument.Dispose();
        outputDocument.Close();
        return stMergePDF.ToArray();

    }


    public PdfDocument Open(MemoryStream sourceStream, PdfDocumentOpenMode openmode, OptionMergePDF option)
    {
        PdfDocument outDoc = null;

        if (option == OptionMergePDF.ITextSharp)
        {
            outDoc = OpenItextSharp(sourceStream, openmode);
        }
        else if (option == OptionMergePDF.PDFSharp)
        {
            outDoc = PdfReader.Open(sourceStream, openmode);
        }

        return outDoc;
    }

    protected PdfDocument OpenItextSharp(MemoryStream sourceStream, PdfDocumentOpenMode openmode)
    {
        PdfDocument outDoc = null;
        sourceStream.Position = 0;
        try
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(sourceStream))
                {
                    using (iTextSharp.text.pdf.PdfStamper pdfStamper = new iTextSharp.text.pdf.PdfStamper(reader, outputStream))
                    {
                        pdfStamper.FormFlattening = true;
                        pdfStamper.Writer.SetPdfVersion(iTextSharp.text.pdf.PdfWriter.PDF_VERSION_1_7);
                        pdfStamper.Writer.CloseStream = false;
                        pdfStamper.Close();
                        outDoc = PdfReader.Open(outputStream, openmode);
                    }
                }
            }
        }
        catch (Exception ex) { outDoc = null; }
        return outDoc;
    }



}

public enum OptionMergePDF
{
    ITextSharp,
    PDFSharp
}
