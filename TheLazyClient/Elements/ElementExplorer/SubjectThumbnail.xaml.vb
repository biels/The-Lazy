Imports TheLazyClientMVVM.Entities

Public Class SubjectThumbnail
    Private _Subject As SubjectEntity
    Public Property Subject() As SubjectEntity
        Get
            Return _Subject
        End Get
        Set(ByVal value As SubjectEntity)
            _Subject = value
            UpdateUI()
        End Set
    End Property
    Public Sub UpdateUI()
        If Subject IsNot Nothing Then
            rectBack.Fill = New SolidColorBrush(ColorFromInt(Subject.color))
            lblShortcode.Content = Subject.shortcode
        Else
            rectBack.Fill = Brushes.Gray
            lblShortcode.Content = "#"
        End If
        
    End Sub
End Class
