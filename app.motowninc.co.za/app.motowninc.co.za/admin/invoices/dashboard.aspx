<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_invoices_dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
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
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <label class="input-group-text">From Date</label>
                                        <asp:TextBox runat="server" ID="FromDate" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <cc1:CalendarExtender runat="server" TargetControlID="FromDate" PopupButtonID="FromDate" Format="dd/MM/yyyy" />
                                </div>
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <label class="input-group-text">To Date</label>
                                        <asp:TextBox runat="server" ID="ToDate" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <cc1:CalendarExtender runat="server" TargetControlID="ToDate" PopupButtonID="ToDate" Format="dd/MM/yyyy" />
                                </div>
                                <div class="col-md-2">
                                    <label class="form-label">&nbsp;</label>
                                    <asp:Button runat="server" ID="Search" Text="Search" OnClick="Search_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>
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
                                                        <th>Payment Type</th>
                                                        <th>Date Created</th>
                                                        <td></td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#State(Eval("InvoiceId").ToString().Substring(0,8).ToUpper(),Eval("Cancelled").ToString(), Eval("ReturnedQuantity").ToString())%>
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
                                                    <%#Eval("PaymentType")%>
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

