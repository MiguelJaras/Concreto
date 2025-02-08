using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing;
using Microsoft.Win32;

public partial class Utils_Export : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExportPDF();
    }

    void ExportPDF()
    {
        string type = Request.QueryString["type"];
        string titulo = Request.QueryString["Titulos"];
        string impresora = string.Empty;

        if (Request.QueryString["Impresora"] != null)
            impresora = Request.QueryString["Impresora"].ToString();

        string fileName = HttpContext.Current.Server.MapPath("~/Temp/") + titulo + "_" + System.IO.Path.GetRandomFileName().Replace(".", "") + "." + type;


        ExportAPP(type, fileName);

        if (File.Exists(fileName))
        {
            if (impresora != "" && impresora != "DOWNLOAD")
                Print(fileName, impresora);
            else
            DownloadFile(fileName, true);
        }
    }

    void ExportAPP(string ext, string fileName)
    {

        using (Process process = new Process())
        {
            string Reporte = "";
            string DB = "", TipoCrystal = "", Exe = "";
            string pathWebConfig = HttpContext.Current.Server.MapPath("~/Web.config");
            string par = "";
            //string type;

            DB = Request.QueryString["db"];
            Reporte = AppDomain.CurrentDomain.BaseDirectory.Trim() + Request.QueryString["report"].Trim();
            string parametros = Request.QueryString["parameters"].ToString();
            if (Request.QueryString["parameters"] != null)
                TipoCrystal = Request.QueryString["Crystal"].ToString();
            else
                TipoCrystal = "11";

            StreamReader outputReader = null;
            StreamReader errorReader = null; 

            try
            {
                switch (TipoCrystal)
                { 
                    case "11": Exe = HttpContext.Current.Server.MapPath("~/Bin/ExportExe/") + "ExportApp.exe"; break;
                    case "85": Exe = HttpContext.Current.Server.MapPath("~/Bin/ExportExe85/") + "ExportApp.exe"; break;
                    case "1104": Exe = HttpContext.Current.Server.MapPath("~/Bin/ExportExe1104/") + "ExportApp.exe"; break;
                    default: Exe = HttpContext.Current.Server.MapPath("~/Bin/ExportExe/") + "ExportApp.exe"; break;
                }  
                
                par = DB + "¬";
                par += Reporte + "¬";
                par += ext + "¬";
                par += fileName + "¬";
                par += parametros + "¬";
                par += pathWebConfig;

                //UtilFunctions.WriteInFile(Exe + " " + par);
                
                ProcessStartInfo processStartInfo = new ProcessStartInfo(Exe, par);
                processStartInfo.ErrorDialog = false;
                processStartInfo.UseShellExecute = false;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;

                //Execute the process

                process.StartInfo = processStartInfo;
                bool processStarted = process.Start();

                if (processStarted)
                {
                    //Get the output stream
                    outputReader = process.StandardOutput;
                    errorReader = process.StandardError;
                    process.WaitForExit();
                }
                
            }
            catch (Exception ex)
            {
                EventLog m_EventLog = new EventLog("");
                m_EventLog.Source = "ExportApp Fail WEB";
                m_EventLog.WriteEntry("Error ExportApp "+DateTime.Now + " " + ex.Message + " " + Exe + " " + par, EventLogEntryType.FailureAudit);
            }
            finally
            {
                if (outputReader != null)
                {
                    outputReader.Close();
                }
                if (errorReader != null)
                {
                    errorReader.Close();
                }
            }

            //process.Dispose();
            //process.Close(); 
        }
        GC.Collect();
        //GC.WaitForPendingFinalizers();
    }

    private void DownloadFile(string fname, bool forceDownload)
    {
        FileInfo asd = new FileInfo(fname);
        string path = asd.FullName;
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "";
        // set known types based on file extension  
        if (ext != null)
        {
            switch (ext.ToLower())
            {
                case ".xls":
                    type = "application/vnd.ms-excel";
                    break;
                case ".doc":
                    type = "application/msword";
                    break;
                case ".pdf":
                    type = "application/pdf";
                    break;
            }
        }

        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }

        if (type != "")
            Response.ContentType = type;

        Response.WriteFile(path);  
        Response.Flush();

        //if (File.Exists(fname))
        //{
        //    try
        //    {

        //        PrintDocument sad = new PrintDocument();
        //        Session["FileName"] = fname;
        //        sad.PrintPage += new PrintPageEventHandler(sad_PrintPage);
        //        sad.PrinterSettings.PrinterName = Session["Impresora"].ToString();
        //        sad.Print();

        //        Session.Remove("Impresora");
        //        Directory.Delete(asd.DirectoryName, true);

        //    }
        //    catch (IOException ex) { }
        //    //File.Delete(fname);
        //}

        if (File.Exists(fname))
        {
            File.Delete(fname); 
        }
        
        Response.End(); 
       
    }

    public static bool Print(string file, string printer)
    {
        try
        {
            Process.Start(
               Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion" +
                    @"\App Paths\AcroRd32.exe").GetValue("").ToString(),
               string.Format("/h /t \"{0}\" \"{1}\"", file, printer));
            return true;
        }
        catch { }
        return false;
    }

    void sad_PrintPage(object sender, PrintPageEventArgs ev)
    {
        if (Session["FileName"] != null)
        {
            StreamReader streamToPrint = streamToPrint = new StreamReader(Session["FileName"].ToString());
            Session.Remove("FileName");
            Font printFont = new Font("Arial", 10);
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            // Print each line of the file. 
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count *
                   printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page. 
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }
    }
}
