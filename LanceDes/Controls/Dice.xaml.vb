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

Partial Public Class Dice
    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
    End Sub

    Private currentMultiplier As Double = 1

    Public Shared ReadOnly RealDiceColorProperty As DependencyProperty = DependencyProperty.Register("RealDiceColor", GetType(SolidColorBrush), GetType(Dice), New PropertyMetadata())
    Public Property RealDiceColor As SolidColorBrush
        Get
            Return CType(GetValue(RealDiceColorProperty), SolidColorBrush)
        End Get
        Set(ByVal value As SolidColorBrush)
            SetValue(RealDiceColorProperty, value)
        End Set
    End Property

    Public Shared ReadOnly RealTextProperty As DependencyProperty = DependencyProperty.Register("RealText", GetType(String), GetType(Dice), New PropertyMetadata())
    Public Property RealText As String
        Get
            Return CType(GetValue(RealTextProperty), String)
        End Get
        Set(ByVal value As String)
            SetValue(RealTextProperty, value)
        End Set
    End Property

    Public Sub SetTransform(ByVal multiplier As Double)
        Dim scaleTransform As New ScaleTransform
        scaleTransform.ScaleX = multiplier
        scaleTransform.ScaleY = multiplier

        currentMultiplier = multiplier

        LayoutRoot.RenderTransform = scaleTransform
    End Sub

    Public ReadOnly Property RealWidth
        Get
            Return Me.Width * currentMultiplier
        End Get
    End Property

    Public ReadOnly Property RealHeight
        Get
            Return Me.Height * currentMultiplier
        End Get
    End Property

    Public Shared Event Click As EventHandler
    Private Sub LayoutRoot_MouseLeftButtonUp(sender As Object, e As System.Windows.Input.MouseButtonEventArgs) Handles LayoutRoot.MouseLeftButtonUp
        RaiseEvent Click(Me, Nothing)
    End Sub
End Class
