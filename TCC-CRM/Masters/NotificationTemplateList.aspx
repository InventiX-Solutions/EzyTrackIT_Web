<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="NotificationTemplateList.aspx.cs" Inherits="TCC_CRM.Masters.NotificationTemplateList" ValidateRequest="false" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>

    <script type="text/javascript">
        $(function () {
            debugger;


            var idval = getParameterByName('Saved');
            if (idval > 0) {
                // sweetAlert("Saved Successfully.!");
                sweetAlert($('input[id$=hdnSaveSuccessfully]').val());
            }
            $('.mybtn').on('click', function (event) {
                event.preventDefault();
                var url = $(this).data('target');

                location.replace(url);
            });

            $('.mybtn2').on('click', function (event) {
                event.preventDefault();
                var url = $(this).data('target');

                location.replace(url);
            });
        });
    </script>
    <script>
        function OnMoreInfoPOClick(s, e) {
            window.location.href = "NewNotificationTemplate.aspx?id=" + e;
        }

        function deleteRecord(s, e) {
            //if (confirm('Are you sure to delete this record?')) {
            //    deleterec(e);
            //}
            if (confirm($('input[id$=hdnAreyousuretodelete]').val())) {
                deleterec(e);
            }
        }

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        function deleterec(idval) {

            $.ajax({
                type: "POST",
                url: "../Masters/NotificationTemplateList.aspx/DeleteNotificationTemplate",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    idval: idval
                }),
                dataType: "json",
                error: function (jqXHR, sStatus, sErrorThrown) {
                    sweetAlert('data: ' + sErrorThrown);
                    //sweetAlert('Get Data Error: ' + sStatus);
                    //window.location.href = "../Login.aspx";
                },
                success: function (data) {
                    var obj = JSON.parse(JSON.stringify(data));
                    if (obj.d == "Success") {

                        // sweetAlert("Deleted Successfully");
                        sweetAlert($('input[id$=hdnDeleteSuccessfully]').val());
                        NotificationTemplateGrid.Refresh();
                    }
                    else {
                        // sweetAlert("Something went wrong..Please try again");
                        sweetAlert($('input[id$=hdnSomethingWentWrong]').val());
                    }
                    //var res="{"d":"success"}"
                }
            });
        }
    </script>
    <div class="row" style="width: 100%">
        <div class="col-12">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white"> <span id="spnNotificationTemplateHeader" runat="server">Notification Template</span></h4>
                </div>
                <div class="card-body" style="padding-top: 5px;">
                    <div class="row" style="margin-bottom: -5px !important; margin-left: -10px !important;">
                       
                        <div class="col-sm-12">
                            <button runat="server" id="btnExcel" class="btn btn-outline-success btn-rounded" title="Export To Excel" style="float: right" onserverclick="excelexport_Click">
                                <i class="fa fa-file-excel-o"></i>&nbsp;<span id="spnNotificationTemplateToExcel" runat="server">To Excel</span>
                            </button>
                            <button runat="server" id="btnaadnew" type="submit" class="mybtn btn btn-outline-success btn-rounded" title="Add New NotificationTemplate" style="margin-right: 10px; float: right;" data-target="../Masters/NewNotificationTemplate.aspx?id=0">
                                <i class="fa fa-plus"></i>&nbsp;<span id="spnNotificationTemplateNewNotificationTemplate" runat="server">New Template</span>
                            </button>
                        </div>

                    </div>
                    <div class="row" style="margin-top: -5px;">
                        <div class="table-responsive m-t-1 p-t-10">
                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="99%" Theme="DevEx" EnableRowsCache="False" KeyFieldName="Template_ID" Font-Names="Poppins" Font-Size="13px" ClientInstanceName="NotificationTemplateGrid">
                                <SettingsPager AlwaysShowPager="True" PageSize="10">
                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                </SettingsPager>
                                <Settings ShowGroupPanel="true" />
                                <SettingsCookies CookiesID="NotificationTemplategv" Enabled="true" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem FieldName="Template_ID" SummaryType="Count" />
                                </GroupSummary>
                                <Settings ShowFooter="true" GridLines="Both" ShowFilterRow="true" VerticalScrollBarMode="Auto" />

                                <Styles>
                                    <AlternatingRow Enabled="true" />
                                </Styles>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Template_ID" Caption="Action" ReadOnly="True" VisibleIndex="1" CellStyle-HorizontalAlign="Center" Width="50">
                                        <DataItemTemplate>
                                            <a href="javascript:void(0);" title="Delete this Record?" onclick="deleteRecord(this, '<%# DataBinder.Eval(Container.DataItem,"Template_ID") %>')">
                                                <img src="../images/action_delete.gif" /></a>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="TemplateName" Caption="Name" ReadOnly="True" VisibleIndex="2" CellStyle-HorizontalAlign="Justify">
                                        <DataItemTemplate>
                                            <a href="javascript:void(0);" title="Edit this Record?" onclick="OnMoreInfoPOClick(this, '<%# DataBinder.Eval(Container.DataItem,"Template_ID") %>')">
                                                <%# DataBinder.Eval(Container.DataItem,"TemplateName") %></a>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="TemplateType" Caption="Type" ReadOnly="True" VisibleIndex="3" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="EmailFlag" Caption="Email" ReadOnly="True" VisibleIndex="4" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SMSFlag" Caption="SMS" ReadOnly="True" VisibleIndex="5" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>
                                  <%--  <dx:GridViewDataTextColumn FieldName="WhatsappFlag" Caption="Whatsapp" ReadOnly="True" VisibleIndex="6" CellStyle-HorizontalAlign="Justify" >
                                    </dx:GridViewDataTextColumn>--%>

                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  
</asp:Content>
