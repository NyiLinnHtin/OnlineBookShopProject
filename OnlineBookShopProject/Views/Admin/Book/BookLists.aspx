﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BookLists.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Book.BookLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showModal() {
            document.getElementById("authorSearchModal").style.display = "block";
        }

        function hideModal() {
            document.getElementById("authorSearchModal").style.display = "none";
        }

        window.onload = function () {
            // Check if the modal should be opened
            var modalOpen = document.getElementById('<%= hfModalOpen.ClientID %>').value;
            if (modalOpen === "true") {
                showModal();
                // Reset the hidden field to avoid reopening on future postbacks
                document.getElementById('<%= hfModalOpen.ClientID %>').value = "false";
            }
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfModalOpen" runat="server" />
    <div class="container-fluid">
        <div class="row mb-3">
            <div class="text-center mt-3">
                <h3>Book Lists</h3>
            </div>
        </div>

        <div>
            <asp:Label ID="lblDisplayError" runat="server" CssClass="text-danger h5"></asp:Label>
        </div>
        <div class="container mt-4">
            <!-- Collapsible Panel -->
            <div class="card">

                <div class="card-header" data-bs-toggle="collapse" data-bs-target="#searchBooksCollapse" aria-expanded="true" aria-controls="searchBooksCollapse" style="cursor: pointer; background-color: lightblue">
                    <h5 class="mb-0">Search Books</h5>
                </div>

                <div id="searchBooksCollapse" class="collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                    <div class="card-body">
                        <!-- Book Name Input -->
                        <div class="row mb-3 justify-content-center">
                            <label class="col-md-2 control-label text-black align-self-center">Book Name</label>
                            <div class="col-md-4">
                                <input id="txtBookName" type="text" autocomplete="off" class="form-control border-black" runat="server" />
                            </div>
                        </div>

                        <!-- Author Input -->
                        <div class="row mb-3 justify-content-center">
                            <label class="col-md-2 control-label text-black align-self-center">Author</label>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control border-dark" placeholder="No Author is Selected!" autocomplete="off" Enabled="false" />
                                    <asp:LinkButton ID="btnAuthorSearch" runat="server" CssClass="input-group-text border-dark" Style="cursor: pointer; text-decoration: none;" OnClick="btnAuthorSearch_Click">
                                    <i class="fas fa-search"></i>
                                </asp:LinkButton>
                                </div>
                            </div>
                            <asp:HiddenField ID="hfAuthorId" runat="server" />
                        </div>

                        <!-- Category Dropdown -->
                        <div class="row mb-3 justify-content-center">
                            <label class="col-md-2 control-label text-black align-self-center">Category</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-select border-dark">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Price Range Inputs -->
                        <div class="row justify-content-center">
                            <label class="col-md-2 control-label text-black align-self-center">Price Range</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtPriceStart" runat="server" CssClass="form-control border-dark" placeholder="Min Price" TextMode="Number" min="1" />
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtPriceEnd" runat="server" CssClass="form-control border-dark" placeholder="Max Price" TextMode="Number" min="1" />
                            </div>
                        </div>

                        <!-- Search Button -->
                        <div class="row mt-3 justify-content-center">
                            <div class="col-md-4 text-center">
                                <asp:Button ID="btnGridSearch" Text="Search" runat="server" class="btn btn-primary" OnClick="btnGridSearch_Click" />
                                <asp:Button ID="btnClear" Text="Clear" runat="server" class="btn btn-secondary" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row mt-3 justify-content-start">
                <div class="col">
                    <asp:Button ID="btnAdd" Text="Add" runat="server" class="btn btn-success" Width="150px" OnClick="btnAdd_Click" />
                </div>
            </div>

            <div class="row mt-1 justify-content-center">
                <asp:GridView ID="gridBooks" runat="server" CssClass="searchGrid"
                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
                    OnPageIndexChanging="gridBooks_PageIndexChanging"
                    OnRowCommand="gridBooks_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CommandName="EditBook" CommandArgument='<%# Eval("BookId") %>'>
                                <i class="fa fa-edit fa-lg"></i>
                            </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="DeleteBook" CommandArgument='<%# Eval("BookId") %>'>
                                <i class="fa fa-trash fa-lg"></i>
                            </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="ViewBook" CommandArgument='<%# Eval("BookId") %>'>
                                <i class="fa fa-search fa-lg"></i>
                            </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BookName" HeaderText="Book Name" />
                        <asp:BoundField DataField="AuthorName" HeaderText="Author" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="BookQty" HeaderText="Quantity" />
                        <asp:BoundField DataField="BookPrice" HeaderText="Price" />
                    </Columns>
                </asp:GridView>
            </div>
            <div>
                <asp:Label ID="lblNoListDisplay" runat="server" CssClass="no-data-message">There is no Corresponding Book to Display</asp:Label>
            </div>


            <!-- Author Search Modal -->
            <div class="modal" id="authorSearchModal" tabindex="-1" role="dialog" aria-labelledby="authorSearchModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="authorSearchModalLabel">Author Lists</h5>
                            <button type="button" class="close" aria-label="Close" onclick="hideModal()" style="position: absolute; right: 10px; top: 10px; background-color: red; border: none; color: white;">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div>
                            <asp:Label ID="lblAuthorSearchErr" CssClass="text-danger" runat="server"></asp:Label>
                        </div>
                        <div class="modal-body">
                            <label class="form-label text-dark">Author ID</label>
                            <asp:TextBox ID="txtAuthorID" runat="server" CssClass="form-control border-dark" placeholder="Enter author ID" autocomplete="off" />

                            <label class="form-label text-dark mt-2">Author Name</label>
                            <asp:TextBox ID="txtAuthorName" runat="server" CssClass="form-control border-dark" placeholder="Enter author name" autocomplete="off" />

                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch_Click" />

                            <div>
                                <asp:Label ID="lblNoAuthor" runat="server" CssClass="no-data-message">There is no Corresponding Author</asp:Label>
                            </div>

                            <div style="overflow: auto;">
                                <asp:GridView ID="gvAuthors" runat="server" CssClass="table table-striped mt-3"
                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
                                    OnPageIndexChanging="gvAuthors_PageIndexChanging" OnRowCommand="gvAuthors_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="SelectAuthor" CommandArgument='<%# Eval("AuthorId") + ";" + Eval("AuthorName") %>'>
                                            <i class="fa fa-check fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AuthorId" HeaderText="Author ID" />
                                        <asp:BoundField DataField="AuthorName" HeaderText="Author Name" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
