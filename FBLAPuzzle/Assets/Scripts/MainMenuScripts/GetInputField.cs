using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GetInputField : MonoBehaviour
{
    // Start is called before the first frame update
    MainMenuManager mainMenuManager;
    public bool accountTaken=false;
    public bool accountExist = false;
    public GameObject accManagerObj;
    private AccountsManager accManager;
    private string warningNote = "";
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
                Debug.Log(accManager.activeAccount.password);
                //get the account info and continue
                //mainMenuManager.UserNameInputBoxCanvas.SetActive(false);

            }
            else
            {
                
                warningNote = "Account does not exist! Please create a new account";
                StartCoroutine(displayWarning());
            }
        }
    }

    public void GetPassword(GameObject userPassword) {
        string password = userPassword.GetComponent<TMPro.TextMeshProUGUI>().text;
        //Get the username bank and check if there's duplicate, if account already exist, accountTaken=true;
        if (string.IsNullOrEmpty(password) || password.Length == 1)
        {
            if (warningNote!= "Account does not exist! Please create a new account") {
                warningNote = "Incorrect Password!";
                StartCoroutine(displayWarning());
            }
            
            return;
        }
        else
        {
            password = password.Substring(0, password.Length - 1);
            bool successLogin = accManager.confirmLogin(password);
            if (!successLogin)
            {
                if (warningNote != "Account does not exist! Please create a new account")
                {
                    warningNote = "Incorrect Password!";
                    StartCoroutine(displayWarning());
                }
            }
            else
            {
                warningNote = "Sign in Successfully!";
                StartCoroutine(displayWarning());
            }
        }
    }

    public void createAccount(GameObject UserNameInputBoxCanvas)
    {
        UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(true);
        UserNameInputBoxCanvas.transform.GetChild(0).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);

    }
    //enteredCreatedName() is for creating new account
    public void enteredCreateName(GameObject textObj) {
        string name = textObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        //Get the username bank and check if there's duplicate, if account already exist, accountTaken=true;
        if (string.IsNullOrEmpty(name) || name.Length == 1)
        {
            return;
        }
        else if (!seeIfEnteredCreatedPassword(GameObject.Find("PasswordText"))) {
            warningNote = "Please enter a password!";
            StartCoroutine(displayWarning());
        }
        else {
            name = name.Substring(0, name.Length - 1);
            if (name.Length > 17)
            {
                warningNote = "Username too long!";
                StartCoroutine(displayWarning());
            }
            else
            {
                bool successCreate = accManager.createAccount(name);
                if (!successCreate)
                {
                    warningNote = "Username already taken! Choose another username";
                    StartCoroutine(displayWarning());
                }
                else
                {
                    warningNote = "Account Created Successfully!";
                    StartCoroutine(displayWarning());



                }
            }
        }  
    }

    public void enterCreatedPassword(GameObject textObj) {
        string pwd = textObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(pwd) || pwd.Length == 1)
        {
            return;
        }
        else {
            pwd = pwd.Substring(0, pwd.Length - 1);
            if (!(accManager.activeAccount == null))
            {
                accManager.activeAccount.password = pwd;
                accManager.activeAccount.saveAccount();
                //accManager.activeAccount = null;
            }
        }
    }
    public bool seeIfEnteredCreatedPassword(GameObject textObj) {
        string pwd = textObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(pwd) || pwd.Length == 1)
        {
            return false;
        }
        return true;
    }
    public void resetInputField(GameObject inputField) {
        TMP_InputField mainInputField;
        mainInputField=inputField.GetComponent<TMP_InputField>();
        mainInputField.text = "";
    }
    IEnumerator displayWarning() {
        GameObject tempObj = mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(3).gameObject;
        tempObj.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text= warningNote;
        tempObj.SetActive(true);
        for (int i = 0; i < mainMenuManager.buttonsToDisableOnWarning.Length; i++)
        {
            mainMenuManager.buttonsToDisableOnWarning[i].GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1f);
        tempObj.SetActive(false);
        
        if (warningNote == "Sign in Successfully!")
        {
            mainMenuManager.UserNameInputBoxCanvas.SetActive(false);
            GameObject.Find("MainPageCanvas").transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (warningNote == "Account Created Successfully!") {
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(false);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(0).gameObject.SetActive(true);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(4).gameObject.SetActive(true);
        }
        for (int i = 0; i < mainMenuManager.buttonsToDisableOnWarning.Length; i++)
        {
            
            mainMenuManager.buttonsToDisableOnWarning[i].GetComponent<Button>().interactable = true;
        }
        warningNote = "";
    }
}
