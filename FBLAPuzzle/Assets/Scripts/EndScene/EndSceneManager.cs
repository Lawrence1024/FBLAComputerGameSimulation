using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndSceneManager : MonoBehaviour
{
    AccountsManager accountManager;
    public GameObject creditText;
    public GameObject partyPopper1;
    public GameObject partyPopper2;
    public GameObject secondBackground;
    public GameObject TheEndText;
    public GameObject LoadingCanvas;
    public GameObject skipButton;
    private Vector4 secondBackgroundColor;
    private float convertingScale;
    private bool opacityChangeActivated=false;
    Vector3 stageDimensions;

    void Start()
    {
        accountManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        convertingScale = findConvertingScale();
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        LoadingCanvas.SetActive(false);
        secondBackgroundColor = secondBackground.GetComponent<Image>().color;
        creditText.transform.localPosition = new Vector3(0f, -1605.4f, 0f);

        TheEndText.SetActive(false);

        setButton();

    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }
        creditText.transform.position = Vector3.MoveTowards(creditText.transform.position, new Vector3(0f, 13.62f*convertingScale, 0f), 5f* convertingScale * Time.deltaTime);
        if (Mathf.Abs(creditText.transform.position.y- 13.62f *convertingScale)<=0.05) {
            if (!opacityChangeActivated) {
                opacityChangeActivated = true;
                StartCoroutine(decreaseOpacity());

            }
        }
    }
    public void setButton() {
        if (accountManager.activeAccount.endSceneActivated)
        {
            skipButton.SetActive(true);
        }
        else
        {
            accountManager.activeAccount.endSceneActivated = true;
            accountManager.activeAccount.saveAccount();
            skipButton.SetActive(false);
        }
    }
    public float findConvertingScale()
    {
        float initialX = creditText.transform.position.x;
        creditText.transform.localPosition += new Vector3(107.8949f, 0f, 0f);
        float finalX = creditText.transform.position.x;
        creditText.transform.localPosition += new Vector3(-107.8949f, 0f, 0f);
        return finalX - initialX;
    }
    
    IEnumerator decreaseOpacity() {
        yield return new WaitForSeconds(0.1f);
        if (secondBackgroundColor[3]<1) {
            secondBackground.GetComponent<Image>().color = new Vector4(secondBackgroundColor[0], secondBackgroundColor[1], secondBackgroundColor[2], secondBackgroundColor[3] + 0.1f);
            secondBackgroundColor = secondBackground.GetComponent<Image>().color;
            StartCoroutine(decreaseOpacity());
        }
        else
        {
            StartCoroutine(displayTheEnd());
        }
        
    }
    IEnumerator displayTheEnd() {
        TheEndText.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
    public void skip() {
        LoadingCanvas.SetActive(true);
        LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
    }
}
