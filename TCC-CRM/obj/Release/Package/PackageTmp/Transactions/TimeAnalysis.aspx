<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="TimeAnalysis.aspx.cs" Inherits="TCC_CRM.Transactions.TimeAnalysis" %>

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

        .table td, .table th {
            padding-left: .75rem !important;
        }

        th, td {
            white-space: nowrap;
        }

        #headerdiv > * {
            text-decoration: none;
        }
    </style>
    <script>
        $(document).ready(function () {
            $(".select2").select2();

        });
    </script>

    <script type="text/javascript">
        function SelectedDropdown(selectedValues) {
            //  alert('hi');

            checkListBoxProduct.UnselectAll();
            var tex = "Dashboard ;Sales Order ;Settings"
            var texts = selectedValues.split(tex);
            //  var texts = "Dashboard;".split(textSeparator);
            var values = getValuesByTexts(texts);
            //   alert(tex);
            checkListBoxProduct.SelectValues(values);
            updateTextProduct(); // for remove non-existing texts
            // alert('hi');
        }
        var textSeparator = ";";
        function updateTextProduct() {
            var selectedItems = checkListBoxProduct.GetSelectedItems();
            checkComboBoxProduct.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBoxProduct.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            //  var texts = "Dashboard;".split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBoxProduct.SelectValues(values);
            updateTextProduct(); // for remove non-existing texts
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
                item = checkListBoxProduct.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

        function updateTextCustomer() {
            var selectedItems = checkListBoxCustomer.GetSelectedItems();
            checkComboBoxCustomer.SetText(getSelectedCustomerItemsText(selectedItems));
        }
        function synchronizeCustomerListBoxValues(dropDown, args) {
            checkListBoxCustomer.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            //  var texts = "Dashboard;".split(textSeparator);
            var values = getCustomerValuesByTexts(texts);
            checkListBoxCustomer.SelectValues(values);
            updateTextCustomer(); // for remove non-existing texts
        }

        function getSelectedCustomerItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getCustomerValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBoxCustomer.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

        function myFunction() {
            var From = sdate.GetText();
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
    </script>
    <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Time Analysis</h4>
                </div>

                <div class="card-body" style="margin-top: -18px;">
                    <div class="form-group row p-t-20">

                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="Customer"></asp:Label>

                            <dx:ASPxDropDownEdit ClientInstanceName="checkComboBoxCustomer" TabIndex="1" ID="ASPxDropDownEdit_Customer" Theme="Glass" Width="180px" Height="28px" runat="server" AnimationType="None">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox Width="100%" ID="ListBoxCustomer" ClientInstanceName="checkListBoxCustomer"
                                        SelectionMode="CheckColumn" Theme="Glass"
                                        runat="server" Height="200" EnableSelectAll="true">
                                        <FilteringSettings ShowSearchUI="true" />
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                        <Items>
                                        </Items>
                                        <ClientSideEvents SelectedIndexChanged="updateTextCustomer" Init="updateTextCustomer" />
                                    </dx:ASPxListBox>

                                </DropDownWindowTemplate>
                                <ClientSideEvents TextChanged="synchronizeCustomerListBoxValues" DropDown="synchronizeCustomerListBoxValues" />
                            </dx:ASPxDropDownEdit>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server" Text="Employee"></asp:Label>

                            <dx:ASPxDropDownEdit ClientInstanceName="checkComboBoxProduct" TabIndex="2" ID="ASPxDropDownEdit_Product" Theme="Glass" Width="180px" Height="28px" runat="server" AnimationType="None">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox Width="100%" ID="ListBoxLocation" ClientInstanceName="checkListBoxProduct"
                                        SelectionMode="CheckColumn" Theme="Glass"
                                        runat="server" Height="200" EnableSelectAll="true">
                                        <FilteringSettings ShowSearchUI="true" />
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                        <Items>
                                        </Items>
                                        <ClientSideEvents SelectedIndexChanged="updateTextProduct" Init="updateTextProduct" />
                                    </dx:ASPxListBox>

                                </DropDownWindowTemplate>
                                <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                            </dx:ASPxDropDownEdit>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                            <dx:ASPxDateEdit ID="ASPxDateEdit1" Font-Names="Poppins" Font-Size="13px" TabIndex="3" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
                            <dx:ASPxDateEdit ID="ASPxDateEdit2" Font-Names="Poppins" Font-Size="13px" TabIndex="4" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>
                        </div>
                        <div class="col-md-4" style="padding: 21px;">
                            <asp:Button ID="btnSave" runat="server" Text="Go" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded" TabIndex="5"
                                ToolTip="GO" Style="color: white; width: 70px;" OnClick="btn_go_click" />

                            <asp:Button ID="excelxport" runat="server" Text="Download Excel" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded" TabIndex="6"
                                ToolTip="Download Excel" Style="color: white; width: 150px;" OnClick="excelexport_Click" OnClientClick="return myFunction();" />
                        </div>
                    </div>
                    <div class="table-responsive m-t-1" style="margin-top: 15px">
                        <div id="headerdiv"></div>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" Theme="DevEx" EnableTheming="True" KeyFieldName="RowId"
                            AutoGenerateColumns="false">
                            <SettingsPager AlwaysShowPager="True">
                            </SettingsPager>
                            <Settings ShowGroupPanel="true" />
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="RowId" SummaryType="Count" />
                            </GroupSummary>
                            <Settings ShowFooter="true" GridLines="Both" ShowFilterRow="true" />
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="customer_name" VisibleIndex="1" Caption="Customer Name" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="engineer_name" VisibleIndex="2" Caption="Employee Name" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TicketNo" VisibleIndex="3" Caption="Job No" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TicketDT" VisibleIndex="4" Caption="Job Date" SortIndex="0" Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="starttime" VisibleIndex="5" Caption="Start Time" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="endtime" VisibleIndex="6" Caption="End Time" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="startdatetime" VisibleIndex="5" Caption="Start Time" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="enddatetime" VisibleIndex="6" Caption="End Time" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="TimeSpent" VisibleIndex="7" Caption="Total Time Spent (days.Hours:Minutes:Seconds)" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>

                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
