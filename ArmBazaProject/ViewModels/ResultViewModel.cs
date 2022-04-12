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
    public class ResultViewModel : NotifyableObject
    {
        Result result;
        CompetitionViewModel competition;
        CategoryViewModel[] resultCategoryGirls;
        CategoryViewModel[] resultCategoryBoys;

        CategoryViewModel[] resultTeamCategoryGirls;
        CategoryViewModel[] resultTeamCategoryBoys;

        CategoryViewModel[] resultRegionCategoryBoys;
        CategoryViewModel[] resultRegionCategoryGirls;

        private ObservableCollection<ProtocolModel> summaryTeamsG;
        private ObservableCollection<ProtocolModel> summaryTeamsB;

        private ObservableCollection<ProtocolModel> resultSummaryTeams;

        private ObservableCollection<ProtocolModel> summaryRegionsG;
        private ObservableCollection<ProtocolModel> summaryRegionsB;

        private ObservableCollection<ProtocolModel> resultSummaryRegions;


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

        public ObservableCollection<ProtocolModel> ResultSummaryRegions
        {
            get { return resultSummaryRegions; }
            set
            {
                if (resultSummaryRegions != value)
                {
                    resultSummaryRegions = value;
                    OnPropertyChanged("ResultSummaryRegions");
                }
            }
        }

        public ObservableCollection<ProtocolModel> ResultSummaryTeams
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

        public ObservableCollection<ProtocolModel> SummaryTeamsB
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
        public ObservableCollection<ProtocolModel> SummaryTeamsG
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
            summaryTeamsG = new ObservableCollection<ProtocolModel>();
            summaryTeamsB = new ObservableCollection<ProtocolModel>();
            resultSummaryTeams = new ObservableCollection<ProtocolModel>();

            summaryRegionsG = new ObservableCollection<ProtocolModel>();
            summaryRegionsB = new ObservableCollection<ProtocolModel>();
            resultSummaryRegions = new ObservableCollection<ProtocolModel>();
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
           // GetCategotyTeams(resultCategoryGirls, competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG);
            //GetCategotyTeams(resultCategoryBoys, competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB);

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

        #region Team + Region Protocol

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
                    if (!categoriesLeft[t].PlaceMembers[l].IsRegion)
                    {
                        categoriesLeft[t].PlaceMembers[l].LeftHandPlaceRegionProtocol = 100;
                    }
                    else
                    {
                        categoriesLeft[t].PlaceMembers[l].LeftHandPlaceRegionProtocol = categoriesLeft[t].PlaceMembers[l].Place;
                    }
                    categoriesLeft[t].PlaceMembers[l].LeftHandPlace = categoriesLeft[t].PlaceMembers[l].Place;
                }
            }

            //сортировка по возрастанию!!! для команды

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


            //сортировка для региона

            for (int t = 0; t < categoriesLeft.Length; t++)
            {
                for (int i = 0; i < categoriesLeft[t].PlaceMembers.Count - 1; i++)
                {
                    for (int j = i + 1; j < categoriesLeft[t].PlaceMembers.Count; j++)
                    {
                        if (categoriesLeft[t].PlaceMembers[i].LeftHandPlaceRegionProtocol > categoriesLeft[t].PlaceMembers[j].LeftHandPlaceRegionProtocol)
                        {
                            temp = categoriesLeft[t].PlaceMembers[i];
                            categoriesLeft[t].PlaceMembers[i] = categoriesLeft[t].PlaceMembers[j];
                            categoriesLeft[t].PlaceMembers[j] = temp;
                        }
                    }
                }

            }
            int placeRegion = 0;
            for (int t = 0; t < categoriesLeft.Length; t++)
            {
                for (int i = 0; i < categoriesLeft[t].PlaceMembers.Count; i++)
                {
                    placeRegion = i;
                    placeRegion += 1;
                    categoriesLeft[t].PlaceMembers[i].LeftHandPlaceRegionProtocol = placeRegion;
                }
            }


            //правая рука

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
                    if (!categoriesRight[t].PlaceMembers[l].IsRegion)
                    {
                        categoriesRight[t].PlaceMembers[l].RightHandPlaceRegionProtocol = 100;
                    }
                    else
                    {
                        categoriesRight[t].PlaceMembers[l].RightHandPlaceRegionProtocol = categoriesRight[t].PlaceMembers[l].Place;
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


            //сортировка для региона

            for (int t = 0; t < categoriesRight.Length; t++)
            {
                for (int i = 0; i < categoriesRight[t].PlaceMembers.Count - 1; i++)
                {
                    for (int j = i + 1; j < categoriesRight[t].PlaceMembers.Count; j++)
                    {
                        if (categoriesRight[t].PlaceMembers[i].RightHandPlaceRegionProtocol > categoriesRight[t].PlaceMembers[j].RightHandPlaceRegionProtocol)
                        {
                            temp = categoriesRight[t].PlaceMembers[i];
                            categoriesRight[t].PlaceMembers[i] = categoriesRight[t].PlaceMembers[j];
                            categoriesRight[t].PlaceMembers[j] = temp;
                        }
                    }
                }

            }
            
            placeRegion = 0;
            for (int t = 0; t < categoriesRight.Length; t++)
            {
                for (int i = 0; i < categoriesRight[t].PlaceMembers.Count; i++)
                {
                    placeRegion = i;
                    placeRegion += 1;
                    categoriesRight[t].PlaceMembers[i].RightHandPlaceRegionProtocol = placeRegion;
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
                        if(categoriesLeft[t].PlaceMembers[l].LeftHandPlaceRegionProtocol == place)
                        {
                            categoriesLeft[t].PlaceMembers[l].LeftHandScoreRegion = protocolPoints[z];
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
                        if (categoriesRight[t].PlaceMembers[l].RightHandPlaceRegionProtocol == place)
                        {
                            categoriesRight[t].PlaceMembers[l].RightHandScoreRegion = protocolPoints[z];
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
                        member.LeftHandScoreRegionVM = member.LeftHandScoreRegion.ToString();
                        for (int k = 0; k < categoriesRight[i].PlaceMembers.Count; k++)
                        {
                            if (CheckMember(categoriesLeft[i].PlaceMembers[j], categoriesRight[i].PlaceMembers[k]))
                            {
                                member.RightHandScore = categoriesRight[i].PlaceMembers[k].RightHandScore;
                                member.RightHandScoreVM = categoriesRight[i].PlaceMembers[k].RightHandScore.ToString();
                                member.RightHandPlaceVM = categoriesRight[i].PlaceMembers[k].RightHandPlace.ToString();
                                member.RightHandScoreRegionVM = categoriesRight[i].PlaceMembers[k].RightHandScoreRegion.ToString();
                            }
                        }

                    }
                    else if (member.isLeftHand && !member.isRightHand)
                    {
                        member.LeftHandScoreVM = member.LeftHandScore.ToString();
                        member.LeftHandPlaceVM = member.LeftHandPlace.ToString();
                        member.LeftHandScoreRegionVM = member.LeftHandScoreRegion.ToString();
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
                        member.RightHandScoreRegionVM= member.RightHandScoreRegion.ToString();
                        member.LeftHandScoreRegionVM = "";
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
                    if (categories[i].ResultMembers[j].isLeftHand)
                    {
                        if (!categories[i].ResultMembers[j].IsSportTeam)
                        {
                            categories[i].ResultMembers[j].LeftHandScoreVM = "Л";
                        }
                        if (!categories[i].ResultMembers[j].IsRegion)
                        {
                            categories[i].ResultMembers[j].LeftHandScoreRegionVM = "Л";
                        }
                            
                    }
                    else
                    {
                        categories[i].ResultMembers[j].LeftHandScoreVM = "-";
                    }
                    if (categories[i].ResultMembers[j].isRightHand)
                    {
                        if (!categories[i].ResultMembers[j].IsSportTeam)
                        {
                            categories[i].ResultMembers[j].RightHandScoreVM = "Л";
                        }
                        if (!categories[i].ResultMembers[j].IsRegion)
                        {
                            categories[i].ResultMembers[j].RightHandScoreRegionVM = "Л";
                        }

                    }
                    else
                    {
                        categories[i].ResultMembers[j].RightHandScoreVM = "-";
                    }

                }
            }
        }


        #endregion


        public void GetTotalResults()
        {
            GetCategotyModels(ResultTeamCategoryBoys, competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB);
            GetCategotyModels(ResultTeamCategoryGirls, competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG);
            

            GetSummaryProtocolPoints(ResultTeamCategoryGirls, summaryTeamsG, summaryRegionsG);
            GetSummaryProtocolPoints(ResultTeamCategoryBoys, summaryTeamsB, summaryRegionsB);


            CollectDataModel(summaryTeamsB, summaryTeamsG, summaryRegionsB, summaryRegionsG);


            SetPlaceSummaryModels(resultSummaryTeams, resultSummaryRegions);
        }

        #region ResultTeamProtocol

        private void GetCategotyModels(CategoryViewModel[] categories, CategoryViewModel[] categoriesL, CategoryViewModel[] categoriesR)
        {
            ProtocolResultModel team;
            ProtocolResultModel region;
            for (int i = 0; i < categoriesL.Length; i++)
            {
                for (int j = 0; j < categoriesL[i].PlaceMembers.Count; j++)
                {
                    team = new ProtocolResultModel(categoriesL[i].PlaceMembers[j].TeamName);
                    region = new ProtocolResultModel(categoriesL[i].PlaceMembers[j].RegionName);
                    if (categories[i].Teams.Count == 0)
                    {
                        categories[i].Teams.Add(team);
                    }
                    else if (team.Name != "Лично")
                    {
                        if (!categories[i].CheckTeam(team))
                        {
                            categories[i].Teams.Add(team);
                        }
                    }
                    if (categories[i].Regions.Count == 0)
                    {
                        categories[i].Regions.Add(region);
                    }
                    else if (!categories[i].CheckRegion(region))
                    {
                        categories[i].Regions.Add(region);
                    }
                }
            }

            for (int i = 0; i < categoriesR.Length; i++)
            {
                for (int j = 0; j < categoriesR[i].PlaceMembers.Count; j++)
                {
                    team = new ProtocolResultModel(categoriesR[i].PlaceMembers[j].TeamName);
                    region = new ProtocolResultModel(categoriesL[i].PlaceMembers[j].RegionName);
                    if (categories[i].Teams.Count == 0)
                    {
                        categories[i].Teams.Add(team);
                    }
                    else if (team.Name != "Лично")
                    {
                        if (!categories[i].CheckTeam(team))
                        {
                            categories[i].Teams.Add(team);
                        }
                    }

                    if (categories[i].Regions.Count == 0)
                    {
                        categories[i].Regions.Add(region);
                    }
                    else if (!categories[i].CheckRegion(region))
                    {
                        categories[i].Regions.Add(region);
                    }
                }
            }

        }

        private void GetSummaryProtocolPoints(CategoryViewModel[] categories, ObservableCollection<ProtocolModel> summaryTeams, ObservableCollection<ProtocolModel> summaryRegions)
        {
            int score = 0;
            bool isSportTeam = false;
            bool isContainsTeam = false;
            bool isRegion = false;
            bool isContainsRegion = false;
            ProtocolModel protocolTeam;
            ProtocolModel protocolRegion;
            for (int k = 0; k < categories.Length; k++)
            {
                for (int l = 0; l < categories[k].Teams.Count; l++)
                {
                    protocolTeam = new ProtocolModel(categories[k].Teams[l].Name);
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
                    isContainsTeam=false;
                }

                for (int l = 0; l < categories[k].Regions.Count; l++)
                {
                    protocolRegion = new ProtocolModel(categories[k].Regions[l].Name);
                    if (summaryRegions.Count > 0)
                    {
                        for (int i = 0; i < summaryRegions.Count; i++)
                        {
                            if (summaryRegions[i].Name == protocolRegion.Name)
                            {
                                isContainsRegion = true;
                            }
                        }
                        if (!isContainsRegion)
                        {
                            summaryRegions.Add(protocolRegion);
                        }
                    }
                    else
                    {
                        summaryRegions.Add(protocolRegion);
                    }
                    isContainsRegion = false;
                }

            }

            GetMembersToModels(categories);

            for (int i = 0; i < categories.Length; i++)
            {
                //teams
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
                                        score = categories[i].Teams[j].Members[t].LeftHandScore + categories[i].Teams[j].Members[t].RightHandScore;
                                    }
                                    else if (categories[i].Teams[j].Members[t].isLeftHand && !categories[i].Teams[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Teams[j].Members[t].LeftHandScore;
                                    }
                                    else if (!categories[i].Teams[j].Members[t].isLeftHand && categories[i].Teams[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Teams[j].Members[t].RightHandScore;
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

                //region
                for (int j = 0; j < categories[i].Regions.Count; j++)
                {
                    for (int k = 0; k < summaryRegions.Count; k++)
                    {
                        if (categories[i].Regions[j].Name == summaryRegions[k].Name)
                        {
                            for (int t = 0; t < categories[i].Regions[j].Members.Count; t++)
                            {
                                if (categories[i].Regions[j].Members[t].IsSportTeam)
                                {
                                    if (categories[i].Regions[j].Members[t].isLeftHand && categories[i].Regions[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Regions[j].Members[t].LeftHandScore + categories[i].Regions[j].Members[t].RightHandScore;
                                    }
                                    else if (categories[i].Regions[j].Members[t].isLeftHand && !categories[i].Regions[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Regions[j].Members[t].LeftHandScore;
                                    }
                                    else if (!categories[i].Regions[j].Members[t].isLeftHand && categories[i].Regions[j].Members[t].isRightHand)
                                    {
                                        score = categories[i].Regions[j].Members[t].RightHandScore;
                                    }
                                    isRegion = true;

                                }

                                if (categories[i].Regions[j].Members[t].Member.Gender == "ж" && isRegion)
                                {
                                    summaryRegions[k].ScoreG += score;
                                }
                                else if (categories[i].Regions[j].Members[t].Member.Gender == "м" && isRegion)
                                {
                                    summaryRegions[k].ScoreB += score;
                                }
                                isRegion = false;
                            }
                        }
                    }
                }
            }

        }


        private void GetMembersToModels(CategoryViewModel[] categories)
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

                    for (int k = 0; k < categories[i].Regions.Count; k++)
                    {
                        if (categories[i].ResultMembers[j].RegionName == categories[i].Regions[k].Name)
                        {
                            categories[i].Regions[k].Members.Add((MemberViewModel)categories[i].ResultMembers[j].Clone());
                        }
                    }

                }
            }

        }

        private void SortSummaryModels(ObservableCollection<ProtocolModel> summaryModel)
        {
            ProtocolModel temp;
            for (int i = 0; i < summaryModel.Count - 1; i++)
            {
                for (int j = i + 1; j < summaryModel.Count; j++)
                {
                    if (summaryModel[i].TotalScore > summaryModel[j].TotalScore)
                    {
                        temp = summaryModel[i];
                        summaryModel[i] = summaryModel[j];
                        summaryModel[j] = temp;
                    }
                }
            }

        }
        private void SetPlaceSummaryModels(ObservableCollection<ProtocolModel> summaryTeams, ObservableCollection<ProtocolModel> summaryRegions)
        {
            int place = 0;
            SortSummaryModels(summaryTeams);
            SortSummaryModels(summaryRegions);
            for (int i = 0; i < summaryTeams.Count; i++)
            {
                place = summaryTeams.Count - i;
                summaryTeams[i].TotalPlace = place;
            }

            for (int i = 0; i < summaryRegions.Count; i++)
            {
                place = summaryRegions.Count - i;
                summaryRegions[i].TotalPlace = place;
            }
        }

        private void CollectDataModel(ObservableCollection<ProtocolModel> summaryTeamsB, ObservableCollection<ProtocolModel> summaryTeamsG, ObservableCollection<ProtocolModel> summaryRegionsB, ObservableCollection<ProtocolModel> summaryRegionsG)
        {
            //team
            bool isExist = false;
            for (int i = 0; i < summaryTeamsB.Count; i++)
            {
                isExist = false;
                if (resultSummaryTeams.Count == 0)
                {
                    resultSummaryTeams.Add(new ProtocolModel(summaryTeamsB[i].Name));
                    resultSummaryTeams[0].ScoreB = summaryTeamsB[i].ScoreB;
                }
                else
                {
                    for (int j = 0; j < resultSummaryTeams.Count; j++)
                    {
                        if (resultSummaryTeams[j].Name == summaryTeamsB[i].Name)
                        {
                            isExist = true;
                        }
                    }
                    if (!isExist)
                    {
                        resultSummaryTeams.Add(new ProtocolModel(summaryTeamsB[i].Name));
                        resultSummaryTeams[resultSummaryTeams.Count-1].ScoreB = summaryTeamsB[i].ScoreB;
                    }
                }
            }

            for (int i = 0; i < summaryTeamsG.Count; i++)
            {
                for (int j = 0; j < resultSummaryTeams.Count; j++)
                {
                    if(resultSummaryTeams[j].Name == summaryTeamsG[i].Name)
                    {
                        resultSummaryTeams[j].ScoreG = summaryTeamsG[i].ScoreG;
                        
                    }
                    else
                    {
                        for (int l = 0; l < resultSummaryTeams.Count; l++)
                        {
                            if (resultSummaryTeams[l].Name == summaryTeamsG[i].Name)
                            {
                                isExist = true;
                            }
                        }
                        if (!isExist)
                        {
                            resultSummaryTeams.Add(new ProtocolModel(summaryTeamsG[i].Name));
                            resultSummaryTeams[resultSummaryTeams.Count - 1].ScoreG = summaryTeamsG[i].ScoreG;
                        }
                        isExist = false;
                        
                    }
                }
            }

            for (int j = 0; j < resultSummaryTeams.Count; j++)
            {
                resultSummaryTeams[j].TotalScore = resultSummaryTeams[j].ScoreG + resultSummaryTeams[j].ScoreB;
            }

            //region
            isExist = false;

            for (int i = 0; i < summaryRegionsB.Count; i++)
            {
                isExist = false;
                if (resultSummaryRegions.Count == 0)
                {
                    resultSummaryRegions.Add(new ProtocolModel(summaryRegionsB[i].Name));
                    resultSummaryRegions[0].ScoreB = summaryRegionsB[i].ScoreB;
                }
                else
                {
                    for (int j = 0; j < resultSummaryRegions.Count; j++)
                    {
                        if (resultSummaryRegions[j].Name == summaryRegionsB[i].Name)
                        {
                            isExist = true;
                        }
                    }
                    if (!isExist)
                    {
                        resultSummaryRegions.Add(new ProtocolModel(summaryRegionsB[i].Name));
                        resultSummaryRegions[resultSummaryRegions.Count - 1].ScoreB = summaryRegionsB[i].ScoreB;
                    }
                }
            }



            for (int i = 0; i < summaryRegionsG.Count; i++)
            {
                for (int j = 0; j < resultSummaryRegions.Count; j++)
                {
                    if (resultSummaryRegions[j].Name == summaryRegionsG[i].Name)
                    {
                        resultSummaryRegions[j].ScoreG = summaryRegionsG[i].ScoreG;

                    }
                    else
                    {
                        for (int l = 0; l < resultSummaryRegions.Count; l++)
                        {
                            if (resultSummaryRegions[l].Name == summaryRegionsG[i].Name)
                            {
                                isExist = true;
                            }
                        }
                        if (!isExist)
                        {
                            resultSummaryRegions.Add(new ProtocolModel(summaryRegionsG[i].Name));
                            resultSummaryRegions[resultSummaryRegions.Count - 1].ScoreG = summaryRegionsG[i].ScoreG;
                        }
                        isExist = false;

                    }
                }
            }

            for (int j = 0; j < resultSummaryRegions.Count; j++)
            {
                resultSummaryRegions[j].TotalScore = resultSummaryRegions[j].ScoreG + resultSummaryRegions[j].ScoreB;
            }
        }


        #endregion

        public void GetTotalRegionResults()
        {

        }

        #region ResultRegionProtocol
        #endregion  


    }
}



