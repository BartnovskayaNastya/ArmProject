using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArmBazaProject.Models;
using System.Windows.Input;

namespace ArmBazaProject.ViewModels
{
    public delegate void ToureHandler(ToureViewModel toure);

    public class ToureViewModel : ViewModelBase
    {
        public event ToureHandler buttonPressedEvent = null;

        private Toure toure;
        public bool ifExtra = false;
        string name;
        bool isVisisble = false;
        MemberViewModel someMember;

        public ICommand ToureCommand { set; get; }

        public Toure Toure
        {
            get { return toure; }
            set
            {
                if (toure != value)
                {
                    toure = value;
                    OnPropertyChanged("Toure");
                }
            }
        }

        public string Name
        {
            get { return name + " ТУР"; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public bool IsVisible
        {
            get { return isVisisble; }
            set
            {
                isVisisble = value;
                OnPropertyChanged("IsVisible");
            }
        }

        public ToureViewModel()
        {

            ToureCommand = new DelegateCommand(ExecuteToure, CanExecute);
            toure = new Toure();
        }

        private bool CanExecute(object arg)
        {
            if (isVisisble)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ExecuteToure(object param)
        {
            ButtonPressedEvent();
        }

        public void ButtonPressedEvent()
        {
            if (buttonPressedEvent != null)
                buttonPressedEvent.Invoke(this);
        }

        //сортировка 1 тура
        public void SortFirstToure()
        {
            foreach (MemberViewModel member in toure.ToureMembers)
            {
                someMember = new MemberViewModel();
                someMember = (MemberViewModel)member.Clone();
                if (member.IsWiner)
                {
                    someMember.IsWiner = false;
                    toure.ToureMembersA.Add(someMember);

                }
                else
                {
                    someMember.LoseCounter++;
                    toure.ToureMembersB.Add(someMember);
                }
            }
        }

    }
}
