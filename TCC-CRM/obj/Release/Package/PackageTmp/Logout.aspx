<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Nexus.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script src="../assets/plugins/jquery/jquery.min.js"></script>

    <script>
        $(function () {
            var Backlen = history.length;
            history.go(-Backlen);
            sessionStorage.clear();
            window.location.href = "Login.aspx";
        });
    </script>
</asp:Content>
