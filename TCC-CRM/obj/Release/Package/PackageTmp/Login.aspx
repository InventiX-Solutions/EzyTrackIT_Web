﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TCC_CRM.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta charset="utf-8">
     <link rel="icon" type="image/png" sizes="16x16" href="../images/favicon-25.png" />
  <title>EzyTrackIT-Service Desk | Login</title>
   <style>

html,body,div,span,applet,object,iframe,h1,h2,h3,h4,h5,h6,p,blockquote,pre,a,abbr,acronym,address,big,cite,code,del,dfn,em,img,ins,kbd,q,s,samp,small,strike,strong,sub,sup,tt,var,b,u,i,dl,dt,dd,ol,nav ul,nav li,fieldset,form,label,legend,table,caption,tbody,tfoot,thead,tr,th,td,article,aside,canvas,details,embed,figure,figcaption,footer,header,hgroup,menu,nav,output,ruby,section,summary,time,mark,audio,video{margin:0;padding:0;border:0;font-size:100%;font:inherit;vertical-align:baseline;}
article, aside, details, figcaption, figure,footer, header, hgroup, menu, nav, section {display: block;}
ol,ul{list-style:none;margin:0px;padding:0px;}
blockquote,q{quotes:none;}
blockquote:before,blockquote:after,q:before,q:after{content:'';content:none;}
table{border-collapse:collapse;border-spacing:0;}
a{text-decoration:none;}
.txt-rt{text-align:right;}/* text align right */
.txt-lt{text-align:left;}/* text align left */
.txt-center{text-align:center;}/* text align center */
.float-rt{float:right;}/* float right */
.float-lt{float:left;}/* float left */ 
.clear{clear:both;}/* clear float */
.pos-relative{position:relative;}/* Position Relative */
.pos-absolute{position:absolute;}/* Position Absolute */
.vertical-base{	vertical-align:baseline;}/* vertical align baseline */
.vertical-top{	vertical-align:top;}/* vertical align top */
nav.vertical ul li{	display:block;}/* vertical menu */
nav.horizontal ul li{	display: inline-block;}/* horizontal menu */
img{max-width:100%;}
body {
	padding: 0;
	margin: 0;
	background: #fff;
    min-height: 100vh;
    background: linear-gradient(to left, #f5f5f5 50%, #fff 50%);
	font-family: 'Raleway', sans-serif;
}
body a {
	transition: 0.5s all;
	-webkit-transition: 0.5s all;
	-moz-transition: 0.5s all;
	-o-transition: 0.5s all;
	-ms-transition: 0.5s all;
	text-decoration: none;
	letter-spacing:1px;
	font-size:15px;
	font-weight:600;
}
body a:hover {
	text-decoration: none;
}
body a:focus, a:hover {
	text-decoration: none;
}
/*input[type="button"], input[type="submit"] {
	transition: 0.5s all;
	-webkit-transition: 0.5s all;
	-moz-transition: 0.5s all;
	-o-transition: 0.5s all;
	-ms-transition: 0.5s all;
}*/
h1, h2, h3, h4, h5, h6 {
	margin: 0;
	padding: 0;
    font-family: 'Raleway', sans-serif;
    font-weight:600;
	letter-spacing:1px;
		
}
.clear{
	clear:both;
}
p {
	margin: 0;
	color:#666;
	letter-spacing:1px;
	line-height:1.8em;
	font-size:16px;
	font-weight:400;
}
.row{
	margin:0px;
	padding:0px;
}
ul {
	margin: 0;
	padding: 0;
}
label {
	margin: 0;
}
a:focus, a:hover {
	text-decoration: none;
	outline: none;
}
img{
	width:100%;
}
/*-- //Reset-Code --*/

.signupform {
    padding: 2em 0;
}
.left_grid_info h1 {
    font-size: 45px;
    margin: 1em 0em .5em 0em;
    color: #333;
    font-weight: 700;
}

input[type="email"],input[type="text"] {
	font-size: 15px;
	color: #333;
    text-align: left;
    letter-spacing: 1px;
    padding: 14px 10px;
	width:93%;
	display:inline-block;
    box-sizing: border-box;
   	border: none;
    outline: none;
    background: transparent;
	font-family: 'Raleway', sans-serif;
}
input[type="Password"] {
	font-size: 15px;
	color: #333;
    text-align: left;
    text-transform: capitalize;
    letter-spacing: 1px;
    padding: 14px 10px;
	width:93%;
	display:inline-block;
    box-sizing: border-box;
   	border: none;
    outline: none;
    background: transparent;
	font-family: 'Raleway', sans-serif;
}
.input-group {
    margin-top: 10px;
    margin-bottom: 20px;
	padding: 3px 10px;
	border: 1px solid #ddd;
	background: #fff;
}
.btn-block {
    background: #669ce4;
    border: none;
    color: #fff;
    font-size: 13px;
    padding: 12px 40px;
    letter-spacing: 2px;
    text-transform: uppercase;
    cursor: pointer;
}
.w3_info h2 {
  display: inline-block;
    font-size: 26px;
    margin-bottom: 10px;
    color: #333;
    letter-spacing: 2px;	
    text-transform: capitalize;
}
.w3_info h4 {
    display: inline-block;
    font-size: 15px;
	padding: 8px 0px;
    color: #444;
    text-transform: capitalize;
}
a.btn.btn-block.btn-social.btn-facebook {
    display: block;
    width: 100%;
    padding: 10px 0px;
    text-align: center;
    font-size: 16px;
    font-weight: 600;
	letter-spacing: 1px;
	font-family: 'Raleway', sans-serif;
}

.w3_info {
    flex-basis: 50%;
	-webkit-flex-basis: 50%;
    box-sizing: border-box;
	padding: 3em 4em;
    /* -webkit-box-shadow: 1px 0px 15px 0px rgba(0,0,0,0.2);
    -moz-box-shadow: 1px 0px 15px 0px rgba(0,0,0,0.2);
    box-shadow: 1px 0px 15px 0px rgba(0,0,0,0.2); */
}


.login-check {
    position: relative;
}
.w3_info .login-check label{
    text-transform: capitalize;
    font-size: 13px;
    letter-spacing: 1px;
    color: #000;
}
.checkbox i {
    position: absolute;
    top: 0px;
    left: 0%;
    text-align: center;
    display: block;
    width: 14px;
    height: 14px;
    border: 1px solid #777;
    outline: none;
    border-radius: 0px;
    -webkit-border-radius: 0px;
    -moz-border-radius: 0px;
    -o-border-radius: 0px;
    cursor: pointer;
}
.checkbox input:checked + i:after {
    opacity: 1;
}
.checkbox input + i:after {
    position: absolute;
    opacity: 0;
    transition: opacity 0.1s;
    -o-transition: opacity 0.1s;
    -ms-transition: opacity 0.1s;
    -moz-transition: opacity 0.1s;
    -webkit-transition: opacity 0.1s;
}
.checkbox input + i:after {
    content: url(../images/tick.png);
    top: 0px;
    left: 2px;
}
.checkbox {
    position: relative;
    display: inline-block;
    padding-left: 25px !important;
    text-transform: capitalize;
    letter-spacing: 1px;
    font-size: 14px;
    color: #fff;
}
input[type="checkbox" i] {
    display: none;
}
.agile_info {
	display: -webkit-box;      /* OLD - iOS 6-, Safari 3.1-6 */
	display: -moz-box;         /* OLD - Firefox 19- (buggy but mostly works) */
	display: -ms-flexbox;      /* TWEENER - IE 10 */
	display: -webkit-flex;     /* NEW - Chrome */
	display: flex;             /* NEW, Spec - Opera 12.1, Firefox 20+ */
}
.left_grid_info {
    padding-right: 3em;
}
.w3l_form {
    padding: 0px;
    flex-basis: 50%;
	-webkit-flex-basis: 50%;
}
.w3_info p {
    padding-bottom: 30px;
}
p.account,p.account1 {
    padding-top: 20px;
    padding-bottom: 0px;
    line-height: 1.5em;
    font-size: 13px;
}
p.account a,p.account1 a {
    color: #ff4040;
    font-size: 11px;
    text-decoration: underline;
}
p.account a:hover,p.account1 a:hover {
    text-decoration: none;
}
.w3_info label {
    margin: 0;
    text-transform: uppercase;
    letter-spacing: 1px;
    font-weight: 500;
    color: #000;
    margin-bottom: 20px;
    font-size: 13px;
    padding-left: 0px;
}
h3.w3ls {
    margin: 10px 0px;
    padding-left: 60px;
}
h3.agileits {
    padding-left: 10px;
}
.container {
    width: 65%;
    margin: 0 auto;
}
.input-group span.fa {
   font-size: 16px;
    vertical-align: middle;
	box-sizing:border-box;
	float:left;
    text-align: center;
	width:6%;
    padding: 15px 0px;
	color: #669ce4;
}
h5 {
    text-align: center;
    margin: 10px 0px;
    font-size: 15px;
    font-weight: 600;
	    color: #000;
}
.footer p {
    color: #666;
    text-align: center;
    letter-spacing: 1px;
    font-size: 15px;
    margin-top: 2em;
}
.footer p a {
    color: #333;
}
/** Responsive **/
@media screen and (max-width: 1440px){
	.container {
		width: 70%;
		margin: 0 auto;
	}
}
@media screen and (max-width: 1366px){
	.container {
		width: 75%;
	}
	.left_grid_info h1 {
		font-size: 35px;
	}
}
@media screen and (max-width: 1080px){
	.container {
		width: 80%;
	}
	.w3_info h2 {
		font-size: 24px;
		letter-spacing: 1px;
	}
	.left_grid_info h1 {
		font-size: 35px;
	}
	.w3_info {
		padding: 3em 3em;
	}
}
@media screen and (max-width: 1024px){
	.signupform {
		padding: 1em 0;
	}
	.container {
		width: 85%;
	}
	.left_grid_info {
		padding-right: 3em;
		padding-top: 2em;
	}
}
@media screen and (max-width: 991px){
	.w3_info h2 {
		font-size: 24px;
	}
	h1 {
		font-size: 40px;
		letter-spacing: 2px;
	}
}
@media screen and (max-width: 900px){
	
	.container {
		width: 90%;
	}
	.left_grid_info h4 {
		font-size: 1em;
	}
	.w3_info {
		padding: 3em 2.5em;
	}
}
@media screen and (max-width: 800px){
	.container {
		width: 95%;
	}
	input[type="text"],input[type="email"],input[type="password"] {
		font-size: 14px;
	}
	.input-group span.fa {
		font-size: 16px;
		padding: 13px 0px;
	}
	.w3_info h2 {
		font-size: 23px;
	}
}
@media screen and (max-width: 768px){
	.container {
		width: 95%;
	}
	.w3_info h2 {
		letter-spacing: 1px;
	}
	.left_grid_info h1 {
		font-size: 30px;
		letter-spacing: 1px;
	}
	.left_grid_info {
		padding-right: 2em;
	}
}
@media screen and (max-width: 736px){
	.left_grid_info h3 {
		font-size: 1.7em;
	}
	.w3_info h2 {
		font-size: 22px;
	}
	.w3_info {
		padding: 3em 2em;
	}
	.footer p {
		font-size: 14px;
	}
	.w3_info h4 {
		font-size: 14px;
	}
	.footer p {
		margin-top: 0em;
		margin-bottom: 2em;
	}
}

@media screen and (max-width: 684px){
	.w3_info h2 {
		font-size: 20px;
		letter-spacing: 0px;
	}
	.left_grid_info h1 {
		font-size: 26px;
		letter-spacing: 1px;
	}
}
@media screen and (max-width: 667px){
	.container {
		width: 80%;
	}
}
@media screen and (max-width: 640px){
	h1 {
		font-size: 37px;
	}
	.agile_info {
		flex-direction: column;
	}
	body {
		background: #FFF;
	}
	.left_grid_info {
		padding-right: 0em;
	}
}
@media screen and (max-width: 480px){
	.w3l_form {
		padding: 0em;
	}
	h1 {
		font-size: 34px;
	}
	.w3_info {
		padding: 3em 1em;
	}
	.footer {
		margin: 0 1em;
	}
	.left_grid_info h1 {
		margin: 0em 0em .5em 0em;
	}
}
@media screen and (max-width: 414px){
	.w3_info {
		padding: 2em;
	}
	h1 {
		font-size: 32px;
		letter-spacing: 1px;
	}
	.left_grid_info p {
		font-size: 14px;
	}
	.container {
		width: 90%;
	}
	.left_grid_info {
		padding-top: 1em;
	}
	.w3_info p {
		font-size: 14px;
	}
}
@media screen and (max-width: 384px){
	.left_grid_info h4 {
		font-size: .9em;
	}
	.w3_info {
		padding: 2em 1em;
	}
	.left_grid_info h1 {
		font-size: 21px;
		letter-spacing: 0px;
	}
	.w3_info h2 {
		font-size: 18px;
	}
}
@media screen and (max-width: 375px){
	.left_grid_info h3 {
		font-size: 1.5em;
	}
}
@media screen and (max-width: 320px){
	h1 {
		font-size: 25px;
		letter-spacing: 1px;
	}
	.w3_info h2 {
		font-size: 18px;
		letter-spacing: 0px;
	}
	.btn-danger {
		padding: 13px 12px;
		font-size: 13px;
	}
	input[type="text"], input[type="email"], input[type="password"] {
		font-size: 13px;
	}
	.footer p {
		font-size: 13px;
	}
	.footer p a{
		font-size: 13px;
	}
}
/** /Responsive **/


   </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
       <script src="../assets/plugins/jquery/jquery.min.js"></script>
        <script type="text/javascript">
            $(function () {
                window.history.pushState(null, "", window.location.href);
                window.onpopstate = function () {
                    //sessionStorage.clear();
                    alert("This function is not allowed..!!")
                    window.history.pushState(null, "", window.location.href);
                    return;
                };

                $(this).bind("contextmenu", function (e) {
                    e.preventDefault();
                    alert("This function is not allowed..!!")
                });
                //$(this).bind("mousewheel", function (ex) {
                //    if (ex.which == 2) {
                //        ex.preventDefault();
                //        alert("This function is not allowed..!!")
                //        return false;
                //    }
                //});
                $(this).mousedown(function (b) {
                    if (b.which == 2) {
                        alert("This function is not allowed..!!")
                        b.preventDefault();
                        //        alert("This function is not allowed..!!")
                    }
                });
            });
    </script>
</head>
<body>
   
    <div class="signupform">
	<div class="container">
		<!-- main content -->
		<!---728x90--->

		<div class="agile_info">
			<div class="w3l_form">
				<div class="left_grid_info">
					<h1>EzyTrackIT-Service Desk</h1>
					<p><i><b>Service Desk</b></i> is a wonderful tracking services that brings all of your incumbent deliveries into one place where you can monitor each one's status on the go.</p>
					<img src="../Images/landing.png" alt="" style="height:300px; width:300px" />
				</div>
			</div>

			<div class="w3_info">
			<!---728x90--->

				<h2>Login to your Account</h2>
				<p>Enter your details to login.</p>
				<form  runat="server" id="form1">
                    <label>Client Code</label>
                    <div class="input-group">
						<span class="fa fa-podcast" aria-hidden="true"></span>
                        	<input type="text" placeholder="Enter ClientCode" id="txtclientcode" runat="server" required="" tabindex="1" autofocus="autofocus" > 
                        </div>
					<label>Username</label>
					<div class="input-group">
						<span class="fa fa-envelope" aria-hidden="true"></span>
						<input type="text" placeholder="Enter Your Username" id="txtusername" runat="server" required="" tabindex="2"> 
					</div>
					<label>Password</label>
					<div class="input-group">
						<span class="fa fa-lock" aria-hidden="true"></span>
						<input type="Password" placeholder="Enter Password" id="txtpass" runat="server" required="" tabindex="2">
					</div> 
					<div class="login-check">
                         <label for="pass" id="txtnewerroe" class="error" style="color: red;" runat="server" visible="false">Login Validation</label>
						<%-- <label class="checkbox"><input type="checkbox" name="checkbox" checked=""><i> </i> Remember me</label>--%>
					</div>					
					<%--	<button class="btn btn-danger btn-block" type="submit">Login</button >   --%>
                        <asp:Button ID="btnSubmit" class="btn btn-danger btn-block" runat="server" OnClick="OnBtnLoginClick" Text="Login" TabIndex="3" />             
				</form>
			<%--	<p class="account">By clicking login, you agree to our <a href="#">Terms & Conditions!</a></p>
				<p class="account1">Dont have an account? <a href="#">Register here</a></p>--%>
			</div>
		</div>
		<!-- //main content -->
	</div>
	<!---728x90--->

<%--	<!-- footer -->
	<div class="footer">
		<p>&copy; 2019 Business login form. All Rights Reserved | Design by <a href="https://w3layouts.com/" target="blank">W3layouts</a></p>
	</div>
	<!-- footer -->--%>
</div>
    
</body>
</html>
