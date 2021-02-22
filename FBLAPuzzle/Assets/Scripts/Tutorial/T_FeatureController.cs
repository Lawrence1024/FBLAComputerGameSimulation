using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_FeatureController : MonoBehaviour
{
    public GameObject TCanvas;
    public T_TutorialFlowController TFController;
    public GameObject backBut;
    public GameObject resetBut;
    // Start is called before the first frame update
    void Start()
    {
        TFController = TCanvas.GetComponentInChildren<T_TutorialFlowController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TFController.currentStep < 22 && (backBut.GetComponent<Button>().interactable == true || resetBut.GetComponent<Button>().interactable == true))
        {
            deactivateButtons();
        }
        if (TFController.currentStep == 22)
        {
            backBut.GetComponent<Button>().interactable = true;
        }
        else if (TFController.currentStep == 23)
        {
            backBut.GetComponent<Button>().interactable = false;
            resetBut.GetComponent<Button>().interactable = true;
        }
        else if (TFController.currentStep == 24)
        {
            resetBut.GetComponent<Button>().interactable = false;
        }
    }
    public void deactivateButtons()
    {
        backBut.GetComponent<Button>().interactable = false;
        resetBut.GetComponent<Button>().interactable = false;
    }
}
