using ClosedXML.Excel;

using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Xml;
using CRM.Bussiness.Masters;
using CRM.Bussiness;
using CRM.Artifacts;
using TrackIT.ClassModules;

namespace TCC_CRM.Masters
{
    public partial class ImportCustomer : System.Web.UI.Page
    {
        public BLLCustomer bllCustomer = new BLLCustomer();

        public BLLCommon bll = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        public static DataTable gridviewdatatable;
        public static DataTable ExcelData;
        public static DataTable BackupExcelData;
        public static string UploadedExcel = "";
        public string UploadPath1 = "/UploadDocuments/CustomerImport/";
        public static string Constring = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionMgr.DBName))
            {
                if (!IsPostBack)
                {

                }
                bindcompileddata();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void bindcompileddata()
        {
            try
            {
                if (BackupExcelData != null)
                {
                    ASPxGridView1.DataSource = BackupExcelData;
                    ASPxGridView1.DataBind();
                }
            }
            catch (Exception ex)
            {


            }
        }

        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        protected void btnExcelupload_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (FileUploader.PostedFile != null)
                {
                    UploadPath1 = UploadPath1 + "/";
                    string Excelfilepath = "";
                    if (!Directory.Exists(Server.MapPath(UploadPath1)))
                    {
                        Directory.CreateDirectory(Server.MapPath(UploadPath1));
                    }
                    if (!string.IsNullOrEmpty(FileUploader.PostedFile.FileName.ToString()))
                    {
                        string[] str = FileUploader.PostedFile.FileName.Split('\\');
                        string ext = Path.GetExtension(FileUploader.PostedFile.FileName);
                        if (ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX")
                        {
                            FileUploader.PostedFile.SaveAs(Server.MapPath(UploadPath1 + str[str.Length - 1].ToString().Replace(" ", "")));
                            Excelfilepath = Server.MapPath(UploadPath1 + FileUploader.PostedFile.FileName.Replace(" ", ""));
                        }
                        else
                        {
                            lblerror.InnerText = "Only .XLS or .XLSX formats are allowed";
                        }

                        if (!string.IsNullOrEmpty(Excelfilepath))
                        {
                            DataTable dtgrn = new DataTable();

                            DataRow dr = null;
                            dtgrn = AddAutoIncrementColumn();

                            DataTableReader reader = new DataTableReader(ConvertExcelToDataTable(Excelfilepath));
                            dtgrn.Load(reader);
                            dtgrn = RemoveNullColumnFromDataTable(dtgrn);

                            SqlConnection con = new SqlConnection();

                            DataTable compileddata = new DataTable();
                            compileddata = bll.gett_emp_customer_compileddata(dtgrn, SessionMgr.DBName);

                            ExcelData = compileddata;
                            BackupExcelData = compileddata;
                            ASPxGridView1.DataSource = compileddata;
                            ASPxGridView1.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                lblerror.InnerText = ex.Message;
            }
        }

        protected void btnimportdata_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (BackupExcelData != null)
                {
                    DataTable dt = new DataTable();
                    dt = BackupExcelData;
                    DataRow[] foundRows = dt.Select("VerificationStatus='Can be processed'");
                    DataTable dt_clone = new DataTable();
                    dt.TableName = "CloneTable";
                    dt_clone = dt.Clone();

                    if (foundRows.Length > 0)
                    {
                        foreach (DataRow temp in foundRows)
                        {
                            dt_clone.Rows.Add(temp.ItemArray);
                        }



                        string sMessage = string.Empty;
                        int res = bllCustomer.InsertBulkRecord(dt_clone, SessionMgr.UserID, SessionMgr.DBName);
                        if (res == 1)
                        {
                            foreach (DataRow r in dt_clone.Rows)
                            {
                                r["VerificationStatus"] = "Success";
                            }
                            BackupExcelData = dt_clone;
                            bindcompileddata();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "alert('Imported Successfully')", true);
                        }
                        else
                        {
                            string[] msg = sMessage.Split(':');


                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "alert('Something went wrong , please try again')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "alert('All records are incorrect')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "alert('No Records Found')", true);
                }
            }
            catch (Exception ex)
            {

                lblerror.InnerText = ex.Message;
            }
        }

        public static DataTable ConvertExcelToDataTable(string FileName)
        {
            Common cm = new Common();
            DataTable dtResult = null;
            int totalSheet = 0;
            try
            {
                using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + FileName + "';Extended Properties='Excel 12.0 Macro;HDR=YES;IMEX=1;';"))
                {
                    objConn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    System.Data.DataSet ds = new System.Data.DataSet();
                    DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetName = string.Empty;
                    if (dt != null)
                    {
                        var tempDataTable = (from dataRow in dt.AsEnumerable()
                                             where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                             select dataRow).CopyToDataTable();
                        dt = tempDataTable;
                        totalSheet = dt.Rows.Count;
                        sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                    }
                    cmd.Connection = objConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                    oleda = new OleDbDataAdapter(cmd);
                    oleda.Fill(ds, "excelData");
                    dtResult = ds.Tables["excelData"];
                    objConn.Close();
                    return dtResult;
                }
            }
            catch (Exception ex)
            {
                return dtResult;

            }
        }

        public static DataTable RemoveNullColumnFromDataTable(DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][1].ToString() == "")
                    dt.Rows[i].Delete();
            }
            dt.AcceptChanges();
            return dt;
        }

        public static DataTable Duplicate(DataTable dt)
        {
            Common cm = new Common();
            DataTable dtDuplicate = new DataTable();

            try
            {
                dtDuplicate = dt.AsEnumerable()
                               .GroupBy(r => new { UserName = r[0], Password = r[1], KeyType = r[2] })
                               .Where(gr => gr.Count() > 1)
                               .Select(g => g.First()).CopyToDataTable();
            }
            catch (Exception ex)
            {


            }
            finally
            {

            }
            return dtDuplicate;
        }

        public static DataTable Unique(DataTable dt)
        {
            Common cm = new Common();
            DataTable dtUnique = new DataTable();

            try
            {
                dtUnique = dt.AsEnumerable()
                                      .GroupBy(r => new { UserName = r[0], Password = r[1], KeyType = r[2] })
                                      .Where(gr => gr.Count() <= 1)
                                      .Select(g => g.First()).CopyToDataTable();
            }
            catch (Exception ex)
            {


            }
            finally
            {
            }

            return dtUnique;
        }

        private static DataTable AddAutoIncrementColumn()
        {
            DataColumn myDataColumn = new DataColumn();
            myDataColumn.AllowDBNull = false;
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementSeed = 1;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.ColumnName = "SNO";
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.Unique = true;

            //Create a new datatable
            DataTable mydt = new DataTable();

            //Add this AutoIncrement Column to a new datatable
            mydt.Columns.Add(myDataColumn);

            return mydt;
        }

        private SqlConnection CreateConnection(string connStr)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;
            string msg = e.GetValue("VerificationStatus").ToString();
            if (msg == "Data is Incorrect")
                e.Row.ForeColor = System.Drawing.Color.DarkRed;
            if (msg == "Can be processed")
                e.Row.BackColor = System.Drawing.Color.LightCyan;
            if (msg == "Success")
                e.Row.BackColor = System.Drawing.Color.LightCyan;
        }


    }
}