Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data

Public Class BdConexion

#Region "PROPIEDADES"
    Protected _con As OleDbConnection
    Protected _Bd As BdConexionManager.BD_DISPONIBLES
    Protected _noAbrirNiCerrar As Boolean = False
    Protected cTokenAbrir As String = TOKENNOESTABLECIDO
    Protected cTokenCerrar As String = TOKENNOESTABLECIDO
    Public Const TOKENNOESTABLECIDO As String = ""

    Protected transactionWRK As OleDbTransaction = Nothing

    Public ReadOnly Property hayTransaccion() As OleDbTransaction
        Get
            Return transactionWRK
        End Get
    End Property

    Public Property NoAbrirNiCerrar() As Boolean
        Get
            Return _noAbrirNiCerrar
        End Get
        Set(ByVal value As Boolean)
            _noAbrirNiCerrar = value
        End Set
    End Property

    Public Function EstablecerTokenAbrir() As String
        If NoAbrirNiCerrar() = False Then
            cTokenAbrir = Util.NombreFicheroAleatorio()
        Else
            Return TOKENNOESTABLECIDO
        End If
        Return cTokenAbrir

    End Function

    Public Function EstablecerTokenCerrar(ByVal cToken As String) As Boolean
        If cToken IsNot Nothing AndAlso cToken = cTokenAbrir Then
            cTokenCerrar = cToken
            Return True
        Else
            Return False
        End If
    End Function


    Public Property Bd() As BdConexionManager.BD_DISPONIBLES
        Get
            Return _Bd
        End Get
        Set(ByVal value As BdConexionManager.BD_DISPONIBLES)
            _Bd = value
        End Set
    End Property

    Public Property Conexion() As OleDbConnection
        Get
            Return _con
        End Get
        Set(ByVal con As OleDbConnection)
            _con = con
        End Set
    End Property

#End Region

    Public Sub New(ByVal pBd As BdConexionManager.BD_DISPONIBLES)
        BdConexionManager.ValidarBd(pBd)

        Bd = pBd
    End Sub

    Public Sub Conecta()
        If NoAbrirNiCerrar() = False Then
            _con.Open()
        End If
    End Sub

    Public Sub Cierra()
        If NoAbrirNiCerrar() = False Then
            _con.Close()
        End If
    End Sub

    Public Function ConectaAnidado() As String
        Return ConectaAnidado(False)
    End Function

    Public Function ConectaAnidado(ByVal conTransaccion As Boolean) As String
        Dim miToken As String = TOKENNOESTABLECIDO
        If NoAbrirNiCerrar() = False Then
            Conecta()
            If conTransaccion Then
                transactionWRK = Conexion.BeginTransaction()
            Else
                transactionWRK = Nothing
            End If
            miToken = EstablecerTokenAbrir()
            NoAbrirNiCerrar = True
        End If
        Return miToken
    End Function

    Public Sub CierraAnidado()
        CierraAnidado(TOKENNOESTABLECIDO)
    End Sub

    Public Sub CierraAnidado(ByVal mitoken As String)
        EstablecerTokenCerrar(mitoken)
        If cTokenAbrir = cTokenCerrar Then
            cTokenAbrir = TOKENNOESTABLECIDO
            cTokenCerrar = TOKENNOESTABLECIDO
            NoAbrirNiCerrar = False
            transactionWRK = Nothing
            Cierra()
        End If
    End Sub

    'Public Sub Commit(Optional ByVal mitoken As String = TOKENNOESTABLECIDO)
    '    EstablecerTokenCerrar(mitoken)
    '    If cTokenAbrir = cTokenCerrar Then
    '        If transactionWRK IsNot Nothing Then
    '            transactionWRK.Commit()
    '        End If
    '    End If
    'End Sub

    'Public Sub Rollback(Optional ByVal mitoken As String = TOKENNOESTABLECIDO)
    '    EstablecerTokenCerrar(mitoken)
    '    If cTokenAbrir = cTokenCerrar Then
    '        If transactionWRK IsNot Nothing Then
    '            transactionWRK.Rollback()
    '        End If
    '    End If
    'End Sub

    'Public Function EstadoCerrada() As Boolean
    '    If Conexion().State = ConnectionState.Closed Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Public Function CreateOleDbCommand() As OleDbCommand
        Dim cmd As New OleDbCommand()
        cmd.Connection = Conexion()
        If transactionWRK IsNot Nothing Then
            cmd.Transaction = transactionWRK
        End If
        Return cmd
    End Function

    Public Function CreateOleDbCommand(ByVal cmdText As String, ByVal ParamArray parameters() As Object) As OleDbCommand
        Dim cmd As OleDbCommand = CreateOleDbCommand()
        Util.OleDbCommandSet(cmd, cmdText, parameters)
        Return cmd
    End Function


    '  Ejecuta una instrucción SQL en y devuelve el número de filas afectadas.
    Public Function ExecuteNonQuery(ByVal cmdText As String, ByVal ParamArray parameters() As Object) As Integer
        Dim nRetorno As Integer = -1

        Dim cmd As OleDbCommand = CreateOleDbCommand(cmdText, parameters)

        Try
            Conecta()
            Try
                HttpContext.Current.Application.Lock()

                Try
                    nRetorno = cmd.ExecuteNonQuery()
                Catch ex As Exception

                    Dim parameters_Str As StringBuilder = New StringBuilder()
                    For i As Integer = 0 To parameters.Length() - 1
                        If parameters(i) IsNot Nothing Then
                            parameters_Str.Append("[" + parameters(i).ToString() + "]" + vbCrLf)
                        Else
                            parameters_Str.Append("[NOTHING]" + vbCrLf)
                        End If
                    Next
                    Throw New Exception(ex.Message + vbCrLf + vbCrLf + ">>>>>>>>>>>>>" + vbCrLf + ">>>>Sentencia SQL:" + cmdText + vbCrLf + "PARAMETERS:" + vbCrLf + parameters_Str.ToString() + ">>>>>>>>>>>>>" + vbCrLf, ex)
                End Try

            Finally
                cmd.Connection = Nothing
                cmd = Nothing
                '
                HttpContext.Current.Application.Unlock()
            End Try
        Finally
            Cierra()
        End Try

        Return nRetorno
    End Function

    'Public Function ExecuteScalar(ByVal cmdText As String, ByVal ParamArray parameters() As Object) As Integer
    '    Dim nRetorno As Integer = -1

    '    Dim cmd As OleDbCommand = CreateOleDbCommand(cmdText, parameters)

    '    Try
    '        Conecta()
    '        Try
    '            HttpContext.Current.Application.Lock()

    '            Try
    '                nRetorno = cmd.ExecuteScalar()
    '            Catch ex As Exception
    '                Throw New Exception(ex.Message + vbCrLf + vbCrLf + ">>>>>>>>>>>>>" + vbCrLf + ">>>>Sentencia SQL:" + cmdText + vbCrLf + ">>>>>>>>>>>>>" + vbCrLf, ex)
    '            End Try

    '        Finally
    '            cmd.Connection = Nothing
    '            cmd = Nothing
    '            '
    '            HttpContext.Current.Application.Unlock()
    '        End Try
    '    Finally
    '        Cierra()
    '    End Try

    '    Return nRetorno
    'End Function

    Public Function ExecuteQuery_DataTable(ByVal cmdText As String, ByVal ParamArray parameters() As Object) As DataTable
        Dim cmd As OleDbCommand = CreateOleDbCommand(cmdText, parameters)

        Dim da As New OleDbDataAdapter(cmd)
        Dim t As New DataTable

        Try
            Conecta()

            da.Fill(t)
            Return t
        Catch ex As Exception
            Throw New Exception(ex.Message + vbCrLf + vbCrLf + Util.SQL2String(cmdText, parameters), ex)
        Finally
            Cierra()
        End Try

    End Function

    Public Function ExecuteQuery_DataRow(ByVal cmdText As String, ByVal ParamArray parameters() As Object) As DataRow
        Dim dt As DataTable = ExecuteQuery_DataTable(cmdText, parameters)
        If dt.Rows.Count = 0 Then
            Throw New Exception("Registro no existente. [" + IIf(parameters.Length > 0, parameters(0).ToString(), "") + "]")
        End If
        Return dt.Rows(0)
    End Function


    Public Function GetAutoIncKey(ByVal columna As String, ByVal tabla As String) As Integer
        Dim cmd As OleDbCommand = CreateOleDbCommand("SELECT " + columna + " FROM " + tabla + " WHERE " + columna + "=GETAUTOINCVALUE(0)")
        Dim o As Object
        o = cmd.ExecuteScalar()

        Dim ncod As Integer

        If Not o Is DBNull.Value AndAlso Not o Is Nothing Then
            ncod = CType(o, Integer)
            Return ncod
        Else
            Throw New Exception("Error recuperando código " + tabla + "." + columna)
        End If
    End Function

End Class