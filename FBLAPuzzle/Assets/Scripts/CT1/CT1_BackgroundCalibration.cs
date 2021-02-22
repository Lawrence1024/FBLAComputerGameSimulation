using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CT1_BackgroundCalibration : MonoBehaviour
{
    public GameObject MainBackground;
    public GameObject InstructionBackground;
    public GameObject LoadingBackground;
    public GameObject PauseMenuBackground;
    // Start is called before the first frame update
    void Start()
    {
    //    Debug.Log(Screen.width);
    //    Debug.Log(Screen.height);

        SetBackground(MainBackground);
        SetBackground(LoadingBackground);
        //MainBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        //MainBackground
    }

    // Update is called once per frame
    void Update()
    {
        //MainBackground
    }

    void SetBackground(GameObject backgroundObj) {
        SpriteRenderer sr = backgroundObj.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        transform.localScale = new Vector2(Screen.width / width, Screen.height / height);
    }
  
}
