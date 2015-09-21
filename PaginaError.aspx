<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" CodeFile="PaginaError.aspx.vb" Inherits="Fw_ERROR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p>  &nbsp; &nbsp;<asp:Panel ID="Panel1" Width="874px" BorderWidth="2px" 
        BorderColor="Red"  Height="350px" runat="server">
 <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/mensaje-info.png" 
            ImageAlign="Middle" Height="61px" Width="76px" />
        &nbsp;<br />
        <br />
        <br />
        <asp:Label runat="server" ID="Lbl1" Text=" La página requerida no ha sido encontrada "
                     CssClass="label" Width="564px" Style="margin-left:100px" />
        
        <br />
        <br />
        <asp:Label runat="server" ID="Label1" Text=" Por favor revise la URL "
                     CssClass="label" Width="564px" Style="margin-left:100px" />
         <br />
         <br />
        <asp:Label runat="server" ID="Label2" Text=" Puede ser que el administrador haya eliminado el calendario compartido al que corresponde "
                     CssClass="label" Width="700px" Style="margin-left:100px" />
        
        <br />
         </asp:Panel>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
