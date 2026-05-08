Public Class Object

    Inherits SharedClasses.ObjectEditorForm

    Public fsuo As UnitOperations.Flowsheet

    Private Sub FlowsheetUOViewerForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            fsuo.InitializeInternalFlowsheet()
        Catch ex As Exception
        End Try

        If fsuo.Fsheet Is Nothing Then
'            MessageBox.Show("Please select and setup the internal flowsheet so it can be displayed here.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        fsuo.Fsheet.GetSurfaceControl().Flowsheet = fsuo.Fsheet
        fsuo.Fsheet.GetSurfaceControl().FControl.FlowsheetObject = fsuo.Fsheet
        Me.Controls.Add(fsuo.Fsheet.GetSurfaceControl().FControl)
        fsuo.Fsheet.GetSurfaceControl().FlowsheetSurface.ZoomAll(Width, Height)
        fsuo.Fsheet.GetSurfaceControl().FlowsheetSurface.ZoomAll(Width, Height)
        fsuo.Fsheet.GetSurfaceControl().Invalidate()
        Me.Invalidate()

    End Sub

End Class