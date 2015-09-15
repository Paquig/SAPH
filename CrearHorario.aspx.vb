Public Class FWCrearHorario
    Inherits FormComunBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' MyBase.Page_Load(sender, e)
    End Sub

    Protected Sub BtnSiguiente_Click(sender As Object, e As EventArgs) Handles BtnSiguiente.Click

        'Validamos los campos que consideramos obligatorios para crear el horario

        Try
            ValidarCampos()

        Catch ex As ValidarException
            MostrarError(ex, ex.controlObject)
            Return
        End Try

        'Insertamos en la Tabla Horarios y en Historial
        Try

            AsignarConexionBD()

            Dim idhorario As Integer = 0
            idhorario = dSaph.Insert_Horario(TxtTitulo.Text, TxtSubtitulo.Text, TxtNomUsuario.Text, TxtCorreoElectronico.Text, "")

            dSaph.Insert_Historia(DateTime.Now, TxtNomUsuario.Text, Util.ACCION_HORARIO.Añadir, idhorario, 0)

            MostrarError(New Exception("El Calendario se ha creado correctamente"), Nothing, Util.MENSAJE_TIPO.MT_INFO)

            'Hay que montar la siguiente url con parametros, idhorario y cNombreUsuario
            ' Response.Redirect(Util.ComponerParametrosUrl("PropuestaHorario.aspx", Util.QueryString_PonerParametro(Util.QUERYSTRING_IDHORARIO, idhorario.ToString)))
            Response.Redirect(Parametros.URL_PROPUESTAHORARIO(idhorario))
        Catch ex As Exception
            MostrarError(ex, Nothing, Util.MENSAJE_TIPO.MT_ERROR)
            Return
        End Try



    End Sub


    Protected Sub ValidarCampos()

        If (Util.EsVacio(TxtTitulo.Text)) Then
            Throw New ValidarException("Debes indicar el Título del Calendario." + Util.HTML_SALTOLINEA, TxtTitulo)
        End If

        'If (Util.EsVacio(TxtSubtitulo.Text)) Then
        '    Throw New ValidarException("Debes indicar el Subtítulo del Calendario." + Util.HTML_SALTOLINEA, TxtSubtitulo)
        'End If

        'If (Util.EsVacio(TxtNomUsuario.Text)) Then
        '    Throw New ValidarException("Debes indicar el Nombre del Administrador del Calendario." + Util.HTML_SALTOLINEA, TxtNomUsuario)
        'End If

        If (Util.EsVacio(TxtCorreoElectronico.Text)) Then
            Throw New ValidarException("Debes indicar el correo electrónico del Administrador del Calendario." + Util.HTML_SALTOLINEA, TxtCorreoElectronico)
        Else
            If Not Util.EsEmailValido(TxtCorreoElectronico.Text.Trim) Then
                Throw New ValidarException("El correo electrónico no tiene formato correcto." + Util.HTML_SALTOLINEA, TxtCorreoElectronico)
            End If
        End If
    End Sub
End Class