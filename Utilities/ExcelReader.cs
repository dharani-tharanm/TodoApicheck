using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using TodoApi.Models; // Ensure you add the correct namespace for TodoItem

namespace TodoApi.Utilities // Adjust namespace to match your project structure
{
    public static class ExcelReader
    {
        public static List<TodoItem> LoadTasksFromExcel(string filePath)
        {
            var tasks = new List<TodoItem>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Task"];
                //var worksheet = package.Workbook.Worksheets[0];
                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    var description = worksheet.Cells[row, 1].Text;
                    tasks.Add(new TodoItem { Id = row - 1, Description = description });
                }
            }
            return tasks;
        }
    }
}
