Public Class FWConfiguracion
    Inherits FormComunBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnAnterior_Click(sender As Object, e As EventArgs) Handles BtnAnterior.Click
        Response.Redirect("CrearHorario.aspx")

    End Sub

    Protected Sub BtnSiguiente_Click(sender As Object, e As EventArgs) Handles BtnSiguiente.Click
        Response.Redirect("PropuestaHorario.aspx")
    End Sub
End Class