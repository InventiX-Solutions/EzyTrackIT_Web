<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true"  EnableEventValidation="false"  EnableViewState="true" CodeBehind="StatusDashboard.aspx.cs" Inherits="TCC_CRM.Transactions.StatusDashboard" %>

<%@ Register Assembly="DevExpress.XtraCharts.v18.2.Web, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.PivotGrid.v18.2.Core, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.PivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Charts.v18.2.Core, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Charts" TagPrefix="dx" %>
<%--<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>--%>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register TagPrefix="cc1" Namespace="DevExpress.XtraCharts" Assembly="DevExpress.XtraCharts.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/countup.js@2.0.7/dist/countUp.umd.js"></script>

    <script>
        window.addEventListener('DOMContentLoaded', () => {
            document.querySelectorAll('.job-count').forEach(el => {
                const target = parseInt(el.getAttribute('data-count'));
                const counter = new countUp.CountUp(el.id, target);
                if (!counter.error) {
                    counter.start();
                } else {
                    console.error(counter.error);
                }
            });
        });
    </script>

    <style>

      /*  .hover-shadow:hover {
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
    transform: translateY(-3px);*/
       .hover-shadow:hover {
        border-color: #31d2f2 !important;
        box-shadow: 0 4px 12px rgba(13, 202, 240, 0.3); /* light info shadow */
        transform: translateY(-3px);
        transition: 0.3s;
    
}
    </style>
<%--    <div class="container mt-4">
    <div class="row" runat="server">
        <asp:Repeater ID="rptStatusDashboard" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-3">
                    <div class="card border-info h-100">
                        <div class="card-body text-center">
                            <h6 class="card-title"><%# Eval("StatusName") %></h6>
                            <span class="h4 text-primary"><%# Eval("JobCount") %></span>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>--%>

<%--    <div class="container mt-4">
    <div class="row" runat="server">
        <asp:Repeater ID="rptStatusDashboard" runat="server">
            <ItemTemplate>
                <div class="col-md-2 col-sm-4 col-6 mb-3">
                    <div class="card border-info text-center h-100">
                        <div class="card-body p-2">
                            <div class="text-muted" style="font-size: 13px;">
                                <%# Eval("StatusName") %>
                            </div>
                            <div class="h4 text-primary fw-bold">
                                <%# Eval("JobCount") %>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>--%>

<%--    <asp:Repeater ID="rptStatusDashboard" runat="server">
    <ItemTemplate>
        <div class="col-md-2 col-sm-4 col-6 mb-3">
            <div class="card border-info text-center h-100">
                <div class="card-body p-2">
                    <!-- Make status name bold -->
                    <div class="fw-bold text-uppercase" style="font-size: 13px;">
                        <%# Eval("StatusName") %>
                    </div>

                    <!-- Center number with large bold style -->
                    <div class="h4 fw-bold text-primary mt-1">
                        <%# Eval("JobCount") %>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>--%>

<%--    <asp:Repeater ID="rptStatusDashboard" runat="server">
    <ItemTemplate>
        <div class="col-md-2 col-sm-4 col-6 mb-1">
            <div class="card border-info text-center" style="min-height: 80px;">
                <div class="card-body p-2" style="padding: 0.5rem;">
                    <div class="fw-bold text-uppercase" style="font-size: 12px;">
                        <%# Eval("StatusName") %>
                    </div>
                    <div class="fw-bold text-primary" style="font-size: 20px;">
                        <%# Eval("JobCount") %>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>--%>


    <%--<asp:Repeater ID="rptStatusDashboard" runat="server">
    <ItemTemplate>
        <div class="col-md-2 col-sm-4 col-6 mb-1">--%>
            <%--  <div class="card border-info text-center" style="min-height: 80px;">--%>
           <%-- <a href='<%# "TicketList.aspx?status=" + Eval("StatusName") %>' style="text-decoration: none;">--%>
            <%-- <a href='<%# "TicketList.aspx?statusid=" + Eval("StatusID") %>' style="text-decoration: none;">
                <div class="card border-info  text-center" style="min-height: 80px;">--%>
                   <%-- <div class="card-body p-2">
                        <div class="status-title"><%# Eval("StatusName") %></div>
                        <div class="status-count"><%# Eval("JobCount") %></div>
                    </div>--%>
                   <%-- <div class="card-body p-2" style="padding: 0.5rem;">
                    <div class="fw-bold text-uppercase" style="font-size: 12px;">
                        <%# Eval("StatusName") %>
                    </div>
                    <div class="fw-bold text-primary" style="font-size: 20px;">
                        <%# Eval("JobCount") %>
                    </div>
                </div>
                    
                </div>
            </a>
                  </div>
        </div>
    </ItemTemplate>
</asp:Repeater>--%>

    

  <%--  <asp:Repeater ID="rptStatusDashboard" runat="server">
    <ItemTemplate>
        <div class="col-md-2 col-sm-4 col-6 mb-1">

            <a href='<%# "TicketList.aspx?statusid=" + Eval("StatusID") %>' style="text-decoration: none;">
                <div class="card shadow-sm border-0 text-center hover-shadow" style="min-height: 100px; border-radius: 10px; transition: 0.3s;">
                    <div class="card-body p-2 d-flex flex-column justify-content-center align-items-center">
                        <!-- Status Icon or Circle -->
                        <div class="mb-1">
                            <i class="bi bi-layers-fill" style="font-size: 20px; color: #17a2b8;"></i>
                        </div>
                        <!-- Status Name -->
                        <div style="font-size: 11px; font-weight: bold; text-transform: uppercase; color: #333; margin-bottom: 4px;">
                            <%# Eval("StatusName") %>
                        </div>
                        <!-- Job Count -->
                        <div style="font-size: 18px; font-weight: bold; color: #0d6efd;">
                            <%# Eval("JobCount") %>
                        </div>
                    </div>
                </div>
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>--%>

  <%--  <asp:Repeater ID="rptStatusDashboard" runat="server">
    <ItemTemplate>
        <div class="col-md-2 col-sm-4 col-6 mb-1">
            <a href='<%# "TicketList.aspx?statusid=" + Eval("StatusID") %>' style="text-decoration: none;">
                <div class="card shadow-sm border border-info text-center hover-shadow" 
                     style="min-height: 100px; border-radius: 10px; transition: 0.3s;">
                    <div class="card-body p-2 d-flex flex-column justify-content-center align-items-center">
                        
                        <!-- Icon -->
                        <div class="mb-1">
                            <i class="bi bi-layers-fill" style="font-size: 20px; color: #17a2b8;"></i>
                        </div>

                        <!-- Status Name -->
                        <div style="font-size: 14px; font-weight: bold; text-transform: uppercase; color: #333; margin-bottom: 4px;">
                            <%# Eval("StatusName") %>
                        </div>

                        <!-- Animated Job Count -->
                        <div 
                            class="job-count" 
                            data-count='<%# Eval("JobCount") %>' 
                            id='count_<%# Eval("StatusID") %>' 
                            style="font-size: 18px; font-weight: bold; color: #0d6efd;">
                            0
                        </div>

                    </div>
                </div>
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>--%>
    <asp:Repeater ID="rptStatusDashboard" runat="server">
    <ItemTemplate>
        <div class="col-md-2 col-sm-4 col-6 mb-1">
            <%-- Check if StatusID is not null --%>
            <%# Eval("StatusID") != DBNull.Value ? 
                "<a href='TicketList.aspx?statusid=" + Eval("StatusID") + "' style=\"text-decoration: none;\">" : "<div>" %>

                <div class="card shadow-sm border border-info text-center hover-shadow" 
                     style="min-height: 100px; border-radius: 10px; transition: 0.3s;">
                    <div class="card-body p-2 d-flex flex-column justify-content-center align-items-center">
                        
                        <!-- Icon -->
                        <div class="mb-1">
                            <i class="bi bi-layers-fill" style="font-size: 20px; color: #17a2b8;"></i>
                        </div>

                        <!-- Status Name -->
                        <div style="font-size: 14px; font-weight: bold; text-transform: uppercase; color: #333; margin-bottom: 4px;">
                            <%# Eval("StatusName") %>
                        </div>

                        <!-- Animated Job Count -->
                        <div 
                            class="job-count" 
                            data-count='<%# Eval("JobCount") %>' 
                            id='count_<%# Eval("StatusID") %>' 
                            style="font-size: 18px; font-weight: bold; color: #0d6efd;">
                            0
                        </div>

                    </div>
                </div>

            <%# Eval("StatusID") != DBNull.Value ? "</a>" : "</div>" %>
        </div>
    </ItemTemplate>
</asp:Repeater>
   <%--   <script>
        
    </script>
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
        <asp:HiddenField ID="hdncusSelectedValue" runat="server" />
         <asp:HiddenField ID="hdnNOP" runat="server" />
        <asp:HiddenField ID="hdnengg" runat="server" />
        <asp:HiddenField ID="hdnjob" runat="server" />
        <div class="col-12" style="margin-top: -18px;">

            <div class="card">
                <div class="card-header">
                    <h7 class="m-b-0 text-white">Product Analysis </h7>
                </div>
                <div class="card-body" style="padding: 22px;">
             
                             <div class="container py-5">
  
  <div class="py-5">
    <div class="row">
      <div class="col-lg-12 mb-1">
      
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
                                            AutoResizeWithContainer="true" Width="80%" Height="45%" >
                                        </dx:ASPxDateEdit>
                                    </div>
                                    <div class="col-md-3">
                                            <label class="col-md-12 col-form-label">To Date</label>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Theme="Glass"
                                            AutoResizeWithContainer="true" Width="80%" Height="45%" >
                                        </dx:ASPxDateEdit>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Customer</label>
                                        <asp:DropDownList ID="cmbcustomer" runat="server" TabIndex="7" Class="select2 form-control">
                                        </asp:DropDownList>
                                    </div>
                             
                                    <div class="col-md-3">
                                        <label>Product<span class="text-danger" style="font-size: larger;"></span></label>
                                        <asp:DropDownList ID="cmbproduct" runat="server" TabIndex="7" Class="select2 form-control">
                                        </asp:DropDownList>
                                    </div>
                   
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
                                    
                                    <div class="col-md-1">
                                        <label></label>
                                       
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
                          
                               </div>
              </div>
            </div>
                </div>
            </div>
         </div>
            </div>     
               



                    <div class="form-group row p-t-20">
                        <div>
                             <div style="overflow-x:auto;width: 1172px;">
                            <dx:aspxpivotgrid id="ASPxPivotGrid2" clientidmode="AutoID" clientinstancename="pivot" runat="server" width="100%">

                                <clientsideevents endcallback="PivotOnEndCallback" cellclick="function(s, e) {
   var columnIndex = e.ColumnIndex;
 Func();
&nbsp;&nbsp;&nbsp;var&nbsp;rowIndex = e.RowIndex;
pivot.PerformCallback(columnIndex+'|'+rowIndex);
}" />
                            </dx:aspxpivotgrid>
</div>

                            <div>

                                <dx:webchartcontrol id="WebChartControl1" runat="server" crosshairenabled="True" tooltipenabled="True" datasourceid="ASPxPivotGrid2" height="240px" width="1150px" seriesdatamember="Series">
                                    <diagramserializable>
                                        <cc1:xydiagram>
                                            <axisx title-text="DCName" visibleinpanesserializable="-1">
                                            </axisx>

                                            <axisy title-text="Dispatched" visibleinpanesserializable="-1"></axisy>


                                            <axisy title-text="ProjectNum" visibleinpanesserializable="-1"></axisy>
                                        </cc1:xydiagram>
                                    </diagramserializable>

                                    <legend maxhorizontalpercentage="30" name="Default Legend"></legend>

                                    <seriestemplate seriesdatamember="Series" argumentdatamember="Arguments" valuedatamembersserializable="Values" argumentscaletype="Qualitative"></seriestemplate>
                                </dx:webchartcontrol>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>--%>
</asp:Content>
 
