using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Helper
{
    public class DataReader
    {
        private readonly string _fileName;
        public DataReader(string fileName)
        {
            _fileName = fileName;
        }

         

        public System.Data.DataTable LoadDataTableFromExcelSheet(string sheetName)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var filePath = SystemIOHelper.GetResourceFilePath(_fileName);
            System.Data.DataTable sheetDataTable = null;
            FileStream stream = null;
            IExcelDataReader reader = null;
            using (stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {

                //Check file extension to adjust the reader to the excel file type
                if (Path.GetExtension(filePath).Equals(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (Path.GetExtension(filePath).Equals(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                //DataSet - The result of each spreadsheet will be created in the dataTable.Tables
                DataSet result = reader?.AsDataSet();
                sheetDataTable = result?.Tables[sheetName];

            }
            return sheetDataTable;
        }

        private static OleDbConnection GetConnection(string filename, bool openIt)
        {
            // if your data has no header row, change HDR=NO
            OleDbConnection objConn = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{filename}';Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\" ");
            if (openIt)
                objConn.Open();
            return objConn;
        }

        private static DataSet GetExcelFileAsDataSet(OleDbConnection conn)
        {
            var sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new[] { default, default, default, "TABLE" });
            var ds = new DataSet();
            foreach (DataRow r in sheets.Rows)
                ds.Tables.Add(GetExcelSheetAsDataTable(conn, r["TABLE_NAME"].ToString()));
            return ds;
        }

        private static System.Data.DataTable GetExcelSheetAsDataTable(OleDbConnection conn, string sheetName)
        {
            using (var da = new OleDbDataAdapter($"select * from [{sheetName}]", conn))
            {
                System.Data.DataTable dt = new System.Data.DataTable() { TableName = sheetName.TrimEnd('$') };
                da.Fill(dt);
                return dt;
            }
        }


        //public System.Data.DataTable GetDataTable(string fileName)
        //{
        //    System.Data.DataTable dtResult = null;
        //    int totalSheet = 0; //No of sheets on excel file  
        //    using (OleDbConnection objConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
        //    {
        //        objConn.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        OleDbDataAdapter oleda = new OleDbDataAdapter();
        //        DataSet ds = new DataSet();
        //        System.Data.DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //        string sheetName = string.Empty;
        //        if (dt != null)
        //        {
        //            var tempDataTable = (from dataRow in dt.AsEnumerable()
        //                                 where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
        //                                 select dataRow).CopyToDataTable();
        //            dt = tempDataTable;
        //            totalSheet = dt.Rows.Count;
        //            sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
        //        }
        //        cmd.Connection = objConn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
        //        oleda = new OleDbDataAdapter(cmd);
        //        oleda.Fill(ds, "excelData");
        //        dtResult = ds.Tables["excelData"];
        //        objConn.Close();
        //        return dtResult; //Returning Dattable  
        //    }

        //}
    }
}
