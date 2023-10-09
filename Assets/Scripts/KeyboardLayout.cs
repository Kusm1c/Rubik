using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardLayout : MonoBehaviour
{
    private Cube cubeMainScript;

    public KeyboardLayout(Cube cubeMainScript)
    {
        this.cubeMainScript = cubeMainScript;
    }
    
    void Start()
    {
        //this script is attached to an empty game object in the canvas and is used to visualize the keyboard layout
        //first we want to create images for each key
        //then we want to position them in the correct place
        Image CameraUpKey = new GameObject("CameraUpKey").AddComponent<Image>();
        CameraUpKey.transform.SetParent(transform);
        CameraUpKey.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraUpKey.rectTransform.anchoredPosition = new Vector2(-50, 0);
        CameraUpKey.color = Color.white;
        TextMeshProUGUI CameraUpKeyText = new GameObject("CameraUpKeyText").AddComponent<TextMeshProUGUI>();
        CameraUpKeyText.transform.SetParent(CameraUpKey.transform);
        CameraUpKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraUpKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        CameraUpKeyText.text = "Z";
        CameraUpKeyText.color = Color.black;
        CameraUpKeyText.alignment = TextAlignmentOptions.Center;
        CameraUpKeyText.fontSize = 30;
        
        Image CameraDownKey = new GameObject("CameraDownKey").AddComponent<Image>();
        CameraDownKey.transform.SetParent(transform);
        CameraDownKey.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraDownKey.rectTransform.anchoredPosition = new Vector2(-50, -50);
        CameraDownKey.color = Color.white;
        TextMeshProUGUI CameraDownKeyText = new GameObject("CameraDownKeyText").AddComponent<TextMeshProUGUI>();
        CameraDownKeyText.transform.SetParent(CameraDownKey.transform);
        CameraDownKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraDownKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        CameraDownKeyText.text = "S";
        CameraDownKeyText.color = Color.black;
        CameraDownKeyText.alignment = TextAlignmentOptions.Center;
        CameraDownKeyText.fontSize = 30;
        
        Image CameraLeftKey = new GameObject("CameraLeftKey").AddComponent<Image>();
        CameraLeftKey.transform.SetParent(transform);
        CameraLeftKey.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraLeftKey.rectTransform.anchoredPosition = new Vector2(-100, -50);
        CameraLeftKey.color = Color.white;
        TextMeshProUGUI CameraLeftKeyText = new GameObject("CameraLeftKeyText").AddComponent<TextMeshProUGUI>();
        CameraLeftKeyText.transform.SetParent(CameraLeftKey.transform);
        CameraLeftKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraLeftKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        CameraLeftKeyText.text = "Q";
        CameraLeftKeyText.color = Color.black;
        CameraLeftKeyText.alignment = TextAlignmentOptions.Center;
        CameraLeftKeyText.fontSize = 30;
        
        Image CameraRightKey = new GameObject("CameraRightKey").AddComponent<Image>();
        CameraRightKey.transform.SetParent(transform);
        CameraRightKey.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraRightKey.rectTransform.anchoredPosition = new Vector2(0, -50);
        CameraRightKey.color = Color.white;
        TextMeshProUGUI CameraRightKeyText = new GameObject("CameraRightKeyText").AddComponent<TextMeshProUGUI>();
        CameraRightKeyText.transform.SetParent(CameraRightKey.transform);
        CameraRightKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraRightKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        CameraRightKeyText.text = "D";
        CameraRightKeyText.color = Color.black;
        CameraRightKeyText.alignment = TextAlignmentOptions.Center;
        CameraRightKeyText.fontSize = 30;
        
        Image CameraClockwiseKey = new GameObject("CameraClockwiseKey").AddComponent<Image>();
        CameraClockwiseKey.transform.SetParent(transform);
        CameraClockwiseKey.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraClockwiseKey.rectTransform.anchoredPosition = new Vector2(0, 0);
        CameraClockwiseKey.color = Color.white;
        TextMeshProUGUI CameraClockwiseKeyText = new GameObject("CameraClockwiseKeyText").AddComponent<TextMeshProUGUI>();
        CameraClockwiseKeyText.transform.SetParent(CameraClockwiseKey.transform);
        CameraClockwiseKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraClockwiseKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        CameraClockwiseKeyText.text = "E";
        CameraClockwiseKeyText.color = Color.black;
        CameraClockwiseKeyText.alignment = TextAlignmentOptions.Center;
        CameraClockwiseKeyText.fontSize = 30;
        
        Image CameraCounterClockwiseKey = new GameObject("CameraCounterClockwiseKey").AddComponent<Image>();
        CameraCounterClockwiseKey.transform.SetParent(transform);
        CameraCounterClockwiseKey.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraCounterClockwiseKey.rectTransform.anchoredPosition = new Vector2(-100, 0);
        CameraCounterClockwiseKey.color = Color.white;
        TextMeshProUGUI CameraCounterClockwiseKeyText = new GameObject("CameraCounterClockwiseKeyText").AddComponent<TextMeshProUGUI>();
        CameraCounterClockwiseKeyText.transform.SetParent(CameraCounterClockwiseKey.transform);
        CameraCounterClockwiseKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        CameraCounterClockwiseKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        CameraCounterClockwiseKeyText.text = "A";
        CameraCounterClockwiseKeyText.color = Color.black;
        CameraCounterClockwiseKeyText.alignment = TextAlignmentOptions.Center;
        CameraCounterClockwiseKeyText.fontSize = 30;
        
        Image RotateClockwiseKey = new GameObject("RotateClockwiseKey").AddComponent<Image>();
        RotateClockwiseKey.transform.SetParent(transform);
        RotateClockwiseKey.rectTransform.sizeDelta = new Vector2(50, 50);
        RotateClockwiseKey.rectTransform.anchoredPosition = new Vector2(125, -25);
        RotateClockwiseKey.color = Color.white;
        TextMeshProUGUI RotateClockwiseKeyText = new GameObject("RotateClockwiseKeyText").AddComponent<TextMeshProUGUI>();
        RotateClockwiseKeyText.transform.SetParent(RotateClockwiseKey.transform);
        RotateClockwiseKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        RotateClockwiseKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        RotateClockwiseKeyText.text = "L";
        RotateClockwiseKeyText.color = Color.black;
        RotateClockwiseKeyText.alignment = TextAlignmentOptions.Center;
        RotateClockwiseKeyText.fontSize = 30;
        
        Image RotateCounterClockwiseKey = new GameObject("RotateCounterClockwiseKey").AddComponent<Image>();
        RotateCounterClockwiseKey.transform.SetParent(transform);
        RotateCounterClockwiseKey.rectTransform.sizeDelta = new Vector2(50, 50);
        RotateCounterClockwiseKey.rectTransform.anchoredPosition = new Vector2(75, -25);
        RotateCounterClockwiseKey.color = Color.white;
        TextMeshProUGUI RotateCounterClockwiseKeyText = new GameObject("RotateCounterClockwiseKeyText").AddComponent<TextMeshProUGUI>();
        RotateCounterClockwiseKeyText.transform.SetParent(RotateCounterClockwiseKey.transform);
        RotateCounterClockwiseKeyText.rectTransform.sizeDelta = new Vector2(50, 50);
        RotateCounterClockwiseKeyText.rectTransform.anchoredPosition = new Vector2(0, 0);
        RotateCounterClockwiseKeyText.text = "K";
        RotateCounterClockwiseKeyText.color = Color.black;
        RotateCounterClockwiseKeyText.alignment = TextAlignmentOptions.Center;
        RotateCounterClockwiseKeyText.fontSize = 30;
    }

    // Update is called once per frame
    void Update()
    {
        // when the user presses a key, we want to highlight the corresponding key on the keyboard layout
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Find("CameraCounterClockwiseKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Find("CameraRightKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Find("CameraClockwiseKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Find("CameraLeftKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Find("CameraDownKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Find("CameraUpKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.Find("RotateCounterClockwiseKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            transform.Find("RotateClockwiseKey").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        
        // when the user releases a key, we want to unhighlight the corresponding key on the keyboard layout
        if (Input.GetKeyUp(KeyCode.Q))
        {
            transform.Find("CameraCounterClockwiseKey").GetComponent<Image>().color = Color.white;
        }
        
        if (Input.GetKeyUp(KeyCode.D))
        {
            transform.Find("CameraRightKey").GetComponent<Image>().color = Color.white;
        }
        
        if (Input.GetKeyUp(KeyCode.E))
        {
            transform.Find("CameraClockwiseKey").GetComponent<Image>().color = Color.white;
        }
        
        if (Input.GetKeyUp(KeyCode.A))
        {
            transform.Find("CameraLeftKey").GetComponent<Image>().color = Color.white;
        }
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            transform.Find("CameraDownKey").GetComponent<Image>().color = Color.white;
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            transform.Find("CameraUpKey").GetComponent<Image>().color = Color.white;
        }
        
        if (Input.GetKeyUp(KeyCode.K))
        {
            transform.Find("RotateCounterClockwiseKey").GetComponent<Image>().color = Color.white;
        }
        
        if (Input.GetKeyUp(KeyCode.L))
        {
            transform.Find("RotateClockwiseKey").GetComponent<Image>().color = Color.white;
        }
    }
}
