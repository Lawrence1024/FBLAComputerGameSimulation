﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AccountsManager : MonoBehaviour
{
    public List<Account> accounts;
    private int activeIndex=-1;
    public Account activeAccount=null;
    // Start is called before the first frame update
    void Start()
    {
        accounts = new List<Account>();
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
        {
            if(file.Length>8 && file.Substring(file.Length - 8, 8)==".account")
            {
                int nameLength = file.Length - Application.persistentDataPath.Length - 9;
                string accountName = file.Substring(Application.persistentDataPath.Length + 1, nameLength);
                Account tempAcc = new Account(accountName);
                accounts.Add(tempAcc);
                tempAcc.loadAccount();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool createAccount(string name)
    {
        bool find = checkIfAccountTaken(name);
        if (!find)
        {
            Account tempAcc = new Account(name);
            accounts.Add(tempAcc);
            tempAcc.saveAccount();
            activeAccount = tempAcc;
        }
        return !find;
    }
    public bool checkIfAccountTaken(string name)
    {
        for (int i = 0; i < accounts.Count; i++)
        {
            if (name.Equals(accounts[i].userName))
            {
                return true;
            }
        }
        return false;
    }
    public void loadAccount(GameObject textArea)
    {
        Account account = null;
        string name = textArea.GetComponent<TMPro.TextMeshProUGUI>().text;
        bool find = checkIfAccountExist(name);
        if (!find)
        {
           
        }
        else
        {
            account = accounts[activeIndex];
            activeAccount = accounts[activeIndex];
        }
    }
    public bool confirmLogin(string password)
    {
        if (activeAccount!=null && activeAccount.password.Equals(password))
        {
            return true;
        }
        activeAccount = null;
        activeIndex = -1;
        return false;
    }
    public bool checkIfAccountExist(string name)
    {
        bool find = false;
        //Why is there a empty space after the input text????
        name = name.Substring(0,name.Length-1);
        for(int i = 0; i < accounts.Count; i++)
        {
            if (name.Equals(accounts[i].userName))
            {
                find = true;
                activeIndex = i;
            }
        }
        return find;
    }
    public int lookUpAccountIndex()
    {
        return -1;
    }
    public bool accountGreaterThan(Account acc1, Account acc2)
    {
        return acc1.getTotalStar() > acc2.getTotalStar();
    }
}
