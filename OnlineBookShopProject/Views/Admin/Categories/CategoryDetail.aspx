<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CategoryDetail.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Categories.CategoryDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <div class="row mt-3 mb-5">
            <div class="col text-center">
                <h3>Manage Categories</h3>
            </div>
        </div>

        <!-- Error message row (above the Category label) -->
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center"></asp:Label>
            </div>
            <div class="col-md-4"></div>
        </div>

        <!-- Category input field -->
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center ">
                        Category
                <span class="text-danger">*</span>
                    </label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCategory" autocomplete="off" CssClass="form-control border-black" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <!-- Description input field -->
        <div class="row">
            <div class="mt-3">
                <div class="row justify-content-center">
                    <label class="col-md-2 control-label text-black align-self-center ">Description</label>
                    <div class="col-md-4">
                        <textarea id="txtDescription" type="text" autocomplete="off" class="form-control border-black" runat="server" rows="3"></textarea>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Buttons -->
    <div class="row mt-3 justify-content-center">
        <div class="col-md-4 text-center">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" class="btn btn-warning" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnRegister" Text="Register" runat="server" class="btn btn-success" OnClick="btnRegister_Click" OnClientClick="return confirmRegister()" />
            <asp:Button ID="btnDelete" Text="Delete" runat="server" class="btn btn-danger" OnClick="btnDelete_Click" />
            <asp:Button ID="btnBack" Text="Back" runat="server" class="btn btn-secondary" OnClick="btnBack_Click" OnClientClick="return confirmChanges()" />
        </div>
    </div>


    <!-- Modal -->
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
