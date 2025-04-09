using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using backend;


public static class ReportPdfGenerator
{
    public static byte[] Generate(Report report)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.Content()
                    .Column(col =>
                    {
                        col.Item().PaddingBottom(10).Text("Auto Diagnostic Report")
                            .FontSize(24)
                            .Bold();// ✅ Replaced SpacingBottom with valid method

                        col.Item().Text($"VIN: {report.VIN}");
                        col.Item().Text($"Date: {report.Date}");
                        col.Item().Text($"Issues: {report.Issues}");
                    });
            });
        });

        return document.GeneratePdf(); // ✅ Generates valid PDF bytes
    }
}








