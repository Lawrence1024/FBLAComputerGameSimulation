using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    AccountsManager accountsManager;
    // Start is called before the first frame update
    void Start()
    {
        accountsManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getAccountsTotalStars() {
        foreach (Account acc in accountsManager.accounts)
        {
            int totalStar = 0;
            foreach(int score in acc.starsList)
            {
                if (score == -1)
                {
                    break;
                }
                totalStar += score;
            }
            Debug.Log("Account "+acc.userName+" has a total star of "+totalStar);
        }
    }
}
