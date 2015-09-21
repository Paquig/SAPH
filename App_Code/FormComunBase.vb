Public Class FormComunBase
    Inherits System.Web.UI.Page

    ' Acceso de BD Saph 
    Public dSaph As DatosSaph = Nothing

#Region "ERROR_ALERT"
    '
    ' Mensajes de error y alerta
    '
    Protected cError As String
    Protected oException As Exception  ' La excepción que generó el error
    Protected cAlert As String

    Public Sub SetErrorException(ByVal errorValue As String, ByVal exception As Exception)
        SetError(errorValue)
        SetException(exception)
    End Sub

    Public Function GetError() As String
        Return cError
    End Function

    Public Sub SetError(ByVal value As String, Optional ByVal append As Boolean = False)
        If append Then
            cError += value
        Else
            cError = value
        End If
    End Sub

    Public Function GetAlert() As String
        Return cAlert
    End Function

    Public Function GetAlert_JS() As String
        ' GetAlert para javascript: Convierte las comillas dobles en simples
        ' o al generarse el código HTML no se mostrará el mensaje del alert().
        Return cAlert.Replace("""", "'").Replace("\", "\\").Replace(Util.HTML_SALTOLINEA, "\n")
    End Function

    Public Sub SetAlert(ByVal value As String, Optional ByVal append As Boolean = False)
        If append Then
            cAlert += value
        Else
            cAlert = value
        End If
    End Sub

    Public Function GetException() As Exception
        Return oException
    End Function

    Public Sub SetException(ByVal value As Exception)
        oException = value
    End Sub

    Public Sub MostrarError(ByVal ex As Exception, Optional ByRef ocontrol As WebControl = Nothing, Optional mensajeTipo As Util.MENSAJE_TIPO = Util.MENSAJE_TIPO.MT_INFO)

        SetAlert(ex.Message)
        Control_lblMensajeDatos.Text = ex.Message

        Select Case mensajeTipo
            Case Util.MENSAJE_TIPO.MT_ERROR
                Control_lblMensajeDatos.CssClass = "mensaje-error-dialog"
            Case Util.MENSAJE_TIPO.MT_WARNING
                Control_lblMensajeDatos.CssClass = "mensaje-warning-dialog"
            Case Util.MENSAJE_TIPO.MT_INFO
                Control_lblMensajeDatos.CssClass = "mensaje-info-dialog"
        End Select

        If Not ocontrol Is Nothing Then

            ' NOTA: Si está a True, parece que no lleva el cursor al campo en cuestión.
            Page.MaintainScrollPositionOnPostBack = False

            Page.SetFocus(ocontrol)
            ocontrol.ToolTip = ex.Message
        End If
    End Sub


    Public Function Control_lblMensajeDatos() As Label
        Dim o As Label = Util.FindControlRecursive(Master, "lblMensajeDatos")
        Return o
    End Function

    Public Sub ResetError()
        SetAlert("")

        Control_lblMensajeDatos.Visible = False
        Control_lblMensajeDatos.Text = ""

    End Sub

#End Region

    Protected Sub AsignarConexionBD()
        dSaph = New DatosSaph
    End Sub

    Public Sub Liberar()
        dSaph = Nothing
    End Sub


End Class

