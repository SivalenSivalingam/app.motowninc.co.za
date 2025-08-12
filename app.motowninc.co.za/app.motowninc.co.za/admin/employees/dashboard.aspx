<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_employees_dashboard" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" Runat="Server">
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
                                                    <div class="filter">
                                                        <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bx bx-dots-vertical"></i></a>
                                                        <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                                                            <li>
                                                                <asp:LinkButton runat="server" ID="Edit" CommandName="Edit" CommandArgument='<%#Eval("EmployeeId")%>' class="dropdown-item">Edit</asp:LinkButton></li>
                                                            <li class="dropdown-divider"></li>
                                                            <li>
                                                                <asp:LinkButton runat="server" ID="Delete" CommandName="Delete" CommandArgument='<%#Eval("EmployeeId")%>' OnClientClick="return confirm('Are you sure you want to delete, this information will be permanently lost?')" class="dropdown-item red-text">Delete</asp:LinkButton></li>
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

