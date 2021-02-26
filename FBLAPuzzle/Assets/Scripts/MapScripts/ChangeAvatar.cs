using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeAvatar : MonoBehaviour

{
    private Account activeAccount;
    // Start is called before the first frame update
    void Start()
    {
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeAvatarColor() {
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(true);
    }
    public void updateAvatarInScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        if (currentScene.name == "Map") {
            GameObject.Find("AvatarColor").GetComponent<Image>().color = activeAccount.avatarColor;
        }
        
    }
}
