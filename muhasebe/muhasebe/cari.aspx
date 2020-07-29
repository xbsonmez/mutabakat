<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cari.aspx.cs" Inherits="muhasebe.cari" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>ADALICAM MUHASEBE MUTABAKAT SİSTEMİ - CARİ</title>
    <!--// Stylesheets //-->
    <link href="assets/css/style.css" rel="stylesheet" media="screen" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/icheck.min.js"></script>
    <!--[if lt IE 9]>
	    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
	    <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <script language="javascript" type="text/javascript">
        function Alert(e) {
            alert(e);
        }
    </script>
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
    <script language="javascript" type="text/javascript">
        function validateEmail(email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        }



        function addEmail() {

            var id = "";
            var objID = $("#hfID");
            id = objID.val();

            if (id == "") {
                alert('Lütfen bir firma seçiniz');
                return;
            }

            var email = document.getElementById('<%=txt_mail.ClientID%>').value;
            if (email == "") {
                alert('Bir mail adresi giriniz');
                return;
            }
            if (validateEmail(email)) {

                var objList = document.getElementById('lst_mail');

                for (var i = 0; i < objList.options.length; i++) {
                    if (objList.options[i].text == email) {
                        alert('Bu mail listede mevcuttur.');
                        return;
                    }
                }

                var opt = document.createElement("option");
                opt.text = email;
                //opt.value = "0";
                objList.options.add(opt);
                addCompanyMail(email);
            }
            else {
                alert('Uygun formatta bir mail adresi giriniz');
                return;
            }
        }


        function deleteEmail() {

            var objList = document.getElementById('lst_mail');
            var q = 0;
            for (var i = 0; i < objList.options.length; i++) {
                if (objList.options[i].selected) {
                    deleteCompanyMail(objList.options[i].text);
                    objList.options[i] = null;
                    q++;
                }
            }
            if (q == 0) {
                alert('Silenecek mail adresi seçiniz');
            }


        }







        function getDetails() {

            var objList = document.getElementById('companyList');
            
            var id = "";
            for (var i = 0; i < objList.options.length; i++) {
                if (objList.options[i].selected) {
                    id = i;
                }
            }

            $.ajax({
                type: "POST",
                url: "CariService.asmx/getDetails",
                data: "id=" + id,
                success: OnSuccessCall,
                error: OnErrorCall
            });

        }
        function sendMailAllSelectedCompanies() {
            let childElement = document.getElementById("GridView1_SelectCheckBox");
            var count = 0;
            let companyRows = childElement.parentNode.parentNode.parentNode.parentNode;
            for (let i = 1; i < companyRows.rows.length ; i++) {

                if (companyRows.rows[i].firstElementChild.firstElementChild.firstChild.checked) {
                 
                    var id = i - 1;
                    sendAllCompaniesMails(id);
                    count++;
                }
            }
            alert("Gönderilen Mail Sayısı :>>" + count);

        }
        function sendAllCompaniesMails(id) {
            

            var a = $.ajax({
                type: "POST",
                url: "CariService.asmx/sendMail",
                data: "ID=" + id,
                success: OnSuccessAllSend,
                error: OnErrorAllSend
            });
            return a;


        }

        function OnSuccessAllSend(response) {
         
            if ($(response)) {

            }
        }

        function OnErrorAllSend(response) {
            alert("MAİL GÖNDERME HATASI :" + response);
        }
        function selectAllCompanies(selectHeader) {
            if (selectHeader) {
                var checkBoxRows = selectHeader.parentNode.parentNode.parentNode.parentNode;
                if (selectHeader.checked) {
                    selectHeader.checked = true;
                   
                    for (var i = 0; i < checkBoxRows.rows.length - 1; i++) {
                        var detailDocumentAboutRow = getDetailsSelection(i);
                        if (detailDocumentAboutRow.children[0].children[10].firstChild && detailDocumentAboutRow.children[0].children[11].firstChild) {
                            if (detailDocumentAboutRow.children[0].children[10].firstChild.textContent !== "") {
                               
                                if (detailDocumentAboutRow.children[0].children[11].firstChild.textContent == "false") {
                                    checkBoxRows.rows[i + 1].firstElementChild.firstElementChild.firstElementChild.checked = true;
                                    checkBoxRows.rows[i + 1].style.backgroundColor = '#cd0638';
                                    checkBoxRows.rows[i + 1].lastElementChild.style.color = 'white'
                                } else {
                                    checkBoxRows.rows[i + 1].style.backgroundColor = 'rgb(182, 214, 232)';
                                }
                            } else {
                                //checkBoxRows[i + 1].style.backgroundColor = '#0e8e40';
                            }
                        } else if (detailDocumentAboutRow.children[0].children[10] && detailDocumentAboutRow.children[0].children[11]) {

                            checkBoxRows.rows[i + 1].style.backgroundColor = 'yellow';
                        }
                    }

                } else {

                    for (var i = 0; i < checkBoxRows.rows.length; i++) {
                       
                        checkBoxRows.rows[i].firstElementChild.firstElementChild.firstElementChild.checked = false;
                        checkBoxRows.rows[i].style.backgroundColor = 'white';
                        checkBoxRows.rows[i].lastElementChild.style.color = 'black';
                    }

                }
            }

            return;
        }
        function checkColor(Detay) {
            let tbody = Detay.parentNode.parentNode.parentNode.parentNode;
          

            for (let i = 0; i < tbody.rows.length; i++) {
                tbody.rows[i].style.backgroundColor = 'white';
                tbody.rows[i].lastElementChild.style.color = 'black';
            }
            for (let i = 0; i < document.getElementsByClassName('showDetails').length; i++) {
                document.getElementsByClassName('showDetails')[i].style.backgroundColor = 'white';
            }
            Detay.style.backgroundColor = 'rgb(182, 214, 232)';
            var row = Detay.parentNode.parentNode.parentNode;
            row.style.backgroundColor = 'rgb(182, 214, 232)';
            row.lastElementChild.style.color = 'white'

        }
        function getDetailsSelection(i) {

            var detailDoc = $.ajax({
                type: "POST",
                url: "CariService.asmx/getDetails",
                data: "id=" + i,
                async: false,
                success: OnSuccessResponceCall,
                error: OnErrorCall
            });
           

            return detailDoc.responseXML;

        }
        function OnSuccessResponceCall(response) {
            return response;
        }
        function GetSelectedRow(Detay) {
            checkColor(Detay);


            var row = Detay.parentNode.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
         
            $.ajax({
                type: "POST",
                url: "CariService.asmx/getDetails",
                data: "id=" + rowIndex,
                success: OnSuccessCall,
                error: OnErrorCall
            });
            return false;

        }
        function OnSuccessCall(response) {
            $(response).find("Cari").each(function () {

                var objUnvan = document.getElementById('<%=lbl_unvan.ClientID %>');
                var objMuhasebeKod = document.getElementById('<%=lbl_muhasebeKod.ClientID %>');
                var objHesapKod = document.getElementById('<%= lbl_hesapKod.ClientID %>');
                var objBorc = document.getElementById('<%= lbl_borc.ClientID %>');
                var objBorcBakiye = document.getElementById('<%= lbl_borcBakiye.ClientID %>'); 
                var objAlacak = document.getElementById('<%= lbl_alacak.ClientID %>');
                var objAlacakBakiye = document.getElementById('<%= lbl_alacakBakiye.ClientID %>');
                var objvkno = document.getElementById('<%= lbl_vkno.ClientID %>'); 
                var objID = $("#hfID");

                objUnvan.innerHTML = $(this).find("unvan").text();
                objMuhasebeKod.innerHTML = $(this).find("muhasebeKodu").text();
                objHesapKod.innerHTML = $(this).find("hesapKodu").text();
                objBorc.innerHTML = $(this).find("borc").text();
                objBorcBakiye.innerHTML = $(this).find("borcBakiye").text();
                objAlacak.innerHTML = $(this).find("alacak").text();
                objAlacakBakiye.innerHTML = $(this).find("alacakBakiye").text();
                objID.val($(this).find("ID").text());
                objvkno.innerHTML = $(this).find("vkno").text();

                document.getElementById('<%=txt_mail.ClientID%>').value = "";
                RemoveMailItems();
                var objList = document.getElementById('lst_mail');
                var mailList = $(this).find("email").text();

                if (mailList != "") {

                    var parseMailArray = mailList.split(";");

                    for (var j = 0; j < parseMailArray.length; j++) {
                        var opt = document.createElement("option");
                        opt.text = parseMailArray[j];
                        objList.options.add(opt);
                    }
                }

                var mailGonderim = $(this).find("mailGonderim").text()

                if (mailGonderim == "true") {
                    $("#btnSendWrapper").hide();
                    $("#successWraper").show();
                }
                else {
                    $("#btnSendWrapper").show();
                    $("#successWraper").hide();
                }


            });
        }

        function OnErrorCall(response) {

            alert("HATA :" + response);
        }










        function addCompanyMail(m) {

            var id = "";
            var objID = $("#hfID");
            id = objID.val();

            $.ajax({
                type: "POST",
                url: "CariService.asmx/addMail",
                data: "ID=" + id + "&Mail=" + m,
                success: OnSuccessAddMaill,
                error: OnErrorAddMail
            });
        }
        function OnSuccessAddMaill(response) {
            if ($(response)) {
                document.getElementById('<%=txt_mail.ClientID%>').value = "";
            }
        }

        function OnErrorAddMail(response) {
            alert("MAİL EKLEME HATASI :" + response);
        }



        function RemoveMailItems() {
            var objList = document.getElementById('lst_mail');
            for (i = objList.length - 1; i >= 0; i--) {
                objList.options[i] = null;
            }
        }









        function deleteCompanyMail(m) {
            var id = "";
            var objID = $("#hfID");
            id = objID.val();

            $.ajax({
                type: "POST",
                url: "CariService.asmx/deleteMail",
                data: "ID=" + id + "&Mail=" + m,
                success: OnSuccessDeleteMaill,
                error: OnErrorDeleteMail
            });
        }
        function OnSuccessDeleteMaill(response) {
            if ($(response)) {
                alert
                document.getElementById('<%=txt_mail.ClientID%>').value = "";
            }
        }

        function OnErrorDeleteMail(response) {
            alert("MAİL SİLME HATASI :" + response);
        }






        function sendEmail() {
            var id = "";
            var objID = $("#hfID");
            id = objID.val();

            if (id == "") {
                alert("Mail gönderilecek bir firma seçiniz");
                return;
            }


            var objList2 = document.getElementById('lst_mail');
            if (objList2.options.length == 0) {
                alert("Lütfen bir mail adresi giriniz");
                return;
            }


            $("#btnSendWrapper").hide();
            $("#progressMail").show();

            $.ajax({
                type: "POST",
                url: "CariService.asmx/sendMail",
                data: "ID=" + id,
                success: OnSuccessSend,
                error: OnErrorSend
            });
        }

        function OnSuccessSend(response) {
            if ($(response)) {
                $("#progressMail").hide();
                $("#successWraper").show();
            }
        }

        function OnErrorSend(response) {
            $("#progressMail").hide();
            $("#btnSendWrapper").show();
            alert("MAİL GÖNDERME HATASI :" + response);
        }
        



    </script>
</head>
<body>
    <form id="form1" runat="server">
     <!-- Wrapper Start -->
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
        <!--Nav Container End-->
        
          <div class="xmlFileZone">
            <asp:Panel runat="server" ID="pnl_fileShow">
                <br />
                <asp:Literal ID="ltr_fChange" Text="" runat="server"></asp:Literal><br />
            </asp:Panel>
          </div>


        <div class="zone">
           
            <asp:Panel runat="server" ID="pnl_proccess">
                <asp:HiddenField ID="hfID" runat="server" Value="" />
                <div class="zone-left">
                    <asp:ListBox ID="companyList" runat="server" CssClass="list" Height="287px" SelectionMode="Multiple" Visible="false"></asp:ListBox>
                     
                    <asp:GridView  ID="GridView1" runat="server" CssClass="list" Height="287px" style="overflow-y: auto; position: relative;" >
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="SelectCheckBox" runat="server"
                                OnCheckedChanged="SelectCheckBox_OnCheckedChanged" style="width: 50px;padding-left: 10px;" AutoPostBack="True" OnClick="return selectAllCompanies(this);"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:CheckBox ID="SelectAll" runat="server" style="width: 50px !important;padding-left: 10px;"/>
                                 <asp:BoundField ID="Firmalar" HeaderText="Firmalar" style="width: 50px !important;padding-left: 10px;"/>
                                 <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                                  <asp:Button ID="Detay" class="btn"  ClientIDMode="Static" runat="server"  CssClass="showDetails"
                                       OnClientClick = "return GetSelectedRow(this)"> 
                            
                                          </asp:Button>
     
                                </ItemTemplate>
                                    
                            </asp:TemplateField>

                           
          
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="zone-right">
                    <div class="formTxt">
                        Tarih
                    </div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label Text="" runat="server" ID="lbl_tarih" /></strong></div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Vergi K. No
                    </div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label Text="" runat="server" ID="lbl_vkno" /></strong></div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Muhasebe Kodu
                    </div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label Text="" runat="server" ID="lbl_muhasebeKod" /></strong></div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Hesap Kodu
                    </div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label ID="lbl_hesapKod" Text="" runat="server"></asp:Label></strong></div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Ünvan
                    </div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label ID="lbl_unvan" Text="" runat="server"></asp:Label></strong>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Borç</div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label ID="lbl_borc" Text="" runat="server"></asp:Label> </strong>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Borç Bakiye</div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label ID="lbl_borcBakiye" Text="" runat="server"></asp:Label> </strong>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Alacak</div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label ID="lbl_alacak" runat="server" Text=""></asp:Label></strong>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Alacak Bakiye</div>
                    <div class="formItem">
                        : <strong>
                            <asp:Label ID="lbl_alacakBakiye" Text="" runat="server"></asp:Label></strong>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="formTxt">
                        Mail Adresleri</div>
                    <div class="formItem" style="width: 20px; float: left; width: 390px;">
                        <div style="float: left; width: 190px;">
                            <asp:ListBox runat="server" CssClass="list" Height="150px" Width="188px" ID="lst_mail">
                            </asp:ListBox>
                        </div>
                        <div style="float: left; padding-left: 10px;">
                            <asp:TextBox runat="server" ID="txt_mail" Text="" CssClass="txtBox" Width="160px" />
                            <div style="padding-top: 10px;">
                                <input type="button" value="Ekle" id="btn_add_mail" onclick="javascript:addEmail();"
                                    class="btn" style="width: 75px; padding-left: 16px;" />&nbsp;&nbsp;
                                <input type="button" value="Sil" id="Button1" onclick="javascript:deleteEmail();"
                                    class="btn" style="width: 75px; padding-left: 16px;" />
                                    <div class="btnWrapper">
                    <div id="btnSendWrapper">
                        <input type="button" value="Mail Gönder" id="btnSendmail" onclick="javascript: sendEmail();" class="btn" style="width: 120px; padding-left: 16px;" />
                    </div>
                     <div id="progressMail">Gönderiliyor</div>
                    <div id="successWraper">Gönderildi</div>
                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear">
                </div>
                  <div class="btnWrapper">
                    <div id="btnSendAllWrapper">
                        <input type="button" value="Toplu Mail Gönder" id="btnAllSendmail" onclick="javascript: sendMailAllSelectedCompanies();" class="btn" style="width: 155px; padding-left: 16px;" />
                    </div>
                     <div id="progressMail">Gönderiliyor</div>
                    <div id="successWraper">Gönderildi</div>
                </div>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
