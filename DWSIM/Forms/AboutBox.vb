Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.IO
Imports Yeppp
Imports System.Linq
Imports Cudafy
Imports Cudafy.Host

Public Class AboutBox

    Private _IsPainted As Boolean = False
    Private _EntryAssemblyName As String
    Private _CallingAssemblyName As String
    Private _ExecutingAssemblyName As String
    Private _EntryAssembly As System.Reflection.Assembly
    Private _EntryAssemblyAttribCollection As Specialized.NameValueCollection

    Private Sub AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ExtensionMethods.ChangeDefaultFont(Me)

        TextBox1.Font = New Font("Consolas", 9, GraphicsUnit.Point)
        tbAcknowledgements.Font = New Font("Consolas", 9, GraphicsUnit.Point)

        Dim updfile = My.Application.Info.DirectoryPath & Path.DirectorySeparatorChar & "version.info"


#If NOADS Then
        Version.Text = "Version " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & " (Patreon Supporters Build)"
#Else
        Version.Text = "Version " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
#End If

#If DEBUG Then
        Version.Text += " - " + IO.File.GetLastWriteTimeUtc(Assembly.GetExecutingAssembly().Location).ToString()
#End If

        tbAcknowledgements.Text = "A HUGE thank you to the following Patrons/Sponsors who made this release possible:" + vbCrLf + vbCrLf +
            Patrons.GetList() + vbCrLf + vbCrLf + tbAcknowledgements.Text

        Copyright.Text = My.Application.Info.Copyright

        LblOSInfo.Text = My.Computer.Info.OSFullName & ", Version " & My.Computer.Info.OSVersion & ", " & My.Computer.Info.OSPlatform & " Platform"
        LblCLRInfo.Text = SharedClasses.Utility.GetRuntimeVersion()
        Lblmem.Text = (GC.GetTotalMemory(False) / 1024 / 1024).ToString("#") & " MB managed, " & (My.Application.Info.WorkingSet / 1024 / 1024).ToString("#") & " MB total"

        Lblcpuinfo.Text = "Retrieving CPU info..."

        If Not DWSIM.App.IsRunningOnMono Then

            Threading.Tasks.Task.Factory.StartNew(Function()
                                                      Dim scrh As New System.Management.ManagementObjectSearcher("select * from Win32_Processor")
                                                      Dim text1 As String = System.Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")
                                                      For Each qinfo In scrh.Get()
                                                          text1 += " / " & qinfo.Properties("Name").Value.ToString
                                                      Next
                                                      Return text1
                                                  End Function).ContinueWith(Sub(t)
                                                                                 Lblcpuinfo.Text = t.Result
                                                                             End Sub, Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext)

        Else

            Threading.Tasks.Task.Factory.StartNew(Function()
                                                      Dim sinfo As New ProcessStartInfo With {.FileName = "lshw", .Arguments = "-c CPU", .RedirectStandardOutput = True, .UseShellExecute = False}
                                                      Dim p As New Process With {.StartInfo = sinfo}
                                                      p.Start()
                                                      Dim output As String = p.StandardOutput.ReadToEnd
                                                      p.WaitForExit()
                                                      Dim lbltext As String = ""
                                                      For Each l In output.Split(New Char() {vbCrLf, vbLf, vbCr})
                                                          If l.Contains("product") Then
                                                              lbltext = l.Split(":")(1).TrimStart(" ")
                                                          End If
                                                          If l.Contains("vendor") Then
                                                              lbltext += " / " & l.Split(": ")(1).TrimStart(" ")
                                                              Exit For
                                                          End If
                                                      Next
                                                      Return lbltext
                                                  End Function).ContinueWith(Sub(t)
                                                                                 Lblcpuinfo.Text = t.Result
                                                                             End Sub, Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext)

        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' populate a listview with the specified key and value strings
    ''' </summary>
    Private Sub Populate(ByVal lvw As ListView, ByVal Key As String, ByVal Value As String)
        If Value = "" Then Return
        Dim lvi As New ListViewItem
        lvi.Text = Key
        lvi.SubItems.Add(Value)
        lvw.Items.Add(lvi)
    End Sub

    Private Sub AboutBox_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        FormMain.TranslateFormFunction?.Invoke(Me)
    End Sub
End Class