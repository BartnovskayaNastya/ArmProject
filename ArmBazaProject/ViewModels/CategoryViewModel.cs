using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArmBazaProject.Entities;
using ArmBazaProject.Models;
using ArmBazaProject.ViewModels;
using System.Windows.Input;

namespace ArmBazaProject
{

    public delegate void FirstMembersHandler(ObservableCollection<MemberViewModel> members);

    public class CategoryViewModel : ViewModelBase
    {
        #region объявление
        readonly Random random = new Random();
        int index = 0;
        int indexA;
        int indexB;
        bool isAall = false;
        bool isBall = false;
        bool isDone = false;
        bool isDonisimo = false;

        private WeightCategory weightCategory;

        private ObservableCollection<ToureViewModel> toures;
        ToureViewModel toure;
        ToureViewModel toureToAdd;
        ToureViewModel reserveToureToAdd;


        MemberViewModel someMemberFirst;
        MemberViewModel someMemberSecond;
        MemberViewModel someMember;

        private ObservableCollection<MemberViewModel> resultMembers;
        private string membersCount = "";
        private ObservableCollection<MemberViewModel> allMembers;
        private ObservableCollection<MemberViewModel> placeMembers;
        private ObservableCollection<MemberViewModel> firstTourMembers;
        private ObservableCollection<MemberViewModel> semifinal;
        private ToureViewModel final;

        private ObservableCollection<ProtocolResultModel> regions;

        private ObservableCollection<ProtocolResultModel> teams;

        private ObservableCollection<ProtocolResultModel> resultTeams;


        #endregion


        #region свойства доступа

        public DelegateCommand FirstToureVisability {set; get; }

        public DelegateCommand StartToureCommand { set; get; }
        public DelegateCommand FirstToureCommand {set; get; }

        public DelegateCommand DrawCommand { set; get; }

        public DelegateCommand FinalCommandA { set; get; }
        public DelegateCommand FinalCommandB {  set; get; }
        public DelegateCommand SemiFinalCommand { set; get; }


        public WeightCategory WeightCategory
        {
            get { return weightCategory; }
            set
            {
                if (weightCategory != value)
                {
                    weightCategory = value;
                    OnPropertyChanged("WeightCategory");
                }
            }
        }
        public string MembersCount
        {
            get { return membersCount; }
            set
            {
                if (membersCount != value)
                {
                    membersCount = value;
                    OnPropertyChanged("MembersCount");
                }
            }
        }
        public ObservableCollection<ProtocolResultModel> ResultTeams
        {
            get { return resultTeams; }
            set
            {
                if (resultTeams != value)
                {
                    resultTeams = value;
                    OnPropertyChanged("ResultTeams");
                }
            }
        }

        public ObservableCollection<ProtocolResultModel> Regions
        {
            get { return regions; }
            set
            {
                if (regions != value)
                {
                    regions = value;
                    OnPropertyChanged("Regions");
                }
            }
        }

        public ObservableCollection<ProtocolResultModel> Teams
        {
            get { return teams; }
            set
            {
                if (teams != value)
                {
                    teams = value;
                    OnPropertyChanged("Teams");
                }
            }
        }

        public ObservableCollection<ToureViewModel> Toures
        {
            get { return toures; }
            set
            {
                if (toures != value)
                {
                    toures = value;
                    OnPropertyChanged("Toures");
                }
            }
        }

        public ToureViewModel Final
        {
            get { return final; }
            set
            {
                if (final != value)
                {
                    final = value;
                    OnPropertyChanged("Final");
                }
            }
        }

        public ObservableCollection<MemberViewModel> SemiFinal
        {
            get { return semifinal; }
            set
            {
                if (semifinal != value)
                {
                    semifinal = value;
                    OnPropertyChanged("SemiFinal");
                }
            }
        }
        public ObservableCollection<MemberViewModel> PlaceMembers
        {
            get { return placeMembers; }
            set
            {
                if (placeMembers != value)
                {
                    placeMembers = value;
                    OnPropertyChanged("PlaceMembers");
                }
            }
        }

        public ObservableCollection<MemberViewModel> ResultMembers
        {
            get { return resultMembers; }
            set
            {
                if (resultMembers != value)
                {
                    resultMembers = value;
                    OnPropertyChanged("ResultMembers");
                }
            }
        }


        public ObservableCollection<MemberViewModel> FirstToureMembers
        {
            get { return firstTourMembers; }
            set
            {
                if (firstTourMembers != value)
                {
                    firstTourMembers = value;
                    OnPropertyChanged("FirstToureMembers");
                }
            }
        }

        public ObservableCollection<MemberViewModel> AllMembers
        {
            get { return allMembers; }
            set
            {
                if (allMembers != value)
                {
                    allMembers = value;
                    OnPropertyChanged("AllMembers");
                }
            }
        }

        #endregion

        public CategoryViewModel(float weight, string name)
        {
            weightCategory = new WeightCategory(weight, name);

            allMembers = new ObservableCollection<MemberViewModel>();
            placeMembers = new ObservableCollection<MemberViewModel>();
            resultMembers = new ObservableCollection<MemberViewModel>();

            toures = new ObservableCollection<ToureViewModel>();
            firstTourMembers = new ObservableCollection<MemberViewModel>();
            final = new ToureViewModel();
            semifinal = new ObservableCollection<MemberViewModel>();

            regions = new ObservableCollection<ProtocolResultModel>();

            teams = new ObservableCollection<ProtocolResultModel>();
            resultTeams = new ObservableCollection<ProtocolResultModel>();

            StartToureCommand = new DelegateCommand(StartCommand);
            FirstToureCommand = new DelegateCommand(FirstCommand);
            FirstToureCommand.isVisible = false;
            DrawCommand = new DelegateCommand(RandomDraw);
            FinalCommandA = new DelegateCommand(FinalButtonA);
            FinalCommandA.isVisible = false;
            FinalCommandB = new DelegateCommand(FinalButtonB);
            FinalCommandB.isVisible = false;
            SemiFinalCommand = new DelegateCommand(SemiFinalButton);
            SemiFinalCommand.isVisible = false;

            toure = new ToureViewModel();
            toure.buttonPressedEvent += ButtonClickToure;

        }

        public void SetTeams()
        {
            ProtocolResultModel team;
            for (int i = 0; i < allMembers.Count; i++)
            {
                team = new ProtocolResultModel(allMembers[i].TeamName);
                if (!CheckTeam(team))
                {
                    teams.Add(team);
                }
            }
        }

        public void RandomDraw(object param)
        {
            Random rng = new Random();
            int n = allMembers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                MemberViewModel value = allMembers[k];
                allMembers[k] = allMembers[n];
                allMembers[n] = value;
            }
        }

        public void RandomDraw(Random rng)
        {

            int n = allMembers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                MemberViewModel value = allMembers[k];
                allMembers[k] = allMembers[n];
                allMembers[n] = value;
            }
        }

        public bool CheckRegion(ProtocolResultModel regionToAdd)
        {
            bool isExist = false;
            if (regions.Count > 0)
            {
                foreach (ProtocolResultModel region in regions)
                {
                    if (region.Name == regionToAdd.Name)
                    {
                        isExist = true;
                        break;
                    }
                }
            }
            return isExist;

        }

        public bool CheckTeam(ProtocolResultModel teamToAdd)
        {
            bool isExist = false;
            if(teams.Count > 0)
            {
                foreach (ProtocolResultModel team in teams)
                {
                    if (team.Name == teamToAdd.Name)
                    {
                        isExist = true;
                        break;
                    }
                }
            }
            return isExist;

        }

        public void AddMember(MemberViewModel somemember)
        {
            bool isExist = false;
            foreach (MemberViewModel member in allMembers)
            {
                if (somemember.Member.FullName == member.Member.FullName)
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                allMembers.Add(somemember);
            }

        }

        private void StartCommand(object param)
        {
            if (allMembers.Count == 1)
            {
                allMembers[0].Place = 1;
                placeMembers.Add((MemberViewModel)allMembers[0].Clone());
            }
            else if (allMembers.Count == 2)
            {
                semifinal.Add((MemberViewModel)allMembers[0].Clone());
                semifinal.Add((MemberViewModel)allMembers[1].Clone());
                SemiFinalCommand.isVisible = true;
            }
            else
            {
                int size;
                bool isU = false;
                foreach (MemberViewModel member in allMembers)
                {
                    firstTourMembers.Add((MemberViewModel)member.Clone());
                }
                //если нечетное кол-во участников
                if (firstTourMembers.Count % 2 > 0)
                {
                    size = firstTourMembers.Count - 1;
                    isU = true;
                }
                else
                {
                    size = firstTourMembers.Count;
                }
                for (int i = 0; i < size; i++)
                {
                    toure.Toure.ToureMembers.Add((MemberViewModel)firstTourMembers[i].Clone());
                }
                if (isU)
                {
                    toure.Toure.ToureMembersA.Add((MemberViewModel)firstTourMembers[firstTourMembers.Count - 1].Clone());
                    firstTourMembers.RemoveAt(firstTourMembers.Count - 1);
                }
                toures.Add(toure);
                
            }
            StartToureCommand.isVisible = false;
            if(allMembers.Count > 2)
            {
                FirstToureCommand.isVisible = true;
            }
            
        }
        private void FirstCommand(object param)
        {
            
            someMember = new MemberViewModel();
            toure.Toure.ToureMembers.Clear();
            foreach (MemberViewModel member in firstTourMembers)
            {
                toure.Toure.ToureMembers.Add((MemberViewModel)member.Clone());
            }
            //3 участника
            if (firstTourMembers.Count == 2 && toures[0].Toure.ToureMembersA.Count == 1)
            {
                getThreeMembers();
                toures[0].IsVisible = true;
            }
            // 2 участника
            else if (firstTourMembers.Count == 2)
            {
                someMember = (MemberViewModel)toure.Toure.ToureMembers[0].Clone();
                if (someMember.IsWiner)
                {
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)toure.Toure.ToureMembers[1].Clone();
                    someMember.LoseCounter++;
                    final.Toure.ToureMembersA.Add(someMember);
                }
                else
                {
                    someMember = (MemberViewModel)toure.Toure.ToureMembers[1].Clone();
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)toure.Toure.ToureMembers[0].Clone();
                    someMember.LoseCounter++;
                    final.Toure.ToureMembersA.Add(someMember);
                }
                FinalCommandA.isVisible = true;
            }
            //3+ участника
            else
            {
                toure.SortFirstToure();
                if (toures.Count >= 1)
                {
                    
                    if (toures[toures.Count - 1].Toure.CheckExtraA() || toures[toures.Count - 1].Toure.CheckExtraB())
                    {
                        reserveToureToAdd = new ToureViewModel();
                        reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                        ExtraToure(toures[toures.Count - 1], reserveToureToAdd);
                        toures[toures.Count - 1].ifExtra = true;
                        index = toures.Count - 1;
                        toures.Add(reserveToureToAdd);
                    }
                }
                toures[0].IsVisible = true;

            }
            FirstToureCommand.isVisible = false;
            
        }

        private void FinalButtonA(object param)
        {
            someMemberFirst = (MemberViewModel)final.Toure.ToureMembersA[0].Clone();
            someMemberSecond = (MemberViewModel)final.Toure.ToureMembersA[1].Clone();
            if(allMembers.Count == 2)
            {
                if (someMemberFirst.IsWiner)
                {
                    someMemberSecond.LoseCounter++;
                    if(someMemberSecond.LoseCounter == 2)
                    {
                        someMemberSecond.Place = 2;
                        placeMembers.Add(someMemberSecond);
                        someMemberFirst.Place = 1;
                        placeMembers.Add(someMemberFirst);
                        
                    }
                    else
                    {
                        someMemberFirst.IsWiner = false;
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberFirst.Clone());
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberSecond.Clone());
                    }
                }
                else
                {
                    someMemberFirst.LoseCounter++;
                    if (someMemberFirst.LoseCounter == 2)
                    {
                        someMemberFirst.Place = 2;
                        placeMembers.Add(someMemberFirst);
                        someMemberSecond.Place = 1;
                        placeMembers.Add(someMemberSecond);

                    }
                    else
                    {
                        someMemberSecond.IsWiner = false;
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberSecond.Clone());
                        final.Toure.ToureMembersB.Add((MemberViewModel)someMemberFirst.Clone());
                        FinalCommandB.isVisible = true;
                    }
                }
            }
            else
            {
                if (final.Toure.ToureMembersA[0].IsWiner)
                {

                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                }
                else
                {
                    someMemberFirst.IsWiner = false;
                    final.Toure.ToureMembersB.Add(someMemberFirst);
                    someMemberSecond.IsWiner = false;
                    final.Toure.ToureMembersB.Add(someMemberSecond);
                    FinalCommandB.isVisible = true;
                }
            }

            FinalCommandA.isVisible = false;

        }
        private void FinalButtonB(object param)
        {
            someMemberFirst = (MemberViewModel)final.Toure.ToureMembersB[0].Clone();
            someMemberSecond = (MemberViewModel)final.Toure.ToureMembersB[1].Clone();
            if (allMembers.Count == 2)
            {
                if (someMemberFirst.IsWiner)
                {
                    someMemberSecond.Place = 2;
                    placeMembers.Add(someMemberSecond);
                    someMemberFirst.Place = 1;
                    placeMembers.Add(someMemberFirst);
                }
                else
                {
                    someMemberFirst.Place = 2;
                    placeMembers.Add(someMemberFirst);
                    someMemberSecond.Place = 1;
                    placeMembers.Add(someMemberSecond);
                }
            }
            else
            {
                if (final.Toure.ToureMembersB[0].IsWiner)
                {
                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                }
                else
                {
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);
                }
            }
            FinalCommandB.isVisible = false;
        }
        private void SemiFinalButton(object param)
        {
            if(allMembers.Count != 2)
            {
                someMember = new MemberViewModel();
                if (semifinal[0].IsWiner)
                {
                    someMember = (MemberViewModel)semifinal[0].Clone();
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)semifinal[1].Clone();
                    someMember.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMember);
                }
                else
                {
                    someMember = (MemberViewModel)semifinal[1].Clone();
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)semifinal[0].Clone();
                    someMember.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMember);
                }
            }
            else
            {
                someMember = new MemberViewModel();
                if (semifinal[0].IsWiner)
                {
                    someMember = (MemberViewModel)semifinal[0].Clone();
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)semifinal[1].Clone();
                    someMember.LoseCounter++;
                    final.Toure.ToureMembersA.Add(someMember);
                }
                else
                {
                    someMember = (MemberViewModel)semifinal[1].Clone();
                    someMember.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMember);
                    someMember = (MemberViewModel)semifinal[0].Clone();
                    someMember.LoseCounter++;
                    final.Toure.ToureMembersA.Add(someMember);
                }
            }
            

            SemiFinalCommand.isVisible = false;
            FinalCommandA.isVisible = true;
           
        }

        private void ButtonClickToure(ToureViewModel toure)
        {
            int indexOfVisability = 0;
            bool isToureBDone = true;
            int ind = 0;
            
            //3 участника
            if (toures[0].Toure.ToureMembersA.Count == 2 && toures[0].Toure.ToureMembersB.Count == 0)
            {
                if (toures[0].Toure.ToureMembersA[0].IsWiner)
                {
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();
                    someMemberFirst.IsWiner  = false;
                    someMemberSecond.IsWiner = false;

                    final.Toure.ToureMembersA.Add(someMemberFirst);
                    semifinal.Add(someMemberSecond);
                }
                else if (toures[0].Toure.ToureMembersA[1].IsWiner)
                {
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();
                    someMemberFirst.IsWiner = false;
                    someMemberSecond.IsWiner = false;

                    final.Toure.ToureMembersA.Add(someMemberSecond);
                    semifinal.Add(someMemberFirst);
                }
            }
            //4 участника
            else if (toures[0].Toure.ToureMembersA.Count == 2 && toures[0].Toure.ToureMembersB.Count == 2 && allMembers.Count == 4)
            {
                someMemberFirst = new MemberViewModel();
                someMemberSecond = new MemberViewModel();
                someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();


                if (someMemberFirst.IsWiner)
                {
                    someMemberFirst.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMemberFirst);
                    semifinal.Add(someMemberSecond);


                }
                else if (someMemberSecond.IsWiner)
                {
                    someMemberSecond.IsWiner = false;
                    final.Toure.ToureMembersA.Add(someMemberSecond);
                    semifinal.Add(someMemberFirst);
                }

                someMemberFirst = new MemberViewModel();
                someMemberSecond = new MemberViewModel();
                someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersB[0].Clone();
                someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersB[1].Clone();

                if (someMemberFirst.IsWiner)
                {
                    someMemberFirst.IsWiner = false;
                    semifinal.Add(someMemberFirst);
                    someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberSecond);


                }
                else if (someMemberSecond.IsWiner)
                {
                    someMemberSecond.IsWiner = false;
                    semifinal.Add(someMemberSecond);
                    someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMemberFirst);
                }

            }
            //5 
            else if (allMembers.Count == 5)
            {
                
                if(toures[0].Toure.ToureMembersA.Count == 2 && toures[0].Toure.ToureMembersB.Count == 2 && !isDone)
                {
                    isDone = true;
                    toureToAdd = toures[toures.Count - 1];
                    toureToAdd.buttonPressedEvent += ButtonClickToure;

                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();


                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        toureToAdd.Toure.ToureMembersA.Add(someMemberFirst);
                        toureToAdd.Toure.ToureMembersB.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        toureToAdd.Toure.ToureMembersA.Add(someMemberSecond);
                        toureToAdd.Toure.ToureMembersB.Add(someMemberFirst);
                    }

                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersB[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersB[1].Clone();

                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        toureToAdd.Toure.ToureMembersB.Add(someMemberFirst);
                        someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        toureToAdd.Toure.ToureMembersB.Add(someMemberSecond);
                        someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberFirst);
                    }
                    int name = toures.Count;
                    toureToAdd.Name = name.ToString();
                }
                else if (!isDonisimo)
                {
                    isDonisimo = true;
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[1].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[1].Toure.ToureMembersA[1].Clone();


                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberFirst);
                        semifinal.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberSecond);
                        semifinal.Add(someMemberFirst);
                    }

                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[1].Toure.ToureMembersB[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[1].Toure.ToureMembersB[1].Clone();

                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        semifinal.Add(someMemberFirst);
                        someMemberSecond.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberSecond);


                    }
                    else if (someMemberSecond.IsWiner)
                    {
                        someMemberSecond.IsWiner = false;
                        semifinal.Add(someMemberSecond);
                        someMemberFirst.Place = allMembers.Count - placeMembers.Count;
                        placeMembers.Add(someMemberFirst);
                    }
                }

            }   
            
            else
            {
                if (toures[0].Toure.ToureMembersA.Count == 2 && toures.Count == 1 && toures[0].Toure.ToureMembersB.Count > 2)
                {
                    someMemberFirst = new MemberViewModel();
                    someMemberSecond = new MemberViewModel();
                    someMemberFirst = (MemberViewModel)toures[0].Toure.ToureMembersA[0].Clone();
                    someMemberSecond = (MemberViewModel)toures[0].Toure.ToureMembersA[1].Clone();
                    if (someMemberFirst.IsWiner)
                    {
                        someMemberFirst.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberFirst);
                        semifinal.Add(someMemberSecond);

                    }
                    else
                    {
                        someMemberSecond.IsWiner = false;
                        final.Toure.ToureMembersA.Add(someMemberSecond);
                        semifinal.Add(someMemberFirst);
                    }
                }
                if (toures.Count != 1)
                {
                    if (((toures[toures.Count - 1].Toure.ToureMembersA.Count == 2 || toures[toures.Count - 2].Toure.ToureMembersA.Count == 2)&& (toures[toures.Count - 1].Toure.ToureMembersA.Count != 1))&&
                        ((toures[toures.Count - 1].Toure.ToureMembersB.Count == 1 && toures[toures.Count - 2].Toure.ToureMembersB.Count == 2) ||
                        (toures[toures.Count - 1].Toure.ToureMembersB.Count > 2)
                        ))
                    {
                        isToureBDone = false;
                    }
                    else if (((toures[toures.Count - 2].Toure.ToureMembersA.Count == 2 && toures[toures.Count - 1].Toure.ToureMembersA.Count == 0) || (toures[toures.Count - 1].Toure.ToureMembersA.Count == 2)) && (!isAall))
                    {
                        if (toures[toures.Count - 1].Toure.ToureMembersA.Count == 0)
                        {
                            ind = toures.Count - 2;
                        }
                        else if (toures[toures.Count - 1].Toure.ToureMembersA.Count == 2)
                        {
                            ind = toures.Count - 1;
                        }
                        isAall = true;
                        someMemberFirst = new MemberViewModel();
                        someMemberSecond = new MemberViewModel();
                        someMemberFirst = (MemberViewModel)toures[ind].Toure.ToureMembersA[0].Clone();
                        someMemberSecond = (MemberViewModel)toures[ind].Toure.ToureMembersA[1].Clone();
                        if (someMemberFirst.IsWiner)
                        {
                            someMemberFirst.IsWiner = false;
                            final.Toure.ToureMembersA.Add(someMemberFirst);
                            semifinal.Add(someMemberSecond);

                        }
                        else
                        {
                            someMemberSecond.IsWiner = false;
                            final.Toure.ToureMembersA.Add(someMemberSecond);
                            semifinal.Add(someMemberFirst);
                        }

                    }

                    if ((toures[toures.Count - 2].Toure.ToureMembersB.Count == 2 && toures[toures.Count - 1].Toure.ToureMembersB.Count == 0) || (toures[toures.Count - 1].Toure.ToureMembersB.Count == 2))
                    {
                        if (toures[toures.Count - 1].Toure.ToureMembersB.Count == 0)
                        {
                            ind = toures.Count - 2;
                        }
                        else if (toures[toures.Count - 1].Toure.ToureMembersB.Count == 2)
                        {
                            ind = toures.Count - 1;
                        }
                        isBall = true;
                        someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[0].Clone();
                        if (someMember.IsWiner)
                        {
                            someMember.IsWiner = false;
                            semifinal.Add(someMember);
                            someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[1].Clone();
                            someMember.Place = allMembers.Count - placeMembers.Count;
                            placeMembers.Add(someMember);
                        }
                        else
                        {
                            someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[1].Clone();
                            someMember.IsWiner = false;
                            semifinal.Add(someMember);
                            someMember = (MemberViewModel)toures[ind].Toure.ToureMembersB[0].Clone();
                            someMember.Place = allMembers.Count - placeMembers.Count;
                            placeMembers.Add((MemberViewModel)someMember.Clone());

                        }

                    }

                    else if (toures[index].ifExtra)
                    {
                        toures[index].ifExtra = false;
                        SortToures(toures[toures.Count - 1], toures[index], isAall, isBall, isToureBDone);
                        if (toures[toures.Count - 1].Toure.CheckExtraA() || toures[toures.Count - 1].Toure.CheckExtraB())
                        {
                            reserveToureToAdd = new ToureViewModel();
                            reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                            ExtraToure(toures[toures.Count - 1], reserveToureToAdd);
                            toures[toures.Count - 1].ifExtra = true;
                            index = toures.Count - 1;
                            toures.Add(reserveToureToAdd);
                            reserveToureToAdd.Name = toures.Count.ToString();
                        }
                    }
                    else
                    {
                        toureToAdd = new ToureViewModel();
                        toureToAdd.buttonPressedEvent += ButtonClickToure;
                        SortToures(toureToAdd, toure, isAall, isBall, isToureBDone);
                        if (toureToAdd.Toure.CheckExtraA() || toureToAdd.Toure.CheckExtraB())
                        {
                            reserveToureToAdd = new ToureViewModel();
                            reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                            ExtraToure(toureToAdd, reserveToureToAdd);
                            toureToAdd.ifExtra = true;
                            toures.Add(toureToAdd);
                            index = toures.Count - 1;
                            toureToAdd.Name = toures.Count.ToString();
                            toures.Add(reserveToureToAdd);
                            reserveToureToAdd.Name = toures.Count.ToString();
                        }
                        else
                        {
                            toures.Add(toureToAdd);
                            toureToAdd.Name = toures.Count.ToString();
                        }

                    }

                }
                else if (toures[index].ifExtra)
                {
                    toures[index].ifExtra = false;
                    SortToures(toures[toures.Count - 1], toures[index], isAall, isBall, isToureBDone);
                    if (toures[toures.Count - 1].Toure.CheckExtraA() || toures[toures.Count - 1].Toure.CheckExtraB())
                    {
                        reserveToureToAdd = new ToureViewModel();
                        reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                        ExtraToure(toures[toures.Count - 1], reserveToureToAdd);
                        toures[toures.Count - 1].ifExtra = true;
                        index = toures.Count - 1;
                        toures.Add(reserveToureToAdd);
                        reserveToureToAdd.Name = toures.Count.ToString();
                    }
                }
                else
                {
                    toureToAdd = new ToureViewModel();
                    toureToAdd.buttonPressedEvent += ButtonClickToure;
                    SortToures(toureToAdd, toure, isAall, isBall, isToureBDone);
                    if (toureToAdd.Toure.CheckExtraA() || toureToAdd.Toure.CheckExtraB())
                    {
                        reserveToureToAdd = new ToureViewModel();
                        reserveToureToAdd.buttonPressedEvent += ButtonClickToure;
                        ExtraToure(toureToAdd, reserveToureToAdd);
                        toureToAdd.ifExtra = true;
                        toures.Add(toureToAdd);
                        index = toures.Count - 1;
                        toureToAdd.Name = toures.Count.ToString();
                        toures.Add(reserveToureToAdd);
                        reserveToureToAdd.Name = toures.Count.ToString();
                    }
                    else
                    {
                        toures.Add(toureToAdd);
                        toureToAdd.Name = toures.Count.ToString();
                    }

                }

            }

            if (!SemiFinalCommand.isVisible)
            {
                for (int i = 0; i < toures.Count; i++)
                {
                    if (toures[i].IsVisible)
                    {
                        indexOfVisability = i;
                    }
                }

                if (indexOfVisability == toures.Count - 1)
                {
                    SemiFinalCommand.isVisible = true;
                }
                else
                {
                    toures[indexOfVisability + 1].IsVisible = true;
                }

                toures[indexOfVisability].IsVisible = false;
            }
            

        }


        private void getThreeMembers()
        {
            someMemberFirst = new MemberViewModel();
            someMemberSecond = new MemberViewModel();
            someMemberFirst = (MemberViewModel)firstTourMembers[0].Clone();
            someMemberSecond = (MemberViewModel)firstTourMembers[1].Clone();
            if (someMemberFirst.IsWiner)
            {
                someMemberFirst.IsWiner = false;
                toures[0].Toure.ToureMembersA.Add(someMemberFirst);
                someMemberSecond.LoseCounter++;
                semifinal.Add(someMemberSecond);

            }
            else
            {
                someMemberSecond.IsWiner = false;
                toures[0].Toure.ToureMembersA.Add(someMemberSecond);
                someMemberFirst.LoseCounter++;
                semifinal.Add(someMemberFirst);

            }
        }
        //сортировка A тура
        private void SortAToure(ToureViewModel toureToAdd, ToureViewModel toure, bool isAall, bool isToureBDone)
        {
            foreach (MemberViewModel member in toure.Toure.ToureMembersA)
            {
                someMember = new MemberViewModel();
                someMember = (MemberViewModel)member.Clone();
                if (someMember.IsWiner && !isAall && isToureBDone)
                {
                    someMember.IsWiner = false;
                    toureToAdd.Toure.ToureMembersA.Add(someMember);
                }
                else if(!someMember.IsWiner && !isAall && isToureBDone)
                {
                    someMember.LoseCounter++;
                    toureToAdd.Toure.ToureMembersB.Add(someMember);
                }
                else if (!isToureBDone)
                {
                    someMember.IsWiner = false;
                    toureToAdd.Toure.ToureMembersA.Add(someMember);
                }
            }
        }

        //сортировка B тура
        private void SortBToure(ToureViewModel toureToAdd, ToureViewModel toure, bool isBall)
        {
            foreach (MemberViewModel member in toure.Toure.ToureMembersB)
            {
                //someMember = new MemberViewModel();
                someMember = (MemberViewModel)member.Clone();
                if (someMember.IsWiner && !isBall)
                {
                    someMember.IsWiner = false;
                    toureToAdd.Toure.ToureMembersB.Add(someMember);
                }
                else
                {
                    someMember.LoseCounter++;
                    someMember.Place = allMembers.Count - placeMembers.Count;
                    placeMembers.Add(someMember);
                }
            }
        }

        private void SortToures(ToureViewModel toureToAdd, ToureViewModel toure, bool isAall, bool isBall, bool isToureBDobe)
        {
            SortAToure(toureToAdd, toure, isAall, isToureBDobe);
            SortBToure(toureToAdd, toure, isBall);
        }


        private void ExtraToure(ToureViewModel toureToAdd, ToureViewModel extraToure)
        {
            indexA = toureToAdd.Toure.ToureMembersA.Count - 1;
            indexB = toureToAdd.Toure.ToureMembersB.Count - 1;

            if (toureToAdd.Toure.CheckExtraA())
            {
                extraToure.Toure.ToureMembersA.Add((MemberViewModel)toureToAdd.Toure.ToureMembersA[indexA].Clone());
                toureToAdd.Toure.ToureMembersA.RemoveAt(indexA);
            }
            if (toureToAdd.Toure.CheckExtraB())
            {
                extraToure.Toure.ToureMembersB.Add((MemberViewModel)toureToAdd.Toure.ToureMembersB[indexB].Clone());
                toureToAdd.Toure.ToureMembersB.RemoveAt(indexB);
            }
        }

    }
}