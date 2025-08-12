<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="create.aspx.cs" Inherits="admin_employees_create" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" Runat="Server">
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
                                <label class="col-md-4 col-lg-3 col-form-label">Account Type</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:DropDownList runat="server" ID="AccountType" class="form-control">
                                        <asp:ListItem Value="0">Please Select Account Type</asp:ListItem>
                                        <asp:ListItem Value="Employee">Employee</asp:ListItem>
                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                        <div class="row">
                            <div class="col-12 text-end">
                                <asp:Button runat="server" ID="Cancel" Text="Cancel" OnClick="Cancel_Click" CssClass="btn btn-secondary btn-sm" />
                                <asp:Button runat="server" ID="Create" Text="Create & Continue" OnClick="Create_Click" CssClass="btn btn-primary btn-sm" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';"/>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>

