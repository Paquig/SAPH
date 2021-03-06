﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Saph.Master" Inherits="FWPropuestaHorario" Culture="es-ES" uiCulture="es-ES" Codefile="PropuestaHorario.aspx.vb" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<script src="Scripts/DayPilot/modal.js" type="text/javascript"></script>
<script src="Scripts/DayPilot/event_handling.js" type="text/javascript"></script>
 <script type="text/javascript">
     function NuevoRecurso() {
         // alert(' Actualiza nuevo recurso');
         document.getElementById('<% =Util.FindControlRecursive(Master, "form1").ClientID %>').submit();
     }
    </script>
<div id="splash">
	<p><span>calendario propuesto</span><br />
    <asp:Label ID="LblTexto" runat="server" Text="" ></asp:Label>
    </p>
 </div>
<asp:Label ID="LblTitulo" runat="server" Text="Por favor, marque su elección en el siguiente calendario" CssClass="label" Width="500"></asp:Label>
<br />


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
		OnEventResize="DayPilotCalendar_EventResize" TimeFormat="Clock24Hours"
        />
    <br />
    <asp:Button ID="BtnSiguiente" runat="server" Text="Compartir" CssClass="boton-accion" />
    <br />
    <script type="text/javascript">
        // Necesito pasar el idhorario en el evento en JavaScript de Create
         var idhorario = <%=Id_Horario %>;
    </script>
</asp:Content>


