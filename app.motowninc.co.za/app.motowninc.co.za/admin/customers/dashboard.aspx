<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_customers_dashboard" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    Customers Dashboard
                                </div>
                                <div class="col text-end">
                                    <asp:Button runat="server" ID="CreateCustomer" Text="Create Customer" OnClick="CreateCustomer_Click" CssClass="btn btn-primary btn-sm" />
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Customers" OnItemCommand="Customers_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="datatable" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Contact Number</th>
                                                        <th>Email Address</th>
                                                        <th>Account Type</th>
                                                        <th>Edit</th>
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
                                                    <asp:LinkButton runat="server" ID="Edit" CommandName="Edit" CommandArgument='<%#Eval("CustomerId")%>' class="dropdown-item"><i class="bx bx-edit"></i></asp:LinkButton></li>
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

