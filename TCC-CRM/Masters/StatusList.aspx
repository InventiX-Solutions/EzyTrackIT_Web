<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="StatusList.aspx.cs" Inherits="TCC_CRM.Masters.StatusList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
<style>
        .card-header {
            padding: .75rem 1.25rem;
            margin-bottom: 0;
            background-color: #18306c;
            border-bottom: 1px solid rgba(0,0,0,.125);
        }
    </style>
     <script src="../assets/plugins/jquery/jquery.min.js"></script>
  <script type="text/javascript"> function successalert(test)
 { swal({ title: test, type: 'success' }); }

  </script> 
     <script type="text/javascript">
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         } </script> 
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
            var foo = getParameterByName('Saved');
            if (foo == 1) {
                swal('Saved Successfully');
            }

            $('#closemodal').click(function () {
                $('[id*=edit]').modal('hide');
            });
            $('#<%= gvStatuslist.ClientID %>').DataTable(
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
                                              return 'StatusList_' + dateString;
                                          }
                                      },
                                        {
                                            text: 'New',
                                            action: function (e, dt, node, config) {
                                                var url = "Status.aspx?StatusID=0&field2=New";
                                                $(location).attr('href', url);



                                                // action: function (e, dt, node, config) {
                                                //var url = "NewBrand.aspx?Brand_ID=0&field2=New";
                                                //$(location).attr('href', url);

                                                // $('#Heading').text('New Status');
                                                // openModal();
                                            }
                                        }
                                    ]
                                });
        });

    </script>
<div class="row" style="width:100%">
     
     <div class="col-12">
           <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white">Status List</h4>
        </div>
        <div class="card">
            <div class="card-body">
                <div class="table-responsive m-t-1">
                <asp:GridView ID="gvStatuslist" runat="server" AutoGenerateColumns="true"  OnPreRender="GridView_PreRender" CssClass="display nowrap table"
                     DataKeyNames="StatusID" OnRowDataBound="GridView_RowDataBound">
                      <Columns>
                        <asp:TemplateField HeaderText="StatusID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblStatusID" Visible="false" runat="server"  Text='<%# Eval("StatusID")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Code" >
                    <ItemTemplate>
                        <asp:Label ID="lblcode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="noExport">
                         
                        <ItemTemplate>
                           
                            <asp:LinkButton ID="lnkBtnEdit" runat="server"  Text="<i class='fa fa-pencil' style='color: #0f52ba;'></i>" 
                                OnClick="Display" ></asp:LinkButton>
                           
                             <asp:LinkButton ID="lnkBtnDelete" runat="server"  Text="<i class='fa fa-trash-o' style='color: #0f52ba;'></i>" 
                                OnClick="Delete" ></asp:LinkButton>
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
