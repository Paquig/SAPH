<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" Inherits="FWPaginaFinal" CodeFile="PaginaFinal.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="splash">
	<p><span>Gracias por utilizar SAPH</span></p>
</div>
<br />
<asp:Label ID="Lbltitulo" runat="server" Text="Su propuesta de horario ha sido enviada" CssClass="label" Width="500"></asp:Label>
<br />
<asp:Label ID="Lblusuarios" runat="server" Text="Enlace enviado de participación:" CssClass="label" Width="400" ></asp:Label>
<br />
<asp:HyperLink ID="HLUsuarios" runat="server">Enlace enviado de participación</asp:HyperLink>
<br />
<asp:Label ID="Lbladmin" runat="server" Text="Enlace enviado para el administrador:" CssClass="label" Width="400" ></asp:Label>
<br />
<asp:HyperLink ID="HLAdministrador" runat="server">Enlace enviado para el administrador</asp:HyperLink>
<br />
<asp:Button ID="BtnAdministrar" runat="server" Text="Administrar Calendario" />

</asp:Content>
