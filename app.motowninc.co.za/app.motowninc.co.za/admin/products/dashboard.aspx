<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_products_dashboard" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" Runat="Server">
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
                                                        <th>Employee Number</th>
                                                        <th>Full Name</th>
                                                        <th>Contact Number</th>
                                                        <th>Department</th>
                                                        <th>Position</th>
                                                        <th>Active</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("EmployeeNumber")%>
                                                </td>
                                                <td>
                                                    <%#GetName(Eval("FullName").ToString(), Eval("NickName").ToString())%>
                                                </td>
                                                <td>
                                                    <%#Eval("ContactNumber")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Department")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Position")%>
                                                </td>
                                                <td>
                                                    <%#Active(Eval("Active").ToString())%>
                                                </td>
                                                <td style="text-align: end">
                                                    <asp:LinkButton runat="server" ID="Edit" CommandName="Edit" CommandArgument='<%#Eval("EmployeeId")%>' class="dropdown-item">Edit</asp:LinkButton></li>
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

