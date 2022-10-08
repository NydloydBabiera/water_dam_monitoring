Imports System.Data
Imports System.Data.SqlClient
Public Class DatabaseClass

    Dim c_Connection As New SqlConnection("Data Source=DESKTOP-FV3MKIT\SQL;Initial Catalog=DB_WaterDamMonitoring;User ID=sa;Password=123456")
    Dim c_Command As New SqlCommand
    Dim c_DataAdapter As New SqlDataAdapter
    Dim c_DataTable As New DataTable

    Dim c_Columname As String
    Dim c_Values As String

    Dim c_UpdateColumn As String
    Dim c_WhereClause As String


    Public Function getdata(ByVal databaseTable As String, ByVal tableId As String) As DataTable
        c_DataTable = New DataTable

        c_Connection.Open()
        c_DataAdapter = New SqlDataAdapter("Select * from " & databaseTable & " ORDER BY " & tableId & " DESC ", c_Connection)
        c_DataAdapter.SelectCommand.ExecuteNonQuery()
        c_DataAdapter.Fill(c_DataTable)
        c_Connection.Close()

        Return c_DataTable
    End Function
    Public Function getdataRestricted(ByVal databaseTable As String, ByVal whereClause As String) As DataTable
        c_DataTable = New DataTable

        c_Connection.Open()
        c_DataAdapter = New SqlDataAdapter("Select * from " & databaseTable & " where " & whereClause, c_Connection)
        c_DataAdapter.SelectCommand.ExecuteNonQuery()
        c_DataAdapter.Fill(c_DataTable)
        c_Connection.Close()

        Return c_DataTable
    End Function

    Public Function getProdPurchase(ByVal databaseTable As String, ByVal whereClause As String) As DataTable
        c_DataTable = New DataTable

        c_Connection.Open()
        c_DataAdapter = New SqlDataAdapter("Select * from " & databaseTable & " where " & whereClause, c_Connection)
        c_DataAdapter.SelectCommand.ExecuteNonQuery()
        c_DataAdapter.Fill(c_DataTable)
        c_Connection.Close()

        Return c_DataTable
    End Function

    Public Function getSpecificData(ByVal dbTable As String, ByVal whereClause As String) As DataTable
        c_DataTable = New DataTable

        c_Connection.Open()
        c_DataAdapter = New SqlDataAdapter("Select * from " & dbTable & " where " & whereClause, c_Connection)
        c_DataAdapter.SelectCommand.ExecuteNonQuery()
        'IDstore = c_DataAdapter
        c_DataAdapter.Fill(c_DataTable)
        c_Connection.Close()

        Return c_DataTable
    End Function
    Public Function get_prodStatus(ByVal dbTable As String, ByVal dbColumn As String, ByVal whereClause As String) As String

        Dim VALUE As Boolean

        c_Connection.Open()
        c_Command = New SqlCommand("Select " & dbColumn & " from " & dbTable & " where " & whereClause, c_Connection)
        c_Command.CommandType = CommandType.Text
        VALUE = c_Command.ExecuteScalar
        c_Connection.Close()

        Return VALUE
    End Function

    Public Function get_transactionDetails(ByVal dbColumn As String, ByVal whereClause As String) As String
        Dim VALUE As String
        Dim obj As Object

        c_Connection.Open()
        c_Command = New SqlCommand("Select " & dbColumn & " from TB_SalesTransaction trans " _
                                   & " inner join TB_UserInfo userInfo on userInfo.user_id = trans.user_id " _
                                   & " left join TB_Discount disc on disc.discount_id = trans.discount_id " _
                                   & " where trans.sales_id =  " & whereClause, c_Connection)
        c_Command.CommandType = CommandType.Text
        obj = c_Command.ExecuteScalar
        c_Connection.Close()

        If obj Is Nothing Then
            VALUE = ""
        Else
            VALUE = obj.ToString
        End If


        Return VALUE
    End Function

    Public Function getTransactionLine(ByVal whereClause As String) As DataTable
        c_DataTable = New DataTable

        c_Connection.Open()
        c_DataAdapter = New SqlDataAdapter(" Select * from TB_SalesTransaction trans " _
                    & " inner join TB_SalesTransactionLine line on line.sales_id = trans.sales_id " _
                    & " inner join TB_Product prod on prod.product_id = line.product_id " _
                    & " where trans.sales_id = " & whereClause, c_Connection)
        c_DataAdapter.SelectCommand.ExecuteNonQuery()
        c_DataAdapter.Fill(c_DataTable)
        c_Connection.Close()

        Return c_DataTable
    End Function
    Public Function get_discountPercent(ByVal dbTable As String, ByVal dbColumn As String, ByVal whereClause As String) As Decimal

        Dim VALUE As Decimal

        c_Connection.Open()
        c_Command = New SqlCommand("Select " & dbColumn & " from " & dbTable & " where " & whereClause, c_Connection)
        c_Command.CommandType = CommandType.Text
        VALUE = c_Command.ExecuteScalar
        c_Connection.Close()

        Return VALUE
    End Function

    Public Function get_value(ByVal dbTable As String, ByVal dbColumn As String, ByVal whereClause As String) As String

        Dim VALUE As String
        Dim obj As Object
        c_Connection.Open()
        c_Command = New SqlCommand("Select " & dbColumn & " from " & dbTable & " where " & whereClause, c_Connection)
        c_Command.CommandType = CommandType.Text
        obj = c_Command.ExecuteScalar
        c_Connection.Close()

        If obj Is Nothing Then
            VALUE = ""
        Else
            VALUE = obj.ToString
        End If

        Return VALUE
    End Function

    Public Sub AddRecord(ByVal databaseTable As String)
        c_Connection.Open()
        c_Command = New SqlCommand("insert into " & databaseTable & "(" & c_Columname & ") values (" & c_Values & ")", c_Connection)
        c_Command.ExecuteNonQuery()
        c_Connection.Close()
    End Sub
    Public Sub setColumn(ByVal columnName As String)
        If c_Columname = String.Empty Then
            c_Columname = columnName
        Else
            c_Columname = c_Columname & ", " & columnName
        End If
    End Sub

    Public Sub setValues(ByVal val As String)
        If c_Values = String.Empty Then
            c_Values = "'" & val & "'"
        Else
            c_Values = c_Values & ", '" & val & "'"
        End If
    End Sub
    Public Sub updateRecords(ByVal dbTable As String)
        c_Connection.Open()
        c_Command = New SqlCommand("Update " & dbTable & " set " & c_UpdateColumn & " where " & c_WhereClause, c_Connection)
        c_Command.ExecuteNonQuery()
        c_Connection.Close()
    End Sub
    Public Sub SetColumnUpdateRecord(ByVal columnName As String, ByVal columnValue As String)

        If c_UpdateColumn = String.Empty Then
            c_UpdateColumn = columnName & "= '" & columnValue & "'"
        Else
            c_UpdateColumn &= ", " & columnName & "= '" & columnValue & "'"
        End If
    End Sub

    Public Sub setwhereClause(ByVal value As String)
        c_WhereClause = value
    End Sub
    Public Sub clearItems()
        c_Columname = String.Empty
        c_Values = String.Empty
        c_UpdateColumn = String.Empty
        c_WhereClause = String.Empty
    End Sub

    Public Sub DeleteRecords(ByVal dbTable As String, ByVal whereclause As String)
        c_Connection.Open()
        c_Command = New SqlCommand("Delete from " & dbTable & " where " & whereclause, c_Connection)
        c_Command.ExecuteNonQuery()
        c_Connection.Close()
    End Sub

    Public Function searchData(ByVal databaseTable As String, ByVal whereClause As String) As DataTable
        c_DataTable = New DataTable

        c_Connection.Open()
        c_DataAdapter = New SqlDataAdapter("Select * from " & databaseTable & " where " & whereClause, c_Connection)
        c_DataAdapter.SelectCommand.ExecuteNonQuery()
        c_DataAdapter.Fill(c_DataTable)
        c_Connection.Close()

        Return c_DataTable

    End Function
End Class
