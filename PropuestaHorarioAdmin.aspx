<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" Inherits="FWPropuestaHorarioAdmin" CodeFile="PropuestaHorarioAdmin.aspx.vb" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   
<script src="Scripts/DayPilot/modal.js" type="text/javascript"></script>
<script src="Scripts/DayPilot/event_handling.js" type="text/javascript"></script>
 <script type="text/javascript">
         var idhorario = <%=Id_Horario %>;
         var paquivar = "hola";
      //   alert(idhorario);
     </script>
      <script type="text/javascript">
          function NuevoRecurso() {
              // alert(' Actualiza nuevo recurso');
              document.getElementById('<% =Util.FindControlRecursive(Master, "form1").ClientID %>').submit();
          }
    </script>

 <div id="menu2"><h2>Administración</h2>
		 <asp:Table ID="Table1" runat="server" Width="884px">
           <asp:TableRow BorderStyle="None">
            <asp:TableCell BorderStyle="None">
           
            <li><asp:LinkButton ID="LinkBtonEditar" runat="server">Editar Calendario</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkBtonHistoria" runat="server">Historia</asp:LinkButton> </li>
            <li><asp:LinkButton ID="LinkBtonInvitar" runat="server">Invitar</asp:LinkButton> </li>
           	
            </asp:TableCell>
            <asp:TableCell BorderStyle="None">
           
            <li><asp:LinkButton ID="LinkBtonImprimir" runat="server">Imprimir</asp:LinkButton> </li>
            <li><asp:LinkButton ID="LinkBtonExcel" runat="server">Exportar a Excell</asp:LinkButton> </li>
		<!--	<li><asp:LinkButton ID="LinkBtonPDF" runat="server">Exportar a PDF</asp:LinkButton> </li> -->
            
       	    </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
	</div>

<asp:Label ID="Lbltitulo" runat="server" Text="Calendario Compartido "  CssClass="label" Width="500"></asp:Label>

<br />

<DayPilot:DayPilotCalendar ID="DayPilotCalendar" runat="server" Days="7" 
        HeaderDateFormat="dddd" StartDate="2015-06-08" 
        ToolTip='Pincha en una casilla para añadir tu elección' BackColor="#FFFFD5" 
        BorderColor="#000000" CssClassPrefix="calendar_default" 
        
        EventClickHandling="JavaScript"  
        EventClickJavaScript="edit(e)"  
        TimeRangeSelectedHandling="JavaScript"
		TimeRangeSelectedJavaScript="create(start, end, resource, idhorario);"

        DataEndField="tEndTime" DataStartField="tStarttime" DataTextField="cDesRecurso" 
        DataValueField="IdRecurso"
       
        DayFontFamily="Tahoma" DayFontSize="10pt" 
        DurationBarColor="Blue" EventBackColor="#FFFFFF" EventBorderColor="#000000" 
        EventFontFamily="Tahoma" EventFontSize="8pt" EventHoverColor="#DCDCDC" 

        HeaderHeight="25" HourBorderColor="#EAD098" HourFontFamily="Tahoma" 
        HourFontSize="16pt" HourHalfBorderColor="#F3E4B1" HourNameBackColor="#ECE9D8" 
        HourNameBorderColor="#ACA899" HoverColor="#FFED95" 
        NonBusinessBackColor="#FFF4BC" ScrollPositionHour="9" 
        ClientObjectName="dp"
        EventMoveHandling="CallBack" 
        EventResizeHandling="CallBack"
        OnEventMove="DayPilotCalendar_EventMove" 
		OnEventResize="DayPilotCalendar_EventResize" 
        />
         <br />
   
</asp:Content>


