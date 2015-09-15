Imports System.Data
Public Class Editar
    Inherits System.Web.UI.Page
    Private idRecurso As Integer = 0
    Private idHorario As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()

        If Not IsPostBack Then

            Dim dSaph As New DatosSaph
            idRecurso = CInt(Util.QueryString_ObtenerIdRecurso(Request))

            Dim dr As DataRow = dSaph.Get_Recurso(idRecurso)
            Dim inicio As DateTime = dr("tStartTime")
            LabelStart.Text = inicio.ToString("HH:mm:ss")
            Dim fin As DateTime = dr("tEndTime")
            LabelEnd.Text = fin.ToString("HH:mm:ss")

            TxtDescripcion.Text = dr("cDesRecurso")
            TxtUsuario.Text = dr("cNombreUsuario")
            idHorario = dr("ipkidHorario")

        End If
    End Sub

    Protected Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim note As String = TxtDescripcion.Text
        Dim dSaph As New DatosSaph
        idRecurso = Util.QueryString_ObtenerIdRecurso(Request)

        dSaph.UpdateRecursoDescripcion(idRecurso, TxtDescripcion.Text, "")
        dSaph.Insert_Historia(DateTime.Now, TxtUsuario.Text, Util.ACCION_HORARIO.ModificarRecurso, idHorario, idRecurso)

        Dim ht As New Hashtable()
        ht("refresh") = "yes"
        ht("message") = "Recurso Actualizado."

        Util.Close(Me, ht)

    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Util.Close(Me)
    End Sub

    Protected Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dSaph As New DatosSaph
        idRecurso = Util.QueryString_ObtenerIdRecurso(Request)
        dSaph.DeleteRecurso(idRecurso)

        dSaph.Insert_Historia(DateTime.Now, TxtUsuario.Text, Util.ACCION_HORARIO.BorrarRecurso, idHorario, idRecurso)

        Dim ht As New Hashtable()
        ht("refresh") = "yes"
        ht("message") = "Recurso borrado."
        Util.Close(Me, ht)
    End Sub


End Class