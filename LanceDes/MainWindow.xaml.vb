Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation

Partial Public Class MainWindow
    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
        StoryRollDice = New Storyboard
        Me.Resources.Add("StoryRollDice", StoryRollDice)

        AddHandler Dice.Click, AddressOf Dice_Clicked
    End Sub

    Private StoryRollDice As Storyboard

    Private Sub BTReset_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles BTReset.Click
        For Each tempControl As DiceControl In StackDC.Children
            tempControl.ResetCount()
        Next
    End Sub

    Private Sub BTRoll_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles BTRoll.Click
        CanvasDice.Children.Clear()
        Dim rdm As New Random

        For Each tempControl As DiceControl In StackDC.Children
            If Not tempControl.GetCount = 0 Then
                For Index As Integer = 1 To tempControl.GetCount
                    Dim newDice As New Dice
                    newDice.RealDiceColor = tempControl.DiceColor
                    newDice.RealText = CStr(rdm.Next(1, (tempControl.DiceNumber + 1)))
                    newDice.SetTransform((tempControl.DiceNumber + 10) / 20)

                    CanvasDice.Children.Add(newDice)
                Next
            End If
        Next

        StoryRollDice.Children.Clear()
        Dim otherDiceRects = New List(Of Rect)

        Dim maxWidth As Integer = CanvasDice.ActualWidth
        Dim maxHeight As Integer = CanvasDice.ActualHeight

        For Each dice As Dice In CanvasDice.Children
            'Find a position to place the dice in 3 loop max.
            Dim posRect As Rect

            Dim posOk As Boolean = False
            Dim tryCount As Short = 0

            While (Not posOk) And tryCount < 10
                posRect = New Rect(rdm.Next(0, (maxWidth - dice.RealWidth)), rdm.Next(0, (maxHeight - dice.RealHeight)), dice.RealWidth, dice.RealHeight)

                posOk = True
                For Each otherDiceRect As Rect In otherDiceRects
                    If otherDiceRect.IntersectsWith(posRect) Then
                        posOk = False
                    End If
                Next

                tryCount += 1
            End While

            'We add the current dice's rect to the otherRects list.
            otherDiceRects.Add(posRect)

            'Prepare the animation
            Dim AnimX As New DoubleAnimationUsingKeyFrames
            Dim AnimY As New DoubleAnimationUsingKeyFrames

            Dim StartX As New EasingDoubleKeyFrame
            StartX.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))
            StartX.Value = -100

            Dim StartY As New EasingDoubleKeyFrame
            StartY.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))
            StartY.Value = 100

            Dim easingFunction As New CircleEase
            easingFunction.EasingMode = EasingMode.EaseOut

            Dim EndX As New EasingDoubleKeyFrame
            EndX.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))
            EndX.Value = posRect.X
            EndX.EasingFunction = easingFunction

            Dim EndY As New EasingDoubleKeyFrame
            EndY.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))
            EndY.Value = posRect.Y
            EndY.EasingFunction = easingFunction

            AnimX.KeyFrames.Add(StartX)
            AnimX.KeyFrames.Add(EndX)

            AnimY.KeyFrames.Add(StartY)
            AnimY.KeyFrames.Add(EndY)

            Storyboard.SetTarget(AnimX, dice)
            Storyboard.SetTarget(AnimY, dice)

            Storyboard.SetTargetProperty(AnimX, New PropertyPath("(Canvas.Left)"))
            Storyboard.SetTargetProperty(AnimY, New PropertyPath("(Canvas.Top)"))

            StoryRollDice.Children.Add(AnimX)
            StoryRollDice.Children.Add(AnimY)
        Next

        StoryRollDice.Duration = New Duration(TimeSpan.FromSeconds(1))
        StoryRollDice.Begin()
    End Sub

    Private Sub CanvasDice_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles CanvasDice.SizeChanged
        StoryRollDice.Children.Clear()

        For Each tempDice As Dice In CanvasDice.Children
            If (tempDice.GetValue(Canvas.LeftProperty) + tempDice.RealWidth) > e.NewSize.Width Then
                Dim AnimX As New DoubleAnimationUsingKeyFrames
                Dim StartX As New EasingDoubleKeyFrame
                StartX.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))
                StartX.Value = (e.NewSize.Width - tempDice.RealWidth)

                AnimX.KeyFrames.Add(StartX)
                StoryRollDice.Children.Add(AnimX)

                Storyboard.SetTarget(AnimX, tempDice)
                Storyboard.SetTargetProperty(AnimX, New PropertyPath("(Canvas.Left)"))
            End If

            If (tempDice.GetValue(Canvas.TopProperty) + tempDice.RealHeight) > e.NewSize.Height Then
                Dim AnimY As New DoubleAnimationUsingKeyFrames
                Dim StartY As New EasingDoubleKeyFrame
                StartY.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))
                StartY.Value = (e.NewSize.Height - tempDice.RealHeight)

                AnimY.KeyFrames.Add(StartY)
                StoryRollDice.Children.Add(AnimY)

                Storyboard.SetTarget(AnimY, tempDice)
                Storyboard.SetTargetProperty(AnimY, New PropertyPath("(Canvas.Top)"))
            End If
        Next

        StoryRollDice.Duration = New Duration(TimeSpan.FromSeconds(0))
        StoryRollDice.Begin()
    End Sub

    Private Sub Dice_Clicked(sender As Object, e As EventArgs)
        CanvasDice.Children.Remove(sender)
        CanvasDice.Children.Add(sender)
    End Sub
End Class
