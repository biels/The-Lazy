Imports SelectorConnexions
Imports System.Windows.Forms

Public Class Login

    ' TODO: inserte el código para realizar autenticación personalizada usando el nombre de usuario y la contraseña proporcionada 
    ' (Consulte http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' El objeto principal personalizado se puede adjuntar al objeto principal del subproceso actual como se indica a continuación: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' donde CustomPrincipal es la implementación de IPrincipal utilizada para realizar la autenticación. 
    ' Posteriormente, My.User devolverá la información de identidad encapsulada en el objeto CustomPrincipal
    ' como el nombre de usuario, nombre para mostrar, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Login_GotFocus(sender As Object, e As System.EventArgs)
        progComprovant.NoOfCircles = 20
    End Sub

    Private Sub Login_LostFocus(sender As Object, e As System.EventArgs)
        progComprovant.NoOfCircles = 12
    End Sub
    Private Sub Login_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ' Comunicació.Disponibilitat.ComprovarDisponibilitatAsync()
        'AddHandler Comunicació.Disponibilitat.DisponibilitatActualitzada, AddressOf DispAct 'De moment no
        ' CheckForIllegalCrossThreadCalls = False
        Me.TopMost = True
        'Me.Width += 100
        'Me.Height += 100
        'Codigo para convertir el formulario a redondo
        Dim region As New System.Drawing.Drawing2D.GraphicsPath()
        region.AddEllipse(30, 30, Me.Width - 35, Me.Height - 35)
        Dim elipse As New System.Drawing.Region(region)
        Me.Region = elipse
        'OvalShape1.Top = 32 '7
        'OvalShape1.Left = 33 '30
        'OvalShape1.Height = Me.Height - 40
        'OvalShape1.Width = Me.Width - 40
        CircleColor = System.Drawing.SystemColors.Highlight
        'OvalShape1.Visible = False
        With progComprovant
            .Top = 30
            .Left = 30
            .Height = Me.Height - 35
            .Width = Me.Width - 35
            .BringToFront()
        End With
        'l.initConnection()
        ' lblAddr.Text = Client.LocalInfo.Address
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
    
    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
        End Try
    End Function

    Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
        End Try
    End Function
    Sub AbansDeLogin()

        'pnlDescarregant.Visible = True
        'pnlDescarregant.BackColor = Color.LightYellow
        'lblPercentatge.Text = "------"
        'lblMb.Text = "S'està connectant..."
        'lblPercentatge.Refresh()
        'lblMb.Refresh()
        'Dim descarr As New Descarregador
        'AddHandler descarr.DercarregaIniciada, AddressOf DercarregaIniciada
        'AddHandler descarr.DercarregaFinalitzada, AddressOf DercarregaFinalitzada
        'AddHandler descarr.ProgrésActualitzat, AddressOf ProgrésActualitzat
        'AddHandler descarr.DercarregaFallada, AddressOf DercarregaFallada
        'With descarr
        '    .URL = ArxiuDadesRemot
        '    .NomArxiu = ""
        '    .ComençaDescàrrega()
        'End With

    End Sub

    Sub Carregant(value As Boolean)
        progComprovant.Visible = value
        ' Me.Invalidate()
    End Sub





    Private Sub TextboxDinàmic2_TextChanged(sender As System.Object, e As System.EventArgs)

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
        AbansDeLogin()
        Timer1.Enabled = False
    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub lblPercentatge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

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

        ' System.Threading.Thread.Sleep(1000)
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
            UpdateMainWindow()
        End Sub))
    End Sub

    Private Sub backIntroduirUsuari_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles backIntroduirUsuari.RunWorkerCompleted
        Me.Close()
    End Sub

    Private Sub txtContrasenya_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

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
            'Dim rectSCenter As PointF = New PointF(e.ClipRectangle.Width / 2 + e.ClipRectangle.Left, e.ClipRectangle.Width / 2 + e.ClipRectangle.Top)
            'Dim gp As New Drawing2D.GraphicsPath()
            'gp.AddEllipse(e.ClipRectangle)
            'Dim pgb As New Drawing2D.PathGradientBrush(gp)

            'pgb.CenterPoint = rectSCenter
            'pgb.CenterColor = Color.White
            'pgb.Blend.Factors = New Single() {0.33, 0.33, 0.33}
            'pgb.FocusScales = New PointF(0.3F, 0.8F)
            'pgb.SurroundColors = New Color() {Color.DarkOrchid, Color.Yellow, Color.Green}


            ''G.FillPath(pgb, gp)
            ''Dim b As Brush = New TextureBrush(Entity.Icon(MasterCommon.EntityType.Archer))
            'g.FillPie(pgb, e.ClipRectangle, 0, 360)
        End If

    End Sub

    Private Sub Minibotó1_Click(sender As Object, e As EventArgs) Handles Minibotó1.Click
        Dim frm As New frmOptions
        Me.Visible = False
        frm.ShowDialog()
        Me.Visible = True
        c.connectionParametersRefreshed()
    End Sub


    Private Sub Label3_Click_1(sender As Object, e As EventArgs) Handles lblAddr.Click
        'Dim frm As New frmServerSelect
        'Me.Visible = False
        'frm.ShowDialog()
        'Me.Visible = True
    End Sub
    'Public Delegate Sub LoginConfirmDelegate(p As MasterCommon.LoginResultPacket)
    'Sub LoginConfirm(p As MasterCommon.LoginResultPacket)
    '    'If Me.InvokeRequired Then
    '    '    Invoke(New LoginConfirmDelegate(AddressOf LoginConfirm), p)
    '    'Else
    '    '    Dim Result As LoginResult = p.Result
    '    '    Carregant(False)
    '    '    Select Case Result
    '    '        Case LoginResult.Success
    '    '            Client.LocalInfo.UserName = p.Name
    '    '            Client.LocalInfo.LoggedIn = True
    '    '            Invoke(New MethodInvoker(AddressOf Me.Close))
    '    '        Case LoginResult.WrongPassword
    '    '            txtContrasenya.Text = ""
    '    '            txtContrasenya.BackColor = Color.OrangeRed
    '    '            CircleColor = Color.Orange
    '    '        Case LoginResult.UsernameNotFound
    '    '            txtUsuari.Text = ""
    '    '            txtContrasenya.Text = ""
    '    '            txtUsuari.BackColor = Color.OrangeRed
    '    '            CircleColor = Color.Orange
    '    '        Case LoginResult.Undefined
    '    '            CircleColor = Color.Gray
    '    '        Case LoginResult.Banned
    '    '            CircleColor = Color.Violet
    '    '            MsgBox("Aquest jugador està banejat!")
    '    '    End Select
    '    'End If

    'End Sub

    Private Sub txtContrasenya_TextChanged_1(sender As Object, e As EventArgs) Handles txtContrasenya.TextChanged

    End Sub


    Private Sub progComprovant_Click(sender As Object, e As EventArgs) Handles progComprovant.Click

    End Sub

    Private Sub txtUsuari_TextChanged(sender As Object, e As EventArgs) Handles txtUsuari.TextChanged

    End Sub
End Class
