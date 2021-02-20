using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountData
{
    public string userName;
    public string password;
    public int totalStar;
    public List<int> starsList;
    public List<int> potentialStarsList;
    public List<int> pointsList;

    public AccountData(Account account)
    {
        userName = account.userName;
        password = account.password;
        totalStar = account.totalStar;
        starsList = account.starsList;
        potentialStarsList = account.potentialStarsList;
        pointsList = account.pointsList;
    }
}
