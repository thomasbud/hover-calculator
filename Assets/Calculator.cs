using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Calculator : MonoBehaviour, IPointerEnterHandler
{
    public Text answerDisplay;

    public double value;
    public double oldValue;
    public string op;
    public double answer;
    public bool opPerformed;
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        answerDisplay.text = "";
        opPerformed = false;
        rend = GetComponent<Renderer>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
        //eventData.
        //answerDisplay.text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<Text>().text);
        //answerDisplay.text = "hello";
        EventSystem.current.SetSelectedGameObject(eventData.pointerCurrentRaycast.gameObject);
        //numberClick();
        //answerDisplay.text = eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<Text>().text;
    }

    // ...the red fades out to cyan as the mouse is held over...
    //public void OnMouseOver()
    //{
    //    rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    //}


    public void numberClick()
    {
        //Debug.Log("hi");
        if (opPerformed)
        {
            answerDisplay.text = "";
            opPerformed = false;
        }
        //Debug.Log(PointerEventData.);
        
        answerDisplay.text += EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        value = double.Parse(answerDisplay.text);
        
    }

    public void operationClick()
    {
        op = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        oldValue = value;
        if (opPerformed)
        {
            switch (op)
            {
                case "+":
                    answer = oldValue + value;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
                case "-":
                    answer = oldValue - value;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
                case "x":
                    answer = oldValue * value;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
                case "/":
                    answer = oldValue / value;
                    answerDisplay.text = "";
                    answerDisplay.text = answer.ToString();
                    break;
            }
        }
        opPerformed = true;

        
    }

    public void clearClick() {
        answerDisplay.text = "";
    }

    public void minusClick() {
        answerDisplay.text += "-";
    }

    public void decimalClick() {
        answerDisplay.text += ".";
    }

    public void deleteClick() {
        if (answerDisplay.text.Length > 0) {
            answerDisplay.text = answerDisplay.text.Substring(0, answerDisplay.text.Length-1);
        }
    }

    public void equalClick()
    {
        switch (op)
        {
            case "+":
                answer = oldValue + value;
                break;
            case "-":
                answer = oldValue - value;
                break;
            case "x":
                answer = oldValue * value;
                break;
            case "/":
                answer = oldValue / value;
                break;
        }
        op = "";
        answerDisplay.text = "";
        answerDisplay.text = answer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
