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

Partial Public Class DiceCounter
    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
        DiceNumber = 0
    End Sub

    Public Shared ReadOnly DiceNumberProperty As DependencyProperty = DependencyProperty.Register("DiceNumber", GetType(Integer), GetType(DiceCounter), New PropertyMetadata())
    Public Property DiceNumber As Integer
        Get
            Return CType(GetValue(DiceNumberProperty), Integer)
        End Get
        Set(ByVal value As Integer)
            If value >= 0 And value < 30 Then
                SetValue(DiceNumberProperty, value)
            End If
        End Set
    End Property

    Private Sub GridAdd_MouseLeftButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles GridAdd.MouseLeftButtonUp
        DiceNumber += 1
    End Sub

    Private Sub GridSub_MouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles GridSub.MouseLeftButtonDown
        DiceNumber -= 1
    End Sub
End Class
