<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Engineer.aspx.cs" Inherits="TCC_CRM.Masters.Engineer" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="../assets/plugins/jquery/jquery.min.js"></script>

    <style>
        .select2-container {
            box-sizing: border-box;
            display: inline-block;
            margin: 0;
            position: relative;
            vertical-align: middle;
            width: 100% !important;
        }


        body {
            counter-reset: Serial2;
        }

        .auto-index2 td:first-child:before {
            counter-increment: Serial2; /* Increment the Serial counter */
            content: counter(Serial2); /* Display the counter */
        }

        .elementstyle {
            width: 200px;
            opacity: 0.7;
            background-color: #e9ecef;
            border-radius: 4px;
            border: 1px solid #ccc;
            height: 35px;
        }
    </style>
    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
    <script>
        $(function () {
            $(".select2").select2();
            mp_GetData();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(".select2").select2();
                }
            });
        };
    </script>
    <%--    <script>
        $(function () { $("#txtdate").datepicker; });
</script>--%>
    <script type="text/javascript">
        function BACK() {
            var url = "../Transactions/TicketList.aspx";
            window.location.href = url;
        }

        function formatAMPM(date) {
            var hours = date.getHours();
            var minutes = date.getMinutes();
            var ampm = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12;
            hours = hours ? hours : 12; // the hour '0' should be '12'
            minutes = minutes < 10 ? '0' + minutes : minutes;
            var strTime = hours + ':' + minutes + ' ' + ampm;
            return strTime;
        }
        function onInit(s, e) {
            var today = new Date();

            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var y = today.getFullYear();
            var time = formatAMPM(new Date);
            var someFormattedDate = addzeros(dd) + '/' + addzeros(mm) + '/' + y + ' ' + time;
            //alert(someFormattedDate.trim());
            //cinstartdate.SetDate(someFormattedDate);


        }
        function onInit1(s, e) {

            var DateTo = new Date(cinstartdate.GetDate());

        }

    </script>
    <script language="Javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script>
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
        function myFunction() {
            //ProductUpdate();
            var Productdet = new Array();
            var ticketids = getUrlVars()["TicketID"];
            $("#tblstatus tr").each(function () {

                var row = $(this);
                var BETicketStatus = {};
                BETicketStatus.TicketID = ticketids;
                BETicketStatus.status_id = row.find("td").eq(1).text().trim();
                BETicketStatus.hrs = row.find("td").eq(5).text().trim();
                BETicketStatus.remarks = row.find("td").eq(6).text().trim();
                BETicketStatus.starttime = row.find("td").eq(3).text().trim();
                BETicketStatus.endtime = row.find("td").eq(4).text().trim();

                Productdet.push(BETicketStatus);

            });
            document.getElementsByName("tblstatusdetail")[0].value = JSON.stringify(Productdet);

            var Productdetpart = new Array();
            $("#tblpart tr").each(function () {

                var row = $(this);
                var BETicketpartdetail = {};
                BETicketpartdetail.TicketID = ticketids;

                BETicketpartdetail.OrderNo = row.find("td").eq(1).text().trim();
                BETicketpartdetail.old_ref1 = row.find("td").eq(2).text().trim();
                BETicketpartdetail.old_ref2 = row.find("td").eq(3).text().trim();
                BETicketpartdetail.new_ref1 = row.find("td").eq(4).text().trim();
                BETicketpartdetail.new_ref2 = row.find("td").eq(5).text().trim();
                BETicketpartdetail.remarks = row.find("td").eq(6).text().trim();

                Productdetpart.push(BETicketpartdetail);

            });
            document.getElementsByName("tblpartdetail")[0].value = JSON.stringify(Productdetpart);

            var ProdctDocDetail = new Array();
            $("#tbldoclist tr").each(function () {

                var row = $(this);
                var BETicketdocdetail = {};
                BETicketdocdetail.TicketID = ticketids;
                BETicketdocdetail.DocumentPath = row.find("td").eq(2).text().trim();
                BETicketdocdetail.DocumentName = row.find("td").eq(3).text().trim();

                ProdctDocDetail.push(BETicketdocdetail);

            });
            document.getElementsByName("tbldoclistdetail")[0].value = JSON.stringify(ProdctDocDetail);

            var cust = $("#<%=cmbcustomer.ClientID %>").val();
            var prod = $("#<%=cmbproduct.ClientID %>").val();
            var brand = $("#<%=cmbbrand.ClientID %>").val();
            var model = $("#<%=cmbmodel.ClientID %>").val();
            var servname = $("#<%=cmbsev.ClientID %>").val();
            var pvname = $("#<%=cmbpr.ClientID %>").val();
            var service = $("#<%=cmbST.ClientID %>").val();
            var NOP = $("#<%=cmbNOP.ClientID %>").val();
            var assigned = $("#<%=ddlengg.ClientID %>").val();

            if (cust == "null" || cust == null || cust == "Please Select") {

                swal({
                    title: "Customer is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (prod == "null" || prod == null || prod == "Please Select") {

                swal({
                    title: "Product is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (brand == "null" || brand == null || brand == "Please Select") {

                swal({
                    title: "Brand is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (model == "null" || model == null || model == "Please Select") {

                swal({
                    title: "Model is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (service == "null" || service == null || service == "Please Select") {

                swal({
                    title: "Service Type is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (NOP == "null" || NOP == null || NOP == "Please Select") {

                swal({
                    title: "Problem is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            //if (assigned == "null" || assigned == null || assigned == "Please Select") {

            //    swal({
            //        title: "AssignTo is Required",
            //        text: "",
            //        icon: "warning",

            //    });
            //    return false;
            //}
        }

        function mp_GetData() {


            var ticketid = getUrlVars()["TicketID"];

            if (ticketid > 0) {
                $.ajax({

                    type: "POST",
                    url: "../Transactions/Tickets.aspx/TicketEdit",
                    data: JSON.stringify({
                        TicketID: ticketid,
                    }),

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    error: function (jqXHR, sStatus, sErrorThrown) {
                        // window.location.href = "../Login.aspx";
                        alert(sErrorThrown);
                    },

                    success: function (data) {

                        var TicketTable = data.d;
                        // alert(JSON.stringify(TicketTable));

                        $("#<%=lblnewjob.ClientID%>").text('Edit Job');
                        $("#<%=txttno.ClientID %>").val(TicketTable.TicketNo);
                        $("#<%=txtdate.ClientID %>").val(TicketTable.Date);
                        $("#<%=cmbcustomer.ClientID %>").val(TicketTable.CustomerID).trigger('change');
                        $("#<%=cmbproduct.ClientID %>").val(TicketTable.ProductID).trigger('change');

                        $("#<%=hdnbrand.ClientID %>").val(TicketTable.BrandID);
                        $("#<%=cmbST.ClientID %>").val(TicketTable.ServiceTypeID).trigger('change');
                        $("#<%=hdnmodel.ClientID %>").val(TicketTable.ModelID);
                        $("#<%=ddlengg.ClientID %>").val(TicketTable.EngineerID).trigger('change');
                        $("#<%=cmbNOP.ClientID %>").val(TicketTable.ProblemID).trigger('change');
                        $("#<%=txtcs.ClientID %>").val(TicketTable.Status);
                        $("#<%=txtdtlrem.ClientID %>").val(TicketTable.remarks);
                        $("#<%=txtserno.ClientID %>").val(TicketTable.SerialNumber);
                        $("#<%=lblhdncs.ClientID %>").val(TicketTable.StatusID);
                        $("#<%=txtinvamt.ClientID %>").val(TicketTable.invamt);
                        $("#<%=txtinvoiceno.ClientID %>").val(TicketTable.invoiceno);
                        $("#<%=txtrecpamt.ClientID %>").val(TicketTable.recamt);

                        $("#<%=cmbpr.ClientID %>").val(TicketTable.prID).trigger('change');

                        $("#<%=cmbsev.ClientID %>").val(TicketTable.sevID).trigger('change');

                        invdtcin.SetText(TicketTable.invdate);
                        var partrows = "";
                        for (i = 0; i <= TicketTable.partdetails.length - 1; i++) {
                            partrows = partrows + "<tr><td></td><td>" + TicketTable.partdetails[i].old_ref1 + "</td><td>" + TicketTable.partdetails[i].old_ref2 + "</td><td>" + TicketTable.partdetails[i].new_ref1 + "</td><td>" + TicketTable.partdetails[i].new_ref2 + "</td><td>" + TicketTable.partdetails[i].remarks + "</td><td><input type='image'  src='../Images/trash.png' onclick='deleteRowparts(this)'/></td></tr>";
                        }
                        $("#tblpart").empty();
                        $("#tblpart").append(partrows);

                        var statusrows = "";
                        for (j = 0; j <= TicketTable.ticketStatus.length - 1; j++) {
                            statusrows = statusrows + "<tr><td></td><td style='display:none'>" + TicketTable.ticketStatus[j].status_id + "</td><td>" + TicketTable.ticketStatus[j].NewStatus + "</td><td>" + TicketTable.ticketStatus[j].starttime + "</td><td>" + TicketTable.ticketStatus[j].endtime + "</td><td class='calsum'>" + TicketTable.ticketStatus[j].tothrs + "</td><td>" + TicketTable.ticketStatus[j].remarks + "</td><td><input type='image'  src='../Images/trash.png' onclick='deleteRowsatus(this)'/></td></tr>";
                        }
                        $("#tblstatus").empty();
                        $("#tblstatus").append(statusrows);
                        addSerialNumber();

                        var docrows = "";
                        for (k = 0; k <= TicketTable.document.length - 1; k++) {
                            docrows = docrows + "<tr><td></td><td>" + TicketTable.document[k].DocumentName + "</td><td style='display:none'>" + TicketTable.document[k].DocumentPath + "</td><td style='display:none'>" + TicketTable.document[k].DocfullName + "</td><td><a href='javascript:void(0)' onclick=ReortViewer('" + encodeURI(TicketTable.document[k].DocumentPath) + "') >" + TicketTable.document[k].DocfullName + "</a></td><td><input type='image'  src='../Images/trash.png' onclick='deleteRowdocs(this)'/></td></tr>"
                        }
                        $("#tbldoclist").empty();
                        $("#tbldoclist").append(docrows);
                        addnewSerialNumber();

                    }

                });
            }
        }
    </script>
    <script language="Javascript" type="text/javascript">

        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>

    <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">
            <div class="card-header">
                <h4 class="m-b-0 text-white">
                    <asp:Label class="col-md-4 col-form-label" ID="lblnewjob" runat="server" Text="New Job"></asp:Label>

                </h4>

            </div>
            <div class="card-body">
                <div class="form-body">

                    <div class="form-actions">
                        <asp:Label ID="err" runat="server"></asp:Label>
                        <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                            TabIndex="29" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnbtnBackClick" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                            TabIndex="28" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnSaveClick"
                            OnClientClick="return myFunction();" />
                    </div>
                    <br />
                    <br />
                    <ul class="nav nav-tabs customtab" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#details" role="tab" style="color: #0f52b9">
                            <span class="hidden-sm-up"><i class="ti-home"></i></span>
                            <span class="hidden-xs-down"><b>Job Details</b> </span></a></li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#statusupdate" role="tab" style="color: #0f52b9">
                            <span class="hidden-sm-up"><i class="ti-home"></i></span>
                            <span class="hidden-xs-down"><b>Status Update</b> </span></a></li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#partdetails" role="tab" style="color: #0f52b9"><span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down"><b>Part Details</b> </span></a></li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#documents" role="tab" style="color: #0f52b9"><span class="hidden-sm-up"><i class="ti-user"></i></span><span class="hidden-xs-down"><b>Documents</b></span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#invoices" role="tab" style="color: #0f52b9"><span class="hidden-sm-up"><i class="ti-user"></i></span><span class="hidden-xs-down"><b>Billing Details</b></span></a> </li>

                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active " id="details" role="tabpanel">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="container-fluid">
                                        <input type="hidden" id="hdnbrand" name="hdnbrand" runat="server" />
                                        <input type="hidden" id="hdnmodel" name="hdnmodel" runat="server" />
                                        <asp:Label ID="lblticketid" runat="server" Style="visibility: hidden" Text=""></asp:Label>
                                        <div class="form-group row p-t-20">
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Job No<span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:TextBox ID="txttno" runat="server" TabIndex="1" CssClass="form-control" required="required">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Job Date<span class="text-danger" style="font-size: larger;">*</span></label>
                                                <%--  <asp:TextBox ID="txtdate" runat="server" TabIndex="2" MaxLength="30" CssClass="form-control" placeholder="select date" required="required"></asp:TextBox>
                                                --%>
                                                <asp:TextBox runat="server" ID="txtdate" ClientIDMode="Static" TabIndex="2" CssClass="form-control" />
                                            </div>

                                            <div class="col-md-3">
                                                <label class="col-md-12 col-form-label">Reported Date</label>
                                                <dx:ASPxDateEdit ID="Reporteddate" runat="server" Theme="Glass" DateRangeSettings-CalendarColumnCount="8" AutoPostBack="true"
                                                    DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy" Font-Names="Poppins" TabIndex="3" Font-Size="14px" Height="35px" Width="260px">
                                                </dx:ASPxDateEdit>

                                            </div>
                                            <%-- <div class="col-md-3">
                                                <label class="col-md-6 col-form-label"><span class="text-danger" style="font-size: larger;">*</span> </label>--%>
                                            <%--<asp:DropDownList ID="cmbcustomer" runat="server" TabIndex="4" Class="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbcustomer_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                            <%-- </div>--%>
                                            <div class="col-md-3">
                                                <label class="col-md-3 col-form-label">Customer<span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:DropDownList ID="cmbcustomer" runat="server" TabIndex="7" Class="select2 form-control" OnSelectedIndexChanged="cmbcustomer_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-6">
                                                <label class="col-md-6 col-form-label">Customer Address<span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:TextBox ID="txtcustadd" runat="server" TabIndex="5" CssClass="form-control" required="required" ReadOnly="true">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-6 col-form-label">Service Location<span class="text-danger" style="font-size: larger;">*</span></label>

                                                <asp:TextBox ID="txtserlocation" runat="server" MaxLength="200" TabIndex="6" CssClass="form-control"></asp:TextBox>

                                            </div>




                                            <div class="col-md-3">
                                                <label class="col-md-3 col-form-label">Product<span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:DropDownList ID="cmbproduct" runat="server" TabIndex="7" Class="select2 form-control" OnSelectedIndexChanged="cmbproductselectedvalue" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-3 col-form-label">Brand<span class="text-danger" style="font-size: larger;">*</span> </label>
                                                <asp:DropDownList ID="cmbbrand" runat="server" TabIndex="8" Class="select2 form-control" OnSelectedIndexChanged="cmbbrandselectedvalue" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Model <span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:DropDownList ID="cmbmodel" runat="server" TabIndex="9" Class="select2 form-control">
                                                </asp:DropDownList>
                                            </div>



                                            <div class="col-md-3">
                                                <label class="col-md-12 col-form-label">ServiceType <span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:DropDownList ID="cmbST" runat="server" TabIndex="10" Class="select2 form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-12 col-form-label">Nature of Problem <span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:DropDownList ID="cmbNOP" runat="server" TabIndex="11" Class="select2 form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-12 col-form-label">Assigned To <span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:DropDownList ID="ddlengg" runat="server" TabIndex="12" Class="select2 form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-12 col-form-label">Priority<span class="text-danger" style="font-size: larger;">*</span></label>
                                                <asp:DropDownList ID="cmbpr" runat="server" TabIndex="10" Class="select2 form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-12 col-form-label">Severity<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:DropDownList ID="cmbsev" runat="server" TabIndex="11" Class="select2 form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-12 col-form-label">Serial Number <span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtserno" runat="server" TabIndex="13" MaxLength="40" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-12 col-form-label">Part Number <span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtpartno" runat="server" TabIndex="14" MaxLength="40" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <label class="col-md-12 col-form-label">Remarks</label>
                                                <asp:TextBox ID="txtdtlrem" runat="server" TabIndex="15" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                        <asp:Label ID="lblval" runat="server" Text="ticketno" Visible="false"></asp:Label>

                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="tab-pane" id="statusupdate" role="tabpanel">
                                <asp:Label ID="lblhdncs" runat="server" Style="visibility: hidden"></asp:Label>
                                <div class="container-fluid">
                                    <div class="form-group row p-t-10">
                                        <div class="col-md-3">
                                            <label class="col-md-12 col-form-label">Current Status</label>

                                            <asp:TextBox ID="txtcs" runat="server" TabIndex="1" CssClass="form-control" disabled="disabled">
                                            </asp:TextBox>
                                        </div>

                                        <div class="col-md-3">
                                            <label class="col-md-12 col-form-label">New Status</label>
                                            <asp:DropDownList ID="ddlnewstatus" runat="server" TabIndex="2" Class="select2 form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="col-md-12 col-form-label">Remarks</label>
                                            <asp:TextBox ID="txtremarks" runat="server" TabIndex="3" CssClass="form-control" MaxLength="100">
                                            </asp:TextBox>

                                        </div>


                                        <div class="col-md-3">
                                            <label class="col-md-12 col-form-label">Start Time</label>
                                            <dx:ASPxDateEdit ID="startdate" runat="server" ClientInstanceName="cinstartdate" Theme="Glass" EditFormat="Custom" EditFormatString="dd/MM/yyyy hh:mm tt" Height="50%" Width="100%">
                                                <ClientSideEvents Init="onInit" />
                                                <TimeSectionProperties Visible="true">
                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                </TimeSectionProperties>
                                                <CalendarProperties>
                                                    <FastNavProperties DisplayMode="Inline" />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="col-md-12 col-form-label">End Time</label>
                                            <dx:ASPxDateEdit ID="enddate" runat="server" ClientInstanceName="cinenddate" Theme="Glass" EditFormat="Custom" EditFormatString="dd/MM/yyyy hh:mm tt" Height="50%" Width="100%">
                                                <ClientSideEvents Init="onInit1" />
                                                <TimeSectionProperties Visible="true">
                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                </TimeSectionProperties>
                                                <CalendarProperties>
                                                    <FastNavProperties DisplayMode="Inline" />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>

                                        </div>
                                        <div class="col-md-3">
                                            <label class="col-md-12 col-form-label">Total Hours</label>
                                            <asp:TextBox ID="txthrs" MaxLength="2" onkeypress="return onlyNos(event,this);" runat="server" TabIndex="4" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>

                                        <div class="col-sm-2">

                                            <label class="col-md-3 col-form-label">&nbsp;</label>
                                            <button type="button" title="Add" value="Add" onclick="addstatustogrid()" class="btn btn-outline-success btn-rounded" style="float: left; margin-top: 35px;"><i class="fa fa-plus"></i>Add</button>

                                        </div>
                                    </div>
                                    <script>
                                        function addzeros(str) {
                                            if (str < 10) {
                                                str = "0" + str;
                                            }
                                            return str;
                                        }
                                        function clearstatus() {
                                            var vd = "Please Select";
                                            document.getElementById("<%=ddlnewstatus.ClientID %>").value = vd;
                                            $(<%=ddlnewstatus.ClientID %>).trigger("change");
                                            $("#<%=txtremarks.ClientID %>").val("");
                                            $("#<%=txthrs.ClientID %>").val("");
                                            cinstartdate.SetText("");
                                            cinenddate.SetText("");
                                        }
                                        function deleteRowsatus(o) {
                                            var p = o.parentNode.parentNode;
                                            p.parentNode.removeChild(p);
                                            addSerialNumber();
                                        }
                                        function addstatustogrid() {
                                            var rows = "";

                                            var rem = $("#<%=txtremarks.ClientID %>").val();
                                            var hrs = $("#<%=txthrs.ClientID %>").val();
                                            var statusname = $("#<%=ddlnewstatus.ClientID %>").find('option:selected').text();

                                            var statusid = $("#<%=ddlnewstatus.ClientID %>").find('option:selected').val();
                                            var today = new Date();
                                            var dd = today.getDate();
                                            var mm = today.getMonth() + 1;
                                            var y = today.getFullYear();
                                            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                                            var someFormattedDate = addzeros(dd) + '-' + addzeros(mm) + '-' + y + ' ' + time;
                                            var starttime = cinstartdate.GetText();
                                            var endtime = cinenddate.GetText();

                                            if (statusname == "Please Select") { swal("Please Select Status"); return; }
                                            if (starttime == "") { swal("Please Select StartTime"); return; }
                                            if (endtime == "") { swal("Please Select EndTime"); return; }
                                            //if (hrs == "") { swal("Please Enter TotalHours"); return; }
                                            rows = "<tr><td></td><td style='display:none'>" + statusid + "</td><td>" + statusname + "</td><td>" + starttime + "</td><td>" + endtime + "</td><td class='calsum'>" + hrs + "</td><td>" + rem + "</td><td><input type='image'  src='../Images/trash.png' onclick='deleteRowsatus(this)'/></td></tr>";
                                            $("#tblstatus").append(rows);
                                            addSerialNumber();
                                            clearstatus();
                                        }

                                        var addSerialNumber = function () {
                                            var i = 0
                                            $('.auto-index tbody tr').each(function (index) {
                                                $(this).find('td:nth-child(1)').html(index - 1 + 1);
                                            });
                                            AddSumofhours();
                                        };
                                        var addnewSerialNumber = function () {
                                            var i = 0
                                            $('.auto-index3 tr').each(function (index) {
                                                $(this).find('td:nth-child(1)').html(index - 1 + 1);
                                            });

                                        };

                                        var AddSumofhours = function () {
                                            $('#tblstatus tr').each(function () {
                                                var sum = 0
                                                $(this).find('.calsum').each(function () {
                                                    var combat = $(this).text();
                                                    if (!isNaN(combat) && combat.length !== 0) {
                                                        sum += parseFloat(combat);
                                                    }
                                                });
                                                $("#<%=lbltothrs.ClientID %>").text(sum);
                                                $("#<%=hdntothrs.ClientID %>").val(sum);

                                            });
                                        };
                                    </script>

                                    <div class="form-group row table-responsive">
                                        <input type="hidden" name="tblstatusdetail" />
                                        <input type="hidden" id="hdntothrs" name="hdntothrs" runat="server" />
                                        <table class="table nowrap display auto-index">
                                            <thead bgcolor="#0f52b9" style="color: white">
                                                <tr>
                                                    <th>SNO</th>

                                                    <th style="display: none">newstatusid</th>
                                                    <th>Status</th>
                                                    <th>Start Time</th>
                                                    <th>End Time</th>
                                                    <th>Total Hours</th>
                                                    <th>Remarks</th>

                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblstatus">
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="4" style="text-align: right;">Total Summary</td>
                                                    <td colspan="3">
                                                        <asp:Label ID="lbltothrs" runat="server"></asp:Label></td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="partdetails" role="tabpanel">
                                <div class="container-fluid">
                                    <div class="form-group row p-t-20">
                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">Order No</label>
                                            <asp:TextBox ID="txtorderno" runat="server" TabIndex="1" MaxLength="20" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">Old Ref No 1</label>
                                            <asp:TextBox ID="txtoldpno" runat="server" TabIndex="2" MaxLength="20" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">Old Ref No 2</label>
                                            <asp:TextBox ID="txtoldref2" runat="server" TabIndex="3" MaxLength="20" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">New Ref No 1</label>
                                            <asp:TextBox ID="txtnewpno" runat="server" TabIndex="4" MaxLength="20" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>
                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">New Ref No 2</label>
                                            <asp:TextBox ID="txtnewref2" runat="server" TabIndex="5" MaxLength="20" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>
                                        <div class="col-sm-2">
                                            <label class="col-md-3 col-form-label">&nbsp;</label>
                                            <button type="button" title="Add" value="Add" onclick="addpartstogrid()" class="btn btn-outline-success btn-rounded" style="float: left; margin-top: 35px;"><i class="fa fa-plus"></i>Add</button>

                                        </div>
                                        <div class="col-md-6">
                                            <label class="col-md-12 col-form-label">Remarks</label>
                                            <asp:TextBox ID="txtpartrem" runat="server" TabIndex="6" MaxLength="100" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>

                                    </div>
                                    <script>
                                        function addpartstogrid() {
                                            var rows = "";

                                            var orderno = $("#<%=txtorderno.ClientID %>").val();
                                            var oldpart = $("#<%=txtoldpno.ClientID %>").val();
                                            var oldpart2 = $("#<%=txtoldref2.ClientID %>").val();
                                            var newpart = $("#<%=txtnewpno.ClientID %>").val();
                                            var newpart2 = $("#<%=txtnewref2.ClientID %>").val();

                                            var partrem = $("#<%=txtpartrem.ClientID %>").val();

                                            if (orderno == "") { swal("Please Enter OrderNo"); return; }
                                            if (oldpart == "") { swal("Please Enter Old Ref1"); return; }
                                            if (oldpart2 == "") { swal("Please Enter Old Ref2"); return; }
                                            if (newpart == "") { swal("Please Enter New Ref1"); return; }
                                            if (newpart2 == "") { swal("Please Enter New Ref2"); return; }

                                            rows = "<tr><td></td><td>" + orderno + "</td><td>" + oldpart + "</td><td>" + oldpart2 + "</td><td>" + newpart + "</td><td>" + newpart2 + "</td><td>" + partrem + "</td><td><input type='image'  src='../Images/trash.png' onclick='deleteRowparts(this)'/></td></tr>";
                                            $("#tblpart").append(rows);
                                            clearparts();

                                        }
                                        function clearparts() {

                                            $("#<%=txtorderno.ClientID %>").val("");
                                            $("#<%=txtoldpno.ClientID %>").val("");
                                            $("#<%=txtnewpno.ClientID %>").val("");
                                            $("#<%=txtoldref2.ClientID %>").val("");
                                            $("#<%=txtnewref2.ClientID %>").val("");
                                            $("#<%=txtpartrem.ClientID %>").val("");
                                        }
                                        function deleteRowparts(o) {
                                            var p = o.parentNode.parentNode;
                                            p.parentNode.removeChild(p);

                                        }
                                        function deleteRowdocs(o) {
                                            var p = o.parentNode.parentNode;
                                            p.parentNode.removeChild(p);

                                        }
                                    </script>
                                    <div class="form-group row table-responsive">
                                        <input type="hidden" name="tblpartdetail" />
                                        <table class="table nowrap display auto-index2">
                                            <thead bgcolor="#0f52b9" style="color: white">
                                                <tr>
                                                    <th>SNO</th>
                                                    <th>Order No</th>
                                                    <th>Old Ref1</th>
                                                    <th>Old Ref2</th>
                                                    <th>New Ref1</th>
                                                    <th>New Ref2</th>
                                                    <th>Remarks</th>

                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblpart">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="documents" role="tabpanel">

                                <ul class="nav nav-tabs customtab" role="tablist">
                                    <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#uploaddoc" role="tab"><span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down">Upload Document</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#viewdoc" role="tab"><span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down">View Uploaded Documents</span></a></li>

                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active " id="uploaddoc" role="tabpanel">
                                        <div class="container-fluid">
                                            <div class="form-group row p-t-20">
                                                <div align="center" class="col-md-12">
                                                    <table class="table nowrap display">


                                                        <tr>
                                                            <td>
                                                                <asp:FileUpload ID="fileuplaod1" runat="server" AllowMultiple="true" Font-Bold="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="button1" runat="server" Text="Upload" OnClick="button1_Click" Width="82px" Style="visibility: hidden" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="label1" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="labbel2" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Large"></asp:Label><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="label3" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="Large"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="viewdoc" role="tabpanel">
                                        <div class="container-fluid">
                                            <div class="form-group row table-responsive">
                                                <input type="hidden" name="tbldoclistdetail" />
                                                <table class="table nowrap display auto-index3">

                                                    <tr>
                                                        <th bgcolor="#0f52b9" style="color: white">SNO</th>

                                                        <th bgcolor="#0f52b9" style="color: white">Document Name</th>
                                                        <th bgcolor="#0f52b9" style="color: white">View</th>

                                                        <th bgcolor="#0f52b9" style="color: white">Action</th>
                                                    </tr>
                                                    <tbody id="tbldoclist">
                                                    </tbody>
                                                </table>
                                                <script type="text/javascript">
                                                    function ReortViewer(file) {

                                                        var urlsamp = "viewer.aspx?viwerfile=" + file

                                                        $('#abc_frame').attr('src', 'about:blank')
                                                        $.ajax({
                                                            type: "POST",
                                                            url: "viewer.aspx/GetResponse",
                                                            data: '{}',
                                                            contentType: "application/json; charset=utf-8",
                                                            dataType: "json",
                                                            success: function (response) {
                                                                debugger;
                                                                $('#abc_frame').attr('src', urlsamp)
                                                            },
                                                            failure: function (response) {
                                                                alert(response.d);
                                                            }
                                                        });


                                                        $('#modelviewer').modal('toggle');

                                                    }

                                                </script>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="invoices" role="tabpanel">
                                <div class="container-fluid">
                                    <div class="form-group row p-t-20">
                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">InvoiceNo</label>
                                            <asp:TextBox ID="txtinvoiceno" runat="server" TabIndex="1" MaxLength="30" CssClass="form-control">
                                            </asp:TextBox>

                                        </div>

                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">Invoice Date</label>
                                            <dx:ASPxDateEdit ID="invdate" runat="server" Theme="Glass" ClientInstanceName="invdtcin"
                                                AutoResizeWithContainer="true" Font-Names="Poppins" Font-Size="16px">
                                            </dx:ASPxDateEdit>

                                        </div>

                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">Invoice Amount</label>
                                            <asp:TextBox ID="txtinvamt" TabIndex="2" runat="server" CssClass="form-control" MaxLength="9" onkeypress="return onlyNos(event,this);"></asp:TextBox>

                                        </div>

                                        <div class="col-md-2">
                                            <label class="col-md-12 col-form-label">Receipt Amount</label>
                                            <asp:TextBox ID="txtrecpamt" runat="server" CssClass="form-control" MaxLength="9" TabIndex="3" onkeypress="return onlyNos(event,this);"></asp:TextBox>

                                        </div>




                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="modelviewer" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="height: 100%; width: 100%">
                            <div class="modal-dialog modal-lg" style="height: 100%; width: 100%">
                                <div class="modal-content" style="height: 100%; width: 100%">
                                    <div class="form-body" style="height: 100%; width: 100%">

                                        <iframe id="abc_frame" src="gb" style="height: 100%; width: 100%" height="100%" width="100%"></iframe>

                                        <%--  <div class="loader-wrapper" id="loader-1">
                               <div id="loader"></div>
                         </div>--%>



                                        <%--  <div  id='include-from-outside'   >--%>


                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnTicketID" runat="server" />
                    </div>
                </div>
            </div>
            </div>
        </div>
</asp:Content>
