Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Web.HttpRequest
Imports System.Data
Imports System.Data.OleDb
Imports System.ComponentModel
Imports System.Xml


Public Class Util

#Region "CONSTANTES"

    Public Const HTML_SALTOLINEA As String = "<br>"

    Public Enum MENSAJE_TIPO As Integer
        MT_ERROR = 1
        MT_WARNING = 2
        MT_INFO = 3
    End Enum


    Public Enum ACCION_HORARIO As Integer
        Añadir = 1
        Borrar = 2
        Modificar = 3
        AñadirRecurso = 4
        ModificarRecurso = 5
        BorrarRecurso = 6
        Invitar = 7
    End Enum

    Protected Shared ACCION_ARRAY_STRING() As String = {"Crear Horario", "Borrar Horario", "Modificar Horario", "Añadir Recurso", "Modficar Recurso", "Borrar Recurso", "Invitar"}

    Public Shared Function ACCIONVALORLETRA(ByVal op As ACCION_HORARIO) As String
        Return ACCION_ARRAY_STRING(op - 1)
    End Function

    Public Enum LINK_LLAMADOR As Integer
        PropuestaHorario = 1
        PropuestaAdmin = 2
    End Enum
#End Region

#Region "RUTINAS"

    Public Shared Function NombreFicheroAleatorio() As String
        Dim cnombrefichero As String = String.Empty

        Dim separador As String = "_"

        Dim id As Integer
        '--------------------------

        HttpContext.Current.Application.Lock()
        '--------------------------
        If HttpContext.Current.Application("ID_TEMP") Is Nothing Then
            HttpContext.Current.Application("ID_TEMP") = 0
        End If
        'Incrementamos
        HttpContext.Current.Application("ID_TEMP") += 1
        'No permitimos mas de 300
        id = HttpContext.Current.Application("ID_TEMP") Mod 300
        '--------------------------
        HttpContext.Current.Application.UnLock()
        '--------------------------
        cnombrefichero += separador

        cnombrefichero += id.ToString.Trim.PadLeft(3, "0")
        '
        Return cnombrefichero
    End Function

    Public Shared Function EsVacio(str As String) As Boolean
        If String.IsNullOrEmpty(str) Then
            Return True
        Else
            ' Sin el If anterior, si "str" es Nothing, str.Trim() daría error.
            Return (String.IsNullOrEmpty(str.Trim()))
        End If
    End Function

    Public Shared Sub AbrirPagina(ByRef cURl As String, ByRef clblJavaScript As Object)
        If Not EsVacio(cURl) Then
            Dim cmd As String
            cmd = "AbrirVentana_Saph('" + cURl + "')"
            clblJavaScript.Text = "<script type='text/javascript'>" + cmd + "</script>"
        End If
    End Sub


    Public Shared Function FindControlRecursive(ByVal root As System.Web.UI.Control, ByVal id As String) As System.Web.UI.Control
        ' Ejemplo:  Dim mitxtClave As Control = FindControlRecursive(Master, "txtClave")
        If (root.ID = id) Then
            Return root
        End If

        Dim foundCtl As System.Web.UI.Control = Nothing
        For Each ctl As System.Web.UI.Control In root.Controls

            foundCtl = FindControlRecursive(ctl, id)

            If (Not foundCtl Is Nothing) Then
                Return foundCtl
            End If
        Next

        Return Nothing
    End Function

#End Region
#Region "DBCOMMAND"
    Public Shared Sub AñadirParametro(ByRef cmd As OleDbCommand, ByVal parameter As String, Optional ByVal paramType As OleDbType = OleDbType.Date)

        Dim op As OleDbParameter = New OleDbParameter(parameter, paramType)
        op.Direction = ParameterDirection.Input
        op.OleDbType = paramType
        op.Value = parameter
        cmd.Parameters.Add(op)

    End Sub


    Public Shared Sub OleDbCommandSet(ByRef cmd As OleDbCommand, ByVal cmdText As String, ByVal ParamArray parameters() As Object)

        cmd.CommandText = cmdText

        Dim parameter As Object

        For i As Integer = 0 To parameters.Length() - 1
            parameter = parameters(i)

            If TypeOf (parameter) Is String Then
                AñadirParametro(cmd, parameter, OleDbType.Char)
            ElseIf TypeOf (parameter) Is Integer Then
                AñadirParametro(cmd, parameter, OleDbType.Integer)
            ElseIf TypeOf (parameter) Is Date Then
                AñadirParametro(cmd, parameter, OleDbType.Date)
            ElseIf TypeOf (parameter) Is DateTime Then
                AñadirParametro(cmd, parameter, OleDbType.Date)
            ElseIf TypeOf (parameter) Is Decimal Then  ' Importes con decimales
                AñadirParametro(cmd, parameter, OleDbType.Currency)
            ElseIf TypeOf (parameter) Is Boolean Then
                AñadirParametro(cmd, parameter, OleDbType.Boolean)
            ElseIf TypeOf (parameter) Is Long Then
                AñadirParametro(cmd, parameter, OleDbType.Numeric)
            Else
                Throw New Exception("OledbCommandSet:tipo parámetro no contemplado [NumParametro=" + (i + 1).ToString() + " Tipo=" + parameter.GetType().ToString() + " Valor=" + parameter.ToString() + "] ")
            End If
        Next

    End Sub
#End Region
#Region "SQL"
    Public Shared Function SQL2String(ByRef cmdText As String, ByVal ParamArray parameters() As Object) As String
        Dim parameters_Str As StringBuilder = New StringBuilder()
        For i As Integer = 0 To parameters.Length() - 1
            If parameters(i) IsNot Nothing Then
                parameters_Str.Append("[" + parameters(i).ToString() + "]" + vbCrLf)
            Else
                parameters_Str.Append("[NOTHING]" + vbCrLf)
            End If
        Next
        Return ">>>>>>>>>>>>>" + vbCrLf + ">>>>Sentencia SQL:" + cmdText + vbCrLf + "PARAMETERS:" + vbCrLf + parameters_Str.ToString() + ">>>>>>>>>>>>>" + vbCrLf
    End Function
#End Region

#Region "QUERYSTRING"

    Public Const QUERYSTRING_IDHORARIO As String = "idhorario"
    Public Const QUERYSTRING_IDRECURSO As String = "idrecurso"

    Public Shared Function ComponerParametrosUrl(ByVal url As String, Optional ByVal parametros As String = "") As String
        Dim sbuf As StringBuilder = New StringBuilder()
        sbuf.Append(url).Append("?")

        If Not EsVacio(parametros) Then
            sbuf.Append(parametros)
            If Not parametros.EndsWith("&") Then
                sbuf.Append("&")
            End If
        End If

        Return sbuf.ToString()
    End Function

    Public Shared Function QueryString_PonerParametro(ByVal qryClave As String, ByVal qryValor As String) As String
        Dim sbuf As StringBuilder = New StringBuilder()
        sbuf.Append(qryClave).Append("=").Append(qryValor).Append("&")
        Return sbuf.ToString()
    End Function

    Public Shared Function QueryString_ObtenerParametro(ByVal request As HttpRequest, ByVal parametro As String, ByVal obligatorio As Boolean, Optional ByVal valorSiNoExiste As Object = Nothing) As String
        Dim param_Str As String

        param_Str = request(parametro)
        If param_Str Is Nothing Then
            If obligatorio = True Then
                Throw New Exception("No se ha indicado " + parametro)
            Else
                param_Str = valorSiNoExiste
            End If
        End If

        Return param_Str
    End Function

    Public Shared Function QueryString_ObtenerIdHorario(ByVal request As HttpRequest) As String
        Return QueryString_ObtenerParametro(request, QUERYSTRING_IDHORARIO, False, Nothing)
    End Function

    Public Shared Function QueryString_ObtenerIdHorario_uuid(ByVal request As HttpRequest) As String
        Dim cidHorarioUuid As String = QueryString_ObtenerParametro(request, QUERYSTRING_IDHORARIO, False, Nothing)
        Return cidHorarioUuid
    End Function

    Public Shared Function QueryString_ObtenerIdRecurso(ByVal request As HttpRequest) As String
        Return QueryString_ObtenerParametro(request, QUERYSTRING_IDRECURSO, False, Nothing)
    End Function
#End Region

#Region "MODAL"
    Public Shared Sub Close(ByVal page As Page)
        Close(page, Nothing)
    End Sub

    Public Shared Sub Close(ByVal page As Page, ByVal result As Object)
        page.Response.Clear()
        page.Response.ContentType = "text/html"
        page.Response.Buffer = True

        Dim sb As New StringBuilder()
        sb.Append("<html>")
        sb.Append("<head>")
        sb.Append("<script type='text/javascript'>")
        sb.Append("if (parent) {")
        sb.Append("parent.NuevoRecurso();")
        sb.Append("parent.DayPilot.ModalStatic.close(" & DayPilot.Web.Ui.Json.SimpleJsonSerializer.Serialize(result) & ");")
        sb.Append("}")
        sb.Append("</script>")
        sb.Append("</head>")
        sb.Append("</html>")

        Dim output As String = sb.ToString()

        Dim s() As Byte = Encoding.UTF8.GetBytes(output)
        page.Response.AddHeader("Content-Length", s.Length.ToString())

        page.Response.Write(output)

        page.Response.Flush()
        page.Response.Close()

    End Sub

#End Region
#Region "RUTINA HORAS"
    Public Shared Function ObtenHoras(ByVal start As TimeSpan) As Integer
        Return start.Hours
    End Function
#End Region
#Region "ENVIARCORREO"
    Public Shared Sub EnviarMail(tcRemitente As String, tcDestinatarios As String, tcAsunto As String, tcCuerpo As String, tcAdjuntos As String, Optional lEnviarMails As Boolean = True)
        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress(tcRemitente, "Servicio SAPH")

        For Each cmail As String In tcDestinatarios.Split(";")
            correo.To.Add(cmail)
        Next

        correo.Subject = tcAsunto
        tcCuerpo &= vbCrLf & vbCrLf & _
                        "Fecha y hora GMT: " & _
                        DateTime.Now.ToUniversalTime.ToString("dd/MM/yyyy HH:mm:ss")
        correo.Body = tcCuerpo
        correo.IsBodyHtml = True
        correo.Priority = System.Net.Mail.MailPriority.Normal

        Dim plainView As Net.Mail.AlternateView = Net.Mail.AlternateView.CreateAlternateViewFromString(tcCuerpo, Nothing, "text/plain")

        Dim htmlView As Net.Mail.AlternateView = Net.Mail.AlternateView.CreateAlternateViewFromString(tcCuerpo + "<img src=cid:logosaph>", Nothing, "text/html")


        Dim logo As New Net.Mail.LinkedResource(tcAdjuntos)
        logo.ContentId = "logosaph"

        htmlView.LinkedResources.Add(logo)

        correo.AlternateViews.Add(plainView)
        correo.AlternateViews.Add(htmlView)


        Dim smtp As New System.Net.Mail.SmtpClient
        '
        '---------------------------------------------
        ' Estos datos  rellanarlos correctamente
        '---------------------------------------------
        'los detalles SMTP de Windows Live / Hotmail:
        'Nombre de servidor SMTP de Hotmail: smtp.live.com
        'Nombre de usuario SMTP de Hotmail: su cuenta de Hotmail
        'Contraseña de SMTP de Hotmail: su contraseña de Hotmail
        'Puerto SMTP de Hotmail: 25 o 465

        smtp.Host = "smtp.live.com"
        smtp.Credentials = New System.Net.NetworkCredential("saphservicio@hotmail.com", "saph-servicio")
        smtp.EnableSsl = True

        smtp.Send(correo)

    End Sub

    Public Shared Function EsEmailValido(ByVal email As String) As Boolean
        Return Regex.IsMatch(email, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

    End Function

#End Region

#Region "UUID"
    Public Shared Function GeneraGUID() As String

        Dim sGUID As String = ""
        sGUID = System.Guid.NewGuid.ToString("N")
        'Lo genero en formato de 32 digitos seguidos, sin guiones
        Return sGUID

    End Function


#End Region
#Region "REPORTS"

    Public Shared Sub ReportGenerarEnNavegador(ByVal rutaPlantilla As String, datos As DataTable, ByVal parameters As String(,), ByVal nombreFichero As String, ByRef pResponse As HttpResponse)
        Dim r As New Telerik.Reporting.Report()

        Dim settings As New XmlReaderSettings()
        settings.IgnoreWhitespace = True

        Using xmlReader As System.Xml.XmlReader = System.Xml.XmlReader.Create(rutaPlantilla, settings)
            Dim xmlSerializer As New Telerik.Reporting.XmlSerialization.ReportXmlSerializer()
            r = DirectCast(xmlSerializer.Deserialize(xmlReader), Telerik.Reporting.Report)
        End Using

        ' Parámetros globales
        For i As Integer = 0 To parameters.GetUpperBound(0) - 1
            r.ReportParameters(parameters(i, 0)).Value = parameters(i, 1)
        Next

        ' Datos sobre los que iterar en la sección detalle del informe
        r.DataSource = datos

        Dim rp As New Telerik.Reporting.Processing.ReportProcessor()
        Dim rpi As New Telerik.Reporting.InstanceReportSource()
        rpi.ReportDocument = r

        Dim rr As Telerik.Reporting.Processing.RenderingResult
        rr = rp.RenderReport("PDF", rpi, Nothing)

        If rr.HasErrors Then
            Dim mensajes As String = ""
            For Each ex As Exception In rr.Errors
                mensajes += ex.Message + "<br />"
            Next
            Throw New Exception("Error generando el informe: " + mensajes)
        End If

        ' Para generarlo a PDF: System.IO.File.WriteAllBytes(rutaPDF, rr.DocumentBytes)

        ' Para generarlo directamente al browser
        Dim mimeType As String = String.Empty
        Dim reportBytes As Byte() = rr.DocumentBytes()
        ' inject the bytes to the Response stream
        Dim fileName As String = nombreFichero
        pResponse.Clear()
        pResponse.ContentType = mimeType
        pResponse.Cache.SetCacheability(HttpCacheability.Private)
        pResponse.Expires = -1
        pResponse.Buffer = False
        pResponse.AddHeader("Content-Disposition", String.Format("{0};FileName=""{1}""", "attachment", fileName))
        pResponse.OutputStream.Write(reportBytes, 0, reportBytes.Length)
        pResponse.End()
    End Sub

    Public Shared Sub ReportGenerarEnPDF(ByVal rutaPlantilla As String, datos As DataTable, ByVal parameters As String(,), ByVal rutaPDF As String)
        Dim r As New Telerik.Reporting.Report()

        Dim settings As New XmlReaderSettings()
        settings.IgnoreWhitespace = True

        Using xmlReader As System.Xml.XmlReader = System.Xml.XmlReader.Create(rutaPlantilla, settings)
            Dim xmlSerializer As New Telerik.Reporting.XmlSerialization.ReportXmlSerializer()
            r = DirectCast(xmlSerializer.Deserialize(xmlReader), Telerik.Reporting.Report)
        End Using

        ' Parámetros globales
        For i As Integer = 0 To parameters.GetUpperBound(0) - 1
            r.ReportParameters(parameters(i, 0)).Value = parameters(i, 1)
        Next

        ' Datos sobre los que iterar en la sección detalle del informe
        r.DataSource = datos

        Dim rp As New Telerik.Reporting.Processing.ReportProcessor()
        Dim rpi As New Telerik.Reporting.InstanceReportSource()
        rpi.ReportDocument = r

        Dim rr As Telerik.Reporting.Processing.RenderingResult
        rr = rp.RenderReport("PDF", rpi, Nothing)

        If rr.HasErrors Then
            Dim mensajes As String = ""
            For Each ex As Exception In rr.Errors
                mensajes += ex.Message + "<br />"
            Next
            Throw New Exception("Error generando el informe: " + mensajes)
        End If

        ' Crea un archivo nuevo, escribe en él la matriz de bytes especificada y, a continuación, lo cierra. 
        ' Si el archivo de destino ya existe, se sobrescribe
        System.IO.File.WriteAllBytes(rutaPDF, rr.DocumentBytes)

    End Sub

    Public Shared Sub ReportGenerarEnExcellNavegador(ByVal rutaPlantilla As String, datos As DataTable, ByVal parameters As String(,), ByVal rutaExcel As String, ByRef pResponse As HttpResponse)

        Dim r As New Telerik.Reporting.Report()

        Dim settings As New XmlReaderSettings()
        settings.IgnoreWhitespace = True

        Using xmlReader As System.Xml.XmlReader = System.Xml.XmlReader.Create(rutaPlantilla, settings)
            Dim xmlSerializer As New Telerik.Reporting.XmlSerialization.ReportXmlSerializer()
            r = DirectCast(xmlSerializer.Deserialize(xmlReader), Telerik.Reporting.Report)
        End Using

        ' Parámetros globales
        For i As Integer = 0 To parameters.GetUpperBound(0) - 1
            r.ReportParameters(parameters(i, 0)).Value = parameters(i, 1)
        Next

        ' Datos sobre los que iterar en la sección detalle del informe
        r.DataSource = datos

        Dim rp As New Telerik.Reporting.Processing.ReportProcessor()
        Dim rpi As New Telerik.Reporting.InstanceReportSource()
        rpi.ReportDocument = r

        Dim rr As Telerik.Reporting.Processing.RenderingResult
        rr = rp.RenderReport("XLS", rpi, Nothing)

        If rr.HasErrors Then
            Dim mensajes As String = ""
            For Each ex As Exception In rr.Errors
                mensajes += ex.Message + "<br />"
            Next
            Throw New Exception("Error generando el informe: " + mensajes)
        End If

        ' Crea un archivo nuevo, escribe en él la matriz de bytes especificada y, a continuación, lo cierra. 
        ' Si el archivo de destino ya existe, se sobrescribe
        'System.IO.File.WriteAllBytes(rutaExcel, rr.DocumentBytes)

        ' Para generarlo directamente al browser
        Dim mimeType As String = String.Empty
        Dim reportBytes As Byte() = rr.DocumentBytes()
        ' inject the bytes to the Response stream
        Dim fileName As String = rutaExcel
        pResponse.Clear()
        pResponse.ContentType = mimeType
        pResponse.Cache.SetCacheability(HttpCacheability.Private)
        pResponse.Expires = -1
        pResponse.Buffer = False
        pResponse.AddHeader("Content-Disposition", String.Format("{0};FileName=""{1}""", "attachment", fileName))
        pResponse.OutputStream.Write(reportBytes, 0, reportBytes.Length)
        pResponse.End()

    End Sub

#End Region
End Class
