<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="OnlineBookShopProject.Views.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <title></title>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
        </div>
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-6 mt-5">
                <form id="form1" runat="server">
                    <div class="text-center">
                        <img src="../Assets/images/book.png" width="100" height="100" />
                    </div>

                    <div class="mt-3">
                        <div class="row">
                            <label class="col-md-4 control-label">Email</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="test@gmail.com" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="lnkCode" runat="server" Text="Get Code" OnClick="Code_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="mt-3">
                        <div class="row">
                            <label class="col-md-4 control-label">Code Password</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtCode" runat="server" placeholder="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mt-3" id="divPassword" runat="server">
                        <div class="row">
                            <label class="col-md-4 control-label">Password</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mt-3" id="divConfirmPassword" runat="server">
                        <div class="row">
                            <label class="col-md-4 control-label">Confirm Password</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- Error message label -->
                    <div class="mt-3">
                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                    </div>

                    <div class="mt-3" id="divSignUp" runat="server">
                        <asp:Button ID="btnSignUp" Text="Signup" runat="server" class="btn my-button-login" />
                    </div>
                </form>
            </div>
            <div class="col-md-3">
            </div>
        </div>
    </div>
</body>
</html>
