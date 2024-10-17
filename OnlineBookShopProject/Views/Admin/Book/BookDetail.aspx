<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Books" %>

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

        // Preview the selected image and store the file path in the hidden field
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('<%= bookImage.ClientID %>').src = e.target.result;
                };
                reader.readAsDataURL(input.files[0]);

                // Store the file name (or full path if needed) in the hidden field
                document.getElementById('<%= hiddenImagePath.ClientID %>').value = input.files[0]; // You can also store the path if you have one
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfModalOpen" runat="server" />
    <asp:HiddenField ID="HDAuthorID" runat="server" />

    <div class="container-fluid">
        <%--Title--%>
        <div class="row">
            <div class="col text-center">
                <h3>Manage Books</h3>
            </div>
        </div>

        <%--Error Label Text--%>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center"></asp:Label>
            </div>
            <div class="col-md-4"></div>
        </div>

        <!-- Book Image -->
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center">Book Image</label>
                    <div class="col-md-4 text-center">
                        <!-- Hidden field to store the image path -->
                        <input type="hidden" id="hiddenImagePath" runat="server" />

                        <!-- File input (hidden) -->
                        <input type="file" id="fileInput" accept="image/*" style="display: none;" onchange="previewImage(this);" />

                        <!-- Image display (click to open file input) -->
                        <img id="bookImage" runat="server" src="../../../Assets/images/UnknownBook.png" alt="Book Image" width="200" height="200"
                            style="cursor: pointer; border: 1px solid black;" onclick="document.getElementById('fileInput').click();" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Book Name Input -->
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center ">Name</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBookName" autocomplete="off" CssClass="form-control border-black" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <!-- Book Description -->
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center ">Description</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBookDescription" autocomplete="off" CssClass="form-control border-black" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <!-- Book Author Input-->
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center">Author</label>
                    <div class="col-md-4">
                        <div class="input-group">
                            <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control border-dark" placeholder="Search for author" autocomplete="off" Enabled="false" />
                            <asp:LinkButton ID="btnAuthorSearch" runat="server" CssClass="input-group-text border-dark" Style="cursor: pointer; text-decoration: none;" OnClick="btnAuthorSearch_Click">
                                <i class="fas fa-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--Category--%>
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center">Category</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-select border-dark">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center">Price</label>
                    <div class="col-md-4">
                        <input id="txtBookPrice" runat="server" type="number" autocomplete="off" class="form-control border-dark" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center">Quantity</label>
                    <div class="col-md-4">
                        <input id="txtBookQty" runat="server" type="number" autocomplete="off" class="form-control border-dark" />
                    </div>
                </div>
            </div>
        </div>

        <%--Buttons--%>
        <div class="row mt-3 justify-content-center">
            <div class="col-md-4 text-center">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" class="btn btn-warning" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnRegister" Text="Register" runat="server" class="btn btn-success" />
                <asp:Button ID="btnDelete" Text="Delete" runat="server" class="btn btn-danger" />
                <asp:Button ID="btnBack" Text="Back" runat="server" class="btn btn-secondary" OnClick="btnBack_Click" />
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

    <div id="confirmationModal" class="modal" runat="server">
        <div class="modal-content">
            <h4>Confirmation</h4>
            <asp:Label ID="lblConfirmContents" runat="server"></asp:Label>
            <div class="button-container mt-2">
                <asp:Button ID="btnConfirm" runat="server" Text="Yes" CssClass="btn btn-success" OnClick="btnConfirm_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="No" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
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

</asp:Content>
