Imports BielUtils

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.progComprovant = New Owf.Controls.OwfProgressControl(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.backVerificarUsuari = New System.ComponentModel.BackgroundWorker()
        Me.backIntroduirUsuari = New System.ComponentModel.BackgroundWorker()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.lblAddr = New System.Windows.Forms.Label()
        Me.Minibotó1 = New Minibotó()
        Me.txtUsuari = New TextboxDinàmic()
        Me.txtContrasenya = New TextboxDinàmic()
        CType(Me.Minibotó1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'progComprovant
        '
        Me.progComprovant.AnimationSpeed = CType(100, Short)
        Me.progComprovant.BackColor = System.Drawing.SystemColors.Control
        Me.progComprovant.CirclesColor = System.Drawing.Color.Black
        Me.progComprovant.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.25!)
        Me.progComprovant.IsTransperant = False
        Me.progComprovant.Location = New System.Drawing.Point(171, 271)
        Me.progComprovant.Name = "progComprovant"
        Me.progComprovant.NoOfCircles = 6
        Me.progComprovant.Size = New System.Drawing.Size(79, 80)
        Me.progComprovant.TabIndex = 7
        Me.progComprovant.TitileText = "Comprovant..."
        Me.progComprovant.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.25!)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(13, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(394, 25)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "REGISTRA'T"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.25!)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(13, 309)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(394, 44)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "SURT"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'backVerificarUsuari
        '
        '
        'backIntroduirUsuari
        '
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!)
        Me.CheckBox1.Location = New System.Drawing.Point(82, 264)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(127, 21)
        Me.CheckBox1.TabIndex = 10
        Me.CheckBox1.Text = "Recorda el nom"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!)
        Me.CheckBox2.Location = New System.Drawing.Point(82, 285)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(185, 21)
        Me.CheckBox2.TabIndex = 12
        Me.CheckBox2.Text = "Recorda la contrassenya"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'lblAddr
        '
        Me.lblAddr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.lblAddr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblAddr.Location = New System.Drawing.Point(121, 87)
        Me.lblAddr.Name = "lblAddr"
        Me.lblAddr.Size = New System.Drawing.Size(167, 16)
        Me.lblAddr.TabIndex = 13
        Me.lblAddr.Text = "ordinadorcasa.no-ip.org"
        Me.lblAddr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Minibotó1
        '
        Me.Minibotó1.Image = CType(resources.GetObject("Minibotó1.Image"), System.Drawing.Image)
        Me.Minibotó1.Location = New System.Drawing.Point(273, 264)
        Me.Minibotó1.Name = "Minibotó1"
        Me.Minibotó1.Resaltament = 3
        Me.Minibotó1.Size = New System.Drawing.Size(59, 35)
        Me.Minibotó1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Minibotó1.TabIndex = 11
        Me.Minibotó1.TabStop = False
        '
        'txtUsuari
        '
        Me.txtUsuari.BackColor = System.Drawing.Color.White
        Me.txtUsuari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsuari.ColorFocus = System.Drawing.Color.Empty
        Me.txtUsuari.ÉsContrassenya = False
        Me.txtUsuari.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.25!)
        Me.txtUsuari.ForeColor = System.Drawing.Color.Green
        Me.txtUsuari.Location = New System.Drawing.Point(13, 106)
        Me.txtUsuari.Name = "txtUsuari"
        Me.txtUsuari.Size = New System.Drawing.Size(381, 68)
        Me.txtUsuari.TabIndex = 2
        Me.txtUsuari.Text = "Usuari"
        Me.txtUsuari.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtUsuari.TextIndicatiu = "Usuari"
        '
        'txtContrasenya
        '
        Me.txtContrasenya.BackColor = System.Drawing.Color.White
        Me.txtContrasenya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContrasenya.ColorFocus = System.Drawing.Color.Empty
        Me.txtContrasenya.ÉsContrassenya = True
        Me.txtContrasenya.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.25!)
        Me.txtContrasenya.ForeColor = System.Drawing.Color.Gold
        Me.txtContrasenya.Location = New System.Drawing.Point(13, 190)
        Me.txtContrasenya.Name = "txtContrasenya"
        Me.txtContrasenya.Size = New System.Drawing.Size(381, 68)
        Me.txtContrasenya.TabIndex = 3
        Me.txtContrasenya.Text = "Contrasenya"
        Me.txtContrasenya.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtContrasenya.TextIndicatiu = "Contrasenya"
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(394, 394)
        Me.Controls.Add(Me.lblAddr)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.Minibotó1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.progComprovant)
        Me.Controls.Add(Me.txtUsuari)
        Me.Controls.Add(Me.txtContrasenya)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Login"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        CType(Me.Minibotó1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtContrasenya As TextboxDinàmic
    Friend WithEvents txtUsuari As TextboxDinàmic
    Friend WithEvents progComprovant As Owf.Controls.OwfProgressControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents backVerificarUsuari As System.ComponentModel.BackgroundWorker
    Friend WithEvents backIntroduirUsuari As System.ComponentModel.BackgroundWorker
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Minibotó1 As Minibotó
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents lblAddr As System.Windows.Forms.Label

End Class
