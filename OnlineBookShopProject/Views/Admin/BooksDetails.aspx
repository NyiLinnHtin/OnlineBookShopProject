<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BooksDetails.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Books" %>

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
        <div class="row">
            <div class="col text-center">
                <h3>Manage Books</h3>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4">
                <div>
                    <label class="form-label text-dark">Book Title</label>
                    <input type="text" autocomplete="off" class="form-control border-dark" />
                </div>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div class="input-group">
                    <label class="form-label text-dark">Book Author</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control border-dark" placeholder="Search for author" autocomplete="off" Enabled="false" />
                        <asp:LinkButton ID="btnAuthorSearch" runat="server" CssClass="input-group-text border-dark" Style="cursor: pointer; text-decoration: none;" OnClick="btnAuthorSearch_Click">
                            <i class="fas fa-search"></i>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Category</label>
                    <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-select border-dark">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Price</label>
                    <input type="number" autocomplete="off" class="form-control border-dark" />
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Quality</label>
                    <input type="number" autocomplete="off" class="form-control border-dark" />
                </div>
            </div>
        </div>
        <div class="row mb-3 justify-content-lg-start">
            <div class="col-md-4 d-flex justify-content-between">
                <asp:Button Text="Update" runat="server" class="btn btn-warning" />
                <asp:Button Text="Register" runat="server" class="btn btn-success" />
                <asp:Button Text="Delete" runat="server" class="btn btn-danger" />
            </div>
        </div>
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
                <div class="modal-body">
                    <label class="form-label text-dark">Author ID</label>
                    <asp:TextBox ID="txtAuthorID" runat="server" CssClass="form-control border-dark" placeholder="Enter author ID" autocomplete="off" />

                    <label class="form-label text-dark mt-2">Author Name</label>
                    <asp:TextBox ID="txtAuthorName" runat="server" CssClass="form-control border-dark" placeholder="Enter author name" autocomplete="off" />

                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch_Click" />

                    <div>
                        <label id="lblNoAuthor" class="text-danger" runat="server">There is no Author to Display</label>
                    </div>
                    <!-- GridView for displaying search results -->
                    <div style="overflow: auto;">
                        <asp:GridView ID="gvAuthors" runat="server" CssClass="table table-striped mt-3"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10"
                            OnPageIndexChanging="gvAuthors_PageIndexChanging" OnRowCommand="gvAuthors_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandName="SelectAuthor" CommandArgument='<%# Eval("AuthorName") %>'>
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
</asp:Content>
