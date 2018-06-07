<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MeetMeLogReg.master" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="registrationContentBody" ContentPlaceHolderID="contentBody" runat="server" >
    <link href="CSS/RegistrationStyle.css" rel="stylesheet" />
    <div style="width:50%; float:left">
            <a href="Login.aspx"><img src="Images/Logo.png" alt="Error" style="padding-top:25%; margin-left:70px;" ></a>
    </div>
    <div style="width:50%; float:right; text-align: center;">
        <p class="titleFashion">Registration</p>
        <table class="tableStyle">
            <tr>
                <td class="auto-style1"><b>First Name</b></td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBoxFirstName" runat="server" OnTextChanged="TextBox1_TextChanged" Width="192px" CssClass="inputFashion"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxFirstName" ErrorMessage="First Name is required." ForeColor="Red" ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"><b>Surname</b></td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBoxSurname" runat="server" Width="192px" CssClass="inputFashion"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxSurname" ErrorMessage="Surname is required." ForeColor="Red" ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"><b>E-mail</b></td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBoxEmail" runat="server" Width="192px" CssClass="inputFashion"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="E-mail is required." ForeColor="Red" ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Please enter a valid E-mail Address." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="RegistrationValidation"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"><b>Password</b></td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password" Width="192px" CssClass="inputFashion"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxPass" ErrorMessage="Password is required." ForeColor="Red" ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"><b>Confirm Password</b></td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBoxCPass" runat="server" TextMode="Password" Width="192px" CssClass="inputFashion"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxCPass" ErrorMessage="Confirm password is required." ForeColor="Red" ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxPass" ControlToValidate="TextBoxCPass" ErrorMessage="Passwords must match." ForeColor="Red" ValidationGroup="RegistrationValidation"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"><b>Country</b></td>
                <td class="auto-style3">
                    <asp:DropDownList ID="DropDownListCountry" runat="server" Width="200px" CssClass="dropDownFashion">
                        <asp:ListItem>Select Country</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
                        <asp:ListItem>Romania</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownListCountry" ErrorMessage="Please select a Country." ForeColor="Red" InitialValue="Select Country" ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"><b>Gender</b></td>
                <td class="auto-style3">
                    <asp:DropDownList ID="DropDownListGender" runat="server" Width="200px" CssClass="dropDownFashion">
                        <asp:ListItem>Select Gender</asp:ListItem>
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DropDownListGender" ErrorMessage="Please select a Gender." ForeColor="Red" InitialValue="Select Gender" ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>              
        </table>
        <div class="buttonSection">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Register" Width="100px" ValidationGroup="RegistrationValidation" CssClass="buttonFashion" />
            <asp:Button ID="ButtonToLogin" runat="server" OnClick="ButtonToLogin_Click" Text="Back to Login" Width="100px" CssClass="buttonFashion" />              
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 127px;
            text-align: right;
        }
    </style>
</asp:Content>

