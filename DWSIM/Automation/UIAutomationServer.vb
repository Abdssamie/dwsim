Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Threading
Imports System.Threading.Tasks

Namespace Automation.UI.Classic

    Public Class UIAutomationServer

        Public Property Flowsheet As AutomatedFlowsheet

        Private Server As TcpComm.Server
        Private LAT As TcpComm.Utilities.LargeArrayTransferHelper

        Dim ts As CancellationTokenSource

        Public Sub New()


            Server = New TcpComm.Server(AddressOf Process)
            LAT = New TcpComm.Utilities.LargeArrayTransferHelper(Server)

            Dim port As Integer

            If My.Application.CommandLineArgs.Count > 0 Then
                port = My.Application.CommandLineArgs(0)
            Else
                Console.Write("Please enter the TCP Port Number to listen to: ")
                port = Console.ReadLine()
            End If

            Server.Start(port)

            Console.WriteLine("[" & Date.Now.ToString & "] " & "Server IP Addresses:")
            For Each adr In System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList()
                Console.WriteLine(adr.ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("[" & Date.Now.ToString & "] " & "Server is running and listening to incoming data on port " & port & "...")

            Dim icounter As Integer = 100

            While Server.IsRunning

                Thread.Sleep(1000)

                icounter += 1

                If Math.IEEERemainder(icounter, 100) = 0 Then

                    Dim binFormatter = New BinaryFormatter()
                    Dim mStream = New MemoryStream()
                    'binFormatter.Serialize(mStream, solutions)

                    'If mStream.Length > 100 * 1024 * 1024 Then
                    '    solutions.Clear()
                    'End If

                End If

            End While

        End Sub

        Public Sub Process(ByVal bytes() As Byte, ByVal sessionID As Int32, ByVal dataChannel As Byte)

            ' Use TcpComm.Utilities.LargeArrayTransferHelper to make it easier to send and receive 
            ' large arrays sent via lat.SendArray()
            ' The LargeArrayTransferHelperb will assemble any number of incoming large arrays
            ' on any channel or from any sessionId, and pass them back to this callback
            ' when they are complete. Returns True if it has handled this incomming packet,
            ' so we exit the callback when it returns true.
            If LAT.HandleIncomingBytes(bytes, 100, sessionID) Then Return

            If ts Is Nothing Then ts = New CancellationTokenSource

            If dataChannel = 100 Then

                Dim ct As CancellationToken = ts.Token

                Dim errmsg As String = ""

                Console.WriteLine("[" & Date.Now.ToString & "] " & "Data received from " & Server.GetSession(sessionID).machineId & ", flowsheet solving started!")
                If Not Server.SendText("Data received from " & Server.GetSession(sessionID).machineId & ", flowsheet solving started!", 2, sessionID, errmsg) Then
                    Console.WriteLine(errmsg)
                End If

                Task.Factory.StartNew(Sub()
                                          ProcessData(bytes, sessionID, dataChannel)
                                      End Sub, ct, TaskCreationOptions.LongRunning).ContinueWith(
                                      Sub(t)
                                          If Not t.Exception Is Nothing Then
                                              Console.WriteLine("[" & Date.Now.ToString & "] " & "Error solving flowsheet: " & t.Exception.ToString)
                                              errmsg = ""
                                              If Not Server.SendText("Error solving flowsheet: " & t.Exception.ToString, 3, sessionID, errmsg) Then
                                                  Console.WriteLine(errmsg)
                                              End If
                                          ElseIf t.IsCanceled Then
                                              Console.WriteLine("[" & Date.Now.ToString & "] " & "Calculation aborted.")
                                              errmsg = ""
                                              If Not Server.SendText("Calculation aborted.", 2, sessionID, errmsg) Then
                                                  Console.WriteLine(errmsg)
                                              End If
                                          End If
                                      End Sub,
                                      TaskContinuationOptions.OnlyOnFaulted).ContinueWith(
                                      Sub()
                                          Console.WriteLine("[" & Date.Now.ToString & "] " & "Closing current session with " & Server.GetSession(sessionID).machineId & ".")
                                          errmsg = ""
                                          If Not Server.SendText("Closing current session with " & Server.GetSession(sessionID).machineId & ".", 2, sessionID, errmsg) Then
                                              Console.WriteLine(errmsg)
                                          End If
                                          ts.Dispose()
                                          ts = Nothing
                                          Server.GetSession(sessionID).Close()
                                      End Sub)

            ElseIf dataChannel = 3 Then

                If Not ts Is Nothing Then ts.Cancel()

            ElseIf dataChannel = 255 Then

                Dim tmp = ""
                Dim msg As String = TcpComm.Utilities.BytesToString(bytes)
                ' server has finished sending the bytes you put into sendBytes()
                If msg.Length > 3 Then tmp = msg.Substring(0, 3)
                If tmp = "UBS" Then ' User Bytes Sent.

                End If

            End If

        End Sub

        Sub ProcessData(bytes As Byte(), sessionid As Integer, datachannel As Byte)
            Dim errmsg As String = ""
            Using bytestream As New MemoryStream(bytes)
                'Using form As FormFlowsheet = Flowsheet.InitializeFlowsheet(bytestream, New FormFlowsheet)
                '    If Not solutions.ContainsKey(form.Options.Key) Then
                '        DWSIM.FlowsheetSolver.FlowsheetSolver.SolveFlowsheet(form, 1, ts)
                '        Dim retbytes As MemoryStream = DWSIM.UnitOperations.UnitOperations.Flowsheet.ReturnProcessData(form)
                '        Using retbytes
                '            Dim uncompressedbytes As Byte() = retbytes.ToArray
                '            Using compressedstream As New MemoryStream()
                '                Using gzs As New BufferedStream(New Compression.GZipStream(compressedstream, Compression.CompressionMode.Compress, True), 64 * 1024)
                '                    gzs.Write(uncompressedbytes, 0, uncompressedbytes.Length)
                '                    gzs.Close()
                '                    solutions.Add(form.Options.Key, compressedstream.ToArray)
                '                End Using
                '            End Using
                '        End Using
                '    End If
                '    LAT.SendArray(solutions(form.Options.Key), 100, sessionid, errmsg)
                '    Console.WriteLine("[" & Date.Now.ToString & "] " & "Byte array length: " & solutions(form.Options.Key).Length)
                'End Using
            End Using
        End Sub

    End Class

End Namespace
