<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="TCC_CRM.Masters.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
    <script type="text/javascript">
        function BACK() {
            var url = "../Masters/CompanyList.aspx";
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
        <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white">Product</h4>

        </div>
        <div class="card-body">
            <div class="form-body">

                <div class="form-actions">
                    <asp:Label ID="err" runat="server"></asp:Label>
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="29" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnbtnBackClick" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="28" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnSaveClick" />
                </div>
                <div class="container-fluid">

                    <asp:Label ID="lblproductid" runat="server" Style="visibility: hidden" Text=""></asp:Label>
                     <div class="form-group row p-t-20">
                        <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Product Code<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpcode" runat="server" TabIndex="1" MaxLength="50" CssClass="form-control" placeholder="Enter Code" required="required"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Product Name<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpname" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" placeholder="Enter Name" required="required">
                            </asp:TextBox>
                        </div>
                         
                    
                         <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Part No<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpno" runat="server" TabIndex="3" MaxLength="30" CssClass="form-control" placeholder="Enter Part number" required="required"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-5">
                            <label class="col-md-6 col-form-label">Product SerialNo<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpserialno" runat="server" TabIndex="3" MaxLength="30" CssClass="form-control" placeholder="Enter serial number" required="required"></asp:TextBox>
                        </div>--%>
                        
                        <div class="col-md-3">
                        <label class="col-md-3 col-form-label">Brand </label>                            
                          <asp:DropDownList ID="cmbbrandname" runat="server" TabIndex="5" Class="form-control"  OnSelectedIndexChanged="cmbbrandselectedvalue" AutoPostBack="true">
                                            </asp:DropDownList>                                       
                          </div>                   
                         <div class="col-md-3">
                        <label class="col-md-3 col-form-label">Model</label>                            
                               <asp:DropDownList ID="cmbmodelname" runat="server" TabIndex="4" Class=" form-control" AutoPostBack ="true">
                                            </asp:DropDownList>                                      
                            </div>
                        </div>
                    <asp:Label ID="lblval" runat="server" Text="Product Code" Visible="false"></asp:Label>
                   
                </div></div>
        </div>
         <asp:HiddenField ID="hdnProductID" runat="server" />
        </div>
        </asp:Content>