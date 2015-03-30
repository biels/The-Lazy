Imports TheLazyClientMVVM

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
    End Sub

    Public Sub LoadElement(id As Integer)
        Element = DbClient.DbElementClient.getElementInfo(id)
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
            lblTitle.Content = Element.name
            lblDescription.Content = Element.description
            lblPrice.Content = Element.price
            lblAcademicCenter.Content = Element.user.education_center.name
            lblAcademicLevel.Content = Element.subject.academic_level.name
            lblSubject.Content = Element.subject
            lblCreatorName.Content = Element.user.username
            setStarred(ModifiedLocalData.favourite)
            lblFavouriteAmount.Content = Element.favourite_amount + FavouriteIncrement()
            Rating = ModifiedLocalData.rating
            UpdateUIRatingArea()
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
    End Sub

    Private Sub ElementViewer_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Activate()
        dispatcherTimer.Interval = New TimeSpan(0, 0, 25)
        dispatcherTimer.Start()
    End Sub
    Private Sub ElementViewer_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If Not Me.IsLoaded Then Return
        Dim m As Double = e.NewSize.Width - e.PreviousSize.Width
        rtnRatingControl.Width += m
    End Sub
    Sub SaveLocalInfo()
        ' If ModifiedLocalData.GetHashCode <> (Element.local_data).GetHashCode Then
        DbClient.DbElementClient.setFavourite(c.localUser.id, Element.id, ModifiedLocalData.favourite)
        DbClient.DbElementClient.setElementRating(c.localUser.id, Element.id, ModifiedLocalData.rating)
        ' End If
    End Sub

    Private Sub dispatcherTimer_Tick(sender As Object, e As EventArgs) Handles dispatcherTimer.Tick
        SaveLocalInfo()
    End Sub
End Class
