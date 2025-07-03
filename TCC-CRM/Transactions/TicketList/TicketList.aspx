<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="TicketList.aspx.cs" Inherits="TCC_CRM.Transactions.TicketList" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>

    <script type="text/javascript">

        function OnMoreInfoPOClick(s, e) {

            window.location.href = "Tickets.aspx?TicketID=" + e+"&id=1";

        }
        function PrintTicket(s, e) {

            url = "TicketPrint.aspx?TicketID=" + e;
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
    <script type="text/javascript">
        function opennewpage() {
            window.location.href = "~/Transactions/Tickets.aspx?TicketID=0";
        }
        function myFunction() {
            var From =sdate.GetText();
            var To = tdate.GetText();
           
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

    <div class="row" style="width: 100%">
        <div class="col-12">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Job List</h4>
                </div>
                <div class="card-body" style="padding: 22px;">

                    <div class="row">
                        <div class="col-md-2">
                            <label for="lblorderfromdt" class="Themefontstyles">Status</label>
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" DataSourceID="XmlDataSource1"
                                ImageUrlField="ImageUrl" TextField="Text" ValueField="Name" ValueType="System.String"
                                ShowImageInEditBox="True" SelectedIndex="0" Height="25px" Theme="Glass" Width="100%">
                                <ItemImage Height="12px" Width="12px" />
                            </dx:ASPxComboBox>
                            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Transactions/Status.xml"
                                XPath="//Status" />
                        </div>
                        <div class="col-md-2">
                            <label for="lblorderfromdt" class="Themefontstyles">From Date</label>
                            <dx:ASPxDateEdit ID="orderfromdt" runat="server"  Theme="Glass" ClientInstanceName="sdate" DateRangeSettings-CalendarColumnCount="8"
                                DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy" Font-Names="Poppins" Font-Size="13px">
                            </dx:ASPxDateEdit>
                        </div>

                        <div class="col-md-2">
                            <label for="lblordertodt" class="Themefontstyles">To Date</label>
                            <dx:ASPxDateEdit ID="ordertodt" Font-Names="Poppins" Font-Size="13px" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>
                        </div>

                        <div class="col-md-2">
                            <br />
                            <asp:Button ID="btngo" runat="server" Text="Apply" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                ToolTip="Go" Style="color: white; width: 150px;" OnClick="btn_go_click" OnClientClick="return myFunction();" ></asp:Button>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <asp:Button ID="btnnew" runat="server" Text="Add New" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                ToolTip="Add New" Style="color: white; width: 150px;" OnClick="btn_new_click"></asp:Button>
                        </div>
                        <div class="col-md-1.5">
                            <br />
                            <asp:Button ID="excelxport" runat="server" Text="Download Excel" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                ToolTip="Add New" Style="color: white; width: 150px;" OnClick="excelexport_Click" OnClientClick="return myFunction();" />
                            <%-- <asp:ImageButton ID="excelexport" runat="server" ImageUrl="../images/ExportExcel.png" OnClick="excelexport_Click" />--%>
                        </div>
                    </div>

                    <div class="table-responsive m-t-1 p-t-10">
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
                                <dx:GridViewDataTextColumn FieldName="TicketID" VisibleIndex="0" Caption="Print" Width="10px" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>

                                        <a href="javascript:void(0);" title="Print Ticket" onclick="PrintTicket(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>')">
                                            <img src="../images/printicon.png" /></a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TicketNo" VisibleIndex="1" Caption="Job No" CellStyle-HorizontalAlign="Center" Width="50px" SortOrder="Descending">
                                    <DataItemTemplate>
                                        <%--  <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />--%>
                                        <a href="javascript:void(0);" onclick="OnMoreInfoPOClick(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>')">
                                            <%# DataBinder.Eval(Container.DataItem,"TicketNo") %></a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="TicketNo" Caption="TicketNo" ReadOnly="True" Width="50px" VisibleIndex="1"  CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>--%>

                                <dx:GridViewDataDateColumn FieldName="Date" Caption="Job Date" ReadOnly="True" Width="50px" VisibleIndex="2" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>

                                <dx:GridViewDataDateColumn FieldName="ReportDt" Caption="Reported Date" ReadOnly="True" Width="50px" VisibleIndex="3" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss" EditFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="customer_name" Caption="Customer" ReadOnly="True" Width="50px" VisibleIndex="3" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="product_name" Caption="Product" ReadOnly="True" Width="50px" VisibleIndex="4" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="brand_name" Caption="Brand" ReadOnly="True" Width="50px" VisibleIndex="5" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="model_name" Caption="Model" ReadOnly="True" Width="50px" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="engineer_code" Caption="Engineer" ReadOnly="True" Width="50px" VisibleIndex="7" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="CurrentStatus" Caption="CurrentStatus" ReadOnly="True" Width="50px" VisibleIndex="8" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="tothrs" Caption="Hours Spent" ReadOnly="True" Width="50px" VisibleIndex="9" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Invoice" Caption="Invoice" ReadOnly="True" Width="50px" VisibleIndex="10" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="invoice_amt" Caption="InvoiceAmt" ReadOnly="True" Width="50px" VisibleIndex="11" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PartNo" Caption="PartNo" ReadOnly="True" Width="50px" VisibleIndex="13" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SerialNumber" Caption="SerialNumber" ReadOnly="True" Width="50px" VisibleIndex="12" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Receipt_amt" Caption="ReceiptAmt" ReadOnly="True" Width="50px" VisibleIndex="13" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="service_type_name" Caption="Service Type" ReadOnly="True" Width="50px" VisibleIndex="14" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="problem_name" Caption="Problem" ReadOnly="True" Width="50px" VisibleIndex="15" CellStyle-HorizontalAlign="Center" >
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
