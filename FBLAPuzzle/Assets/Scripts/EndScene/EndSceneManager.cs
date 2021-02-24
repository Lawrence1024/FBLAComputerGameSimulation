using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndSceneManager : MonoBehaviour
{
    AccountsManager accountManager;
    // Start is called before the first frame update
    public GameObject creditText;
    public GameObject partyPopper1;
    public GameObject partyPopper2;
    public GameObject secondBackground;
    public GameObject TheEndText;
    public GameObject LoadingCanvas;
    public GameObject skipButton;
    private ParticleSystem ps1;
    private ParticleSystem ps2;
    private Vector4 secondBackgroundColor;
    private float convertingScale;

    //public GameObject 

    void Start()
    {
        accountManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        convertingScale = findConvertingScale();
        Debug.Log(convertingScale);
        StartCoroutine(startScroll());
        LoadingCanvas.SetActive(false);
        ps1 = partyPopper1.GetComponent<ParticleSystem>();
        ps2 = partyPopper2.GetComponent<ParticleSystem>();
        secondBackgroundColor = secondBackground.GetComponent<Image>().color;
        TheEndText.SetActive(false);
        if (accountManager.activeAccount.endSceneActivated) {
            skipButton.SetActive(true);
        }
        else
        {
            accountManager.activeAccount.endSceneActivated = true;
            skipButton.SetActive(false);

        }

    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            Application.Quit();
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
    void dimScene() { 
        
    }
    IEnumerator startScroll() {
        if (accountManager.activeAccount.endSceneActivated)
        {
            skipButton.SetActive(true);
        }
        else
        {
            accountManager.activeAccount.endSceneActivated = true;
            skipButton.SetActive(false);

        }
        yield return new WaitForSeconds(0.00005f);
        if (creditText.transform.position[1] < 14* convertingScale)
        {
            creditText.transform.position = new Vector3(creditText.transform.position[0], creditText.transform.position[1] + 0.005f, 0);
            Debug.Log("y pos < 100 "+ creditText.transform.position[1]);
            StartCoroutine(startScroll());
        }
        else {
            StartCoroutine(decreaseOpacity());
        }
        
    }
    IEnumerator decreaseOpacity() {
        yield return new WaitForSeconds(0.1f);
        
        //        secondBackgroundColor = secondBackground.GetComponent<Image>().color;
        if (secondBackgroundColor[3]<1) {
            secondBackground.GetComponent<Image>().color = new Vector4(secondBackgroundColor[0], secondBackgroundColor[1], secondBackgroundColor[2], secondBackgroundColor[3] + 0.05f);
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
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
    public void skip() {
        LoadingCanvas.SetActive(true);
        LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
    }
}
