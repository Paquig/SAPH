Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Net

Public Class DatosGenericos

    Protected bd As BdConexionManager.BD_DISPONIBLES = BdConexionManager.BD_DISPONIBLES.BD_NOESTABLECIDA
    Protected dgBdConexion As BdConexion

    Public Function GetBdConexion() As BdConexion
        Return dgBdConexion
    End Function

    Public Sub New()
        dgBdConexion = Nothing
    End Sub

    Public Sub New(ByVal pBd As BdConexionManager.BD_DISPONIBLES)
        Dim bdCM As BdConexionManager = New BdConexionManager()
        dgBdConexion = bdCM.AsignaConexion(pBd)
        bdCM = Nothing
    End Sub

    Public Function List_TablaClave(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As Integer) As DataRow
        Return dgBdConexion.ExecuteQuery_DataRow("SELECT * FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    End Function

    Public Function List_TablaClave(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As String) As DataRow
        Return dgBdConexion.ExecuteQuery_DataRow("SELECT * FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    End Function

    Public Function List_SELECT(ByVal cmdSELECT As String, ByVal ParamArray parameters() As Object) As DataTable
        Return dgBdConexion.ExecuteQuery_DataTable(cmdSELECT, parameters)
    End Function

    Public Sub Del_Tabla(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As Integer)
        dgBdConexion.ExecuteNonQuery("DELETE FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    End Sub

    Public Function GetAutoIncKey(ByVal columna As String, ByVal tabla As String) As Integer
        Return dgBdConexion.GetAutoIncKey(columna, tabla)
    End Function

End Class