// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Runtime.CompilerServices;

// namespace Lecture_3;

// public class DataContextMa : INotifyPropertyChanged
// {
//     public string _text = "Khaki";

//     public string Text
//     {
//         get
//         {
//             return _text;
//         }

//         set
//         {
//             _text = value;
//             PropertyChangedEventHandler.?invoke()
//             SetField(ref _text, value);
//         }
//     }

//     public void ExecuteCommand(object parameter)
//     {
//         if (parameter is string text)
//         {
//             Text = text;
//         }
//     }



//     public event PropertyChangedEventHandler? PropertyChanged;

//     protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
//     {
//         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//     }

//     protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
//     {
//         if (EqualityComparer<T>.Default.Equals(field, value)) return false;
//         field = value;
//         OnPropertyChanged(propertyName);
//         return true;
//     }
// }

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lecture_3;

public class DataContextMa : INotifyPropertyChanged
{
    private string _text = "Hello World";

    public string Text
    {
        get
        {
            return _text;
        }

        set
        {
            _ = SetField(ref _text, value);
        }
    }

    public void ExecuteCommand(object parameter)
    {
        if (parameter is string text)
        {
            Text = text;
        }
    }

    public bool CanExecuteCommand(object parameter)
    {
        if (parameter is string text)
        {
            return text.StartsWith("Hello") == false;
        }

        return false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}