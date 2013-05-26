Module Module1

    Sub Main()
        Dim query As New WDK.Utilities.SQL.CriteriaBuilder
        Dim startDate As String = FormatDateTime(Now, DateFormat.GeneralDate)
        Dim endDate As String = FormatDateTime(DateAdd("d", -7, Now), 0)

        query.Add("adminviewed", "AdminViewed", WDK.Utilities.SQL.CriteriaBuilder.CriteriaOperation.EQUAL, False, WDK.Utilities.SQL.CriteriaBuilder.CriteriaLogic.OR)
        query.Add("date", "ReportDate", WDK.Utilities.SQL.CriteriaBuilder.CriteriaOperation.BETWEEN, "#" + startDate + "#" + " AND #" + endDate + "#", WDK.Utilities.SQL.CriteriaBuilder.CriteriaLogic.AND)
        query.Add("b", "IsAvailable", WDK.Utilities.SQL.CriteriaBuilder.CriteriaOperation.EQUAL, True)
        query.Add("b", "Some", WDK.Utilities.SQL.CriteriaBuilder.CriteriaOperation.EQUAL, "hello there")
        query.Add("b", "Counter", WDK.Utilities.SQL.CriteriaBuilder.CriteriaOperation.EQUAL, 120, WDK.Utilities.SQL.CriteriaBuilder.CriteriaLogic.AND)
        Debug.WriteLine("SELECT * FROM Table1 WHERE App=@app AND (" + query.ToString + ")")
    End Sub

End Module
