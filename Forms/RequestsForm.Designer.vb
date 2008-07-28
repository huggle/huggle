<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RequestsForm
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
        Me.List = New huggle.ListView2
        Me.DateColumn = New System.Windows.Forms.ColumnHeader
        Me.TypeColumn = New System.Windows.Forms.ColumnHeader
        Me.ActionColumn = New System.Windows.Forms.ColumnHeader
        Me.QueryColumn = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'List
        '
        Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.DateColumn, Me.TypeColumn, Me.ActionColumn, Me.QueryColumn})
        Me.List.FullRowSelect = True
        Me.List.GridLines = True
        Me.List.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.List.Location = New System.Drawing.Point(12, 12)
        Me.List.Name = "List"
        Me.List.Size = New System.Drawing.Size(454, 231)
        Me.List.TabIndex = 0
        Me.List.UseCompatibleStateImageBehavior = False
        Me.List.View = System.Windows.Forms.View.Details
        '
        'DateColumn
        '
        Me.DateColumn.Text = "Time"
        '
        'TypeColumn
        '
        Me.TypeColumn.Text = "Type"
        Me.TypeColumn.Width = 92
        '
        'ActionColumn
        '
        Me.ActionColumn.Text = ""
        Me.ActionColumn.Width = 40
        '
        'QueryColumn
        '
        Me.QueryColumn.Text = "Query"
        Me.QueryColumn.Width = 229
        '
        'RequestsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 255)
        Me.Controls.Add(Me.List)
        Me.Name = "RequestsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Requests"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents List As huggle.ListView2
    Friend WithEvents DateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents TypeColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ActionColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents QueryColumn As System.Windows.Forms.ColumnHeader
End Class
