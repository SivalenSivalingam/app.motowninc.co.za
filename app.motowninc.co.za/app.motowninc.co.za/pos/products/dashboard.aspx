<%@ Page Title="" Language="C#" MasterPageFile="~/pos/pos.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="pos_products_dashboard" %>

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
                            </div>
                        </div>
                        <div class="card-body p-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater runat="server" ID="Products">
                                        <HeaderTemplate>
                                            <table id="datatable" class="table table-striped table-borderless">
                                                <thead>
                                                    <tr>
                                                        <th>Code</th>
                                                        <th>Type</th>
                                                        <th>Name</th>
                                                        <th>Description</th>
                                                        <th>Quantity</th>
                                                        <th>Price</th>
                                                        <th>Discount</th>
                                                        <th>Barcode</th>
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

