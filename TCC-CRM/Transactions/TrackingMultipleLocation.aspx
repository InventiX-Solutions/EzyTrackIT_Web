<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTemplate/TCC-CRM.Master" AutoEventWireup="true" CodeBehind="TrackingMultipleLocation.aspx.cs" Inherits="TCC_CRM.Transactions.TrackingMultipleLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
   <%--  <style>
        /*  <span class="metadata-marker" style="display: none;" data-region_tag="css"></span>       Set the size of the div element that contains the map */
        #map {
            height: 400px;
            /* The height is 400 pixels */
            width: 100%;
            /* The width is the width of the web page */
        }
    </style>--%>
<%--    <script>
        var map;
        var InforObj = [];
        var centerCords = {
            lat: -25.344,
            lng: 131.036
        };

        var markersOnMap = "";

        $.ajax({

            type: "POST",
            url: "../Transactions/TrackingMultipleLocation.aspx/Test",
            data: JSON.stringify({
                TicketID: ticketid,
            }),

            contentType: "application/json; charset=utf-8",
            dataType: "json",

            error: function (jqXHR, sStatus, sErrorThrown) {
                // window.location.href = "../Login.aspx";
                alert(sErrorThrown);
            },

            success: function (data) {
                       
                 markersOnMap = data.d;
            }

        });
        //var markersOnMap = [{
        //    placeName: "Australia (Uluru)",
        //    LatLng: [{
        //        lat: -25.344,
        //        lng: 131.036
        //    }]
        //},
        //    {
        //        placeName: "Australia (Melbourne)",
        //        LatLng: [{
        //            lat: -37.852086,
        //            lng: 504.985963
        //        }]
        //    },
        //    {
        //        placeName: "Australia (Canberra)",
        //        LatLng: [{
        //            lat: -35.299085,
        //            lng: 509.109615
        //        }]
        //    },
        //    {
        //        placeName: "Australia (Gold Coast)",
        //        LatLng: [{
        //            lat: -28.013044,
        //            lng: 513.425586
        //        }]
        //    },
        //    {
        //        placeName: "Australia (Perth)",
        //        LatLng: [{
        //            lat: -31.951994,
        //            lng: 475.858081
        //        }]
        //    }
        //];

        window.onload = function () {
            initMap();
        };

        function addMarkerInfo() {
            for (var i = 0; i < markersOnMap.length; i++) {
                var contentString = '<div id="content"><h1>' + markersOnMap[i].placeName +
                    '</h1><p>Lorem ipsum dolor sit amet, vix mutat posse suscipit id, vel ea tantas omittam detraxit.</p></div>';

                var marker = new google.maps.Marker({
                    position: markersOnMap[i].LatLng[0],
                    map: map
                });

                var infowindow = new google.maps.InfoWindow({
                    content: contentString,
                    maxWidth: 200
                });

                marker.addListener('click', function () {
                    closeOtherInfo();
                    infowindow.open(marker.get('map'), marker);
                    InforObj[0] = infowindow;
                });
                // marker.addListener('mouseover', function () {
                //     closeOtherInfo();
                //     infowindow.open(marker.get('map'), marker);
                //     InforObj[0] = infowindow;
                // });
                // marker.addListener('mouseout', function () {
                //     closeOtherInfo();
                //     infowindow.close();
                //     InforObj[0] = infowindow;
                // });
            }
        }

        function closeOtherInfo() {
            if (InforObj.length > 0) {
                /* detach the info-window from the marker ... undocumented in the API docs */
                InforObj[0].set("marker", null);
                /* and close it */
                InforObj[0].close();
                /* blank the array */
                InforObj.length = 0;
            }
        }

        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                zoom: 4,
                center: centerCords
            });
            addMarkerInfo();
        }
    </script>--%>

      <div id="map"></div>

  <%--  <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB_KRurVRkcQ7TpyXf2-m3-4Oef6N-s2IY"></script>--%>
    <%--  <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOGtMMeVBugeFEQDs9hEotyrAQklRadbg&callback=initMap"></script>--%>
      <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCOGtMMeVBugeFEQDs9hEotyrAQklRadbg&callback=initMap"
  type="text/javascript"></script>

    <a href="https://www.google.com/maps/dir/12.965363,80.201653/12.975971,80.22120919999998/13.010236,80.215652/13.010236,80.215652/"><i class="fa fa-eercast"></i><span>&nbsp;</span> Service Type</a>
    

</asp:Content>
