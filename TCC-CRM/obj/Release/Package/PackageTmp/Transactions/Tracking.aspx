<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Tracking.aspx.cs" Inherits="TCC_CRM.Transactions.Tracking" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">

        function OnMoreInfoPOClick(s, e) {

            window.location.href = "Tickets.aspx?TicketID=" + e + "&id=2";

        }
        function PrintTicket(s, e) {

            url = "TicketPrint.aspx?TicketID=" + e;
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
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

        .table td, .table th
        {
            padding-left: .75rem !important;
        }

        th, td
        {
            white-space: nowrap;
        }



        #headerdiv > *
        {
            text-decoration: none;
        }
    </style>
    <script>
        $(document).ready(function () {
            $(".select2").select2();

        });
    </script>

    <script>

        function onMyFrameLoad(ele, lat, long) {

            var params = "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=600,height=450,left=100,top=100";

            var url = "https://www.google.com/maps/embed/v1/place?key=AIzaSyB_KRurVRkcQ7TpyXf2-m3-4Oef6N-s2IY&q=" + lat + "," + long + "";
            var iframe = "<iframe width='600'  height='450' frameborder='0' style='border:0' src='" + url + "' allowfullscreen></iframe>'";
            var win = window.open(url, 'test', params);

        }
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
    </script>
    <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Tracking</h4>
                </div>

                <div class="card-body" style="margin-top: -18px;">

                    <div class="form-group row p-t-20">

                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server" Text="Employee"></asp:Label>

                            <dx:ASPxDropDownEdit ClientInstanceName="checkComboBoxProduct" ID="ASPxDropDownEdit_Product" Theme="Glass" Width="180px" Height="28px" runat="server" AnimationType="None">
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


                            <dx:ASPxDateEdit ID="ASPxDateEdit1" Font-Names="Poppins" Font-Size="13px" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>
                        </div>



                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
                            <dx:ASPxDateEdit ID="ASPxDateEdit2" Font-Names="Poppins" Font-Size="13px" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>


                        </div>
                        <div class="col-md-2" style="padding: 21px;">
                            <%--<dx:ASPxButton ID="btn_go" runat="server" OnClick="btn_go_click" CssClass="btn btn-success" Text="GO" Width="100px"></dx:ASPxButton>--%>
                            <asp:Button ID="btnSave" runat="server" Text="Track" CssClass="btn btn-success" ToolTip="Track" Width="70px" BackColor="#0f52ba" Height="30px" Style="padding: 2px;" BorderColor="#0f52ba" OnClick="btn_go_click" />
                        </div>
                    </div>
                    <div class="table-responsive m-t-1" style="margin-top: 15px">
                        <div id="headerdiv"></div>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" Theme="DevEx" EnableTheming="True" KeyFieldName="TicketID"
                            AutoGenerateColumns="false">
                            <SettingsPager AlwaysShowPager="True">
                            </SettingsPager>
                            <Settings ShowGroupPanel="true" />


                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="TicketID" SummaryType="Count" />
                            </GroupSummary>
                            <Settings ShowFooter="true" GridLines="Both" ShowFilterRow="true" />
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="id" VisibleIndex="1" Caption="TicketID" SortIndex="0" Width="100px" SortOrder="None" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="Intime_img" VisibleIndex="1" Caption="Intime_img" SortIndex="0" Width="100px" SortOrder="Ascending" Visible="false">
                                </dx:GridViewDataTextColumn>--%>


                                <%--   <dx:GridViewDataTextColumn FieldName="TicketNo" VisibleIndex="3" Caption="TicketNo" SortIndex="0" Width="100px" SortOrder="Ascending" >
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="TicketNo" VisibleIndex="1" Caption="Job No" CellStyle-HorizontalAlign="Center" Width="50px">
                                    <DataItemTemplate>
                                        <%--  <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />--%>
                                        <a href="javascript:void(0);" onclick="OnMoreInfoPOClick(this, '<%# DataBinder.Eval(Container.DataItem,"TicketID") %>')">
                                            <%# DataBinder.Eval(Container.DataItem,"TicketNo") %></a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="engineer_name" VisibleIndex="2" Caption="Engineer Name" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="customer_Name" VisibleIndex="3" Caption="Customer Name" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="customer_branch_Name" VisibleIndex="3" Caption="Branch Name" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataTextColumn FieldName="JobDate" VisibleIndex="4" Caption="JobDate" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="completionsDate" VisibleIndex="5" Caption="CompletionsDate" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Status" VisibleIndex="6" Caption="Status" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <%-- <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Image" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="Intime_img" OnInit="Image_Init" Width="200px"  Height="200px"></dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="TicketID" VisibleIndex="7" Caption="GeoLocation" Width="10px" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <a href="javascript:void(0);" onclick="onMyFrameLoad(this, '<%# Eval("Intime_lat") %>','<%# Eval("Intime_Long") %>')">
                                            <img src="../images/location.png" /></a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="8" Caption="Address" ReadOnly="True" Width="100px" CellStyle-HorizontalAlign="Center">
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <%--   <dx:GridViewDataTextColumn FieldName="Intime_lat" VisibleIndex="9" Caption="Latitude" SortIndex="0" Width="100px" Visible="true">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Intime_Long" VisibleIndex="10" Caption="Longitude" SortIndex="0" Width="100px" Visible="true">
                                </dx:GridViewDataTextColumn>--%>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- <script src="../assets/plugins/jquery/jquery.min.js"></script>
      <script>
          $(function () {
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
    </script>
      <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Staff Tracking</h4>
                </div>
                <div class="card-body" style="padding: 22px;">
                    <div class="row">
                         <div class="col-md-2">
                              <label class="col-md-12 col-form-label">Engineer </label>    
                             </div>
                       <div class="col-md-3">
                                             
                    
                            <dx:ASPxDropDownEdit ClientInstanceName="checkComboBoxProduct" ID="ASPxDropDownEdit_Product" Theme="Glass" Width="280px"  Height="28px" runat="server" AnimationType="None">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox Width="100%" ID="ListBoxLocation" ClientInstanceName="checkListBoxProduct"
                                       SelectionMode="Single" Theme="Glass"
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
                        <div class="=col-md-1" >
                                <asp:Button ID="btnSave" runat="server" Text="Track" CssClass="btn btn-success" ToolTip="Track" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba" OnClick="btn_go_click"
                             />
                        </div>
                        </div>
                    <br />
                    <div class="row">
                         <div class="col-md-6">
                   
                             <div id="Map" runat="server"></div>
                             </div>
                    </div>
                    </div>
                </div>
            </div>
          </div>--%>
</asp:Content>
