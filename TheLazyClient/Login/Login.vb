Imports SelectorConnexions
Imports System.Windows.Forms

Public Class Login

    ' Formulari de login by Biel, en Windows Forms @ .NET 4.5
    Private Sub Login_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.TopMost = True
        'Formulari rodó
        Dim region As New System.Drawing.Drawing2D.GraphicsPath()
        region.AddEllipse(30, 30, Me.Width - 35, Me.Height - 35)
        Dim elipse As New System.Drawing.Region(region)
        Me.Region = elipse

        CircleColor = System.Drawing.SystemColors.Highlight

        With progComprovant
            .Top = 30
            .Left = 30
            .Height = Me.Height - 35
            .Width = Me.Width - 35
            .BringToFront()
        End With
        'USER/PASS SAVING >--<
        RetrieveSavedLoginInfo()
    End Sub
    Function pLoginInfo() As BielUtils.GestorPropietats
        Return New BielUtils.GestorPropietats(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\logininfo.lck")
    End Function
    Sub RetrieveSavedLoginInfo()
        If pLoginInfo.P("SaveUser") = "true" Then
            chkRemeberName.Checked = True
            txtUsuari.Text = pLoginInfo.P("User")
        End If
        If pLoginInfo.P("SavePass") = "true" Then
            chkRememberPassword.Checked = True
            txtContrasenya.UseSystemPasswordChar = True
            txtContrasenya.Text = pLoginInfo.P("Pass")
        End If
    End Sub
    Sub SaveLoginInfo()
        pLoginInfo.P("SaveUser") = If(chkRemeberName.Checked, "true", "false")
        pLoginInfo.P("SavePass") = If(chkRememberPassword.Checked, "true", "false")
        If pLoginInfo.P("SaveUser") = "true" Then           
            pLoginInfo.P("User") = txtUsuari.Text
        End If
        If pLoginInfo.P("SavePass") = "true" Then
            pLoginInfo.P("Pass") = txtContrasenya.Text
        End If
    End Sub
    Sub Carregant(value As Boolean)
        progComprovant.Visible = value
        ' Me.Invalidate()
    End Sub

    Sub CrearCompte()
        Dim frm As New frmRegister
        Me.Visible = False
        frm.ShowDialog()
        Me.Visible = True
    End Sub



    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click
        Label1.ForeColor = System.Drawing.Color.Gold
        Label1.Refresh()
        System.Threading.Thread.Sleep(200)
        Label1.ForeColor = System.Drawing.Color.White
        CrearCompte()
    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click
        Me.Close()
        End
    End Sub
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
    End Sub
    'Verificar
    Private Sub TextboxDinàmic2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsuari.KeyPress, txtContrasenya.KeyPress
        If e.KeyChar = Chr(13) Then
            Carregant(True)
            If Not backVerificarUsuari.IsBusy Then
                Me.TopMost = False
                backVerificarUsuari.RunWorkerAsync({txtUsuari.Text, txtContrasenya.Text})
                Me.TopMost = True
            End If
            'VerificarUsuari(txtUsuari.Text, txtContrasenya.Text)

        End If
    End Sub
    Private Sub backVerificarUsuari_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles backVerificarUsuari.DoWork
        VerificarUsuari(e.Argument(0), e.Argument(1))
    End Sub
    Private Sub backVerificarUsuari_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles backVerificarUsuari.RunWorkerCompleted
        Dim LoginCorrecte As Boolean = c.loginManager.loggedIn
        Carregant(LoginCorrecte)
        If LoginCorrecte = False Then
            CircleColor = System.Drawing.Color.Red
        Else
            CircleColor = System.Drawing.Color.Green
            progComprovant.NoOfCircles = 15
            SaveLoginInfo()
            backIntroduirUsuari.RunWorkerAsync(txtUsuari.Text)
        End If
    End Sub
    Public Sub VerificarUsuari(Name As String, Password As String)
        'Client.Box.SendPacket(New MasterCommon.LoginDataPacket With {.CUID = Client.LocalInfo.CUID, .Name = Name, .Password = Password})
        I("Verificant usuari...")
        If c.onlineMode Then
            c.loginManager.login(Name, Password)
        Else
            MsgBox("No es pot connectar al servidor, comprova els paràmetres de connexió i que aquest sigui accesible.")
        End If

    End Sub
    'Introduir
    Private Sub backIntroduirUsuari_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles backIntroduirUsuari.DoWork
        'IntroduirUsuari(e.Argument)
        c.getHeadingInfo()
        c.initChatService()
        Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, New Action(
        Sub()
            GetMainWindow().UpdateUI()
        End Sub))
        Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, New Action(
        Sub()
            GetMainWindow().UpdateHeavyElements()
        End Sub))
    End Sub

    Private Sub backIntroduirUsuari_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles backIntroduirUsuari.RunWorkerCompleted
        Me.Close()
    End Sub

    Private _CircleColor As System.Drawing.Color
    Public Property CircleColor() As System.Drawing.Color
        Get
            Return _CircleColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _CircleColor = value
            Me.Invalidate()
        End Set
    End Property

    Private Sub Login_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        If TypeOf sender Is Form Then
            Dim g As System.Drawing.Graphics = e.Graphics
            g.DrawEllipse(New System.Drawing.Pen(CircleColor, 16), e.ClipRectangle)
        End If
    End Sub
    Private Sub Minibotó1_Click(sender As Object, e As EventArgs) Handles Minibotó1.Click
        Dim frm As New frmOptions
        Me.Visible = False
        frm.ShowDialog()
        Me.Visible = True
        c.connectionParametersRefreshed()
    End Sub
End Class
