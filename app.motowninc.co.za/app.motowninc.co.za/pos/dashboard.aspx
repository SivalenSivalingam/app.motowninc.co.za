<%@ Page Title="" Language="C#" MasterPageFile="~/pos/pos.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="pos_dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ACK" %>
<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    <b>Customer</b>
                                </div>
                                <div class="col text-end">
                                    <asp:LinkButton runat="server" ID="CustomerSearch" OnClick="CustomerSearch_Click" ForeColor="Black"><i class="bx bx-search-alt"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="pos-label">Full Name</label>
                                    <asp:TextBox runat="server" ID="FullName" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="pos-label">Contact Number</label>
                                    <asp:TextBox runat="server" ID="ContactNumber" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="pos-label">Email Address</label>
                                    <asp:TextBox runat="server" ID="EmailAddress" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="pos-label">Payment Type</label>
                                    <asp:DropDownList runat="server" ID="PaymentType" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <label class="pos-label">Make</label>
                                    <asp:TextBox runat="server" ID="Make" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="pos-label">Model</label>
                                    <asp:TextBox runat="server" ID="Model" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <label class="pos-label">Mileage</label>
                                    <asp:TextBox runat="server" ID="Mileage" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="pos-label">License Plate</label>
                                    <asp:TextBox runat="server" ID="LicensePlate" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <label class="pos-label">Note</label>
                                    <asp:TextBox runat="server" ID="Notes" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    <b>Checkout</b>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <h5>Subtotal</h5>
                                </div>
                                <div class="col text-end">
                                    <h5>R<asp:Label runat="server" ID="Subtotal"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <h5>Discount</h5>
                                </div>
                                <div class="col text-end">
                                    <h5>R<asp:Label runat="server" ID="Discount"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <h5>VAT</h5>
                                </div>
                                <div class="col text-end">
                                    <h5>R<asp:Label runat="server" ID="VAT"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <h5>Total</h5>
                                </div>
                                <div class="col text-end">
                                    <h5>R<asp:Label runat="server" ID="Total" CssClass="totalAmount"></asp:Label></h5>
                                </div>
                            </div>
                             <div class="row CashReceived">
                                <div class="col">
                                    <h5>Cash Received</h5>
                                </div>
                                <div class="col text-end">
                                    <asp:TextBox runat="server" ID="CashReceived" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <h5>Cash Returned</h5>
                                </div>
                                <div class="col text-end">
                                    <h5>
                                        R<asp:Label runat="server" ID="Change" CssClass="CashReturned"></asp:Label></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    Products
                                </div>
                                <div class="col text-end">
                                    <asp:TextBox runat="server" ID="Product" Placeholder="Scan Barcode" CssClass="form-control" AutoPostBack="true" OnTextChanged="Product_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Products" OnItemCommand="Products_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="datatableProducts" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Code</th>
                                                        <th>Type</th>
                                                        <th>Name</th>
                                                        <th>Description</th>
                                                        <th>Quantity</th>
                                                        <th>Price</th>
                                                        <th>Discount</th>
                                                        <th>Barcode</th>
                                                        <th>Select</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("Code")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Type")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Name")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Description")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Quantity")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Price")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Discount")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Barcode")%>
                                                </td>
                                                <td style="text-align: end !important">
                                                    <asp:LinkButton runat="server" ID="Select" CommandName="Select" CommandArgument='<%#Eval("ProductId")%>' class="dropdown-item"><i class="bx bx-right-arrow-alt"></i></asp:LinkButton></li>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                    </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    Selected Products
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Cart" OnItemCommand="Cart_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="datatableCart" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Code</th>
                                                        <th>Type</th>
                                                        <th>Name</th>
                                                        <th>Quantity</th>
                                                        <th>Price</th>
                                                        <th>Discount</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("Code")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Type")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Name")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Description")%>
                                                </td>
                                                <td>
                                                   <asp:TextBox runat="server" ID="Quantity" Text='<%#Eval("Quantity")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <%#Eval("Price")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Discount")%>
                                                </td>
                                                <td style="text-align: end !important">
                                                    <asp:LinkButton runat="server" ID="Update" CommandName="Update" CommandArgument='<%#Eval("ProductId")%>' class="dropdown-item"><i class="bx bx-right-arrow-alt"></i></asp:LinkButton>
                                                    </li>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                    </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <asp:LinkButton ID="Dummy" runat="server"></asp:LinkButton>
    <ACK:ModalPopupExtender ID="CustomerSearchModal" runat="server" PopupControlID="CustomerSearchPanel" TargetControlID="Dummy" BackgroundCssClass="background"></ACK:ModalPopupExtender>
    <asp:Panel ID="CustomerSearchPanel" runat="server" CssClass="popup-search" align="center" Style="display: none">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <asp:LinkButton ID="CustomerSearchPanelPopUpClose" runat="server" OnClick="CustomerSearchPanelPopUpClose_Click" CssClass="close"><i class="bx bx-x"></i></asp:LinkButton>
                    <div class="card">
                        <div class="card-header">Select Customer</div>
                        <div class="card-body">
                            <asp:Repeater runat="server" ID="Customers" OnItemCommand="Customers_ItemCommand">
                                <HeaderTemplate>
                                    <table id="datatableCustomers" class="table table-striped table-borderless">
                                        <thead>
                                            <tr>
                                                <th>Name</th>
                                                <th>Contact Number</th>
                                                <th>Email Address</th>
                                                <th>Account Type</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name")%></td>
                                        <td><%#Eval("ContactNumber")%></td>
                                        <td>
                                            <%#Eval("EmailAddress")%></td>
                                        <td>
                                            <%#Eval("AccountType")%></td>

                                        <td style="text-align: end !important">
                                            <asp:LinkButton runat="server" ID="Select" CommandName="Select" CommandArgument='<%#Eval("CustomerId")%>' class="dropdown-item"><i class="bx bx-plus"></i></asp:LinkButton></li>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script>
        $(document).ready(function () {
            new DataTable('#datatableCustomers', {
                stateSave: true,
                order: false
            });
            new DataTable('#datatableProducts', {
                stateSave: true,
                order: false
            });
            new DataTable('#datatableCart', {
                stateSave: true,
                order: false
            });
        });
    </script>
</asp:Content>

