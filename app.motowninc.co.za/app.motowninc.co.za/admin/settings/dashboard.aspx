<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_settings_dashboard" ValidateRequest="false" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
      <script src="/js/jquery-3.7.0.min.js"></script>
     <link href="/lib/summernote/summernote-bs5.min.css" rel="stylesheet" />
    <script src="/lib/summernote/summernote-bs5.min.js"></script>
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            Settings
                        </div>
                        <div class="card-body p-3">
                            <nav>
                                <div class="nav nav-tabs mb-3" id="nav-tab" role="tablist">
                                    <button class="nav-link" id="TermsAndConditions-tab" data-bs-toggle="tab" data-bs-target="#TermsAndConditions" type="button" role="tab" aria-controls="TermsAndConditions" aria-selected="false">Terms & Conditions</button>
                                </div>
                            </nav>
                            <div class="tab-content" id="nav-tabContent">
                                <div class="tab-pane fade" id="TermsAndConditions" role="tabpanel" aria-labelledby="TermsAndConditions-tab">
                                    <div class="row">
                                        <div class="col-md-12">
                                             <textarea name="editordata" runat="server" id="TermsAndConditionsContent" class="TermsAndConditionsContent"></textarea>
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
                <div class="col-md-12 text-end">
                    <asp:Button runat="server" ID="Save" Text="Save" OnClick="Save_Click" CssClass="btn btn-primary btn-sm" />
                </div>
            </div>
        </section>
    </main>
    <asp:HiddenField ID="SelectedTab" runat="server" />
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const tabButtons = document.querySelectorAll('[data-bs-toggle="tab"]');
            tabButtons.forEach(btn => {
                btn.addEventListener("shown.bs.tab", function (e) {
                    const selectedId = e.target.getAttribute("data-bs-target");
                    document.getElementById('<%= SelectedTab.ClientID %>').value = selectedId;
                });
            });

            // On page load, activate the stored tab
            const selectedTab = document.getElementById('<%= SelectedTab.ClientID %>').value;
            if (selectedTab) {
                const tab = document.querySelector(`[data-bs-target="${selectedTab}"]`);
                if (tab) new bootstrap.Tab(tab).show();
            }
        });
    </script>
     <script>
        $(document).ready(function () {
            $('.TermsAndConditionsContent').summernote({ height: 200 });
        });
    </script>
</asp:Content>

