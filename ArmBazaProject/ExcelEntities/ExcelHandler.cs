using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using ArmBazaProject.ViewModels;

namespace ArmBazaProject.ExcelEntities
{
    public class ExcelHandler
    {
        Application excel;
        //draw
        Worksheet manSheetLeftHand;
        Worksheet womanSheetLeftHand;
        Worksheet womanSheetRightHand;
        Worksheet manSheetRightHand;

        //resultHands
        Worksheet womanSheetResultHands;
        Worksheet manSheetResultHands;

        CompetitionViewModel competition;
        public ResultViewModel result;

        public ExcelHandler(CompetitionViewModel competitionViewModel)
        {
            excel = new Application();
            competition = competitionViewModel;
        }


        #region
        public void SetApplicationParametersForTwoHandsRelults()
        {

            Workbook workbook = excel.Workbooks.Add();
            manSheetResultHands = (Worksheet)workbook.Sheets[1];
            manSheetResultHands.Name = "Мужчины";

            womanSheetResultHands = (Worksheet)excel.Worksheets.Add();
            womanSheetResultHands.Name = "Женщины";
        }


        public void SaveTwoHandsRelultsCategoriesData(CategoryViewModel[] categories, Worksheet sheet)
        {
            string[] resultHandGridHeaders = new string[] { "Имя", "Команда", "Вес", "Место Левая", "Очки Левая", "Место Правая", "Очки Правая", "Место" };
            List<List<string>> content = new List<List<string>>();

            int index_x = 0;
            int index_Stopped = 0;



            //назначение заголовий
            


            foreach (CategoryViewModel category in categories)
            {
                content = new List<List<string>>();

                Range myRangeWeight = (Range)sheet.Cells[index_x + 2, 1];
                sheet.Cells[index_x + 1, 1].Font.Bold = true;
                sheet.Columns[ 1].ColumnWidth = 15;
                myRangeWeight.Value2 = category.WeightCategory.WeightName;

                index_x += 1;

                for (int j = 0; j < resultHandGridHeaders.Length; j++)
                {
                    Range myRange = (Range)sheet.Cells[index_x + 2, j + 1];
                    sheet.Cells[index_x + 1, j + 1].Font.Bold = true;
                    sheet.Columns[j + 1].ColumnWidth = 15;
                    myRange.Value2 = resultHandGridHeaders[j];

                }

                index_x += 1;

                foreach (MemberViewModel member in category.ResultMembers)
                {
                    content.Add(new List<string>() { member.Member.FullName,
                                                 member.TeamName,
                                                 member.Member.Weight.ToString(),
                                                 member.LeftHandPlaceVM,
                                                 member.LeftHandScoreVM,
                                                 member.RightHandPlaceVM,
                                                 member.RightHandScoreVM,
                                                 member.ResultHandPlace.ToString()
                    });
                }

                for (int i = 0; i < resultHandGridHeaders.Length; i++)
                {
                    for (int j = 0; j < content.Count; j++)
                    {
                        for (int k = 0; k < content[j].Count; k++)
                        {
                            Range myRange = (Range)sheet.Cells[index_x + j + 2, k + 1];
                            myRange.Value2 = content[j][k];
                        }
                    }
                    index_Stopped = content.Count + index_x;
                }

                index_x = index_Stopped + 1;
            }


            

        }

        public void SaveAllTwoHandsRelultsData()
        {

            SaveTwoHandsRelultsCategoriesData(result.ResultCategoryBoys, manSheetResultHands);
            SaveTwoHandsRelultsCategoriesData(result.ResultCategoryGirls, womanSheetResultHands);

            excel.Visible = true;
        }


        #endregion

        #region Draw 

        public void SetApplicationParametersForDraw()
        {

            Workbook workbook = excel.Workbooks.Add();
            manSheetLeftHand = (Worksheet)workbook.Sheets[1];
            manSheetLeftHand.Name = "МужчиныЛевая";
            
            womanSheetLeftHand = (Worksheet)excel.Worksheets.Add();
            womanSheetLeftHand.Name = "ЖенщиныЛевая";
            
            womanSheetRightHand = (Worksheet)excel.Worksheets.Add();
            womanSheetRightHand.Name = "ЖенщиныПравая";
            
            manSheetRightHand = (Worksheet)excel.Worksheets.Add();
            manSheetRightHand.Name = "МужчиныПравая";
        }

        public void SaveAllDrawData()
        {

            SaveDrawCategoriesData(competition.CompetitionLeftHand.CategoriesB, manSheetLeftHand);
            SaveDrawCategoriesData(competition.CompetitionLeftHand.CategoriesG, womanSheetLeftHand);
            SaveDrawCategoriesData(competition.CompetitionRightHand.CategoriesB, manSheetRightHand);
            SaveDrawCategoriesData(competition.CompetitionRightHand.CategoriesG, womanSheetRightHand);

            excel.Visible = true;
        }

        public void SaveDrawCategoriesData(CategoryViewModel[] categories, Worksheet sheet)
        {
            List<string> categoriesWeight = new List<string>();
            foreach (CategoryViewModel category in categories)
            {
                categoriesWeight.Add(category.WeightCategory.WeightName);
            }
            List<List<string>> content = new List<List<string>>();

            //назначение заголовков по категориям
            for (int j = 0; j < categoriesWeight.Count; j++)
            {
                Range myRange = (Range)sheet.Cells[2, j + 1];
                sheet.Cells[1, j + 1].Font.Bold = true;
                sheet.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = categoriesWeight[j];
            }

            
            foreach (CategoryViewModel category in categories)
            {
                List<string> categoryNames = new List<string>();
                foreach(MemberViewModel member in category.AllMembers){
                    categoryNames.Add(member.Member.FullName);
                }
                content.Add(categoryNames);
            }


            for (int i = 0; i < categoriesWeight.Count; i++)
            {
                for (int j = 0; j < content.Count; j++)
                {
                    for (int k = 0; k < content[j].Count; k++)
                    {
                        Range myRange = (Range)sheet.Cells[k + 3, j + 1];
                        myRange.Value2 = content[j][k];
                    }
                }
            }

        }

        #endregion


    }
}
