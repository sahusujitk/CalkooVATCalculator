using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NUnit.Framework.Internal;

namespace CalckooTest
{
    public class ExcelReader
    {
        public static List<object[]> ReadExcelFile(string filePath)
        {
            var testData = new List<object[]>();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
            {
                
                bool headerSkipped = false;
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                // Read data from each row
                foreach (Row row in sheetData.Elements<Row>())
                {
                    List<object> list = new List<object>(); 
                    if (!headerSkipped)
                    {
                        headerSkipped = true;
                        continue;
                    }

                    // Read data from each cell in the row
                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        string cellValue = GetCellValue(cell, workbookPart);
                        list.Add(cellValue);
                    }
                    testData.Add(list.ToArray());
                }
            }
            return testData;
        }

        private static string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            string value = cell.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                if (sharedStringPart != null)
                {
                    value = sharedStringPart.SharedStringTable.ChildElements[int.Parse(value)].InnerText;
                }
            }

            return value;
        }
    }
}
