<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="TCC_CRM.Masters.Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
     <SCRIPT language=Javascript>
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }
   </SCRIPT>
  <script type="text/javascript">
      function BACK() {
          var url = "../Masters/CompanyList.aspx";
          window.location.href = url;
      }
           </script>
    <script>
        $(document).ready(function () {
            $('.dropify').dropify();

            // Translated
            $('.dropify-fr').dropify({
                messages: {
                    default: 'Glissez-déposez un fichier ici ou cliquez',
                    replace: 'Glissez-déposez un fichier ou cliquez pour remplacer',
                    remove: 'Supprimer',
                    error: 'Désolé, le fichier trop volumineux'
                }
            });

            // Used events
            var drEvent = $('#input-file-events').dropify();

            drEvent.on('dropify.beforeClear', function (event, element) {
                return confirm("Do you really want to delete \"" + element.file.name + "\" ?");
            });

            drEvent.on('dropify.afterClear', function (event, element) {
                alert('File deleted');
            });

            drEvent.on('dropify.errors', function (event, element) {
                console.log('Has Errors');
            });

            var drDestroy = $('#input-file-to-destroy').dropify();
            drDestroy = drDestroy.data('dropify')
            $('#toggleDropify').on('click', function (e) {
                e.preventDefault();
                if (drDestroy.isDropified()) {
                    drDestroy.destroy();
                } else {
                    drDestroy.init();
                }
            });
        });
    </script>
    <script language="Javascript">
        function isNumber(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
   <%-- <script type="text/javascript">
        function CheckEmailAddress(value) {
            
            var regex = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            if (value.match(regex)) {
                alert('match');
            }
        }
       </script>--%>
     <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">
        <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white">Edit Company</h4>

        </div>
        <div class="card-body">
            <div class="form-body">

                <div class="form-actions">
                    <asp:Label ID="err" runat="server"></asp:Label>
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba" Visible="false"
                        TabIndex="18" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="btnback_Click"  formnovalidate="formnovalidate"/>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="17" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnSaveClick" />
                </div>
                   <div class="container-fluid">
                          <div class="form-group row p-t-10">

                    <asp:Label ID="lblcompanyid" runat="server" Style="visibility: hidden" Text=""></asp:Label>

                    

                        <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Company Code<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtccode" runat="server" TabIndex="1" MaxLength="15" CssClass="form-control" placeholder="Enter Code" required="required"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Company Name<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtcname" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" placeholder="Enter Name" required="required">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-12">
                            <label class="col-md-6 col-form-label">Address Line 1<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtadd1" runat="server" TabIndex="3" MaxLength="500" CssClass="form-control" placeholder="Enter Address Line 1" required="required">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-12">
                            <label class="col-md-6 col-form-label">Address Line 2<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtadd2" runat="server" TabIndex="4" MaxLength="500" CssClass="form-control" placeholder="Enter Address Line 2" required="required">
                            </asp:TextBox>
                        </div>



                        <div class="col-md-3">
                            <label class="col-md-4 col-form-label">City<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtcity" runat="server" TabIndex="5" MaxLength="50" CssClass="form-control" placeholder="Enter City" required="required"></asp:TextBox>
                        </div>

                        <div class="col-md-3">
                            <label class="col-md-4 col-form-label">State<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtstate" runat="server" TabIndex="6" MaxLength="50" CssClass="form-control" placeholder="Enter State" required="required">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-4 col-form-label">Country<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtcountry" runat="server" TabIndex="7" MaxLength="50" CssClass="form-control" placeholder="Enter Country" required="required">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-8 col-form-label">Postal Code<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpostalcode" runat="server" TabIndex="8" MaxLength="6" CssClass="form-control" placeholder="Enter Postal Code" required="required" onkeypress="return isNumberKey(event)">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Phone No<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtphoneno" runat="server" TabIndex="9" MaxLength="15" CssClass="form-control" placeholder="Enter Phone No" required="required" onkeypress="return isNumberKey(event)">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Fax No<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtfaxno" runat="server" TabIndex="10" MaxLength="15" CssClass="form-control" placeholder="Enter Fax No" required="required">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-4 col-form-label">Website<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtwebsite" runat="server" TabIndex="11" MaxLength="50" CssClass="form-control" placeholder="Enter Website" required="required">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Email ID<span class="text-danger" style="font-size: larger;"></span></label>
                            <span id="check"></span>
                            <asp:TextBox ID="txtmailid" runat="server" TabIndex="12" MaxLength="50" CssClass="form-control" placeholder="Enter Email ID" required="required" onkeydown="CheckEmailAddress(this.value);">
                            </asp:TextBox>
                        </div>
                               <div class="col-md-3">
                            <label class="col-md-6 col-form-label" title="Business Registration Number">BRN<span class="text-danger" style="font-size: larger; " ></span></label>
                            <asp:TextBox ID="txttino" runat="server" TabIndex="13" MaxLength="25" CssClass="form-control" placeholder="Enter Business Registration NO" required="required">
                            </asp:TextBox>
                        </div>
                     <div class="col-md-3">
                            <label class="col-md-6 col-form-label" title="Greenwich Mean Time Zone">GMT<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtGMT" runat="server" TabIndex="14" MaxLength="15" CssClass="form-control" onkeypress="return isNumber(event,this);" placeholder="Enter GMT" required="required">
                            </asp:TextBox>
                        </div>
                       
                     <%--   <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Pan No<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtpanno" runat="server" TabIndex="14" MaxLength="15" CssClass="form-control" placeholder="Enter Pan No" required="required">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-6 col-form-label">Service Tax<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtservicetax" runat="server" TabIndex="15" MaxLength="4" CssClass="form-control" placeholder="Enter Service Tax" required="required">
                            </asp:TextBox>
                        </div>--%>
                        <div class="col-md-12">
                            <label class="col-md-4 col-form-label">Remarks<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtremarks" runat="server" TabIndex="16" MaxLength="1000" CssClass="form-control" placeholder="Enter Remarks" TextMode="MultiLine" Rows="4" required="required">
                            </asp:TextBox>
                        </div>
                              <br />
                              <div class="col-md-6">
                                    <label class="col-md-6 col-form-label">Company Logo1(Navbar)</label>
                                            <asp:Label ID="lbllogo" runat="server" Visible="false" TabIndex="7"></asp:Label>
                                            <%--  <input type="file" id="UserPerson"  class="dropify" runat="server" />--%>
                                            <asp:FileUpload ID="FileUpload1" runat="server" type="file" class="dropify"  />
                              </div>
                                 <div class="col-md-6">
                                    <label class="col-md-6 col-form-label">Company Logo2(Print)</label>
                                            <asp:Label ID="lbllogo2" runat="server" Visible="false" TabIndex="7"></asp:Label>
                                            <%--  <input type="file" id="UserPerson"  class="dropify" runat="server" />--%>
                                            <asp:FileUpload ID="FileUpload2" runat="server" type="file" class="dropify"  />
                              </div>
                        
                    </div>
                       <asp:Label ID="lblval" runat="server" Text="Customer Code" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
         </div>
      <script language="JavaScript">
          var ev = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/; var x = document.getElementById("check");
          function CheckEmailAddress(email)
          { if (!ev.test(email)) { x.innerHTML = "Invalid Email..!!"; x.style.color = "red" } else { x.innerHTML = "&#10004;"; x.style.color = "green" } } </script>
    <asp:HiddenField ID="hdnCompanyID" runat="server" />
</asp:Content>
