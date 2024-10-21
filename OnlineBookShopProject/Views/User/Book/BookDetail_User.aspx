<%@ Page Title="" Language="C#" MasterPageFile="~/Views/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="BookDetail_User.aspx.cs" Inherits="OnlineBookShopProject.Views.User.Book.BookDetail_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <img id="bookImage" runat="server" src="../../../Assets/images/UnknownBook.png" alt="Book Image" width="200" height="200"
                            style="border: 1px solid black;" />
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
                        <asp:TextBox ID="txtBookName" Enabled="false" autocomplete="off" CssClass="form-control border-black" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="txtBookDescription" Enabled="false" autocomplete="off" CssClass="form-control border-black" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control border-dark" autocomplete="off" Enabled="false" />
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
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control border-dark" autocomplete="off" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>

        <%--Book Price--%>
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center">Price</label>
                    <div class="col-md-4">
                        <input id="txtBookPrice" runat="server" disabled="disabled" autocomplete="off" class="form-control border-dark" />
                    </div>
                </div>
            </div>
        </div>

        <%--Book Qty--%>
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center">In Stock</label>
                    <div class="col-md-4">
                        <input id="txtBookQty" runat="server" disabled="disabled" autocomplete="off" class="form-control border-dark" />
                    </div>
                </div>
            </div>
        </div>

        <%--Buttons--%>
        <div class="row mt-3 justify-content-center">
            <div class="col-md-4 text-center">
                <asp:Button ID="btnAddCart" Text="Add To Cart" runat="server" class="btn btn-success" />
                <asp:Button ID="btnBack" Text="Back" runat="server" class="btn btn-secondary" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</asp:Content>
