﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using UnityEngine.UI;
public class CT1_QuestionBoxCondition : MonoBehaviour
{
    CT1_QuestionInteraction questionInteraction;
    CT1_LevelManager levelManager;
    public bool activated = false;
    public int QuestionNumber;

    string question;
    string answer1;
    string answer2;
    string answer3;
    string answer4;
    string correctAnswer;
    private GameObject[] buttons;

    void Start()
    {
        questionInteraction= GameObject.Find("LevelManager").GetComponent<CT1_QuestionInteraction>();
        levelManager= GameObject.Find("LevelManager").GetComponent<CT1_LevelManager>();
        buttons = GameObject.FindGameObjectsWithTag("Buttons");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkBoxQuestionStatus()
    {
        if (!activated) {

            activated = true;
            levelManager.QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            levelManager.QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            levelManager.QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            levelManager.QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            question =questionInteraction.getQuestion(QuestionNumber);
            answer1=questionInteraction.getAnswer1(QuestionNumber);
            answer2=questionInteraction.getAnswer2(QuestionNumber);
            answer3=questionInteraction.getAnswer3(QuestionNumber);
            answer4=questionInteraction.getAnswer4(QuestionNumber);
            correctAnswer = questionInteraction.getCorrectAnswer(QuestionNumber);
            questionInteraction.QuestionBox.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = question;
            questionInteraction.AnswerButton1.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer1;
            questionInteraction.AnswerButton2.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer2;
            questionInteraction.AnswerButton3.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer3;
            questionInteraction.AnswerButton4.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer4;

            checkCorrectAnswer(questionInteraction.AnswerButton1, 1);
            checkCorrectAnswer(questionInteraction.AnswerButton2, 2);
            checkCorrectAnswer(questionInteraction.AnswerButton3, 3);
            checkCorrectAnswer(questionInteraction.AnswerButton4, 4);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
                Debug.Log("button uninteractable");
            }
            levelManager.QuestionCanvas.SetActive(true);
        }
    }


    void checkCorrectAnswer(GameObject button, int buttonNum) {

        if (buttonNum == int.Parse(correctAnswer)) {
            button.GetComponent<CT1_ButtonRightOrWrong>().RightOrWrong = "right";
        } else{
            button.GetComponent<CT1_ButtonRightOrWrong>().RightOrWrong = "wrong";
        }
    }
}
