Partial Class FWInicio
    Inherits FormComunBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub BtnCrearHorario_Click(sender As Object, e As EventArgs) Handles BtnCrearHorario.Click
        'Sacar la siguiente página. Creación de horario.

        Response.Redirect("CrearHorario.aspx")
    End Sub
End Class
