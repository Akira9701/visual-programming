using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace Calculator;

public class Calculator : INotifyPropertyChanged
{
    private double _prevValue = 0;
    private double _currValue = 0;

    private string _valueToShow = "0";

    private bool isEqualActive = false;
    
    private Operation _prevOperation = Operation.Result;

    public string ValueToShow
    {
        get 
        {
            return _valueToShow;
        }

        set 
        {
            _ = SetField(ref _valueToShow, value);
        }
    }
    
    public void EraseOneSymbol(object parameter)
    {
        if (ValueToShow.Length > 1)
            ValueToShow = ValueToShow.Substring(0, ValueToShow.Length - 1);
        else
            ValueToShow = "0";

        _prevValue = 0;
        _prevOperation = Operation.Result;
        _currValue = double.Parse(ValueToShow);
        isEqualActive = false;
    }

    public void EraseAllSymbols(object parameter)
    {
        _prevValue = 0;
        _currValue = 0;
        ValueToShow = "0";
        _prevOperation = Operation.Result;
        isEqualActive = false;
    }

    public void InsertSymbol(object parameter)
    {
        if (parameter is string text)
        {
            if (text.Equals(","))
            {
                if (ValueToShow.Contains(','))
                    return;

                _currValue = double.Parse(ValueToShow + ",0");
                ValueToShow += ",";
            }
            else
            {
                if (_currValue == 0 && !ValueToShow.Equals("0,"))
                    _currValue = double.Parse(text);
                else
                    _currValue = double.Parse(ValueToShow + text);

                ValueToShow = _currValue.ToString();
            }
        }
    }

    private int GetFactorial(int number)
    {
        int result = 1;
        for (int i = 1; i <= number; i++)
            result *= i;

        return result;
    }

    public void Execute(Operation operation)
    {
        double _valueToShow = double.Parse(ValueToShow);
        switch (operation)
        {
            case Operation.Add:
                _prevValue += _currValue;
                break;

            case Operation.Subtruct:
                _prevValue -= _currValue;
                break;

            case Operation.Multiply:
                if (_prevValue == 0)
                    _prevValue = 1;
                _prevValue *= _currValue;
                break;

            case Operation.Divide:
                _prevValue /= _currValue;
                break;

            case Operation.Modulo:
                _prevValue %= _currValue;
                break;

            case Operation.RaiseToPower:
                if (_prevValue == 0)
                    _prevValue = _currValue;
                else
                    _prevValue = Math.Pow(_prevValue, _currValue);
                break;

            case Operation.TakeDecLog:
                _prevValue = Math.Log10(_valueToShow);
                break;

            case Operation.TakeNatLog:
                _prevValue = Math.Log(_valueToShow);
                break;

            case Operation.TakeOpposite:
                _currValue = _valueToShow * -1;
                ValueToShow = _currValue.ToString();
                return;

            case Operation.TakeFactorial:
                if (ValueToShow.Contains(',') || _valueToShow < 0)
                    _prevValue /= 0;
                else
                    _prevValue = GetFactorial((int)_valueToShow);
                break;

            case Operation.TakeTangent:
                _prevValue = Math.Tan(_valueToShow);
                break;

            case Operation.TakeCosine:
                _prevValue = Math.Cos(_valueToShow);
                break;

            case Operation.TakeSine:
                _prevValue = Math.Sin(_valueToShow);
                break;

            case Operation.RoundUp:
                _prevValue = Math.Ceiling(_valueToShow);
                break;

            case Operation.RoundDown:
                _prevValue = Math.Floor(_valueToShow);
                break;

            case Operation.Result:
                _prevValue = _currValue;
                break;
        }

        if (double.IsInfinity(_prevValue))
            ValueToShow = "Ошибка";
        else
            ValueToShow = _prevValue.ToString();
    }

    public void Take(Operation operation)
    {
        if (operation != Operation.Result)
        {
            if (!isEqualActive)
                Execute(_prevOperation);
            else
                isEqualActive = false;

            _currValue = 0;
            _prevOperation = operation;
        }
        else
        {
            Execute(_prevOperation);
            isEqualActive = true;
        }
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