Public Class ProfileEditor

    Private Sub btnSelectGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnSelectGroup.Click
        Dim frm As New GroupEditor
        frm.Init()
        frm.UpdateUI()
        frm.ShowDialog()
        If frm.SelectedGroup IsNot Nothing Then
            'Establir el grup seleccionat
        End If
    End Sub
End Class
