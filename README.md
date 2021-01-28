# Avalonia Tutorial

### Install Avalonia Template
Follow the instructions on the [Avalonia Dotnet Template](https://github.com/AvaloniaUI/avalonia-dotnet-templates) page.

### Rider Plugin Installation 
When I searched for the AvaloniaRider plugin in the IDE,
it didn't work for me. So I manually installed the plugin.
To install the plugin manually, I downloaded the latest version
from the [JetBrains marketplace](https://plugins.jetbrains.com/plugin/14839-avaloniarider/versions).
The Rider IDE has a nice Avalonia UI preview.

### Visual Studio
Install the [Visual Studio Plugin](https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaforVisualStudio)

### VS Code
If you do not have access to the Rider or a Visual Studio (on Linux), 
you can always use VS Code. You can still create and run the project. 
I will show you how below.

### My First Avalonia Project.
We will write a simple Avalonia UI with a button and a text block. We will change the text on the text block when a user clicks the button.

Create a new project using the command mentioned below.

`dotnet new avalonia.mvvm -o MyFirstAvaloniaProject`

Open the project folder in your preferred IDE. Open file `Views -> MainWindow.axaml`.

Create a `Grid` and define two columns.

```xaml
    <Grid ColumnDefinitions="*,*">
    </Grid>
```

Add a `Button` in the center of column one of the `Grid`. 
```xaml
    <Button 
        Grid.Column="0" 
        HorizontalAlignment="Center" 
        VerticalAlignment="Center">Click Me!</Button>
```
Add a `TextBlock` in the center of column two of the `Grid`.
```xaml
    <TextBlock Grid.Column="1" 
        HorizontalAlignment="Center" VerticalAlignment="Center"/>
```

Now open the `ViewModels -> MainWindowViewModel.cs` file.
Change the existing property `Greeting` to the code below. The `this.RaiseAndSetIfChanged` will apply the updated value of the `Greeting` property to the `TextBlock` in the `MainWindow.axaml`.

```C#
	private string _greeting;
	public string Greeting { get => _greeting; set => this.RaiseAndSetIfChanged(ref _greeting, value); }
```

Now create a new folder named `Commands` under the root project folder. 
Create a new class called `RelayCommand` that implements 
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.icommand?view=net-5.0) interface.

Create two readonly delegate variables in the `RelayCommand` class and initialize them in the constructor.

```c#
private readonly Action<object?> _execute;
private readonly Predicate<object?> _canExecute;

public RelayCommand(Action<object?> execute, Predicate<object?> canExecute)
{
    _execute = execute;
    _canExecute = canExecute;
}
```

Implement [ICommand](https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.icommand?view=net-5.0) interface methods and a property.

As the name suggests, call the `_canExecute` in the method `CanExecute`, and `_execute` in the `Execute` method.

```C#
public bool CanExecute(object? parameter)
{
    return _canExecute(parameter);
}

public void Execute(object? parameter)
{
    _execute(parameter);
}
```

Go back to the `MainWindowViewModel.cs` and create a new property, `OnClickMe`.

```C#
public ICommand OnClickMe { get; set; }
```

Define the method `OnExecuteButtonClickEvent`. Here we will execute logic to change the value of the `Greeting` property.

```C#
private void OnExecuteButtonClickEvent(object? parameter)
{
    Greeting = Greeting == "Welcome to Avalonia!" ? "You just clicked the button! Click again to go back." : "Welcome to Avalonia!";
}

```

Initialize the variable `_greeting` and the property `OnClickMe` in the constructor.

```C#
public MainWindowViewModel()
{
    OnClickMe = new RelayCommand(OnExecuteButtonClickEvent, o => true);
    _greeting = "Welcome to Avalonia!";
}
```


Bind the `Greeting` property to the `TextBlock` we created in the `MainWindow.axaml`
by adding the attribute below to the `TextBlock` tag.

```xaml
	Text="{Binding Greeting}" 
```

Bind the `OnClickMe` property to the `Button` we created in the `MainWindow.axaml` 
by adding the attribute below to the `Button` tag. 

```xaml
Command="{Binding OnClickMe}"
```

The binding works because the `window` tag in the `MainWindow.axaml`
contains the attribute `x:Class="MyFirstAvaloniaProject.Views.MainWindow"`

To run the project, go to the root project folder on terminal and type the command below.

```
dotnet run
```


