<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="DateInvite.aspx.cs" Inherits="DateInvite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/DateFormSheet.css" rel="stylesheet" />
    <div class="titleBlock">
        <p>Date Invitation for <%= friendName %></p>
    </div>
    <div class="allContent">
        <div class="dateLocation">
            <div class="box1">
                <p class="dateTextFashion">Location</p>
            </div>
            <div class="box2">
                <asp:TextBox ID="TextBoxLocation" autocomplete="off" runat="server" CssClass="textBoxFashion" MaxLength="43"></asp:TextBox>
            </div>
        </div>

        <div class="dateTime">
            <div class="box1">
                <p class="dateTextFashion">Time</p>
            </div>
            <div class="box2">
                <asp:TextBox ID="TextBoxTime" runat="server" CssClass="textBoxFashion" TextMode="Time"></asp:TextBox>
            </div>
        </div>

        <div class="dateDate">
            <div class="box1">
                <p class="dateTextFashion">Date</p>
            </div>
            <div class="box2">
                <asp:TextBox ID="TextBoxDate" runat="server" CssClass="textBoxFashion" TextMode="Date"></asp:TextBox>
            </div>
        </div>

        <div class="dateDescription">
            <div class="box1">
                <p class="dateTextFashionDescription">Description</p>
            </div>
            <div class="box2">
                <asp:TextBox ID="TextBoxDescription" runat="server" CssClass="textBoxFashionDescription" TextMode="MultiLine" MaxLength="350"></asp:TextBox>
            </div>
            <div class="clearArea"></div>
        </div>

        <div class="buttonArea">
            <asp:Button ID="ButtonSendInvitation" runat="server" Text="Send Invitation" CssClass="buttonFashionSend" OnClick="ButtonSendInvitation_Click" />
            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="buttonFashionCancel" OnClick="ButtonCancel_Click" />
        </div>
    </div>
</asp:Content>

