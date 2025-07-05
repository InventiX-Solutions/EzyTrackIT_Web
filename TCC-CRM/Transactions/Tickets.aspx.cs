using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM.Transactions
{
    public partial class Tickets : System.Web.UI.Page
    {
        public BETicket m_BETicket;
        public BLLTicket bllTicket = new BLLTicket();

        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionMgr.DBName))
            {
                hdnClientCode.Value = SessionMgr.ClientCode.ToUpper();

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["statusid"]))
                    {
                        Session["DashboardStatusID"] = Request.QueryString["statusid"];
                    }

                    // ASPxWebControl.RegisterBaseScript(this);  
                    hdnTicketID.Value = Request.QueryString["TicketID"].ToString();
                    hdnID.Value = Request.QueryString["id"].ToString();

                    txtdtlrem.Attributes.Add("maxlength", txtdtlrem.MaxLength.ToString());

                    if (Convert.ToString(Reporteddate.Value) != string.Empty || hdnTicketID.Value.ToString() == "0")
                    {
                        DateTime dtFrom = DateTime.Now;
                        DateTime dtTO = DateTime.Now.AddDays(1);

                        txtdate.Text = dtFrom.ToString("dd/MM/yyyy hh:mm tt");
                        startdate.Value = dtFrom;
                        Reporteddate.Value = dtFrom;
                        ASPx_Call_received_at_C1.Value = dtFrom;
                        enddate.Value = dtTO;

                    }
                    txtdate.Enabled = false;
                    // getTicketlist(Convert.ToInt32((hdnTicketID.Value)));
                    if (Convert.ToInt32(hdnTicketID.Value) != 0)
                    {
                        getReportDate(Convert.ToInt32((hdnTicketID.Value)));


                    }

                    GetCurrentStatus(Convert.ToInt32((hdnTicketID.Value)));


                    if (Convert.ToInt32(hdnTicketID.Value) == 0)
                    {
                        txttno.Text = bllTicket.GetDocumentNumber(SessionMgr.CompanyID, "JOB", "S", SessionMgr.DBName);
                    }
                    txttno.Enabled = false;
                    //cmbstatus.Enabled = false;
                    LoadDefaultVaues();
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        //private void DerializebrandDataTable()
        //{
        //    try
        //    {

        //        DataTable dtLoadList = bllTicket.GetBrandlist(SessionMgr.DBName);
        //        cmbbrand.DataSource = dtLoadList;
        //        cmbbrand.DataTextField = "brand_name";
        //        cmbbrand.DataValueField = "brand_id";
        //        cmbbrand.DataBind();
        //        cmbbrand.Items.Insert(0, "Please Select");
        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMsg(ex.Message);
        //    }

        //}

        private void LoadEngineers()
        {
            DataTable dtLoadList = bllTicket.GetEngineerlist(SessionMgr.DBName);
            ddlengg.DataSource = dtLoadList;
            ddlengg.DataTextField = "engineer_name";
            ddlengg.DataValueField = "engineer_id";
            ddlengg.DataBind();
            ddlengg.Items.Insert(0, "Please Select");
        }

        private void LoadAssignedTo()
        {
            DataTable dtLoadList = bllTicket.GetEngineerlist(SessionMgr.DBName);
            ddlasignedto.DataSource = dtLoadList;
            ddlasignedto.DataTextField = "engineer_name";
            ddlasignedto.DataValueField = "engineer_id";
            ddlasignedto.DataBind();
            ddlasignedto.Items.Insert(0, "Please Select");
        }

        private void LoadDefaultVaues()
        {

            //DerializebrandDataTable();
            //DerializeModelDataTable();
            DerializeproductDataTable();
            DerializeSTDataTable();
            DerializeNOPDataTable();
            DerializeCustomerDataTable();
            DerializestatusDataTable();
            DerializeSevereDataTable();
            DerializePriorityDataTable();
            LoadEngineers();
            LoadAssignedTo();
            DerializecmbJoTysDataTable();


        }
        private void DerializeSevereDataTable()
        {
            try
            {
                DataTable dtLoadList = bllTicket.GetSeverelist(SessionMgr.DBName);
                cmbsev.DataSource = dtLoadList;
                cmbsev.DataTextField = "sevName";
                cmbsev.DataValueField = "sevID";
                cmbsev.DataBind();
                cmbsev.Items.Insert(0, "Please Select");
                //cmbstatus.SelectedValue = "1";

            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }
        }
        private void DerializePriorityDataTable()
        {
            try
            {
                DataTable dtLoadList = bllTicket.GetPrioritylist(SessionMgr.DBName);
                cmbpr.DataSource = dtLoadList;
                cmbpr.DataTextField = "prName";
                cmbpr.DataValueField = "prID";
                cmbpr.DataBind();
                cmbpr.Items.Insert(0, "Please Select");
                //cmbstatus.SelectedValue = "1";

            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }
        }
        protected void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "<b>Upload Success<b/><br/>";
            labbel2.Text = "<b>Upload Failure<b/><br/>";
            label1.Visible = true;
            try
            {
                // Check File Prasent or not  
                if (fileuplaod1.HasFiles)
                {
                    int filecount = 0;
                    int fileuploadcount = 0;
                    //check No of Files Selected  
                    filecount = fileuplaod1.PostedFiles.Count();
                    if (filecount <= 10)
                    {
                        foreach (HttpPostedFile postfiles in fileuplaod1.PostedFiles)
                        {
                            //Get The File Extension  
                            string filetype = Path.GetExtension(postfiles.FileName);
                            if (filetype.ToLower() == ".docx" || filetype.ToLower() == ".pdf" || filetype.ToLower() == ".txt" || filetype.ToLower() == ".doc" || filetype.ToLower() == ".png" || filetype.ToLower() == ".jpg" || filetype.ToLower() == ".xls" || filetype.ToLower() == ".xlsx")
                            {
                                //Get The File Size In Bite  
                                double filesize = postfiles.ContentLength;
                                if (filesize < (1048576))
                                {
                                    fileuploadcount++;
                                    string serverfolder = string.Empty;
                                    string serverpath = string.Empty;
                                    // Adding File Into Scecific Folder Depend On his Extension  
                                    switch (filetype)
                                    {
                                        case ".doc":
                                        case ".docx":
                                            serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\document\");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            fileuplaod1.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- document file uploaded  successfully<br/>";
                                            break;
                                        case ".pdf":
                                            serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\pdf\");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            fileuplaod1.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- PDF file uploaded  successfully<br/>";
                                            break;
                                        case ".txt":
                                            serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\text_document\");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            fileuplaod1.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- Text file uploaded  successfully <br/>";
                                            break;
                                        case ".xlsx":
                                        case ".xls":
                                            serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\excel\");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            fileuplaod1.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- Excel file uploaded  successfully <br/>";
                                            break;
                                        case ".jpg":
                                        case ".gif":
                                        case ".png":
                                            serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\images\");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            fileuplaod1.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- Image file uploaded  successfully <br/>";
                                            break;
                                    }
                                }
                                else
                                {
                                    labbel2.Text += "[" + postfiles.FileName + "]- Upload failed - file size is greater then(1)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
                                }
                            }
                            else
                            {
                                labbel2.Text += "[" + postfiles.FileName + "]- file type must be doc,txt,pdf, xls and png <br/>";
                            }
                        }
                    }
                    else
                    {
                        label1.Visible = false;
                        labbel2.Text = "you are select(" + filecount + ")files <br/>";
                        labbel2.Text += "please select Maximum five(10) files !!!";
                    }
                    label3.Visible = true;
                    label3.Text = "ToTal File =(" + filecount + ")<br/> Uploded file =(" + fileuploadcount + ")<br/> Not Uploaded=(" + (filecount - fileuploadcount) + ")";
                }
                else
                {
                    label1.Visible = false;
                    label3.Visible = false;
                    labbel2.Text = "<b>please select the file for upload !!!</b></br>";
                }
            }
            catch (Exception ex)
            {
                labbel2.Text = ex.Message;
            }
        }

        private void DerializeModelDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetModellist(SessionMgr.DBName);
                cmbmodel.DataSource = dtLoadList;
                cmbmodel.DataTextField = "model_name";
                cmbmodel.DataValueField = "Model_id";
                cmbmodel.DataBind();
                cmbmodel.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }

        private void DerializeproductDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetProductlist(SessionMgr.DBName);
                cmbproduct.DataSource = dtLoadList;
                cmbproduct.DataTextField = "product_name";
                // cmbproduct.DataTextField = "ProductSerialNo";
                cmbproduct.DataValueField = "product_id";
                cmbproduct.DataBind();
                cmbproduct.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void DerializeSTDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetSTlist(SessionMgr.DBName);
                cmbST.DataSource = dtLoadList;
                cmbST.DataTextField = "service_type_name";
                cmbST.DataValueField = "service_typeid";
                cmbST.DataBind();
                cmbST.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void DerializeNOPDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetProblemlist(SessionMgr.DBName);
                cmbNOP.DataSource = dtLoadList;
                cmbNOP.DataTextField = "problem_name";
                cmbNOP.DataValueField = "problem_id";
                cmbNOP.DataBind();
                cmbNOP.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }

        private void DerializecmbJoTysDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetJobTypeslist(SessionMgr.DBName);
                cmbJoTys.DataSource = dtLoadList;
                cmbJoTys.DataTextField = "JobTypes";
                cmbJoTys.DataValueField = "JobTypeId";
                cmbJoTys.DataBind();
                cmbJoTys.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }

        private void DerializeCustomerDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetCustomerlist(SessionMgr.DBName);
                cmbcustomer.DataSource = dtLoadList;
                cmbcustomer.DataTextField = "customer_Name";
                cmbcustomer.DataValueField = "customer_ID";
                cmbcustomer.DataBind();
                cmbcustomer.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }

        private void DerializestatusDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetStatuslist(SessionMgr.DBName);
                ddlnewstatus.DataSource = dtLoadList;
                ddlnewstatus.DataTextField = "StatusName";
                ddlnewstatus.DataValueField = "StatusID";
                ddlnewstatus.DataBind();
                ddlnewstatus.Items.Insert(0, "Please Select");
                //cmbstatus.SelectedValue = "1";

            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void GetCurrentStatus(int TicketID)
        {
            DataTable dt = bllTicket.GetCurrentStatus(TicketID, SessionMgr.DBName);
            if (dt.Rows.Count > 0)
            {
                txtcs.Text = dt.Rows[0]["StatusCode"].ToString();
                lblhdncs.Text = dt.Rows[0]["StatusID"].ToString();
            }

        }
        private void getReportDate(int TicketID)
        {
            try
            {
                DataTable dt = bllTicket.GetReportDate(TicketID, SessionMgr.DBName);
                if (dt.Rows.Count > 0)
                {
                    string date = dt.Rows[0]["ReportDt"].ToString();
                    // AlertMsg(date);
                    //string DateString = "11/12/2009 12:04:34";
                    //IFormatProvider culture = new CultureInfo("en-US", true);
                    //DateTime dateVal = DateTime.ParseExact(DateString, "yyyy-MM-dd hh:mm:ss", culture);

                    //  string[] s3 = date.Split('/');

                    // Reporteddate.Value = Convert.ToDateTime(s3[1] + "/" + s3[0] + "/" + s3[2]);
                    //  Reporteddate.Value = DateTime.ParseExact(date.Trim(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    Reporteddate.Value = date;
                    if (dt.Rows[0]["CallRecivedAt"] != null)
                    {
                        string CallRecivedAt = dt.Rows[0]["CallRecivedAt"].ToString();
                        ASPx_Call_received_at_C1.Value = CallRecivedAt;
                    }
                }
            }
            catch (Exception ex)
            {
                BeCommon.FormName = "Tickets-getReportDate";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }
        }
        private void getTicketlist(int TicketID)
        {
            try
            {
                if (m_BETicket == null) m_BETicket = new BETicket();

                m_BETicket = bllTicket.GetTicket(SessionMgr.CompanyID, TicketID, SessionMgr.DBName);
                if (m_BETicket != null)
                {
                    //txtdate.Text = Convert.ToDateTime(m_BETicket.Date);
                    txttno.Text = m_BETicket.TicketNo;
                    cmbcustomer.SelectedValue = m_BETicket.CustomerID.ToString();
                    cmbbranch.SelectedValue = m_BETicket.BranchID.ToString();
                    cmbproduct.SelectedValue = m_BETicket.ProductName;
                    //cmbproductselectedvalue(null, null);
                    cmbbrand.SelectedValue = m_BETicket.BrandName;
                    //cmbbrandselectedvalue(null, null);
                    cmbmodel.SelectedValue = m_BETicket.ModelName;
                    cmbST.SelectedValue = m_BETicket.ServiceType;



                    cmbNOP.SelectedValue = m_BETicket.NatureOfProblem;

                    cmbJoTys.SelectedValue = m_BETicket.JobTypes;

                    // ddlengg.SelectedValue = m_BETicket.Assigned_to;
                    txtinvamt.Text = m_BETicket.invamt.ToString();
                    txtinvoiceno.Text = m_BETicket.invoiceno.ToString();
                    cmbpr.SelectedValue = m_BETicket.prName;
                    cmbsev.SelectedValue = m_BETicket.sevName;
                    if (!string.IsNullOrEmpty(m_BETicket.invdate.ToString()))
                    {
                        invdate.Value = Convert.ToDateTime(m_BETicket.invdate).ToString("dd/MM/yyyy");
                    }

                    Reporteddate.Value = Convert.ToDateTime(m_BETicket.ReportDate).ToString("dd/MM/yyyy");
                    txtcustadd.Text = m_BETicket.invamt.ToString();
                    txtserlocation.Text = m_BETicket.invamt.ToString();
                    txtpartno.Text = m_BETicket.invamt.ToString();

                }
            }
            catch (Exception ex)
            {
                BeCommon.FormName = "Tickets-getTicketlist";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }
        }

        [WebMethod()]
        public static BETicket TicketEdit(int TicketID)
        {

            BETicket tkt = new BETicket();
            BLLTicket bllTicket = new BLLTicket();
            tkt = bllTicket.GetTicket(SessionMgr.CompanyID, TicketID, SessionMgr.DBName);
            //     ddlengg.SelectedValue = tkt.Assigned_to;


            return tkt;
        }

        //protected void OnbtnBackClick(object sender, EventArgs e)
        //{
        //    if (hdnID.Value == "1")
        //    {
        //        Response.Redirect("~/Transactions/Ticketlist.aspx", false);
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Transactions/Tracking.aspx", false);
        //    }

        //}

        protected void OnbtnBackClick(object sender, EventArgs e)
        {
            string statusId = Session["DashboardStatusID"] as string;

            if (hdnID.Value == "1")
            {
                if (!string.IsNullOrEmpty(statusId))
                {
                    Response.Redirect("~/Transactions/Ticketlist.aspx?statusid=" + statusId, false);
                }
                else
                {
                    Response.Redirect("~/Transactions/Ticketlist.aspx", false);
                }
            }
            else
            {
                Response.Redirect("~/Transactions/Tracking.aspx", false);
            }
        }
       
        protected void OnBtnSaveClick(object sender, EventArgs e)
        {

            try
            {
                if (cmbcustomer.SelectedValue.ToString() == "" || cmbcustomer.SelectedValue.ToString() == "Please Select")
                {
                    AlertMsg("Please select a valid customer.");
                    return;
                }

                //if (cmbbranch.SelectedValue.ToString() == "" || cmbbranch.SelectedValue.ToString() == "Please Select"||hdnbranch.Value == "Please Select"|| hdnbranch.Value == ""|| hdnbranch.Value == "0")
                //{
                //    AlertMsg("Please select a valid branch.");
                //    return;
                //}
                string ddlVal = cmbbranch.SelectedValue?.Trim() ?? "";
                string hdnVal = hdnbranch.Value?.Trim() ?? "";

                bool ddlInvalid = ddlVal == "" || ddlVal.Equals("Please Select", StringComparison.OrdinalIgnoreCase) || ddlVal == "0";
                bool hdnInvalid = hdnVal == "" || hdnVal.Equals("Please Select", StringComparison.OrdinalIgnoreCase) || hdnVal == "0";

                if (ddlInvalid && hdnInvalid)
                {
                    AlertMsg("Please select a valid branch.");
                    return;
                }

                //if (string.IsNullOrWhiteSpace(txtcustadd.Text) || string.IsNullOrWhiteSpace(hdncustadd.Value))
                //{
                //    AlertMsg("Address is required.");
                //    return;
                //}
                if (string.IsNullOrWhiteSpace(txtcustadd.Text) && string.IsNullOrWhiteSpace(hdncustadd.Value))
                {
                    AlertMsg("Address is required.");
                    return;
                }


                if (string.IsNullOrWhiteSpace(txtserlocation.Text))
                {
                    AlertMsg("Service location is required.");
                    return;
                }

                //if (cmbproduct.SelectedValue.ToString() == "" || cmbproduct.SelectedValue.ToString() == "Please Select" || hdnproduct.Value == "Please Select" || hdnproduct.Value == "")
                //{
                //    AlertMsg("Please select a valid product.");
                //    return;
                //}

                //if (cmbbrand.SelectedValue.ToString() == "" || cmbbrand.SelectedValue.ToString() == "Please Select" || hdnbrand.Value == "Please Select" || hdnbrand.Value == "")
                //{
                //    AlertMsg("Please select a valid brand.");
                //    return;
                //}

                //if (cmbmodel.SelectedValue.ToString() == "" || cmbmodel.SelectedValue.ToString() == "Please Select"||hdnmodel.Value == "Please Select" || hdnmodel.Value == "")
                //{
                //    AlertMsg("Please select a valid model.");
                //    return;
                //}

                // For Product dropdown + hidden field
                string ddlProd = cmbproduct.SelectedValue?.Trim() ?? "";
                string hdnProd = hdnproduct.Value?.Trim() ?? "";
                bool ddlProdInvalid = ddlProd == "" || ddlProd.Equals("Please Select", StringComparison.OrdinalIgnoreCase)|| ddlProd=="0";
                bool hdnProdInvalid = hdnProd == "" || hdnProd.Equals("Please Select", StringComparison.OrdinalIgnoreCase )|| hdnProd== "0";

                if (ddlProdInvalid && hdnProdInvalid)
                {
                    AlertMsg("Please select a valid product.");
                    return;
                }

                // For Brand dropdown + hidden field
                string ddlBrand = cmbbrand.SelectedValue?.Trim() ?? "";
                string hdnBrand = hdnbrand.Value?.Trim() ?? "";
                bool ddlBrandInvalid = ddlBrand == "" || ddlBrand.Equals("Please Select", StringComparison.OrdinalIgnoreCase)|| ddlBrand =="0";
                bool hdnBrandInvalid = hdnBrand == "" || hdnBrand.Equals("Please Select", StringComparison.OrdinalIgnoreCase)|| hdnBrand == "0";

                if (ddlBrandInvalid && hdnBrandInvalid)
                {
                    AlertMsg("Please select a valid brand.");
                    return;
                }

                // For Model dropdown + hidden field
                string ddlModel = cmbmodel.SelectedValue?.Trim() ?? "";
                string hdnModel = hdnmodel.Value?.Trim() ?? "";
                bool ddlModelInvalid = ddlModel == "" || ddlModel.Equals("Please Select", StringComparison.OrdinalIgnoreCase)|| ddlModel == "0";
                bool hdnModelInvalid = hdnModel == "" || hdnModel.Equals("Please Select", StringComparison.OrdinalIgnoreCase)|| hdnModel == "0";

                if (ddlModelInvalid && hdnModelInvalid)
                {
                    AlertMsg("Please select a valid model.");
                    return;
                }

                if (cmbST.SelectedValue.ToString() == "" || cmbST.SelectedValue.ToString() == "Please Select")
                {
                    AlertMsg("Please select a valid service type.");
                    return;
                }

                if (cmbNOP.SelectedValue.ToString() == "" || cmbNOP.SelectedValue.ToString() == "Please Select")
                {
                    AlertMsg("Please select a valid nature of problem.");
                    return;
                }

                if (cmbpr.SelectedValue.ToString() == "" || cmbpr.SelectedValue.ToString() == "Please Select")
                {
                    AlertMsg("Please select a valid priority.");
                    return;
                }

                if (cmbsev.SelectedValue.ToString() == "" || cmbsev.SelectedValue.ToString() == "Please Select")
                {
                    AlertMsg("Please select a valid severity.");
                    return;
                }



                hdnTicketID.Value = Request.QueryString["TicketID"].ToString();
                BETicket br = new BETicket();
                List<BETicketStatus> tblstatusdetail_list = new List<BETicketStatus>();
                string strEngineerID = string.Empty;
                string jobAction = "N";

                if (br.ticketStatus == null)
                {
                    //It's null - create it
                    br.ticketStatus = new List<BETicketStatus>();
                }

                if (br.partdetails == null)
                {
                    //It's null - create it
                    br.partdetails = new List<BETicketPartDetails>();
                }
                if (br.document == null)
                {
                    br.document = new List<BETicketDocument>();
                }

                if (!string.IsNullOrEmpty(Request.Form["tblstatusdetail"].ToString()))
                {
                    string tblstatusdetail = Request.Form["tblstatusdetail"];
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(tblstatusdetail);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[dt.Rows.Count - 1]["Status_ID"].ToString() == "2")
                        {
                            jobAction = "C";
                        }

                        tblstatusdetail_list = (from DataRow dr in dt.Rows
                                                select new BETicketStatus()
                                                {
                                                    TicketID = Convert.ToInt32(dr["TicketID"]),
                                                    status_id = Convert.ToInt32(dr["status_id"]),

                                                    remarks = dr["remarks"].ToString(),

                                                    starttime = dr["starttime"].ToString(),
                                                    endtime = dr["endtime"].ToString(),
                                                    tothrs = Convert.ToDecimal(dr["hrs"]),
                                                    Assignedto = Convert.ToInt32(dr["assignedtoid"]),
                                                    ClaimAmount = dr["Claimamount"].ToString(),
                                                }).ToList();
                    }

                    foreach (var item in tblstatusdetail_list)
                    {
                        BETicketStatus pindt = new BETicketStatus();
                        pindt.TicketID = item.TicketID;
                        pindt.status_id = item.status_id;
                        pindt.starttime = item.starttime;
                        pindt.endtime = item.endtime;
                        pindt.remarks = item.remarks;
                        pindt.ModifiedBy = SessionMgr.UserID;
                        pindt.ClaimAmount = item.ClaimAmount;
                        pindt.Assignedto = Convert.ToInt32(item.Assignedto);
                        pindt.tothrs = item.tothrs;
                        br.ticketStatus.Add(pindt);
                        if (!string.IsNullOrEmpty(item.Assignedto.ToString()))
                        {
                            strEngineerID = strEngineerID + "','" + item.Assignedto.ToString();
                        }
                        else
                        {
                            strEngineerID = item.Assignedto.ToString();
                        }
                    }
                }
                List<BETicketPartDetails> tblpartdetail_list = new List<BETicketPartDetails>();
                if (!string.IsNullOrEmpty(Request.Form["tblpartdetail"].ToString()))
                {
                    string tblpartdetail = Request.Form["tblpartdetail"];
                    DataTable dtp = JsonConvert.DeserializeObject<DataTable>(tblpartdetail);

                    if (dtp.Rows.Count > 0)
                    {
                        tblpartdetail_list = (from DataRow dr in dtp.Rows
                                              select new BETicketPartDetails()
                                              {
                                                  TicketID = Convert.ToInt32(dr["TicketID"]),

                                                  OrderNo = dr["OrderNo"].ToString(),
                                                  old_ref1 = dr["old_ref1"].ToString(),
                                                  old_ref2 = dr["old_ref2"].ToString(),
                                                  new_ref1 = dr["new_ref1"].ToString(),
                                                  new_ref2 = dr["new_ref2"].ToString(),
                                                  remarks = dr["remarks"].ToString(),

                                              }).ToList();
                    }

                    foreach (var item in tblpartdetail_list)
                    {
                        BETicketPartDetails pindt = new BETicketPartDetails();


                        pindt.TicketID = item.TicketID;
                        pindt.OrderNo = item.OrderNo;
                        pindt.old_ref1 = item.old_ref1;
                        pindt.old_ref2 = item.old_ref2;
                        pindt.new_ref1 = item.new_ref1;
                        pindt.new_ref2 = item.new_ref2;
                        pindt.remarks = item.remarks;
                        pindt.CreatedBy = SessionMgr.UserID;
                        br.partdetails.Add(pindt);
                    }

                }
                if (!string.IsNullOrEmpty(Request.Form["tbldoclistdetail"].ToString()))
                {
                    string tbldocument = Request.Form["tbldoclistdetail"];
                    DataTable dtd = JsonConvert.DeserializeObject<DataTable>(tbldocument);
                    List<BETicketDocument> tbldocument_list = new List<BETicketDocument>();
                    if (dtd.Rows.Count > 0)
                    {
                        tbldocument_list = (from DataRow dr in dtd.Rows
                                            select new BETicketDocument()
                                            {
                                                TicketID = Convert.ToInt32(dr["TicketID"]),
                                                DocumentName = dr["DocumentName"].ToString(),
                                                DocumentPath = dr["DocumentPath"].ToString(),
                                            }).ToList();
                    }

                    foreach (var item in tbldocument_list)
                    {
                        BETicketDocument pindt = new BETicketDocument();
                        pindt.TicketID = item.TicketID;
                        pindt.DocumentName = item.DocumentName;
                        pindt.DocumentPath = item.DocumentPath;
                        pindt.CreatedBy = SessionMgr.UserID;
                        br.document.Add(pindt);
                    }
                }

                //int intExist = 0;
                int id = 0;

                //string lblid = lblcustomerid.Text;
                string lblid = hdnTicketID.Value;

                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }

                br.TicketDt = txtdate.Text.Trim();
                br.TicketNo = txttno.Text.Trim();
                //br.ProductSerialNo = txtpserialno.Text;
                br.TicketID = id;
                br.CustomerID = Convert.ToInt32(cmbcustomer.SelectedValue);
                br.BranchID = Convert.ToInt32(hdnbranch.Value);
                if (ddlengg.SelectedValue == "Please Select")
                {
                    br.Assigned_to = 0;
                }
                else
                {
                    br.Assigned_to = Convert.ToInt32(ddlengg.SelectedValue);
                }
                if (!string.IsNullOrEmpty(lblhdncs.Text.Trim()))
                {
                    br.StatusID = Convert.ToInt32(lblhdncs.Text);
                }
                br.ModelID = ((hdnmodel.Value == "" || hdnmodel.Value == "Please Select") ? 0 : Convert.ToInt32(hdnmodel.Value));
                br.BrandID = ((hdnbrand.Value == "" || hdnbrand.Value == "Please Select") ? 0 : Convert.ToInt32(hdnbrand.Value));
                br.ProductID = ((hdnproduct.Value == "" || hdnproduct.Value == "Please Select") ? 0 : Convert.ToInt32(hdnproduct.Value));
                br.ServiceType = cmbST.SelectedValue;
                br.ServiceTypeID = ((cmbST.SelectedValue.ToString() == "" || cmbST.SelectedValue.ToString() == "Please Select") ? 0 : Convert.ToInt32(cmbST.SelectedValue));
                br.ProblemID = ((cmbNOP.SelectedValue.ToString() == "" || cmbNOP.SelectedValue.ToString() == "Please Select") ? 0 : Convert.ToInt32(cmbNOP.SelectedValue));
                br.NatureOfProblem = cmbNOP.SelectedValue;

                if (cmbJoTys.SelectedValue != "Please Select" && cmbJoTys.SelectedValue != null)
                {
                    br.JobTypeId = Convert.ToInt32(cmbJoTys.SelectedValue);
                    //br.JobTypes = cmbJoTys.SelectedValue;
                }
                else
                {
                    br.JobTypeId = 0;
                }

                br.remarks = txtdtlrem.Text.Trim();
                br.SerialNumber = txtserno.Text.Trim();

                //--Added mano-- 22-03-2022//

                br.CallRecivedAt = ASPx_Call_received_at_C1.Text.Trim();
                br.NameOfCaller = txtNameofcaller.Text.Trim();

                //--end mano-- 22-03-2022//

                br.ReportDate = Reporteddate.Text.Trim();
                br.PartNo = txtpartno.Text.Trim();

                br.Call_Detail_Nature = txtCall_Detail_Nature.Text.Trim();

                if (!string.IsNullOrEmpty(txtcustadd.Text.Trim()))
                {
                    br.CustomerAddress = txtcustadd.Text.Trim();
                }
                else { br.CustomerAddress = hdncustadd.Value.Trim(); }
                br.ServiceLocation = txtserlocation.Text.Trim();
                br.sevID = ((cmbsev.SelectedValue.ToString() == "" || cmbsev.SelectedValue.ToString() == "Please Select") ? 0 : Convert.ToInt32(cmbsev.SelectedValue));
                br.prID = ((cmbpr.SelectedValue.ToString() == "" || cmbpr.SelectedValue.ToString() == "Please Select") ? 0 : Convert.ToInt32(cmbpr.SelectedValue));
                if (!string.IsNullOrEmpty(txtinvoiceno.Text.Trim()))
                {
                    br.invoiceno = txtinvoiceno.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtinvamt.Text.Trim()))
                {
                    br.invamt = Convert.ToDecimal(txtinvamt.Text.Trim());
                }
                else
                {
                    br.invamt = Convert.ToDecimal("0.00");
                }

                // if(!string.IsNullOrEmpty(invdate.Value.ToString()))
                // {
                string ivdate = Convert.ToDateTime(invdate.Value).ToString("yyyy-MM-dd");
                //br.ProductSerialNo = txtpserialno.Text.Trim();
                br.invdate = ivdate;
                // }

                if (!string.IsNullOrEmpty(txtrecpamt.Text.Trim()))
                {
                    br.recamt = Convert.ToDecimal(txtrecpamt.Text.Trim());
                }
                else
                {
                    br.recamt = Convert.ToDecimal("0.00");
                }

                if (!string.IsNullOrEmpty(txtotherclaimamount.Text.Trim()))
                {
                    br.Otherclaimamount = Convert.ToDecimal(txtotherclaimamount.Text.Trim());
                }
                else
                {
                    br.Otherclaimamount = Convert.ToDecimal("0.00");
                }

                br.CreatedBy = SessionMgr.UserID;
                br.ModifiedBy = SessionMgr.UserID;
                if (!string.IsNullOrEmpty(hdntothrs.Value))
                {
                    br.tothrsspent = Convert.ToDecimal(hdntothrs.Value.ToString());
                }
                else
                {
                    br.tothrsspent = Convert.ToDecimal("0.00");
                }

                int insertorupdate = bllTicket.InsertOrUpdateRecord(br, SessionMgr.DBName);

                if (insertorupdate > 0)
                {
                    try
                    {
                        // Check File Prasent or not  
                        if (fileuplaod1.HasFiles)
                        {
                            int filecount = 0;
                            int fileuploadcount = 0;
                            //check No of Files Selected  
                            filecount = fileuplaod1.PostedFiles.Count();
                            if (filecount <= 10)
                            {
                                foreach (HttpPostedFile postfiles in fileuplaod1.PostedFiles)
                                {
                                    //Get The File Extension  
                                    string filetype = Path.GetExtension(postfiles.FileName);
                                    if (filetype.ToLower() == ".docx" || filetype.ToLower() == ".pdf" || filetype.ToLower() == ".txt" || filetype.ToLower() == ".doc" || filetype.ToLower() == ".png" || filetype.ToLower() == ".jpg" || filetype.ToLower() == ".jpeg" || filetype.ToLower() == ".xls" || filetype.ToLower() == ".xlsx" || filetype.ToLower() == ".xml")
                                    {
                                        //Get The File Size In Bite  
                                        double filesize = postfiles.ContentLength;
                                        if (filesize < (1048576))
                                        {
                                            fileuploadcount++;
                                            string serverfolder = string.Empty;
                                            string serverpath = string.Empty;
                                            // Adding File Into Specific Folder Depend On his Extension  
                                            //switch (filetype)
                                            //{
                                            //case ".doc":
                                            //case ".docx":
                                            serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            fileuplaod1.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- document file uploaded  successfully<br/>";
                                            //    break;
                                            //case ".pdf":
                                            //    serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\");
                                            //    //check Folder avlalible or not  
                                            //    if (!Directory.Exists(serverfolder))
                                            //    {
                                            //        // create Folder  
                                            //        Directory.CreateDirectory(serverfolder);
                                            //    }
                                            //    serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            //    fileuplaod1.SaveAs(serverpath);
                                            //    label1.Text += "[" + postfiles.FileName + "]- PDF file uploaded  successfully<br/>";
                                            //    break;
                                            //case ".txt":
                                            //    serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\");
                                            //    //check Folder avlalible or not  
                                            //    if (!Directory.Exists(serverfolder))
                                            //    {
                                            //        // create Folder  
                                            //        Directory.CreateDirectory(serverfolder);
                                            //    }
                                            //    serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            //    fileuplaod1.SaveAs(serverpath);
                                            //    label1.Text += "[" + postfiles.FileName + "]- Text file uploaded  successfully <br/>";
                                            //    break;
                                            //case ".xlsx":
                                            //case ".xls":
                                            //    serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\");
                                            //    //check Folder avlalible or not  
                                            //    if (!Directory.Exists(serverfolder))
                                            //    {
                                            //        // create Folder  
                                            //        Directory.CreateDirectory(serverfolder);
                                            //    }
                                            //    serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            //    fileuplaod1.SaveAs(serverpath);
                                            //    label1.Text += "[" + postfiles.FileName + "]- Excel file uploaded  successfully <br/>";
                                            //    break;
                                            //case ".jpg":
                                            //case ".gif":
                                            //case ".png":
                                            //    serverfolder = Server.MapPath(@"UploadDocuments\TicketUploads\");
                                            //    //check Folder avlalible or not  
                                            //    if (!Directory.Exists(serverfolder))
                                            //    {
                                            //        // create Folder  
                                            //        Directory.CreateDirectory(serverfolder);
                                            //    }
                                            //    serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            //    fileuplaod1.SaveAs(serverpath);
                                            //    label1.Text += "[" + postfiles.FileName + "]- Image file uploaded  successfully <br/>";
                                            //    break;
                                            //}

                                            BETicketDocument pindt = new BETicketDocument();
                                            pindt.TicketID = insertorupdate;
                                            pindt.DocumentName = postfiles.FileName.ToString();
                                            pindt.DocumentPath = Path.GetFileName(postfiles.FileName).ToString();
                                            pindt.CreatedBy = SessionMgr.UserID;

                                            br.document.Add(pindt);
                                        }
                                        else
                                        {
                                            labbel2.Text += "[" + postfiles.FileName + "]- Upload failed - file size is greater then(1)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
                                        }
                                    }
                                    else
                                    {
                                        labbel2.Text += "[" + postfiles.FileName + "]- file type must be doc,txt,pdf, xls and png <br/>";
                                    }
                                }
                            }
                            else
                            {
                                label1.Visible = false;
                                labbel2.Text = "you are select(" + filecount + ")files <br/>";
                                labbel2.Text += "please select Maximum five(10) files !!!";
                            }
                            label3.Visible = true;
                            label3.Text = "ToTal File =(" + filecount + ")<br/> Uploded file =(" + fileuploadcount + ")<br/> Not Uploaded=(" + (filecount - fileuploadcount) + ")";
                        }
                        else
                        {
                            label1.Visible = false;
                            label3.Visible = false;
                            labbel2.Text = "<b>please select the file for upload !!!</b></br>";
                        }
                    }
                    catch (Exception ex)
                    {
                        BeCommon.FormName = "Tickets";
                        BeCommon.ErrorDescription = ex.Message;
                        BeCommon.ErrorType = ex.Message;
                        BeCommon.CreatedDate = DateTime.Now;

                        m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
                        labbel2.Text = ex.Message;
                    }

                    string document_name = fileuplaod1.FileName;
                    // string DocumentPath = fileuplaod1.SaveAs(MapPath);
                    int insertorupdatedoc = bllTicket.InsertOrUpdateDoc(br, SessionMgr.DBName);
                    if (insertorupdatedoc > 0)
                    {
                        Response.Redirect("~/Transactions/TicketList.aspx?Saved=" + insertorupdatedoc, false);

                        if (SessionMgr.ClientCode != "TCC")
                        {
                            if (id > 0 && jobAction == "N")
                                jobAction = "E";
                            
                            Common.LogInfo(DateTime.Now, "Email Initiated - " + SessionMgr.DBName);
                            Email.SendEmailNotification(insertorupdate, jobAction);
                            Common.LogInfo(DateTime.Now, "Email Completed - " + SessionMgr.DBName);
                        }
                    }

                    try
                    {//push notification - new job
                        PushNotificationSend(id, br.Assigned_to.ToString(), SessionMgr.DBName);
                    }
                    catch (Exception ex)
                    { }
                }
            }
            catch (Exception ex)
            {
                BeCommon.FormName = "Tickets";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }
        }

        #region Alerts
        private void AlertMsg(string Msg)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true);
        }
        #endregion
        public int PushNotificationSend(int ticketid, String userID, string clientcode)
        {
            int m_rowsAffected = 0;
            BLLTicket DBJobs = new BLLTicket();
            List<PushNotificationDetails> mPushNotificationDetails = new List<PushNotificationDetails>();


            try
            {
                DataSet dt = DBJobs.GetPushnotificationDetails(ticketid, userID, clientcode);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    mPushNotificationDetails = Common.ConvertDataTable<PushNotificationDetails>(dt.Tables[0]);

                    Common.FCMPushNotification fcmPush = new Common.FCMPushNotification();

                    foreach (var mPush in mPushNotificationDetails)
                    {
                        String msg = mPush.PushNotification_Content.Replace("@Assigned_to", String.IsNullOrEmpty(mPush.Assigned_to) ? " " : mPush.Assigned_to.ToString()).Replace("@CreateBY", String.IsNullOrEmpty(mPush.CreateBY) ? " " : mPush.CreateBY.ToString()).Replace("@SerialNo", String.IsNullOrEmpty(mPush.TicketNo) ? " " : mPush.TicketNo.ToString());
                        Common.FCMPushNotification ms = fcmPush.SendNotification("EzyTackIT ", msg, "topic", mPush.TokenID, (mPush.engineer_id == null ? "" : mPush.engineer_id.ToString()), mPush.StatusCode.ToString(), mPush.TicketID.ToString());
                    }

                }



            }
            catch (Exception ex)
            {

                m_rowsAffected = 0;
            }

            return m_rowsAffected;
        }

        [WebMethod()]
        public static List<ListItem> GetCustomerList(string CustomerID)
        {

            DataTable dtLoadDropdownvalues = new DataTable();


            BLLTicket bll = new BLLTicket();
            List<ListItem> ListItems = new List<ListItem>();

            if (string.IsNullOrEmpty(CustomerID) || CustomerID == "Please Select")
            {
                return ListItems; // return empty list or handle as needed
            }

            int customerIdParsed;
            if (!Int32.TryParse(CustomerID, out customerIdParsed))
            {
                return ListItems; // return empty list if parsing fails
            }

            dtLoadDropdownvalues = bll.GetCustomerdetails(customerIdParsed, SessionMgr.DBName);
            foreach (DataRow drRow in dtLoadDropdownvalues.Rows)
            {
                ListItems.Add(new ListItem
                {
                    Value = drRow["Value"].ToString(),
                    Text = drRow["Name"].ToString()
                });
            }


            return ListItems;
        }
        [WebMethod()]
        public static branchDeatils GetBranchList(string branchID)
        {
            //branchDeatils mbranchDeatils = new branchDeatils();

            //BLLTicket bll = new BLLTicket();


            //DataTable dtLoadList = bll.GetBranchdetails(Int32.Parse(branchID), SessionMgr.DBName);

            //if (dtLoadList.Rows.Count > 0)
            //{
            //    mbranchDeatils.custAddress = dtLoadList.Rows[0]["Address"].ToString();
            //    mbranchDeatils.serLocation = dtLoadList.Rows[0]["Address"].ToString();
            //}
            //else
            //{
            //    mbranchDeatils.custAddress = "";
            //    mbranchDeatils.serLocation = "";
            //}

            //return mbranchDeatils;
            branchDeatils mbranchDeatils = new branchDeatils();

            if (string.IsNullOrEmpty(branchID) || branchID == "Please Select")
            {
                mbranchDeatils.custAddress = "";
                mbranchDeatils.serLocation = "";
                return mbranchDeatils;
            }

            int branchIdParsed;
            if (!Int32.TryParse(branchID, out branchIdParsed))
            {
                mbranchDeatils.custAddress = "";
                mbranchDeatils.serLocation = "";
                return mbranchDeatils;
            }

            BLLTicket bll = new BLLTicket();
            DataTable dtLoadList = bll.GetBranchdetails(branchIdParsed, SessionMgr.DBName);

            if (dtLoadList.Rows.Count > 0)
            {
                mbranchDeatils.custAddress = dtLoadList.Rows[0]["Address"].ToString();
                mbranchDeatils.serLocation = dtLoadList.Rows[0]["Address"].ToString();
            }
            else
            {
                mbranchDeatils.custAddress = "";
                mbranchDeatils.serLocation = "";
            }

            return mbranchDeatils;
        }
        public class branchDeatils
        {
            public string custAddress { get; set; }
            public string serLocation { get; set; }
        }


        [WebMethod()]
        public static List<ListItem> cmbproductselectedvalue(string prid)
        {

            DataTable dtLoadDropdownvalues = new DataTable();
            BLLTicket bll = new BLLTicket();
            List<ListItem> ListItems = new List<ListItem>();
           

            if (string.IsNullOrEmpty(prid) || prid == "Please Select")
            {
                return ListItems; // return empty list, do NOT allow
            }

            int productId;
            if (!Int32.TryParse(prid, out productId))
            {
                return ListItems; // return empty list if invalid number
            }
            dtLoadDropdownvalues = bll.GetDropDownValues("brand_name", "brand_id", "brands", "product_id = " + productId + "", SessionMgr.DBName);
            foreach (DataRow drRow in dtLoadDropdownvalues.Rows)
            {
                ListItems.Add(new ListItem
                {
                    Value = drRow["Value"].ToString(),
                    Text = drRow["Name"].ToString()
                });
            }


            return ListItems;
        }

        [WebMethod()]
        public static List<ListItem> cmbbrandselectedvalue(string bid, string prid)
        {

            DataTable dtLoadDropdownvalues = new DataTable();
            BLLTicket bll = new BLLTicket();
            List<ListItem> ListItems = new List<ListItem>();
            if (string.IsNullOrEmpty(bid) || bid == "Please Select" || string.IsNullOrEmpty(prid) || prid == "Please Select")
            {
                return ListItems; // return empty list, do NOT allow
            }

            int brandId, productId;

            if (!Int32.TryParse(bid, out brandId) || !Int32.TryParse(prid, out productId))
            {
                return ListItems; // return empty list if invalid number(s)
            }
            dtLoadDropdownvalues = bll.GetDropDownValues("model_name", "model_id", "models", "Brand_ID = " + brandId + " and product_id=" + productId + "", SessionMgr.DBName);
            foreach (DataRow drRow in dtLoadDropdownvalues.Rows)
            {
                ListItems.Add(new ListItem
                {
                    Value = drRow["Value"].ToString(),
                    Text = drRow["Name"].ToString()
                });
            }


            return ListItems;
        }
    }
}