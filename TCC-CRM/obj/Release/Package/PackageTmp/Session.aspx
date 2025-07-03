<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Session.aspx.cs" Inherits="Nexus.Session" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script src="../assets/plugins/jquery/jquery.min.js"></script>
     <script>
         function redirect() {
             window.location.href = "Home.aspx";
         }
    </script>
 
    <section id="wrapper" class="error-page">
        <div class="error-box text-center">
            <div class="error-body text-center">
                <h1 class="text-info">409</h1>
                <h3 class="text-uppercase">Conflict !</h3>
                <p class="text-muted m-b-30" style="text-align: center;padding-bottom:10px">A Session is already running, Please logout and try again..!!</p>
               <%--<img src="../Support/images/maintenance.jpg" />--%>
               <%-- <a href="index.html" class="btn btn-info btn-rounded waves-effect waves-light m-b-40">Back to home</a>--%>
              

            </div>
             <button type="button" id="btnGO" onclick="redirect()" class="btn btn-outline-success btn-rounded " >Back to Home</button>
        </div>
        
    </section>
 

</asp:Content>
