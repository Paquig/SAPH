<%@Page Language="vb" AutoEventWireup="false" CodeFile="Editar.aspx.vb" Inherits="Editar"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edición de un Recurso del horario </title>
</head>
<body> 
    <form id="form1" runat="server" style="background-image:none">
    <div>
    <table border="0" cellspacing="4" cellpadding="0" style="width: 403px";>
			<tr>
				<td align="right" valign="top"></td>
				<td><h1 class="label" style= "color:Red" lang="es">Edición</h1></td>
			</tr>
			<tr>
				<td align="right" valign="top">Inicio:</td>
				<td><asp:Label ID="LabelStart" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td align="right" valign="top">Fin:</td>
				<td><asp:Label ID="LabelEnd" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td align="right" valign="top">Usuario:</td>
			<td><asp:TextBox ID="TxtUsuario" runat="server" Width="269px"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right" valign="top">Descripción:</td>
				<td><asp:TextBox ID="TxtDescripcion" runat="server" Width="269px"></asp:TextBox></td>
			</tr>
			<tr>
				<td></td>
				<td><asp:LinkButton ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" Text="Borrar Recurso" /></td>
			</tr>
			<tr>
				<td align="right"></td>
				<td>
					<asp:HiddenField ID="Recurrence" runat="server" />
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
