<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SockReportForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Accounts = New System.Windows.Forms.ListBox
        Me.AccountsLabel = New System.Windows.Forms.Label
        Me.Add = New System.Windows.Forms.Button
        Me.Remove = New System.Windows.Forms.Button
        Me.Clear = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.DetailsLabel = New System.Windows.Forms.Label
        Me.Details = New Huggle.WikiTextBox
        Me.SuspendLayout()
        '
        'Accounts
        '
        Me.Accounts.FormattingEnabled = True
        Me.Accounts.Location = New System.Drawing.Point(12, 25)
        Me.Accounts.Name = "Accounts"
        Me.Accounts.Size = New System.Drawing.Size(237, 56)
        Me.Accounts.TabIndex = 1
        '
        'AccountsLabel
        '
        Me.AccountsLabel.AutoSize = True
        Me.AccountsLabel.Location = New System.Drawing.Point(12, 9)
        Me.AccountsLabel.Name = "AccountsLabel"
        Me.AccountsLabel.Size = New System.Drawing.Size(198, 13)
        Me.AccountsLabel.TabIndex = 0
        Me.AccountsLabel.Text = "Known or suspected alternate accounts:"
        '
        'Add
        '
        Me.Add.Location = New System.Drawing.Point(12, 87)
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(75, 23)
        Me.Add.TabIndex = 2
        Me.Add.Text = "Add"
        Me.Add.UseVisualStyleBackColor = True
        '
        'Remove
        '
        Me.Remove.Enabled = False
        Me.Remove.Location = New System.Drawing.Point(93, 87)
        Me.Remove.Name = "Remove"
        Me.Remove.Size = New System.Drawing.Size(75, 23)
        Me.Remove.TabIndex = 3
        Me.Remove.Text = "Remove"
        Me.Remove.UseVisualStyleBackColor = True
        '
        'Clear
        '
        Me.Clear.Enabled = False
        Me.Clear.Location = New System.Drawing.Point(174, 87)
        Me.Clear.Name = "Clear"
        Me.Clear.Size = New System.Drawing.Size(75, 23)
        Me.Clear.TabIndex = 4
        Me.Clear.Text = "Clear"
        Me.Clear.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(212, 314)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 7
        Me.OK.Text = "Submit"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(293, 314)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'DetailsLabel
        '
        Me.DetailsLabel.AutoSize = True
        Me.DetailsLabel.Location = New System.Drawing.Point(12, 122)
        Me.DetailsLabel.Name = "DetailsLabel"
        Me.DetailsLabel.Size = New System.Drawing.Size(42, 13)
        Me.DetailsLabel.TabIndex = 5
        Me.DetailsLabel.Text = "Details:"
        '
        'Details
        '
        Me.Details.Location = New System.Drawing.Point(15, 138)
        Me.Details.Name = "Details"
        Me.Details.Size = New System.Drawing.Size(353, 170)
        Me.Details.TabIndex = 9
        Me.Details.Text = ""
        '
        'SockReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 349)
        Me.Controls.Add(Me.Details)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Clear)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Remove)
        Me.Controls.Add(Me.Add)
        Me.Controls.Add(Me.DetailsLabel)
        Me.Controls.Add(Me.AccountsLabel)
        Me.Controls.Add(Me.Accounts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "SockReportForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reporting user"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Accounts As System.Windows.Forms.ListBox
    Friend WithEvents AccountsLabel As System.Windows.Forms.Label
    Friend WithEvents Add As System.Windows.Forms.Button
    Friend WithEvents Remove As System.Windows.Forms.Button
    Friend WithEvents Clear As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents DetailsLabel As System.Windows.Forms.Label
    Friend WithEvents Details As Huggle.WikiTextBox
End Class
