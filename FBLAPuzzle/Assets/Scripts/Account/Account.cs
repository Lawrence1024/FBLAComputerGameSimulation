using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    //fields
    public string userName;
    public string password;
    public int totalStar;
    public List<int> starsList;
    public List<int> potentialStarsList;
    public List<int> pointsList;
    //constructor
    public Account(string name)
    {
        userName = name;
        password = "lol";
        totalStar = 0;
        starsList = new List<int> { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1 };
        potentialStarsList = new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        pointsList = new List<int> { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1 };
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
        //Debug.Log(data.userName);
        userName = data.userName;
        password = data.password;
        //Debug.Log("loadAccount password of account \""+userName+"\": " + data.password);
        totalStar = data.totalStar;
        starsList = data.starsList;
        potentialStarsList = data.potentialStarsList;
        pointsList = data.pointsList;
    }
    //private method


}
