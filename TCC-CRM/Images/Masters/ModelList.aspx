<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="ModelList.aspx.cs" Inherits="TCC_CRM.Masters.ModelList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .card-header {
            padding: .75rem 1.25rem;
            margin-bottom: 0;
            background-color: #18306c;
            border-bottom: 1px solid rgba(0,0,0,.125);
        }
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
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

    </script>
      <script>
          $(function () {
              $(".select2").select2();
          });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            function getParameterByName(name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }
            //var foo = getParameterByName('Saved');
            //if (foo == 1) {
            //    swal('Saved Successfully');
            //}
            $('#<%= cmbbrand.ClientID %>').change(function () {
                var value = $(this).val();

                $('#<%= hdnbrand.ClientID %>').val(value);

                   //alert(value);
               });

            $('#<%= ddlproduct.ClientID %>').change(function () {
                var value = $(this).val();

                $('#<%= hdnprod.ClientID %>').val(value);

                  //alert(value);
              });

            $('#closemodal').click(function () {
                $('#<%= lblval.ClientID %>').text('');

                $('[id*=edit]').modal('hide');
            });
            $('#<%= gvModellist.ClientID %>').DataTable(
                                {
                                    dom: 'Bfrtip',
                                    //iDisplayLength: gridsize,
                                    buttons: [
                                      {
                                          extend: 'excelHtml5',
                                          text: 'Excel',
                                          exportOptions: {
                                              columns: "thead th:not(.noExport)"
                                          },
                                          title: function () {
                                              var currentDate = new Date();

                                              var date = currentDate.getDate();
                                              var month = currentDate.getMonth(); //Be careful! January is 0 not 1
                                              var year = currentDate.getFullYear();

                                              var dateString = date + "-" + (month + 1) + "-" + year;
                                              return 'ModelList_' + dateString;
                                          }
                                      },
                                        {
                                            text: 'New',
                                            action: function (e, dt, node, config) {
                                                //var url = "NewBrand.aspx?Brand_ID=0&field2=New";
                                                //$(location).attr('href', url);
                                                $('#<%= txtbcode.ClientID %>').val('');
                                                $('#<%= txtbname.ClientID %>').val('');
                                                $('#<%= lblModelid.ClientID %>').text('');
                                                $('#<%= lblbrandid.ClientID %>').text('');
                                                $('#<%= lblval.ClientID %>').text('');
                                                $('#<%= hdnbrand.ClientID %>').val('');
                                                $('#<%= hdnprod.ClientID %>').val('');
                                                $('#<%= cmbbrand.ClientID %>').val('Please Select').trigger('change');
                                                $('#<%= ddlproduct.ClientID %>').val('Please Select');
                                                $('#Heading').text('New Model');
                                                openModal();
                                            }
                                        }
                                    ]
                                });
        });

    </script>
    <script type='text/javascript'>
        function openModal() {
            $('[id*=edit]').modal('show');
        }

    </script>

    <div class="row" style="width: 100%">
        <div class="modal" id="edit" tabindex="-1" role="dialog" aria-labelledby="edit" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title " id="Heading">Edit Model</h4>
                    </div>

                    <div class="modal-body">
                        <asp:Label ID="lblModelid" runat="server" Style="visibility: hidden" Text=""></asp:Label>
                         <div class="form-group">
                             <asp:Label ID="lblprodname" runat="server" Text="Product"></asp:Label>
                             <input type="text" id="hdnprod" name="hdnprod" runat="server" style="visibility:hidden" />
                               <asp:DropDownList ID="ddlproduct" runat="server" TabIndex="2" CssClass="select2 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged">
                            </asp:DropDownList>
                             </div>
                         <div class="form-group">
                            <asp:Label ID="lblbrandid" runat="server" Style="visibility: hidden" Text=""></asp:Label>
                            <asp:Label ID="lblbrandname" runat="server" Text="Brand Name"></asp:Label>
                                <input type="text"  id="hdnbrand" name="hdnbrand" runat="server" style="visibility:hidden" />
                            <asp:DropDownList ID="cmbbrand" runat="server" TabIndex="2" CssClass="select2 form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">

                            <asp:Label ID="lblcode" runat="server" Text="Model Code"></asp:Label>
                            <asp:TextBox ID="txtbcode" runat="server" TabIndex="3" MaxLength="30" CssClass="form-control" placeholder="Enter Code" required="required"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblname" runat="server" Text="Model Name"></asp:Label>
                            <asp:TextBox ID="txtbname" runat="server" TabIndex="4" MaxLength="60" CssClass="form-control" placeholder="Enter Name" required="required">
                            </asp:TextBox>
                        </div>
                       


                        <asp:Label ID="lblval" runat="server" Text="Model Code" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                    </div>
                    <div class="modal-footer ">
                        <asp:Button ID="btnSave" class="btn btn-info" runat="server"
                            OnClick="btnSave_Click" Text="Save"></asp:Button>

                        <button type="button" class="btn btn-info" id="closemodal" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="col-12">
            <div class="card-header" style="margin-top:-18px";>
                <h4 class="m-b-0 text-white">Model List</h4>
            </div>
            <div class="card">
                <div class="card-body">
                    <asp:Button ID="btnnew" runat="server" Text="&#43; Add New" CssClass="btn btn-success" ToolTip="Save" Width="100px"
                        TabIndex="28" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClientClick="openModal();" />

                    <div class="table-responsive m-t-1">
                        <asp:GridView ID="gvModellist" runat="server" AutoGenerateColumns="true" OnPreRender="GridView_PreRender" CssClass="display nowrap table"
                            DataKeyNames="Model_id" OnRowDataBound="GridView_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Model_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel_id" runat="server" Text='<%# Eval("Model_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="brand" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbrandName" runat="server" Text='<%# Eval("Brand Name") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprdname" runat="server" Text='<%# Eval("Product") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="noExport">

                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="<i class='fa fa-pencil' style='color: #0f52ba;'></i>"
                                            OnClick="Display"></asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnDelete" runat="server" Text="<i class='fa fa-trash-o' style='color: #0f52ba;'></i>"
                                            OnClick="Delete"></asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
