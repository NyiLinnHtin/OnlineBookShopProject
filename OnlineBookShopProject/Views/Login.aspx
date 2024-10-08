﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineBookShopProject.Views.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Assets/styles/MyStyles.css" />
</head>
<body>
    <div class="container-fluid">
        <div class="row">
        </div>
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4 mt-5">
                <form id="form1" runat="server">
                    <div class="text-center">
                        <img src="../Assets/images/book.png" width="100" height="100" />
                    </div>
                    <div class="mt-3">
                        <div class="row">
                            <label class="col-md-4 control-label">User Name</label>
                            <div class="col-md-8">
                                <input type="text" placeholder="Mr.Smith" autocomplete="off" class="form-control placeholder-wave" />
                            </div>
                        </div>
                    </div>
                    <div class="mt-3">
                        <div class="row">
                            <label class="col-md-4 control-label">Password</label>
                            <div class="col-md-8">
                                <input type="password" placeholder="Password" autocomplete="off" class="form-control placeholder-wave" />
                            </div>
                        </div>
                    </div>

                    <div class="mt-3">
                        <asp:Button ID="LoginBtn" Text="Login" runat="server" class="btn my-button-login" OnClick="LoginBtn_Click" />
                    </div>
                </form>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>
</body>
</html>
