Module Module1

    Sub Main()
        Dim testi As New WDK.ContentManagement.Testimonials.Manager

        Dim entry As New WDK.ContentManagement.Testimonials.TestimonialType
        entry.Author = "me"

        testi.Add(entry)
    End Sub

End Module
