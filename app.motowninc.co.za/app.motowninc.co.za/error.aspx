<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motown Inc - Error</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="/lib/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .main {
            height: 100vh;
        }

        .sub-text {
            font-size: 15px;
        }
    </style>
</head>
<body>
    <div class="d-flex justify-content-center align-items-center main p-5">
		<h1 class="mr-3 pr-3 align-top border-right inline-block align-content-center">Sorry!&nbsp;</h1>
		<div class="inline-block align-middle">
			<h2 class="font-weight-normal lead">We are experiencing technical difficulties with our online platform right now.</h2>
			<h3 class="font-weight-normal lead sub-text">Please contact our Technical Support department at support@webox.co.za.</h3>
            <h3 class="font-weight-normal lead sub-text"><asp:Label runat="server" ID="Url"></asp:Label></h3>
            <h3 class="font-weight-normal lead sub-text"><asp:Label runat="server" ID="Message"></asp:Label></h3>
		</div>
	</div>
</body>
</html>
