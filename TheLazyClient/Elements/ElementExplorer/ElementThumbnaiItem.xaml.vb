﻿Imports TheLazyClientMVVM
Public Class ElementThumbnaiItem
    Private _Element As Entities.ElementEntity
    Public Property Element() As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
            UpdateUI()
        End Set
    End Property
    Sub UpdateUI()
        txtTitle.Content = Element.name
        lblDescription.Content = Element.description
        lblPrice.Content = Element.price
        lblAcademicLevel.Content = Element.subject.academic_level
        lblSubjectTag.Content = Element.subject.shortcode
        lblSubjectTag.Foreground = If(Element.subject.color = 0, Brushes.Black, New SolidColorBrush(ColorFromInt(Element.subject.color)))
        imgPriceIcon.Visibility = If(Element.price = 0, Windows.Visibility.Collapsed, Windows.Visibility.Visible)
        lblPrice.Visibility = imgPriceIcon.Visibility
        imgFavourite.Visibility = If(Element.local_data.favourite, Windows.Visibility.Visible, Windows.Visibility.Collapsed)
        imgEditGo.Visibility = If(Element.isFromLocalUser, Windows.Visibility.Visible, Windows.Visibility.Collapsed)
        grdGrid.Background = If(Element.isFromLocalUser, New SolidColorBrush(Windows.Media.Color.FromRgb(90, 200, 255)), New SolidColorBrush(Windows.Media.Color.FromRgb(255, 246, 205)))
        UpdateWidth()
    End Sub
    Sub UpdateWidth()
        Try
            Me.Width = DirectCast(Me.Parent, StackPanel).ActualWidth
        Catch ex As Exception
            Me.Width = 604 'Valor *Hardcoded*
        End Try
    End Sub

    Sub OpenElementView()        
        Dim frm As New ElementViewer
        frm.LoadElement(Element.id)
        frm.WindowStartupLocation = WindowStartupLocation.CenterScreen
        frm.Show()
        UpdateMainWindow()
    End Sub
    Private Sub ElementThumbnaiItem_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDown
        e.Handled = True
        OpenElementView()

    End Sub

    Private Sub ElementThumbnaiItem_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseUp
        'MsgBox("s")
    End Sub

    Private Sub imgEditGo_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles imgEditGo.MouseDown
        e.Handled = True
        Dim frm As New ElementEditor
        frm.WindowStartupLocation = WindowStartupLocation.CenterScreen
        frm.Element = Element
        frm.IsNew = False
        frm.Show()
    End Sub
End Class
