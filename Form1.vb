Imports System.IO.Ports
Imports System.Text
Imports System.IO
Imports System.Threading

Public Class Form1
    Dim co2 As Integer = 0
    Dim co1 As Integer = 0
    Dim f0 As Integer = 0
    Dim SerialPort1 As SerialPort = New SerialPort()
    Dim SUM
    Public k1 As String
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        If ((ComboBox1.Text).Length < 1 Or (ComboBox2.Text).Length < 1 Or (TextBox2.Text).Length < 1 Or (TextBox4.Text).Length < 1) Then

            MsgBox("請確認是否資料都有輸入完全，並且再試一次")

        Else
            Try
                SerialPort1.DataBits = 8
                If ComboBox2.Text = "1" Then
                    SerialPort1.StopBits = StopBits.One
                ElseIf ComboBox2.Text = "2" Then
                    SerialPort1.StopBits = StopBits.Two
                End If
                SerialPort1.DataBits = Val(TextBox4.Text)
                SerialPort1.PortName = ComboBox1.Text
                SerialPort1.Open()
                Button4.Visible = True
                Button1.Visible = False
                Button2.Enabled = True
                Button3.Enabled = True
            Catch exception As Exception
                MsgBox("發生了不可預期錯誤，請連路你的系統管理員")
            End Try
        End If


    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Label5.Text = "已接收 :    第0筆資料"
        Label6.Text = "已傳送 :    第0筆資料"

        Form.CheckForIllegalCrossThreadCalls = False
        For Each k1 In SerialPort.GetPortNames()
            ComboBox1.Items.Add(k1)
        Next
      

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork




        'text

        Dim w1 As New FileInfo("C:\FCOM\receive.txt")
        Dim w2 As StreamWriter = w1.AppendText

        'RX

        Dim s As String = ""
        Dim RX() As Byte
        ReDim RX(SerialPort1.BytesToRead - 1)

        SerialPort1.Read(RX, 0, RX.Length)

        For i As Integer = 0 To RX.Length - 1
            s += RX(i).ToString("X2")

        Next
        If s.Length > 0 Then
            TextBox1.Text = TextBox1.Text + vbCrLf + Now + ":     " + s


            co1 += 1
            Label5.Text = "已接收:第" + Str(co1) + "筆資料"
            w2.WriteLine(Now + "已接收第" + Str(co1) + "筆資料" + ":     " + s)
            TextBox1.SelectionStart = TextBox1.Text.Length
            TextBox1.ScrollToCaret()
        End If

        w2.Close()

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Try
            Dim r1 As New FileInfo("C:\FCOM\send.txt")
            Dim r2 As StreamWriter = r1.AppendText

            Dim buffer(0) As Byte
            Button9.PerformClick()
            buffer(0) = SUM
            co2 += 1
            ' Dim s1 As String = System.Text.Encoding.Unicode.GetString()
            SerialPort1.Write(buffer, 0, buffer.Length)
            Label6.Text = "已傳送:第" + Str(co2) + "筆資料"
            r2.WriteLine(Now + "已傳送第" + Str(co2) + "筆資料:" + TextBox3.Text)
            TextBox3.Text = ""
            r2.Close()
        Catch exception As Exception
            MsgBox("發生了不可預期錯誤，請連路你的系統管理員")
        End Try

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click


        Timer1.Interval = 10
        Timer1.Start()

        Button3.Visible = False
        Button8.Visible = True

    End Sub

    Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click


        SerialPort1.Close()
        Button8.PerformClick()
        While BackgroundWorker1.IsBusy = 1
            Thread.Sleep(500)
        End While
        TextBox1.Text = ""
        Button1.Visible = True
        Button4.Visible = False
        Button2.Enabled = False
        Button3.Enabled = False

        

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        '報告覆寫
        Dim s1 As StreamWriter = New StreamWriter("C:\FCOM\receive.txt", False)
        s1.Close()
        Dim s2 As StreamWriter = New StreamWriter("C:\FCOM\send.txt", False)
        s2.Close()
        MsgBox("刪除完成")

    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        'Process.Start("C:\FCOM\receive.txt")
        k1 = "C:\FCOM\receive.txt"
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        'Process.Start("C:\FCOM\send.txt")
        k1 = "C:\FCOM\send.txt"
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If (BackgroundWorker1.IsBusy = False) Then
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Timer1.Stop()
        If (BackgroundWorker1.IsBusy = True) Then
            BackgroundWorker1.Dispose()
        End If
        Button3.Visible = True
        Button8.Visible = False
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://funnyworker.tw/8051")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://www.facebook.com/groups/268688359973119/")
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        Dim f1 As Integer = 0
        Dim sixty As Integer = 0

        Dim k1 As Integer
        SUM = 0
        REM
        If (ComboBox3.Text).Length < 1 Then
            MsgBox("請選擇你要使用的進位")
            Return
        End If
        If ComboBox3.Text = "十進位" Then

            SUM = TextBox3.Text

            If Val(SUM) > 255 Or Val(SUM) < 0 Then
                MsgBox("數字輸入錯誤")

                Return

            End If

        End If
        REM
        If ComboBox3.Text = "二進位" Then

            sixty = (TextBox3.Text).Length
            If Not sixty = 8 Then
                MsgBox("數字輸入錯誤")
                Return
            Else

                For j As Integer = 1 To sixty
                    k1 = Val(GetChar(TextBox3.Text, j))

                    SUM = (k1 * 2 ^ (sixty - j)) + SUM

                Next

            End If
        End If
        REM 
        If ComboBox3.Text = "十六進位" Then
            sixty = (TextBox3.Text).Length
            If Not sixty = 2 Then
                MsgBox("數字輸入錯誤")
                Return
            Else
                For i As Integer = 1 To sixty

                    k1 = Val(GetChar(TextBox3.Text, i))
                    k1 = sixtygetten(k1, i)
                    SUM = (k1 * 16 ^ (sixty - i)) + SUM

                Next
            End If


        End If
        REM

    End Sub
    Private Function sixtygetten(k1, i)

        If GetChar(TextBox3.Text, i) = "A" Then
            k1 = 10
        End If
        If GetChar(TextBox3.Text, i) = "B" Then
            k1 = 11
        End If
        If GetChar(TextBox3.Text, i) = "C" Then
            k1 = 12
        End If
        If GetChar(TextBox3.Text, i) = "D" Then
            k1 = 13
        End If
        If GetChar(TextBox3.Text, i) = "E" Then
            k1 = 14
        End If
        If GetChar(TextBox3.Text, i) = "F" Then
            k1 = 15
        End If
        Return k1
    End Function
   
    Private Sub LinkLabel3_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("http://funnyworker.tw")
    End Sub

    Private Sub Label7_Click(sender As System.Object, e As System.EventArgs) Handles Label7.Click

    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button2.PerformClick()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        
    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
       
    End Sub
End Class
