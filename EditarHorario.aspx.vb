﻿Imports System.Data
Public Class FWEditarHorario
    Inherits FormComunBase
    Protected idhorario As Integer = 0
    Private cUuid As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' idhorario = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            ' AsignarConexionBD()
            cUuid = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            ' si viene del link del correo...averiguamos el idhorario de la tabla recursos
            AsignarConexionBD()
            If cUuid.Length > 5 Then

                Dim dr As DataRow = dSaph.Get_Horario_UUid(cUuid)
                idhorario = dr("idHorario")
              
            Else
                idhorario = Convert.ToInt32(cUuid)
            End If

            If Not IsPostBack Then

                Dim drhorario As DataRow = dSaph.Get_Horario(idhorario)
                TxtTitulo.Text = drhorario("cTituloHorario")
                TxtSubtitulo.Text = drhorario("cSubtituloHorario")
                TxtNomUsuario.Text = drhorario("cNombreAdmin")
                TxtCorreoElectronico.Text = drhorario("cemailAdmin")
            End If
        Catch ex As Exception
            MostrarError(ex, Nothing, Util.MENSAJE_TIPO.MT_ERROR)
            '   Page.EnableViewState = False
            '  MyBase.Finalize()
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

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Try
            dSaph.DeleteHorario(idhorario)
        Catch ex As Exception
            MostrarError(ex, Nothing, Util.MENSAJE_TIPO.MT_ERROR)
        End Try

    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        'Validamos los campos que consideramos obligatorios para crear el horario

        Try
            ValidarCampos()

        Catch ex As ValidarException
            MostrarError(ex, ex.controlObject)
            Return
        End Try

        'Insertamos en la Tabla Horarios y en Historial
        Try

            dSaph.Update_Horario(idhorario, TxtTitulo.Text, TxtSubtitulo.Text, TxtNomUsuario.Text, TxtCorreoElectronico.Text)

            dSaph.Insert_Historia(DateTime.Now, TxtNomUsuario.Text, Util.ACCION_HORARIO.Modificar, idhorario, 0)

            MostrarError(New Exception("El Calendario se ha modificado correctamente"), Nothing, Util.MENSAJE_TIPO.MT_INFO)

        Catch ex As Exception
            MostrarError(ex, Nothing, Util.MENSAJE_TIPO.MT_ERROR)
            '  Return
        End Try
    End Sub

    Protected Sub Btnvolver_Click(sender As Object, e As EventArgs) Handles Btnvolver.Click

        Try
            If Not idHorario = -1 Then
                '  Response.Redirect(Util.ComponerParametrosUrl("PropuestaHorario.aspx", Util.QueryString_PonerParametro(Util.QUERYSTRING_IDHORARIO, idhorario)))
                '   Response.Redirect(Parametros.URL_PROPUESTAHORARIO_ADMIN(idhorario))
                Response.Redirect(Parametros.URL_PROPUESTAHORARIO_ADMIN(cUuid))
            Else
                Throw New Exception("Código de Horario incorrecto: " + idHorario.ToString)
            End If
        Catch ex As Exception
            MostrarError(ex)
        End Try

    End Sub
End Class