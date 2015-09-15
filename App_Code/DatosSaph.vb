Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Net
Imports DayPilot.Web.Ui

Public Class DatosSaph
    Inherits DatosGenericos


    Public Sub New()

        MyBase.New(BdConexionManager.BD_DISPONIBLES.BD_SAPH)
    End Sub


#Region "Recursos"
    Public Function Get_Recurso(ByVal IdRecurso As Integer) As DataRow
        Return List_TablaClave("Recursos", "IdRecurso", IdRecurso)
    End Function

    'Public Function List_Recursos(ByVal IdRecurso As Integer) As DataTable
    '    Return List_SELECT("SELECT * FROM Recursos  WHERE IdRecurso=?", IdRecurso)
    'End Function

    Public Function List_Recursos_Horario(ByVal idhorario As Integer) As DataTable
        Return List_SELECT("SELECT * FROM Recursos WHERE IpkIdHorario = ?", idhorario)
    End Function

    Public Function Insert_Recurso(ByVal cDesRecurso As String, _
                                         ByVal tStartTime As DateTime, _
                                         ByVal tEndTime As DateTime, _
                                         ByVal cDiaSemana As String, _
                                         ByVal cNombreUsuario As String, _
                                         ByVal ipkidHorario As Integer _
                                         ) As Integer

        Dim ipkRecurso As Integer = -1

        Try

            HttpContext.Current.Application.Lock()

            Dim cmd As String = "INSERT INTO Recursos(cDesRecurso, tStartTime," + _
                                "tEndTime, cDiaSemana, cNombreUsuario, ipkIdHorario) " + _
                                "VALUES (?,?,?,?,?,?)"
            Try

                dgBdConexion.ConectaAnidado()

                dgBdConexion.ExecuteNonQuery(cmd, cDesRecurso, _
                                       tStartTime, tEndTime, cDiaSemana, _
                                       cNombreUsuario, _
                                       ipkidHorario)


                ipkRecurso = dgBdConexion.GetAutoIncKey("IdRecurso", "Recursos")

            Finally
                dgBdConexion.CierraAnidado()

            End Try
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            HttpContext.Current.Application.UnLock()

        End Try

        Return ipkRecurso

    End Function

    Public Sub MoverRecurso(ByVal idRecurso As Integer, ByVal startTime As DateTime, ByVal endTime As DateTime)

        dgBdConexion.ExecuteNonQuery("UPDATE Recursos SET tStartTime=?, tEndTime=? " + _
                                    "WHERE IDRECURSO=?", _
                                   startTime, endTime, idRecurso)

       
    End Sub

    Public Sub UpdateRecursoDescripcion(ByVal idRecurso As Integer, ByVal cDescripcion As String, ByVal color As String)

        dgBdConexion.ExecuteNonQuery("UPDATE Recursos SET cDesRecurso=? " + _
                                   "WHERE IDRECURSO=?", _
                                   cDescripcion, idRecurso)
    End Sub

    Public Sub DeleteRecurso(ByVal idRecurso As Integer)
        dgBdConexion.ExecuteNonQuery("DELETE FROM Recursos " + _
                                  "WHERE IDRECURSO=?", _
                                   idRecurso)
       
    End Sub

    'Public Function Get_DocAnexo_Numero(ByVal nNumRegistro As Integer) As Integer
    '    Dim numFicherosAdjuntos As Integer = 0

    '    Dim dT_RD As DataTable = List_SELECT("SELECT COUNT(*) AS nNumFichAdj FROM DocAnexo WHERE nNumRegistro = ?", nNumRegistro)

    '    If dT_RD.Rows.Count > 0 Then
    '        numFicherosAdjuntos = dT_RD.Rows(0)("nNumFichAdj")
    '    End If

    '    Return numFicherosAdjuntos
    'End Function

#End Region
#Region "Horario"

    Public Function Insert_Horario(ByVal cTituloHorario As String, _
                                      ByVal cSubtituloHorario As String, _
                                      ByVal cNombreAdmin As String, _
                                      ByVal cEmailAdmin As String, _
                                      ByVal cUuid As String _
                                      ) As Integer

        Dim ipkhorario As Integer = -1

        Try

            HttpContext.Current.Application.Lock()

            Dim cmd As String = "INSERT INTO Horario(CTITULOHORARIO, CSUBTITULOHORARIO," + _
                                "CNOMBREADMIN, CEMAILADMIN, CUUID) " + _
                                "VALUES (?,?,?,?,?)"
            Try

                dgBdConexion.ConectaAnidado()

                dgBdConexion.ExecuteNonQuery(cmd, cTituloHorario, _
                                       cSubtituloHorario, _
                                       cNombreAdmin, _
                                       cEmailAdmin, cuuid)


                ipkHorario = dgBdConexion.GetAutoIncKey("IdHorario", "Horario")

            Finally
                dgBdConexion.CierraAnidado()

            End Try
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            HttpContext.Current.Application.UnLock()

        End Try

        Return ipkHorario

    End Function

    Public Sub Update_Horario(ByVal idHorario As Integer, _
                                    ByVal cTituloHorario As String, _
                                    ByVal cSubtituloHorario As String, _
                                    ByVal cNombreAdmin As String, _
                                    ByVal cEmailAdmin As String _
                                   )

        dgBdConexion.ExecuteNonQuery("UPDATE Horario SET CTITULOHORARIO=?,CSUBTITULOHORARIO=?," + _
                            "CNOMBREADMIN=?,CEMAILADMIN=? " + _
                            "WHERE IDHORARIO=?", _
                            cTituloHorario, cSubtituloHorario, cNombreAdmin, cEmailAdmin, _
                            idHorario)
    End Sub


    Public Sub UpdateHorarioUuid(ByVal idHorario As Integer, ByVal cUuid As String)

        dgBdConexion.ExecuteNonQuery("UPDATE Horario SET cUuid=? " + _
                                   "WHERE idHorario=?", _
                                   cUuid, idHorario)
    End Sub
    Public Function Get_Horario(ByVal idHorario As Integer) As DataRow
        Return List_TablaClave("Horario", "idHorario", idHorario)
    End Function
    Public Function Get_Horario_UUid(ByVal cUuid As string) As DataRow
        Return List_TablaClave("Horario", "cUuid", cUuid)
    End Function

    Public Function List_General(ByVal strSELECT As String, ByVal ParamArray parameters() As Object) As DataTable
        Return List_SELECT(strSELECT, parameters)
    End Function

    Public Sub DeleteHorario(ByVal idHorario As Integer)
        dgBdConexion.ExecuteNonQuery("DELETE FROM Horario " + _
                                  "WHERE IDHORARIO=?", _
                                   idHorario)

    End Sub
#End Region

#Region "Historia"

    Public Function Insert_Historia(ByVal tFecAccion As DateTime, _
                                    ByVal cNomUsuario As String, _
                                    ByVal ipkaccion As Integer, _
                                    ByVal ipkidhorario As Integer, _
                                    ByVal ipkidRecurso As Integer _
                                      ) As Integer

        Dim ipkHistoria As Integer = -1

        Try

            HttpContext.Current.Application.Lock()

            Dim cmd As String = "INSERT INTO Historia(TFECACCION, CNOMUSUARIO," + _
                                "IPKACCION, ipkidhorario, ipkidrecurso) " + _
                                "VALUES (?,?,?,?,?)"
            Try

                dgBdConexion.ConectaAnidado()

                dgBdConexion.ExecuteNonQuery(cmd, tFecAccion, _
                                       cNomUsuario, _
                                       ipkaccion, _
                                       ipkidhorario, _
                                       ipkidrecurso)


                ipkHistoria = dgBdConexion.GetAutoIncKey("IdHistoria", "Historia")

            Finally
                dgBdConexion.CierraAnidado()

            End Try
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            HttpContext.Current.Application.UnLock()

        End Try

        Return ipkHistoria

    End Function

    Public Function List_Historia_Horario(ByVal idhorario As Integer) As DataTable
        Return List_SELECT("SELECT * FROM Historia WHERE ipkidhorario = ?", idhorario)
    End Function
#End Region

    Public Sub Pon_SetHoras()

        Dim c As New OleDbConnection(Parametros.CS_SAPH)

        c.Open()


        'tr = c.BeginTransaction()


        Dim cmd_VFP As New OleDbCommand()
        cmd_VFP.Connection = c
        '  cmd_VFP.Connection = dgBdConexion.Conexion
        'cmd_VFP.CommandText = "SET REPROCESS TO 300 SECONDS"
        cmd_VFP.CommandText = "SET HOURS TO 24"
        'cmd_VFP.CommandText = "SET DATE TO DMY"
        'cmd_VFP.Transaction = tr
        cmd_VFP.ExecuteNonQuery()


        '   HttpContext.Current.Application.Lock()

        'Dim cmd As String = "SET HOURS TO 24"

        'dgBdConexion.ExecuteNonQuery(cmd)


    
        '    End Try
        'Catch ex As Exception
        '    Throw New Exception(ex.Message, ex)
        'Finally
        '    HttpContext.Current.Application.UnLock()

        'End Try



    End Sub
End Class
