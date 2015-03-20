Imports BielUtils

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegister
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
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txtEmail = New BielUtils.TextboxDinàmic()
        Me.txtPssw = New BielUtils.TextboxDinàmic()
        Me.txtUser = New BielUtils.TextboxDinàmic()
        Me.txtRealName = New BielUtils.TextboxDinàmic()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnRegister
        '
        Me.btnRegister.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRegister.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegister.Location = New System.Drawing.Point(117, 139)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(98, 35)
        Me.btnRegister.TabIndex = 0
        Me.btnRegister.Text = "Registra't"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'txtEmail
        '
        Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmail.BackColor = System.Drawing.Color.White
        Me.txtEmail.ColorFocus = System.Drawing.Color.Empty
        Me.txtEmail.ÉsContrassenya = False
        Me.txtEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(12, 108)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(203, 26)
        Me.txtEmail.TabIndex = 4
        Me.txtEmail.Text = "Correu electrònic"
        Me.txtEmail.TextIndicatiu = "Correu electrònic"
        '
        'txtPssw
        '
        Me.txtPssw.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPssw.BackColor = System.Drawing.Color.White
        Me.txtPssw.ColorFocus = System.Drawing.Color.Empty
        Me.txtPssw.ÉsContrassenya = False
        Me.txtPssw.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPssw.Location = New System.Drawing.Point(12, 44)
        Me.txtPssw.Name = "txtPssw"
        Me.txtPssw.Size = New System.Drawing.Size(203, 26)
        Me.txtPssw.TabIndex = 2
        Me.txtPssw.Text = "Contrassenya"
        Me.txtPssw.TextIndicatiu = "Contrassenya"
        Me.txtPssw.UseSystemPasswordChar = True
        '
        'txtUser
        '
        Me.txtUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUser.BackColor = System.Drawing.Color.White
        Me.txtUser.ColorFocus = System.Drawing.Color.Empty
        Me.txtUser.ÉsContrassenya = False
        Me.txtUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUser.Location = New System.Drawing.Point(12, 12)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(203, 26)
        Me.txtUser.TabIndex = 1
        Me.txtUser.Text = "Nom d'usuari"
        Me.txtUser.TextIndicatiu = "Nom d'usuari"
        '
        'txtRealName
        '
        Me.txtRealName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRealName.BackColor = System.Drawing.Color.White
        Me.txtRealName.ColorFocus = System.Drawing.Color.Empty
        Me.txtRealName.ÉsContrassenya = False
        Me.txtRealName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRealName.Location = New System.Drawing.Point(12, 76)
        Me.txtRealName.Name = "txtRealName"
        Me.txtRealName.Size = New System.Drawing.Size(203, 26)
        Me.txtRealName.TabIndex = 3
        Me.txtRealName.TextIndicatiu = "Nom real"
        '
        'frmRegister
        '
        Me.AcceptButton = Me.btnRegister
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(227, 186)
        Me.Controls.Add(Me.txtRealName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPssw)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.btnRegister)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmRegister"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registre"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents txtUser As TextboxDinàmic
    Friend WithEvents txtPssw As TextboxDinàmic
    Friend WithEvents txtEmail As TextboxDinàmic
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtRealName As BielUtils.TextboxDinàmic
End Class
