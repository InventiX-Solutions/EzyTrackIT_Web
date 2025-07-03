<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketPrint.aspx.cs" Inherits="TCC_CRM.Transactions.TicketPrint" %>


<!DOCTYPE html>
<html lang="en">

<head id="Head1" runat="server">
  <meta charset="utf-8">
  <title>Print Ticket</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">  
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>  


  <link rel="stylesheet" href="../css/normalize.css">

  <link rel="stylesheet" href="../css/paper.css">
        <script src="../assets/plugins/jquery/jquery.min.js"></script>
  <style>
      @page {
          size: A4;
      }

      tr.spaceUnder > td {
          padding-bottom: 40em;
      }

      table, th, td {
          border: none;
      }

      textarea {
          resize: none;
      }

      .view {
          display: none;
      }

      .hide {
          display: none;
      }

      body {
          font-size: 13px;
      }

          body.A4 .sheet {
              width: 275mm;
              /*body.A4 .sheet {
    width: 275mm;   height: 260mm;
   /*height: 440mm;*/ /*Standard A4 Size*/
              height: 380mm;
          }

      body {
          counter-reset: Serial2;
      }

      .auto-index2 tbody td:first-child:before {
          counter-increment: Serial2; /* Increment the Serial counter */
          content: counter(Serial2); /* Display the counter */
      }
  </style>
   
  <script>
      $(document).ready(function () {
          $(function () {
              mp_GetData();
          });
          function getUrlVars() {
              var vars = [], hash;
              var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
              for (var i = 0; i < hashes.length; i++) {
                  hash = hashes[i].split('=');
                  vars.push(hash[0]);
                  vars[hash[0]] = hash[1];
              }
              return vars;
          }

          var addSerialNumber = function () {
              var i = 0
              $('.auto-index tbody tr').each(function (index) {
                  $(this).find('td:nth-child(1)').html(index + 1);
              });
              //AddSumofhours();
          };
          function mp_GetData() {
              var ticketid = getUrlVars()["TicketID"];
              if (ticketid > 0) {
                  $.ajax({
                      type: "POST",
                      url: "../Transactions/TicketPrint.aspx/TicketEdit",
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
                          var TicketTable = data.d;
                          $("#clientcode").append(TicketTable.ClientCode);
                          $("#ticketno").append(TicketTable.TicketNo);
                          $("#ticketdate").append(TicketTable.Date);
                          $("#tickettime").append(TicketTable.TicketTime);
                          
                          $("#customer").append(TicketTable.CustomerName);
                          $("#product").append(TicketTable.ProductName);
                          $("#servicetype").append(TicketTable.ServiceType);
                          $("#problem").append(TicketTable.NatureOfProblem);
                          $("#assignedto").append(TicketTable.EngineerName);
                          $("#brand").append(TicketTable.BrandName);
                          $("#remarks").append(TicketTable.remarks);
                          $("#model").append(TicketTable.ModelName);
                          $("#address").append(TicketTable.CustomerAddress);
                          $("#contactperson").append(TicketTable.ContactPerson);
                          $("#phoneno").append(TicketTable.PhoneNo);
                          $("#serialno").append(TicketTable.SerialNumber);
                         // $("#ticketdate")
                          var logos = "<img src='" + TicketTable.companylogo + "' height='70px' width='150px'/>";
                          //  $("#imgdiv").append(logos);
                          $("#companynamediv").append(TicketTable.companyname);
                          $("#addressdiv").append(TicketTable.address1 + TicketTable.address2 + TicketTable.PostalCode + TicketTable.country + TicketTable.PhoneNumber);
                          //$("#addressdiv2").append(TicketTable.country + " - " + TicketTable.PostalCode + ",");
                         // $("#addressdiv2").append(TicketTable.PhoneNumber);
                         $("#companycode").append(TicketTable.CompanyCode);
                          var imgdiv = "<img src='" + TicketTable.barcodeimage + "' '/>";
                          $("#barcode").append(imgdiv);
                          var statusrows = "";

                          var statusemptyrows = "<tr><td style='text-align:center;border: 1px solid black;padding: 4px;'></td><td style='text-align:center;border: 1px solid black;padding: 4px;'></td><td style='text-align:center;border: 1px solid black;padding: 4px;'></td><td style='text-align:center;border: 1px solid black;padding: 4px;'></td><td class='calsum' style='text-align:center;border: 1px solid black;padding: 4px;'></td><td style='text-align:center;border: 1px solid black;padding: 4px;'></td></tr>";
                          for (j = 0; j <= TicketTable.ticketStatus.length - 1; j++) {
                              statusrows = statusrows + "<tr><td style='text-align:center;border: 1px solid black;padding: 4px;'></td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.ticketStatus[j].NewStatus + "</td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.ticketStatus[j].starttime + "</td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.ticketStatus[j].endtime + "</td><td class='calsum' style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.ticketStatus[j].tothrs + "</td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.ticketStatus[j].remarks + "</td></tr>";
                          }
                          for (k = 0; k <= 2 - 1; k++) {
                              statusrows = statusrows + statusemptyrows;
                          }
                          $("#tblstatus").empty();
                          $("#tblstatus").append(statusrows);
                          var partrows = "";
                          for (i = 0; i <= TicketTable.partdetails.length - 1; i++) {
                              partrows = partrows + "<tr><td style='text-align:center;border: 1px solid black;padding: 4px;'></td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.partdetails[i].old_ref1 + "</td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.partdetails[i].old_ref2 + "</td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.partdetails[i].new_ref1 + "</td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.partdetails[i].new_ref2 + "</td><td style='text-align:center;border: 1px solid black;padding: 4px;'>" + TicketTable.partdetails[i].remarks + "</td></tr>";
                          }
                          for (l = 0; l <= 2 - 1; l++) {
                              partrows = partrows + statusemptyrows;
                          }
                          $("#tblpart").empty();
                          $("#tblpart").append(partrows);
                          addSerialNumber()
                      }
                  });
              }
          }
      });
  </script>
</head>

<body class="A4" >
   <div id="maindiv">
       <section class='sheet' style='padding:25px;'>
         <%--  <div class='row' style='height:35px; border:1px solid #000;'>
               <div class='col-12' style='text-align:center;'>
                   <span style='font-size:24px;font-weight:bold;'>JOB SHEET</span>
                   </div>
               </div>--%>
             <div class='row' >
                 
                    <div class='col-7' style='height:130px;text-align:left;margin-top:-8px;'>
                       
                        <p style='font-size:37px;text-align:left;'><b><span id="companynamediv"></span></b></p>
                        <p style='font-size:17px;margin-top: -15px;text-align:left;font-weight:bold;'><span id="addressdiv"></span></p>
                        <p style='font-size:16px;margin-top: -15px;text-align:left;font-weight:bold;'>VAT NO: 114148440-7000</p>
                      <%--   <p style='padding-left:4px;font-size:18px;margin-top: -15px;text-align:left;'><span id="addressdiv3"></span></p>--%>
                       <%-- <p style='padding-left:4px;font-size:18px;margin-top: -15px;text-align:left;'><span id="addressdiv3"></span></p>--%>
                          

                    
                        </div><!--VAT NO: 114148440-7000-->
                  <div class='col-1' style='height:100px;'><br />
                       <p style='padding-right:10px;text-align:right;'>
                      <span id="barcode"></span>
                              </p>
                      </div>
                </div>
              <div class='row' style="font-weight:bold;font-size:20px;margin-top:-10px" >
             <div class='col-5'>
                      <p style='text-align:left;'>TCC Job Sheet</p>
                  </div>
                  <div class='col-1'>
                      <p style='padding-left:15px;text-align:left'></p>
                  </div>
            <div class='col-2'>
                      <p style='text-align:right'>Job No</p>
                  </div>
                  <div class='col-1'>
                      <p style='text-align:left'>:&nbsp;<span id="ticketno"></span></p>
                  </div></div>
                <div class="row" style="margin-top:-50px">
                   <p style='padding-left:16px;'><br />
            <strong>_____________________________________________________________________________________________________________________________________________________</strong></p>
             
             </div>
        <div class='row' style="font-weight:bold;font-size:15px;" >
             <div class='col-2'>
                      <p style='text-align:left'>Job Log Date</p>
                  </div>
                  <div class='col-5'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="ticketdate"></span></p>
                  </div>
            <div class='col-1'>
                      <p style='padding-left:10px;text-align:left'>LogTime</p>
                  </div>
                  <div class='col-1'>
                      <p style='text-align:left'>:&nbsp;<span id="tickettime" ></span></p>
                  </div>
            </div>
              <div class="row" style="margin-top:-25px">
             <p style='padding-left:16px;'>
            <strong>_____________________________________________________________________________________________________________________________________________________</strong></p>
             
      </div>
             <div class='row' style="font-size:15px;">
                   <%-- <p style='margin:0;padding-left:10px;text-align:left'><b><u>Job Details:</u></b></p>--%>
                 
               <div class='col-12' style="font-weight:bold;">
                 <div class='row'>
               
                       <div class='col-1'>
                      <p style='text-align:left;'>Customer</p>
                  </div>
                  <div class='col-6'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="customer"></span></p>
                     
                  </div>
                     <div class='col-1'>
                      <p style='padding-left:10px;text-align:left'>TelNo</p>
                  </div>
                  <div class='col-1'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="phoneno"></span></p>
                  </div>
                         
                     </div>
                 </div>
                    <div class='col-12' style="font-weight:bold">
                    <div class='row'>
                         <div class='col-1'>
                        <p style='text-align:left'>Address  </p>
                  </div>
                  <div class='col-6'>
                      <p style='padding-left:15px;text-align:left;text-wrap:inherit;'>:&nbsp;<span id="address" ></span></p>
                     
                  </div>
                         <div class='col-1'>
                      <p style='padding-left:10px;text-align:left'>Contacts</p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="contactperson"></span></p>
                  </div>   
                         </div>
                 </div>
                     <div class='col-12' style="font-weight:bold">
                    <div class='row'>
                           <div class='col-1'>
                      <p style='text-align:left'>Dept</p>
                  </div>
                  <div class='col-6'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="Span2"></span></p>
                  </div> 
                         <div class='col-1'>
                      <p style='padding-left:10px;text-align:left'>User</p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="Span3"></span></p>
                  </div> 
                        </div>
                         </div>
                 <p style='padding-left:16px;margin-top:-25px'>
            <strong> _____________________________________________________________________________________________________________________________________________________________</strong></p>

               </div>

             <div class='row' style="font-size:15px;">
                  <div class='col-12' style="font-weight:bold;">
                    <div class='row'>
                         <div class='col-1'>
                      <p style='text-align:left;'>Product</p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="product"></span></p>
                  </div>
                        <div class='col-1'>
                      <p style='padding-left:-5px;text-align:left'>Make </p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="brand"></span></p>
                  </div> 
                         <div class='col-1'>
                      <p style='padding-left:-5px;text-align:left'>ModelsNo</p>
                  </div>
                  <div class='col-3'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="model"></span></p>
                  </div> 
                       
                         </div>
                      </div>
                
           <div class='col-12' style="font-weight:bold">
                    <div class='row'>
                         <div class='col-1'>
                      <p style='text-align:left'>SerialNo</p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="serialno"></span></p>
                  
                  </div> 
                         <div class='col-1'>
                      <p style='padding-left:-5px;text-align:right;'>ServiceType </p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left;text-wrap:inherit;'>:&nbsp;<span id="servicetype" ></span></p>
                     
                  </div>
                         <div class='col-1'>
                      <p style='padding-left:-5px;text-align:left'>Problem</p>
                  </div>
                  <div class='col-4'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="problem"></span></p>
                  </div>   
                         </div>
                 </div>
                       
           <div class="col-12" style="font-weight:bold">
                    <div class='row'>
                        <div class='col-1'>
                      <p style='text-align:left'>Engineer</p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id=""></span></p>
                  </div> 
                      <div class='col-1'>
                      <p style='padding-left:-5px; text-align:left'>ResponseDate</p>
                  </div>
                  <div class='col-2'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="Span4"></span></p>
                  </div> 
                     <div class='col-1'>
                      <p style='padding-left:-5px; text-align:left'>ResponseTime</p>
                  </div>
                  <div class='col-3'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="Span5"></span></p>
                  </div> 
                     
                          <%-- <div class='col-1'>
                      <p style='padding-left:10px;text-align:left'>Remarks</p>
                  </div>
                  <div class='col-5'>
                      <p style='padding-left:15px;text-align:left'>:&nbsp;<span id="remarks"></span></p>
                  
                  </div>--%>
                        </div>
                      </div>


                
              <%--  <div class='row' >
                    <p style='margin:0;padding-left:10px;text-align:left'><b><u>Status Details:</u></b></p>
                    <table style='width: 100%;border: 1px solid black;font-size:13px' class="auto-index2">
                        <thead>
                            <tr>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>SNO</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>Status</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>StartTime</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>EndTime</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>Total Hrs</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>Remarks</b></td>
                            </tr>
                        </thead>
                        <tbody id="tblstatus">
                            
                        </tbody>
                        </table>
                    </div>

                  <div class='row' >
                    <p style='margin:0;padding-left:10px;text-align:left'><b><u>Part Details:</u></b></p>
                    <table style='width: 100%;border: 1px solid black;font-size:13px' class="auto-index">
                        <thead>
                            <tr>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>SNO</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>Old Ref1</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>Old Ref2</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>New Ref1</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>New Ref2</b></td>
                                 <td style='text-align:center;border: 1px solid black;padding: 4px;'><b>Remarks</b></td>
                            </tr>
                        </thead>
                        <tbody id="tblpart">
                            
                        </tbody>
                        </table>
                    </div>--%>
              <%--  <div class='row' >--%>
                      <%--  <br />
                    <p style='margin:0;padding-left:10px;text-align:left;height:50px;font-size:15px'><b><u>Office Use Only:</u></b></p>
                     <div class='col-12'>
                         <br />
                            <br />
                         <br />
                         <br />--%>
                         <div class='col-12' style="font-weight:bold">
                    <div class='row'>
                         <div class='col-3'>
                      <p style='text-align:left;padding-left:1px;'>Engineers Comments:</p>
                  </div>

                  <div class='col-0'>
                      <p style='text-align:left'></p>
                  </div>
                        <div class='col-2'>
                      <p style='text-align:left'>&#x2610;&nbsp;HW</p>
                  </div>
                  <div class='col-1'>
                      <p style='text-align:left'></p>
                  </div> 
                         <div class='col-2'>
                      <p style='text-align:left'>&#x2610;&nbsp;SW</p>
                  </div>
                  <div class='col-1'>
                      <p style='text-align:left'></p>
                  </div> 
                         <div class='col-1'>
                      <p style='text-align:left'>&#x2610;&nbsp;Other</p>
                  </div>
                        
                        </div>
                        
                   
                                   <%--<p style='margin:0;text-align:left;font-size:15px';><b>Engineers Comments:   
                                        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&#x2610;&nbsp;HW
                                         &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&#x2610;&nbsp;SW  
                                         &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&#x2610;&nbsp;Other </b>   </p>      --%>
                                                                                                 
                                  <table style='width: 100%;font-size:15px;font-weight:bold;line-height:38px;margin-top:-15px' >
                               <thead>
                                  
                                       <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                  <%-- <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>--%>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                   
                                        </thead>
                                        </table>
                                 </div>
                 <p style='padding-left:16px;line-height:48px;'>
            <strong> B/C & FRU___________________________________________________________________________________________________________________________________________________</strong></p>

                      </div>
                        
                                 <br />
                          <div class='row-2'>
                                   <div class='col-9'>
                                      <p style='padding-left:10px;text-align:right;font-size:15px'><b>Engineers Signature:</b></p>
                                  </div>
                                  <div class='col-3'>
                                      <p style='text-align:left'><b> _______________________________________</b></p>
                                  </div>
                                 </div>
          
                   <div class='col-12'>
                             <div class='row'>
                                        <br /> <p style='margin:0;text-align:left;font-size:15px'><b>Supervisor Comments:</b></p><br />
                          <div class='col-3'>
               <p style='text-align:left'><b> ________________________________________________________________________________________________________________________________________________________</b></p>
                                  </div>
                                 </div>

             </div>
                     <div class='col-12'>
                             <div class='row'>
              <br /> <p style='margin:0;text-align:left;font-size:15px'><b>Customers Comments:
                              &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&#x2610;&nbsp;Satisfied   &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&#x2610;&nbsp;Not Satisfied </b></p><br />
                                 <table style='width: 100%;font-size:15px;line-height:40px;'>
                               <thead>
                                       <tr style="border-bottom: 1px solid black;">
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            </tr>
                                           
                                        </thead>
                                        </table>
                                 </div>
                           </div>
                    
                       <div class='col-12'>
                             <div class='row-2'>
                              <br />
                                  
                                   <table style='width: 100%;font-size:15px;font-weight:bold' >
                                      
                                    <thead>
                                        <tr style="bottom: 2px">
                                     
                                            <td>&nbsp;Completed Date______________________ </td>
                                            <td>&nbsp;Time :________________</td>
                                             <td style="text-align:left">&nbsp;Customer Name / Signature:&nbsp;  ________________________________________</td>
                                          
                                            </tr>
                                   
                                        </thead>
                                        </table>
                                 
                               
                                    
                                 </div>
                           </div>
           </section>
                       </div>
           
   
</body>
    </html>