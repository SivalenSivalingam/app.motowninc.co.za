<%@ Page Title="" Language="C#" MasterPageFile="~/pos/pos.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="pos_invoices_dashboard" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" Runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    Invoices Dashboard
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Invoices" OnItemCommand="Invoices_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="datatable" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Invoice</th>
                                                        <th>Customer</th>
                                                        <th>Contact Number</th>
                                                        <th>Total</th>
                                                        <th>Date Created</th>
                                                        <td></td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("InvoiceId").ToString().Substring(0,8).ToUpper()%>
                                                </td>
                                                <td>
                                                    <%#Eval("FullName")%>
                                                </td>
                                                 <td>
                                                    <%#Eval("ContactNumber")%>
                                                </td>
                                                 <td>
                                                    <%#Eval("Total")%>
                                                </td>
                                                 <td>
                                                    <%#Eval("DateCreated")%>
                                                </td>
                                                 <td style="text-align: end !important">
                                                    <asp:LinkButton runat="server" ID="Edit" CommandName="Edit" CommandArgument='<%#Eval("InvoiceId")%>' class="dropdown-item"><i class="bx bx-edit"></i></asp:LinkButton></li>
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

