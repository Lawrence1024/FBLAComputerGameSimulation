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
    public Vector4 avatarColor;
    public List<bool> tutorialProgress;
    //constructor
    public Account(string name)
    {
        userName = name;
        password = "lol";
        totalStar = 0;
        starsList = new List<int> { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1 };
        potentialStarsList = new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        pointsList = new List<int> { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1 };
        avatarColor = new Vector4(1f, 1f, 1f, 1f);
        tutorialProgress = new List<bool> { false, false };
    }
    //setter/getter

    //public method
    public void saveAccount()
    {
        Debug.Log("The Account \""+userName+"\" is saved");
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
        avatarColor = new Vector4(data.avatarColor[0], data.avatarColor[1], data.avatarColor[2], data.avatarColor[3]);
        tutorialProgress = data.tutorialProgress;
    }
    public int getTotalStar()
    {
        int total = 0;
        foreach (int score in starsList)
        {
            if (score == -1)
            {
                break;
            }
            total += score;
        }
        return total;
    }
    //private method


}
