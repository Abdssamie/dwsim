Imports System.Linq

Public Class FormUnitOperationExtensionManager

    Public Property Flowsheet As Interfaces.IFlowsheet

    Private Sub FormUnitOperationExtensionManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ExtensionMethods.ChangeDefaultFont(Me)
        PopulateUnitOperations()
    End Sub

    Private Sub PopulateUnitOperations()
        Dim prevTag As String = If(lvUnitOps.SelectedItems.Count > 0, lvUnitOps.SelectedItems(0).Text, Nothing)

        lvUnitOps.Items.Clear()

        For Each obj In Flowsheet.SimulationObjects.Values
            If TypeOf obj Is DWSIM.UnitOperations.BaseClasses.UnitOpBaseClass Then
                Dim item As New ListViewItem(obj.GraphicObject.Tag)
                item.SubItems.Add(obj.GetDisplayName())
                item.Tag = obj
                lvUnitOps.Items.Add(item)
            End If
        Next

        If lvUnitOps.Items.Count > 0 Then
            Dim restore = If(prevTag IsNot Nothing,
                             lvUnitOps.Items.Cast(Of ListViewItem).FirstOrDefault(Function(x) x.Text = prevTag),
                             Nothing)
            Dim target = If(restore IsNot Nothing, restore, lvUnitOps.Items(0))
            target.Selected = True
            target.EnsureVisible()
        Else
            clbExtensions.Items.Clear()
            ClearExtensionDetails()
        End If
    End Sub

    Private Sub lvUnitOps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvUnitOps.SelectedIndexChanged
        If lvUnitOps.SelectedItems.Count = 0 Then
            clbExtensions.Items.Clear()
            ClearExtensionDetails()
            Return
        End If

        Dim uo = DirectCast(lvUnitOps.SelectedItems(0).Tag,
                            DWSIM.UnitOperations.BaseClasses.UnitOpBaseClass)

        ' Suppress CheckItemChanged while populating
        RemoveHandler clbExtensions.ItemCheck, AddressOf clbExtensions_ItemCheck

        clbExtensions.Items.Clear()

        For Each kvp In FormFlowsheet.AvailableUnitOperationExtensions
            Dim isAttached = uo.AttachedExtensions IsNot Nothing AndAlso
                             uo.AttachedExtensions.Any(Function(x) x.Name = kvp.Key)
            clbExtensions.Items.Add(kvp.Value, isAttached)
        Next

        AddHandler clbExtensions.ItemCheck, AddressOf clbExtensions_ItemCheck

        If clbExtensions.Items.Count > 0 Then
            clbExtensions.SelectedIndex = 0
        Else
            ClearExtensionDetails()
        End If
    End Sub

    Private Sub clbExtensions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbExtensions.SelectedIndexChanged
        If clbExtensions.SelectedItem IsNot Nothing Then
            ShowExtensionDetails(DirectCast(clbExtensions.SelectedItem, Interfaces.IUnitOperationExtension))
        Else
            ClearExtensionDetails()
        End If
    End Sub

    Private Sub clbExtensions_ItemCheck(sender As Object, e As ItemCheckEventArgs)
        ' Defer so CheckedItems reflects the new state
        BeginInvoke(Sub() ApplyExtensionsToSelectedUnitOp())
    End Sub

    Private Sub ShowExtensionDetails(ext As Interfaces.IUnitOperationExtension)
        lblExtNameValue.Text = ext.Name
        lblExtDescValue.Text = ext.Description
        lblExtAuthorValue.Text = ext.Author
        lblExtWebsiteValue.Text = ext.Website
    End Sub

    Private Sub ClearExtensionDetails()
        lblExtNameValue.Text = ""
        lblExtDescValue.Text = ""
        lblExtAuthorValue.Text = ""
        lblExtWebsiteValue.Text = ""
    End Sub

    ''' <summary>Synchronises the selected unit op's AttachedExtensions with the current check state.</summary>
    Private Sub ApplyExtensionsToSelectedUnitOp()
        If lvUnitOps.SelectedItems.Count = 0 Then Return

        Dim uo = DirectCast(lvUnitOps.SelectedItems(0).Tag,
                            DWSIM.UnitOperations.BaseClasses.UnitOpBaseClass)

        If uo.AttachedExtensions Is Nothing Then
            uo.AttachedExtensions = New List(Of Interfaces.IUnitOperationExtension)
        End If

        Dim newList As New List(Of Interfaces.IUnitOperationExtension)

        For i = 0 To clbExtensions.Items.Count - 1
            If clbExtensions.GetItemChecked(i) Then
                Dim ext = DirectCast(clbExtensions.Items(i), Interfaces.IUnitOperationExtension)
                ' Reuse an existing instance if already attached; otherwise create a new one
                Dim existing = uo.AttachedExtensions.FirstOrDefault(Function(x) x.Name = ext.Name)
                newList.Add(If(existing, ext.NewInstance()))
            End If
        Next

        uo.AttachedExtensions = newList
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
