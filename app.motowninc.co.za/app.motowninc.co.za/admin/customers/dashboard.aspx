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
                                                        <th>Employee</th>
                                                        <th>Company Name</th>
                                                        <th>Full Name</th>
                                                        <th>Email Address</th>
                                                        <th>Mobile Number</th>
                                                        <th>Landline Number</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("Employee")%></td>
                                                <td><%#Eval("CompanyName")%></td>
                                                <td>
                                                    <%#Eval("FullName")%></td>
                                                <td>
                                                    <%#Eval("EmailAddress")%></td>

                                                <td>
                                                    <%#Eval("MobileNumber")%></td>
                                                <td>
                                                    <%#Eval("LandlineNumber")%></td>
                                                <td style="text-align: end;">
                                                    <div class="filter">
                                                        <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bx bx-dots-vertical"></i></a>
                                                        <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                                                            <li>
                                                                <asp:LinkButton runat="server" ID="Edit" CommandName="Edit" CommandArgument='<%#Eval("CustomerId")%>' class="dropdown-item">Edit</asp:LinkButton></li>
                                                            <li class="dropdown-divider"></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" ID="Notes" CommandName="Notes" CommandArgument='<%#Eval("CustomerId")%>' class="dropdown-item">Notes</asp:LinkButton></li>
                                                            <li class="dropdown-divider"></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" ID="Delete" CommandName="Delete" CommandArgument='<%#Eval("CustomerId")%>' OnClientClick="return confirm('Are you sure you want to delete, this information will be permanently lost?')" class="dropdown-item red-text">Delete</asp:LinkButton></li>
                                                        </ul>
                                                    </div>
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

