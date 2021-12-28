using System;
using ArmBazaProject.Entities;
using ArmBazaProject.ViewModels;

namespace ArmBazaProject
{
    [Serializable]
    public class MemberViewModel : NotifyableObject, ICloneable
    {
        private Member member;
        private bool isWiner = false;
        private string weightCategory;
        private string qualificationName;
        private string teamName;
        private string trainerName;
        private string regionName;
        private int place;
        private string hand;
        private int loseCounter;
        private int score;

        public bool isRightHand;
        public bool isLeftHand;

        //resultData
        private int leftHandScore;
        private int rightHandScore;
        private int resultHandScore;

        public bool scoreZero = false;

        private string rightHandScoreVM;
        private string leftHandScoreVM;

        private int rightHandPlaceRegionProtocol;
        private int leftHandPlaceRegionProtocol;

        private int leftHandScoreRegion;
        private int rightHandScoreRegion;

        private string leftHandScoreRegionVM;
        private string rightHandScoreRegionVM;

        private string leftHandPlaceRegionVM;
        private string rightHandPlaceRegionVM;



        private int rightHandPlace;
        private int leftHandPlace;
        private int resultHandPlace;

        private int rightHandPlaceProtocol;
        private int leftHandPlaceProtocol;

        private string rightHandPlaceVM;
        private string leftHandPlaceVM;


        //sportTeam
        private bool isSportTeam;
        private bool isRegion;
        private int leftHandSTScore;
        private int rightHandSTScore;
        private string leftHandSTScoreVM;
        private string rightHandSTScoreVM;

        private int tempResultScoreHands;



        public object Clone()
        {
            return new MemberViewModel()
            {
                Member = (Member)Member.Clone(),
                IsWiner = IsWiner,
                WeightCategory = WeightCategory,
                QualificationName = QualificationName,
                TeamName = TeamName,
                TrainerName = TrainerName,
                RegionName = RegionName,
                Place = Place,
                Hand = Hand,
                LoseCounter = LoseCounter,
                Score = Score,
                isLeftHand = isLeftHand,
                isRightHand = isRightHand,
                LeftHandScore = LeftHandScore,
                LeftHandPlace = LeftHandPlace,
                RightHandScore = RightHandScore,
                RightHandPlace = RightHandPlace,
                ResultHandPlace = ResultHandPlace,
                ResultHandScore = ResultHandScore,
                IsSportTeam = IsSportTeam,
                IsRegion = IsRegion,
                RightHandSTScore = RightHandSTScore,
                LeftHandSTScore = LeftHandSTScore,
                LeftHandSTScoreVM = LeftHandSTScoreVM,
                RightHandSTScoreVM = RightHandSTScoreVM,
                TempResultScoreHands = TempResultScoreHands,
                LeftHandScoreVM = LeftHandScoreVM,
                RightHandScoreVM = RightHandScoreVM,
                RightHandPlaceProtocol = RightHandPlaceProtocol,
                LeftHandPlaceProtocol = LeftHandPlaceProtocol,
                RightHandPlaceRegionProtocol = RightHandPlaceRegionProtocol,
                LeftHandPlaceRegionProtocol = LeftHandPlaceRegionProtocol,
                RightHandScoreRegion = RightHandScoreRegion,
                LeftHandScoreRegion = LeftHandScoreRegion,
                LeftHandScoreRegionVM = LeftHandScoreRegionVM,
                RightHandScoreRegionVM = RightHandScoreRegionVM,
                LeftHandPlaceRegionVM = LeftHandPlaceRegionVM,
                RightHandPlaceRegionVM = RightHandPlaceRegionVM


            };
        }

        #region Properties
        public Member Member
        {
            get { return member; }
            set
            {
                member = value;
                OnPropertyChanged("Member");
            }
        }

        public string RightHandPlaceRegionVM
        {
            get { return rightHandPlaceRegionVM; }
            set
            {
                rightHandPlaceRegionVM = value;
                OnPropertyChanged("RightHandPlaceRegionVM");
            }
        }

        public string LeftHandPlaceRegionVM
        {
            get { return leftHandPlaceRegionVM; }
            set
            {
                leftHandPlaceRegionVM = value;
                OnPropertyChanged("LeftHandPlaceRegionVM");
            }
        }

        public string LeftHandScoreRegionVM
        {
            get { return leftHandScoreRegionVM; }
            set
            {
                leftHandScoreRegionVM = value;
                OnPropertyChanged("LeftHandScoreRegionVM");
            }
        }

        public string RightHandScoreRegionVM
        {
            get { return rightHandScoreRegionVM; }
            set
            {
                rightHandScoreRegionVM = value;
                OnPropertyChanged("RightHandScoreRegionVM");
            }
        }

        public int RightHandScoreRegion
        {
            get { return rightHandScoreRegion; }
            set
            {
                rightHandScoreRegion = value;
                OnPropertyChanged("RightHandScoreRegion");
            }
        }

        public int LeftHandScoreRegion
        {
            get { return leftHandScoreRegion; }
            set
            {
                leftHandScoreRegion = value;
                OnPropertyChanged("LeftHandScoreRegion");
            }
        }

        public int LeftHandPlaceProtocol
        {
            get { return leftHandPlaceProtocol; }
            set
            {
                leftHandPlaceProtocol = value;
                OnPropertyChanged("LeftHandPlaceProtocol");
            }
        }

        public int LeftHandPlaceRegionProtocol
        {
            get { return leftHandPlaceRegionProtocol; }
            set
            {
                leftHandPlaceRegionProtocol = value;
                OnPropertyChanged("LeftHandPlaceRegionProtocol");
            }
        }

        public int RightHandPlaceRegionProtocol
        {
            get { return rightHandPlaceRegionProtocol; }
            set
            {
                rightHandPlaceRegionProtocol = value;
                OnPropertyChanged("RightHandPlaceRegionProtocol");
            }
        }

        public bool IsRegion
        {
            get { return isRegion; }
            set
            {
                isRegion = value;
                OnPropertyChanged("IsRegion");
            }
        }

        public int RightHandPlaceProtocol
        {
            get { return rightHandPlaceProtocol; }
            set
            {
                rightHandPlaceProtocol = value;
                OnPropertyChanged("RightHandPlaceProtocol");
            }
        }

        public string RightHandPlaceVM
        {
            get { return rightHandPlaceVM; }
            set
            {
                rightHandPlaceVM = value;
                OnPropertyChanged("RightHandPlaceVM");
            }
        }

        public string LeftHandPlaceVM
        {
            get { return leftHandPlaceVM; }
            set
            {
                leftHandPlaceVM = value;
                OnPropertyChanged("LeftHandPlaceVM");
            }
        }

        public string RightHandScoreVM
        {
            get { return rightHandScoreVM; }
            set
            {
                rightHandScoreVM = value;
                OnPropertyChanged("RightHandScoreVM");
            }
        }

        public string LeftHandScoreVM
        {
            get { return leftHandScoreVM; }
            set
            {
                leftHandScoreVM = value;
                OnPropertyChanged("LeftHandScoreVM");
            }
        }

        public int TempResultScoreHands
        {
            get { return tempResultScoreHands; }
            set
            {
                tempResultScoreHands = value;
                OnPropertyChanged("TempResultScoreHands");
            }
        }




        public string LeftHandSTScoreVM
        {
            get { return leftHandSTScoreVM; }
            set
            {
                leftHandSTScoreVM = value;
                OnPropertyChanged("LeftHandSTScoreVM");
            }
        }

        public string RightHandSTScoreVM
        {
            get { return rightHandSTScoreVM; }
            set
            {
                rightHandSTScoreVM = value;
                OnPropertyChanged("RightHandSTScoreVM");
            }
        }

        public bool IsSportTeam
        {
            get { return isSportTeam; }
            set
            {
                isSportTeam = value;
                OnPropertyChanged("IsSportTeam");
            }
        }

        public int RightHandPlace
        {
            get { return rightHandPlace; }
            set
            {
                rightHandPlace = value;
                OnPropertyChanged("RightHandPlace");
            }
        }

        public int LeftHandSTScore
        {
            get { return leftHandSTScore; }
            set
            {
                leftHandSTScore = value;
                OnPropertyChanged("LeftHandSTScore");
            }
        }

        public int RightHandSTScore
        {
            get { return rightHandSTScore; }
            set
            {
                rightHandSTScore = value;
                OnPropertyChanged("RightHandSTScore");
            }
        }

        public int ResultHandPlace
        {
            get { return resultHandPlace; }
            set
            {
                resultHandPlace = value;
                OnPropertyChanged("ResultHandPlace");
            }
        }

        public int LeftHandPlace
        {
            get { return leftHandPlace; }
            set
            {
                leftHandPlace = value;
                OnPropertyChanged("LeftHandPlace");
            }
        }

        public int LeftHandScore
        {
            get { return leftHandScore; }
            set
            {
                leftHandScore = value;
                OnPropertyChanged("LeftHandScore");
            }
        }

        public int RightHandScore
        {
            get { return rightHandScore; }
            set
            {
                rightHandScore = value;
                OnPropertyChanged("RightHandScore");
            }
        }

        public int ResultHandScore
        {
            get { return resultHandScore; }
            set
            {
                resultHandScore = value;
                OnPropertyChanged("ResultHandScore");
            }
        }


        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        public int LoseCounter
        {
            get { return loseCounter; }
            set
            {
                loseCounter = value;
                OnPropertyChanged("LoseCounter");
            }
        }
        public string Hand
        {
            get { return hand; }
            set
            {
                hand = value;
                OnPropertyChanged("Hand");
            }
        }

        public int Place
        {
            get { return place; }
            set
            {
                place = value;
                OnPropertyChanged("Place");
            }
        }

        public string QualificationName
        {
            get { return qualificationName; }
            set
            {
                qualificationName = value;
                OnPropertyChanged("QualificationName");
            }
        }

        public string TeamName
        {
            get { return teamName; }
            set
            {
                teamName = value;
                OnPropertyChanged("TeamName");
            }
        }

        public string TrainerName
        {
            get { return trainerName; }
            set
            {
                trainerName = value;
                OnPropertyChanged("TrainerName");
            }
        }

        public string RegionName
        {
            get { return regionName; }
            set
            {
                regionName = value;
                OnPropertyChanged("RegionName");
            }
        }

        public string QualificationNameDB
        {
            get
            {

                if (member.Qualification != null)
                {
                    return member.Qualification.Name;
                }
                else
                    return "";
            }
            set
            {
                member.Qualification.Name = value;
                OnPropertyChanged("QualificationNameDB");
            }
        }

        public string RegionNameDB
        {
            get
            {

                if (member.Team != null)
                {
                    return member.Team.Region.Name;
                }
                else
                    return "";
            }
            set
            {
                member.Team.Region.Name = value;
                OnPropertyChanged("RegionNameDB");
            }
        }

        public string TeamNameDB
        {
            get
            {

                if (member.Team != null)
                {
                    return member.Team.Name;
                }
                else
                    return "";
            }
            set
            {
                member.Team.Name = value;
                OnPropertyChanged("TeamNameDB");
            }
        }

        public string TrainerNameDB
        {
            get
            {

                if (member.Team != null)
                {
                    return member.Team.TrainerName;
                }
                else
                    return "";
            }
            set
            {
                member.Team.TrainerName = value;

                OnPropertyChanged("TrainerNameDB");
            }
        }


        public bool IsWiner
        {
            get { return isWiner; }
            set
            {
                isWiner = value;
                OnPropertyChanged("IsWiner");
            }
        }


        public string WeightCategory
        {
            get { return weightCategory; }
            set
            {
                weightCategory = value;
                OnPropertyChanged("WeightCategory");
            }
        }

        #endregion

        public MemberViewModel()
        {
            member = new Member();
        }

    }
}