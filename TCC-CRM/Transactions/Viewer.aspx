<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer.aspx.cs" Inherits="MPORT.Discussions.WebForm2" %>

<%@ Register TagPrefix="GleamTech" Namespace="GleamTech.Examples" Assembly="GleamTech.Core" %>
<%@ Register TagPrefix="GleamTech" Namespace="GleamTech.DocumentUltimate.AspNet.WebForms" Assembly="GleamTech.DocumentUltimate" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <GleamTech:DocumentViewerControl ID="documentViewer" runat="server" 
        Width="100%" 
        Height="600" 
        Resizable="True" />

    </div>
    </form>
</body>
</html>
