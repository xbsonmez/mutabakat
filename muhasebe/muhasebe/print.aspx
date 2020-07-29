<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print.aspx.cs" Inherits="muhasebe.print" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    body { margin: 0 0; 
     font-size: 10px;
     font-family: Tahoma;       
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table border="1" cellpadding="0" cellspacing="0" style="width:100%; border: solid 1px #e0e0e0;">
                <tr>
                        <td style="min-width:100px;">
                                        <strong> DÖNEM</strong>
                                            </td>
                                            <td style="min-width:180px;">
                                                <strong>FİRMA ÜNVANI</strong>
                                            </td>
                                            <td style="width:180px;">
                                                <strong>VERGİ NO</strong>
                                            </td>
                                            <td style="width:180px;">
                                                <strong>BELGE SAYISI</strong>
                                            </td>
                                             <td style="width:180px">
                                                <strong> TUTAR</strong>
                                            </td>
                                             <td style="width:100px">
                                                <strong> TİP</strong>
                                            </td>
                                            <td style="width:180px">
                                                <strong> GÖNDERİM TARİHİ</strong>
                                            </td>
                                            <td style="width:180px">
                                                <strong>DURUM</strong>
                                            </td>
                                             <td style="width:180px">
                                               <strong> YANIT TARİHİ</strong>
                                            </td>
                                              <td style="width:180px">
                                               <strong> YANITLAYAN</strong>
                                            </td>
                </tr>
           
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td valign="middle">
                                                        <%# Eval("donem")%>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("unvan") %>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("vkno") %>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("belgeSayisi").ToString() %>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("malHizmetBedeli") %>
                                                    </td>
                                                     <td valign="middle">
                                                        <%# Eval("mutabakatTipi").ToString().Replace("1","BS").Replace("2","BA")%>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("gonderilmeTarihi")%>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("durum").ToString().Replace("0", "Bekliyor").Replace("1", "<span style='color:green'>Mutabık</span>").Replace("2", "<span style='color:red'>Mutabık Değil</span>")%>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("yanitlanmaTarihi")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("yanitlayanMail")%>
                                                    </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
         </table>
        </FooterTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
