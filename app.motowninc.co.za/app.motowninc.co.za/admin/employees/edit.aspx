<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="admin_employees_edit" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header"><a href="/admin/employees/dashboard" class="black-links"><span class="bx bx-arrow-back"></span></a>&nbsp;Employee Information</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Full Name</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="FullName" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Nickname</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="NickName" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Email Address</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="EmailAddress" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Password</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="Password" TextMode="Password" class="form-control"></asp:TextBox>
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
                                <div class="col-md-6">
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Contact Number</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="ContactNumber" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Address</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="Address" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">ID Number</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="IDNumber" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Platform Access</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:CheckBox runat="server" ID="PlatformAccess" CssClass="CheckBox-Margin-Top-15" />
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Active</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:CheckBox runat="server" ID="Active" CssClass="CheckBox-Margin-Top-15" />
                                        </div>
                                    </div>
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
                        <div class="card-header">Employment Information</div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Date Engaged</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="DateEngaged" class="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Employee Number</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="EmployeeNumber" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Department</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Department" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Position</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Position" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Line Manager</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="LineManager" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Pay Day</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="PayDay" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Rate</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Rate" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">Hours</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="Hours" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <label class="col-md-4 col-lg-3 col-form-label">LOA Rate</label>
                                <div class="col-md-8 col-lg-9">
                                    <asp:TextBox runat="server" ID="LOARate" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">Emergency Contact</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Full Name</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="EmergencyContactFullName" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Relationship</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="EmergencyContactRelationship" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Contact Number</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="EmergencyContactContactNumber" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="card">
                        <div class="card-header">Bank Account</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Bank Name</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="BankAccountBankName" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Branch Code</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="BankAccountBranchCode" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Account Name</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="BankAccountAccountName" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Account Number</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="BankAccountAccountNumber" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-md-4 col-lg-3 col-form-label">Account Type</label>
                                        <div class="col-md-8 col-lg-9">
                                            <asp:TextBox runat="server" ID="BankAccountAccountType" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col text-end">
                    <asp:Button runat="server" ID="Cancel" Text="Cancel" OnClick="Cancel_Click" CssClass="btn btn-secondary btn-sm" />
                    <asp:Button runat="server" ID="Update" Text="Update" OnClick="Update_Click" CssClass="btn btn-primary btn-sm" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';" />
                </div>
            </div>
        </section>
    </main>
</asp:Content>

