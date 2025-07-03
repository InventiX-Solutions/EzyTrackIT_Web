<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="NewNotificationTemplate.aspx.cs" Inherits="TCC_CRM.Masters.NewNotificationTemplate" ValidateRequest="false" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../assets/plugins/bootstrap-datepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script src="../assets/plugins/bootstrap-datepicker/bootstrap-datepicker.min.js"></script>
    <style>
        .customfont {
            font-family: sans-serif;
            font-size: 16px;
        }

        .select2-container {
            box-sizing: border-box;
            display: inline-block;
            margin: 0;
            position: relative;
            vertical-align: middle;
            width: 100% !important;
        }

        .dropify-message p {
            text-align: center !important;
        }
    </style>

    <style>
        .select2-container
        {
            box-sizing: border-box;
            display: inline-block;
            margin: 0;
            position: relative;
            vertical-align: middle;
            width: 100% !important;
        }

        .tab-pane.active
        {
            animation: slide-down 2s ease-out;
        }

        .cellMinWidthCss
        {
            min-width: 150px;
        }

        .passNoMinWidthCss
        {
            min-width: 50px;
        }

        input:focus,
        select:focus
        {
            -moz-box-shadow: none !important;
            -webkit-box-shadow: none !important;
            box-shadow: none !important;
            border: 1px solid #E53935 !important;
            outline-width: 0 !important;
        }

        .custom
        {
            width: 760px;
            min-height: 400px;
        }

        .table_
        {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

        .td_, .th_
        {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 2px;
            font-size: 13px;
            text-transform: capitalize;
        }

        .th_
        {
            font-size: 15px;
        }

        .msbdd
        {
            font-family: WMitraBold;
            font-weight: normal;
            color: #000;
            font-size: 12px;
            text-align: right !important;
            direction: rtl;
            width: 100%;
            border: 1px solid #aaa;
            outline: none;
            box-sizing: border-box;
            background-color: rgba(255,255,255,0.7);
        }

        .tab_font
        {
            color: #0f52b9;
        }

        .table td
        {
            padding: .75rem !important;
            vertical-align: middle !important;
        }

        .rtable th
        {
            font-size: 16px;
            text-align: left;
            /*text-transform: uppercase;*/
            background: #f2f0e6;
        }

        .rtable th,
        .rtable td
        {
            vertical-align: middle !important;
            border: 1px solid #d9d7ce;
            font-weight: normal;
        }
    </style>

    <script type="text/javascript">
        $(function () {

            //$('[id*=cmbTemplateType]').blur(function (e) {
            //    var code = $("#<=cmbTemplateType.ClientID %>").val();
            //    var idval = getParameterByName('id');
            //    if (idval == "" || idval == null) {
            //        idval = 0;
            //    }
            //    if (idval == 0) {
            //        mp_GetData(code);
            //    }
            //});


            $('.mybtn').on('click', function (event) {
                event.preventDefault();
                var url = $(this).data('target');

                location.replace(url);
            });

            $("[id*=chkWhatsApp]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=txtWhatsappContent.ClientID %>').removeAttr("disabled");
                    $('#<%=divWhatsappRecipientType.ClientID %>').removeAttr("disabled");
                } else {
                    $('#<%=txtWhatsappContent.ClientID %>').attr("disabled", "disabled");
                    $('#<%=divWhatsappRecipientType.ClientID %>').attr("disabled", "disabled");
                }
            });

            $("[id*=chkEmail]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=txtMailSubject.ClientID %>').removeAttr("disabled");
                    $('#<%=txtEmailContent.ClientID %>').removeAttr("disabled");
                    $('#<%=divEmailRecipientType.ClientID %>').removeAttr("disabled");
                } else {
                    $('#<%=txtMailSubject.ClientID %>').attr("disabled", "disabled");
                    $('#<%=txtEmailContent.ClientID %>').attr("disabled", "disabled");
                    $('#<%=divEmailRecipientType.ClientID %>').attr("disabled", "disabled");
                }
            });

            $("[id*=chkSMS]").click(function () {
                if ($(this).is(":checked")) {
                    $('#<%=txtSMSSubject.ClientID %>').removeAttr("disabled");
                    $('#<%=txtSMSContent.ClientID %>').removeAttr("disabled");
                    $('#<%=divSMSRecipientType.ClientID %>').removeAttr("disabled");
                } else {
                    $('#<%=txtSMSSubject.ClientID %>').attr("disabled", "disabled");
                    $('#<%=txtSMSContent.ClientID %>').attr("disabled", "disabled");
                    $('#<%=divSMSRecipientType.ClientID %>').attr("disabled", "disabled");
                }
            });

           

        });

        var textSeparator = ";";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }

        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);

            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }

        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

    </script>
    <script>

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        //function mp_GetData(code) {

        //    $.ajax({
        //        type: "POST",
        //        url: "../Masters/NewNotificationTemplate.aspx/VerifyCode",
        //        contentType: "application/json; charset=utf-8",
        //        data: JSON.stringify({
        //            code: code

        //}),
        //dataType: "json",
        //error: function (jqXHR, sStatus, sErrorThrown) {
        //    sweetAlert('data: ' + sErrorThrown);
        //},
        //success: function (data) {
        //    var obj = JSON.parse(JSON.stringify(data));
        //    if (obj.d == "New Value") {

        //                $("#<=err.ClientID %>").text("");
        //            }
        //            else {
        //                $("#<=err.ClientID %>").text("Type Already Exists");
        //                $("#<=cmbTemplateType.ClientID %>").focus();
        //            }
        //        }
        //    });
        //}

        function myFunction() {
            var type = $("#<%=cmbTemplateType.ClientID %>").val();
            var whatsappflag = $("[id*=chkWhatsApp]").is(":checked");
            var emailflag = $("[id*=chkEmail]").is(":checked");
            var smsflag = $("[id*=chkSMS]").is(":checked");

            if (type == "" || type == null || type == 0 || type == "Please Select") {
                $("#<%=err.ClientID %>").text("Type is Mandatory");
                return false;
            }

           // if (whatsappflag == false && emailflag == false && smsflag == false) {
             //   $("#<%=err.ClientID %>").text("Notification is Mandatory");
             //   return false;
        //    }

            if (whatsappflag == true) {

            }

            if (emailflag == true) {

            }

            if (smsflag == true) {

            }
        }

    </script>
    <style>
        .tags {
            list-style: none;
            margin: 0;
            overflow: hidden;
            padding: 0;
        }

            .tags li {
                float: left;
            }

        .tag {
            background: #eee;
            border-radius: 3px 0 0 3px;
            color: #999;
            display: inline-block;
            height: 26px;
            line-height: 26px;
            padding: 0 20px 0 23px;
            position: relative;
            margin: 0 10px 10px 0;
            text-decoration: none;
            -webkit-transition: color 0.2s;
        }

            .tag::before {
                background: #fff;
                border-radius: 10px;
                box-shadow: inset 0 1px rgba(0, 0, 0, 0.25);
                content: '';
                height: 6px;
                left: 10px;
                position: absolute;
                width: 6px;
                top: 10px;
            }

            .tag::after {
                background: #fff;
                border-bottom: 13px solid transparent;
                border-left: 10px solid #eee;
                border-top: 13px solid transparent;
                content: '';
                position: absolute;
                right: 0;
                top: 0;
            }

            .tag:hover {
                background-color: #18306C;
                color: white;
            }

                .tag:hover::after {
                    border-left-color: #18306C;
                }
    </style>
    <div class="row" style="width: 100%">
        <div class="col-12">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white"><span id="spnNewNotificationTemplateHeader" runat="server">Notification Template</span></h4>
                </div>
                <div class="card-body" style="padding-top: 5px;">
                    <asp:Label ID="err" runat="server" CssClass="text-danger" Font-Size="Larger"></asp:Label>
                    <div class="row" style="margin-left: -10px !important;">
                        <div class="col-md-12">
                            <asp:Button ID="Savebtn" runat="server" Text="Save" CssClass="btn btn-outline-success btn-rounded" ToolTip="Save" Width="70px" BackColor="#1e7db6" BorderColor="#1e7db6"
                                TabIndex="13" Style="margin-right: 10px; float: right;" OnClientClick="return myFunction();" OnClick="btnSave_ServerClick" />

                            <button runat="server" id="btnback" class="mybtn btn btn-outline-success btn-rounded" title="Back" style="margin-right: 10px;" data-target="../Masters/NotificationTemplateList.aspx">
                                <i class="fa fa-angle-left"></i>&nbsp;<span id="spnNewNotificationTemplateBranchBack" runat="server">Back</span>
                            </button>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updatepnl" runat="server">
                        <ContentTemplate>
                    <div class="form-group row">
                        <div class="col-md-9">
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateName" runat="server">Name</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtTemplateName" Class="form-control" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateType" runat="server">Type</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-4">
                                   <%-- <asp:UpdatePanel ID="updatepnl" runat="server">--%>
                                       <%-- <ContentTemplate>--%>
                                            <asp:DropDownList ID="cmbTemplateType" runat="server" TabIndex="2" Class="select2 form-control" AutoPostBack="true" AutoCompleteType="Disabled" Width="100%" OnSelectedIndexChanged="cmbTemplateType_SelectedIndexChanged">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                       <%-- </ContentTemplate>--%>
                                    <%--</asp:UpdatePanel>--%>
                                </div>
                            </div>

                            <div id="divemailcategoryrecip" runat="server" class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateNotification" runat="server">Notification</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-check form-check-inline">
                                        <asp:CheckBox runat="server" ID="chkEmail" Text="Email" TabIndex="2" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-check form-check-inline">
                                        <asp:CheckBox runat="server" ID="chkSMS" Text="SMS" TabIndex="3" />
                                    </div>
                                </div>
                                <div class="col-md-1" >
                                    <div class="form-check form-check-inline" id="chkWhatsAppChkBX" runat="server">
                                        <asp:CheckBox runat="server" ID="chkWhatsApp" Text="Whatsapp" TabIndex="4" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateCategory" runat="server">Category</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-4">
                                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="chkCategory" Theme="Aqua" runat="server" AnimationType="None" Height="35px" Width="100%">
                                        <DropDownWindowStyle BackColor="#EDEDED" />
                                        <DropDownWindowTemplate>
                                            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" TextField="Name" ValueField="Value" SelectionMode="CheckColumn" Theme="MaterialCompact"
                                                runat="server" Height="200" EnableSelectAll="true" OnInit="ASPxComboBoxInstance_Init" OnDataBound="ASPxComboBoxInstance_DataBound">
                                                <FilteringSettings ShowSearchUI="true" />
                                                <Border BorderStyle="None" />
                                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                                            </dx:ASPxListBox>
                                            <%--<table style="width: 100%">
                                                <tr>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right" Theme="Aqua">
                                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>--%>
                                        </DropDownWindowTemplate>

                                        <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                                    </dx:ASPxDropDownEdit>
                                </div>
                            </div>

                            <%--Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration --%>
                            <div id="divEmailRecipientType" runat="server" class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnEmailRecipientType" runat="server">Email Recipients</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>

                                <div class="col-md-10">
                                    <asp:RadioButtonList ID="rdoEmailRecipientType" name="rdoEmailRecipientType" runat="server" ForeColor="#333300"
                                         RepeatDirection="Horizontal" Width="100%" TabIndex="1" >
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <%--Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration --%>
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnemailcc" runat="server">Email CC</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtemailcc" Class="form-control" runat="server" MaxLength="200" TabIndex="5"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateEmailSubject" runat="server">Email Subject</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtMailSubject" Class="form-control" runat="server" MaxLength="200" TabIndex="5"></asp:TextBox>
                                </div>
                            </div>
                             
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateEmailContent" runat="server">Email Content</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-10">
                                    <dx:ASPxHtmlEditor ID="txtEmailContent" runat="server" Border-BorderColor="#ced4da" Border-BorderStyle="Solid" Border-BorderWidth="1px" Width="100%" Height="250px" TabIndex="6"></dx:ASPxHtmlEditor>
                                </div>
                            </div>

                            <%--Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration --%>
                            <div id="divSMSRecipientType" runat="server" class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnSMSRecipientType" runat="server">SMS Recipients</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>

                                <div class="col-md-10">
                                    <asp:RadioButtonList ID="rdoSMSRecipientType" name="rdoSMSRecipientType" runat="server" ForeColor="#333300" RepeatDirection="Horizontal" Width="100%" TabIndex="1">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <%--Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration --%>

                            <div  id="divSMScontent" runat="server" class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateSMSSubject" runat="server">SMS Subject</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSMSSubject" Class="form-control" runat="server" MaxLength="100" TabIndex="7"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateSMSContent" runat="server">SMS Content</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSMSContent" Class="form-control" runat="server" MaxLength="200" TabIndex="8" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>

                            <%--Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration --%>
                            <div id="divWhatsappRecipientType" runat="server" class="form-group row">
                                <div class="col-md-2">
                                    <label><span id="spnWhatsappRecipientType" runat="server">Whatsapp Recipients</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>

                                <div class="col-md-10">
                                    <asp:RadioButtonList ID="rdoWhatsappRecipientType" name="rdoWhatsappRecipientType" runat="server" ForeColor="#333300" RepeatDirection="Horizontal" Width="100%" TabIndex="1">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <%--Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration --%>

                            <div class="form-group row" id="divWhatsappContent" runat="server">
                                <div class="col-md-2">
                                    <label><span id="spnNewNotificationTemplateWhatsappContent" runat="server">Whatsapp Content</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtWhatsappContent" Class="form-control" runat="server" MaxLength="1000" TextMode="MultiLine" Rows="3" TabIndex="9"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <label><span id="spnNewNotificationTemplateKeywords" runat="server">Keywords</span><span class="text-danger" style="font-size: larger;"></span></label>
                                </div>
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div runat="server" id="keywords"></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                   </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script>
        //var nouns = ['guitar'];
        //var verbs = ['play'];

        function allowDrop(ev) {
            ev.preventDefault();
        }

        function drag(ev) {
            ev.dataTransfer.setData("Text", ev.target.id);
        }

        //function drop(ev) {
        //    ev.preventDefault();
        //    var data = ev.dataTransfer.getData("Text");
        //    if (ev.target.id == 'verb' && verbs.indexOf(data) != -1) {
        //        ev.target.appendChild(document.getElementById(data));
        //    }
        //    else {
        //        alert(data + ' is not a ' + ev.target.id + '. Try again');
        //    }

        //}
    </script>
</asp:Content>
