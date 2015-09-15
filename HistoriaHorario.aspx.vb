Imports System.Data
Partial Class FwHistoria
    Inherits FormComunBase
    Private idhorario As Integer = 1
    Private cuuid As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '  idhorario = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            'AsignarConexionBD()

            cuuid = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            ' si viene del link del correo...averiguamos el idhorario de la tabla recursos
            AsignarConexionBD()
            If cuuid.Length > 5 Then

                Dim dr As DataRow = dSaph.Get_Horario_UUid(cuuid)
                idhorario = dr("idHorario")

            Else
                idhorario = Convert.ToInt32(cuuid)
            End If

            GrdHistoria.DataSource = dSaph.List_General("SELECT * FROM historia LEFT OUTER JOIN recursos ON recursos.idrecurso = historia.ipkidrecurso WHERE historia.ipkidhorario=?", idhorario)
            GrdHistoria.DataBind()

        Catch ex As Exception
            MostrarError(ex, Nothing, Util.MENSAJE_TIPO.MT_ERROR)
        End Try


    End Sub
    Protected Sub Btnvolver_Click(sender As Object, e As EventArgs) Handles Btnvolver.Click

        Try
            If Not idhorario = -1 Then
                '  Response.Redirect(Util.ComponerParametrosUrl("PropuestaHorario.aspx", Util.QueryString_PonerParametro(Util.QUERYSTRING_IDHORARIO, idhorario)))
                ' Response.Redirect(Parametros.URL_PROPUESTAHORARIO_ADMIN(idhorario))
                Response.Redirect(Parametros.URL_PROPUESTAHORARIO_ADMIN(cuuid))
            Else
                Throw New Exception("Código de Horario incorrecto: " + idhorario.ToString)
            End If
        Catch ex As Exception
            MostrarError(ex)
        End Try

    End Sub
End Class