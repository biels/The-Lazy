Imports TheLazyClientMVVM

Public Class ElementViewer
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
            UpdateUIRatingArea()
        End If
    End Sub
    Dim s As Boolean
    Private Sub imgFavorites_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles imgFavorites.MouseDown
        'Dim conv As New ImageSourceConverter()
        'imgFavorites.Source = conv.ConvertFromString("/TheLazyClient;component/media/Icones/Star.png")
        s = Not s
        setStarred(s)
    End Sub
    Sub setStarred(starred As Boolean)
        Dim n As String = If(starred, "Star", "Draw-Star")
        Dim image As BitmapImage = New BitmapImage()
        image.BeginInit()
        image.UriSource = New Uri("pack://application:,,,/media/Icones/" + n + ".png")
        image.EndInit()
        imgFavorites.Source = image
    End Sub


   

    Private Sub ElementViewer_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If Not Me.IsLoaded Then Return
        Dim m As Double = e.NewSize.Width - e.PreviousSize.Width
            rtnRatingControl.Width += m
    End Sub

    
End Class
