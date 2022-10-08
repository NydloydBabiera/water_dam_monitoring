Public Class RegistrationForm
    Dim member As New DatabaseClass
    Dim setup As New SetupClass
    Private Sub RegistrationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGV_Residents.AutoGenerateColumns = False
        DGV_Residents.DataSource = member.getdata("TB_residents", "resident_id")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
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

        DGV_Residents.DataSource = member.getdata("TB_residents", "resident_id")
    End Sub
End Class