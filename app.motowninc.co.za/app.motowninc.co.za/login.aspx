<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ACK" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motown Inc - Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="/lib/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/lib/bootstrap/js/bootstrap.bundle.min.js"></script>
    <link href="/lib/boxicons/css/boxicons.min.css" rel="stylesheet" />
    <link href="/css/login.css" rel="stylesheet" />
</head>
<body>
    <main>
        <form id="Form" runat="server">
            <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
            <div class="container">
                <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center">
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">
                                <div class="card p-2">
                                    <div class="card-body">
                                        <img src="/img/logo.png" class="img-fluid" />
                                        <br />
                                        <br />
                                        <p class="text-center small">Enter your email address & password to login</p>
                                        <div class="row">
                                            <div class="col-12">
                                                <label class="form-label">Email Address</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ID="EmailAddress" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-12">
                                                <label class="form-label">Password</label>
                                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="col-12">
                                            <asp:Button runat="server" ID="Login" Text="Login" OnClick="Login_Click" CssClass="btn btn-primary btn-sm" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 text-center">
                                        <br />
                                        <sup>&copy; <%=DateTime.Now.Year.ToString() %> Syndicate Piling (Pty) Ltd. All Rights Reserved. Hosted by Webox.</sup>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <asp:LinkButton ID="Dummy" runat="server"></asp:LinkButton>
            <ACK:ModalPopupExtender ID="PopUp" runat="server" PopupControlID="PopUpPanel" TargetControlID="Dummy" BackgroundCssClass="background"></ACK:ModalPopupExtender>
            <asp:Panel ID="PopUpPanel" runat="server" CssClass="popup" align="center" Style="display: none">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:LinkButton ID="PopUpClose" runat="server" OnClick="PopUpClose_Click" CssClass="close"><i class="bx bx-x"></i></asp:LinkButton>
                            <div id="PopUpDiv" runat="server"></div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </form>
    </main>
</body>
</html>
