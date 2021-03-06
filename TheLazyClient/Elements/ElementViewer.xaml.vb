﻿Imports TheLazyClientMVVM

Public Class ElementViewer
    WithEvents dispatcherTimer As New Windows.Threading.DispatcherTimer()
    Private _Element As Entities.ElementEntity
    Public Property Element() As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
            InitModfiedData()
            UpdateUI()
        End Set
    End Property
    Private _ModifiedLocalData As Entities.LocalElementDataEntity
    Public Property ModifiedLocalData() As Entities.LocalElementDataEntity
        Get
            Return _ModifiedLocalData
        End Get
        Set(ByVal value As Entities.LocalElementDataEntity)
            _ModifiedLocalData = value
        End Set
    End Property
    Sub InitModfiedData()
        ModifiedLocalData = New Entities.LocalElementDataEntity
        ModifiedLocalData.favourite = _Element.local_data.favourite
        ModifiedLocalData.rating = _Element.local_data.rating
        ModifiedLocalData.purchase = _Element.local_data.purchase
    End Sub

    Public Sub LoadElement(id As Integer, Optional direct As Boolean = False)
        Title = "[Carregant...]"
        Element = c.cache.element_cache.getElement(id, direct)
    End Sub
    Private _Rating As Integer = -1
    Public Property Rating() As Integer
        Get
            Return _Rating
        End Get
        Set(ByVal value As Integer)
            _Rating = value
            rtnRatingControl.Rating = value
            UpdateUIRatingArea()
            ModifiedLocalData.rating = value
        End Set
    End Property
    Sub ClearRating()
        Rating = -1
        Rating = -1
    End Sub
    Function IsRated()
        Return Rating <> -1
    End Function
    Sub UpdateUIRatingArea()
        lblCurrentRating.Content = If(IsRated, (Rating / 100), "[-,--]")
        lblCurrentRating.Foreground = If(IsRated, ColorForRating(Rating / 100), Brushes.Gray)
        imgClearRating.Visibility = If(IsRated, Windows.Visibility.Visible, Windows.Visibility.Collapsed)
    End Sub
    Private Sub rtnRatingControl_ValueChanged(value As Double) Handles rtnRatingControl.ValueChanged
        Rating = value
    End Sub
    Private Sub imgClearRating_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles imgClearRating.MouseDown
        ClearRating()
    End Sub
    Private Sub UpdateUI()
        If Element IsNot Nothing Then
            Title = Element.name
            lblTitle.Content = Element.name
            lblDescription.Text = Element.description
            lblPrice.Content = Element.price
            lblAcademicCenter.Content = Element.user.education_center.name
            lblAcademicLevel.Content = Element.subject.academic_level.name
            lblSubject.Content = Element.subject
            lblCreatorName.Text = String.Format("{0} ({1}) @ {2} ({3}){4}", Element.user.real_name, Element.user.username, Element.create_time, TimeAgo(Element.create_time), If(Element.hasBeenModified, String.Format(", modificat el {0} ({1})", Element.update_time, TimeAgo(Element.update_time)), "")) 'Biel Simon (biel) @ 22/02/2015 - fa 5 dies
            lblTotalPurchases.Content = Element.purchase_amount
            lblAvgRating.Content = Element.average_rating / 100
            lblAvgRating.ToolTip = "Mitjana ponderada intel·ligent basada en les qualificacions dels usuaris" & vbCrLf & If(Element.rating_amount = 0, "No hi ha cap qualificació", String.Format("Basada en l'opinió {2}{0} {1}", Element.rating_amount, If(Element.rating_amount = 1, "usuari", "usuaris"), If(Element.rating_amount = 1, "d'", "de ")))
            setStarred(ModifiedLocalData.favourite)
            lblFavouriteAmount.Content = Element.favourite_amount + FavouriteIncrement()
            Rating = ModifiedLocalData.rating
            UpdateUIRatingArea()
            UpdateCommentList()
            UpdateBuyButtonUI()
        End If
    End Sub
    Private Sub imgFavorites_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles imgFavorites.MouseDown
        'Dim conv As New ImageSourceConverter()
        'imgFavorites.Source = conv.ConvertFromString("/TheLazyClient;component/media/Icones/Star.png")
        ModifiedLocalData.favourite = Not ModifiedLocalData.favourite
        UpdateUI()
    End Sub
    Function FavouriteIncrement() As Integer
        If Element.local_data.favourite = False And ModifiedLocalData.favourite = True Then
            Return 1
        End If
        If Element.local_data.favourite = True And ModifiedLocalData.favourite = False Then
            Return -1
        End If
        Return 0
    End Function
    Sub UpdateBuyButtonUI()
        Dim balance As Integer = c.localUser.balance
        Dim required As Integer
        If ModifiedLocalData.isUnlocked Then
            btnBuy.IsEnabled = True
            btnBuy.Background = Brushes.Orange
            btnBuy.Content = "Obrir"
            Exit Sub
        End If
        If Element.isFromLocalUser Then
            btnBuy.IsEnabled = True
            btnBuy.Background = Brushes.DeepSkyBlue
            btnBuy.Content = "Veure element propi"
            Exit Sub
        End If
        If Element.price > c.localUser.balance Then
            btnBuy.IsEnabled = False
            required = Element.price - balance
            btnBuy.Content = "Et falten " + required.ToString + " monedes"
        End If
    End Sub
    Sub setStarred(starred As Boolean)
        Dim n As String = If(starred, "Star", "Draw-Star")
        Dim image As BitmapImage = New BitmapImage()
        image.BeginInit()
        image.UriSource = New Uri("pack://application:,,,/media/Icones/" + n + ".png")
        image.EndInit()
        imgFavorites.Source = image
    End Sub

    Private Sub ElementViewer_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        SaveLocalInfo()
        GetMainWindow.UpdateUI()
    End Sub

    Private Sub ElementViewer_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Activate()
        dispatcherTimer.Interval = New TimeSpan(0, 0, 25)
        dispatcherTimer.Start()

    End Sub
    Private Sub ElementViewer_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If Not Me.IsLoaded Then Return
        Dim m As Double = e.NewSize.Width - e.PreviousSize.Width
        If e.NewSize.Width < 600 Then
            e.Handled = True
        End If
        If rtnRatingControl.Width + m > 0 Then
            rtnRatingControl.Width += m
        End If
    End Sub
    Sub SaveLocalInfo()
        If ModifiedLocalData.GetHashCode <> (Element.local_data).GetHashCode Then
            DbClient.DbElementClient.setFavourite(c.localUser.id, Element.id, ModifiedLocalData.favourite)
            DbClient.DbElementClient.setElementRating(c.localUser.id, Element.id, ModifiedLocalData.rating)
        End If
        Task.Run(AddressOf UpdateCache)
    End Sub
    Sub UpdateCache()
        c.cache.element_cache.getElement(Element.id, True)
    End Sub
    Public Sub FullReload()
        SaveLocalInfo()
        LoadElement(Element.id, True)
    End Sub
    Private Sub dispatcherTimer_Tick(sender As Object, e As EventArgs) Handles dispatcherTimer.Tick
        SaveLocalInfo()
        UpdateCommentList()
    End Sub
    'COMMENTS
    Dim editing_comment As Entities.ElementCommentEntity = Nothing
    Sub UpdateCommentList()
        With pnlComments.Children
            .Clear()
            For Each c As Entities.ElementCommentEntity In DbClient.DbElementClient.getElementCommentList(Element.id)
                Dim control As New CommentDisplayControl With {.Comment = c}
                AddHandler control.EditCommentClick, AddressOf EditCommentClick
                AddHandler control.DeleteCommentClick, AddressOf DeleteCommentClick
                If control.txtText.Text.Length > 50 Then
                    control.Height += 14
                End If
                If control.txtText.Text.Length > 120 Then
                    control.Height += 16
                End If
                .Add(control)
            Next
        End With
    End Sub
    Function IsEditing() As Boolean
        Return editing_comment IsNot Nothing
    End Function
    Sub EditCommentClick(Comment As Entities.ElementCommentEntity)
        editing_comment = Comment
        UpdateCommentEnteringUI()
    End Sub
    Sub DeleteCommentClick(Comment As Entities.ElementCommentEntity)
        If MsgBox("Estas segur que vols esborrar el teu comentari?" & vbCrLf & Comment.text, MsgBoxStyle.YesNo, "Esborra el comentari") = MsgBoxResult.Yes Then
            DbClient.DbElementClient.deleteElementComment(Comment.id)
        End If
        UpdateCommentList()
    End Sub
    Sub UpdateCommentEnteringUI()
        txtIntroduceComment.Text = If(Not IsEditing(), "", editing_comment.text)
        btnCancelComment.Content = If(Not IsEditing(), "Neteja", "Cancel·la")
        btnSendComment.Content = If(Not IsEditing(), "Envia", "Edita")
    End Sub
    Function VerifyComment() As Boolean
        'Comprova errors
        Dim err As New List(Of String)
        If txtIntroduceComment.Text.Trim.Length < 5 Then err.Add("El comentari ha de contenir com a mínim 5 caràcters.")
        If txtIntroduceComment.Text.Length > 195 Then err.Add("El comentari pot contenir com a màxim 200 caràcters.")
        If err.Count > 0 Then
            Dim s As String = "No es pot completar l'operació, detalls: "
            For Each err_m As String In err
                s = s & vbCrLf & "  + " & err_m
            Next
            MsgBox(s)
            Return False
            Exit Function
        End If
        Return True
    End Function
    Sub PostComment()
        If VerifyComment() Then
            If IsEditing() Then
                DbClient.DbElementClient.updateElementComment(editing_comment.id, txtIntroduceComment.Text)
                editing_comment = Nothing
            Else
                DbClient.DbElementClient.insertElementComment(c.localUser.id, Element.id, txtIntroduceComment.Text)
            End If
        End If
        UpdateCommentEnteringUI()
        UpdateCommentList()
    End Sub

    Private Sub btnCancelComment_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelComment.Click
        editing_comment = Nothing
        UpdateCommentEnteringUI()
    End Sub
    Sub Button_click(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub btnSendComment_Click(sender As Object, e As RoutedEventArgs) Handles btnSendComment.Click
        PostComment()
    End Sub

    Private Sub txtIntroduceComment_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtIntroduceComment.GotFocus
        If txtIntroduceComment.Text = "Introdueix un comentari..." Then
            txtIntroduceComment.Clear()
        End If
    End Sub

    Private Sub txtIntroduceComment_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIntroduceComment.KeyDown
        If e.Key = Key.Enter Then
            PostComment()
        End If
    End Sub

    Private Sub txtIntroduceComment_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtIntroduceComment.TextChanged

    End Sub

    Private Sub btnBuy_Click(sender As Object, e As RoutedEventArgs) Handles btnBuy.Click
        If ModifiedLocalData.isUnlocked Or Element.isFromLocalUser Then
            Dim frm = New ElementExplorer()
            frm.Show()
            Exit Sub
        End If
        If Not ModifiedLocalData.isUnlocked Then
            Dim dr As Microsoft.VisualBasic.MsgBoxResult = MsgBox(String.Format("Estas segur que vols comprar aquest element? Aquesta acció costa {0} cr.", Element.price), MsgBoxStyle.YesNo)
            If dr = MsgBoxResult.Yes Then
                Dim r As Integer = DbClient.DbElementClient.buyElement(c.localUser.id, Element.id)
                'CODIS: [-1] Invalid ElementID, [-2] Not enough money, [-3] Danger zone break, [-4] Buy yourself attempt, [-5] Already bought, [>0] Done, out = purchaseID
                Select Case r
                    Case Is > 0
                        MsgBox("Has desbloquejat l'element!", MsgBoxStyle.Information)
                    Case Is = -1
                        MsgBox("Error: Element invàlid.", MsgBoxStyle.Critical)
                    Case Is = -2
                        MsgBox("Crèdits insuficients, operació anul·lada.", MsgBoxStyle.Information)
                    Case Is = -3
                        MsgBox("Hi ha hagut un error en l'operació", MsgBoxStyle.Exclamation)
                    Case Is = -4
                        MsgBox("No pots comprar el teu propi element!", MsgBoxStyle.Exclamation)
                    Case Is = -5
                        MsgBox("Ja has comprat aquest element anteriorment, operació anul·lada.", MsgBoxStyle.Information)
                End Select
                FullReload()
            End If
        End If
    End Sub
  
End Class
