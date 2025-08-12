<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="admin_products_edit" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" Runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <h6 class="card-header"><a href="/admin/products/dashboard" class="black-links"><span class="bx bx-arrow-back"></span></a>&nbsp;Edit Product</h6>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Type</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Type" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Code</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Code" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Name</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Name" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                           <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Description</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Description" class="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Quantity</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Quantity" class="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Price</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Price" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Discount</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Discount" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Barcode</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Barcode" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="row">
                                <div class="col text-end">
                                    <asp:Button runat="server" ID="Update" Text="Update" OnClick="Update_Click" CssClass="btn btn-primary btn-sm" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>

