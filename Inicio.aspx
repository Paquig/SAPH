<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" CodeFile="Inicio.aspx.vb" Inherits="FWInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2"  runat="server">
   
    <div id="wrapper">
	<div id="splash">
		<p><span>Bienvenido!</span>&nbsp;&nbsp;</p>
        <p>Cree un calendario compartido con SAPH </p>
        <p>Un servicio sencillo y gratuito</p>
        <p>
            <asp:Button ID="BtnCrearHorario" runat="server" Font-Size="Large" Text="Crear horario" 
                Width="262px" Font-Bold="True" ForeColor="#006666" Height="46px" />
            <asp:Literal ID="lblJavaScript" runat="server" Text="" />
            
        </p>
	</div>
     <!--
	<div id="poptrox"> 
		<!-- start -->
        <!--paqui 
		<ul id="gallery">
			<li></li>
			<li></li>
			<li></li>
		</ul>
		<br class="clear" />
		<script type="text/javascript">
		    $('#gallery').poptrox();
		</script> 
		
         <!-- end 
        </div> -->
 </div> 

</asp:Content>
