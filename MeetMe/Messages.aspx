<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Messages" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>
    </title>
    <link href="CSS/MainStyle.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">
        .auto-style1 {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript">
        function pressButtonLogout() {
            document.getElementById('<%=ButtonLogout.ClientID%>').click();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#messagesScreenID").scrollTop($("#messagesScreenID")[0].scrollHeight);
            document.getElementById("<%=TextBoxInputMessage.ClientID%>").focus();
        });
    </script>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="menu">
            <a href="Login.aspx" style="float:left;"><img src="Images/Logo.png" alt="Error" style="height:42px; width:100px; padding-left: 40px;" ></a>
            <div class="searchBarBlock">
                <asp:TextBox ID="TextBoxSearchBar" placeholder="Search.." runat="server" CssClass="serachBarInput"></asp:TextBox>
                <asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="buttonFashion1" OnClick="ButtonSearch_Click" />
                <asp:DropDownList ID="DropDownListSearchCategory" runat="server" CssClass="searchCat">
                    <asp:ListItem>Name</asp:ListItem>
                    <asp:ListItem>City</asp:ListItem>
                    <asp:ListItem>Friend of</asp:ListItem>
                </asp:DropDownList>
            </div>
            
            
                <ul>
                    <li class="menu_first"><a href ="UserProfile.aspx">Profile</a></li>
                    <li><a id="chatTab" runat="server" href="Chat.aspx">Chat</a></li>
                    <li><a href ="DateRequests.aspx">Date Requests</a></li>
                    <li class="menu_last"><a href="FriendRequests.aspx" id="testDepTxt" runat="server">Friend Requests</a></li>
                </ul>
                
        </div>
        <div class="clear"></div>
        <div class="content">
    <link href="CSS/MessageRoomStyle.css" rel="stylesheet" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            setInterval(function () { $('#<%=ButtonInv.ClientID %>').click(); }, 2000);
        });
    </script>
    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        // It is important to place this JavaScript code after ScriptManager1
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=messagesScreenID.ClientID%>') != null) {
              // Get X and Y positions of scrollbar before the partial postback
                xPos = $get('<%=messagesScreenID.ClientID%>').scrollLeft;
                yPos = $get('<%=messagesScreenID.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=messagesScreenID.ClientID%>') != null) {
             // Set X and Y positions back to the scrollbar
             // after partial postback
                $get('<%=messagesScreenID.ClientID%>').scrollLeft = xPos;
                $get('<%=messagesScreenID.ClientID%>').scrollTop = yPos;
            }
        }

        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
    </script>
    <div class ="allCont">
        <div class="titleBlock">
            <p>Chat</p>
        </div>
        <div class="friendProfile">
            <asp:Image ID="ImageProfilePicture" runat="server" CssClass="profImg" />
            <a class="friendName" href="ViewProfile.aspx?id=<%=friendUID %>"><%=friendName %></a>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
                <div id="messagesScreenID" runat="server" class="messagesScreen">

                </div>
                <asp:Button runat="server" Text="Button" ID="ButtonInv" OnClick="ButtonInv_Click" Style="display:none;"></asp:Button>
           </ContentTemplate>
        </asp:UpdatePanel>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="ButtonSendMsg">
        <div class="inputArea">
            <asp:TextBox ID="TextBoxInputMessage" runat="server" autocomplete="off" placeholder="Type a message.." CssClass="inputFashion"></asp:TextBox>
            <asp:Button ID="ButtonSendMsg" runat="server" Text="Send" OnClick="ButtonSendMsg_Click" CssClass="buttonFashionSend" />
        </div>
    </asp:Panel>
        <div runat="server" id="SuggestionDiv" class="suggestionMessageDiv">
            <p runat="server" id="suggestionText" class="suggestionFashion">Message Suggestion:</p>
        </div>

    </div>

</div>
        <div class="clear"></div>
        <div class="footer">
            <ul>
                <li class="footer_first" style="float:right;"><a href ="UserProfile.aspx" onclick="pressButtonLogout(); return false;">Logout</a></li>
                <li class="footer_last"><a href ="AccountSettings.aspx">Account Settings</a></li>
            </ul>
            <asp:Button ID="ButtonLogout" runat="server" Text="Log out" CssClass="auto-style1" OnClick="ButtonLogout_Click" Style="display:none;"/>
            
        </div>

    </div>
    </form>
</body>
</html>

