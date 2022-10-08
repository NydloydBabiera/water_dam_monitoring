Public Class SetupClass
    Public Function getrowdata(ByVal dgv As Windows.Forms.DataGridView, ByVal columnindex As Int16) As String

        If IsDBNull(dgv.Item(columnindex, dgv.CurrentCell.RowIndex).Value) Then
            Return ""
        Else
            Return dgv.Item(columnindex, dgv.CurrentCell.RowIndex).Value
        End If
    End Function
End Class
