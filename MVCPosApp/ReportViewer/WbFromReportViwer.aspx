<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WbFromReportViwer.aspx.cs" Inherits="MVCPosApp.ReportViewer.WbFromReportViwer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="width: 100%;">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="margin: auto; width: 50%">
                            <table style="text-align: center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnPdf" OnClick="btnPdf_Click" runat="server" Text="PDF" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnPdf" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="div_reportViewerHolder" runat="server" style="height: auto; width: 90%; position: relative; background-color: White; z-index: 0; margin-left: auto; margin-right: auto">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" Width="1110px" Height="1110px" AutoDataBind="true" />
            </div>
        </div>
    </form>
</body>
</html>
