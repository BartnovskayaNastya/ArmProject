using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ArmBazaProject.UserControlWindows
{
    /// <summary>
    /// Логика взаимодействия для ResultHandsTableTemplate.xaml
    /// </summary>
    public partial class ResultHandsTableTemplate : UserControl
    {
        public ResultHandsTableTemplate()
        {
            InitializeComponent();
        }

        private void svT_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineUp();
            }
            else
            {
                scrollviewer.LineDown();
            }
        }

        //private void exportResultsButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Excel.Application excel = new Excel.Application();
        //    excel.Visible = true;
        //    Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
        //    Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

        //    for (int j = 0; j <  .Columns.Count; j++)
        //    {
        //        Range myRange = (Range)sheet1.Cells[1, j + 1];
        //        sheet1.Cells[1, j + 1].Font.Bold = true;
        //        sheet1.Columns[j + 1].ColumnWidth = 15;
        //        myRange.Value2 = dgrid.Columns[j].Header;
        //    }
        //    for (int i = 0; i < dgrid.Columns.Count; i++)
        //    {
        //        for (int j = 0; j < dgrid.Items.Count; j++)
        //        {
        //            TextBlock b = dgrid.Columns[i].GetCellContent(dgrid.Items[j]) as TextBlock;
        //            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
        //            myRange.Value2 = b.Text;
        //        }
        //    }
        //}

    }
}
