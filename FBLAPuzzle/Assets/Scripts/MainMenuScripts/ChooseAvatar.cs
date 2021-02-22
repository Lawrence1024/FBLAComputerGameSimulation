using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAvatar : MonoBehaviour
{
    private Vector4 avaColor;
    public AccountsManager accManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chooseColor(GameObject userColor) {
        if (accManager.activeAccount == null)
        {
            Debug.Log("Error in ChooseAvatar Script chooseColor function: accManager.activeAccount==null");
            return;
        }
        if (userColor.name == "Red") {
            Debug.Log("color red");
            Debug.Log(accManager.activeAccount.userName);
            //rgba
            accManager.activeAccount.avatarColor = new Vector4(1, 0.24f, 0.24f, 1);
        } else if (userColor.name=="Orange") {
            //avaColor=
            accManager.activeAccount.avatarColor = new Vector4(1, 0.35f, 0, 1);
        }
        else if (userColor.name == "Yellow")
        {
            accManager.activeAccount.avatarColor = new Vector4(1, 1, 0, 1);
        }
        else if (userColor.name == "Green")
        {
            accManager.activeAccount.avatarColor = new Vector4(0.29f, 1, 0, 1);

        }
        else if (userColor.name == "Blue")
        {
            accManager.activeAccount.avatarColor = new Vector4(0, 0.53f, 1, 1);

        }
        else if (userColor.name == "Purple")
        {
            accManager.activeAccount.avatarColor = new Vector4(1, 0.24f, 0.67f, 1);

        }
        else if (userColor.name == "White")
        {
            accManager.activeAccount.avatarColor = new Vector4(1, 1, 1, 1);
        }
        accManager.activeAccount.saveAccount();
        accManager.activeAccount = null;
    }
    public void inactiveAvatarBoard(GameObject avatarBoard) {
        avatarBoard.SetActive(false);
    }
}
