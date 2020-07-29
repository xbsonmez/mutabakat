<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printcari.aspx.cs" Inherits="muhasebe.printcari" %>

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
                        <td style="min-width: 90px;">
                            TARİH
                        </td>
                        <td style="min-width: 180px;">
                            FİRMA ÜNVANI
                        </td>
                        <td style="width: 90px;">
                            VERGİ NO
                        </td>
                        <td style="width: 170px;">
                            MUHASEBE KODU
                        </td>
                        <td style="width: 180px">
                            BORÇ BAKİYESİ
                        </td>
                        <td style="width: 180px">
                            ALACAK BAKİYESİ
                        </td>
                        <td style="width: 180px">
                            GÖNDERİM TARİHİ
                        </td>
                        <td style="width: 180px">
                            DURUM
                        </td>
                        <td style="width: 180px">
                            YANIT TARİHİ
                        </td>
                        <td style="width: 180px">
                            YANITLAYAN
                        </td>
                </tr>
           
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                
                              <td valign="middle">
                                    <%# Eval("tarih")%>
                                </td>
                                <td valign="middle">
                                    <%# Eval("unvan") %>
                                </td>
                                <td valign="middle">
                                    <%# Eval("vkno") %>
                                </td>
                                <td valign="middle">
                                    <%# Eval("muhasebeKodu") %>
                                </td>
                                <td valign="middle">
                                    <%# Eval("borcBakiye") %>
                                </td>
                                <td valign="middle">
                                    <%# Eval("alacakBakiye")%>
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
