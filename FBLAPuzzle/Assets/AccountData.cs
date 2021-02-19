using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountData
{
    public string userName;
    public int totalStar;
    public List<int> starsList;
    public List<int> pointsList;

    public AccountData(Account account)
    {
        userName = account.userName;
        totalStar = account.totalStar;
        starsList = account.starsList;
        pointsList = account.pointsList;
    }
}
