Imports System.Drawing
Imports System.Windows.Forms

Module UiScale
    Private ReadOnly Baselines As New Dictionary(Of String, Single)(StringComparer.Ordinal)

    Public Sub Apply(frm As Form, scale As Single)
        If scale <= 0F Then scale = 1F

        Dim key = frm.GetType().FullName
        Dim basePt As Single
        If Not Baselines.TryGetValue(key, basePt) Then
            basePt = frm.Font.SizeInPoints
            Baselines(key) = basePt
        End If

        frm.AutoScaleMode = AutoScaleMode.Font
        frm.AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        frm.Font = New Font(frm.Font.FontFamily, basePt * scale, frm.Font.Style)
        frm.PerformAutoScale()

        For Each c As Control In Enumerate(frm)
            If TypeOf c Is Label Then
                Dim lbl = DirectCast(c, Label)
                lbl.AutoSize = True
                lbl.AutoEllipsis = True
            ElseIf TypeOf c Is CheckBox OrElse TypeOf c Is RadioButton OrElse TypeOf c Is Button Then
                c.AutoSize = True
            ElseIf Type Of c Is DataGridView Then
                Dim gv = DirectCast(c, DataGridView)
                gv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
                gv.RowTemplate.Height = Math.Max(18, CInt(18 * scale))
            ElseIf TypeOf c Is ToolStrip Then
                Dim ts = DirectCast(c, ToolStrip)
                Dim s = Math.Max(16, CInt(16 * scale))
                ts.ImageScalingSize = New Size(s, s)
                ts.Font = New Font(ts.Font.FontFamily, ts.Font.SizeInPoints * scale, ts.Font.Style)
            End If
        Next
    End Sub

    Private Iterator Function Enumerate(root As Control) As IEnumerable(Of Control)
        Dim stack As New Stack(Of Control)()
        stack.Push(root)
        While stack.Count > 0
            Dim cur = stack.Pop()
            Yield cur
            For Each child As Control In cur.Controls
                stack.Push(child)
            Next
        End While
    End Function
End Module
