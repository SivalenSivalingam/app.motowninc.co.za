<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="admin_invoices_view" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" Runat="Server">
     <style>
        iframe {
            display: block;
            background: #000;
            border: none;
            height: 75vh;
            width: 100%;
        }
    </style>
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col text-start">
                                    <a href="/pos/invoices/dashboard" class="black-links"><span class="bx bx-arrow-back"></span></a>&nbsp;Invoices Dashboard
                                </div>
                                <div class="col text-end">
                                    <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" ForeColor="Black" OnClientClick="return confirm('Are you sure you want to cancel this invoice?')">Cancel</asp:LinkButton>
                                    &nbsp;|&nbsp;
                                    <asp:LinkButton runat="server" ID="CancelReturnQuantity" OnClick="CancelReturnQuantity_Click" ForeColor="Black" OnClientClick="return confirm('Are you sure you want to cancel and return quantity for this invoice?')">Cancel & Return Quantity</asp:LinkButton>
                                    &nbsp;|&nbsp;
                                    <asp:LinkButton runat="server" ID="Download" OnClick="Download_Click" ForeColor="Black">Download</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-3">
                             <iframe src="/pos/invoices/InvoiceHandler.ashx" id="PdfViewer" runat="server"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>

