using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAvatar : MonoBehaviour
{
    private Vector4 avaColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chooseColor(GameObject userColor) {
        if (userColor.name == "Red") {
            Debug.Log("color red");
            //rgba
            avaColor = new Vector4(1, 0.24f, 0.24f, 1);
        } else if (userColor.name=="Orange") {
            //avaColor=
            avaColor = new Vector4(1, 0.35f, 0, 1);
        }
        else if (userColor.name == "Yellow")
        {
            avaColor = new Vector4(1, 1, 0, 1);
        }
        else if (userColor.name == "Green")
        {
            avaColor = new Vector4(0.29f, 1, 0, 1);

        }
        else if (userColor.name == "Blue")
        {
            avaColor = new Vector4(0, 0.53f, 1, 1);

        }
        else if (userColor.name == "Purple")
        {
            avaColor = new Vector4(1, 0.24f, 0.67f, 1);

        }
        else if (userColor.name == "White")
        {
            avaColor = new Vector4(1, 1, 1, 1);

        }
    }
    public void inactiveAvatarBoard(GameObject avatarBoard) {
        avatarBoard.SetActive(false);
    }
}
