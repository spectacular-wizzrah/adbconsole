Public Class console
    Private WithEvents MyProcess As Process
    Private Delegate Sub AppendOutputTextDelegate(ByVal text As String)


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.Close()
    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.WindowState = FormWindowState.Minimized

    End Sub


    Private Sub console_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AcceptButton = startbutton
        MyProcess = New Process
        With MyProcess.StartInfo
            .FileName = "cmd.exe"
            .UseShellExecute = False
            .CreateNoWindow = True
            .RedirectStandardInput = True
            .RedirectStandardOutput = True
            .RedirectStandardError = True
        End With
        MyProcess.Start()

        MyProcess.BeginErrorReadLine()
        MyProcess.BeginOutputReadLine()
        AppendOutputText("Thanks for trying out ADB Console by wizzrah! " & MyProcess.StartTime.ToString)
    End Sub

    Private Sub MyProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs) Handles MyProcess.ErrorDataReceived
        AppendOutputText(vbCrLf & "Error: " & e.Data)
    End Sub

    Private Sub MyProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs) Handles MyProcess.OutputDataReceived
        AppendOutputText(vbCrLf & e.Data)
    End Sub

    Private Sub startbutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startbutton.Click
        MyProcess.StandardInput.WriteLine(InputTextBox.Text)
        MyProcess.StandardInput.Flush()
        InputTextBox.Text = ""
    End Sub

    Private Sub AppendOutputText(ByVal text As String)
        If OutputTextBox.InvokeRequired Then
            Dim myDelegate As New AppendOutputTextDelegate(AddressOf AppendOutputText)
            Me.Invoke(myDelegate, text)
        Else
            OutputTextBox.AppendText(text)
        End If
    End Sub

    Private mPrevPos As New Point
    Private Sub console_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim delta As New Size(e.X - mPrevPos.X, e.Y - mPrevPos.Y)
        If (e.Button = MouseButtons.Left) Then
            Me.Location += delta
            mPrevPos = e.Location - delta
        Else
            mPrevPos = e.Location
        End If
    End Sub

    Private Sub OutputTextBox_TextChanged(sender As Object, e As EventArgs) Handles OutputTextBox.TextChanged

    End Sub

    Private Sub InputTextBox_TextChanged(sender As Object, e As EventArgs) Handles InputTextBox.TextChanged

    End Sub
End Class