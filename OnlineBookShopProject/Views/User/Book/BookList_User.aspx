<%@ Page Title="" Language="C#" MasterPageFile="~/Views/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="BookList_User.aspx.cs" Inherits="OnlineBookShopProject.Views.User.Book.BookList_User" %>

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
            <div class="card mb-3">

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

            <asp:ListView ID="listBooks" runat="server" OnItemCommand="listBooks_ItemCommand">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-4 col-sm-6 mb-4">
                        <!-- Adjust the size and spacing -->
                        <div class="card h-100">
                            <img class="card-img-top" src='<%# ResolveUrl(Eval("BookImage").ToString()) %>' alt="Book Image" style="height: 200px; object-fit: cover;" />
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("BookName") %></h5>
                                <p class="card-text">Author: <%# Eval("AuthorName") %></p>
                                <p class="card-text">Price: $<%# Eval("BookPrice") %></p>
                                <p class="card-text">Remaining: <%# Eval("BookQty") %></p>
                            </div>
                            <div class="card-footer text-center">
                                <asp:LinkButton runat="server" CommandName="AddToCart" CommandArgument='<%# Eval("BookId") + ";" + Eval("BookName") %>' CssClass="btn btn-success btn-sm">
                                    <i class="fa fa-shopping-cart"></i> Add
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="ViewDetails" CommandArgument='<%# Eval("BookId") %>' CssClass="btn btn-info btn-sm">
                                    <i class="fa fa-search"></i> View
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <LayoutTemplate>
                    <div class="row">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
            </asp:ListView>

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

            <div id="successModal" class="modal" runat="server">
                <div class="modal-content">
                    <h4>Success</h4>
                    <asp:Label ID="lblSuccessContents" runat="server" CssClass="text-success"></asp:Label>
                    <div class="button-container mt-2">
                        <asp:Button ID="btnSuccessOK" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="btnSuccessOK_Click" />
                    </div>
                </div>
            </div>

            <div id="failureModal" class="modal" runat="server">
                <div class="modal-content">
                    <h4>Failure</h4>
                    <asp:Label ID="lblFailureContents" runat="server" CssClass="text-danger"></asp:Label>
                    <div class="button-container mt-2">
                        <asp:Button ID="btnFailureOK" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="btnFailureOK_Click" />
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
