<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" EnableEventValidation="false" AutoEventWireup="true"  CodeBehind="TicketList.aspx.cs" Inherits="TCC_CRM.Transactions.TicketList" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>

    <script type="text/javascript">

        //function OnMoreInfoPOClick(s, e) {

        //    window.location.href = "Tickets.aspx?TicketID=" + e + "&id=1";

        //}

        function OnMoreInfoPOClick(s, e) {
            // Get statusid from the current page URL
            const urlParams = new URLSearchParams(window.location.search);
            const statusid = urlParams.get("statusid");

            // Build the new URL
            let newUrl = "Tickets.aspx?TicketID=" + e + "&id=1";
            if (statusid) {
                newUrl += "&statusid=" + statusid;
            }

            // Redirect
            window.location.href = newUrl;
        }


        function PrintTicket(s, e) {

            url = "TicketPrint.aspx?TicketID=" + e;
            var win = window.open(url, '_blank');
            win.focus();
        }


        function DownloadPDF(s, id, c, emp) {
            debugger;
            //  alert(status)
            //if (status == "8_Closed")
            //    {}
            url = "http://testservices.ezytrackit.com/api/ReportDownload/GetReport?UserId=" + emp + "&clientcode=" + c + "&id=" + id;


            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
    <script type="text/javascript">
        function opennewpage() {
            window.location.href = "~/Transactions/Tickets.aspx?TicketID=0";
        }
        function myFunction() {
            debugger;
            var From = sdate.GetText();
            var To = tdate.GetText();
           

            //assignValueTostatusHiddenField();
            //assignValueToCustomerHiddenField();
            //assignValueToBranchHiddenField();
            //assignValueToProductHiddenField();
            //assignValueToModelHiddenField();
            //assignValueToBrandHiddenField();
            //assignValueToProblemHiddenField();
            //assignValueToEnggHiddenField();
            //assignValueToJobTypeHiddenField();

            if (From == null || From == "" || To == null || To == "") {

                swal({
                    title: "From/To Date is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
        }
        //function OnToolbarItemClick(s, e) {

        //    if (e.item.name == "") {
        //        window.location.href = "~/Transactions/Tickets.aspx?TicketID=0";

        //}


    </script>
    

    <script type="text/javascript">
       

        function assignValueToHiddenField(cmbID, hdnID) {
            var cmbElement = document.getElementById(cmbID);
            var hdnElement = document.getElementById(hdnID);

            if (cmbElement && hdnElement) {
                hdnElement.value = cmbElement.value;
              //  sessionStorage.setItem(hdnID, cmbElement.value);
            }
        }

        function assignValueTostatusHiddenField() {
            assignValueToHiddenField('<%= cmbstatus.ClientID %>', '<%= hdnstatus.ClientID %>');
        }

        function assignValueToCustomerHiddenField() {
            assignValueToHiddenField('<%= cmbcustomer.ClientID %>', '<%= hdncustomer.ClientID %>');
}



function assignValueToProductHiddenField() {
    assignValueToHiddenField('<%= cmbproduct.ClientID %>', '<%= hdnProduct.ClientID %>');
}



function assignValueToProblemHiddenField() {
    assignValueToHiddenField('<%= cmbNOP.ClientID %>', '<%= hdnProblem.ClientID %>');
}

      function assignValueToEnggHiddenField() {
            assignValueToHiddenField('<%= ddlengg.ClientID %>', '<%= hdnEngineer.ClientID %>');
         }

function assignValueToJobTypeHiddenField() {
    assignValueToHiddenField('<%= cmbJoTys.ClientID %>', '<%= hdnJobtype.ClientID %>');
}
    </script>
    <script>

       // $(document).ready(function () {
         //   restoreDropdownSelections();
       // });
        //function btn_go_click() {
        //    debugger;
        // assignValueTostatusHiddenField(); // Assign value for status dropdown
        // assignValueToCustomerHiddenField(); // Assign value for customer dropdown
        // assignValueToBranchHiddenField(); // Assign value for branch dropdown
        // assignValueToProductHiddenField(); // Assign value for product dropdown
        // assignValueToModelHiddenField(); // Assign value for model dropdown
        // assignValueToBrandHiddenField(); // Assign value for brand dropdown
        // assignValueToProblemHiddenField(); // Assign value for problem dropdown
        // assignValueToEnggHiddenField(); // Assign value for engineer dropdown
        // assignValueToJobTypeHiddenField(); // Assign value for job type dropdown

     //}
        </script>
    <script>
        $(function () {
            debugger;
            $(".select2").select2();
          //  restoreDropdownSelections();
         //   mp_GetData();
        });

        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //if (prm != null) {
        //    prm.add_endRequest(function (sender, e) {
        //        if (sender._postBackSettings.panelsToUpdate != null) {
        //            $(".select2").select2();
        //        }
        //    });
        //};
    </script>
   
   
    <%--<script>
        function togglePanel() {
            var panelContent = document.querySelector('.panel-content');
            if (panelContent.style.display === 'none') {
                panelContent.style.display = 'block'; // Show the panel content
            } else {
                panelContent.style.display = 'none'; // Hide the panel content
            }
        }
    </script>--%>
    <script>
        var collapseElement = document.getElementById('collapseExample1');
        var toggleButton = document.querySelector('[data-toggle="collapse"]');

        // Initially hide the collapse element
        collapseElement.classList.remove('show');

        // Add event listener to toggle the collapse on button click
        toggleButton.addEventListener('click', function () {
            // Toggle the show class to open/close the collapse panel
            collapseElement.classList.toggle('show');
            // Toggle the aria-expanded attribute
            var expanded = collapseElement.classList.contains('show') ? 'true' : 'false';
            toggleButton.setAttribute('aria-expanded', expanded);
        });
</script>
    <style>

.with-chevron[aria-expanded='true'] i {
  display: block;
  transform: rotate(180deg) !important;
}

.py-5 {
    padding-bottom: 0rem !important;
}
.py-5 {
    padding-top: 0rem !important;
}
.container {
    max-width: 1433px;
     width: 100%;
    padding-right: 15px;
    /* padding-left: 0px; */
    margin-right: auto;
    margin-left: auto;
}
 .btn-primary
         {
         background-color: #0f52ba;
         }
body {
  min-height: 100vh;
  background-color: #fafafa;
}
.hidden-element {
    display: none;
}
    </style>

    <div class="row" style="width: 100%">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Job List</h4>
                </div>
                <div class="card-body" style="padding: 22px;">
                  <%--  <div class="form-group row p-t-20">--%>
<div id="filterPanel" runat= "server" class="col-lg-12 mb-1">
              <div class="container py-5">
  <!-- For Demo Purpose-->
  <div class="py-5">
    <div class="row">
      <div class="col-lg-12 mb-1">
        <!-- Collapse Panel 1-->
        <a data-toggle="collapse" href="#collapseExample1" role="button" aria-expanded="false" aria-controls="collapseExample1" class="btn btn-primary btn-block py-2 shadow-sm with-chevron">
          <p class="d-flex align-items-center justify-content-between mb-0 px-3 py-2"><strong class="text-uppercase">Filters</strong><i class="fa fa-angle-down"></i></p>
        </a>
        <div id="collapseExample1" class="collapse shadow-sm">
          <div class="card">
            <div class="card-body">
                              <div class="container-fluid">
                           
                                <div class="row">
                                    <%-- <div class="row mb-2">--%>
                                   
                                    <div class="col-md-3">
                                         <label class="col-md-12 col-form-label">From Date</label>
                                        <dx:ASPxDateEdit ID="orderfromdt" runat="server" Theme="Glass" ClientInstanceName="sdate" DateRangeSettings-CalendarColumnCount="8" Height="45%" Width="80%"
                                            DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"  Font-Names="Poppins" Font-Size="13px">
                                        </dx:ASPxDateEdit>
                                    </div>
                                    <div class="col-md-3">
                                         <label class="col-md-12 col-form-label">To Date</label>
                                        <dx:ASPxDateEdit ID="ordertodt" Font-Names="Poppins" Font-Size="13px" ClientInstanceName="tdate" runat="server" Theme="Glass" Height="45%" Width="80%"
                                            DisplayFormatString="dd-MM-yyyy"  EditFormatString="dd-MM-yyyy">
                                        </dx:ASPxDateEdit>
                                    </div>
                                     <div class="col-md-3">
                                        <label>Status</label>
                                        <asp:DropDownList ID="cmbstatus" runat="server" TabIndex="7" Class="select2 form-control" onchange="assignValueTostatusHiddenField()"></asp:DropDownList>
                                        <asp:HiddenField ID="hdnstatus" runat="server" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Customer</label>
                                        <asp:DropDownList ID="cmbcustomer" runat="server" TabIndex="7" Class="select2 form-control" onchange="assignValueToCustomerHiddenField()"></asp:DropDownList>
                                    </div>
                                   <%--</div>
                                    <div class="row mb-2">--%>
                                    <%--<div class="col-md-3 hidden-element">
                                        <label>Branch</label>
                                        <asp:DropDownList ID="cmbbranch" runat="server" TabIndex="8" Class="select2 form-control" onchange="assignValueToBranchHiddenField()"></asp:DropDownList>
                                    </div>--%>
                                    <div class="col-md-3">
                                        <label>Product</label>
                                        <asp:DropDownList ID="cmbproduct" runat="server" TabIndex="7" Class="select2 form-control" onchange="assignValueToProductHiddenField()"></asp:DropDownList>
                                    </div>
                                  <%--  <div class="col-md-3 hidden-element">
                                        <label>Brand</label>
                                        <asp:DropDownList ID="cmbbrand" runat="server" TabIndex="8" Class="select2 form-control" onchange="assignValueToBrandHiddenField()"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 hidden-element">
                                        <label>Model</label>
                                        <asp:DropDownList ID="cmbmodel" runat="server" TabIndex="9" Class="select2 form-control" onchange="assignValueToModelHiddenField()"></asp:DropDownList>
                                    </div>--%>
                                    <%--</div>
                                    <div class="row mb-2">--%>
                                    <div class="col-md-3">
                                        <label>Problem</label>
                                        <asp:DropDownList ID="cmbNOP" runat="server" TabIndex="11" Class="select2 form-control" onchange="assignValueToProblemHiddenField()"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Engineer</label>
                                        <asp:DropDownList ID="ddlengg" runat="server" TabIndex="12" Class="select2 form-control" onchange="assignValueToEnggHiddenField()"></asp:DropDownList>
                                    </div>
                                    <%-- <div class="col-md-2">
                                <label for="lblcmb" class="Themefontstyles">Engineer</label>
                                <asp:DropDownList ID="ddlengg" runat="server" TabIndex="1" CssClass="select2 form-control" onchange="assignValueToEnggHiddenField()" OnSelectedIndexChanged="ddlengg_SelectedIndexChanged" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>--%>
                                             <%--  <asp:Label ID="lblbranch" runat="server" class="Themefontstyles total" Style="text-align: center" Text="Engg" ></asp:Label>
                                 <asp:DropDownList ID="ddlengg" runat="server" TabIndex="3" Class=" form-control" AutoCompleteType="Disabled" onchange="assignValueToEnggHiddenField()">
                                 <asp:ListItem>Please Select</asp:ListItem>
                            </asp:DropDownList>--%>
                                    <div class="col-md-3">
                                        <label>JobType</label>
                                        <asp:DropDownList ID="cmbJoTys" runat="server" TabIndex="11" Class="select2 form-control" onchange="assignValueToJobTypeHiddenField()"></asp:DropDownList>
                                    </div>
                                    
                                    <div class="col-md-3">
                                        <asp:Button ID="btngo" runat="server" Text="Apply" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                            ToolTip="Apply" Style="color: white;  margin-top: 10px; width: 150px;" OnClick="btn_go_click" OnClientClick="return myFunction();"></asp:Button>
                                    </div>
                                 <%--  </div>
                                   <div class="row mb-2">--%>
                                    
                                    <div class="col-md-3">
                                        <asp:Button ID="btnnew" runat="server" Text="Add New" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                            ToolTip="Add New" Style="color: white; margin-top: 10px; width: 150px;" OnClick="btn_new_click"></asp:Button>
                                    </div>
                                    
                                    <div class="col-md-3">
                                        <asp:Button ID="excelxport" runat="server" Text="Download Excel" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                            ToolTip="Download Excel" Style="color: white; margin-top: 10px; width: 150px;" OnClick="excelexport_Click" OnClientClick="return myFunction();" />
                                    </div>
                                      <br />
                                       <div class="col-md-3">
                                        <asp:Button ID="Btnclear" runat="server" Text="Reload" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                            ToolTip="Reload" Style="color: white;  margin-top: 10px; width: 150px;" OnClick="Btnclear_Click"   />
                                    </div>
                                  <%-- </div>--%>
                                </div>
                                  </div>
                         </div>
          </div>
        </div>
      </div>

      

    </div>
  </div>
</div>
</div>

               <%-- </div>--%>
                <div class="table-responsive m-t-1 p-t-10" style="overflow-y: scroll; overflow-x: scroll; width: 100%; min-height: 150px; max-height: 450px">

                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" Theme="DevEx" EnableRowsCache="False" KeyFieldName="TicketID" Font-Names="Poppins" Font-Size="13px">
                        <SettingsPager AlwaysShowPager="True">
                        </SettingsPager>
                        <Settings ShowGroupPanel="true" />
                        <GroupSummary>
                            <dx:ASPxSummaryItem FieldName="TicketID" SummaryType="Count" />
                        </GroupSummary>
                        <Settings ShowFooter="true" GridLines="Both" ShowFilterRow="true" />
                        <%--  <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />--%>
                        <%--          <Toolbars >
                                <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" >
                 
                <Items>
                    <dx:GridViewToolbarItem  Text="Add New"  Command="Custom" ClientVisible="True" ClientEnabled="true">
                         <Image IconID="iconbuilder_actions_addcircled_svg_gray_16x16" >
                        </Image>
                    </dx:GridViewToolbarItem>
                    </Items>
                                    </dx:GridViewToolbar>
                          </Toolbars>--%>
                        <Columns>
                            <%--<dx:GridViewDataTextColumn FieldName="TicketID" VisibleIndex="0" Caption="Print" Width="38px" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                   
                           <a href="javascript:void(0);" title="Print Ticket" id="printicon" onclick="PrintTicket(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>')">
                                                <img src="../images/printicon.png" /></a>
                       
                                       
                                          <a href="javascript:void(0);" title="Download PDF" id="download" onclick="DownloadPDF(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>', '<%# DataBinder.Eval(Container.DataItem,"CompanyCode") %>', '<%# DataBinder.Eval(Container.DataItem,"engineer_id") %>')">
                                             <img src="../Images/download.png" height="15" /></a>                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn FieldName="TicketID" VisibleIndex="0" Caption="Print" Width="38px" CellStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <a href="javascript:void(0);" title="Print Ticket" id="printicon" onclick="PrintTicket(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>')">
                                        <img src="../images/printicon.png" /></a>

                                    <%--    <a href="javascript:void(0);" title="Download PDF" id="download" onclick="DownloadPDF(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>', '<%# DataBinder.Eval(Container.DataItem,"CompanyCode") %>', '<%# DataBinder.Eval(Container.DataItem,"engineer_id") %>')">
                                             <img src="../Images/download.png" height="15"  /></a>--%>

                                    <dx:ASPxImage runat="server" ID="image" OnInit="Image_Init"></dx:ASPxImage>

                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn FieldName="TicketNo" VisibleIndex="1" Caption="Job No" CellStyle-HorizontalAlign="Center" Width="50px" SortOrder="Descending">
                                <DataItemTemplate>
                                    <%--  <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />--%>
                                    <a href="javascript:void(0);" onclick="OnMoreInfoPOClick(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>')">
                                        <%# DataBinder.Eval(Container.DataItem,"TicketNo") %></a>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="Date" Caption="Job Date" ReadOnly="True" Width="50px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" CellStyle-Wrap="False">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>

                            <dx:GridViewDataTextColumn FieldName="JobTypes" Caption="JobType" ReadOnly="True" Width="50px" VisibleIndex="4" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataDateColumn FieldName="ReportDt" Caption="Reported Date" ReadOnly="True" Width="50px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" CellStyle-Wrap="False">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>

                            <%--  <dx:GridViewDataTextColumn FieldName="TicketNo" Caption="TicketNo" ReadOnly="True" Width="50px" VisibleIndex="1"  CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>--%>

                            <dx:GridViewDataTextColumn FieldName="customer_Name" Caption="Customer" ReadOnly="True" Width="50px" VisibleIndex="6" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="customer_branch_Name" Caption="Branch" ReadOnly="True" Width="50px" VisibleIndex="7" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataDateColumn FieldName="CallRecivedAt" Caption="Call received" ReadOnly="True" Width="50px" VisibleIndex="8" CellStyle-HorizontalAlign="Left" CellStyle-Wrap="False">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>

                            <dx:GridViewDataTextColumn FieldName="NameOfCaller" Caption="Caller Name" ReadOnly="True" Width="50px" VisibleIndex="8" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn FieldName="Call_Detail_Nature" Caption="Call Details" ReadOnly="True" Width="50px" VisibleIndex="9" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>


                            <dx:GridViewDataTextColumn FieldName="problem_name" Caption="Problem" ReadOnly="True" Width="50px" VisibleIndex="10" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <%-- <dx:GridViewDataTextColumn FieldName="NameOfCaller" Caption="Name of caller" ReadOnly="True" Width="50px" VisibleIndex="15" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataTextColumn>--%>


                            <%-- <dx:GridViewDataDateColumn FieldName="CallRecivedAt" Caption="Call received at" ReadOnly="True" Width="50px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" CellStyle-Wrap="False">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss" EditFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>--%>

                            <dx:GridViewDataTextColumn FieldName="product_name" Caption="Product" ReadOnly="True" Width="50px" VisibleIndex="11" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn FieldName="brand_name" Caption="Brand" ReadOnly="True" Width="50px" VisibleIndex="12" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn FieldName="model_name" Caption="Model" ReadOnly="True" Width="50px" VisibleIndex="13" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>



                            <dx:GridViewDataTextColumn FieldName="CurrentStatus" Caption="CurrentStatus" ReadOnly="True" Width="50px" VisibleIndex="14" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn FieldName="tothrs" Caption="Hours Spent" ReadOnly="True" Width="50px" VisibleIndex="15" CellStyle-HorizontalAlign="Right">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Invoice" Caption="Invoice" ReadOnly="True" Width="50px" VisibleIndex="16" CellStyle-HorizontalAlign="Center">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="invoice_amt" Caption="InvoiceAmt" ReadOnly="True" Width="50px" VisibleIndex="17" CellStyle-HorizontalAlign="Right">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PartNo" Caption="PartNo" ReadOnly="True" Width="50px" VisibleIndex="19" CellStyle-HorizontalAlign="Center">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SerialNumber" Caption="SerialNumber" ReadOnly="True" Width="30px" VisibleIndex="18" CellStyle-HorizontalAlign="Center">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Receipt_amt" Caption="ReceiptAmt" ReadOnly="True" Width="30px" VisibleIndex="20" CellStyle-HorizontalAlign="Right">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="service_type_name" Caption="Service Type" ReadOnly="True" Width="50px" VisibleIndex="22" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>

                            <%--  <dx:GridViewDataTextColumn FieldName="problem_name" Caption="Problem" ReadOnly="True" Width="50px" VisibleIndex="15" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataTextColumn>--%>

                            <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" ReadOnly="True" Width="50px" VisibleIndex="23" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="engineer_name" Caption="Engineer" ReadOnly="True" Width="50px" VisibleIndex="21" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>
                            
                        </Columns>
                    </dx:ASPxGridView>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdncustomer" runat="server" />
    <asp:HiddenField ID="hdnBranch" runat="server" />
    <asp:HiddenField ID="hdnProduct" runat="server" />
    <asp:HiddenField ID="hdnModel" runat="server" />
    <asp:HiddenField ID="hdnBrand" runat="server" />
    <asp:HiddenField ID="hdnProblem" runat="server" />
    <asp:HiddenField ID="hdnEngineer" runat="server" />
    <asp:HiddenField ID="hdnJobtype" runat="server" />
    </div>
   
</asp:Content>
