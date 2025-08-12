<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="admin_employees_edit" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-header"><a href="/admin/employees/dashboard" class="black-links"><span class="bx bx-arrow-back"></span></a>&nbsp;Employee Information</div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Full Name</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="FullName" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Contact Number</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="ContactNumber" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">EmailAddress</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="EmailAddress" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Password</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Password" class="form-control" TextMode="Password" autocomplete="new-password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Account Type</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:DropDownList runat="server" ID="AccountType" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Platform Access</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:CheckBox runat="server" ID="PlatformAccess" />
                                </div>
                            </div>
                             <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Active</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:CheckBox runat="server" ID="Active" />
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                        <div class="row">
                            <div class="col-12 text-end">
                                <asp:Button runat="server" ID="Update" Text="Update" OnClick="Update_Click" CssClass="btn btn-primary btn-sm" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';"/>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <asp:HiddenField runat="server" ID="CurrentEmailAddress" />
</asp:Content>

