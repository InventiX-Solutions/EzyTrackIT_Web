<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="ImportCustomers.aspx.cs" Inherits="TCC_CRM.Masters.ImportCustomers" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <div class="row" style="width: 100%">
        <div class="col-12">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Import Customers</h4>
                </div>
                <div class="card-body" style="padding: 22px;">
                    <label class="control-label" runat="server" id="lblerror"></label>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:FileUpload ID="FileUploader" runat="server" /><br />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <input type="button" class="btn btn-outline-success btn-rounded" value="Excel Upload &#10004;" onserverclick="btnExcelupload_ServerClick" id="btnExcelupload" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <input type="button" class="btn btn-outline-success btn-rounded" value="Import Data &#10004;" runat="server" id="btnimportdata" onserverclick="btnimportdata_ServerClick" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive m-t-1 p-t-10">
                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" Theme="DevEx" EnableRowsCache="False" KeyFieldName="TicketID" Font-Names="Poppins" Font-Size="13px" OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared">
                                <SettingsPager AlwaysShowPager="True" PageSize="10">
                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                </SettingsPager>

                                <Settings ShowGroupPanel="true" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem FieldName="SNO" SummaryType="Count" />
                                </GroupSummary>
                                <Settings ShowFooter="true" GridLines="Both" ShowFilterRow="true" />
                                <Styles>
                                    <AlternatingRow Enabled="true" />
                                </Styles>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="SNO" Caption="SNO" ReadOnly="True" VisibleIndex="1" CellStyle-HorizontalAlign="Left" Visible="false">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="CustomerCode" Caption="Customer Code" ReadOnly="True" VisibleIndex="2" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="CustomerName" Caption="Customer Name" ReadOnly="True" VisibleIndex="3" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="AddressLine1" Caption="Address Line1" ReadOnly="True" VisibleIndex="4" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="AddressLine2" Caption="Address Line2" ReadOnly="True" VisibleIndex="5" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="ContactPerson" Caption="Contact Person" ReadOnly="True" VisibleIndex="6" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="PhoneNo" Caption="Phone No" ReadOnly="True" VisibleIndex="7" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="CustomerCodeVerification" Caption="Customer Code Verification" ReadOnly="True" VisibleIndex="8" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="VerificationStatus" Caption="Verification Status" ReadOnly="True" VisibleIndex="9" CellStyle-HorizontalAlign="Justify">
                                    </dx:GridViewDataTextColumn>

                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
