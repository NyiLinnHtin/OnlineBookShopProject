<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Sellers.aspx.cs" Inherits="OnlineBookShopProject.Views.Admin.Sellers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col text-center">
                <h3>Manage Sellers</h3>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4">
                <div>
                    <label class="form-label text-dark">Seller Name</label>
                    <input type="text" autocomplete="off" class="form-control border-dark" />
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Seller Email</label>
                    <input type="text" autocomplete="off" class="form-control border-dark" />
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Seller Phone</label>
                    <input type="text" autocomplete="off" class="form-control border-dark" />
                </div>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-4 mb-2">
                <div>
                    <label class="form-label text-dark">Seller Address</label>
                    <textarea autocomplete="off" class="form-control border-dark" rows="5"></textarea>
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
</asp:Content>
