<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_products_dashboard" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    Products Dashboard
                                </div>
                                <div class="col text-end">
                                    <asp:Button runat="server" ID="Create" Text="Create Product" OnClick="Create_Click" CssClass="btn btn-primary btn-sm" />
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Products" OnItemCommand="Products_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="datatable" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Code</th>
                                                        <th>Tye</th>
                                                        <th>Name</th>
                                                        <th>Description</th>
                                                        <th>Quantity</th>
                                                        <th>Price</th>
                                                        <th>Discount</th>
                                                        <th>Barcode</th>
                                                        <th>Edit</th>
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
                                                    <asp:LinkButton runat="server" ID="Edit" CommandName="Edit" CommandArgument='<%#Eval("ProductId")%>' class="dropdown-item"><i class="bx bx-edit"></i></asp:LinkButton></li>
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
    <script>
        $(document).ready(function () {
            new DataTable('#datatable', {
                stateSave: true,
                order: false
            });
        });
    </script>
</asp:Content>

