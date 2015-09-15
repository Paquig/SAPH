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

    ' ************************************
    '
    ' FUNCIONES GENÉRICAS COMUNES A LAS BD
    ' 
    ' ************************************

    'Public Function List_TablaAll(ByVal tabla As String, Optional ByVal orderStr As String = "1") As DataTable
    '    Return List_SELECT("SELECT * FROM " + tabla + IIf(Util.EsVacio(orderStr), "", " ORDER BY " + orderStr))
    'End Function

    'Public Function List_TablaAllClave(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As Integer) As DataTable
    '    Return List_SELECT("SELECT * FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    'End Function

    'Public Function List_TablaAllClave(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As String) As DataTable
    '    Return List_SELECT("SELECT * FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    'End Function

    Public Function List_TablaClave(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As Integer) As DataRow
        Return dgBdConexion.ExecuteQuery_DataRow("SELECT * FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    End Function

    Public Function List_TablaClave(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As String) As DataRow
        Return dgBdConexion.ExecuteQuery_DataRow("SELECT * FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    End Function

    'Public Function List_TablaWHERE(ByVal tabla As String, ByVal strWHERE As String, Optional ByVal strORDER As String = "") As DataTable
    '    Dim cmd As String = "SELECT * FROM " + tabla

    '    If Not Util.EsVacio(strWHERE) Then
    '        cmd = cmd + " WHERE " + strWHERE
    '    End If

    '    If Not Util.EsVacio(strORDER) Then
    '        cmd = cmd + " ORDER BY " + strORDER
    '    End If

    '    Return List_SELECT(cmd)
    'End Function

    Public Function List_SELECT(ByVal cmdSELECT As String, ByVal ParamArray parameters() As Object) As DataTable
        Return dgBdConexion.ExecuteQuery_DataTable(cmdSELECT, parameters)
    End Function

    'Public Function List_FirstRow(ByRef dt As DataTable, ByVal mensajeExcepcion As String) As DataRow
    '    If dt.Rows.Count = 0 Then Throw New SqlRegistroNoEncontradoException(mensajeExcepcion)
    '    Return dt.Rows(0)
    'End Function

    Public Sub Del_Tabla(ByVal tabla As String, ByVal campoClave As String, ByVal valorClave As Integer)
        dgBdConexion.ExecuteNonQuery("DELETE FROM " + tabla + " WHERE " + campoClave + "=?", valorClave)
    End Sub

    Public Function GetAutoIncKey(ByVal columna As String, ByVal tabla As String) As Integer
        Return dgBdConexion.GetAutoIncKey(columna, tabla)
    End Function

End Class