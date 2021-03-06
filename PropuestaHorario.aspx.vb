﻿Imports System.Data
Imports DayPilot.Web.Ui.Events

Partial Class FWPropuestaHorario
    Inherits FormComunBase

    Protected Id_horario As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim cuuid As String = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            ' si viene del link del correo...averiguamos el idhorario de la tabla recursos
            AsignarConexionBD()
            Dim dr As DataRow
            If cuuid.Length > 5 Then
                Try
                    dr = dSaph.Get_Horario_UUid(cuuid)
                    Id_horario = dr("idHorario")
                    LblTexto.Text = dr("cTituloHorario").ToString.ToUpper.Trim
                    BtnSiguiente.Visible = False
                Catch ex2 As Exception
                    Response.Redirect("PaginaError.aspx", True)
                End Try
            Else
                Try
                    Id_horario = Convert.ToInt32(cuuid)
                    dr = dSaph.Get_Horario(Id_horario)
                    LblTitulo.Text = "Calendario que va a compartir"
                Catch ex2 As Exception
                    Response.Redirect("PaginaError.aspx", True)
                End Try
            End If

            DayPilotCalendar.DataSource = ObtenerDatos()

            DayPilotCalendar.DataBind()

            Page.MaintainScrollPositionOnPostBack = True

        Catch ex As Exception
            MostrarError(ex, Nothing, Util.MENSAJE_TIPO.MT_ERROR)
        End Try
    End Sub


    Protected Function ObtenerDatos() As DataTable

        Dim dT_Listado As DataTable = dSaph.List_Recursos_Horario(Id_horario)

        Return dT_Listado

    End Function

    Private Sub LoadRecursos()

        DayPilotCalendar.DataSource = ObtenerDatos()
        DayPilotCalendar.DataBind()
        DayPilotCalendar.Update()

    End Sub

    Protected Sub DayPilotCalendar_EventMove(ByVal sender As Object, ByVal e As EventMoveEventArgs)

        dSaph.MoverRecurso(Convert.ToInt32(e.Id), e.NewStart, e.NewEnd)
        dSaph.Insert_Historia(DateTime.Now, "", Util.ACCION_HORARIO.ModificarRecurso, Id_horario, Convert.ToInt32(e.Id))
        LoadRecursos()
        DayPilotCalendar.Update()

    End Sub

    Protected Sub DayPilotCalendar_EventResize(ByVal sender As Object, ByVal e As EventResizeEventArgs)

        dSaph.MoverRecurso(Convert.ToInt32(e.Id), e.NewStart, e.NewEnd)
        dSaph.Insert_Historia(DateTime.Now, "", Util.ACCION_HORARIO.ModificarRecurso, Id_horario, Convert.ToInt32(e.Id))
        LoadRecursos()
        DayPilotCalendar.Update()

    End Sub

    Protected Sub BtnSiguiente_Click(sender As Object, e As EventArgs) Handles BtnSiguiente.Click
        Response.Redirect(Parametros.URL_ENVIARINVITACION(Id_horario))
    End Sub
End Class