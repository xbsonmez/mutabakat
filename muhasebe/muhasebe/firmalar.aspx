<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="firmalar.aspx.cs" Inherits="muhasebe.firmalar" %>

<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FİRMALAR</title>
    <!--// Stylesheets //-->
    <link href="assets/css/style.css" rel="stylesheet" media="screen" />
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
        <br /><br />
         <!-- TABLOLAR -->
                           <div style="width:1020px; margin: auto;">
                            <div class="table-box" style="width:980px;">
                                <script type="text/javascript" src="assets/js/jquery.dataTables.min.js"></script>
                                <table class="display table" id="example">
                                    <thead>
                                        <tr>
                                            <th style="min-width:300px;">
                                                FİRMA ÜNVANI
                                            </th>
                                            <th style="width:300px;">
                                                VERGİ NO / TC NO
                                            </th>
                                            <th style="width:300px;">
                                                YETKİLİ MAİL ADRESLERİ
                                            </th>
                                             <th style="width:180px;">
                                                
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptList">
                                            <ItemTemplate>
                                                <tr class="gradeX">
                                                    <td valign="middle">
                                                        <%# Eval("unvan") %>
                                                    </td>
                                                    <td valign="middle">
                                                        <%# Eval("vkno") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("email").ToString().Replace(";","<br />") %>
                                                    </td>
                                                    <td valign="middle">
                                                       <a href="firmalar.aspx?cmd=1&id=<%#Eval("ID") %>">Düzenle</a>&nbsp;
                                                       <a href="firmalar.aspx?cmd=2&id=<%#Eval("ID") %>">Sil</a>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                                
                                <script type="text/javascript" language="javascript">
                                    var asInitVals = new Array();
                                    $(document).ready(function () {
                                        var oTable = $('#example').dataTable({
                                            "oLanguage": {
                                                "sSearch": "Tüm tabloda ara:",
                                                "sInfo": "_START_ to _END_ arası gösterim. Toplam _TOTAL_ kayıttan",
                                                "sInfoFiltered": "(Toplam _MAX_ kayıt içerisinden filtrelenmiştir)",
                                                "sFirst": "İlk",
                                                "sLast": "Son",
                                                "sNext": "İleri",
                                                "sPrevious": "Geri",
                                                "sEmptyTable": "Tabloda veri bulunmamaktadır.",
                                                "sZeroRecords": "Aradığınız kriterlere uygun kayıt bulunmamaktadır.",
                                                "sLengthMenu": "Sayfada _MENU_ gösterim",
                                                "sPageFirst": "İlk Sayfa",
                                                "sPagePrevious": "Son Sayfa",
                                                "sPageNext": "İleri",
                                                "sPageLast": "Geri",
                                                "sInfoEmpty": "Ekli firma bulunmamaktadır"
                                                
                                            }
                                        });

                                        $("tfoot input").keyup(function () {
                                            /* Filter on the column (the index) of this element */
                                            oTable.fnFilter(this.value, $("tfoot input").index(this));
                                        });



                                        /*
                                        * Support functions to provide a little bit of 'user friendlyness' to the textboxes in 
                                        * the footer
                                        */
                                        $("tfoot input").each(function (i) {
                                            asInitVals[i] = this.value;
                                        });

                                        $("tfoot input").focus(function () {
                                            if (this.className == "search_init") {
                                                this.className = "";
                                                this.value = "";
                                            }
                                        });

                                        $("tfoot input").blur(function (i) {
                                            if (this.value == "") {
                                                this.className = "search_init";
                                                this.value = asInitVals[$("tfoot input").index(this)];
                                            }
                                        });
                                    });

                                </script>
                            </div>

                            </div>
                            <!-- TABLOLAR BİTİŞ -->

                            
                            <asp:Panel ID="pnl_edit" runat="server" Visible="false">
                                <div class="edit">
                                    <div class="formTxt" style="padding-top: 7px;">FİRMA</div>
                                    <div class="formItem">: <asp:TextBox runat="server" ID="txt_firma" Text="" CssClass="txtBox" Width="250px" Enabled="false" /></div>
                                    <div class="clear"></div>

                                    <div class="formTxt" style="padding-top: 7px;">VKNO / TCNO</div>
                                    <div class="formItem">: <asp:TextBox runat="server" ID="txt_vkno" Text="" CssClass="txtBox" Width="250px"  Enabled="false" /></div>
                                    <div class="clear"></div>

                                    <div class="formTxt" style="padding-top: 40px;">YETKİLİ MAİL</div>
                                    <div class="formItem">: <asp:TextBox runat="server" ID="txt_mail" Text="" CssClass="txtBox" Width="250px" TextMode="MultiLine" Height="95px" /></div>
                                    <div class="clear"></div>

                                    <div class="formTxt">&nbsp;</div>
                                    <div class="formItem" style="padding-left:8px;"> <asp:Button ID="btn_edit" 
                                            runat="server" Text="Kaydet" CssClass="btn" onclick="btn_edit_Click" /></div>
                                    <div class="clear"></div>
                                </div>
                            </asp:Panel>
    </div>
    </form>
</body>
</html>
