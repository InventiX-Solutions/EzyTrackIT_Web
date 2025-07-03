<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="TCC_CRM.Masters.User" %>
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
          var url = "../Masters/UserList.aspx";
          window.location.href = url;
      }
           </script>
   <script>
       $(document).ready(function () {
           $('#chkAdmin').click(function (e) {
               $('[name="chk[]"]').prop('checked', this.checked);
           });


           $('[name="chk[]"]').click(function (e) {
               if ($('[name="chk[]"]:checked').length == $('[name="chk[]"]').length || !this.checked)
                   $('#chkAdmin').prop('checked', this.checked);
           });


           $(function () {
               $("#txtUserName").focus();
           });
           // Basic
           function getParameterByName(name, url) {
               if (!url) url = window.location.href;
               name = name.replace(/[\[\]]/g, "\\$&");
               var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                   results = regex.exec(url);
               if (!results) return null;
               if (!results[2]) return '';
               return decodeURIComponent(results[2].replace(/\+/g, " "));
           }
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
           $(".toggle-password").click(function () {

               $(this).toggleClass("fa-eye fa-eye-slash");
               var input = $($(this).attr("toggle"));
               if (input.attr("type") == "password") {
                   input.attr("type", "text");
               } else {
                   input.attr("type", "password");
               }
           });
       });

    </script>
    <script type="text/javascript">
        function validate_pass() {
            //txtPassword is assumed to be the id of the password text field on your page
            var passField = document.getElementById('<%=txtPasswords.ClientID%>');
            if (passField.value.length < 8) {
                alert('Password must contain at least 8 characters');
                return false;
            }
            return true;
        }


        function validatepassword() {
            //TextBox left blank.
            debugger;

            if ($(this).val().length == 0) {
                $("#<%=lblvalidmsg.ClientID%>").Text = "Enter Password";
                return;
            }

            //Regular Expressions.
            var regex = new Array();
            regex.push("[A-Z]"); //Uppercase Alphabet.
            regex.push("[a-z]"); //Lowercase Alphabet.
            regex.push("[0-9]"); //Digit.
            regex.push("[$@$!%*#?&]"); //Special Character.

            var passed = 0;

            //Validate for each Regular Expression.
            for (var i = 0; i < regex.length; i++) {
                if (new RegExp(regex[i]).test($(this).val())) {
                    passed++;
                }
            }


            //Validate for length of Password.
            if (passed > 2 && $(this).val().length > 8) {
                passed++;
            }

            //Display status.
            var color = "";
            var strength = "";
            switch (passed) {
                case 0:
                case 1:
                    strength = "Weak";
                    color = "red";
                    break;
                case 2:
                    strength = "Good";
                    color = "darkorange";
                    break;
                case 3:
                case 4:
                    strength = "Strong";
                    color = "green";
                    break;
                case 5:
                    strength = "Very Strong";
                    color = "darkgreen";
                    break;
            }
            //$("#<%=lblvalidmsg.ClientID%>").Text = strength;
            $("#password_strength").css("color", color);
        }

    </script>
    <script>
        $(document).ready(function () {
            $(".select2").select2();
            $(".toggle-password").click(function () {

                $(this).toggleClass("fa-eye fa-eye-slash");
                var input = $($(this).attr("toggle"));
                if (document.getElementById("<%=txtPasswords.ClientID %>").type == "password") {
                    document.getElementById("<%=txtPasswords.ClientID %>").type = 'text';
                    //input.attr("type", "text");
                } else {
                    document.getElementById("<%=txtPasswords.ClientID %>").type = 'password';
                    //input.attr("type", "password");
                }


              });

            $('#<%=txtPasswords.ClientID%>').keyup(function () {

                 // set password variable
                 var pswd = $(this).val();
                 //alert(pswd);
                 //validate the length
                 if (pswd.length >= 8) {
                     $('#plength').empty();
                     $('#msgstart').empty();
                     //$('#<%=lblvalidmsg.ClientID%>').html("Atleast Length should be  8");
                } else {
                    $('#msgstart').html("Atleast ");
                    $('#plength').html("Minimum 8 Characters");
                }

                //validate number
                if (pswd.match(/\d/)) {
                    $('#pnum').empty();
                } else {
                    //$('#<%=lblvalidmsg.ClientID%>').html("Atleast One number");
                    $('#msgstart').html("Atleast ");
                    $('#pnum').html("One number,");

                }

                //validate special character
                if (pswd.match(/[!@#$ %^&*]/)) {
                    $('#pspl').empty();
                } else {
                    //$('#<%=lblvalidmsg.ClientID%>').html("Atleast One Special character");
                    $('#msgstart').html("Atleast ");
                    $('#pspl').html("One Special Character,")

                }



                //validate capital letter
                if (pswd.match(/[A-Z]/)) {
                    $('#pcaps').empty();
                } else {
                    //$('#<%=lblvalidmsg.ClientID%>').html("Atleast One Capital letter");
                    $('#msgstart').html("Atleast ");
                    $('#pcaps').html("One Uppercase,");
                }

                //validate letter
                if (pswd.match(/[a-z]/)) {
                    $('#psmall').empty();
                } else {
                    //$('#<%=lblvalidmsg.ClientID%>').html("Atleast One small letter");
                    $('#msgstart').html("Atleast ");
                    $('#psmall').html("One Lowercase,");

                }


            });
             $('#<%=txtPasswords.ClientID%>').focus(function () {
                 $('#<%=lblvalidmsg.ClientID%>').show();
            });
             $('#<%=txtPasswords%>').blur(function () {
                 $('#<%=lblvalidmsg.ClientID%>').hide();
            });

         });
    </script>
    <script>
        function myFunction() {
            var code = $("#<%=txtusercode.ClientID %>").val();
            var name = $("#<%=txtUserName.ClientID %>").val();
            var pass = $("#<%=txtPasswords.ClientID %>").val();
            var login = $("#<%=txtLoginName.ClientID %>").val();
            var email = $("#<%=txtmailid.ClientID %>").val();
            var usertype = $("#<%=cmbusertype.ClientID %>").val();
            if (code == "null" || code == null || code == "Please Select") {

                swal({
                    title: "Code is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (name == "null" || name == null || name == "Please Select") {

                swal({
                    title: "Username is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (login == "null" || login == null || login == "Please Select") {

                swal({
                    title: "LoginName is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (pass == "null" || pass == null || pass == "Please Select") {

                swal({
                    title: "Password is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (email == "null" || email == null || email == "Please Select") {

                swal({
                    title: "EmailID is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
            if (usertype == "null" || usertype == null || usertype == "Please Select") {

                swal({
                    title: "Usertype is Required",
                    text: "",
                    icon: "warning",

                });
                return false;
            }
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
    </style>
    <div class="col-12">
        <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white"><asp:Label ID="lblHeader" Text="New User" runat="server"></asp:Label></h4>

        </div>
        <div class="card-body">
            <div class="form-body">

                <div class="form-actions">
                    <asp:Label ID="err" runat="server"></asp:Label>
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="8" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;"  OnClick="btnback_Click" formnovalidate="formnovalidate" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="9" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;"  OnClientClick="return myFunction();" OnClick="OnBtnSaveClick"  />
                </div>
                 <br />
                    <br />
                    <ul class="nav nav-tabs customtab" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#User" role="tab"><span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down">User </span></a></li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#Details" role="tab"><span class="hidden-sm-up"><i class="ti-user"></i></span><span class="hidden-xs-down">Profile</span></a> </li>
                    </ul>
                 <div class="tab-content">
                        <div class="tab-pane active " id="User" role="tabpanel">
                <div class="container-fluid">

                    <asp:Label ID="lblcompanyid" runat="server" Style="visibility: hidden" Text=""></asp:Label>

                   

                        <div class="row p-t-10">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">User Name</label>
                                        <asp:TextBox ID="txtUserName" Class="form-control"  runat="server" MaxLength="50" TabIndex="1" autofocus="true" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">User Code</label>
                                        <asp:TextBox ID="txtusercode" Class="form-control" runat="server" MaxLength="30" TabIndex="1" autofocus="true" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Login Name</label>
                                        <asp:TextBox ID="txtLoginName" Class="form-control" runat="server" MaxLength="50" TabIndex="3" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Password</label>
                                        <asp:TextBox ID="txtPasswords" type="password" Class="form-control"  OnKeyUp="validatepassword()"
                                             runat="server" MaxLength="20" AutoCompleteType="None" TabIndex="4"></asp:TextBox>
                                        <span toggle="#txtPasswords" class="fa fa-fw fa-eye field-icon toggle-password"></span>
                                        <asp:Label ID="lblvalidmsg" runat="server" Style="color: red"><span id="msgstart"> </span>
                                            <span id="psmall"></span><span id="pcaps"></span><span id="pnum"></span><span id="pspl"></span><span id="plength"></span></asp:Label>
                                        <input type="hidden" id="pwdstatus" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                       
                                        <label class="control-label">Mobile</label>
                                        <asp:TextBox ID="txtmobile" Class="form-control" runat="server" MaxLength="15" AutoCompleteType="Disabled" TabIndex="5" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Email ID<span class="text-danger" style="font-size: larger;"></span></label>
                            <span id="check"></span>
                            <asp:TextBox ID="txtmailid" runat="server" TabIndex="6" MaxLength="60" CssClass="form-control" placeholder="Enter Email ID" required="required" onkeydown="CheckEmailAddress(this.value);">
                            </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                    <div class="row" style="display:none;">
                        <div class="col-md-6">
                             <div class="form-group">
                                <label class="control-label">User Type</label>
                             <asp:DropDownList ID="cmbusertype" runat="server"  Class="select2 form-control"  >
                                            </asp:DropDownList>   
                                 </div>  
                        </div>
                    </div>
                        <asp:Label ID="lblval" runat="server" Text="Customer Code" Visible="false"></asp:Label>
                    </div>


            </div>
       
           
                 <div class="tab-pane" id="Details" role="tabpanel">
                            <div class="row p-t-10">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Photo</h4>
                                            <asp:Label ID="lblUserFile" runat="server" Visible="false" TabIndex="7"></asp:Label>
                                            <%--  <input type="file" id="UserPerson"  class="dropify" runat="server" />--%>
                                            <asp:FileUpload ID="FileUpload1" runat="server" type="file" class="dropify"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
            </div>
    </div>
    </div>
        </div>
    
      <script language="JavaScript">
          var ev = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/; var x = document.getElementById("check");
          function CheckEmailAddress(email)
          { if (!ev.test(email)) { x.innerHTML = "Invalid Email..!!"; x.style.color = "red" } else { x.innerHTML = "&#10004;"; x.style.color = "green" } } </script>
    <asp:HiddenField ID="hdnUserID" runat="server" />
</asp:Content>
