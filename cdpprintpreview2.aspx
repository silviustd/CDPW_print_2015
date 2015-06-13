<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cdpprintpreview.aspx.cs" Inherits="CDPW.cdpprintpreview" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" />

    <asp:PlaceHolder ID="phReport" runat="server">
        <rsweb:ReportViewer ID="rwPrintPreview" runat="server" SizeToReportContent="true" ShowPrintButton="true" ProcessingMode="Local">
            <LocalReport DisplayName="Testare" EnableExternalImages="true"></LocalReport>
        </rsweb:ReportViewer>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
        <span class="span50"></span>
        <p id="pError" class="msg_box msg_error corners">
            <asp:Literal ID="ltrError" runat="server" Text="Sorry, an error has occured. Please try again." />
        </p>
    </asp:PlaceHolder>
</asp:Content>
