Imports System.Data
Partial Class FWPaginaFinal
    Inherits FormComunBase

    Private idHorario As Integer = -1
    Private cuuid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '            idHorario = Convert.ToInt32(Util.QueryString_ObtenerIdHorario(Request))
            '           AsignarConexionBD()
            cuuid = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            ' si viene del link del correo...averiguamos el idhorario de la tabla recursos
            AsignarConexionBD()
            Dim dr As DataRow
            If cuuid.Length > 5 Then
                dr = dSaph.Get_Horario_UUid(cuuid)
                idHorario = dr("idHorario")

            Else
                idHorario = Convert.ToInt32(cuuid)
                dr = dSaph.Get_Horario(idHorario)
            End If

            HLUsuarios.Text = Parametros.URL_WEB_RAIZ + Parametros.URL_PROPUESTAHORARIO(dr("cuuid"))

            HLAdministrador.Text = Parametros.URL_WEB_RAIZ + Parametros.URL_PROPUESTAHORARIO_ADMIN(dr("cuuid"))

            '   Lbltitulo.Text = Util.QUERYSTR
        Catch ex As Exception
            MostrarError(ex)

        End Try
    End Sub

    Protected Sub BtnAdministrar_Click(sender As Object, e As System.EventArgs) Handles BtnAdministrar.Click
        Response.Redirect(HLAdministrador.Text)
    End Sub
End Class