<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="UserRoles.aspx.cs" Inherits="TCC_CRM.Masters.UserRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script>
        function fnViewSelectAll(chkViewSelectAll, chkAddSelectAll, chkEditSelectAll, chkDeleteSelectAll) {
            var objchkViewSelectAll = document.getElementById(chkViewSelectAll)
            var objchkAddSelectAll = document.getElementById(chkAddSelectAll)
            var objchkEditSelectAll = document.getElementById(chkEditSelectAll)
            var objchkDeleteSelectAll = document.getElementById(chkDeleteSelectAll)

            var arr = document.getElementsByTagName("INPUT");

            if (objchkViewSelectAll.checked) {
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].id.indexOf("chkView") >= 0) {
                        arr[i].checked = true;
                    }
                }
            }
            else {
                objchkAddSelectAll.checked = false;
                objchkEditSelectAll.checked = false;
                objchkDeleteSelectAll.checked = false;

                for (var i = 0; i < arr.length; i++) {
                    if ((arr[i].id.indexOf("chkView") >= 0) || (arr[i].id.indexOf("chkEdit") >= 0) || (arr[i].id.indexOf("chkAdd") >= 0) || (arr[i].id.indexOf("chkDelete") >= 0)) {
                        arr[i].checked = false;
                    }
                }
            }
        }
    </script>
      <div class="col-12">
        <div class="card-header" style="margin-top:-18px";>
            <h4 class="m-b-0 text-white">User Roles</h4>

        </div>
        <div class="card-body">
            <div class="form-body">

                <div class="form-actions">
                    <asp:Label ID="err" runat="server"></asp:Label>
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-success" ToolTip="Back" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="8" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;" OnClientClick="BACK();"  />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ToolTip="Save" Width="70px" BackColor="#0f52ba" BorderColor="#0f52ba"
                        TabIndex="9" Style="float: right; position: relative; margin-top: 3px; margin-right: 10px;"  OnClientClick="return myFunction();"   />
                </div>
                  <div class="container-fluid">
                   <div class="form-group row p-t-10">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">Usertype </label>
                                    <asp:TextBox ID="txtusertype" Class="form-control" runat="server" MaxLength="30" TabIndex="1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdntypeid" runat="server" />
                         <div id="divgvRoleAccess" runat="server" class="portlet" style="width: 98%; height: auto; padding: 10px; overflow: auto">
                            <asp:GridView ID="gvRoleAccess" runat="server" AutoGenerateColumns="false" GridLines="None"
                                Width="100%" ForeColor="#333333" EmptyDataText="No records Found"  OnRowDataBound="OngvRoleAccessRowDataBound"
                                AlternatingRowStyle-CssClass="alt" CssClass="table table-bordered">
                                <RowStyle Height="25px" />
                                <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#67757c"></HeaderStyle>
                                <HeaderStyle CssClass="mGrid" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Screen Name">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblScreenName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem , "ScreenName") %>' />
                                            <asp:Label ID="lblScreenID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem , "ScreenID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem , "Category") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="htView" Checked="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkView" runat="server" Checked='<%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"ViewUser")) == true ? true : false %>'
                                                Visible='<%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"View_Screen")) == true ? true : false %>' />
                                            <asp:Label ID="lblView_Screen" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem , "ViewUser") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>

                      </div>
                </div>
            </div>
          </div>

</asp:Content>
