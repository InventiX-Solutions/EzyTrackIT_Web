<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="TCC_CRM.Masters.Company" ValidateRequest="false" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>

    <link href="../assets/plugins/bootstrap-datepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script src="../assets/plugins/bootstrap-datepicker/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
    <script language="Javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function BACK() {
            var url = "../Masters/CompanyList.aspx";
            window.location.href = url;
        }
    </script>
    <script>
        $(document).ready(function () {
            $('.dropify').dropify();

            // Translated
            $('.dropify-fr').dropify({
                messages: {
                    default: 'Glissez-déposez un fichier ici ou cliquez',
                    replace: 'Glissez-déposez un fichier ou cliquez pour remplacer',
                    remove: 'Supprimer',
                    error: 'Désolé, le fichier trop volumineux'
                }
            });

            // Used events
            var drEvent = $('#input-file-events').dropify();

            drEvent.on('dropify.beforeClear', function (event, element) {
                return confirm("Do you really want to delete \"" + element.file.name + "\" ?");
            });

            drEvent.on('dropify.afterClear', function (event, element) {
                alert('File deleted');
            });

            drEvent.on('dropify.errors', function (event, element) {
                console.log('Has Errors');
            });

            var drDestroy = $('#input-file-to-destroy').dropify();
            drDestroy = drDestroy.data('dropify')
            $('#toggleDropify').on('click', function (e) {
                e.preventDefault();
                if (drDestroy.isDropified()) {
                    drDestroy.destroy();
                } else {
                    drDestroy.init();
                }
            });


            
            $("[id*=chkEmail]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblchkEmail.ClientID %>').text("Yes");
                }
                else
                {
                    $('#<%=lblchkEmail.ClientID %>').text("No");
                }
            });

            $("[id*=chkMobileMenuAttendance]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblchkMobile_Menu_Attendance.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblchkMobile_Menu_Attendance.ClientID %>').text("No");

                }
            });

            $("[id*=chkMobile_Menu_AttendanceHistry]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblMobile_Menu_AttendanceHistry.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblMobile_Menu_AttendanceHistry.ClientID %>').text("No");

                }
            });


            $("[id*=chkMobile_Menu_NewJob]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblMobile_Menu_NewJob.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblMobile_Menu_NewJob.ClientID %>').text("No");

                }
            });

            $("[id*=chkMobile_Menu_GetMyJobList]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblMobile_Menu_GetMyJobList.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblMobile_Menu_GetMyJobList.ClientID %>').text("No");

                }
            });

            $("[id*=chkMobile_Menu_CompletedJob]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblMobile_Menu_CompletedJob.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblMobile_Menu_CompletedJob.ClientID %>').text("No");

                }
            });

            $("[id*=chkMobile_Menu_MoreDetails]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblMobile_Menu_MoreDetails.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblMobile_Menu_MoreDetails.ClientID %>').text("No");

                }
            });

            $("[id*=chkMobile_Report_Send_to_Mail]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblMobile_Report_Send_to_Mail.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblMobile_Report_Send_to_Mail.ClientID %>').text("No");

                }
            });

            $("[id*=chkMobile_Report_download]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=lblMobile_Report_download.ClientID %>').text("Yes");

                } else {
                    $('#<%=lblMobile_Report_download.ClientID %>').text("No");

                }
            });



        });
    </script>
    <script language="Javascript">
        function isNumber(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <%-- <script type="text/javascript">
        function CheckEmailAddress(value) {
            
            var regex = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            if (value.match(regex)) {
                alert('match');
            }
        }
       </script>--%>




    <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">
            <div class="card-header" style="margin-top: -18px">
                <h4 class="m-b-0 text-white">Edit Company</h4>

            </div>


            <div class="card">
                <%--   <div class="card-header">
                    <h4 class="m-b-0 text-white"><span id="spnCompanyHeader" runat="server">Company</span></h4>
                    <asp:HiddenField ID="hdnViewOnly" runat="server" />
                </div>--%>


                <div class="card-body">
                    <div class="row">
                        <div class="col-6">
                            <asp:Label ID="Label1" runat="server" CssClass="text-danger" Font-Size="Larger"></asp:Label>
                            <ul class="nav nav-tabs customtab" role="tablist" id="companymenu">
                                <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#details" role="tab" style="color: #0f52b9">
                                    <span class="hidden-sm-up"><i class="ti-home"></i></span>
                                    <span class="hidden-xs-down"><b><span id="spnCompanyDetails" runat="server">Company Details</span></b></span></a></li>
                                <li runat="server" id="generallinksettings" class="nav-item"><a class="nav-link" data-toggle="tab" href="#generalsettings" role="tab" style="color: #0f52b9">
                                    <span class="hidden-sm-up"><i class="ti-home"></i></span>
                                    <span class="hidden-xs-down"><b><span id="spnCompanyGensettings" runat="server">Mail Details</span></b> </span></a></li>
                                <li runat="server" class="nav-item" id="MobileDetailslinksettings"><a class="nav-link" data-toggle="tab" href="#MobileDetails" role="tab" style="color: #0f52b9">
                                    <span class="hidden-sm-up"><i class="ti-home"></i></span>
                                    <span class="hidden-xs-down"><b><span id="SpanMobileDetails" runat="server">Mobile Details</span></b> </span></a></li>

                            </ul>
                        </div>
                        <div class="col-6">
                            <div class="form-actions" style="float: right">
                                <asp:Label ID="err" runat="server"></asp:Label>
                                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba" Visible="false"
                                    TabIndex="18" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="btnback_Click" formnovalidate="formnovalidate" />
                                   <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                                    TabIndex="17" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnSaveClick" />
                            </div>
                        </div>

                    </div>

                    <div class="tab-content clearfix">
                        <div class="tab-pane active " id="details" role="tabpanel">
                            <div class="container-fluid">
                                <div class="form-body">
                                 
                                    <%-- <div class="form-actions">
                                        <asp:Label ID="err" runat="server"></asp:Label>
                                        <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba" Visible="false"
                                            TabIndex="18" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="btnback_Click" formnovalidate="formnovalidate" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                                            TabIndex="17" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnSaveClick" />
                                    </div>--%>
                                    <div class="container-fluid">
                                        <div class="form-group row p-t-10">

                                            <asp:Label ID="lblcompanyid" runat="server" Style="visibility: hidden" Text=""></asp:Label>


                                            <div class="col-md-6">
                                                <label class="col-md-6 col-form-label">Company Code<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtccode" runat="server" TabIndex="1" MaxLength="15" CssClass="form-control" placeholder="Enter Code" required="required"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6">
                                                <label class="col-md-6 col-form-label">Company Name<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtcname" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" placeholder="Enter Name" required="required">
                                                </asp:TextBox>
                                            </div>

                                            <div class="col-md-12">
                                                <label class="col-md-6 col-form-label">Address Line 1<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtadd1" runat="server" TabIndex="3" MaxLength="500" CssClass="form-control" placeholder="Enter Address Line 1" required="required">
                                                </asp:TextBox>
                                            </div>

                                            <div class="col-md-12">
                                                <label class="col-md-6 col-form-label">Address Line 2<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtadd2" runat="server" TabIndex="4" MaxLength="500" CssClass="form-control" placeholder="Enter Address Line 2" required="required">
                                                </asp:TextBox>
                                            </div>


                                            <div class="col-md-3">
                                                <label class="col-md-4 col-form-label">City<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtcity" runat="server" TabIndex="5" MaxLength="50" CssClass="form-control" placeholder="Enter City" required="required"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label class="col-md-4 col-form-label">State<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtstate" runat="server" TabIndex="6" MaxLength="50" CssClass="form-control" placeholder="Enter State" required="required">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Country<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtcountry" runat="server" TabIndex="7" MaxLength="50" CssClass="form-control" placeholder="Enter Country" required="required">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-8 col-form-label">Postal Code<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtpostalcode" runat="server" TabIndex="8" MaxLength="6" CssClass="form-control" placeholder="Enter Postal Code" required="required" onkeypress="return isNumberKey(event)">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Phone No<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtphoneno" runat="server" TabIndex="9" MaxLength="15" CssClass="form-control" placeholder="Enter Phone No" required="required" onkeypress="return isNumberKey(event)">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Fax No<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtfaxno" runat="server" TabIndex="10" MaxLength="15" CssClass="form-control" placeholder="Enter Fax No" required="required">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Website<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtwebsite" runat="server" TabIndex="11" MaxLength="50" CssClass="form-control" placeholder="Enter Website" required="required">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label">Email ID<span class="text-danger" style="font-size: larger;"></span></label>
                                                <span id="check"></span>
                                                <asp:TextBox ID="txtmailid" runat="server" TabIndex="12" MaxLength="50" CssClass="form-control" placeholder="Enter Email ID" required="required" onkeydown="CheckEmailAddress(this.value);">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label" title="Business Registration Number">BRN<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txttino" runat="server" TabIndex="13" MaxLength="25" CssClass="form-control" placeholder="Enter Business Registration NO" required="required">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="col-md-6 col-form-label" title="Greenwich Mean Time Zone">GMT<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtGMT" runat="server" TabIndex="14" MaxLength="15" CssClass="form-control" onkeypress="return isNumber(event,this);" placeholder="Enter GMT" required="required">
                                                </asp:TextBox>
                                            </div>
                                             <div class="col-md-2">
                                            <label><span id="spnEmail" runat="server">EMail </span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkEmail" TabIndex="15" />
                                                <asp:Label ID="lblchkEmail" runat="server" AssociatedControlID="chkEmail" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                              <%--<div class="col-md-3">
                                      <label class="col-md-6 col-form-label" title="Email">EMail<span class="text-danger" style="font-size: larger;"></span></label>
                                            <asp:CheckBox runat="server" ID="chkEmail" TabIndex="9" />
                                            <asp:Label ID="lblchkEmail" runat="server" AssociatedControlID="chkEmail" CssClass="checkbox">No</asp:Label>
                                      
                                    </div>--%>

                                            <%--   <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Pan No<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpanno" runat="server" TabIndex="14" MaxLength="15" CssClass="form-control" placeholder="Enter Pan No" required="required">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Service Tax<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtservicetax" runat="server" TabIndex="15" MaxLength="4" CssClass="form-control" placeholder="Enter Service Tax" required="required">
                            </asp:TextBox>
                        </div>--%>
                                            <div class="col-md-12">
                                                <label class="col-md-4 col-form-label">Remarks<span class="text-danger" style="font-size: larger;"></span></label>
                                                <asp:TextBox ID="txtremarks" runat="server" TabIndex="16" MaxLength="1000" CssClass="form-control" placeholder="Enter Remarks" TextMode="MultiLine" Rows="4" required="required">
                                                </asp:TextBox>
                                            </div>
                                            <br />
                                            <div class="col-md-6">
                                                <label class="col-md-6 col-form-label">Company Logo1(Navbar)</label>
                                                <asp:Label ID="lbllogo" runat="server" Visible="false" TabIndex="7"></asp:Label>
                                                <%--  <input type="file" id="UserPerson"  class="dropify" runat="server" />--%>
                                                <asp:FileUpload ID="FileUpload1" runat="server" type="file" class="dropify" />
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-6 col-form-label">Company Logo2(Print)</label>
                                                <asp:Label ID="lbllogo2" runat="server" Visible="false" TabIndex="7"></asp:Label>
                                                <%--  <input type="file" id="UserPerson"  class="dropify" runat="server" />--%>
                                                <asp:FileUpload ID="FileUpload2" runat="server" type="file" class="dropify" />
                                            </div>

                                        </div>
                                        <asp:Label ID="lblval" runat="server" Text="Customer Code" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                    </div>
                                </div>



                            </div>
                        </div>

                        <div class="tab-pane" id="generalsettings" role="tabpanel">
                            <div class="container-fluid">

                                <div class="form-group row">
                                    <div class="form-body">

                                        <div class="form-body">

                                            <div class="container-fluid">
                                                <div class="form-group row p-t-10">

                                                    <asp:Label ID="Label2" runat="server" Style="visibility: hidden" Text=""></asp:Label>



                                                    <div class="col-md-6">
                                                        <label class="col-md-6 col-form-label">SMTP Port<span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtSMTPPort" runat="server" TabIndex="1" MaxLength="100" CssClass="form-control" placeholder="" ></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-md-6 col-form-label">SMTP Host<span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtSMTPHost" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" placeholder="" >
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-md-6 col-form-label">SMTP User Name<span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtSMTPUserName" runat="server" TabIndex="1" MaxLength="100" CssClass="form-control" placeholder=""></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-md-6 col-form-label">SMTP Password <span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtSMTPPassword" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" placeholder="" >
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-md-6 col-form-label">Mail CC<span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtMailCC" runat="server" TabIndex="1" MaxLength="100" CssClass="form-control" placeholder="" ></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-md-6 col-form-label">Mail BCC<span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtMailBCC" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" placeholder="" >
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <label class="col-md-6 col-form-label">Mail Subject <span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtMailSubject" runat="server" TabIndex="1" MaxLength="500" CssClass="form-control" placeholder="" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                    </div>

                                                   <%-- <div class="form-group row">--%>
                                                        <%--<div class="col-md-2">
                                                            <label><span id="spnNewNotificationTemplateEmailContent" runat="server">Email Content</span><span class="text-danger" style="font-size: larger;"></span></label>
                                                        </div>--%>
                                                        <div class="col-md-12" >
                                                             <label class="col-md-6 col-form-label"><span id="spnNewNotificationTemplateEmailContent" runat="server">Email Content</span><span class="text-danger" style="font-size: larger;"></span></label>
                                                            <dx:ASPxHtmlEditor ID="txtMailContent" runat="server" Border-BorderColor="#ced4da" Border-BorderStyle="Solid" Border-BorderWidth="1px" Width="100%" Height="250px" TabIndex="6"></dx:ASPxHtmlEditor>
                                                        </div>
                                                   <%-- </div>--%>

                                                    <%-- <div class="col-md-6">
                                                        <label class="col-md-6 col-form-label">Mail Content<span class="text-danger" style="font-size: larger;"></span></label>
                                                        <asp:TextBox ID="txtMailContent" runat="server" TabIndex="2" MaxLength="999" TextMode="MultiLine" CssClass="form-control" placeholder="" required="required">
                                                        </asp:TextBox>
                                                    </div>--%>
                                                </div>
                                                <asp:Label ID="Label5" runat="server" Text="Customer Code" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="MobileDetails" role="tabpanel">
                            <div class="container-fluid">
                                <div class="form-body">


                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label><span id="spnMobileMenuAttendance" runat="server">Attendance</span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobileMenuAttendance" TabIndex="9" />
                                                <asp:Label ID="lblchkMobile_Menu_Attendance" runat="server" AssociatedControlID="chkMobileMenuAttendance" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <label><span id="spnMobile_Menu_AttendanceHistry" runat="server">Attendance History</span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobile_Menu_AttendanceHistry" TabIndex="10" />
                                                <asp:Label ID="lblMobile_Menu_AttendanceHistry" runat="server" AssociatedControlID="chkMobile_Menu_AttendanceHistry" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label><span id="SpanMobile_Menu_NewJob" runat="server">New Job</span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobile_Menu_NewJob" TabIndex="9" />
                                                <asp:Label ID="lblMobile_Menu_NewJob" runat="server" AssociatedControlID="chkMobile_Menu_NewJob" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <label><span id="SpanMobile_Menu_GetMyJobList" runat="server">Job List</span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobile_Menu_GetMyJobList" TabIndex="10" />
                                                <asp:Label ID="lblMobile_Menu_GetMyJobList" runat="server" AssociatedControlID="chkMobile_Menu_GetMyJobList" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label><span id="SpanMobile_Menu_CompletedJob" runat="server">Completed Job</span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobile_Menu_CompletedJob" TabIndex="9" />
                                                <asp:Label ID="lblMobile_Menu_CompletedJob" runat="server" AssociatedControlID="chkMobile_Menu_CompletedJob" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <label><span id="SpanMobile_Menu_MoreDetails" runat="server">More Details</span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobile_Menu_MoreDetails" TabIndex="10" />
                                                <asp:Label ID="lblMobile_Menu_MoreDetails" runat="server" AssociatedControlID="chkMobile_Menu_MoreDetails" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label><span id="SpanMobile_Report_Send_to_Mail" runat="server">Report Send To Mail </span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobile_Report_Send_to_Mail" TabIndex="9" />
                                                <asp:Label ID="lblMobile_Report_Send_to_Mail" runat="server" AssociatedControlID="chkMobile_Report_Send_to_Mail" CssClass="checkbox">No</asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <label><span id="SpanMobile_Report_download" runat="server">Report download</span><span class="text-danger" style="font-size: larger;"></span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-check form-check-inline">
                                                <asp:CheckBox runat="server" ID="chkMobile_Report_download" TabIndex="10" />
                                                <asp:Label ID="lblMobile_Report_download" runat="server" AssociatedControlID="chkMobile_Report_download" CssClass="checkbox">No</asp:Label>
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
    </div>
    <script language="JavaScript">
        var ev = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/; var x = document.getElementById("check");
        function CheckEmailAddress(email)
        { if (!ev.test(email)) { x.innerHTML = "Invalid Email..!!"; x.style.color = "red" } else { x.innerHTML = "&#10004;"; x.style.color = "green" } } </script>
    <asp:HiddenField ID="hdnCompanyID" runat="server" />
</asp:Content>
