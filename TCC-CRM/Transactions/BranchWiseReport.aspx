<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="BranchWiseReport.aspx.cs" Inherits="TCC_CRM.Transactions.BranchWiseReport" %>

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
            height:20px;
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
                    <h4 class="m-b-0 text-white">Branch Wise Time Analysis</h4>
                </div>

                <div class="card-body" style="margin-top: -18px;">
                    <div class="form-group row p-t-20">

                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="Customer"></asp:Label>
                            <asp:DropDownList ID="cmbcustomer" runat="server" TabIndex="7"  Class="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbcustomer_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label5" runat="server" Text="Branch"></asp:Label>
                            <asp:DropDownList ID="cmbbranch" runat="server" TabIndex="8" Class="select2 form-control" OnSelectedIndexChanged="cmbbranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                            <dx:ASPxDateEdit ID="ASPxDateEdit1" Font-Names="Poppins" Font-Size="15px" TabIndex="3" Height="40px" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
                            <dx:ASPxDateEdit ID="ASPxDateEdit2" Font-Names="Poppins" Font-Size="15px" TabIndex="4" Height="40px" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>
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
                                <dx:GridViewDataTextColumn FieldName="customer_branch_name" VisibleIndex="1" Caption="Branch" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="engineer_name" VisibleIndex="2" Caption="Employee Name" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TicketNo" VisibleIndex="3" Caption="Job No" SortIndex="0" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TicketDT" VisibleIndex="4" Caption="Job Date" SortIndex="0" Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>


                                 <dx:GridViewDataTextColumn FieldName="JobTypes" Caption="JobType" ReadOnly="True" Width="50px" VisibleIndex="5" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataTextColumn>

                                 <dx:GridViewDataTextColumn FieldName="remarks" Caption="Remarks" ReadOnly="True" Width="50px" VisibleIndex="6" CellStyle-HorizontalAlign="Left">
                                </dx:GridViewDataTextColumn>


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
