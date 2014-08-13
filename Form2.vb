Imports System.IO

Public Class Form2

    Private Sub Form2_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form1.Show()

    End Sub


    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        TextBox1.Text = ""
        Dim r1 As StreamReader = New StreamReader(Form1.k1)
        TextBox1.Text = r1.ReadToEnd()
        r1.Close()



    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim s1 As String = SaveFileDialog1.FileName
        Dim w1 As StreamWriter = New StreamWriter(s1 + ".txt", False, System.Text.Encoding.Default)
        w1.WriteLine("FCOM Powered By Funnyworker")
        w1.WriteLine("Version:  V3.0")
        w1.WriteLine("SAVE DATE:" + " " + Today)
        w1.WriteLine("Encoding: ANSI")
        w1.WriteLine(".............................")
        w1.Write(TextBox1.Text)
        w1.Close()
    End Sub
End Class