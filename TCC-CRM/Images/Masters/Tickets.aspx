<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Tickets.aspx.cs" Inherits="TCC_CRM.Masters.Tickets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
<%--    <script>
        $(function () { $("#txtdate").datepicker; });
</script>--%>
    <script type="text/javascript">
        function BACK() {
            var url = "../Masters/TicketList.aspx";
            window.location.href = url;
        }
           </script>
    <script language=Javascript>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
   </script>
    <div class="col-12">
        <div class="card-header">
            <h4 class="m-b-0 text-white">New Ticket</h4>

        </div>
        <div class="card-body">
            <div class="form-body">

                <div class="form-actions">
                    <asp:Label ID="err" runat="server"></asp:Label>
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px"
                        TabIndex="29" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnbtnBackClick" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px"
                        TabIndex="28" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnSaveClick" />
                </div>
                <div class="container-fluid">

                    <asp:Label ID="lblticketid" runat="server" Style="visibility: hidden" Text=""></asp:Label>
                     <div class="form-group row p-t-20">
                         <div class="col-md-3">
                            <label class="col-md-3 col-form-label">TicketNo<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txttno" runat="server" TabIndex="1" CssClass="form-control"  required="required">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-3 col-form-label"> Date<span class="text-danger" style="font-size: larger;"></span></label>
                          <%--  <asp:TextBox ID="txtdate" runat="server" TabIndex="2" MaxLength="30" CssClass="form-control" placeholder="select date" required="required"></asp:TextBox>
                       --%> 
                          <asp:TextBox runat="server" id="txtdate" ClientIDMode="Static"  TabIndex="2"  CssClass="form-control" />
                        </div>

                         <div class="col-md-6">
                        <label class="col-md-6 col-form-label">Customer </label>                            
                          <asp:DropDownList ID="cmbcustomer" runat="server" TabIndex="3" Class="form-control"  OnSelectedIndexChanged="cmbcustomerselectedvalue" AutoPostBack="true">
                                            </asp:DropDownList>                                       
                          </div>  
                   
                         <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Address Line1<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtaddrl1" runat="server" TabIndex="4" CssClass="form-control"  required="required"></asp:TextBox>
                        </div>
                         <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Address Line2<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtaddrl2" runat="server" TabIndex="5"  CssClass="form-control"  required="required"></asp:TextBox>
                        </div>
                               
                   
                        <div class="col-md-3">
                            <label class="col-md-3 col-form-label">MobileNo<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpno" runat="server" TabIndex="6" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                     <div class="col-md-3">
                        <label class="col-md-3 col-form-label">Product</label>                            
                               <asp:DropDownList ID="cmbproduct" runat="server" TabIndex="9" Class=" form-control" OnSelectedIndexChanged="cmbproductselectedvalue" AutoPostBack ="true">
                                            </asp:DropDownList>                                      
                            </div>
                        <div class="col-md-3">
                        <label class="col-md-3 col-form-label">Brand </label>                            
                          <asp:DropDownList ID="cmbbrand" runat="server" TabIndex="7" Class="form-control"  OnSelectedIndexChanged="cmbbrandselectedvalue" AutoPostBack="true">
                                            </asp:DropDownList>                                       
                          </div>                   
                         <div class="col-md-3">
                        <label class="col-md-3 col-form-label">Model</label>                            
                               <asp:DropDownList ID="cmbmodel" runat="server" TabIndex="8"  Class=" form-control" OnSelectedIndexChanged="cmbmodelselectedvalue" AutoPostBack ="true">
                                            </asp:DropDownList>                                      
                            </div>
                           
                         
                         <div class="col-md-3">
                            <label class="col-md-3 col-form-label">SerialNo<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpserialno" runat="server" TabIndex="10" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                            <div class="col-md-3">
                        <label class="col-md-3 col-form-label">ServiceType</label>                            
                               <asp:DropDownList ID="cmbST" runat="server" TabIndex="11" Class=" form-control" AutoPostBack ="true">
                                            </asp:DropDownList>                                      
                            </div>
                           <div class="col-md-6">
                        <label class="col-md-6 col-form-label">Nature of Problem</label>                            
                               <asp:DropDownList ID="cmbNOP" runat="server" TabIndex="12" Class=" form-control" AutoPostBack ="true">
                                            </asp:DropDownList>                                      
                            </div>
                          <div class="form-group row p-t-20">
                     
                        <label class="col-md-4 col-form-label">&nbsp;&nbsp;&nbsp;Status</label>  
                               <div class="col-md-3">                          
                               <asp:DropDownList ID="cmbstatus" runat="server" TabIndex="13" Class=" form-control" AutoPostBack ="true" Enabled="false">
                                            </asp:DropDownList>                                      
                            </div>
                              </div>
                           <%--<div class="col-md-4">
                         <label class="col-md-4 col-form-label">Status</label>                            
                               <asp:DropDownList ID="cmbstatus" runat="server" TabIndex="13" Class=" form-control" AutoPostBack ="true">
                              </asp:DropDownList>   
                        </div>--%>
                     </div>
                    <asp:Label ID="lblval" runat="server" Text="ticketno" Visible="false"></asp:Label>
                   
                </div>
        </div>
         <asp:HiddenField ID="hdnTicketID" runat="server" />
        </div>
        </asp:Content>