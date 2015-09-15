<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" CodeFile="EnviarInvitacion.aspx.vb" Inherits="FWEnviarInvitacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="splash">
	<p><span>INVITAR A PARTICIPAR</span></p>
</div>
<br />
<asp:Label ID="Lbltitulo" runat="server" 
        Text="Por favor, escriba las direcciones de correo electrónico separadas por ;" 
        CssClass="label" Width="662px"></asp:Label>
<br />
    <asp:TextBox ID="TxtDestinatarios" runat="server" Width="758px" 
    Height="25px"></asp:TextBox>
<br />
<br />
<asp:Label ID="Lblmensaje" runat="server" Text="Mensaje" CssClass="label" Width="100"></asp:Label>
<br />
<asp:TextBox ID="TxtMensaje"  TextMode="MultiLine" runat="server" Width="767px" 
        Height="84px"></asp:TextBox>
<br />
<br />
<br />
<br />
<br />

<asp:Button ID="Btnvolver" runat="server" Text="Volver" CssClass="boton-accion" />
<asp:Button ID="BtnFinaliza" runat="server" Text="Finalizar" CssClass="boton-accion" />
<br />

</asp:Content>
 