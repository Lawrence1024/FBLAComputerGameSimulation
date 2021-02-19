using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    //fields
    public string userName;
    public int totalStar;
    public List<int> starsList;
    public List<int> pointsList;
    //constructor
    public Account(string name)
    {
        userName = name;
        totalStar = 0;
        starsList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        pointsList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    //setter/getter

    //public method
    public void saveAccount()
    {
        Debug.Log("The Account is saved");
        SaveSystem.saveAccount(this);
    }
    public void loadAccount()
    {
        AccountData data = SaveSystem.loadAccount(this);
        Debug.Log(data.userName);
        userName = data.userName;
        totalStar = data.totalStar;
        starsList = data.starsList;
        pointsList = data.pointsList;
    }
    //private method


}
