<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="EngineersList.aspx.cs" Inherits="TCC_CRM.Masters.Engineers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .card-header {
            padding: .75rem 1.25rem;
            margin-bottom: 0;
            background-color: #18306c;
            border-bottom: 1px solid rgba(0,0,0,.125);
        }

        @media (min-width: 768px) {
            .modal-xl {
                width: 60%;
                max-width: 1200px;
            }
        }
    </style>
     <script src="../assets/plugins/jquery/jquery.min.js"></script>
  <script type="text/javascript"> function successalert(test) { swal({ title: test, type: 'success' }); }

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

            $('#closemodal').click(function () {
                $('#<%= lblval.ClientID %>').text('');
                $('[id*=edit]').modal('hide');
            });
            $('#<%= gvEngineerlist.ClientID %>').DataTable(
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
                                return 'EngineerList_' + dateString;
                            }
                        },
                        {
                            text: 'New',
                            action: function (e, dt, node, config) {
                                //var url = "NewBrand.aspx?Brand_ID=0&field2=New";
                                //$(location).attr('href', url);
                                $('#<%= txtecode.ClientID %>').val('');
                                                $('#<%= txtename.ClientID %>').val('');
                                                $('#<%= lblengineerid.ClientID %>').text('0');
                                                $('#<%= hdnEngineerID.ClientID %>').val('0');
                                                $('#<%= txtmno.ClientID %>').val('');
                                                $('#<%= txtemailid.ClientID %>').val('');
                                                $('#<%= txtpassword.ClientID %>').val('');
                                                $('#<%= cmbvehicle.ClientID %>').val('Please Select')
                                $('#Heading').text('New Engineer');
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
  
 <div class="row" style="width:100%">
     <div class="modal" id="edit" tabindex="-1" role="dialog" aria-labelledby="edit" aria-hidden="true">
      <div class="modal-dialog modal-xl">
    <div class="modal-content">
          <div class="modal-header">
       
        <h4 class="modal-title " id="Heading">Edit Engineer</h4>
      </div>
        
        <div class="modal-body">
             <asp:Label ID="lblengineerid" runat="server" style="visibility:hidden" Text=""></asp:Label>
            <asp:HiddenField ID="hdnEngineerID" runat="server" />
          <div class="form-group">
              
              <asp:Label ID="lblcode" runat="server" Text="Engineer Code"></asp:Label>
               <asp:TextBox ID="txtecode" runat="server" TabIndex="3" MaxLength="50" CssClass="form-control" placeholder="Enter Code" required="required"></asp:TextBox>
          </div>
      <div class="form-group">
              <asp:Label ID="lblname" runat="server" Text="Engineer Name"></asp:Label>
               <asp:TextBox ID="txtename" runat="server" TabIndex="4" MaxLength="150" CssClass="form-control" placeholder="Enter Name" required="required">
               </asp:TextBox>
          </div>
             <div class="form-group">
              <asp:Label ID="lblskillname" runat="server" Text="Engineer Skill"></asp:Label>
               <asp:DropDownList ID="cmbskill" runat="server" TabIndex="5" Class="form-control">
                                            </asp:DropDownList> 
          </div>
             <div class="form-group">
              
              <asp:Label ID="lblmno" runat="server" Text="Mobile No"></asp:Label>
               <asp:TextBox ID="txtmno" runat="server" TabIndex="6" MaxLength="15" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Enter MobileNo"  onkeypress="return isNumberKey(event)"></asp:TextBox>
          </div>
             <div class="form-group">
              
              <asp:Label ID="lblemailid" runat="server" Text="Email Id"></asp:Label>
                 <span id="check"></span>
               <asp:TextBox ID="txtemailid" runat="server" TabIndex="7" MaxLength="50" CssClass="form-control" placeholder="Enter Email ID" required="required" onkeydown="CheckEmailAddress(this.value);">
               </asp:TextBox>
                 </div>
             <div class="form-group">
              
              <asp:Label ID="Label1" runat="server" Text="password"></asp:Label>
                 <span id="Span1"></span>
               <asp:TextBox ID="txtpassword" runat="server" TabIndex="8" MaxLength="15" TextMode="Password" CssClass="form-control" placeholder="password" required="required" >
               </asp:TextBox>
                 </div>

           <div class="form-group" id="divEngType" runat="server" >
    <asp:Label ID="lblengineertype" runat="server" Text="Engineer Type"></asp:Label>
    <asp:DropDownList ID="cmbengtype" runat="server" TabIndex="9" CssClass="form-control">
        <asp:ListItem Text="Please Select" Value="0" />
        <asp:ListItem Text="Engineer" Value="E" />    
        <asp:ListItem Text="Supervisor" Value="S" />
    </asp:DropDownList>
</div>
             <div class="form-group" id="divVehicle" runat="server">
              <asp:Label ID="lblvehicle" runat="server" Text="Vehicle"></asp:Label>
               <asp:DropDownList ID="cmbvehicle" runat="server" TabIndex="10" Class="form-control">
                                            </asp:DropDownList> 
          </div>

            <asp:Label ID="lblval" runat="server" Text="Engineer Code" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
      </div>
          <div class="modal-footer ">
        <asp:Button ID="btnSave" class="btn btn-info" runat="server" 
            OnClick="btnSave_Click" Text="Save"></asp:Button>
               
           <button type="button" class="btn btn-info" id="closemodal"  data-dismiss="modal" >
                            Close</button>
      </div>
             <script language="JavaScript">
                 var ev = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/; var x = document.getElementById("check");
                 function CheckEmailAddress(email) { if (!ev.test(email)) { x.innerHTML = "Invalid Email..!!"; x.style.color = "red" } else { x.innerHTML = "&#10004;"; x.style.color = "green" } } </script>

        </div>
        
    <!-- /.modal-content --> 
  </div>
      <!-- /.modal-dialog --> 
    </div>
     <div class="col-12">
           <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white">Engineer List</h4>
        </div>
        <div class="card">
            <div class="card-body">
                <asp:Button ID="btnnew" runat="server" Text="&#43; Add New" CssClass="btn btn-success" ToolTip="Save" Width="100px"
                TabIndex="28" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClientClick="openModal();" />
                <div class="table-responsive m-t-1">
                <asp:GridView ID="gvEngineerlist" runat="server" AutoGenerateColumns="true"  OnPreRender="GridView_PreRender" CssClass="display nowrap table"
                     DataKeyNames="engineer_id" OnRowDataBound="GridView_RowDataBound">
                      <Columns>
                        <asp:TemplateField HeaderText="engineer_id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblengineer_id" runat="server" Text='<%# Eval("engineer_id") %>'></asp:Label>
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
                             <asp:TemplateField HeaderText="Skill" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblskillname" runat="server" Text='<%# Eval("SkillName") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="mobileno" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmno" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="EmailID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemailid" runat="server" Text='<%# Eval("EmailID") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="password" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblpassword" runat="server" Text='<%# Eval("password") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Engineer Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblengtype" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

  <%--                        <asp:TemplateField HeaderText="Engineer Type">
    <ItemTemplate>
        <asp:Label ID="lblengtype" runat="server" Text='<%# GetEngineerTypeText(Eval("EngineerType")) %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>--%>

                                               <%--   <asp:TemplateField HeaderText="Engineer Type">
    <ItemTemplate>
        <asp:Label ID="lblengtype" runat="server" Text='<%# Eval("EngineerType") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>--%>
                             <asp:TemplateField HeaderText="Vehicle" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvehicle" runat="server" Text='<%# Eval("vehicle") %>'></asp:Label>
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
