using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScoreBoardData : MonoBehaviour
{
    AccountsManager accountsManager;
    public GameObject[] rankTexts;
    public GameObject[] partyPoppers;
    void Start()
    {
        accountsManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getAccountsPoints(List<int> levelArr)
    {
        changePartyPopperStatus(false);
        int levelValue = levelArr[0] * 3 + levelArr[1] - 4;
        //ctrl c ctrl v
        accountsManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        //List<string> names = new List<string>();
        //List<int> points = new List<int>();
        List<Account> highToLow = new List<Account>();
        for (int i = 0; i < accountsManager.accounts.Count; i++)
        {
            Account acc = accountsManager.accounts[i];
            if (i == 0)
            {
                highToLow.Add(acc);
            }
            else if (acc.pointsList[levelValue]!=-1)
            {
                for(int j = 0; j < highToLow.Count; j++)
                {
                    if (acc.pointsList[levelValue] > highToLow[j].pointsList[levelValue])
                    {
                        highToLow.Insert(j, acc);
                        break;
                    }
                    if (j == highToLow.Count - 1)
                    {
                        highToLow.Add(acc);
                        break;
                    }
                }
            }
        }
        if (highToLow.Count < rankTexts.Length)
        {
            for (int i = 0; i < highToLow.Count; i++)
            {
                string text;
                text = (i + 1) + ". " + highToLow[i].userName;
                int tempTextLength = text.Length;

                //text = text + getTotalStar(highToLow[i]);
                rankTexts[i].GetComponent<TMPro.TextMeshProUGUI>().text = text;
                rankTexts[i].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = highToLow[i].pointsList[levelValue].ToString();
            }
        }
        else
        {
            for (int i = 0; i < rankTexts.Length; i++)
            {
                string text;
                text = (i + 1) + ". " + highToLow[i].userName;
                int tempTextLength = text.Length;

                //text = text + getTotalStar(highToLow[i]);
                rankTexts[i].GetComponent<TMPro.TextMeshProUGUI>().text = text;
//hard code -1 points (not yet)
                rankTexts[i].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = (highToLow[i].pointsList[levelValue]).ToString();
            }
        }
        for (int i = 0; i < highToLow.Count; i++) {
        //&&highToLow[i].pointsList[levelValue]== int.Parse(GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text)
            if (highToLow[i].userName == accountsManager.activeAccount.userName) {
                GameObject.Find("Rank").GetComponent<TMPro.TextMeshProUGUI>().text = "Highest Rank: " + (i + 1);
                if (highToLow[i].pointsList[levelValue]<= int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text)&&i<=5) {
                    changePartyPopperStatus(true);
                }
                break;
            }
            else{

                GameObject.Find("Rank").GetComponent<TMPro.TextMeshProUGUI>().text = "Highest Rank: No Rank";
            }
        }
    }

    void changePartyPopperStatus(bool status)
    {
        for (int i = 0; i < partyPoppers.Length; i++)
        {
            partyPoppers[i].SetActive(status);
        }
    }
}
