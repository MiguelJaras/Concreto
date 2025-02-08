using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Text;
using System.Data;

public class ExcelToDataTable
{
    private string _strPath;
    private string _strFileName;

    public string strPath
    {
        get { return _strPath; }
        set { _strPath = value; }
    }

    public string strFileName
    {
        get { return _strFileName; }
        set { _strFileName = value; }
    }


    public ExcelToDataTable(string strPath, string strFileName)
    {
        _strPath = strPath;
        _strFileName = strFileName;
    }

    public DataSet GetTables()
    {
        DataSet ds = new DataSet();
        try
        {
            string strName = Path.GetFileNameWithoutExtension(_strPath + _strFileName);
            strName = RemoveSpecialCharacters(strName);
            List<ISheet> lst = GetAllSheets();
            foreach (ISheet sheet in lst)
            {
                try
                {
                    string strTableName = strName + "_" + RemoveSpecialCharacters(sheet.SheetName);
                    if (sheet.PhysicalNumberOfRows > 0)
                    {
                        DataTable dtSheet = SheetToTable(sheet);
                        dtSheet.TableName = strTableName;
                        ds.Tables.Add(dtSheet);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    protected List<ISheet> GetAllSheets()
    {
        string strPath = _strPath;
        string strFileName = _strFileName;

        List<ISheet> lst = new List<ISheet>();
        string fileExt = Path.GetExtension(strFileName);
        if (fileExt.ToLower() == ".xls")
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strPath + strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
                workbook.MissingCellPolicy = MissingCellPolicy.RETURN_NULL_AND_BLANK;
            }
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                lst.Add(sheet);
            }
        }
        else
        {
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(strPath + strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
                workbook.MissingCellPolicy = MissingCellPolicy.RETURN_NULL_AND_BLANK;
            }
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                lst.Add(sheet);
            }
        }
        return lst;

    }

    public ISheet GetSheet(int index)
    {
        ISheet sheet;
        HSSFWorkbook workbook;
        using (FileStream file = new FileStream(strPath + strFileName, FileMode.Open, FileAccess.Read))
        {
            workbook = new HSSFWorkbook(file);
        }
        return workbook.GetSheetAt(index);
    }


    protected DataTable SheetToTable(ISheet sheet)
    {
        int headerRowindex = 0;
        DataTable dtReturn = new DataTable();
        IRow headerRow = sheet.GetRow(0);
        foreach (ICell headerCell in headerRow)
        {
            //dtReturn.Columns.Add(RemoveSpecialCharacters("c" + headerRowindex.ToString() + "_" + headerCell.ToString()));
            dtReturn.Columns.Add(RemoveSpecialCharacters(headerCell.ToString()));
            headerRowindex++;
        }

        // write the rest
        int rowIndex = 0;
        foreach (IRow row in sheet)
        {
            // skip header row
            if (rowIndex++ == 0) continue;
            DataRow dataRow = dtReturn.NewRow();
            dataRow.ItemArray = row.Cells.Select(c => c.CellType == CellType.Formula ? c.NumericCellValue.ToString() : c.ToString()).ToArray();
            //dataRow.ItemArray = row.Cells.Select(c => c.ToString()).ToArray();
            dtReturn.Rows.Add(dataRow);
        }
        return dtReturn;
    }


    protected string RemoveSpecialCharacters(string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }


    public string CreateScriptTable(string tableName, System.Data.DataTable table, bool bDropTable)
    {
        string sqlsc = "";
        if (bDropTable)
        {
            sqlsc += "IF EXISTS(SELECT object_id FROM SYS.OBJECTS WHERE name = '" + tableName + "' and type = 'u')";
            sqlsc += "\n BEGIN \n DROP TABLE " + tableName;
            sqlsc += "\n END";
        }
        sqlsc += "\n CREATE TABLE " + tableName + "(";
        for (int i = 0; i < table.Columns.Count; i++)
        {
            sqlsc += "\n [" + table.Columns[i].ColumnName + "] ";
            string columnType = table.Columns[i].DataType.ToString();
            switch (columnType)
            {
                case "System.Int32":
                    sqlsc += " int ";
                    break;
                case "System.Int64":
                    sqlsc += " bigint ";
                    break;
                case "System.Int16":
                    sqlsc += " smallint";
                    break;
                case "System.Byte":
                    sqlsc += " tinyint";
                    break;
                case "System.Decimal":
                    sqlsc += " decimal ";
                    break;
                case "System.DateTime":
                    sqlsc += " datetime ";
                    break;
                case "System.String":
                default:
                    sqlsc += string.Format(" nvarchar({0}) ", table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString());
                    break;
            }
            if (table.Columns[i].AutoIncrement)
                sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
            if (!table.Columns[i].AllowDBNull)
                sqlsc += " NOT NULL ";
            sqlsc += ",";
        }
        return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
    }


}
