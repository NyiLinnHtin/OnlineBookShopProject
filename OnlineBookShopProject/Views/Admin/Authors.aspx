<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Authors.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Authors" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col text-center">
                <h3>Manage Author</h3>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4">
                <div>
                    <label class="form-label text-dark">Author Name</label>
                    <input id="txtAuthorName" type="text" autocomplete="off" class="form-control border-dark" runat="server" />
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Author Gender</label>
                    <asp:DropDownList ID="ddlAuthorGender" runat="server" CssClass="form-select border-dark" data-style="btn-primary">
                        <asp:ListItem Text="-- Select Gender --"></asp:ListItem>
                        <asp:ListItem Text="Male"></asp:ListItem>
                        <asp:ListItem Text="Female"></asp:ListItem>
                        <asp:ListItem Text="Not Mention"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Author Country</label>
                    <asp:DropDownList ID="ddlAuthorCountry" runat="server" CssClass="form-select border-dark" data-style="btn-primary">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
        <div class="row mb-3 justify-content-lg-start">
            <div class="col-md-4 d-flex justify-content-between">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" class="btn btn-warning" />
                <asp:Button ID="btnRegister" Text="Register" runat="server" class="btn btn-success" OnClick="btnRegister_Click" />
                <asp:Button ID="btnDelete" Text="Delete" runat="server" class="btn btn-danger" />
            </div>
        </div>
    </div>

    <div class="col-md-8">
        <asp:GridView ID="gridAuthors" runat="server" CssClass="searchGrid"></asp:GridView>
    </div>
</asp:Content>
