<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" CodeFile="EditarHorario.aspx.vb" Inherits="FWEditarHorario" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="splash"><p><span>Editar Calendario</span></p>
    </div>
    <br />
 
    <asp:Label ID="LblTitulo" runat="server" Text="Título :" AssociatedControlID="TxtTitulo" CssClass="label"> </asp:Label>
    <asp:TextBox ID="TxtTitulo" runat="server" Style="width: 300px;"></asp:TextBox>
    <br />
    <br />

    <asp:Label ID="LblSubtitulo" runat="server" Text="Subtitulo :" AssociatedControlID="TxtSubtitulo" CssClass="label" > </asp:Label>
    <asp:TextBox ID="TxtSubtitulo" runat="server" Style="width: 300px;"></asp:TextBox>
    <br />
    <br />

    <asp:Label ID="LblNomUsuario" runat="server" Text="Nombre Usuario :" AssociatedControlID="TxtNomUsuario" CssClass="label" > </asp:Label>
    <asp:TextBox ID="TxtNomUsuario" runat="server" Width="248px" tooltip="Personaliza el trato"></asp:TextBox>
    <br />
    <br />

    <asp:Label ID="LblCorreoElectronico" runat="server" Text="Correo Electrónico :" AssociatedControlID="TxtCorreoElectronico" CssClass="label"> </asp:Label>
    <asp:TextBox ID="TxtCorreoElectronico" runat="server" Width="300px" tooltip="Recibirá la URL para administrar el Calendario en esta dirección.No supone ningún tipo de registro"></asp:TextBox>
    <br />

	<br />
    <br />
    <asp:Button ID="Btnvolver" runat="server" Text="Volver" CssClass="boton-accion" />
    <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="boton-accion" />
    <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="boton-accion" />
    <br />
    <br />
</asp:Content>

