Namespace My

    Partial Class MyApplication

        Private Sub MyApplication_UnhandledException(ByVal s As Object, _
            ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) _
            Handles Me.UnhandledException

            Dim NewExceptionForm As New ExceptionForm
            NewExceptionForm.Exception = e.Exception
            e.ExitApplication = (NewExceptionForm.ShowDialog() = DialogResult.Cancel)
        End Sub

    End Class

End Namespace

