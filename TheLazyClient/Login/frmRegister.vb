Public Class frmRegister

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim err As New List(Of String)
        If txtUser.IsEmpty Then err.Add("Indica un nom d'usuari")
        If txtUser.Text.Length < 3 Then err.Add("El nom d'usuari ha de contenir com a mínim 3 caràcters")
        If txtUser.Text.Length > 16 Then err.Add("El nom d'usuari pot contenir com a màxim 16 caràcters")

        If txtPssw.IsEmpty Then err.Add("Introdueix una contrasenya")
        If txtPssw.Text.Length < 4 Then err.Add("La contrasenya ha de contenir com a mínim 4 caràcters")
        If txtPssw.Text.Length > 16 Then err.Add("La contrasenya pot contenir com a màxim 16 caràcters")

        If txtRealName.IsEmpty Then err.Add("Introdueix el teu nom real")
        If txtRealName.Text.Length < 8 Then err.Add("El nom real ha de contenir com a mínim 8 caràcters, Nom i Cognoms")
        If txtRealName.Text.Length > 40 Then err.Add("El nom real pot contenir com a màxim 40 caràcters")

        If txtEmail.IsEmpty Then err.Add("Introdueix una aderça de correu electrònic")
        If txtEmail.Text.Length < 8 And txtEmail.Text <> "test" Then err.Add("El correu ha de contenir com a mínim 8 caràcters")
        If txtEmail.Text.Length > 32 Then err.Add("El correu pot contenir com a màxim 16 caràcters")
        If Not (txtEmail.Text = "test" Or txtEmail.IsEmpty Or (txtEmail.Text.Contains("@") And txtEmail.Text.EndsWith(".com") And (txtEmail.Text.Contains("gmail") Or txtEmail.Text.Contains("outlook") Or txtEmail.Text.Contains("msn") Or txtEmail.Text.Contains("google") Or txtEmail.Text.Contains("hotmail")))) Then err.Add("L'adreça de correu electrònic no és vàlida.")

        If err.Count > 0 Then
            Dim s As String = "No es pot completar l'operació, detalls: "
            For Each err_m As String In err
                s = s & vbCrLf & "  + " & err_m
            Next
            MsgBox(s)
            Exit Sub
        End If
        If c.loginManager.register(txtUser.Text, txtPssw.Text, txtRealName.Text, txtEmail.Text) Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            MsgBox("Benvingut/da " & txtUser.Text & "! " & " T'has registrat correctament!" & vbCrLf & "Completa la informació del teu perfil a la pestanya perfil per disposar de totes les funcions.")
            Me.Close()
        Else
            MsgBox("Ja existeix un compte amb aquest nom d'usuari. Has de triar-ne un altre :/")
        End If

    End Sub


    Private Sub txtUser_TextChanged(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtUser.KeyPress, txtPssw.KeyPress, txtEmail.KeyPress
        If e.KeyChar = Chr(13) Then
            btnRegister_Click(sender, e)
        End If
    End Sub

    Private Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class