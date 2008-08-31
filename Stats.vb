NotInheritable Class Stats

    'Static class keeping track of various statistics

    Private Sub New()
    End Sub

    Public Shared Edits, EditsMe, Reverts, RevertsMe, Warnings, WarningsMe, Blocks, BlocksMe As Integer

    Public Shared Sub Update(ByVal Edit As Edit)
        'Update statistics and edit counts
        Stats.Edits += 1

        Select Case Edit.Type
            Case EditType.Revert : Stats.Reverts += 1
            Case EditType.Warning : Stats.Warnings += 1
        End Select
    End Sub

End Class
