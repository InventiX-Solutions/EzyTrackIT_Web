<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="CustomerBranch.aspx.cs" Inherits="TCC_CRM.Masters.CustomerBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
     <script type="text/javascript">
         function BACK()
         {
             var url = "../Masters/CustomerList.aspx";
           
             window.location.href = url;
             
         }
           </script>
    <SCRIPT language=Javascript>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
   </SCRIPT>
    <div class="col-12">
        <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white"><asp:Label ID="lblHeader" Text="New Customer" runat="server"></asp:Label></h4>

        </div>
        <div class="card-body">
            <div class="form-body">

                <div class="form-actions">
                    <asp:Label ID="err" runat="server"></asp:Label>
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="29" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnBackClick" ValidateRequestMode="Disabled" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="28" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnSaveClick" />
                </div>
                <div class="container-fluid">

                    <asp:Label ID="lblcustomerid" runat="server" Style="visibility: hidden" Text=""></asp:Label>

                    <div class="form-group row p-t-20">

                        <div class="col-md-4">
                            <label class="col-md-6 col-form-label">Branch Code<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtccode" runat="server" TabIndex="1" MaxLength="50" CssClass="form-control" placeholder="Enter Code" required="required"></asp:TextBox>
                        </div>

                         <div class="col-md-4">
                            <label class="col-md-6 col-form-label">Branch Name<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtcname" runat="server" TabIndex="2" MaxLength="150" CssClass="form-control" placeholder="Enter Name" required="required">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-4">
                            <label class="col-md-6 col-form-label">Customer Name<span class="text-danger" style="font-size: larger;"></span></label>
                              <asp:DropDownList ID="ddlCustomer" runat="server"  Class="select2 form-control" required="required"  >
                                    </asp:DropDownList>
                        </div>

                        <div class="col-md-12">
                            <label class="col-md-6 col-form-label">Address Line 1<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtadd1" runat="server" TabIndex="3" MaxLength="500" CssClass="form-control" placeholder="Enter Address Line 1" >
                            </asp:TextBox>
                        </div>

                        <div class="col-md-12">
                            <label class="col-md-6 col-form-label">Address Line 2<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtadd2" runat="server" TabIndex="4" MaxLength="500" CssClass="form-control" placeholder="Enter Address Line 2" >
                            </asp:TextBox>
                        </div>



                        
                         <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Service Location<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtslocation" runat="server" TabIndex="7" MaxLength="150" CssClass="form-control" placeholder="Enter Service Location"  >
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-12 col-form-label">Contact Person<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtcper" runat="server" TabIndex="5" MaxLength="150" CssClass="form-control" placeholder="Enter Contact Person" ></asp:TextBox>
                        </div>

                        <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Phone No<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtphonenno" runat="server" TabIndex="6" MaxLength="15" CssClass="form-control" placeholder="Enter Phone No"  onkeypress="return isNumberKey(event)">
                            </asp:TextBox>
                        </div>
                          <div class="col-md-12">
                            <label class="col-md-6 col-form-label">Remarks<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtremarks" runat="server" TabIndex="8" MaxLength="1000" CssClass="form-control" placeholder="Enter Remarks" TextMode="MultiLine" Rows="4" >
                            </asp:TextBox>
                        </div>
                        
                    </div>
                    <asp:Label ID="lblval" runat="server" Text="Customer Code" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
      <input type="text" id="hdnprd" name="hdnprd" runat="server" style="visibility:hidden" />
    <asp:HiddenField ID="hdnCustomerD" runat="server" />
</asp:Content>
