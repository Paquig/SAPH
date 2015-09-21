Imports Microsoft.VisualBasic
Imports System.Data

Public Class Parametros

    Public Shared Function ReplaceParameters(ByVal param As String) As String
        Dim strRet As String
        strRet = param

        If strRet.Contains("#PATH_WEBRAIZ#") Then
            Dim raiz As String = PATH_WEBRAIZ().Trim()
            strRet = strRet.Replace("#PATH_WEBRAIZ#", raiz)
        End If

        If strRet.Contains("#DATOSRAIZ#") Then
            Dim raiz As String = PATH_DATOSRAIZ().Trim()
            strRet = strRet.Replace("#DATOSRAIZ#", raiz)
        End If

        If strRet.Contains("#PATH_BD_SAPH#") Then
            Dim path As String = PATH_BD_SAPH().Trim()
            strRet = strRet.Replace("#PATH_BD_SAPH#", path)
        End If

        Return strRet
    End Function

    Public Shared ReadOnly Property PATH_BD_SAPH() As String
        Get
            Return ReplaceParameters(ConfigurationManager.AppSettings("PATH_BD_SAPH"))
        End Get
    End Property


    Public Shared ReadOnly Property CS_SAPH() As String
        Get
            Return ReplaceParameters(ConfigurationManager.ConnectionStrings("CS_SAPH").ConnectionString)
        End Get
    End Property

    Public Shared ReadOnly Property PATH_DATOSRAIZ() As String
        Get
            Return ReplaceParameters(ConfigurationManager.AppSettings("PATH_DATOSRAIZ"))
        End Get
    End Property

    Public Shared ReadOnly Property PATH_WEBRAIZ() As String
        Get
            Return ReplaceParameters(ConfigurationManager.AppSettings("PATH_WEBRAIZ"))
        End Get
    End Property

    Public Shared ReadOnly Property URL_WEB_RAIZ() As String
        Get
            Return ReplaceParameters(ConfigurationManager.AppSettings("URL_WEB_RAIZ"))
        End Get
    End Property

    Public Shared Function URL_PROPUESTAHORARIO(ByVal idHorario As String) As String

        Return ReplaceParameters(ConfigurationManager.AppSettings("URL_PROPUESTAHORARIO").Replace("#IDHORARIO#", idHorario))
    End Function

    Public Shared Function URL_ENVIARINVITACION(ByVal idHorario As String) As String
        Return ReplaceParameters(ConfigurationManager.AppSettings("URL_ENVIARINVITACION").Replace("#IDHORARIO#", idHorario))
    End Function

    Public Shared Function URL_PAGINAFINAL(ByVal idHorario As String) As String
        Return ReplaceParameters(ConfigurationManager.AppSettings("URL_PAGINAFINAL").Replace("#IDHORARIO#", idHorario))
    End Function

    Public Shared Function URL_EDITARHORARIO(ByVal idHorario As String) As String
        Return ReplaceParameters(ConfigurationManager.AppSettings("URL_EDITARHORARIO").Replace("#IDHORARIO#", idHorario))
    End Function

    Public Shared Function URL_HISTORIAHORARIO(ByVal idHorario As String) As String
        Return ReplaceParameters(ConfigurationManager.AppSettings("URL_HISTORIAHORARIO").Replace("#IDHORARIO#", idHorario))
    End Function

    Public Shared Function URL_PROPUESTAHORARIO_ADMIN(ByVal idHorario As String) As String
        Return ReplaceParameters(ConfigurationManager.AppSettings("URL_PROPUESTAHORARIO_ADMIN").Replace("#IDHORARIO#", idHorario))
    End Function

    Public Shared ReadOnly Property REPORTS_LOGO() As String
        Get
            Return ReplaceParameters(ConfigurationManager.AppSettings("URL_LOGO"))
        End Get
    End Property
End Class
