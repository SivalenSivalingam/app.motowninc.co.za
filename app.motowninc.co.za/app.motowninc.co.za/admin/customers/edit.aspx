<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="admin_customers_edit" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <h6 class="card-header"><a href="/admin/customers/dashboard" class="black-links"><span class="bx bx-arrow-back"></span></a>&nbsp;Customer Information</h6>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Company Name</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="CompanyName" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Full Name</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="FullName" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Email Address</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="EmailAddress" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Mobile Number</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="MobileNumber" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Landline Number</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="LandlineNumber" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <h6 class="card-header">Secondary Contact Information</h6>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Full Name</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="SecondaryFullName" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Email Address</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="SecondaryEmailAddress" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Mobile Number</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="SecondaryMobileNumber" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Landline Number</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="SecondaryLandlineNumber" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">&nbsp;</label>
                                <div class="col-md-8 col-lg-9">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <h6 class="card-header">Physical Address</h6>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Address</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="PhysicalAddress" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">City</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="PhysicalAddressCity" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Province</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="PhysicalAddressProvince" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Country</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="PhysicalAddressCountry" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">PostalCode</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="PhysicalAddressPostalCode" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <h6 class="card-header">Shipping Address</h6>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Address</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="ShippingAddress" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">City</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="ShippingAddressCity" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Province</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="ShippingAddressProvince" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Country</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="ShippingAddressCountry" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">PostalCode</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="ShippingAddressPostalCode" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <h6 class="card-header">Assigned Employee</h6>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Employees</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:DropDownList runat="server" ID="Employees" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col text-end">
                    <asp:Button runat="server" ID="Update" Text="Update" OnClick="Update_Click" CssClass="btn btn-primary btn-sm" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';"/>
                </div>
            </div>
        </section>
    </main>
</asp:Content>

