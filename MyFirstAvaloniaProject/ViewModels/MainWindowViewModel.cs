using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MyFirstAvaloniaProject.Commands;
using ReactiveUI;

namespace MyFirstAvaloniaProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _greeting;
        public MainWindowViewModel()
        {
            OnClickMe = new RelayCommand(OnExecute, o => true);
            _greeting = "Welcome to Avalonia!";
        }

        public ICommand OnClickMe { get; set; }
        public string Greeting { get => _greeting; set => this.RaiseAndSetIfChanged(ref _greeting, value); }

        private void OnExecute(object? parameter)
        {
            Greeting = Greeting == "Welcome to Avalonia!" ? "You just clicked the button! Click to go back." : "Welcome to Avalonia!";
        }
    }
}