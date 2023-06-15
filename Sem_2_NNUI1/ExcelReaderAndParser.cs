using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_2_NNUI1
{
    public class ExcelReaderAndParser
    {
        private StringBuilder data;

        public string[] ReadTheExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            data = new StringBuilder();
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(filePath)))
            {

                var myWorksheet = xlPackage.Workbook.Worksheets.First();
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;


                for (int rowNum = 3; rowNum <= totalRows; rowNum++)
                {
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    data.AppendLine(string.Join(",", row));
                }
            }
            return data.ToString().Split('\n');
        }
        public List<Pub> ParseLinesToListOfPubs(string[] lines)
        {
            List<Pub> pubs = new List<Pub>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                var items = line.Split(',');
                int id = int.Parse(items[0].Trim());
                string name = items[1].Trim();
                double x = double.Parse(items[2].Trim().Replace('.', ','));
                double y = double.Parse(items[3].Trim().Replace('.', ','));

                pubs.Add(new Pub { Id = id, Name = name, Position = new PointWithDouble { X = x, Y = y } });
            }
            return pubs;
        }
    }
}
