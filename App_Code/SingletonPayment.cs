using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SingletonPayment
/// </summary>
public class SingletonPayment
{
    public static SingletonPayment _Instance;
    public SingletonPayment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static SingletonPayment GetInstance()
    {
        if (_Instance == null)
            return _Instance = new SingletonPayment();
        else
            return _Instance;
    }

    private int _UserId;

    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }


    private int _PaymentInfoId;

    public int PaymentInfoId
    {
        get { return _PaymentInfoId; }
        set { _PaymentInfoId = value; }
    }

    private int _SubscriptionId;

    public int SubscriptionId
    {
        get { return _SubscriptionId; }
        set { _SubscriptionId = value; }
    }

    private string _TransactionAmount;

    public string TransactionAmount
    {
        get { return _TransactionAmount; }
        set { _TransactionAmount = value; }
    }

    private int _MonthsToAdd;

    public int MonthsToAdd
    {
        get { return _MonthsToAdd; }
        set { _MonthsToAdd = value; }
    }

    private string _Name;

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    private string _Description;

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }

    private string _AccountType;

    public string AccountType
    {
        get { return _AccountType; }
        set { _AccountType = value; }
    }
}