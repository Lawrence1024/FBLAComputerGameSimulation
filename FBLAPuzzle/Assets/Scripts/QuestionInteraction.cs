using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System;
public class QuestionInteraction : MonoBehaviour
{
    List<QuestionLine> lines;
    public GameObject QuestionBox;
    public GameObject AnswerButton1;
    public GameObject AnswerButton2;
    public GameObject AnswerButton3;
    public GameObject AnswerButton4;

    public int QuestionNumber;
    struct QuestionLine {
        public string context;
        public string condition;
        public QuestionLine(string ct, string cd)
        {
            context = ct;
            condition = cd;
        }
    }
    

    void Start()
    {
        Debug.Log("Question box: "+QuestionBox.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
    }

    // Update is called once per frame
    void Update()
    {
        //If the box activates 

        if (Input.GetKeyDown("space"))
        {
           /* if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else {
                Time.timeScale = 0;
            }*/
            loadQuestion();
        }
    }

    public void loadQuestion() {
        lines = new List<QuestionLine>();
        LoadQuestion("Question"+ QuestionNumber+".txt");
        QuestionBox.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = displayContext(0);
        AnswerButton1.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = displayContext(1);
        AnswerButton1.GetComponent<ButtonRightOrWrong>().RightOrWrong = displayRightOrWrong(1);
        AnswerButton2.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = displayContext(2);
        AnswerButton2.GetComponent<ButtonRightOrWrong>().RightOrWrong = displayRightOrWrong(2);
        AnswerButton3.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = displayContext(3);
        AnswerButton3.GetComponent<ButtonRightOrWrong>().RightOrWrong = displayRightOrWrong(3);
        AnswerButton4.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = displayContext(4);
        AnswerButton4.GetComponent<ButtonRightOrWrong>().RightOrWrong = displayRightOrWrong(4);
        
    }
    public string displayContext(int num) {
        Debug.Log("Question: " + lines[num].context);
        return lines[num].context;
    }
    public string displayRightOrWrong(int num) {
        Debug.Log("condition: " + lines[num].condition);
        return lines[num].condition;
    }

    //Assets/Scripts/QuestionBank/Question1.txt
    void LoadQuestion(string filename)
    {
        string file = "Assets/Scripts/QuestionBank/" + filename;
        string line;
        StreamReader r = new StreamReader(file);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {

                    string[] line_values = SplitCsvLine(line);
                    QuestionLine line_entry = new QuestionLine(line_values[0], line_values[1]);
                    lines.Add(line_entry);
                    //Debug.Log("question: " + line_values[0]);
                    //Debug.Log("answer: " + line_values[1]);
                }
            }
            while (line != null);
            r.Close();
        }

    }

    string[] SplitCsvLine(string line)
    {
        string pattern = @"
     # Match one value in valid CSV string.
     (?!\s*$)                                      # Don't match empty last value.
     \s*                                           # Strip whitespace before value.
     (?:                                           # Group for value alternatives.
       '(?<val>[^'\\]*(?:\\[\S\s][^'\\]*)*)'       # Either $1: Single quoted string,
     | ""(?<val>[^""\\]*(?:\\[\S\s][^""\\]*)*)""   # or $2: Double quoted string,
     | (?<val>[^,'""\s\\]*(?:\s+[^,'""\s\\]+)*)    # or $3: Non-comma, non-quote stuff.
     )                                             # End group of value alternatives.
     \s*                                           # Strip whitespace after value.
     (?:,|$)                                       # Field ends on comma or EOS.
     ";
        string[] values = (from Match m in Regex.Matches(line, pattern,
            RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
                           select m.Groups[1].Value).ToArray();
        return values;
    }
}
