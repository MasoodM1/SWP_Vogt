using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;

namespace MAUIApp.ViewModels
{
    // VM-Klassen müssen von ObservableObject erben => wird für DataBinding (Observer-Pattern) und Commands benötigt
    // die Klasse muss partial sein, damit der Compiler (MVVM-Toolkit) unsere Klasse ergänzen kann
    partial class RegisterPageViewModel : ObservableObject
    {
        // Annotation [ObservableProperty] erzeugt aus dem angegebenen Feld _firstname das Property Firstname inkl. Observer-Pattern
        [ObservableProperty]
        private string _firstname;

        [ObservableProperty] 
        private string _lastname;

        [ObservableProperty]
        private DateTime _birthdate;

        [ObservableProperty]
        private string _street;

        [ObservableProperty]
        private string _number;

        [ObservableProperty]
        private string _postalCode;

        [ObservableProperty]
        private string _city;


        [RelayCommand(CanExecute =nameof(CanSaveUserData))]
        public async Task SaveUserData()
        {
            // in der DB abspeichern
        }

        private bool CanSaveUserData()
        {
            // hier kommt die Überprüfung der Felder hin
            return true;
        }

        [RelayCommand]
        public void ResetRegistrationForm()
        {
            Firstname = "";
            Lastname = "";
            Birthdate = DateTime.Now;
            Street = "";
            Number = "";
            PostalCode = "";
            City = "";
        }

        [RelayCommand]
        public async Task ChangeToMainPage()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

    }
}
