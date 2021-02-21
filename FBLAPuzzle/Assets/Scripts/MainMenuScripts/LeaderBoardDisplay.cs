using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    AccountsManager accountsManager;
    public GameObject[] rankTexts;
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
        accountsManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        List<string> names=new List<string>();
        List<int> stars = new List<int>();
        List<Account> highToLow = new List<Account>();
        for(int i = 0; i < accountsManager.accounts.Count; i++)
        {
            Account acc = accountsManager.accounts[i];
            if (i == 0)
            {
                highToLow.Add(acc);
            }
            else
            {
                highToLow.Add(acc);
                for(int j = i; j >= 1; j--)
                {
                    if (greaterThan(acc, highToLow[j - 1]))
                    {
                        Account tempAcc = highToLow[j - 1];
                        highToLow[j - 1] = acc;
                        highToLow[j] = tempAcc;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < highToLow.Count; i++) {
            string text;
            text = (i+1) + ". " + highToLow[i].userName;
            int tempTextLength = text.Length;
            
            //text = text + getTotalStar(highToLow[i]);
            rankTexts[i].GetComponent<TMPro.TextMeshProUGUI>().text= text;
            rankTexts[i].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text= getTotalStar(highToLow[i]).ToString();
        }
    }
    private int getTotalStar(Account acc)
    {
        int total=0;
        foreach (int score in acc.starsList)
        {
            if (score == -1)
            {
                break;
            }
            total += score;
        }
        return total;
    }
    private bool greaterThan(Account acc1, Account acc2)
    {
        int star1 = 0;
        int star2 = 0;
        foreach (int score in acc1.starsList)
        {
            if (score == -1)
            {
                break;
            }
            star1 += score;
        }
        foreach (int score in acc2.starsList)
        {
            if (score == -1)
            {
                break;
            }
            star2 += score;
        }
        return star1 > star2;
    }
}
