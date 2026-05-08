'Imports Cudafy
Imports System.Threading
'Imports Nini.Config
Imports System.IO
Imports System.Runtime.InteropServices
'Imports Python.Runtime

Public Class Settings

    Public Enum Platform
        Windows
        Linux
        Mac
    End Enum

    Public Enum WindowsPlatformRenderer
        WinForms = 0
        WinForms_Direct2D = 1
        WPF = 2
        Gtk2 = 3
        Gtk3 = 4
    End Enum

    Public Enum LinuxPlatformRenderer
        Gtk2 = 0
        WinForms = 1
        Gtk3 = 2
    End Enum

    Public Enum MacOSPlatformRenderer
        MonoMac = 0
        Gtk2 = 1
        WinForms = 2
        Gtk3 = 3
    End Enum

    Public Enum SkiaCanvasRenderer
        CPU = 0
        OpenGL = 1
    End Enum

    Public Enum AIAssistedConvergenceMode
        Disabled = 0
        Provide_Initial_Estimates = 1
        Provide_Initial_Estimates_2Pass = 2
        Provide_Initial_Estimates_and_Solutions = 3
        Provide_Solutions = 4
        Provide_Initial_Estimates_and_Solutions_2Pass = 5
    End Enum

    Public Shared Property WindowsRenderer As WindowsPlatformRenderer = WindowsPlatformRenderer.WinForms

    Public Shared Property LinuxRenderer As LinuxPlatformRenderer = LinuxPlatformRenderer.Gtk3

    Public Shared Property MacOSRenderer As MacOSPlatformRenderer = MacOSPlatformRenderer.MonoMac

    Public Shared Property FlowsheetRenderer As SkiaCanvasRenderer = SkiaCanvasRenderer.CPU

    Private Shared _tcks As CancellationTokenSource
    Public Shared Property TaskCancellationTokenSource As CancellationTokenSource
        Get
            If _tcks Is Nothing Then _tcks = New CancellationTokenSource
            Return _tcks
        End Get
        Set(value As CancellationTokenSource)
            _tcks = value
        End Set
    End Property

    Private Shared _comode As Boolean = False

    Public Shared Property CAPEOPENMode As Boolean
        Get
            Return _comode
        End Get
        Set(value As Boolean)
            _comode = value
        End Set
    End Property
    Public Shared Property ExcelMode As Boolean = False

    Public Shared Property MaxDegreeOfParallelism As Integer
        Get
            Return -1
        End Get
        Set(value As Integer)
            'do nothing
        End Set
    End Property

    Public Shared Property UseSIMDExtensions As Boolean = False
    Public Shared Property EnableParallelProcessing As Boolean = True
    Public Shared Property EnableGPUProcessing As Boolean = False
    Public Shared Property CudafyTarget As Integer = 0
    Public Shared Property CudafyDeviceID As Integer = 0
    Public Shared Property DebugLevel As Integer = 0
    Public Shared Property MaxThreadMultiplier As Integer = 8
    Public Shared Property SolverTimeoutSeconds As Integer = 3600
    Public Shared Property SolverMode As Integer = 1
    Public Shared Property ServiceBusConnectionString As String = ""
    Public Shared Property CalculatorStopRequested As Boolean
    Public Shared Property CalculatorActivated As Boolean
    Public Shared Property CalculatorBusy As Boolean
    Public Shared Property ServerIPAddress As String = ""
    Public Shared Property ServerPort As Integer = 0
    Public Shared Property CurrentCulture As String = "en"

    Public Shared DefaultEditFormLocation As Integer = 8

    Public Shared SolverBreakOnException As Boolean = False
    Public Shared Property SelectedGPU As String = ""
    Public Shared Property CultureInfo As String = "en"
    Public Shared Property InitializedCOPPM As Boolean = False
    Public Shared Property ExcelErrorHandlingMode As Integer = 0
    Public Shared Property ExcelFlashSettings As String = ""

    Public Shared Property UserInteractionsDatabases As New List(Of String)

    Public Shared Property UserDatabases As New List(Of String)

    Public Shared Property HideSolidPhaseFromCAPEOPENComponents As Boolean = True

    Public Shared Property DrawingAntiAlias As Boolean = True

    Public Shared Property AutomationMode As Boolean = False

    Public Shared Property OctavePath As String = ""

    Public Shared Property OctaveTimeoutInMinutes As Double = 5

    Public Shared Property CurrentEnvironment As Integer = 0

    Public Shared Property CurrentPlatform As String = "Windows"

    Public Shared Property PythonPath As String = ""

    Public Shared Property PythonTimeoutInMinutes As Double = 1

    Public Shared Property PythonInitialized As Boolean = False

    Public Shared Property EnableBackupCopies As Boolean = True

    Public Shared Property SaveExistingFile As Boolean = True

    Public Shared Property BackupInterval As Integer = 5

    Public Shared Property UserUnits As String = "{ }"

    Public Shared Property MostRecentFiles As New List(Of String)

    Public Shared Property ResultsReportFontSize As Integer = 10

    Public Shared Property OldUI As Boolean = True


    Public Shared AutomaticUpdates As Boolean = True


    Public Shared CurrentVersion As String = ""


    Public Shared CurrentRunningVersion As String = ""

    Public Shared Property CalculationRequestID As String = ""

    Public Shared Property InspectorEnabled As Boolean = True

    Public Shared Property ClearInspectorHistoryOnNewCalculationRequest As Boolean = True

    Public Shared Property EditorFontSize As Integer = -1

    Public Shared Property EditorTextBoxFixedSize As Boolean = True

    Public Shared Property EditorTextBoxFixedSizeWidth As Integer = 180

    Public Shared Property EditOnSelect As Boolean = True

    Public Shared Property CallSolverOnEditorPropertyChanged As Boolean = True

    Public Shared Property DpiScale As Double = 1.0

    Public Shared Property DarkMode As Boolean = False

    Public Shared Property UIScalingFactor As Double = 1.0

    Public Shared Property ObjectEditor As Integer = 0

    Public Shared Property CrossPlatformUIItemSpacing As Integer = 5

    Public Shared Property EnableCustomTouchBar As Boolean = True

    Public Shared Property CheckForUpdates As Boolean = True

    Public Shared TranslatorActivated As Boolean = False

    Public Shared TranslatorLanguage As String = ""

    Public Shared LockModelParameters As Boolean = False

    Public Shared IsGTKRenderer As Boolean = False

    Public Shared LinuxDisplayDPI As Double = 96.0

    Public Shared AIAssistedConvergenceLevel As AIAssistedConvergenceMode = AIAssistedConvergenceMode.Disabled

    <DllImport("kernel32.dll", SetLastError:=True)> Public Shared Function AddDllDirectory(lpPathName As String) As Boolean

    End Function

    Public Shared Sub InitializePythonEnvironment(Optional ByVal pythonpath As String = "")
        ' Headless Stub
    End Sub

    Public Shared Sub ShutdownPythonEnvironment()
        ' Headless Stub
    End Sub

    Private Shared Sub SetPythonPath(Optional ByVal pythonpath As String = "")
        ' Headless Stub
    End Sub

    Public Shared Function GetEnvironment() As Integer
        If Environment.Is64BitProcess Then
            Return 64
        Else
            Return 32
        End If
    End Function

    Public Shared Function GetPlatform() As String
        If RunningPlatform() = Platform.Windows Then
            Return "Windows"
        ElseIf RunningPlatform() = Platform.Linux Then
            Return "Linux"
        ElseIf RunningPlatform() = Platform.Mac Then
            Return "Mac"
        Else
            Return "None"
        End If
    End Function

    Public Shared Function RunningPlatform() As Platform
        Select Case Environment.OSVersion.Platform
            Case PlatformID.Unix
                ' Well, there are chances MacOSX is reported as Unix instead of MacOSX.
                ' Instead of platform check, we'll do a feature checks (Mac specific root folders)
                If Directory.Exists("/Applications") And Directory.Exists("/System") And Directory.Exists("/Users") And Directory.Exists("/Volumes") Then
                    Return Platform.Mac
                Else
                    Return Platform.Linux
                End If
            Case PlatformID.MacOSX
                Return Platform.Mac
            Case Else
                Return Platform.Windows
        End Select
    End Function

    Public Shared Function IsRunningOnMono() As Boolean
        Return Not Type.GetType("Mono.Runtime") Is Nothing
    End Function

    Public Shared Function GetConfigFileDir() As String
        Dim configfiledir As String = ""
        If Settings.RunningPlatform = Platform.Mac Then
            configfiledir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Documents", "DWSIM Application Data") & Path.DirectorySeparatorChar
        Else
            configfiledir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & Path.DirectorySeparatorChar & "DWSIM Application Data" & Path.DirectorySeparatorChar
        End If
        Return configfiledir
    End Function

    Shared Sub LoadExcelSettings(Optional ByVal configfile As String = "")
        ' Headless Stub
        UserDatabases.Clear()
        UserInteractionsDatabases.Clear()
    End Sub

    Shared Sub SaveExcelSettings(Optional ByVal configfile As String = "")
        ' Headless Stub
    End Sub

    Shared Sub LoadSettings(Optional ByVal configfile As String = "")
        ' Headless Stub
        MostRecentFiles = New List(Of String)
        UserDatabases = New List(Of String)
        UserInteractionsDatabases = New List(Of String)
    End Sub

    Shared Sub SaveSettings(Optional ByVal configfile As String = "")
        ' Headless Stub
    End Sub

End Class


