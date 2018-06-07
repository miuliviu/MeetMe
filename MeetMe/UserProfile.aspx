<%@ Page Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="userProfileContentBody" ContentPlaceHolderID="contentBody" runat="server" >
    <script type="text/javascript">
         function uploadPicture()
         {
             document.getElementById('<%=FileUploadChangeProfilePicture.ClientID%>').click();
         }

        function goToDB(fileToUpload)
        {

            if (fileToUpload.value != '') {
            document.getElementById("<%=ButtonSendPictureToDB.ClientID %>").click();
            }
            
        }


    </script>
    
    <link href="CSS/UserProfileStyle.css" rel="stylesheet" />

    <div class="contentBorder">
        <div class="leftPart">            
            <!--<asp:Label ID="LabelWelcome" runat="server" Text="Welcome" CssClass="titleFashion"></asp:Label>-->
            <p id="welcomeText" runat="server" class="titleFashion">Welcome</p>
            <br />
            <asp:Image ID="ImageProfilePicture" runat="server" CssClass="mainUserProfilePic" />
            <br />
            <asp:FileUpload ID="FileUploadChangeProfilePicture" runat="server" Style="display:none;"/>

             
            <a href="#" onclick="uploadPicture();return false;" class="changePic">Change Profile Picture</a><br />

            <asp:Button ID="ButtonSendPictureToDB" runat="server" OnClick="ButtonSendPictureToDB_Click" Text="SendPic" Style="display:none;"/>
            <asp:Button ID="ButtonFriends" runat="server" CssClass="buttonFashionFriends" OnClick="ButtonFriends_Click" Text="Friends" />
            <asp:Button ID="ButtonDates" runat="server" Text="Dates" CssClass="buttonFashionDates" OnClick="ButtonDates_Click" />
            <br />
            <div class="containerGal">
                <asp:imagebutton runat="server" ID="ImageButtonGallery" CssClass="galleryImg" Height="180px" OnClick="ImageButtonGallery_Click" Width="180px" ></asp:imagebutton>
                <div class="centeredText"><a href="UserGallery.aspx">Gallery</a></div>
            </div>
        </div>
        
        <div class="rightPart">
            <div class="titleBlock">
                <p>Profile Details</p>
            </div>
            <table class="profileDetailsTable">
                <tr><td><p><b>Description</b></p></td><td><%= description %></td></tr>
                <tr><td><p><b>City</b></p></td><td><%= city %></td></tr>
                <tr><td><p><b>Age</b></p></td><td><%= age %></td></tr>
                <tr><td><p><b>Hobby</b></p></td><td><%= hobby %></td></tr>
                <tr><td><p><b>Gender</b></p></td><td><%= gender %></td></tr>
                <tr><td><p><b>Interested in</b></p></td><td><%= interested %></td></tr>
                <tr><td><p><b>Relationship Status</b></p></td><td><%= relationStatus %></td></tr>
                <tr><td><p><b>Account Privacy</b></p></td><td><%= privacy %></td></tr>
            </table>
            <asp:Button ID="ButtonEditDetails" runat="server" Text="Edit Details" OnClick="ButtonEditDetails_Click" CssClass="buttonFashion" />
            <br />
            <hr class="breakLineRightPart" />
            <br />
            <div class="suggestionTextFashion">
                <p class="peopleTitle">People you may know</p>
            </div>
            <div id="suggestionDiv" runat="server" class="suggestionDivFashion">

            </div>
        </div>
    </div>
</asp:Content>
    
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            text-decoration: none;
        }
    </style>
    </asp:Content>

    
