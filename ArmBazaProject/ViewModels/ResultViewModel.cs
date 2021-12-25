using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmBazaProject.Entities;
using ArmBazaProject.Models;

namespace ArmBazaProject.ViewModels
{
    class ResultViewModel : NotifyableObject
    {
        Result result;
        CompetitionViewModel competition;
        CategoryViewModel[] resultCategoryGirls;
        CategoryViewModel[] resultCategoryBoys;

        CategoryViewModel[] resultTeamCategoryGirls;
        CategoryViewModel[] resultTeamCategoryBoys;

        CategoryViewModel[] resultRegionCategoryBoys;
        CategoryViewModel[] resultRegionCategoryGirls;

        private ObservableCollection<ProtocolTeam> summaryTeamsG;
        private ObservableCollection<ProtocolTeam> summaryTeamsB;

        private ObservableCollection<ProtocolTeam> resultSummaryTeams;


        private int[] protocolPoints = new int[] { 25, 17, 9, 5, 3, 2 };
        int equalizer = -25;


        #region свойства доступа

        public CategoryViewModel[] ResultRegionCategoryBoys
        {
            get { return resultRegionCategoryBoys; }
            set
            {
                if (resultRegionCategoryBoys != value)
                {
                    resultCategoryBoys = value;
                    OnPropertyChanged("ResultRegionCategoryBoys");
                }
            }
        }

        public CategoryViewModel[] ResultRegionCategoryGirls
        {
            get { return resultRegionCategoryGirls; }
            set
            {
                if (resultRegionCategoryGirls != value)
                {
                    resultRegionCategoryGirls = value;
                    OnPropertyChanged("ResultRegionCategoryGirls");
                }
            }
        }

        public ObservableCollection<ProtocolTeam> ResultSummaryTeams
        {
            get { return resultSummaryTeams; }
            set
            {
                if (resultSummaryTeams != value)
                {
                    resultSummaryTeams = value;
                    OnPropertyChanged("ResultSummaryTeams");
                }
            }
        }

        public ObservableCollection<ProtocolTeam> SummaryTeamsB
        {
            get { return summaryTeamsB; }
            set
            {
                if (summaryTeamsB != value)
                {
                    summaryTeamsB = value;
                    OnPropertyChanged("SummaryTeamsB");
                }
            }
        }
        public ObservableCollection<ProtocolTeam> SummaryTeamsG
        {
            get { return summaryTeamsG; }
            set
            {
                if (summaryTeamsG != value)
                {
                    summaryTeamsG = value;
                    OnPropertyChanged("SummaryTeamsG");
                }
            }
        }

        public CategoryViewModel[] ResultCategoryBoys
        {
            get { return resultCategoryBoys; }
            set
            {
                if (resultCategoryBoys != value)
                {
                    resultCategoryBoys = value;
                    OnPropertyChanged("ResultCategoryBoys");
                }
            }
        }

        public CategoryViewModel[] ResultTeamCategoryGirls
        {
            get { return resultTeamCategoryGirls; }
            set
            {
                if (resultTeamCategoryGirls != value)
                {
                    resultTeamCategoryGirls = value;
                    OnPropertyChanged("ResultTeamCategoryGirls");
                }
            }
        }

        public CategoryViewModel[] ResultTeamCategoryBoys
        {
            get { return resultTeamCategoryBoys; }
            set
            {
                if (resultTeamCategoryBoys != value)
                {
                    resultTeamCategoryBoys = value;
                    OnPropertyChanged("ResultTeamCategoryBoys");
                }
            }
        }

        public CategoryViewModel[] ResultCategoryGirls
        {
            get { return resultCategoryGirls; }
            set
            {
                if (resultCategoryGirls != value)
                {
                    resultCategoryGirls = value;
                    OnPropertyChanged("ResultCategoryGirls");
                }
            }
        }

        public Result Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged("Result");
                }
            }
        }

        #endregion

        public ResultViewModel(CompetitionViewModel competition)
        {
            result = new Result();
            this.competition = competition;
            resultCategoryGirls = new CategoryViewModel[competition.CompetitionLeftHand.GirlsWeights.Length];
            resultCategoryBoys = new CategoryViewModel[competition.CompetitionLeftHand.BoysWeights.Length];
            resultTeamCategoryGirls = new CategoryViewModel[competition.CompetitionLeftHand.GirlsWeights.Length];
            resultTeamCategoryBoys = new CategoryViewModel[competition.CompetitionLeftHand.BoysWeights.Length];
            resultRegionCategoryBoys = new CategoryViewModel[competition.CompetitionLeftHand.BoysWeights.Length];
            resultRegionCategoryGirls = new CategoryViewModel[competition.CompetitionLeftHand.GirlsWeights.Length];
            summaryTeamsG = new ObservableCollection<ProtocolTeam>();
            summaryTeamsB = new ObservableCollection<ProtocolTeam>();
            resultSummaryTeams = new ObservableCollection<ProtocolTeam>();
        }

        public void GetResultsTwoHand()
        {
            //установили для всех рук очки
            SetPoints(competition.CompetitionLeftHand.CategoriesB);
            SetPoints(competition.CompetitionLeftHand.CategoriesG);
            SetPoints(competition.CompetitionRightHand.CategoriesB);
            SetPoints(competition.CompetitionRightHand.CategoriesG);

            //сбор информации вместе
            SetAllPointsForMembers(competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB, resultCategoryBoys);
            SetAllPointsForMembers(competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG, resultCategoryGirls);

            // установка мест за 2 руки
            SetResultHandsScore(resultCategoryBoys);
            SetResultHandsScore(resultCategoryGirls);

            //назначение места для каждого участника по сумме 2 - х рук
            GetTotalResultByHand(resultCategoryBoys);
            GetTotalResultByHand(resultCategoryGirls);

            //сбор команд
            GetCategotyTeams(resultCategoryGirls, competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG);
            GetCategotyTeams(resultCategoryBoys, competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB);

        }


        #region Two Hands

        //назначение очков для участников в зависимости от занятого места
        private void SetPoints(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].PlaceMembers.Count; j++)
                {
                    for (int k = 0; k < competition.CompetitionLeftHand.Points.Length; k++)
                    {
                        if (categories[i].PlaceMembers[j].Place == k + 1)
                        {
                            categories[i].PlaceMembers[j].Score = competition.CompetitionLeftHand.Points[k];
                            break;
                        }
                        if (competition.CompetitionLeftHand.Points[0] == 36)
                        {
                            categories[i].PlaceMembers[j].Score = 1;
                        }
                        else
                        {
                            categories[i].PlaceMembers[j].Score = 0;
                        }
                    }

                }
            }
        }

        //назначение очков для левой и правой руки для каждого участника
        private void SetAllPointsForMembers(CategoryViewModel[] categoriesLeft, CategoryViewModel[] categoriesRight,
            CategoryViewModel[] resultCategory)
        {
            MemberViewModel member;

            //проверка по левой руке
            for (int i = 0; i < categoriesLeft.Length; i++)
            {
                resultCategory[i] = new CategoryViewModel(categoriesLeft[i].WeightCategory.CategoryWeight,
                    categoriesLeft[i].WeightCategory.WeightName);
                for (int j = 0; j < categoriesLeft[i].PlaceMembers.Count; j++)
                {
                    member = (MemberViewModel)categoriesLeft[i].PlaceMembers[j].Clone();
                    if (member.isLeftHand && member.isRightHand)
                    {
                        member.LeftHandScore = categoriesLeft[i].PlaceMembers[j].Score;
                        member.LeftHandPlace = categoriesLeft[i].PlaceMembers[j].Place;
                        member.LeftHandScoreVM = member.LeftHandScore.ToString();
                        member.LeftHandPlaceVM = member.LeftHandPlace.ToString();
                        for (int k = 0; k < categoriesRight[i].PlaceMembers.Count; k++)
                        {
                            if (CheckMember(categoriesLeft[i].PlaceMembers[j], categoriesRight[i].PlaceMembers[k]))
                            {
                                member.RightHandScore = categoriesRight[i].PlaceMembers[k].Score;
                                member.RightHandPlace = categoriesRight[i].PlaceMembers[k].Place;
                                member.RightHandScoreVM = member.RightHandScore.ToString();
                                member.RightHandPlaceVM = member.RightHandPlace.ToString();
                            }
                        }

                        if (member.RightHandScore == 0 && member.LeftHandScore == 0)
                        {
                            member.scoreZero = true;
                        }


                    }
                    else if (member.isLeftHand && !member.isRightHand)
                    {
                        member.LeftHandScore = categoriesLeft[i].PlaceMembers[j].Score;
                        member.LeftHandPlace = categoriesLeft[i].PlaceMembers[j].Place;
                        member.LeftHandScoreVM = member.LeftHandScore.ToString();
                        member.LeftHandPlaceVM = member.LeftHandPlace.ToString();
                        member.RightHandScore = 0 + equalizer;
                        member.RightHandPlace = 0;
                        member.RightHandScoreVM = "";
                        member.RightHandPlaceVM = "";
                    }
                    resultCategory[i].ResultMembers.Add(member);

                }
            }

            for (int i = 0; i < categoriesRight.Length; i++)
            {
                for (int j = 0; j < categoriesRight[i].PlaceMembers.Count; j++)
                {
                    if (categoriesRight[i].PlaceMembers[j].isRightHand && !categoriesRight[i].PlaceMembers[j].isLeftHand)
                    {
                        member = (MemberViewModel)categoriesRight[i].PlaceMembers[j].Clone();
                        member.RightHandScore = categoriesRight[i].PlaceMembers[j].Score;
                        member.RightHandPlace = categoriesRight[i].PlaceMembers[j].Place;
                        member.RightHandScoreVM = member.RightHandScore.ToString();
                        member.RightHandPlaceVM = member.RightHandPlace.ToString();
                        member.LeftHandScore = 0 + equalizer;
                        member.LeftHandPlace = 0;
                        member.LeftHandScoreVM = "";
                        member.LeftHandPlaceVM = "";
                        resultCategory[i].ResultMembers.Add(member);
                    }
                }
            }

        }

        //проверка участника на существование в листе
        private bool CheckMember(MemberViewModel member1, MemberViewModel member2)
        {
            if (member1.Member.FullName == member2.Member.FullName
                && member1.TeamName == member2.TeamName &&
                member1.Member.Weight == member2.Member.Weight
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // назначение финальных очков по 2 рукам для каждого участника
        private void SetResultHandsScore(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    categories[i].ResultMembers[j].ResultHandScore =
                    categories[i].ResultMembers[j].LeftHandPlace + categories[i].ResultMembers[j].RightHandPlace;

                }
            }
        }


        //назначение места для каждого участника по сумме 2-х рук
        private void GetTotalResultByHand(CategoryViewModel[] categories)
        {
            int place = 0;
            SortMembers(categories);
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    place = categories[i].ResultMembers.Count - j;
                    categories[i].ResultMembers[j].ResultHandPlace = place;
                }
            }
            for (int i = 0; i < categories.Length; i++)
            {
                SortByWeight(categories[i]);
            }

        }

        private void GetCategotyTeams(CategoryViewModel[] categories, CategoryViewModel[] categoriesL, CategoryViewModel[] categoriesR)
        {
            TeamModel team;
            for (int i = 0; i < categoriesL.Length; i++)
            {
                for (int j = 0; j < categoriesL[i].Teams.Count; j++)
                {
                    team = new TeamModel(categoriesL[i].Teams[j].Name);
                    if (team.Name != "Лично")
                    {
                        categories[i].Teams.Add(team);
                    }


                }
            }

            for (int i = 0; i < categoriesR.Length; i++)
            {
                for (int j = 0; j < categoriesR[i].Teams.Count; j++)
                {
                    for (int k = 0; k < categories.Length; k++)
                    {
                        if (k == i)
                        {
                            for (int m = 0; m < categories[k].Teams.Count; m++)
                            {
                                if (!categories[k].CheckTeam(categoriesR[i].Teams[j]))
                                {
                                    team = new TeamModel(categoriesR[i].Teams[j].Name);
                                    if (team.Name != "Лично")
                                    {
                                        categories[k].Teams.Add(team);
                                    }
                                }
                            }
                        }

                    }
                }
            }

        }


        //сортировка по весу
        private void SortByWeight(CategoryViewModel category)
        {
            int place = 0;
            for (int i = 0; i < category.ResultMembers.Count; i++)
            {
                for (int j = 0; j < category.ResultMembers.Count; j++)
                {
                    if (i != j)
                    {
                        if (((category.ResultMembers[i].LeftHandPlace == category.ResultMembers[j].RightHandPlace) &&
                            (category.ResultMembers[i].RightHandPlace == category.ResultMembers[j].LeftHandPlace)) ||
                            ((category.ResultMembers[i].Member.Weight < category.ResultMembers[j].Member.Weight) &&
                               (category.ResultMembers[i].ResultHandPlace > category.ResultMembers[j].ResultHandPlace)))
                        {
                            if ((category.ResultMembers[i].Member.Weight > category.ResultMembers[j].Member.Weight) &&
                                (category.ResultMembers[i].ResultHandPlace < category.ResultMembers[j].ResultHandPlace))
                            {
                                place = category.ResultMembers[i].ResultHandPlace;
                                category.ResultMembers[i].ResultHandPlace = category.ResultMembers[j].ResultHandPlace;
                                category.ResultMembers[j].ResultHandPlace = place;
                            }

                        }
                    }
                }
            }
        }

        //сортировка участников по месту
        private void SortMembers(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    categories[i].ResultMembers[j].TempResultScoreHands =
                        categories[i].ResultMembers[j].LeftHandScore + categories[i].ResultMembers[j].RightHandScore;
                    if (categories[i].ResultMembers[j].isLeftHand && categories[i].ResultMembers[j].isRightHand && categories[i].ResultMembers[j].scoreZero)
                    {
                        categories[i].ResultMembers[j].TempResultScoreHands += 1;
                    }

                }
            }

            MemberViewModel temp;
            for (int t = 0; t < categories.Length; t++)
            {
                for (int i = 0; i < categories[t].ResultMembers.Count - 1; i++)
                {
                    for (int j = i + 1; j < categories[t].ResultMembers.Count; j++)
                    {
                        if (categories[t].ResultMembers[i].TempResultScoreHands > categories[t].ResultMembers[j].TempResultScoreHands)
                        {
                            temp = categories[t].ResultMembers[i];
                            categories[t].ResultMembers[i] = categories[t].ResultMembers[j];
                            categories[t].ResultMembers[j] = temp;
                        }
                    }
                }
            }
        }

        #endregion

        public void GetProtocolTeamResults()
        {
            PrepareData();
            SetViewModelScores(ResultTeamCategoryGirls);
            SetViewModelScores(ResultTeamCategoryBoys);

        }

        #region Team Protocol

        private void PrepareData()
        {
            //установка мест + игнорирование личников
            UpdatePlaces(competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB);
            UpdatePlaces(competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG);

            //установка очков по местам
            SetPoints(competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB);
            SetPoints(competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG);

            //сбор нформации в 1 категорию для вывода на фронт
            GeneralCategory(competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB, ResultTeamCategoryBoys);
            GeneralCategory(competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG, ResultTeamCategoryGirls);


        }

        private void UpdatePlaces(CategoryViewModel[] categoriesLeft, CategoryViewModel[] categoriesRight)
        {
            MemberViewModel temp;
            for (int t = 0; t < categoriesLeft.Length; t++)
            {
                for (int l = 0; l < categoriesLeft[t].PlaceMembers.Count; l++)
                {
                    if (categoriesLeft[t].PlaceMembers[l].TeamName == "Лично" || !categoriesLeft[t].PlaceMembers[l].IsSportTeam)
                    {
                        categoriesLeft[t].PlaceMembers[l].LeftHandPlaceProtocol = 100;
                    }
                    else
                    {
                        categoriesLeft[t].PlaceMembers[l].LeftHandPlaceProtocol = categoriesLeft[t].PlaceMembers[l].Place;
                    }
                    categoriesLeft[t].PlaceMembers[l].LeftHandPlace = categoriesLeft[t].PlaceMembers[l].Place;
                }
            }
            //сортировка по возрастанию!!!

            for (int t = 0; t < categoriesLeft.Length; t++)
            {
                for (int i = 0; i < categoriesLeft[t].PlaceMembers.Count - 1; i++)
                {
                    for (int j = i + 1; j < categoriesLeft[t].PlaceMembers.Count; j++)
                    {
                        if (categoriesLeft[t].PlaceMembers[i].LeftHandPlaceProtocol > categoriesLeft[t].PlaceMembers[j].LeftHandPlaceProtocol)
                        {
                            temp = categoriesLeft[t].PlaceMembers[i];
                            categoriesLeft[t].PlaceMembers[i] = categoriesLeft[t].PlaceMembers[j];
                            categoriesLeft[t].PlaceMembers[j] = temp;
                        }
                    }
                }

            }
            int place = 0;
            for (int t = 0; t < categoriesLeft.Length; t++)
            {
                for (int i = 0; i < categoriesLeft[t].PlaceMembers.Count; i++)
                {
                    place = i;
                    place += 1;
                    categoriesLeft[t].PlaceMembers[i].LeftHandPlaceProtocol = place;
                }
            }


            for (int t = 0; t < categoriesRight.Length; t++)
            {
                for (int l = 0; l < categoriesRight[t].PlaceMembers.Count; l++)
                {
                    if (categoriesRight[t].PlaceMembers[l].TeamName == "Лично" || !categoriesRight[t].PlaceMembers[l].IsSportTeam)
                    {
                        categoriesRight[t].PlaceMembers[l].RightHandPlaceProtocol = 100;
                    }
                    else
                    {
                        categoriesRight[t].PlaceMembers[l].RightHandPlaceProtocol = categoriesRight[t].PlaceMembers[l].Place;
                    }
                    categoriesRight[t].PlaceMembers[l].RightHandPlace = categoriesRight[t].PlaceMembers[l].Place;
                }
            }
            //сортировка по возрастанию!!!

            for (int t = 0; t < categoriesRight.Length; t++)
            {
                for (int i = 0; i < categoriesRight[t].PlaceMembers.Count - 1; i++)
                {
                    for (int j = i + 1; j < categoriesRight[t].PlaceMembers.Count; j++)
                    {
                        if (categoriesRight[t].PlaceMembers[i].RightHandPlaceProtocol > categoriesRight[t].PlaceMembers[j].RightHandPlaceProtocol)
                        {
                            temp = categoriesRight[t].PlaceMembers[i];
                            categoriesRight[t].PlaceMembers[i] = categoriesRight[t].PlaceMembers[j];
                            categoriesRight[t].PlaceMembers[j] = temp;
                        }
                    }
                }

            }
            for (int t = 0; t < categoriesRight.Length; t++)
            {
                for (int i = 0; i < categoriesRight[t].PlaceMembers.Count; i++)
                {
                    place = i;
                    place += 1;
                    categoriesRight[t].PlaceMembers[i].RightHandPlaceProtocol = place;
                }
            }

        }

        private void SetPoints(CategoryViewModel[] categoriesLeft, CategoryViewModel[] categoriesRight)
        {
            int place = 0;
            for (int t = 0; t < categoriesLeft.Length; t++)
            {
                for (int l = 0; l < categoriesLeft[t].PlaceMembers.Count; l++)
                {
                    for (int z = 0; z < protocolPoints.Length; z++)
                    {
                        place = z;
                        place += 1;
                        if (categoriesLeft[t].PlaceMembers[l].LeftHandPlaceProtocol == place)
                        {
                            categoriesLeft[t].PlaceMembers[l].LeftHandScore = protocolPoints[z];
                        }
                    }

                }
            }

            for (int t = 0; t < categoriesRight.Length; t++)
            {
                for (int l = 0; l < categoriesRight[t].PlaceMembers.Count; l++)
                {
                    for (int z = 0; z < protocolPoints.Length; z++)
                    {
                        place = z;
                        place += 1;
                        if (categoriesRight[t].PlaceMembers[l].RightHandPlaceProtocol == place)
                        {
                            categoriesRight[t].PlaceMembers[l].RightHandScore = protocolPoints[z];
                        }
                    }

                }
            }
        }

        private void GeneralCategory(CategoryViewModel[] categoriesLeft, CategoryViewModel[] categoriesRight, CategoryViewModel[] resultCategory)
        {
            MemberViewModel member;

            //проверка по левой руке
            for (int i = 0; i < categoriesLeft.Length; i++)
            {
                resultCategory[i] = new CategoryViewModel(categoriesLeft[i].WeightCategory.CategoryWeight,
                    categoriesLeft[i].WeightCategory.WeightName);
                for (int j = 0; j < categoriesLeft[i].PlaceMembers.Count; j++)
                {
                    member = (MemberViewModel)categoriesLeft[i].PlaceMembers[j].Clone();
                    if (member.isLeftHand && member.isRightHand)
                    {
                        member.LeftHandScoreVM = member.LeftHandScore.ToString();
                        member.LeftHandPlaceVM = member.LeftHandPlace.ToString();
                        for (int k = 0; k < categoriesRight[i].PlaceMembers.Count; k++)
                        {
                            if (CheckMember(categoriesLeft[i].PlaceMembers[j], categoriesRight[i].PlaceMembers[k]))
                            {
                                member.RightHandScoreVM = categoriesRight[i].PlaceMembers[k].RightHandScore.ToString();
                                member.RightHandPlaceVM = categoriesRight[i].PlaceMembers[k].RightHandPlace.ToString();
                                member.RightHandPlaceProtocol = categoriesRight[i].PlaceMembers[k].RightHandPlaceProtocol;
                            }
                        }

                    }
                    else if (member.isLeftHand && !member.isRightHand)
                    {
                        member.LeftHandScoreVM = member.LeftHandScore.ToString();
                        member.LeftHandPlaceVM = member.LeftHandPlace.ToString();
                        member.RightHandScoreVM = "";
                        member.RightHandPlaceVM = "";
                    }
                    resultCategory[i].ResultMembers.Add(member);

                }
            }

            for (int i = 0; i < categoriesRight.Length; i++)
            {
                for (int j = 0; j < categoriesRight[i].PlaceMembers.Count; j++)
                {
                    if (categoriesRight[i].PlaceMembers[j].isRightHand && !categoriesRight[i].PlaceMembers[j].isLeftHand)
                    {
                        member = (MemberViewModel)categoriesRight[i].PlaceMembers[j].Clone();
                        member.RightHandScoreVM = member.RightHandScore.ToString();
                        member.RightHandPlaceVM = member.RightHandPlace.ToString();
                        member.LeftHandScoreVM = "";
                        member.LeftHandPlaceVM = "";
                        resultCategory[i].ResultMembers.Add(member);
                    }
                }
            }
        }


        private void SetViewModelScores(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    if (!categories[i].ResultMembers[j].IsSportTeam)
                    {
                        categories[i].ResultMembers[j].LeftHandScoreVM = "Л";
                    }
                    else if (!categories[i].ResultMembers[j].isLeftHand)
                    {
                        categories[i].ResultMembers[j].LeftHandScoreVM = "-";
                    }
                    if (!categories[i].ResultMembers[j].IsSportTeam)
                    {
                        categories[i].ResultMembers[j].RightHandScoreVM = "Л";
                    }
                    else if (!categories[i].ResultMembers[j].isRightHand)
                    {
                        categories[i].ResultMembers[j].RightHandScoreVM = "-";
                    }

                }
            }
        }


        #endregion


        /// <summary>
        /// ДОДЕЛАТЬ РЕЗУЛЬТАТЫ КОМАНДЫ ПО ВСЕМ КАТЕГОРИЯМ!!!!!!!!!
        /// </summary>
        public void GetTotalTeamResults()
        {
            /*GetCategotyTeams(ResultTeamCategoryGirls, competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG);
            GetCategotyTeams(ResultTeamCategoryBoys, competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB);


            GetSummaryProtocolPoints(ResultTeamCategoryGirls, summaryTeamsG);
            GetSummaryProtocolPoints(ResultTeamCategoryGirls, summaryTeamsB);

            //SetProtocolVMData(summaryTeamsG);
            //SetProtocolVMData(summaryTeamsB);

            resultSummaryTeams =  CollectDataTeams(summaryTeamsB, summaryTeamsG);


            SetPlaceSummaryTeams(resultSummaryTeams);*/
        }

        #region ResultTeamProtocol
        //получение мест для команд
        private void GetResultTeam(CategoryViewModel[] categories)
        {
            MemberViewModel member;
            int score = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    for (int k = 0; k < categories[i].Teams.Count; k++)
                    {
                        if (categories[i].ResultMembers[j].TeamName == categories[i].Teams[k].Name)
                        {
                            if (categories[i].ResultMembers[j].IsSportTeam)
                            {
                                if (categories[i].ResultMembers[j].isLeftHand && categories[i].ResultMembers[j].isRightHand)
                                {
                                    score = categories[i].ResultMembers[j].LeftHandScore + categories[i].ResultMembers[j].RightHandScore;
                                }
                                else if (categories[i].ResultMembers[j].isLeftHand && !categories[i].ResultMembers[j].isRightHand)
                                {
                                    score = categories[i].ResultMembers[j].LeftHandScore;
                                }
                                else if (!categories[i].ResultMembers[j].isLeftHand && categories[i].ResultMembers[j].isRightHand)
                                {
                                    score = categories[i].ResultMembers[j].RightHandScore;
                                }
                            }

                            member = (MemberViewModel)categories[i].ResultMembers[j].Clone();
                            categories[i].Teams[k].Score += score;
                            categories[i].Teams[k].Members.Add(member);
                        }
                    }
                }
            }

            SetPlaceTeam(categories);
        }

        //сортировка команд по очкам участников
        private void SortTeams(CategoryViewModel[] categories)
        {
            TeamModel temp;
            for (int t = 0; t < categories.Length; t++)
            {
                for (int i = 0; i < categories[t].Teams.Count - 1; i++)
                {
                    for (int j = i + 1; j < categories[t].Teams.Count; j++)
                    {
                        if (categories[t].Teams[i].Score > categories[t].Teams[j].Score)
                        {
                            temp = categories[t].Teams[i];
                            categories[t].Teams[i] = categories[t].Teams[j];
                            categories[t].Teams[j] = temp;
                        }
                    }
                }
            }
        }

        //назначение места для команды
        private void SetPlaceTeam(CategoryViewModel[] categories)
        {
            int place = 0;
            SortTeams(categories);
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].Teams.Count; j++)
                {
                    place = categories[i].Teams.Count - j;
                    categories[i].Teams[j].Place = place;
                }
            }
        }

        private void GetMembersToTeam(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    for (int k = 0; k < categories[i].Teams.Count; k++)
                    {
                        if (categories[i].ResultMembers[j].TeamName == categories[i].Teams[k].Name)
                        {
                            categories[i].Teams[k].Members.Add((MemberViewModel)categories[i].ResultMembers[j].Clone());
                        }
                    }

                }
            }

        }

        private void GetSummaryProtocolPoints(CategoryViewModel[] categories, ObservableCollection<ProtocolTeam> summaryTeams)
        {
            int score = 0;
            bool isSportTeam = false;
            bool isContainsTeam = false;
            ProtocolTeam protocolTeam;
            for (int k = 0; k < categories.Length; k++)
            {
                for (int l = 0; l < categories[k].Teams.Count; l++)
                {
                    protocolTeam = new ProtocolTeam(categories[k].Teams[l].Name);
                    if (summaryTeams.Count > 0)
                    {
                        for (int i = 0; i < summaryTeams.Count; i++)
                        {
                            if (summaryTeams[i].Name == protocolTeam.Name)
                            {
                                isContainsTeam = true;
                            }
                        }
                        if (!isContainsTeam)
                        {
                            summaryTeams.Add(protocolTeam);
                        }
                    }
                    else
                    {
                        summaryTeams.Add(protocolTeam);
                    }

                }

            }

            GetMembersToTeam(categories);

            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].Teams.Count; j++)
                {
                    for (int k = 0; k < summaryTeams.Count; k++)
                    {
                        if (categories[i].Teams[j].Name == summaryTeams[k].Name)
                        {
                            for (int t = 0; t < categories[i].Teams[j].Members.Count; t++)
                            {
                                if (categories[i].Teams[j].Members[t].IsSportTeam)
                                {
                                    if (categories[i].Teams[j].Members[t].isLeftHand && categories[i].Teams[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Teams[j].Members[t].LeftHandSTScore + categories[i].Teams[j].Members[t].RightHandSTScore;
                                    }
                                    else if (categories[i].Teams[j].Members[t].isLeftHand && !categories[i].Teams[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Teams[j].Members[t].LeftHandSTScore;
                                    }
                                    else if (!categories[i].Teams[j].Members[t].isLeftHand && categories[i].Teams[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Teams[j].Members[t].RightHandSTScore;
                                    }
                                    isSportTeam = true;

                                }

                                if (categories[i].Teams[j].Members[t].Member.Gender == "ж" && isSportTeam)
                                {
                                    summaryTeams[k].ScoreG += score;
                                }
                                else if (categories[i].Teams[j].Members[t].Member.Gender == "м" && isSportTeam)
                                {
                                    summaryTeams[k].ScoreB += score;
                                }
                                isSportTeam = false;
                            }
                        }
                    }
                }
            }

        }

        private void SetProtocolVMData(ObservableCollection<ProtocolTeam> summaryTeams)
        {
            string result;
            for (int k = 0; k < summaryTeams.Count; k++)
            {
                for (int i = 0; i < summaryTeams[k].PointsLeftHand.Count; i++)
                {
                    if (summaryTeams[k].PointsLeftHand[i].points.Count >= 2)
                    {
                        result = summaryTeams[k].PointsLeftHand[i].points[0].ToString() + "," +
                       summaryTeams[k].PointsLeftHand[i].points[1].ToString();
                    }
                    else if (summaryTeams[k].PointsLeftHand[i].points.Count == 1)
                    {
                        result = summaryTeams[k].PointsLeftHand[i].points[0].ToString();
                    }
                    else
                    {
                        result = "";
                    }
                    //result = summaryTeams[k].PointsLeftHand[i].points[0].ToString() + "," +
                    //    summaryTeams[k].PointsLeftHand[i].points[1].ToString();

                    summaryTeams[k].PointsLeftHandVM.Add(result);
                }

                for (int i = 0; i < summaryTeams[k].PointsRightHand.Count; i++)
                {
                    if (summaryTeams[k].PointsRightHand[i].points.Count >= 2)
                    {
                        result = summaryTeams[k].PointsRightHand[i].points[0].ToString() + "," +
                        summaryTeams[k].PointsRightHand[i].points[1].ToString();
                    }
                    else if (summaryTeams[k].PointsRightHand[i].points.Count == 1)
                    {
                        result = summaryTeams[k].PointsRightHand[i].points[0].ToString();
                    }
                    else
                    {
                        result = "";
                    }


                    summaryTeams[k].PointsRightHandVM.Add(result);
                }
            }
        }

        private void SortSummaryTeams(ObservableCollection<ProtocolTeam> summaryTeams)
        {
            ProtocolTeam temp;
            for (int i = 0; i < summaryTeams.Count - 1; i++)
            {
                for (int j = i + 1; j < summaryTeams.Count; j++)
                {
                    if (summaryTeams[i].TotalScore > summaryTeams[j].TotalScore)
                    {
                        temp = summaryTeams[i];
                        summaryTeams[i] = summaryTeams[j];
                        summaryTeams[j] = temp;
                    }
                }
            }

        }
        private void SetPlaceSummaryTeams(ObservableCollection<ProtocolTeam> summaryTeams)
        {
            int place = 0;
            SortSummaryTeams(summaryTeams);
            for (int i = 0; i < summaryTeams.Count; i++)
            {
                place = summaryTeams.Count - i;
                summaryTeams[i].TotalPlace = place;
            }
        }

        private ObservableCollection<ProtocolTeam> CollectDataTeams(ObservableCollection<ProtocolTeam> summaryTeams1, ObservableCollection<ProtocolTeam> summaryTeams2)
        {
            ObservableCollection<ProtocolTeam> summaryTeams;
            for (int i = 0; i < summaryTeams1.Count; i++)
            {
                summaryTeams2[i].ScoreB = summaryTeams1[i].ScoreB;
            }

            for (int k = 0; k < summaryTeams2.Count; k++)
            {
                summaryTeams2[k].TotalScore = summaryTeams2[k].ScoreB + summaryTeams2[k].ScoreG;
            }
            summaryTeams = summaryTeams2;

            return summaryTeams;
        }

        #endregion



        public void GetProtocolRegionResults()
        {

        }



    }
}



