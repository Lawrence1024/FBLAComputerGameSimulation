﻿//FileName: AccountData.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: AccountData will contain information of Account in a format that could be converted into a binary file.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountData
{
    //fields
    public string userName;
    public string password;
    public int totalStar;
    public List<int> starsList;
    public List<int> potentialStarsList;
    public List<int> pointsList;
    public List<float> avatarColor;
    public List<bool> tutorialProgress;
    public List<int> tutorialFeatures;
    public bool endSceneActivated;
    //constructor
    public AccountData(Account account)
    {
        userName = account.userName;
        password = account.password;
        totalStar = account.totalStar;
        starsList = account.starsList;
        potentialStarsList = account.potentialStarsList;
        pointsList = account.pointsList;
        avatarColor = new List<float>();
        avatarColor.Add(account.avatarColor.x);
        avatarColor.Add(account.avatarColor.y);
        avatarColor.Add(account.avatarColor.z);
        avatarColor.Add(account.avatarColor.w);
        tutorialProgress = account.tutorialProgress;
        tutorialFeatures = account.tutorialFeatures;
        endSceneActivated = account.endSceneActivated;
    }
}
