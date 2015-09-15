<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" CodeFile="HistoriaHorario.aspx.vb" Inherits="FwHistoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="splash"><p><span>Historia Calendario</span></p>
    </div>
    <br />
<asp:Panel ID="pnlHistoria" runat="server" Visible="true">
    <asp:GridView ID="GrdHistoria" runat="server" ShowHeader="True" 
        AutoGenerateColumns="False"   Width="771px">
        <Columns>
            <asp:TemplateField HeaderText="Fecha" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center" Visible="true">
                <ItemTemplate>
                      <asp:Label ID="txtFecha" runat="server" Text='<%# Eval("tfecaccion").ToString().Trim() %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="txtTipo" runat="server" Text='<%# Util.AccionValorLetra(Eval("ipkaccion").ToString().Trim()) %>' />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:Label ID="lblTipo" Text="Tipo Acción" runat="server" /><br />
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre Usuario" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="txtUsuario" runat="server" Text='<%# Eval("cNomUsuario").ToString().Trim() %>' />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:Label ID="lblusuario" Text="Usuario" runat="server" /><br />
                </HeaderTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Recurso" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="txtRecurso" runat="server" Text='<%# Eval("cDesRecurso").ToString().Trim() %>' />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:Label ID="lblRecurso" Text="Recurso" runat="server" /><br />
                </HeaderTemplate>
            </asp:TemplateField>
                      </Columns>
    </asp:GridView>
    <asp:Label ID="GrdHistoria_lblMensaje" CssClass="mensaje" Text="" runat="server" Visible="false" />
    <br />
</asp:Panel>

<asp:Button ID="Btnvolver" runat="server" Text="Volver" CssClass="boton-accion" />

</asp:Content>
