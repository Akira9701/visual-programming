using System;
using System.Collections.Generic;

// Интерфейс для уведомлений
interface INotifyer
{
    void Notify(decimal balance);
}

class Account
{
    private decimal _balance;
    private List<INotifyer> _Notifyers;

    public Account(decimal initialBalance)
    {
        _balance = initialBalance;
        _Notifyers = new List<INotifyer>();
    }

    public Account()
    {
        _balance = 0;
        _Notifyers = new List<INotifyer>();
    }

    public void AddNotifyer(INotifyer Notifyer)
    {
        _Notifyers.Add(Notifyer);
    }

    public void ChangeBalance(decimal value)
    {
        _balance = value;
        Notification();
    }

    public decimal Balance
    {
        get
        {
            return _balance;
        }
    }

    private void Notification()
    {
        foreach (var Notifyer in _Notifyers)
        {
            Notifyer.Notify(_balance);
        }
    }
}

class SMSLowBalanceNotifyer : INotifyer
{
    private string _phone;
    private decimal _LowBalanceValue;

    public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue)
    {                                                                  
        _phone = phone;
        _LowBalanceValue = lowBalanceValue;
    }

    public void Notify(decimal balance)
    {
        if (balance < _LowBalanceValue)
        {
            Console.WriteLine($"SMSLowBalanceNotifyer:\nNotifying user with phone - {_phone}.\nLow balance detected - {balance}\n");
        }
    }
}


class EMailBalanceChangedNotifyer : INotifyer
{
    private string _email;

    public EMailBalanceChangedNotifyer(string email)
    {
        _email = email;
    }

    public void Notify(decimal balance)
    {
        Console.WriteLine($"EMailBalanceChangedNotifyer:\nNotifying user with email - {_email}.\nNew balance - {balance}\n");
    }
}

class Program
{
    static void Main()
    {
        decimal lowbalance = 349;

        Account userAccount = new Account(2000); 


        SMSLowBalanceNotifyer smsNotifyer = new SMSLowBalanceNotifyer("89538804243", lowbalance);
        EMailBalanceChangedNotifyer emailNotifyer = new EMailBalanceChangedNotifyer("akira.zzz97@gmail.com");


        userAccount.AddNotifyer(smsNotifyer);
        userAccount.AddNotifyer(emailNotifyer);
            
        userAccount.ChangeBalance(1300);
        Console.WriteLine("\n\n");
        userAccount.ChangeBalance(100);
    }
}
