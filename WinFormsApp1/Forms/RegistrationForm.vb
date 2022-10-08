Public Class RegistrationForm
    Dim member As New DatabaseClass
    Dim setup As New SetupClass
    Private Sub RegistrationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGV_Residents.AutoGenerateColumns = False
        DGV_Residents.DataSource = member.getdata("TB_residents", "resident_id")
        defaultState()
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
        btnUpdate.Enabled = False

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
        btnUpdate.Enabled = False

        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtLastName.Text = ""
        txtContact.Text = ""
        txtAddress.Text = ""

    End Sub
End Class