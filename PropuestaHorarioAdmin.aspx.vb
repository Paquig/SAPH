Imports DayPilot.Web.Ui.Events
Imports System.Data

Partial Class FWPropuestaHorarioAdmin
    Inherits FormComunBase

    Protected Id_horario As Integer = -1
    Protected cuuid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            cuuid = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            ' si viene del link del correo...averiguamos el idhorario de la tabla recursos
            AsignarConexionBD()
            If cuuid.Length > 5 Then
                Try
                    Dim dr As DataRow = dSaph.Get_Horario_UUid(cuuid)
                    Id_horario = dr("idHorario")
                Catch ex2 As Exception
                    Response.Redirect("PaginaError.aspx", True)
                End Try
            Else
                Id_horario = Convert.ToInt32(cuuid)
                Try
                    Dim dr As DataRow = dSaph.Get_Horario(Id_horario)
                    Id_horario = dr("idHorario")
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

    Protected Sub LinkBtonEditar_Click(sender As Object, e As EventArgs) Handles LinkBtonEditar.Click
        Response.Redirect(Parametros.URL_EDITARHORARIO(cuuid))
    End Sub

    Protected Sub LinkBtonInvitar_Click(sender As Object, e As EventArgs) Handles LinkBtonInvitar.Click
        Response.Redirect(Parametros.URL_ENVIARINVITACION(cuuid))
    End Sub

    Protected Sub LinkBtonHistoria_Click(sender As Object, e As EventArgs) Handles LinkBtonHistoria.Click
        Response.Redirect(Parametros.URL_HISTORIAHORARIO(cuuid))
    End Sub

    Protected Sub LinkBtonImprimir_Click(sender As Object, e As EventArgs) Handles LinkBtonImprimir.Click
        Listado("PANTALLA")
    End Sub

    Protected Sub LinkBtonExcel_Click(sender As Object, e As EventArgs) Handles LinkBtonExcel.Click
        Listado("EXCEL")
    End Sub

    Protected Sub Listado(ByVal cTipo As String)
        Try

            Dim dT_recursos As DataTable = dSaph.List_General("SELECT *," + _
                                           "PADL(allt(str(HOUR(recursos.tstarttime))),2,""0"") + "":"" + PADR(allt(str(MINUTE(recursos.tstarttime))),2,""0"") as horainicio " + _
                                           " from Recursos where ipkidhorario=? Order by tstarttime", Id_horario)

            If dT_recursos.Rows.Count = 0 Then
                Throw New Exception("No hay recursos en este Calendario Compartido")
            Else


                Dim dt_datoslistado As DataTable = dSaph.List_General("SELECT distinct " + _
                               "PADL(allt(str(HOUR(recursos.tstarttime))),2,""0"") + "":"" + PADR(allt(str(MINUTE(recursos.tstarttime))),2,""0"") as horainicio " + _
                              " from Recursos where ipkidhorario=? ", Id_horario)

                Dim column As DataColumn

                ' Añadir columna lunedesrecurso
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "lunesrecurso"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna lunesusuario
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "lunesusuario"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                '
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "luneshfin"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()


                ' Añadir columna martesdesrecurso
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "martesrecurso"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna martessusuario
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "martesusuario"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "marteshfin"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna miercolesdesrecurso
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "miercolesrecurso"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna miercolesusuario
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "miercolesusuario"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "miercoleshfin"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna juevesrecurso
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "juevesrecurso"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna usuario
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "juevesusuario"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "jueveshfin"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna desrecurso
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "viernesrecurso"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna susuario
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "viernesusuario"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "vierneshfin"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna desrecurso
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "sabadorecurso"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna susuario
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "sabadousuario"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "sabadohfin"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna desrecurso
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "domingorecurso"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                ' Añadir columna usuario
                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "domingousuario"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "domingohfin"

                dt_datoslistado.Columns.Add(column)

                dt_datoslistado.AcceptChanges()

                '---------
                Dim horatrabajo As String = ""

                For Each dr_hora As DataRow In dt_datoslistado.Rows
                    For Each dr_recurso As DataRow In dT_recursos.Rows
                        If dr_hora("horainicio") = dr_recurso("horainicio") Then
                            If dr_recurso("cDiaSemana").ToString.Trim = "lunes" Then
                                dr_hora("lunesrecurso") = dr_recurso("cDesRecurso")
                                dr_hora("lunesusuario") = dr_recurso("cNombreUsuario")
                                dr_hora("luneshfin") = dr_recurso("tEndTime")
                            End If
                            If dr_recurso("cDiaSemana").ToString.Trim = "martes" Then
                                dr_hora("martesrecurso") = dr_recurso("cDesRecurso")
                                dr_hora("martesusuario") = dr_recurso("cNombreUsuario")
                                dr_hora("marteshfin") = dr_recurso("tEndTime")
                            End If
                            If dr_recurso("cDiaSemana").ToString.Trim = "miércoles" Then
                                dr_hora("miercolesrecurso") = dr_recurso("cDesRecurso")
                                dr_hora("miercolesusuario") = dr_recurso("cNombreUsuario")
                                dr_hora("miercoleshfin") = dr_recurso("tEndTime")
                            End If
                            If dr_recurso("cDiaSemana").ToString.Trim = "jueves" Then
                                dr_hora("juevesrecurso") = dr_recurso("cDesRecurso")
                                dr_hora("juevesusuario") = dr_recurso("cNombreUsuario")
                                dr_hora("jueveshfin") = dr_recurso("tEndTime")
                            End If
                            If dr_recurso("cDiaSemana").ToString.Trim = "viernes" Then
                                dr_hora("viernesrecurso") = dr_recurso("cDesRecurso")
                                dr_hora("viernesusuario") = dr_recurso("cNombreUsuario")
                                dr_hora("vierneshfin") = dr_recurso("tEndTime")
                            End If
                            If dr_recurso("cDiaSemana").ToString.Trim = "sábado" Then
                                dr_hora("sabadorecurso") = dr_recurso("cDesRecurso")
                                dr_hora("sabadousuario") = dr_recurso("cNombreUsuario")
                                dr_hora("sabadohfin") = dr_recurso("tEndTime")
                            End If
                            If dr_recurso("cDiaSemana").ToString.Trim = "domingp" Then
                                dr_hora("domingorecurso") = dr_recurso("cDesRecurso")
                                dr_hora("domingousuario") = dr_recurso("cNombreUsuario")
                                dr_hora("domingohfin") = dr_recurso("tEndTime")
                            End If
                            dr_hora.AcceptChanges()
                        End If
                    Next

                    dt_datoslistado.AcceptChanges()
                Next

                '----------


                Dim drhorario As DataRow = dSaph.Get_Horario(Id_horario)

                Dim params(3, 2) As String
                params(0, 0) = "titulo"
                params(0, 1) = "Calendario Compartido " + drhorario("cTituloHorario").ToString.Trim
                params(1, 0) = "subtitulo"
                params(1, 1) = drhorario("csubtituloHorario")
                params(2, 0) = "logotipo"
                params(2, 1) = Parametros.REPORTS_LOGO()

                Select Case cTipo
                    Case "PANTALLA"
                        Util.ReportGenerarEnNavegador(Parametros.PATH_WEBRAIZ() + "ListadoCalendarioCompartido.trdx", dt_datoslistado, params, "CalendarioCompartido.pdf", Response)
                    Case "PDF"
                        Dim rutaPDF As String
                        rutaPDF = Parametros.PATH_DATOSRAIZ() + "temp\" + Util.NombreFicheroAleatorio() + "_CalendarioCompartido" + ".PDF"
                        Util.ReportGenerarEnPDF(Parametros.PATH_WEBRAIZ() + "ListadoCalendarioCompartido.trdx", dt_datoslistado, params, rutaPDF)
                    Case "EXCEL"
                        Util.ReportGenerarEnExcellNavegador(Parametros.PATH_WEBRAIZ() + "ListadoCalendarioCompartido.trdx", dt_datoslistado, params, "CalendarioCompartido.xls", Response)

                End Select

            End If

        Catch ex As Exception
            MostrarError(ex)
        End Try

    End Sub
End Class