Imports Microsoft.VisualBasic
Imports System.Data

Public Class Parametros

    Public Shared Function ReplaceParameters(ByVal param As String) As String
        Dim strRet As String
        strRet = param

        'If strRet.Contains("#APP_NOMBRE#") Then
        '    Dim appName As String = APLICACION_NOMBRE().Trim()
        '    strRet = strRet.Replace("#APP_NOMBRE#", appName)
        'End If

        'If strRet.Contains("#URL_WEB_APP#") Then
        '    Dim raiz As String = URL_WEB_APP().Trim()
        '    strRet = strRet.Replace("#URL_WEB_APP#", raiz)
        'End If

        If strRet.Contains("#PATH_WEBRAIZ#") Then
            Dim raiz As String = PATH_WEBRAIZ().Trim()
            strRet = strRet.Replace("#PATH_WEBRAIZ#", raiz)
        End If

        ' IMPORTANTE: No quitar este If o se mete en un bucle infinito al llamar el método BD_PATH_EADMIN_DATOSRAIZ().
        If strRet.Contains("#DATOSRAIZ#") Then
            Dim raiz As String = PATH_DATOSRAIZ().Trim()
            strRet = strRet.Replace("#DATOSRAIZ#", raiz)
        End If

        'If strRet.Contains("#LISTADOSFAC#") Then
        '    Dim tmp As String = BD_PATH_EADMIN_LISTADOSFAC().Trim()
        '    strRet = strRet.Replace("#LISTADOSFAC#", tmp)
        'End If

        'If strRet.Contains("#MAQUETA#") Then
        '    Dim maqueta As String = BD_PATH_MAQUETA().Trim()
        '    strRet = strRet.Replace("#MAQUETA#", maqueta)
        'End If

        'If strRet.Contains("#ENTIDAD#") Then
        '    strRet = strRet.Replace("#ENTIDAD#", ENTIDAD)
        'End If

        'If strRet.Contains("#CONTEXTO#") Then
        '    strRet = strRet.Replace("#CONTEXTO#", CONTEXTO)
        'End If


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

    'Public Shared Function URL_PROPUESTAHORARIO_UUID(ByVal idHorario As Integer) As String
    '    Dim cidhorariouuid As String = Util.GeneraGUID(idHorario.ToString)

    '    Return ReplaceParameters(ConfigurationManager.AppSettings("URL_PROPUESTAHORARIO").Replace("#IDHORARIO#", cidhorariouuid))
    'End Function

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

    'Public Shared Function URL_PROPUESTAHORARIO_ADMIN_UUID(ByVal idHorario As String) As String
    '    Dim cidhorariouuid As String = Util.GeneraGUID(idHorario.ToString)

    '    Return ReplaceParameters(ConfigurationManager.AppSettings("URL_PROPUESTAHORARIO_ADMIN").Replace("#IDHORARIO#", cidhorariouuid))
    'End Function
    Public Shared ReadOnly Property REPORTS_LOGO() As String
        Get
            Return ReplaceParameters(ConfigurationManager.AppSettings("URL_LOGO"))
        End Get
    End Property
End Class
