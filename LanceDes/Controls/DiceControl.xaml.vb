Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Partial Public Class DiceControl
    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
    End Sub

    Public Shared ReadOnly DiceColorProperty As DependencyProperty = DependencyProperty.Register("DiceColor", GetType(SolidColorBrush), GetType(DiceControl), New PropertyMetadata())
    Public Property DiceColor As SolidColorBrush
        Get
            Return CType(GetValue(DiceColorProperty), SolidColorBrush)
        End Get
        Set(ByVal value As SolidColorBrush)
            SetValue(DiceColorProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TextProperty As DependencyProperty = DependencyProperty.Register("Text", GetType(String), GetType(DiceControl), New PropertyMetadata())
    Public Property Text As String
        Get
            Return CType(GetValue(TextProperty), String)
        End Get
        Set(ByVal value As String)
            SetValue(TextProperty, value)
        End Set
    End Property

    Public Property DiceNumber As Integer

    Public Sub ResetCount()
        DCMain.DiceNumber = 0
    End Sub

    Public Function GetCount() As Integer
        Return DCMain.DiceNumber
    End Function
End Class
