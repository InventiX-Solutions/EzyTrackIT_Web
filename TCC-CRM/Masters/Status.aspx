<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Status.aspx.cs" Inherits="TCC_CRM.Masters.Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
    <script>

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

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
    <div class="col-12">
        <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white"><asp:Label ID="lblHeader" Text="New Status" runat="server"></asp:Label></h4>

        </div>
        <div class="card-body">
            <div class="form-body">

                <div class="form-actions">
                    <asp:Label ID="newerr" runat="server"></asp:Label>
                    <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="29" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnBtnBackClick"  formnovalidate="formnovalidate"/>
                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="28" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClick="OnbtnSaveClick" />
                </div>
                <div class="container-fluid">

                    <asp:Label ID="lblStatusID" runat="server" Style="visibility: hidden" Text=""></asp:Label>

                    <div class="form-group row p-t-20">

                        <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Status Code<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtscode" runat="server" TabIndex="3" MaxLength="50" CssClass="form-control" placeholder="Enter Code" required="required"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Status Name<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtsname" runat="server" TabIndex="4" MaxLength="100" CssClass="form-control" placeholder="Enter Name" required="required">
                            </asp:TextBox>
                        </div>
                         <div class="col-md-6">
                            <label class="col-md-6 col-form-label">Sequence No<span class="text-danger" style="font-size: larger;"></span></label>
                            <asp:TextBox ID="txtseqno" runat="server" TabIndex="5" MaxLength="5" CssClass="form-control" placeholder="Enter Sequence No" required="required"
                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </div>
                         <div class="col-md-6">
                         <div class="tab-pane" id="Details" role="tabpanel">
                            <div class="row p-t-10">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Status Image</h4>
                                            <asp:Label ID="lblstatusimageFile" runat="server" Visible="false" TabIndex="7"></asp:Label>
                                            <%--  <input type="file" id="UserPerson"  class="dropify" runat="server" />--%>
                                            <asp:FileUpload ID="statusimage" runat="server" type="file" class="dropify"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                             </div>
                         <asp:Label ID="lblvalue" runat="server" Text="Status Code" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnStatusID" runat="server" />
</asp:Content>