<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="TCC_CRM.Transactions.Attendance" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--   <script src="../assets/plugins/jquery/jquery.min.js"></script>
      <script>
          $(function () {
              $(".select2").select2();

          });
</script>
      <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">

            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Staff Attendance</h4>
                </div>
                <div class="card-body" style="padding: 22px;">
                     <div class="col-md-12">
                   <label class="col-md-1 col-form-label">Staff </label>
                         <asp:DropDownList ID="cmbengineer" runat="server" Width="200" TabIndex="3" Class="select2 form-control"  >
                              <asp:ListItem Text="Please Select" Value="Engineer1"></asp:ListItem>
                              <asp:ListItem Text="John" Value="Engineer1"></asp:ListItem>
                               <asp:ListItem Text="Adam" Value="Engineer2"></asp:ListItem>
                               <asp:ListItem Text="Gilchrist" Value="Engineer3"></asp:ListItem>
                               <asp:ListItem Text="Rahul" Value="Engineer4"></asp:ListItem>
                               <asp:ListItem Text="Henry" Value="Engineer5"></asp:ListItem>
                                            </asp:DropDownList>
                     <label  class="col-md-1 col-form-label">Date </label> 
                         <asp:TextBox ID="txttno" runat="server" TabIndex="1" CssClass="form-control" Width="200" Text="20-09-2019" required="required" ReadOnly="true">
                                                </asp:TextBox>
                          <asp:Button ID="btnSave" runat="server" Text="GO" CssClass="btn btn-success" ToolTip="GO" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                             />
                      </div>
                          
                         </div>                       
                          
                            
                         
                           
                               
                       
                     <%--<div class="row">
                       <div class="col-md-6">
                      <label class="col-md-6 col-form-label">Staff </label>                            
                          <asp:DropDownList ID="cmbengineer" runat="server" TabIndex="3" Class="select2 form-control"  >
                              <asp:ListItem Text="John" Value="Engineer1"></asp:ListItem>
                               <asp:ListItem Text="Adam" Value="Engineer2"></asp:ListItem>
                               <asp:ListItem Text="Gilchrist" Value="Engineer3"></asp:ListItem>
                               <asp:ListItem Text="Rahul" Value="Engineer4"></asp:ListItem>
                               <asp:ListItem Text="Henry" Value="Engineer5"></asp:ListItem>
                                            </asp:DropDownList>    
                           </div>
                        </div>--%>
    <%-- <br />
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table nowrap display" width="100%">
                                <thead>
                                    <tr><td>#</td><td>Staff</td><td>Date</td><td>Check-In Time</td><td>Check-Out Time</td><td>Location</td></td><td>Selfie</td></tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td><td>John</td><td>20-09-2019</td><td>20-09-2019 08:50</td><td>20-09-2019 17:50</td><td>Mount Road</td><td><img src="../images/user1.png" width="30px" height="30px"/></td>
                                    </tr>
                                     <tr>
                                        <td>2</td><td>Adam</td><td>20-09-2019</td><td>20-09-2019 08:50</td><td>20-09-2019 17:50</td><td>Ranganathan street</td><td><img src="../images/user2.png" width="30px" height="30px"/></td>
                                    </tr>
                                     <tr>
                                        <td>3</td><td>Gilchrist</td><td>20-09-2019</td><td>20-09-2019 08:50</td><td>20-09-2019 17:50</td><td>Guindy Railway station</td><td><img src="../images/user3.png" width="30px" height="30px"/></td>
                                    </tr>
                                     <tr>
                                        <td>4</td><td>Rahul</td><td>20-09-2019</td><td>20-09-2019 08:50</td><td>20-09-2019 17:50</td><td>Central Metro</td><td><img src="../images/user4.png" width="30px" height="30px"/></td>
                                    </tr>
                                     <tr>
                                        <td>5</td><td>Henry</td><td>20-09-2019</td><td>20-09-2019 08:50</td><td>20-09-2019 17:50</td><td>Pheonix Shopping Mall</td><td><img src="../images/user5.png" width="30px" height="30px"/></td>
                                    </tr>
                                </tbody>
                            </table>

                            </div>
                        </div>
                    </div>
                
                </div>
            </div>
          </div>--%>


    <script src="../assets/plugins/jquery/jquery.min.js"></script>

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
                    <h4 class="m-b-0 text-white">Attendance</h4>
                </div>

                <div class="card-body" style="margin-top: -18px;">

                    <%--                       <div class="form-group row p-t-20">
                        
                           <div class="col-md-3">
                               <asp:Label ID="Label3" runat="server" Text="Employee"></asp:Label>
                              
                                       <dx:ASPxDropDownEdit ClientInstanceName="checkComboBoxProduct" ID="ASPxDropDownEdit_Product" Theme="Glass" Width="280px"  Height="28px" runat="server" AnimationType="None">
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
                             <div class= "col-md-2" style="padding: 20px;"> 
  
                                   <asp:Button ID="btnSave" runat="server" Text="Track" CssClass="btn btn-success" ToolTip="GO" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba" OnClick="btn_go_click" />
                                 </div>
                           </div>--%>
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
                        <div class="col-md-2" style="padding: 21px;">
                            <asp:Button ID="excelxport" runat="server" Text="Download Excel" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                ToolTip="Download Excel" width="150px" Height="30px" BackColor="#0f52ba" Style="padding: 2px;" OnClick="excelexport_Click" OnClientClick="return myFunction();" />
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
                                <dx:GridViewDataTextColumn FieldName="RowId" VisibleIndex="1" Caption="RowId" SortIndex="0" Width="100px" SortOrder="Descending" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Intime_img" VisibleIndex="1" Caption="Intime_img" SortIndex="0" Width="100px"  Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Intime_Lat" VisibleIndex="1" Caption="Latitude" SortIndex="0" Width="100px"  Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Intime_Long" VisibleIndex="1" Caption="Longitude" SortIndex="0" Width="100px"  Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="engineer_code" VisibleIndex="2" Caption="EmployeeCode" SortIndex="0" Width="100px" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="engineer_name" VisibleIndex="3" Caption="EmployeeName" SortIndex="0" Width="100px" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SkillName" VisibleIndex="4" Caption="SkillName" SortIndex="0" Width="100px" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PunchDate" VisibleIndex="5" Caption="PunchDate" SortIndex="0" Width="100px" SortOrder="Descending">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EMPStatus" VisibleIndex="6" Caption="Status" SortIndex="0" Width="100px" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TimeSpent" VisibleIndex="7" Caption="TimeSpent" SortIndex="0" Width="100px" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Intime" VisibleIndex="8" Caption="PunchInTime" SortIndex="0" Width="100px" SortOrder="Descending">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="INLocation" VisibleIndex="9" Caption="INLocation" SortIndex="0" Width="100px" >
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn VisibleIndex="10" Caption="Image" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="IN_Img" OnInit="Image_Init" Width="200px" Height="200px"></dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="outtime" VisibleIndex="11" Caption="PunchOutTime" SortIndex="0" Width="100px" SortOrder="Descending">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="OutLocation" VisibleIndex="12" Caption="OutLocation" SortIndex="0" Width="100px" >
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn VisibleIndex="13" Caption="Image" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="OUT_Img" OnInit="Image_Outit" Width="200px" Height="200px"></dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
