<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CategoryLists.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Categories.CategoryLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="text-center mt-3">
                <h3>Categories Lists</h3>
            </div>
        </div>

        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-1 control-label text-black align-self-center ">Category</label>
                    <div class="col-md-4">
                        <input id="txtCategory" type="text" autocomplete="off" class="form-control border-black" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-3 justify-content-center">
            <div class="col-md-4 text-center">
                <asp:Button ID="btnSearch" Text="Search" runat="server" class="btn btn-primary" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" class="btn btn-secondary" OnClick="btnClear_Click" />
            </div>
        </div>

        <div class="row mt-3 justify-content-start">
            <div class="col">
                <asp:Button ID="btnAdd" Text="Add" runat="server" class="btn btn-success" Width="150px" OnClick="btnAdd_Click" />
            </div>
        </div>

        <div class="row mt-1 justify-content-center">
            <asp:GridView ID="gridCategory" runat="server" CssClass="searchGrid"
                AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
                OnPageIndexChanging="gridCategory_PageIndexChanging"
                OnRowCommand="gridCategory_RowCommand">
                <%--<PagerStyle CssClass="gridPager" />--%>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandName="EditCategory" CommandArgument='<%# Eval("CategoryId") %>'>
                                <i class="fa fa-edit fa-lg"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" CommandName="DeleteCategory" CommandArgument='<%# Eval("CategoryId") %>'>
                                <i class="fa fa-trash fa-lg"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" CommandName="ViewCategory" CommandArgument='<%# Eval("CategoryId") %>'>
                                <i class="fa fa-search fa-lg"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="CategoryDescription" HeaderText="Description" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
