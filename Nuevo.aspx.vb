Imports System
Imports System.Collections
Imports System.Data
Imports System.Web.UI.WebControls
Imports DayPilot.Utils
Imports DayPilot.Web.Ui.Enums

Partial Public Class Nuevo
    Inherits System.Web.UI.Page

    Private idhorario As Integer = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        idhorario = Util.QueryString_ObtenerIdHorario(Request)

        Dim Inicio As DateTime = Convert.ToDateTime(Request.QueryString("tstart"))

        TxtInicio.Text = Inicio.ToString("HH:mm:ss")

        TxtDay.Text = WeekdayName(Convert.ToDateTime(Request.QueryString("tstart")).DayOfWeek, False)

    End Sub

    Protected Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim day As String = TxtDay.Text.ToString.Trim
        Dim duracion As Integer = Convert.ToInt32(DropDownListDuration.SelectedValue)
        Dim cDescripcion As String = TxtDescripcion.Text
        Dim cNombreUsuario As String = TxtNombreUsuario.Text

        Dim tStartTime As Date = Util.QueryString_ObtenerParametro(Request, "tstart", True)
        Dim tEndTime As Date = Util.QueryString_ObtenerParametro(Request, "tend", True)

        tEndTime = tStartTime.AddMinutes(duracion)

        Dim dSaph As New DatosSaph
        Dim idrecurso As Integer = -1
        idrecurso = dSaph.Insert_Recurso(cDescripcion, tStartTime, tEndTime, day, cNombreUsuario, idhorario)
        dSaph.Insert_Historia(DateTime.Now, cNombreUsuario, Util.ACCION_HORARIO.AñadirRecurso, idhorario, idrecurso)

        Util.Close(Me)
    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Util.Close(Me)
    End Sub
End Class

