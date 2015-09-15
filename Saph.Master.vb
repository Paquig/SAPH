Public Class Saphmaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    ' Acceso de BD General
    'Public dSahp As DatosSaph = Nothing

    'Public Function Control_lblMensajeDatos() As Label
    '    Dim o As Label = Util.FindControlRecursive(Master, "lblMensajeDatos")
    '    Return o
    'End Function
    Public Function GetAlert() As String
        Return CType(Page, FormComunBase).GetAlert()
    End Function

    Public Function GetAlert_JS() As String
        Return CType(Page, FormComunBase).GetAlert_JS()
    End Function
End Class