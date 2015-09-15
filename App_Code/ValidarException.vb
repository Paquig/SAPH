Imports Microsoft.VisualBasic
Imports System.Runtime.Serialization

Public Class ValidarException
    Inherits System.ApplicationException
    Protected _controlID As String = Nothing
    Protected _controlObject As WebControl = Nothing

    Public Property controlID() As String
        Get
            Return _controlID
        End Get
        Set(ByVal value As String)
            _controlID = value
        End Set
    End Property

    Public Property controlObject() As WebControl
        Get
            Return _controlObject
        End Get
        Set(ByVal value As WebControl)
            _controlObject = value
        End Set
    End Property

    Public Sub New(ByVal new_message As String, Optional ByVal inner_exception As Exception = Nothing)
        MyClass.New(new_message, "", inner_exception)
    End Sub

    Public Sub New(ByVal new_message As String, ByVal pControlObject As WebControl, Optional ByVal inner_exception As Exception = Nothing)
        MyBase.New(new_message, inner_exception)
        controlObject = pControlObject
        controlID = pControlObject.ID
    End Sub
    Public Sub New(ByVal new_message As String, ByVal pControlID As String, Optional ByVal inner_exception As Exception = Nothing)
        MyBase.New(new_message, inner_exception)
        controlObject = Nothing
        controlID = pControlID
    End Sub
End Class