using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GetInputField : MonoBehaviour
{
    // Start is called before the first frame update
    MainMenuManager mainMenuManager;
    public bool accountTaken=false;
    public bool accountExist = false;
    public GameObject accManagerObj;
    private AccountsManager accManager;
    /*
        Children for UserNameInputBoxCanvas
        0. Login
        1. Create Account Hyperlink
        2. Create Account Canvas
        3. Warning for login but account doesn't exist
        4. Warning for creating account but already exist
        5. Account create successfully sign
        6. Wrong password sign
     */
    void Start()
    {
        mainMenuManager = GameObject.Find("SceneManager").GetComponent<MainMenuManager>();
        accManager = accManagerObj.GetComponent<AccountsManager>();
    }

    void Update()
    {
        
    }
    //GetName() is for Login (assume account already created)
    public void GetName(GameObject userName)
    {
        Debug.Log("GetName");
        string inputText = userName.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(inputText) || inputText.Length==1)
        {
            return;
        }
        else {
            //get the user information, if doesn't exist call displayWarning(3 for account not exist, 4 for account already exist)
            //if there is such account, account = true, else false
            accManager.loadAccount(userName);
            accountExist = accManager.checkIfAccountExist(inputText);
            if (accountExist)
            {
                //get the account info and continue
                //mainMenuManager.UserNameInputBoxCanvas.SetActive(false);

            }
            else
            {
                StartCoroutine(displayWarning(3));
            }
        }
    }

    public void GetPassword(GameObject userPassword) {
        Debug.Log("GetPassword");
        string password = userPassword.GetComponent<TMPro.TextMeshProUGUI>().text;
        //Get the username bank and check if there's duplicate, if account already exist, accountTaken=true;
        if (string.IsNullOrEmpty(password) || password.Length == 1)
        {
            StartCoroutine(displayWarning(6));
            return;
        }
        else
        {
            password = password.Substring(0, password.Length - 1);
            bool successLogin = accManager.confirmLogin(password);
            if (!successLogin)
            {
                Debug.Log("asdfasdfasdf");
                StartCoroutine(displayWarning(6));
            }
            else
            {
                StartCoroutine(displayWarning(5));
                mainMenuManager.UserNameInputBoxCanvas.SetActive(false);
            }
        }
    }

    //enteredCreatedName() is for creating new account
    public void enteredCreateName(GameObject textObj) {
        string name = textObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        //Get the username bank and check if there's duplicate, if account already exist, accountTaken=true;
        if (string.IsNullOrEmpty(name) || name.Length == 1)
        {
            return;
        }
        else {
            name = name.Substring(0, name.Length - 1);
            bool successCreate=accManager.createAccount(name);
            if (!successCreate)
            {
                StartCoroutine(displayWarning(4));
            }
            else
            {
                StartCoroutine(displayWarning(5));
                
            }
        }  
        
        
    }

    //displayWarning() takes in the display warning image (child number)
    IEnumerator displayWarning(int childNum) {
        mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(childNum).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(childNum).gameObject.SetActive(false);
        if (childNum==5) {
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(false);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(0).gameObject.SetActive(true);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
