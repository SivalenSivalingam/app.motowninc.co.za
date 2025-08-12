<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_employees_dashboard" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    Employees Dashboard
                                </div>
                                <div class="col text-end">
                                    <asp:Button runat="server" ID="CreateEmployee" Text="Create Employee" OnClick="CreateEmployee_Click" CssClass="btn btn-primary btn-sm" />
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Employees" OnItemCommand="Employees_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="datatable" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Full Name</th>
                                                        <th>Email Address</th>
                                                        <th>Contact Number</th>
                                                        <th>Active</th>
                                                        <th>Account Type</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("FullName")%>
                                                </td>
                                                <td>
                                                    <%#Eval("EmailAddress")%>
                                                </td>
                                                <td>
                                                    <%#Eval("ContactNumber")%>
                                                </td>
                                                <td>
                                                    <%#Active(Eval("Active").ToString())%>
                                                </td>
                                                  <td>
                                                    <%#Eval("AccountType")%>
                                                </td>
                                                <td style="text-align: end !important">
                                                    <asp:LinkButton runat="server" ID="Edit" CommandName="Edit" CommandArgument='<%#Eval("EmployeeId")%>' class="dropdown-item"><i class="bx bx-edit"></i></asp:LinkButton></li>
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

