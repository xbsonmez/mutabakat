<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mutabakat.aspx.cs" Inherits="muhasebe.mutabakat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ADALI CAM DEKORASYON MUTABAKAT</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapperResult">
        <div class="adaliLogo"><img src="mail_files/adali_mail_logo.png" alt ="" /></div>
        <div class="tuvLogo"><img src="mail_files/tuv_logo.jpg" alt =""  style=" width: 130px;"/></div>
        <div class="clear"></div>
        <div class="resultDetails">
             Sayın İlgili,<br /><br />
             <asp:Literal ID="ltr_durum" runat="server" Text=""></asp:Literal>
             <br /><br />
             <asp:TextBox runat="server" ID="txt_description" Text=""  Width="560px" TextMode="MultiLine"   Rows="6" style=" margin-top: 15px; "/>
              <asp:Label style="position:relative;right:-410px" ID="maxTextLimit" runat="server">  250 karakter girebilirsiniz!!</asp:Label>
                
             <asp:Literal ID="ltr_uyari" runat="server" Text=""></asp:Literal><br />
                   <div style="margin-top: 40px; margin-left: 0px;">
                  <asp:Label ID="why_txt" runat="server">  Neden Mutabık olmadığınızı açıklayan bir dosya ekleyebilirsiniz...</asp:Label>
                 <asp:FileUpload ID="fileUpload" runat="server" CssClass="custom-file-input" Width="300px" style="margin-top: 14px;"/>
                       </div>
                <br />
             <asp:Button ID="saveFile" runat="server" OnClick="btnUpload_Click"
                    Text="MUTABIK DEĞİLİZ " CssClass="btn"   style="margin-left: 180px;margin-top: 30px;width: 160px;" />
             <br />
           
             <br />
             <br />
             Saygılarımızla,<br /><br />
             007 014 26 63<br /><br />
             A D A L I C A M
		<br /><br />
        <div style="font-size:10px; text-align: center;"> <asp:Literal ID="ltr_ip" runat="server"></asp:Literal></div>
        </div>
    </div>
    </form>
</body>
</html>
