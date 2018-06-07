<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="EditDetails.aspx.cs" Inherits="EditDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            display: inline;
            width: 100%;
            height: 100%;
            overflow: hidden;
        }
        .auto-style3 {
            height: 43px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/EditDetailsStyle.css" rel="stylesheet" />
    <div class="titleBlock">
            <p>Edit Details</p>
    </div>
    <div class="contentEditDetails">
        <table class="profileDetailsTable">
                <tr>
                    <td><p><b>Description</b></p></td>
                    <td><asp:TextBox ID="TextBoxDescription" runat="server" MaxLength="55" Width="128px" CssClass="inputTextFashion"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td><p><b>Location</b></p></td>
                    <td><asp:TextBox ID="TextBoxLocation" runat="server" MaxLength="25" Width="128px" CssClass="inputTextFashion"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><p><b>Age</b></p></td>
                    <td><asp:TextBox ID="TextBoxAge" runat="server" TextMode="Number" Width="128px" CssClass="inputTextFashion"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><p><b>Hobby</b></p></td>
                    <td><asp:TextBox ID="TextBoxHobby" runat="server" MaxLength="25" Width="128px" CssClass="inputTextFashion"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><p><b>Gender</b></p></td>
                    <td dir="ltr">
                        <asp:DropDownList ID="DropDownListGender" runat="server" Width="128px" CssClass="dropDownFashion">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td><p><b>Interested in</b></p></td>
                    <td>
                        <asp:DropDownList ID="DropDownListInterested" runat="server" Width="128px" CssClass="dropDownFashion">
                            <asp:ListItem>Man</asp:ListItem>
                            <asp:ListItem>Woman</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td><p><b>Relationship Status</b></p></td>
                    <td><asp:DropDownList ID="DropDownListRelationship" runat="server" Width="128px" CssClass="dropDownFashion">
                        <asp:ListItem>In a relationship</asp:ListItem>
                        <asp:ListItem>Single</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>            
            </table>
        
    </div>
    <div class="actionButtons">
        <asp:Button ID="ButtonSaveChanges" runat="server" Text="Save Changes" OnClick="ButtonSaveChanges_Click" CssClass="buttonFashion" /><asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" CssClass="buttonFashion" />
    </div>
    
</asp:Content>

