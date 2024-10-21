<%@ Page Title="" Language="C#" MasterPageFile="~/Views/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmCheckOut_User.aspx.cs" Inherits="OnlineBookShopProject.Views.User.CheckOut.ConfirmCheckOut_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-1 justify-content-center">
        <asp:GridView ID="gridBooks" runat="server" CssClass="searchGrid lastTotalAmount"
            AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
            OnRowCommand="gridBooks_RowCommand">
            <Columns>
                <asp:BoundField DataField="Number" HeaderText="#" />
                <asp:BoundField DataField="BookName" HeaderText="Book Name" />
                <asp:BoundField DataField="BookPrice" HeaderText="Price" />

                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Button ID="btnDecrease" runat="server" CommandName="DecreaseQty" CommandArgument='<%# Eval("BookId") %>' Text="-" CssClass="btn btn-outline-secondary btn-sm" />
                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' CssClass="mx-2" />
                        <asp:Button ID="btnIncrease" runat="server" CommandName="IncreaseQty" CommandArgument='<%# Eval("BookId") %>' Text="+" CssClass="btn btn-outline-secondary btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalAmount", "{0:N2}") %>' CssClass="total-amount-label" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Display Final Total Amount Above the Grid -->
    <div class="row justify-content-center mt-1">
        <div class="col-md-12">
            <div class="alert text-end alert-success rounded-3">
                <h4 class="mb-0">
                    <asp:Label ID="lblFinalTotal" runat="server" Text="0.00" CssClass="final-total-amount" />
                </h4>
            </div>
        </div>
    </div>

    <!-- Display Final Total Amount Above the Grid -->
    <div class="row justify-content-center mt-1">
        <div class="col-md-12 text-end">
            <asp:Button ID="Button1" runat="server" Text="Order Now" CssClass="btn btn-primary btn-lg ms-3" OnClick="btnOrderNow_Click" />
        </div>
    </div>


    <div>
        <asp:Label ID="lblNoListDisplay" runat="server" CssClass="no-data-message">There is no Book in your Cart!</asp:Label>
    </div>
</asp:Content>
