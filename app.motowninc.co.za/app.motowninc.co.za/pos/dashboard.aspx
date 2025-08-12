<%@ Page Title="" Language="C#" MasterPageFile="~/pos/pos.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="pos_dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ACK" %>
<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col text-start">
                                <b>Customer Information</b>
                            </div>
                            <div class="col text-end">
                                <asp:LinkButton runat="server" ID="CustomerSearch" OnClick="CustomerSearch_Click"><i class="bx bx-search-alt"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label>Full Name</label>
                                <asp:TextBox runat="server" ID="FullName" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Contact Number</label>
                                <asp:TextBox runat="server" ID="ContactNumber" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Email Address</label>
                                <asp:TextBox runat="server" ID="EmailAddress" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Payment Type</label>
                                <asp:DropDownList runat="server" ID="PaymentType" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Make</label>
                                <asp:TextBox runat="server" ID="Make" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Model</label>
                                <asp:TextBox runat="server" ID="Model" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Mileage</label>
                                <asp:TextBox runat="server" ID="Mileage" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>License Plate</label>
                                <asp:TextBox runat="server" ID="LicensePlate" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <label>Note</label>
                                <asp:TextBox runat="server" ID="Notes" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-4">
            </div>
            <div class="col-4">
                <asp:Repeater runat="server" ID="Products" OnItemCommand="Products_ItemCommand">
                    <ItemTemplate>
                        <h6><%#Eval("Name")%></h6>
                        <sup><%#Eval("Type")%>-<%#Eval("Code")%> : <%#Eval("Price")%></sup>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-4">
                <asp:Repeater runat="server" ID="Cart" OnItemCommand="Cart_ItemCommand">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

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
        });
    </script>
</asp:Content>

