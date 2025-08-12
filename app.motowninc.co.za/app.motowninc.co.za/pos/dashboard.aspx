<%@ Page Title="" Language="C#" MasterPageFile="~/pos/pos.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="pos_dashboard" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-4">
                <asp:Repeater runat="server" ID="Customers" OnItemCommand="Customers_ItemCommand">
                    <ItemTemplate>
                        <h6><%#Eval("Name")%></h6>
                        <sup><%#Eval("AccountType")%></sup>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-4">
                <asp:Repeater runat="server" ID="Products" OnItemCommand="Products_ItemCommand">
                    <ItemTemplate>
                        <h6><%#Eval("Name")%></h6>
                        <sup><%#Eval("Type")%>-<%#Eval("Code")%> : <%#Eval("Price")%></sup>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-4">
                 <asp:Repeater runat="server" ID="Cart" OnItemCommand="Cart_ItemCommand">
                      <ItemTemplate>

                      </ItemTemplate>
                 </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>

