<%@ Page Title="" Language="C#" MasterPageFile="~/Views/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="CheckOut_User.aspx.cs" Inherits="OnlineBookShopProject.Views.User.CheckOut.CheckOut_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container checkout-container">
        <div class="row">
            <!-- Shipping Information -->
            <div class="col-md-7">
                <div class="card p-4">
                    <h4 class="mb-3">Shipping Information</h4>
                    <form>
                        <div class="mb-3">
                            <label for="fullName" class="form-label">Full Name</label>
                            <input type="text" class="form-control" id="fullName" placeholder="John Doe">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Address</label>
                            <input type="text" class="form-control" id="address" placeholder="1234 Main St">
                        </div>
                        <div class="mb-3">
                            <label for="city" class="form-label">City</label>
                            <input type="text" class="form-control" id="city" placeholder="Los Angeles">
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="state" class="form-label">State</label>
                                <input type="text" class="form-control" id="state" placeholder="CA">
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="zip" class="form-label">Zip Code</label>
                                <input type="text" class="form-control" id="zip" placeholder="90001">
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="phone" class="form-label">Phone Number</label>
                            <input type="text" class="form-control" id="phone" placeholder="(123) 456-7890">
                        </div>
                    </form>
                </div>
            </div>

            <!-- Order Summary -->
            <div class="col-md-5">
                <div class="summary-box">
                    <h5>Order Summary</h5>

                    <!-- Loop through items from the database -->
                    <asp:Repeater ID="rptOrderSummary" runat="server">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between">
                                <!-- Item Name and Quantity -->
                                <p><%# Eval("BookName") %>(x<%# Eval("Qty") %>)</p>
                                <!-- Item Price -->
                                <p>$<%# Eval("TotalAmount", "{0:N2}") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="d-flex justify-content-between">
                        <p>Shipping</p>
                        <p>$<asp:Label ID="lblShippingCost" runat="server" Text="5.00" /></p>
                        <!-- You can dynamically change this -->
                    </div>

                    <hr>
                    <div class="d-flex justify-content-between">
                        <h5>Total</h5>
                        <h5>$<asp:Label ID="lblTotalAmount" runat="server" Text="0.00" /></h5>
                        <!-- Total Amount dynamically calculated -->
                    </div>

                    <div class="mt-3">
                        <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="place-order-btn" />
                    </div>

                    <div class="mt-3">
                        <asp:Button ID="btnPrint" runat="server" Text="Place Order" CssClass="place-order-btn" OnClick="btnPrint_Click" />
                    </div>
                </div>
            </div>

            <div class="row mt-5">
                <!-- Payment Method -->
                <div class="col-md-7">
                    <div class="card p-4">
                        <h4 class="mb-3">Payment Method</h4>
                        <form>
                            <%--<div class="mb-3">
                            <label for="cardNumber" class="form-label">Card Number</label>
                            <input type="text" class="form-control" id="cardNumber" placeholder="1111-2222-3333-4444">
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="expiry" class="form-label">Expiry Date</label>
                                <input type="text" class="form-control" id="expiry" placeholder="MM/YY">
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="cvv" class="form-label">CVV</label>
                                <input type="text" class="form-control" id="cvv" placeholder="123">
                            </div>
                        </div>--%>
                            <div class="form-check mb-3">
                                <input class="form-check-input" type="radio" name="paymentMethod" id="cashOnDeli" checked>
                                <label class="form-check-label" for="paypal">
                                    Cash On Delivery
                                </label>
                            </div>
                            <div class="form-check mb-3">
                                <input class="form-check-input" type="radio" name="paymentMethod" id="paypal">
                                <label class="form-check-label" for="paypal">
                                    PayPal
                                </label>
                            </div>
                            <div class="form-check mb-3">
                                <input class="form-check-input" type="radio" name="paymentMethod" id="creditCard">
                                <label class="form-check-label" for="creditCard">
                                    Credit Card
                           
                                </label>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
