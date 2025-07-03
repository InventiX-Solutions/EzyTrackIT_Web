<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="Milestone.aspx.cs" Inherits="TCC_CRM.Transactions.Milestone" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/tileview.css" rel="stylesheet">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <style>
        newcard {
            background: #fff;
            border-radius: 2px;
            display: inline-block;
            height: 150px;
            margin: 1rem;
            position: relative;
            width: 300px;
        }

        .timeline-panel {
            box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24) !important;
            transition: all 0.3s cubic-bezier(.25,.8,.25,1) !important;
        }

            .timeline-panel:hover {
                box-shadow: 0 14px 28px rgba(0,0,0,0.25), 0 10px 10px rgba(0,0,0,0.22) !important;
                cursor: pointer;
            }

        .badge-success {
            background-color: #28a745 !important;
        }
    </style>
    <script type="text/javascript">
        function getQueryStringValue(key) {
            return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
        }
    </script>
    <script>

        $(function () {
            $(".select2").select2();
            getmilestone();
        });

    </script>
    <script>
        function getmilestone() {
            var MileStonetable = $.parseJSON($("#<%=milestonedetails.ClientID %>").val());
            debugger;
            $("#example23_wrapper").empty();
            var str1 = "  <div class='container'> ";
            str1 = str1 + "  <div class='row'> ";
            str1 = str1 + "  <div class='col-12'> ";
            str1 = str1 + "  <div class='card'> ";
            str1 = str1 + "  <div class='card-body'> ";
            str1 = str1 + "  <div class='container'><div class='row'><h4 class='heading' style='font-size: 24px;'>Engineer Milestone</h4>  <div class='job-listing job-listing--featured ' style='background:#ECECF9'><div class='row'><div class='col-md-12 col-lg-6'><div class='row'>";
            str1 = str1 + " <div class='col-2'><img src='../images/customer.png' alt='Customer ' class='img-fluid'></div>";
            str1 = str1 + "<div class='col-10'> <h4 class='job__title' >" + MileStonetable.engcode + "</h4><p class='job__company'><i class='fa fa-envelope-o job__location'></i>" + MileStonetable.engemail + "</p><p class='job__company'><i class='fa fa-mobile job__location'></i>" + MileStonetable.engmobile + "</p></div> </div></div>";
            str1 = str1 + " 	<div class='col-md-12 col-lg-6'><div class='row' style='Visibility:hidden'><div class='col-2'><img src='../images/products.png' alt='Products ' class='img-fluid'></div><div class='col-10'><p class='job__title' >" + MileStonetable.engemail + "</p><p><i class='fa fa-quora job__location'></i>  " + MileStonetable.engemail + "</p><p><i class='fa fa-hand-o-right job__location'></i> " + MileStonetable.engemail + " / " + MileStonetable.engemail + "</p></div></div></div>";
            str1 = str1 + " </div> </div></div></div><br>";
            //str1 = str1 + "  <div class='container'><div class='row'> ";
            //str1 = str1 + "  <div class='input-group'> ";
            //str1 = str1 + " <input id='txtTrack' type='text' class='form-control' placeholder='OrderNo'  value=" + sOrderNo + "> "
            //str1 = str1 + " <div class='input-group-append'>"
            //str1 = str1 + " <button class='btn btn-info' type='button' onclick='getmilestone()'>Track!</button> "
            //str1 = str1 + " </div> ";
            //str1 = str1 + " </div> ";
            //str1 = str1 + " </div></div> ";
            if (MileStonetable.RowsMileStone.length > 0) {
                str1 = str1 + "  <div class='container'><div class='row'> ";

                str1 = str1 + " <ul class='timeline' style='width:100%'> ";

                var k = 0;
                debugger;
                var str2 = "";
                var str3 = "";
                var str4 = "";
                var str5 = "";
                var str6 = "";
                var ev1 = "";
                var ev2 = "";
                var classstr = "";

                for (i = 0; i <= MileStonetable.RowsMileStone.length - 1; i++) {

                    var milestones = ["Check-IN", "Check-OUT"];
                    var jd = "";
                    if (milestones.includes(MileStonetable.RowsMileStone[i].MilestoneName) == false) {
                        classstr = "timeline-inverted ";
                        jd = MileStonetable.RowsMileStone[i].jobdetails;
                    }
                    else {
                        classstr = "";
                    }

                    if (k > 1) {
                        k = 0;
                    }
                    if (k == 1) {
                        //var str1 = str1 + " <li class='timeline-inverted '> ";
                        //classstr = "timeline-inverted ";
                    }
                    else {
                        //var str1 = str1 + " <li> ";
                        //classstr = "";
                    }

                    var initialbg = "<span class='badge badge-pill badge-primary'>Started</span>";
                    var warningbdg = "<span class='badge badge-pill badge-warning'>Queuing Delay</span>";
                    var succcessbdg = "<span class='badge badge-pill badge-success'>On-Time</span>";
                    var dangerbdg = "<span class='badge badge-pill badge-danger'>Delayed</span>";
                    var badges = succcessbdg;
                    if (MileStonetable.RowsMileStone[i].timediff >= 60 && MileStonetable.RowsMileStone[i].timediff <= 120) {
                        badges = warningbdg;
                    }
                    if (MileStonetable.RowsMileStone[i].timediff > 120) {
                        badges = dangerbdg;
                    }

                    var df = "";
                    if (i == 0) {
                        df = initialbg;
                    }

                    str2 = str2 + " <li class='" + classstr + "'><div class='timeline-badge'><img class='img-responsive' alt='user' src='" + MileStonetable.RowsMileStone[i].Image + "'> </div> ";

                    str2 = str2 + " <div class='timeline-panel'> ";

                    str2 = str2 + " <div class='timeline-heading'> ";
                    str2 = str2 + "   <h3 class='timeline-title'>" + MileStonetable.RowsMileStone[i].MilestoneName + "    " + df + "   " + jd + " </h3> "
                    str2 = str2 + " <p></p>"
                    //var str1 = str1 + "   <h4 class='timeline-title'>" + MileStonetable.RowsMileStone[i].ScheduleTime + "</h4> "
                    //var str1 = str1 + " <p></p>"
                    //var str1 = str1 + "   <h4 class='timeline-title'>" + MileStonetable.RowsMileStone[i].ActualTime + "</h4> "
                    //var str1 = str1 + " <p></p>"
                    //var str1 = str1 + "   <h4 class='timeline-title'>" + MileStonetable.RowsMileStone[i].DelayTime + "</h4> "

                    //var str1 = str1 + " <p><small class='text-muted'><i class='fa fa-clock-o'></i> " + MileStonetable.RowsMileStone[i].ScheduleTime + "</small> </p> "
                    //var str1 = str1 + " <p><small class='text-muted'><i class='fa fa-clock-o'></i> " + MileStonetable.RowsMileStone[i].ActualTime + "</small> </p> "
                    //var str1 = str1 + " <p><small class='text-muted'><i class='fa fa-clock-o'></i> " + MileStonetable.RowsMileStone[i].DelayTime + "</small> </p> "
                    str2 = str2 + "  </div> ";

                    str2 = str2 + " <div class='timeline-body '> ";
                    //var str1 = str1 + " <p><img class='img-responsive' alt='user' src='../assets/images/users/3.jpg'></p> ";
                    //var str1 = str1 + "  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero laboriosam dolor perspiciatis omnis exercitationem. Beatae, officia pariatur? Est cum veniam excepturi. Maiores praesentium, porro voluptas suscipit facere rem dicta, debitis.</p> "
                    //var str1 = str1 + " <p> " + MileStonetable.RowsMileStone[i].ScheduleTime + "<b>" + MileStonetable.RowsMileStone[i].ActualTime + "</b>" + MileStonetable.RowsMileStone[i].DelayTime + " </p> "

                    str2 = str2 + " <p> Event Time: <b><span style='color:#525072;font-weight: 600;'>" + MileStonetable.RowsMileStone[i].MilestoneTime + "</span></b></p> "
                    if (i > 0) {

                        str2 = str2 + " <p> Time Taken: <b><span style='color:#525072;font-weight: 600;'>" + MileStonetable.RowsMileStone[i].timediff + " Minutes</span> " + badges + " </b></p> "
                    }

                    str2 = str2 + " <p></p>"

                    str2 = str2 + "  </div> ";

                    str2 = str2 + "  </div> </li>";
                    k = k + 1;

                    var str1 = str1 + str2;
                    var str2 = "";
                }
                str1 = str1 + "    </ul> ";
                str1 = str1 + "  </div> </div>";

                str1 = str1 + "  </div> ";

                str1 = str1 + "  </div> ";

                str1 = str1 + "  </div> ";

                str1 = str1 + "  </div> ";
                str1 = str1 + "  </div> ";
            }

            $("#example23_wrapper").append(str1);
            $(".footer").hide();
        }
    </script>
    <asp:HiddenField ID="milestonedetails" runat="server" />
    <asp:HiddenField ID="idval" runat="server" />
    <div class="container-fluid">
        <div class="col-12" style="margin-top: -18px;">
            <div class="card">
                <div class="card-header">
                    <h4 class="m-b-0 text-white">Engineer Milestone</h4>
                </div>
                <div class="card-body" style="margin-top: -18px">

                    <div class="container-fluid">
                        <div class="row">

                            <div class="col-md-2">
                                <label for="lblorderfromdt" class="Themefontstyles">From Date</label>
                                <dx:ASPxDateEdit ID="orderfromdt" runat="server" Theme="Glass" ClientInstanceName="sdate" DateRangeSettings-CalendarColumnCount="8"
                                    DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy" Font-Names="Poppins" Font-Size="13px">
                                </dx:ASPxDateEdit>
                            </div>
                            <div class="col-md-2">
                                <label for="lblordertodt" class="Themefontstyles">To Date</label>
                                <dx:ASPxDateEdit ID="ordertodt" Font-Names="Poppins" Font-Size="13px" ClientInstanceName="tdate" runat="server" Theme="Glass" DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></dx:ASPxDateEdit>
                            </div>


                            <div class="col-md-2">
                                <label for="lblcmb" class="Themefontstyles">Engineer</label>
                                <asp:DropDownList ID="cmbeng" runat="server" TabIndex="1" CssClass="select2 form-control" OnSelectedIndexChanged="cmbeng_SelectedIndexChanged" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <asp:Button ID="btngo" runat="server" Text="GO" CssClass="btn btn-success" ToolTip="GO"
                                    Width="70px" Height="30px" BackColor="#0f52ba" BorderColor="#0f52ba"
                                    TabIndex="29" Style="float: right; margin-top:30px; " OnClick="btn_go_click" />
                            </div>
                        </div>
                        <div class="row">
                            <div id="example23_wrapper" class='container row justify-content-center' style="max-width: 100%;">
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
