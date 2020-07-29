<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mutabakatlar.aspx.cs" Inherits="muhasebe.mutabakatlar" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BS / BA MUTABAKAT ARŞİVİ</title>
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
        <br />
        <br />
        <!-- TABLOLAR -->
        <div style="padding-top: 10px; margin: auto; margin-bottom:30px;">
            <table width="300px" style="width: 300px; margin: auto; margin-bottom: 15px;">
                <tr>
                    <td>
                        Mutabakat Tipi :
                    </td>
                    <td>
                        <asp:DropDownList runat="server" CssClass="txtBox" Width="190px" AutoPostBack="false"
                            ID="ddl_tip">
                            <asp:ListItem Text="Tüm Mutabakatlar" Value="0" />
                            <asp:ListItem Text="Form BS Mutabakatlar" Value="1" />
                            <asp:ListItem Text="Form BA Mutabakatlar" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Durumu :
                    </td>
                    <td style="padding-top:5px">
                        <asp:DropDownList runat="server" CssClass="txtBox" Width="190px" AutoPostBack="false"
                            ID="ddlDurum">
                            <asp:ListItem Text="Tümü" Value="all" />
                            <asp:ListItem Text="Bekliyor" Value="0" />
                            <asp:ListItem Text="Mutabık" Value="1" />
                            <asp:ListItem Text="Mutabık Değil" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Dönem Ay - Yil :
                    </td>
                    <td style="padding-top:5px">
                        <asp:DropDownList runat="server" CssClass="txtBox" Width="98px" ID="ddlDonemAy">
                            <asp:ListItem Text="Tümü" Value="0" />
                            <asp:ListItem Text="Ocak" Value="Ocak" />
                            <asp:ListItem Text="Şubat" Value="Şubat" />
                            <asp:ListItem Text="Mart" Value="Mart" />
                            <asp:ListItem Text="Nisan" Value="Nisan" />
                            <asp:ListItem Text="Mayıs" Value="Mayıs" />
                            <asp:ListItem Text="Haziran" Value="Haziran" />
                            <asp:ListItem Text="Temmuz" Value="Temmuz" />
                            <asp:ListItem Text="Ağustos" Value="Ağustos" />
                            <asp:ListItem Text="Eylül" Value="Eylül" />
                            <asp:ListItem Text="Ekim" Value="Ekim" />
                            <asp:ListItem Text="Kasım" Value="Kasım" />
                            <asp:ListItem Text="Aralık" Value="Aralık" />
                        </asp:DropDownList>
                    -
                        <asp:DropDownList runat="server" CssClass="txtBox" Width="80px" ID="ddlYil">
                          
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                       
                    </td>
                    <td style="padding-top:5px">
                        <asp:Button runat="server" ID="btn_filter" CssClass="btn" Text="Filtrele" 
                            onclick="btn_filter_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <div class="table-box">
            <script type="text/javascript" src="assets/js/jquery.dataTables.min.js"></script>
            <table class="display table" id="example">
                <thead>
                    <tr>
                        <th style="min-width:100px;">
                            DÖNEM
                        </th>
                        <th style="min-width: 360px;">
                            FİRMA ÜNVANI
                        </th>
                        <th style="width: 180px;">
                            VERGİ NO
                        </th>
                        <th style="width: 170px;text-align: center;" >
                            BELGE SAYISI
                        </th>
                        <th style="width: 200px;text-align: right;">
                            TUTAR
                        </th>
                        <th style="width: 70px">
                            TİP
                        </th>
                        <th style="width: 180px">
                            GÖNDERİM TARİHİ
                        </th>
                        <th style="width: 180px">
                            DURUM
                        </th>
                        <th style="width: 180px">
                            YANIT TARİHİ
                        </th>
                        <th style="width: 180px">
                            YANITLAYAN
                        </th>
                        <th>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptList">
                        <ItemTemplate>
                            <tr class="gradeX">
                                <td valign="middle">
                                    <%# Eval("donem")%>
                                </td>
                                <td valign="middle">
                                    <%# Eval("unvan") %>
                                </td>
                                <td valign="middle">
                                    <%# Eval("vkno") %>
                                </td>
                                <td valign="middle" style="text-align: center">
                                    <%# Eval("belgeSayisi").ToString() %>
                                </td>
                                <td valign="middle" style="text-align: right;" >
                                    <%# Eval("malHizmetBedeli").ToString().Replace("TL","₺") %>
                                </td>
                                <td valign="middle" >
                                    <%# Eval("mutabakatTipi").ToString().Replace("1","BS").Replace("2","BA")%>
                                </td>
                                <td valign="middle">
                                    <%# Eval("gonderilmeTarihi").ToString().Split(' ')[0]%>
                                </td>
                                <td valign="middle">
                                    <%# Eval("durum").ToString().Replace("0", "Bekliyor").Replace("1", "<span style='color:green'>Mutabık</span>").Replace("2", "<span style='color:red'>Mutabık Değil</span>")%>
                                </td>
                                <td valign="middle">
                                    <%# Eval("yanitlanmaTarihi").ToString().Split(' ')[0]%>
                                </td>
                                <td>
                                    <%# Eval("yanitlayanMail")%>
                                </td>
                                <td>
                                    <a href="mutabakatlar.aspx?ID=<%#Eval("ID") %>">Sil</a>
                                    <a href="MutabakatDetails.aspx?ID=<%#Eval("ID") %>">Detay </a>
                                    
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
                            "sInfoEmpty": "Aradığınız kritere uygun kayıt bulunmamaktadır"

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
             <center>
            <asp:Literal Text="text" runat="server" ID="ltr_print" /></center>
        </div>
        <!-- TABLOLAR BİTİŞ -->
        <br />
       
    </div>
    </form>
</body>
</html>
