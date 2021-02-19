using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AccountsManager : MonoBehaviour
{
    List<Account> accounts;
    // Start is called before the first frame update
    void Start()
    {
        accounts = new List<Account>();
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
        {
            int nameLength = file.Length - Application.persistentDataPath.Length - 9;
            //Debug.Log(Application.persistentDataPath.Length);
            //Debug.Log(file.Substring(Application.persistentDataPath.Length+1, nameLength));
            string accountName = file.Substring(Application.persistentDataPath.Length + 1, nameLength);
            Account tempAcc = new Account(accountName);
            accounts.Add(tempAcc);
            tempAcc.loadAccount();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createAccount(string name)
    {
        bool find = false;
        foreach (Account acc in accounts)
        {
            if (name == acc.userName)
            {
                find = true;
                Debug.Log("The username is already taken!");
            }
        }
        if (!find)
        {
            Account tempAcc = new Account(name);
            accounts.Add(tempAcc);
            tempAcc.saveAccount();
        }
    }
    public void loadAccount(string name)
    {
        bool find = false;
        Account account=null;
        foreach(Account acc in accounts)
        {
            if (name == acc.userName)
            {
                find = true;
                account = acc;
            }
        }
        if (!find)
        {
            Debug.Log("You don't have an account yet");
        }
        if (find)
        {
            Debug.Log(account.userName);
            foreach(int i in account.pointsList)
            {
                Debug.Log(i);
            }
        }
    }
}
