<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MutabakatDetails.aspx.cs" Inherits="muhasebe.MutabakatDetails" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ADALI CAM DEKORASYON MUTABAKAT</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    
    <link href="assets/css/bootstrap.css" rel="stylesheet" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <!--// Javascript //-->
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.accordion.js"></script>
    <script type="text/javascript" src="assets/js/jquery.custom-scrollbar.min.js"></script>
    <script type="text/javascript" src="assets/js/icheck.min.js"></script>
    <script type="text/javascript" src="assets/js/selectnav.min.js"></script>
    <script type="text/javascript" src="assets/js/functions.js"></script>
    <!--[if lt IE 9]>
	    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
	    <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <link href="css/navMenu.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        jQuery("document").ready(function ($) {

            var nav = $('.nav-container');

            $(window).scroll(function () {
                if ($(this).scrollTop() > 136) {
                    nav.addClass("f-nav");
                } else {
                    nav.removeClass("f-nav");
                }
            });
        });

    </script>
</head>
<body>

    <form id="form1" runat="server">
      <div class="wrapper">
        <!-- Nav Container -->
        <div class="nav-container">
            <div class="nav">
                <div class="logo">
                    <img src="img/adalicam_logo.png" alt="" />
                </div>
                <div class="menu">
                   
                    <uc1:menu ID="menu1" runat="server" />
                   
                </div>
            </div>
        </div>
    <div class="wrapperResult">
        <div class="adaliLogo"><img src="mail_files/adali_mail_logo.png" alt ="" /></div>
        <div class="tuvLogo"><img src="mail_files/tuv_logo.jpg" alt ="" /></div>
        <div class="clear"></div>
        <div class="resultDetails">
            <br /><br />
             <asp:Literal ID="ltr_durum" runat="server" Text=""></asp:Literal>
             <br /><br />
               Müşterinin yanıtı;
             <br /><br />
     
             <asp:TextBox runat="server" ID="descDetail" Text="" CssClass="txtBox" TextMode="MultiLine"   Rows="6" Width="560px" ReadOnly="true"/>

             <br />
            <br />
             Ek dosya;
             <br />
               <asp:TextBox runat="server" ID="fileName" Text="" CssClass="txtBox"  TextMode="MultiLine" Width="250px" ReadOnly="true"/>
             <br />
             <br />
               <asp:Button ID="saveFile" runat="server" OnClick="btnGet_Click"  Text="İndir " CssClass="btn"   style="width: 100px;" />

             <br />
           
		<br /><br />
        
        </div>
                  

    </div>
        <div class="wrapperResult">
           <h1 style="text-align:center;"> MUTABAKAT BİLGİLERİNİ DÜZENLE VE TEKRAR GÖNDER </h1> 
        <div class="clear"></div>
        <div class="resultDetails">
            <br /><br />
             <asp:Literal ID="Literal1" runat="server" Text=""></asp:Literal>
             <br /><br />
            
             <asp:Literal ID="Literal2" runat="server" Text=""></asp:Literal>
            
              
             <br />
                Şirket Adı: <asp:TextBox runat="server" ID="unvanCompany" Text="" CssClass="txtBox" Width="250px" ReadOnly="true"/>
             <br />
             <br />
              
             <br />
               VKNO: <asp:TextBox runat="server" ID="vkno" Text="" CssClass="txtBox" Width="180px" ReadOnly="true"/>
             <br />
             <br />
              
             <br />
               Dönem:<asp:TextBox runat="server" ID="donem" Text="" CssClass="txtBox"  Width="100px" ReadOnly="true"/>
             <br />
             <br />
               
             <br />
              Yanıtlayan Mail Adressi:  <asp:TextBox runat="server" ID="yanitlayanMail" Text="" CssClass="txtBox"  Width="200px" ReadOnly="true"/>
             <br />
             <br />
             
             <br />
               Gönderilme Tarihi:   <asp:TextBox runat="server" ID="gonderilmeTarihi" Text="" CssClass="txtBox"  Width="200px" ReadOnly="true"/>
             <br />
             <br />
               Durum: <asp:TextBox runat="server" ID="durum" Text="" CssClass="txtBox" Width="100px" ReadOnly="true"/>
             <br />
             <br />
             <br />
             
               Mailler:  <asp:TextBox runat="server" ID="Mailler" Text="" CssClass="txtBox" Width="200px" ReadOnly="true"/>
             <br />
             <br />
            
             <br />
               Hizmet Bedeli: <asp:TextBox runat="server" ID="MalHizmetBedeli" Text="" CssClass="txtBox"  Width="180px" ReadOnly="true"/>
             <br />
             <br />
              
             <br />
          
               Belge Sayısı: <asp:TextBox runat="server" ID="belgeSayisi" Text="" CssClass="txtBox" Width="100px" ReadOnly="true"/>
             <br />
             <br />
           
            <asp:Button ID="Button1" runat="server" OnClick="reSendMailAndSaveDetail" Visible="false"
                    Text="Kaydet Ve Tekrar Mail Gönder " CssClass="btn"   style="margin-left: 180px;margin-top: 30px;width: 230px;" />
             <br />
           
             <br />
             <br />
               
            <asp:Button ID="BtnEdit" runat="server" OnClick="btnEdit_Click"
                    Text="Düzenle " CssClass="btn"   style="margin-left: 180px;margin-top: 30px;width: 160px;" />
           
		<br /><br />
        
        </div>
    </div>
               </div>
    </form>
</body>
</html>

