using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Calculator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text answerDisplay;

    public static double value;
    public static double oldValue;
    public static string op;
    public static string oldOp;
    public static double answer;
    public double progress;
    public double maxTime;
    public static bool opPerformed;
    public static bool eqPerformed;
    public static bool decFlag;
    public static bool negFlag;
    public static bool clearNext;
    public static bool numPressed;
    public bool hovering;
    public static bool succ;
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        answerDisplay.text = "";
        opPerformed = false;
        eqPerformed = false;
        clearNext = false;
        progress = 0.0;
        maxTime = 10.0;
        op = "";
        rend = GetComponent<Renderer>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        EventSystem.current.SetSelectedGameObject(eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        progress = 0.0f;
    }

    // ...the red fades out to cyan as the mouse is held over...
    //public void OnMouseOver()
    //{
    //    rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    //}

    public void numberClick()
    {
        Debug.Log("called number click from update");
        if (clearNext)
        {
            answerDisplay.text = "";
            //opPerformed = false;
            clearNext = false;
        }
        Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);

        answerDisplay.text += EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        value = double.Parse(answerDisplay.text);
        numPressed = true;

    }

    public void operationClick()
    {
        oldOp = op;
        op = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;

        if (opPerformed)
        {
            switch (oldOp)
            {
                case "+":
                    answer = oldValue + value;
                    oldValue = answer;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
                case "-":
                    answer = oldValue - value;
                    oldValue = answer;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
                case "x":
                    answer = oldValue * value;
                    oldValue = answer;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
                case "/":
                    answer = oldValue / value;
                    oldValue = answer;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
            }
        }
        else
        {
            if (!eqPerformed)
            {
                oldValue = value;
            }
        }
        opPerformed = true;
        decFlag = false;
        negFlag = false;
        clearNext = true;
        numPressed = false;

    }

    public void clearClick()
    {
        answerDisplay.text = "";
        value = 0;
        oldValue = 0;
        op = "";
        oldOp = "";
        opPerformed = false;
        eqPerformed = false;
        decFlag = false;
        negFlag = false;
        clearNext = false;
        numPressed = false;
    }

    public void minusClick()
    {
        if (clearNext)
        {
            answerDisplay.text = "";
            clearNext = false;
        }
        if (!negFlag && !numPressed && !decFlag)
        {
            answerDisplay.text += "-";
            negFlag = true;
        }

    }

    public void decimalClick()
    {
        if (clearNext)
        {
            answerDisplay.text = "";
            clearNext = false;
        }
        if (!decFlag)
        {
            answerDisplay.text += ".";
            decFlag = true;
        }
    }

    public void deleteClick()
    {
        if (answerDisplay.text.Length > 0)
        {
            answerDisplay.text = answerDisplay.text.Substring(0, answerDisplay.text.Length - 1);
        }
    }

    public void equalClick()
    {
        switch (op)
        {
            case "+":
                answer = oldValue + value;
                oldValue = answer;
                break;
            case "-":
                answer = oldValue - value;
                oldValue = answer;
                break;
            case "x":
                answer = oldValue * value;
                oldValue = answer;
                break;
            case "/":
                answer = oldValue / value;
                oldValue = answer;
                break;
            default:
                answer = value;
                break;
        }
        answerDisplay.text = "";
        answerDisplay.text = answer.ToString();
        opPerformed = false;
        eqPerformed = true;
        decFlag = false;
        negFlag = false;
        clearNext = true;
        numPressed = false;
    }
    public void clickType()
    {
        string ObjText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        if (int.TryParse(ObjText, out int n))
        {
            Debug.Log("Gonna number click");
            numberClick();
        }
        else if (ObjText.Contains("+") || ObjText.Contains("-") || ObjText.Contains("/") || ObjText.Contains("x"))
        {
            Debug.Log("Gonna operation click");
            operationClick();
        }
        else if (ObjText.Contains("DEL"))
        {
            deleteClick();
        }
        else if (ObjText.Contains("CLEAR"))
        {
            clearClick();
        }
        else if (ObjText.Contains("."))
        {
            decimalClick();
        }
        else if (ObjText.Contains("="))
        {
            Debug.Log("Gonna equal click");
            equalClick();
        }
    }
    // Update is called once per frame
    void Update()
    {
        maxTime = 1;
        if (hovering)
        {
            //Debug.Log("is progress less than max time?");
            //Debug.Log(progress);
            //Debug.Log(maxTime);
            if (progress < maxTime)
            {
                progress += Time.deltaTime;
                //progressBar.fillAmount = progress / maxTime;
                Debug.Log("progress: " + progress);
            }
            else
            {
                succ = true;
                hovering = false;
                progress = 0.0f;
                clickType();
            }
        }
    }
}


