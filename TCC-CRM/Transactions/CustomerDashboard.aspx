<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CustomerDashboard.aspx.cs" Inherits="TCC_CRM.Transactions.CustomerDashboard" %>

<%@ Register Assembly="DevExpress.XtraCharts.v18.2.Web, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.PivotGrid.v18.2.Core, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.PivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Charts.v18.2.Core, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Charts" TagPrefix="dx" %>
<%--<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>--%>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register TagPrefix="cc1" Namespace="DevExpress.XtraCharts" Assembly="DevExpress.XtraCharts.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
      <script>
      //    $(document).ready(function () {
         //     restoreDropdownSelections();
        //  });
        </script>
         <script>
             $(function () {
                 $(".select2").select2();
              //   restoreDropdownSelections();
              //   mp_GetData();
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
    <script>
     </script>
    <script>
        function myFunction() {
            debugger;

            var selectedValue = $("#<%=cmbcustomer.ClientID %>").val();
             $("#<%=hdncustomerSelectedValue.ClientID %>").val(selectedValue);
            
             var selectedproductValue = $("#<%=cmbproduct.ClientID %>").val();
             $("#<%=hdnproduct.ClientID %>").val(selectedproductValue);
            
             var selectedNOPValue = $("#<%=cmbNOP.ClientID %>").val();
             $("#<%=hdnNOP.ClientID %>").val(selectedNOPValue);
             var selectedenggValue = $("#<%=ddlengg.ClientID %>").val();
             $("#<%=hdnengg.ClientID %>").val(selectedenggValue);
             var selectedjobValue = $("#<%=cmbJoTys.ClientID %>").val();
             $("#<%=hdnjob.ClientID %>").val(selectedjobValue);


             return true; // Ensure the form submission continues


         }

    </script>
     
   <%-- <script>
        $(function () {
            $(".select2").select2();

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(".select2").select2();
                }
            });
        };
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
         .btn-primary
         {
         background-color: #0f52ba;
         }
          /*.btn-primary:hover
         {
         background-color: #0f52ba;
         }*/
.container {
    max-width: 1433px;
     width: 100%;
    padding-right: 15px;
    /* padding-left: 0px; */
    margin-right: auto;
    margin-left: auto;
}

body {
  min-height: 100vh;
  background-color: #fafafa;
}
    </style>
    <div class="container-fluid">
        <asp:HiddenField ID="hdnbrand" runat="server" />
        <asp:HiddenField ID="hdnmodel" runat="server" />
        <asp:HiddenField ID="hdnproduct" runat="server" />
        <asp:HiddenField ID="hdnbranch" runat="server" />
        <asp:HiddenField ID="hdncustomerSelectedValue" runat="server" />
        <asp:HiddenField ID="hdnNOP" runat="server" />
        <asp:HiddenField ID="hdnengg" runat="server" />
        <asp:HiddenField ID="hdnjob" runat="server" />
        <div class="col-12" style="margin-top: -18px;">

            <div class="card">
                <div class="card-header">
                    <h7 class="m-b-0 text-white">Customer Analysis</h7>
                </div>
                <div class="card-body" style="padding: 22px;">
                    <%-- <div class="form-group row p-t-20">--%>
              <%--      <div class="filter-panel">
                        <div class="toggle-button" onclick="togglePanel()">
                            <span class="filter-title">Filter</span>
                            <span class="arrow"></span>
                        </div>
                        <div class="panel-content">--%>
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
                                <div class="col-md-3">
                                      <label class="col-md-12 col-form-label">From Date</label>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Theme="Glass"
                                        AutoResizeWithContainer="true" Width="80%" Height="45%">
                                    </dx:ASPxDateEdit>
                                </div>
                                <div class="col-md-3">
                                      <label class="col-md-12 col-form-label">To Date</label>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Theme="Glass"
                                        AutoResizeWithContainer="true" Width="80%" Height="45%" >
                                    </dx:ASPxDateEdit>
                                </div>
                                <div class="col-md-3">
                                    <label>Customer<span class="text-danger" style="font-size: small;"></span></label>
                                    <asp:DropDownList ID="cmbcustomer" runat="server" TabIndex="7" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>
                              <%--  <div class="col-md-3" style="display:none">
                                    <label>Branch<span class="text-danger" style="font-size: larger;"></span></label>
                                    <asp:DropDownList ID="cmbbranch" runat="server" TabIndex="8" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>--%>
                                <div class="col-md-3">
                                    <label>Product<span class="text-danger" style="font-size: larger;"></span></label>
                                    <asp:DropDownList ID="cmbproduct" runat="server" TabIndex="7" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>
                                <%--<div class="col-md-3"style="display:none">
                                    <label>Brand<span class="text-danger" style="font-size: larger;"></span> </label>
                                    <asp:DropDownList ID="cmbbrand" runat="server" TabIndex="8" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3"style="display:none">
                                    <label>Model <span class="text-danger" style="font-size: larger;"></span></label>
                                    <asp:DropDownList ID="cmbmodel" runat="server" TabIndex="9" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>--%>
                                <div class="col-md-3">
                                    <label>Problem <span class="text-danger" style="font-size: larger;"></span></label>
                                    <asp:DropDownList ID="cmbNOP" runat="server" TabIndex="11" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Engineer<span class="text-danger" style="font-size: larger;"></span></label>
                                    <asp:DropDownList ID="ddlengg" runat="server" TabIndex="12" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>JobType</label>
                                    <asp:DropDownList ID="cmbJoTys" runat="server" TabIndex="11" Class="select2 form-control">
                                    </asp:DropDownList>
                                </div>

                               <%-- <div class="col-md-1">
                                    <label></label>
                                    <%-- <dx:ASPxButton ID="btn_go" runat="server" OnClick="btn_go_click" Theme="Glass" Text="GO"></dx:ASPxButton>--%>
                                    <%--<asp:Button ID="btngo" runat="server" Text="GO" CssClass="btn btn-success" ToolTip="GO"
                                        Width="70px" Height="30px" BackColor="#0f52ba" BorderColor="#0f52ba"
                                        TabIndex="29" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="btn_go_click" OnClientClick="return myFunction();" />
                                </div>
                                 <div class="col-md-1">
                                        <asp:Button ID="Btnclear" runat="server" Text="Reload" EnableTheming="True" CssClass="btn btn-outline-success btn-rounded"
                                            ToolTip="Reload" Style="color: white; width: 150px;" OnClick="Btnclear_Click"   />
                                    </div>--%>
                                    <div class="col-md-1">
                                        <label></label>
                                        <%--  <dx:ASPxButton ID="btn_go" runat="server" OnClick="btn_go_click" Theme="Glass" Text="GO"></dx:ASPxButton>--%>
                                        <asp:Button ID="btngo" runat="server" Text="GO" CssClass="btn btn-success" ToolTip="GO"
                                            Width="90px" Height="50px" BackColor="#0f52ba" BorderColor="#0f52ba"
                                            TabIndex="29" Style="float: right; position: relative; margin-top: 3px; margin-right: -8px;" OnClick="btn_go_click" OnClientClick="return myFunction();" />
                                    </div>
                                      <div class="col-md-1">
                                        <asp:Button ID="Btnclear" runat="server" Text="Reload" EnableTheming="True" CssClass="btn btn-success" width="90px" Height="50px" BackColor="#0f52ba" BorderColor="#0f52ba"
                                            ToolTip="Reload" Style="float: right; position: relative; margin-top: 27px; margin-right: -22px;"  OnClick="Btnclear_Click"   />
                                    </div>
                            </div>
                                         </div>
                      <%--  </div>
                    </div>--%>
  </div>
                    </div>
              </div>
                    </div>
          </div>
                    </div>
                                                  </div>
                 
                    <%--</div>--%>
                </div>



                <div class="form-group row p-t-20">
                    <div style="margin-left: 20px;">
                      <%--  <div style="overflow-x:auto;">--%>
                        <div style="overflow-x:auto;width: 1172px;">
                        <dx:ASPxPivotGrid ID="ASPxPivotGrid2" ClientIDMode="AutoID" ClientInstanceName="pivot"  runat="server" Width="100%">

                            <ClientSideEvents EndCallback="PivotOnEndCallback" CellClick="function(s, e) {
   var columnIndex = e.ColumnIndex;
 Func();
&nbsp;&nbsp;&nbsp;var&nbsp;rowIndex = e.RowIndex;
pivot.PerformCallback(columnIndex+'|'+rowIndex);
}" />
                        </dx:ASPxPivotGrid>
</div>
                       <%-- </div>--%>
                        <div>

                            <dx:WebChartControl ID="WebChartControl1" runat="server" CrosshairEnabled="True" ToolTipEnabled="True" DataSourceID="ASPxPivotGrid2" Height="240px" Width="1150px" SeriesDataMember="Series">
                                <DiagramSerializable>
                                    <cc1:XYDiagram>
                                        <AxisX Title-Text="DCName" VisibleInPanesSerializable="-1">
                                        </AxisX>

                                        <AxisY Title-Text="Dispatched" VisibleInPanesSerializable="-1"></AxisY>


                                        <AxisY Title-Text="ProjectNum" VisibleInPanesSerializable="-1"></AxisY>
                                    </cc1:XYDiagram>
                                </DiagramSerializable>

                                <Legend MaxHorizontalPercentage="30" Name="Default Legend"></Legend>

                                <SeriesTemplate SeriesDataMember="Series" ArgumentDataMember="Arguments" ValueDataMembersSerializable="Values" ArgumentScaleType="Qualitative"></SeriesTemplate>
                            </dx:WebChartControl>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
