using System.Collections.ObjectModel;
using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugReproduce;

internal partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        Items = new ObservableCollection<int> { 1, 2, 3, 4 };
    }

    public ObservableCollection<int> Items { get; }

    // Exception thrown: 'System.InvalidOperationException' in WinCopies.Util.Desktop.dll
    // An unhandled exception of type 'System.InvalidOperationException' occurred in WinCopies.Util.Desktop.dll
    [RelayCommand]
    private void Click(object obj)
    {
        MessageBox.Show($"Click {obj}.");
    }

    [RelayCommand(CanExecute = nameof(CanFill))]
    private void Fill()
    {
        Items.Add(1);
        Items.Add(2);
        Items.Add(3);
        Items.Add(4);
        Items.Add(5);
        Items.Add(6);

        FillCommand.NotifyCanExecuteChanged();
        ClearCommand.NotifyCanExecuteChanged();
    }

    private bool CanFill()
    {
        return Items.Count == 0;
    }

    [RelayCommand(CanExecute = nameof(CanClear))]
    private void Clear()
    {
        Items.Clear();

        FillCommand.NotifyCanExecuteChanged();
        ClearCommand.NotifyCanExecuteChanged();
    }

    private bool CanClear()
    {
        return Items.Count > 0;
    }
}
