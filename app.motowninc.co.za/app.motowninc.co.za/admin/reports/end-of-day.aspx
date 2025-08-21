<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="end-of-day.aspx.cs" Inherits="admin_reports_end_of_day" %>

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
                                    End Of Day Report
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <label class="input-group-text">Select Date</label>
                                        <asp:TextBox runat="server" ID="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <cc1:CalendarExtender runat="server" TargetControlID="Date" PopupButtonID="Date" Format="dd/MM/yyyy" />
                                </div>
                                <div class="col-md-2">
                                    <label class="form-label">&nbsp;</label>
                                    <asp:Button runat="server" ID="Search" Text="Search" OnClick="Search_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Report1">
                                        <HeaderTemplate>
                                            <table id="datatable" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Employee</th>
                                                        <th>Invoice</th>
                                                        <th>Customer</th>
                                                        <th>Contact Number</th>
                                                        <th>Products</th>
                                                        <th>Total</th>
                                                        <th>Payment Type</th>
                                                        <th>Date Created</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("Employee")%>
                                                </td>
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
                                                    <%#GetInvoiceProducts(Eval("InvoiceId").ToString())%>
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
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                         <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    Summary
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Repeater runat="server" ID="Report2">
                                        <HeaderTemplate>
                                            <table class="summary-table-style">
                                                <thead>
                                                    <tr>
                                                        <th>Employee Sales</th>
                                                        <th>Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("Employee")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Total")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                    </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Repeater runat="server" ID="Report3">
                                        <HeaderTemplate>
                                            <table class="summary-table-style">
                                                <thead>
                                                    <tr>
                                                        <th>Payment Type</th>
                                                        <th>Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("PaymentType")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Total")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                    </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Repeater runat="server" ID="Report4">
                                        <HeaderTemplate>
                                            <table class="summary-table-style">
                                                <thead>
                                                    <tr>
                                                        <th>Product Type</th>
                                                        <th>Quantity</th>
                                                        <th>Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("Type")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Quantity")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Total")%>
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

