using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using ArmBazaProject.windows;
using System.Windows.Input;
using ArmBazaProject.ViewModels;
using ArmBazaProject.Entities;
using ArmBazaProject.ExcelEntities;
using System;

namespace ArmBazaProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext dataBase;
        DataBaseModel dataBaseModel;
        CompetitionViewModel competitionVM;
        ResultViewModel resultVM;
        bool isCompetitionStarted = false;
        Serializator serializator;
        

        public MainWindow()
        {
            InitializeComponent();

            #region БД
            dataBase = new ApplicationContext();
            dataBaseModel = new DataBaseModel();

            dataBase.Members.Load();
            dataBase.Teams.Load();
            #endregion

            #region прикрепление данных к листам из бд

            

            teamsList.ItemsSource = dataBase.Teams.Local.ToBindingList();
            competitorList.ItemsSource = dataBase.Members.Local.ToBindingList();
            #endregion
            serializator = new Serializator();
            competitionVM = new CompetitionViewModel();
            ageCategoryCB.ItemsSource = competitionVM.categories;
            pointsCB.ItemsSource = competitionVM.points;


        }

        #region Members Edit

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            /*MemberWindow memberWindow = new MemberWindow(new Member());
            if (memberWindow.ShowDialog() == true)
            {
                Member member = memberWindow.changedMember;
                dataBase.Members.Add(member);
                dataBase.SaveChanges();
            }*/
        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {
            /*
            //  если ни одного объекта не выделено, выходим
            if (competitorList.SelectedItem == null) return;
            // получаем выделенный объект
            Member member = competitorList.SelectedItem as Member;

            MemberWindow windowOfChange = new MemberWindow(new Member
            {
                Id = member.Id,
                FullName = member.FullName,
                QualificationId = member.QualificationId,
                TeamId = member.TeamId,
                DateOfBirth = member.DateOfBirth,
                Weight = member.Weight,
                Gender = member.Gender

            }) ;

            windowOfChange.genderCB.SelectedItem = member.Gender;



            if (windowOfChange.ShowDialog() == true)
            {
                member = dataBase.Members.Find(windowOfChange.changedMember.Id);
                if (member != null)
                {
                    member.FullName = windowOfChange.changedMember.FullName;
                    member.QualificationId = windowOfChange.changedMember.QualificationId;
                    member.DateOfBirth = windowOfChange.changedMember.DateOfBirth;
                    member.TeamId = windowOfChange.changedMember.TeamId;
                    member.Weight = windowOfChange.changedMember.Weight;
                    member.Gender = windowOfChange.changedMember.Gender;
                    dataBase.Entry(member).State = EntityState.Modified;
                    dataBase.SaveChanges();
                }
            }*/

        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {/*
            // если ни одного объекта не выделено, выходим
            if (competitorList.SelectedItem == null) return;
            // получаем выделенный объект
            Member member = competitorList.SelectedItem as Member;
            dataBase.Members.Remove(member);
            dataBase.SaveChanges();*/
        }

        #endregion

        #region Team Edit

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow teamWindow = new TeamWindow(new Team());
            if (teamWindow.ShowDialog() == true)
            {
                Team team = teamWindow.changedTeam;
                team.RegionId = 0;
                team.TrainerName = "Name";
                dataBase.Teams.Add(team);
                dataBase.SaveChanges();

            }
            TeamComboBox.ItemsSource = competitionVM.DataBaseModel.GetAllTeams();
        }

        private void EditTeam_Click(object sender, RoutedEventArgs e)
        {
            //  если ни одного объекта не выделено, выходим
            if (teamsList.SelectedItem == null) return;
            // получаем выделенный объект
            Team team = teamsList.SelectedItem as Team;

            TeamWindow teamWindow = new TeamWindow(new Team
            {
                TrainerName = team.TrainerName,
                RegionId = team.RegionId,
                Name = team.Name,
                Id = team.Id
            }) ;



            if (teamWindow.ShowDialog() == true)
            {
                team = dataBase.Teams.Find(teamWindow.changedTeam.Id);
                if (team != null)
                {
                    team.Name = teamWindow.changedTeam.Name;
                    team.TrainerName = teamWindow.changedTeam.TrainerName;
                    team.RegionId = teamWindow.changedTeam.RegionId;
                    dataBase.Entry(team).State = EntityState.Modified;
                    dataBase.SaveChanges();
                }
            }
        }

        private void DeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (teamsList.SelectedItem == null) return;
            // получаем выделенный объект
            Team team = teamsList.SelectedItem as Team;
            dataBase.Teams.Remove(team);
            dataBase.SaveChanges();
        }

        #endregion

        #region Trainer Edit

        //private void AddTrainer_Click(object sender, RoutedEventArgs e)
        //{
        //    TrainerWindow trainerWindow = new TrainerWindow(new Trainer());
        //    if (trainerWindow.ShowDialog() == true)
        //    {
        //        Trainer trainer = trainerWindow.changedTrainer;
        //        dataBase.Trainers.Add(trainer);
        //        dataBase.SaveChanges();
        //    }
        //}

        //private void EditTrainer_Click(object sender, RoutedEventArgs e)
        //{
        //    //  если ни одного объекта не выделено, выходим
        //    if (trainersList.SelectedItem == null) return;
        //    // получаем выделенный объект
        //    Trainer trainer = trainersList.SelectedItem as Trainer;

        //    TrainerWindow trainerWindow = new TrainerWindow(new Trainer
        //    {
        //        FullName = trainer.FullName,
        //        Id = trainer.Id
        //    });



        //    if (trainerWindow.ShowDialog() == true)
        //    {
        //        trainer = dataBase.Trainers.Find(trainerWindow.changedTrainer.Id);
        //        if (trainer != null)
        //        {
        //            trainer.FullName = trainerWindow.changedTrainer.FullName;
        //            dataBase.Entry(trainer).State = EntityState.Modified;
        //            dataBase.SaveChanges();
        //        }
        //    }
        //}

        //private void DeleteTrainer_Click(object sender, RoutedEventArgs e)
        //{
        //    // если ни одного объекта не выделено, выходим
        //    if (teamsList.SelectedItem == null) return;
        //    // получаем выделенный объект
        //    Team team = teamsList.SelectedItem as Team;
        //    dataBase.Teams.Remove(team);
        //    dataBase.SaveChanges();
        //}

        #endregion


        private void dg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (membersGrid.SelectedItem != null)
                {
                    MemberViewModel memberItem = (MemberViewModel)membersGrid.SelectedItem;
                    competitionVM.AllMembers.Remove(memberItem);
                }
            }
        }


        private void cellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //MemberViewModel member;
            //member = ((MemberViewModel)membersGrid.SelectedItem);
            //member.Member.WeightCategory = competitionVM.GetWeightCategory(member.Member.Weight, member.Member.Gender).ToString();
        }


        private void membersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void findInDBButton_Click(object sender, RoutedEventArgs e)
        {
            //bdMembersList.ItemsSource = competitionVM.GetInfoAboutMembersByParam(searchePanel.Text);
        }

        private void addMemberButton_Click(object sender, RoutedEventArgs e)
        {
            //MemberViewModel member = (MemberViewModel)bdMembersList.SelectedItem;
            //competitionVM.AllMembers.Add(member);
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            //searchePanel.Clear();
        }

        private void getResultsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                resultVM = new ResultViewModel(competitionVM);
                resultVM.GetResultsTwoHand();
                resultTableBoys.DataContext = resultVM.ResultCategoryBoys;
                resultTableGirls.DataContext = resultVM.ResultCategoryGirls;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message  + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }
        }

        private void ColumnDefinition_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        //ТУРНИРНЫЕ ТАБЛИЦЫ
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isCompetitionStarted)
                {
                    competitionVM.SortAllMembers(dataBaseModel.GetAllCategories("ж", competitionVM.CompetitionLeftHand.CategoryName),
                    dataBaseModel.GetAllCategories("м", competitionVM.CompetitionLeftHand.CategoryName));

                    //жеребьевка всех категорий
                    competitionVM.RandomDrawForAll();

                    //исправить добавление без проблем!
                    /* TabBoysLeftHand.DataContext = null;
                     TabBoysRighHand.DataContext = null;
                     TabGirlsLeftHand.DataContext = null;
                     TabGirlsRighHand.DataContext = null;*/

                    //турнирные
                    TabBoysLeftHand.DataContext = competitionVM.CompetitionLeftHand.CategoriesB;
                    TabBoysRighHand.DataContext = competitionVM.CompetitionRightHand.CategoriesB;
                    TabGirlsLeftHand.DataContext = competitionVM.CompetitionLeftHand.CategoriesG;
                    TabGirlsRighHand.DataContext = competitionVM.CompetitionRightHand.CategoriesG;

                    //ComandScore.DataContext = competitionVM.

                    MessageBox.Show("Турнирные таблицы успешно созданы. Жеребьевка была проведена для каждой категории автоматически", "Уведомление");
                    isCompetitionStarted = true;
                    saveDrawButton.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }



        }

        private void findInDBButton_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = 5;
                int y = x / 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }
            //DBList.ItemsSource = competitionVM.GetInfoAboutMembersByParam(searchDB.Text);
        }

        private void resetButton_Click2(object sender, RoutedEventArgs e)
        {
            //searchDB.Text = "";
        }

        private void saveParametrsCompetition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ageCategoryCB.SelectedValue == null || pointsCB.SelectedValue == null)
                {
                    MessageBox.Show("Вы не указали очки и/или возрастную категорию.", "Уведомление");
                }
                else
                {
                    membersGrid.ItemsSource = competitionVM.AllMembers;
                    GenderComboBox.ItemsSource = competitionVM.genders;
                    HandComboBox.ItemsSource = competitionVM.hands;
                    QualificationComboBox.ItemsSource = competitionVM.DataBaseModel.GetAllQualifications();
                    RegionComboBoxz.ItemsSource = competitionVM.DataBaseModel.GetAllRegions();
                    TeamComboBox.ItemsSource = competitionVM.DataBaseModel.GetAllTeams();
                    membersGrid.CellEditEnding += cellEditEnding;

                    competitionVM.SetWeights(dataBaseModel.GetAllCategories("ж", ageCategoryCB.SelectedValue.ToString()),
                        dataBaseModel.GetAllCategories("м", ageCategoryCB.SelectedValue.ToString()));

                    competitionVM.SetPoints(dataBaseModel.GetAllPoints(pointsCB.SelectedValue.ToString()),
                        ageCategoryCB.SelectedValue.ToString());
                    competitionVM.SetWeightsLimits(weightWomen.Text, weightMen.Text);
                    competitionVM.isCompetitionParametersSet = true;
                    MessageBox.Show("Параметры сохранены успешно!", "Уведомление");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }

        }

        

        private void saveButton(object sender, RoutedEventArgs e)
        {       
            PrintDialog dlg = new PrintDialog();

            System.Windows.Window currentMainWindow = System.Windows.Application.Current.MainWindow;

            System.Windows.Application.Current.MainWindow = this;

                if ((bool)dlg.ShowDialog().GetValueOrDefault())
                {
                System.Windows.Application.Current.MainWindow = currentMainWindow; // do it early enough if the 'if' is entered
                    dlg.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
                }
        }

        private void ageCategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoriesGridGirls.ItemsSource = dataBaseModel.GetAllCategories("ж", ageCategoryCB.SelectedValue.ToString());
            categoriesGridBoys.ItemsSource = dataBaseModel.GetAllCategories("м", ageCategoryCB.SelectedValue.ToString());
            
        }

        private void pointsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pointsGrid.ItemsSource = dataBaseModel.GetAllPoints(pointsCB.SelectedValue.ToString());
        }

        private void sv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineUp();
            }
            else
            {
                scrollviewer.LineDown();
            }
            e.Handled = true;
        }

        private void getResultsProtocol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (resultVM == null)
                {
                    MessageBox.Show("Пожалуйста вернитесь во вкладку РЕЗУЛЬТАТЫ ДВОЕБОРЬЯ и нажмите кнопку ПОСЧИТАТЬ ОЧКИ", "Уведомление");
                }
                else
                {
                    resultVM.GetProtocolTeamResults();

                    resultProtocolB.DataContext = resultVM.ResultTeamCategoryBoys;
                    resultProtocolG.DataContext = resultVM.ResultTeamCategoryGirls;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }


        }

        private void saveAllMembers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serializator.SaveData(competitionVM.AllMembers);
                MessageBox.Show("Ваши данные успешно сохранены", "Уведомление");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }
        }

        private void loadAllMembers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!competitionVM.isCompetitionParametersSet)
                {
                    MessageBox.Show("Пожалуйста выберите параметры соревнования перед загрузкой данных", "Уведомление");
                }
                else
                {
                    competitionVM.AllMembers = serializator.OpenData();
                    membersGrid.ItemsSource = competitionVM.AllMembers;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }


        }

        private void getTotalProtocolTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (resultVM == null)
                {
                    MessageBox.Show("Пожалуйста вернитесь во вкладку СК+Область и нажмите кнопку ПОСЧИТАТЬ ОЧКИ", "Уведомление");

                }
                else
                {
                    resultVM.GetTotalResults();
                    totalProtocolTeam.DataContext = resultVM.ResultSummaryTeams;
                    totalProtocolRegion.DataContext = resultVM.ResultSummaryRegions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }


        }

        private void exportResultsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (resultVM == null)
                {
                    MessageBox.Show("Сначала необходимо посчитать данные. Нажмите на кнопку ПОСЧИТАТЬ ОЧКИ", "Уведомление");
                }
                else
                {
                    ExcelHandler excelHandler = new ExcelHandler(competitionVM);
                    excelHandler.SetApplicationParametersForTwoHandsRelults();
                    excelHandler.result = resultVM;
                    excelHandler.SaveAllTwoHandsRelultsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }


        }

        private void Save_Draw_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcelHandler excelHandler = new ExcelHandler(competitionVM);
                excelHandler.SetApplicationParametersForDraw();
                excelHandler.SaveAllDrawData();
               
            }

            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message + ex.StackTrace + "Пожалуйста, оповестите об этом разработчика, Спасибо!", "Ошибка");

            }

        }
    }
}
