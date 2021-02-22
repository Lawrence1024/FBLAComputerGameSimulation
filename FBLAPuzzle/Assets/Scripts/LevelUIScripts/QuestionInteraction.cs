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
    LevelManager levelManager;
    public GameObject QuestionBox;
    public GameObject AnswerButton1;
    public GameObject AnswerButton2;
    public GameObject AnswerButton3;
    public GameObject AnswerButton4;
    
    struct QuestionLine {
        public string question;
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public string correctAnswer;
        public QuestionLine(string q, string a1, string a2, string a3, string a4, string ca)
        {
            question = q;
            answer1 = a1;
            answer2 = a2;
            answer3 = a3;
            answer4 = a4;
            correctAnswer = ca;
        }
    }
    

    void Start()
    {
        //Debug.Log("Question box: "+QuestionBox.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
        levelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        lines = new List<QuestionLine>();
        LoadQuestion("QuestionData.csv");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadQuestion() {
        lines = new List<QuestionLine>();
        LoadQuestion("QuestionData.csv");
        
        
    }
    public string getQuestion(int num) {
        //Debug.Log("Question: " + lines[num].question);
        return lines[num].question;
    }
    public string getAnswer1(int num)
    {
        //Debug.Log("ans1: " + lines[num].answer1);
        return lines[num].answer1;
    }
    public string getAnswer2(int num)
    {
        //Debug.Log("ans2: " + lines[num].answer2);
        return lines[num].answer2;
    }
    public string getAnswer3(int num)
    {
        //Debug.Log("ans3: " + lines[num].answer3);
        return lines[num].answer3;
    }
    public string getAnswer4(int num)
    {
        //Debug.Log("ans4: " + lines[num].answer4);
        return lines[num].answer4;
    }
    public string getCorrectAnswer(int num)
    {
        //Debug.Log("Question: " + lines[num].correctAnswer);
        return lines[num].correctAnswer;
    }

    //Assets/Scripts/QuestionBank/Question1.txt
    void LoadQuestion(string filename)
    {
        TextAsset questionData = Resources.Load<TextAsset>("QuestionData");
        string contents = questionData.text;
        //Debug.Log("this is the question Data: " + questionData.text);

        // convert string to stream
        byte[] byteArray = Encoding.UTF8.GetBytes(contents);
        //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
        MemoryStream stream = new MemoryStream(byteArray);
        // convert stream to string
        //StreamReader reader = new StreamReader(stream);
        //string text = reader.ReadToEnd();

        //string file = "Assets/Resources/" + filename;
        string line;
        StreamReader r = new StreamReader(stream);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {

                    string[] line_values = SplitCsvLine(line);
                    QuestionLine line_entry = new QuestionLine(line_values[0].ToString(), line_values[1].ToString(), line_values[2].ToString(), line_values[3].ToString(), line_values[4].ToString(), line_values[5].ToString());
                    lines.Add(line_entry);
                    //Debug.Log("answer: " + line_values[5]);
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
