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
    private bool opacityChangeActivated=false;
    Vector3 stageDimensions;
    //public GameObject 

    void Start()
    {
        accountManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        convertingScale = findConvertingScale();
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //StartCoroutine(startScroll());
        LoadingCanvas.SetActive(false);
        ps1 = partyPopper1.GetComponent<ParticleSystem>();
        ps2 = partyPopper2.GetComponent<ParticleSystem>();
        secondBackgroundColor = secondBackground.GetComponent<Image>().color;
        //creditText.transform.position = new Vector3(creditText.transform.position[0], -(stageDimensions[1]+10)*convertingScale, 0);
        creditText.transform.localPosition = new Vector3(0f, -1605.4f, 0f);
        //creditText.transform.position = new Vector3(0f, 13.52f, 0f);

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
            Debug.Log("pos <=0.05");
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
            Debug.Log("button true");
        }
        else
        {
            accountManager.activeAccount.endSceneActivated = true;
            accountManager.activeAccount.saveAccount();
            skipButton.SetActive(false);
            Debug.Log("button false");
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
        
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        yield return new WaitForSeconds(0.01f);
        if (creditText.transform.position.y < 24* convertingScale)
        {
            creditText.transform.localPosition = creditText.transform.localPosition + new Vector3(0f, 1.7f, 0f);
                //new Vector3(creditText.transform.localPosition.x, creditText.transform.localPosition.y + 0.5f, 0);
            Debug.Log(creditText.transform.position[1] + " scale "+ convertingScale+" height "+stageDimensions[1]);
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
