<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Nuevo.aspx.vb" Inherits="Nuevo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellspacing="4" cellpadding="0">
			<tr>
				<td align="right" valign="top"></td>
				<td class="style1" style= "color:Red" ><h1>Nuevo Recurso</h1></td>
			</tr>
			<tr>
				<td align="right">Día:</td>
				<td class="style1"><asp:TextBox ID="TxtDay" runat="server" Enabled="false"> </asp:TextBox></td>
			</tr>
			<tr>
				<td align="right">Inicio:</td>
				<td class="style1"><asp:TextBox ID="TxtInicio" runat="server" Enabled="false"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right">Duración(minutos):</td>
				<td class="style1">
				<asp:DropDownList ID="DropDownListDuration" runat="server">
				<asp:ListItem>30</asp:ListItem>
				<asp:ListItem>60</asp:ListItem>
				<asp:ListItem>120</asp:ListItem>
				</asp:DropDownList>
				</td>
			</tr>
			
			<tr>
				<td align="right">Descripción:</td>
				<td class="style1"><asp:TextBox ID="TxtDescripcion" runat="server" Width="269px"></asp:TextBox></td>
</tr>
<tr>
                <td align="right" class="label" >Nombre Usuario:</td>
				<td class="style1"><asp:TextBox ID="TxtNombreUsuario" runat="server" Width="150px"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right"></td>
				<td class="style1">
					<asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="OK" />
					<asp:Button ID="ButtonCancel" runat="server" Text="Cancelar" OnClick="ButtonCancel_Click" />
				</td>
			</tr>
		</table>

		</div>
	</form>

		<script type="text/javascript">
		    document.getElementById("TxtDescripcion").focus();
		</script>

   </body>
</html>
