Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data

Public Class BdConexionManager

#Region "BD_DISPONIBLES"

    ' Bases de datos disponibles
    Public Enum BD_DISPONIBLES As Integer
        BD_NOESTABLECIDA = 0
        BD_SAPH = 1

    End Enum

    Protected Const BD_DISPONIBLES_MINVALUE As Integer = BD_DISPONIBLES.BD_SAPH
    Protected Const BD_DISPONIBLES_MAXVALUE As Integer = BD_DISPONIBLES.BD_SAPH

    Public Shared Sub ValidarBd(ByVal bd As BD_DISPONIBLES)
        If bd < BD_DISPONIBLES_MINVALUE OrElse bd > BD_DISPONIBLES_MAXVALUE Then
            Throw New ArgumentOutOfRangeException("La base de datos [" + bd.ToString + "] no es un argumento válido.")
        End If
    End Sub

#End Region


    Private objectLock As New Object

    Public Sub New()
        Init()
    End Sub

    ' Inicializar
    Public Sub Init()

        SyncLock objectLock
        End SyncLock

    End Sub

    Public Function AsignaConexion_SAPH() As BdConexion
        Return AsignaConexion(BD_DISPONIBLES.BD_SAPH)
    End Function

    Public Function AsignaConexion(ByVal bd As BD_DISPONIBLES, ByVal ParamArray parameters() As String) As BdConexion
        Dim bdConexionTmp As BdConexion
        '  Dim cs_Saph As String
        bdConexionTmp = New BdConexion(bd)

        ValidarBd(bd)

        Select Case bd
            Case BdConexionManager.BD_DISPONIBLES.BD_SAPH
                '    cs_Saph = "\datos\bdSaph.dbc"
                bdConexionTmp.Conexion() = New OleDbConnection(Parametros.CS_SAPH)
                '   bdConexionTmp.Conexion() = New OleDbConnection(cs_Saph)

        End Select

        Return bdConexionTmp
    End Function

End Class