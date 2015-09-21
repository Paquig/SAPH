Imports System.Data
Public Class FWEnviarInvitacion
    Inherits FormComunBase

    Private idHorario As Integer = -1
    Private drHorario As DataRow = Nothing
    Private cUuid As String
    Private nLinkllamador As Integer = Util.LINK_LLAMADOR.PropuestaHorario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            cUuid = Util.QueryString_ObtenerParametro(Request, Util.QUERYSTRING_IDHORARIO, True)
            AsignarConexionBD()
            Try
                If cUuid.Length > 5 Then
                    ' si viene del link del correo...averiguamos el idhorario de la tabla recursos
                    Dim dr As DataRow = dSaph.Get_Horario_UUid(cUuid)
                    idHorario = dr("idHorario")
                    nLinkllamador = Util.LINK_LLAMADOR.PropuestaAdmin
                Else
                    idHorario = Convert.ToInt32(cUuid)

                End If

                'Cogemos los datos del horario
                drHorario = dSaph.Get_Horario(idHorario)
            Catch ex2 As Exception
                Response.Redirect("PaginaError.aspx", True)
            End Try

            If Not IsPostBack Then

                Dim cTitulo As String = drHorario("cTituloHorario").ToString.Trim + IIf(Not Util.EsVacio(drHorario("cSubtituloHorario")), " - " + drHorario("cSubtituloHorario").ToString.Trim, "")

                Dim TextInicio As String = ""

                If Not Util.EsVacio(drHorario("cNombreAdmin").ToString.ToUpper.Trim) Then
                    TextInicio = drHorario("cNombreAdmin").ToString.ToUpper.Trim
                Else
                    TextInicio = drHorario("cEmailAdmin").ToString.ToUpper.Trim
                End If

                TxtMensaje.Text = TextInicio
                TxtMensaje.Text += " le invita a participar en la planificación del Horario: " + vbCrLf + _
                            cTitulo + vbCrLf + _
                            "por considerarle persona interesada." + vbCrLf + vbCrLf

            End If
        Catch ex As Exception
            MostrarError(ex)
        End Try


    End Sub

    Protected Sub BtnFinaliza_Click(sender As Object, e As EventArgs) Handles BtnFinaliza.Click

        Dim lenviados As Boolean = False
        Try
            'Voy a  generar el uuid del horario correspondiente, a no ser que esté reinvitando

            If Util.EsVacio(drHorario("cuuid").ToString.Trim) Then
                cUuid = Util.GeneraGUID()
            Else
                cUuid = drHorario("cuuid").ToString.Trim
            End If

            If Not Util.EsVacio(TxtDestinatarios.Text) Then

                If ComprobarEmails(TxtDestinatarios.Text) Then
                    ' Enviar los emails destinatarios
                    EnvioEmails()
                    lenviados = True
                Else
                    Throw New Exception("Dirección de correo electronico no valida, el correo debe tener el formato: nombre@dominio .xxx;...")
                End If

            End If

            If nLinkllamador = Util.LINK_LLAMADOR.PropuestaHorario Then
                EnvioEmailAdmin()
                'Actualizo el horario con el nuevo uuid.u
                dSaph.UpdateHorarioUuid(idHorario, cUuid)
                'Finalizar
                Response.Redirect(Parametros.URL_PAGINAFINAL(cUuid))
            Else
                'Está reinvitando desde el administrador
                If lenviados Then
                    dSaph.Insert_Historia(DateTime.Now, drHorario("cNombreAdmin"), Util.ACCION_HORARIO.Invitar, idHorario, 0)
                    Throw New Exception("Mensajes enviados a los participantes")
                Else
                    Throw New Exception("No se ha enviado ningún mensaje")
                End If
            End If

        Catch ex As Exception
            MostrarError(ex)
        End Try

    End Sub

    Protected Sub EnvioEmails()

        Dim cDestinatarios As String = ""

        cDestinatarios = TxtDestinatarios.Text.Trim

        Dim cRemitente As String = drHorario("cemailadmin").ToString.Trim

        Dim cAsunto As String = "Participación en la planificación de horario"
        Dim cCuerpo As String = ""
        Dim cAdjuntos As String = ""

        cAdjuntos += Parametros.REPORTS_LOGO()

        ' En el cuerpo voy a poner el mensaje

        cCuerpo += TxtMensaje.Text.ToString

        cCuerpo += "Acceda a la siguiente dirección para su tratamiento: " + vbCrLf
        cCuerpo += "<a href=""" + Parametros.URL_WEB_RAIZ() + Parametros.URL_PROPUESTAHORARIO(cUuid) + """>" + "Pincha aqui para acceder al calendario" + "</a>" + vbCrLf + vbCrLf
        cCuerpo = "<div style=""font-family:Arial"">" + cCuerpo + "</div>" + vbCrLf
        cCuerpo = cCuerpo.Replace(vbCrLf, "<br />")

        Util.EnviarMail(cRemitente, cDestinatarios, cAsunto, cCuerpo, cAdjuntos)

    End Sub

    Protected Sub EnvioEmailAdmin()

        Dim cDestinatarios As String = ""

        cDestinatarios = drHorario("cemailadmin").ToString.Trim

        If Util.EsVacio(cDestinatarios) Then
            Throw New Exception(" No se ha enviado correo porque no se han obtenido el correo del Administrador de este Horario")
            Return
        End If

        'Aqui debería ser un correo fijo que sea el que figure para el envio de correo

        Dim cRemitente As String = "saphservicio@hotmail.com"

        Dim cAsunto As String = "Participación en la planificación de horario como administrador"
        Dim cCuerpo As String = ""
        Dim cAdjuntos As String = ""

        cAdjuntos += Parametros.REPORTS_LOGO()
        'Envio

        cCuerpo += "Hola " + drHorario("cNombreAdmin").ToString.ToUpper.Trim + "! Ha creado una propuesta de calendario." + vbCrLf
        cCuerpo += "Acceda a la siguiente dirección para administrar su propuesta de Horario: " + vbCrLf
        cCuerpo += "<a href=""" + Parametros.URL_WEB_RAIZ() + Parametros.URL_PROPUESTAHORARIO_ADMIN(cUuid) + """>" + "Pincha aqui para acceder al calendario" + "</a>" + vbCrLf + vbCrLf
        cCuerpo = "<div style=""font-family:Arial"">" + cCuerpo + "</div>" + vbCrLf
        cCuerpo = cCuerpo.Replace(vbCrLf, "<br />")

        Util.EnviarMail(cRemitente, cDestinatarios, cAsunto, cCuerpo, cAdjuntos)

    End Sub



    Protected Sub Btnvolver_Click(sender As Object, e As EventArgs) Handles Btnvolver.Click

        Try
            If Not idHorario = -1 Then
              
                If nLinkllamador = Util.LINK_LLAMADOR.PropuestaAdmin Then
                    Response.Redirect(Parametros.URL_PROPUESTAHORARIO_ADMIN(cUuid))
                Else
                    Response.Redirect(Parametros.URL_PROPUESTAHORARIO(idHorario))
                End If
            Else
                Throw New Exception("Código de Horario incorrecto: " + idHorario.ToString)
            End If
        Catch ex As Exception
            MostrarError(ex)
        End Try

    End Sub

    Protected Function ComprobarEmails(ByVal cDestinatarios As String) As Boolean

        Dim lTodosMailValidos As Boolean = True

        For Each mail As String In cDestinatarios.Split(";")

            If Not Util.EsEmailValido(mail) Then
                lTodosMailValidos = False
                Exit For
            End If

        Next
        Return lTodosMailValidos

    End Function
End Class