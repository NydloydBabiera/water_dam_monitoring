Public Class RegistrationForm
    Dim member As New DatabaseClass
    Dim setup As New SetupClass
    Dim isEdit As Boolean = False
    Private Sub RegistrationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGV_Residents.AutoGenerateColumns = False
        DGV_Residents.DataSource = member.getdata("TB_residents", "resident_id")
        defaultState()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If isEdit = True Then
            With member
                .clearItems()
                .setwhereClause("resident_id=" & setup.getrowdata(DGV_Residents, 0))
                .SetColumnUpdateRecord("firstName", txtFirstName.Text)
                .SetColumnUpdateRecord("middleName", txtMiddleName.Text)
                .SetColumnUpdateRecord("lastName", txtLastName.Text)
                .SetColumnUpdateRecord("contactNo", txtContact.Text)
                .SetColumnUpdateRecord("address", txtAddress.Text)
                .updateRecords("TB_residents")
            End With
            MsgBox("Data Updated")
        ElseIf isEdit = False Then

            With member
            .clearItems()
            .setColumn("firstName")
            .setColumn("middleName")
            .setColumn("lastName")
            .setColumn("contactNo")
            .setColumn("address")
            .setValues(txtFirstName.Text)
            .setValues(txtMiddleName.Text)
            .setValues(txtLastName.Text)
            .setValues(txtContact.Text)
            .setValues(txtAddress.Text)

            .AddRecord("TB_residents")

        End With
            MsgBox("Successfully saved!")
        End If

        DGV_Residents.DataSource = member.getdata("TB_residents", "resident_id")
        defaultState()
    End Sub

    Sub defaultState()


        txtFirstName.Enabled = False
        txtMiddleName.Enabled = False
        txtLastName.Enabled = False
        txtContact.Enabled = False
        txtAddress.Enabled = False

        btnAdd.Enabled = True
        btnEdit.Enabled = False
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnCancel.Enabled = False

        DGV_Residents.Enabled = True

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        txtFirstName.Enabled = True
        txtMiddleName.Enabled = True
        txtLastName.Enabled = True
        txtContact.Enabled = True
        txtAddress.Enabled = True

        btnAdd.Enabled = True
        btnEdit.Enabled = False
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnCancel.Enabled = True

        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtLastName.Text = ""
        txtContact.Text = ""
        txtAddress.Text = ""

    End Sub



    Private Sub DGV_Residents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Residents.CellClick
        residentID = setup.getrowdata(DGV_Residents, 0)

        btnAdd.Enabled = False
        btnEdit.Enabled = True
        btnSave.Enabled = False
        btnDelete.Enabled = True
        btnCancel.Enabled = True
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        defaultState()
    End Sub


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        btnAdd.Enabled = False
        btnEdit.Enabled = False
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnCancel.Enabled = True

        txtFirstName.Enabled = True
        txtMiddleName.Enabled = True
        txtLastName.Enabled = True
        txtContact.Enabled = True
        txtAddress.Enabled = True
        txtSearch.Enabled = False

        isEdit = True

        With setup
            txtFirstName.Text = .getrowdata(DGV_Residents, 1)
            txtMiddleName.Text = .getrowdata(DGV_Residents, 2)
            txtLastName.Text = .getrowdata(DGV_Residents, 3)
            txtContact.Text = .getrowdata(DGV_Residents, 4)
            txtAddress.Text = .getrowdata(DGV_Residents, 5)
        End With

        DGV_Residents.Enabled = False
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim dr As New DialogResult
        dr = MessageBox.Show("Delete data?", "CONFIRMATION", MessageBoxButtons.YesNoCancel)
        If dr = Windows.Forms.DialogResult.Yes Then
            member.DeleteRecords("TB_residents", "resident_id = " & residentID)
        End If
        DGV_Residents.DataSource = member.getdata("TB_residents", "resident_id")
        defaultState()
    End Sub

    Private Sub txtContact_TextChanged(sender As Object, e As EventArgs) Handles txtContact.TextChanged

    End Sub

    Private Sub txtContact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContact.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) And Not e.KeyChar = "." Then
            e.Handled = True
        End If
    End Sub



    Private Sub txtFirstName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFirstName.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtMiddleName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMiddleName.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtLastName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLastName.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        DGV_Residents.DataSource = member.getSpecificData("TB_residents", "firstName like '" & txtSearch.Text & "%' OR middleName like '" & txtSearch.Text & "%' OR lastName like '" & txtSearch.Text & "%' OR contactNo like '" & txtSearch.Text & "%' OR address like '" & txtSearch.Text & "%'")
    End Sub
End Class