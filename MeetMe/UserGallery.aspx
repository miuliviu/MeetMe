<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="UserGallery.aspx.cs" Inherits="UserGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <script type="text/javascript">
        function goToDB(fileToUpload)
        {
            if (fileToUpload.value != '') {
            document.getElementById("<%=ButtonSendTheData.ClientID %>").click();
            }
        }

        function uploadPicture() {
            document.getElementById('<%=FileUploadPicture.ClientID%>').click();
        }
    </script>
    <link href="CSS/UserGalleryStyle.css" rel="stylesheet" />
    <div class="titleBlock">
            <p>Your Gallery</p>
    </div>
    <div class="allContent">
        <p id="noPictureText" class="noPcText" runat="server" visible="False">You have no pictures.</p>
        <div class="contentBorder" id="galleryContent" runat="server">

        </div>
        <div class="clearArea"></div>
        <div class="buttonArea">
            <asp:Button ID="ButtonUpload" runat="server" Text="Upload Picture" CssClass="buttonFashionUpload" />
            <asp:FileUpload ID="FileUploadPicture" runat="server" Style="display:none;" />
            <asp:Button ID="ButtonSendTheData" runat="server" Text="Button" OnClick="ButtonSendTheData_Click" Style="display:none;" />
        </div>
    </div>
</asp:Content>

