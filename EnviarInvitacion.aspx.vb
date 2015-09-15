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
            ' si viene del link del correo...averiguamos el idhorario de la tabla recursos
            AsignarConexionBD()
            If cuuid.Length > 5 Then
                Dim dr As DataRow = dSaph.Get_Horario_UUid(cuuid)
                idHorario = dr("idHorario")
                nLinkllamador = Util.LINK_LLAMADOR.PropuestaAdmin
            Else
                idHorario = Convert.ToInt32(cuuid)

            End If

            '   idHorario = Convert.ToInt32(Util.QueryString_ObtenerIdHorario(Request))
            '  AsignarConexionBD()

            'Cogemos los datos del horario
            drHorario = dSaph.Get_Horario(idHorario)

            If Not IsPostBack Then

                Dim cTitulo As String = drHorario("cTituloHorario").ToString.Trim + IIf(Not Util.EsVacio(drHorario("cSubtituloHorario")), " - " + drHorario("cSubtituloHorario").ToString.Trim, "")

                TxtMensaje.Text = drHorario("cNombreAdmin").ToString.ToUpper.Trim
                TxtMensaje.Text += " le invita a participar en la planificación del Horario: " + vbCrLf + _
                            cTitulo + vbCrLf + _
                            "por considerarle persona interesada." + vbCrLf + vbCrLf

            End If
        Catch ex As Exception
            MostrarError(ex)
        End Try


    End Sub

    Protected Sub BtnFinaliza_Click(sender As Object, e As EventArgs) Handles BtnFinaliza.Click

        Try
            ' If Not Util.EsVacio(TxtDestinatarios.Text) Then

            If ComprobarEmails(TxtDestinatarios.Text) Then
                'Voy a  generar el uuid del horario correspondiente, a no ser que esté reinvitando

                If Util.EsVacio(drHorario("cuuid").ToString.Trim) Then
                    cUuid = Util.GeneraGUID()
                Else
                    cUuid = drHorario("cuuid").ToString.Trim
                End If

                ' Enviar los emails destinatarios
                If Not Util.EsVacio(TxtDestinatarios.Text) Then
                    EnvioEmails()
                Else
                    MostrarError(New Exception("No se ha enviado ninguna invitación "), Nothing, Util.MENSAJE_TIPO.MT_INFO)
                End If

                'Envio email al admin
                EnvioEmailAdmin()

                'Actualizo el horario con el nuevo uuid.u
                dSaph.UpdateHorarioUuid(idHorario, cUuid)
                'Finalizar
                ' Response.Redirect(Parametros.URL_PAGINAFINAL(idHorario))
                Response.Redirect(Parametros.URL_PAGINAFINAL(cUuid))
            Else
                Throw New Exception("Dirección de correo electronico no valida, el correo debe tener el formato: nombre@dominio.xxx;...")
            End If

            '  Else
            '    Throw New Exception(" Se debe indicar los correos electrónicos de los Destinatarios.")
            ' End If

        Catch ex As Exception
            MostrarError(ex)
        End Try

    End Sub

    Protected Sub EnvioEmails()

        Dim cDestinatarios As String = ""

        cDestinatarios = TxtDestinatarios.Text.Trim

        'If Util.EsVacio(cDestinatarios) Then
        '    Throw New Exception(" No se ha enviado correo porque no se han obtenido Destinatarios.")
        '    Return
        'End If

        Dim cRemitente As String = drHorario("cemailadmin").ToString.Trim

        '  "pgtebar@hotmail.com"

        Dim cAsunto As String = "Participación en la planificación de horario"
        Dim cCuerpo As String = ""
        Dim cAdjuntos As String = ""
        ' cAdjuntos += Parametros.PATH_WEBRAIZ + "images\logosaph.png"
        cAdjuntos += Parametros.REPORTS_LOGO()
        'Envio
        '  En el cuerpo voy a poner el mensaje
        cCuerpo += TxtMensaje.Text.ToString

        cCuerpo += "Acceda a la siguiente dirección para su tratamiento: " + vbCrLf
        ' cCuerpo += "<a href=""" + Parametros.URL_WEB_RAIZ + Parametros.URL_PROPUESTAHORARIO(idHorario) + """>" + "Participa en el Calendario" + "</a>" + vbCrLf + vbCrLf
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

        'Aqui debería ser un correo fijo mailer@saph.com que sea el que figure para el envio de correo
        Dim cRemitente As String = drHorario("cemailadmin").ToString.Trim

        Dim cAsunto As String = "Participación en la planificación de horario como administrador"
        Dim cCuerpo As String = ""
        Dim cAdjuntos As String = ""
        ' cAdjuntos += Parametros.PATH_WEBRAIZ() + "images\logosaph.png"
        cAdjuntos += Parametros.REPORTS_LOGO()
        'Envio

        cCuerpo += "Hola " + drHorario("cNombreAdmin").ToString.ToUpper.Trim + "! Ha creado una propuesta de calendario." + vbCrLf
        cCuerpo += "Acceda a la siguiente dirección para administrar su propuesta de Horario: " + vbCrLf
        '        cCuerpo += "<a href=""" + Parametros.URL_WEB_RAIZ() + Parametros.URL_PROPUESTAHORARIO_ADMIN(idHorario) + """>" + "Pincha aqui para acceder al calendario" + "</a>" + vbCrLf + vbCrLf

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