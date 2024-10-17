<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AuthorLists.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Authors" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="text-center mt-3">
                <h3>Author Lists</h3>
            </div>
        </div>

        <div class="container mt-4">
            <!-- Collapsible Panel -->
            <div class="card">

                <div class="card-header" data-bs-toggle="collapse" data-bs-target="#searchBooksCollapse" aria-expanded="true" aria-controls="searchBooksCollapse" style="cursor: pointer; background-color: lightblue">
                    <h5 class="mb-0">Search Authors</h5>
                </div>

                <div id="searchBooksCollapse" class="collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                    <div class="card-body">

                        <!-- Author Name Input -->
                        <div class="row mb-3 justify-content-center">
                            <label class="col-md-2 control-label text-black align-self-center">Author Name</label>
                            <div class="col-md-4">
                                <input id="txtAuthorName" type="text" autocomplete="off" class="form-control border-black" runat="server" />
                            </div>
                        </div>

                        <!-- Author Gender DropDown -->
                        <div class="row mb-3 justify-content-center">
                            <label class="col-md-2 control-label text-black align-self-center">Author Gender</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlAuthorGender" runat="server" CssClass="form-select border-dark" data-style="btn-primary">
                                    <asp:ListItem Text="-- Select Gender --"></asp:ListItem>
                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Author Country DropDown -->
                        <div class="row mb-3 justify-content-center">
                            <label class="col-md-2 control-label text-black align-self-center">Author Country</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlAuthorCountry" runat="server" CssClass="form-select border-dark" data-style="btn-primary">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Search & Clear Button -->
                        <div class="row mt-3 justify-content-center">
                            <div class="col-md-4 text-center">
                                <asp:Button ID="btnGridSearch" Text="Search" runat="server" class="btn btn-primary" OnClick="btnGridSearch_Click" />
                                <asp:Button ID="btnClear" Text="Clear" runat="server" class="btn btn-secondary" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-3 justify-content-start">
            <div class="col">
                <asp:Button ID="btnAdd" Text="Add" runat="server" class="btn btn-success" Width="150px" />
            </div>
        </div>

        <div class="row mt-1 justify-content-center">
            <asp:GridView ID="gridAuthors" runat="server" CssClass="searchGrid"
                AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
                OnPageIndexChanging="gridAuthors_PageIndexChanging"
                OnRowCommand="gridAuthors_RowCommand">
                <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CommandName="EditBook" CommandArgument='<%# Eval("AuthorId") %>'>
                                <i class="fa fa-edit fa-lg"></i>
                            </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="DeleteBook" CommandArgument='<%# Eval("AuthorId") %>'>
                                <i class="fa fa-trash fa-lg"></i>
                            </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="ViewBook" CommandArgument='<%# Eval("AuthorId") %>'>
                                <i class="fa fa-search fa-lg"></i>
                            </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AuthorName" HeaderText="Author" />
                        <asp:BoundField DataField="AuthorGender" HeaderText="Gender" />
                        <asp:BoundField DataField="AuthorCountry" HeaderText="Country" />
                    </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
