using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextEngine : MonoBehaviour
{   
    // grabs the textmeshpro from unity to edit
    public TextMeshProUGUI textBox;
    public Animator boxAnimator;
    public Animator charAnimator;

    // what to actually display every frame
    string textOutput = "";
    static string currentMessage = "<Press space or click to progress the story>";
    int timer = currentMessage.Length * 2;
    int listCounter = 0;

    // loads all resources needed to swap animations
    // RuntimeAnimatorController wiggle_green = Resources.Load("Assets/Resources/Animation Controllers/wiggle_green");
    // RuntimeAnimatorController wiggle_blue = Resources.Load("Assets/Resources/Animation Controllers/wiggle_blue");
    // RuntimeAnimatorController wiggle_pink = Resources.Load("Assets/Resources/Animation Controllers/wiggle_pink");

    
    string[,] messageList = new string[3,2] {
        {"Yo, what's up?", "green"},
        {"Im tirede man fojksdjfkls ", "blue"},
        {"AYOOOOOOOOOOOOOOOOOOOOO", "pink"},
    };   

    // Update is called once per frame
    void Update()
    {   
        // checks every frame if text buffer update is needed, and then
        // checks if the user is pressing space
        if (timer > 0) {
            bufferText();
        } else {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                slowText(messageList[listCounter, 0], messageList[listCounter,1]);
                listCounter++;
            }
        }
        

    }

    // called every frame to update any slowText messages
    void bufferText() {
        timer--;
        if (timer % 2 == 0) {
            textOutput = textOutput + currentMessage.Substring(0,1);
            currentMessage = currentMessage.Remove(0,1);
        } 
        textBox.text = textOutput;
    }

    // outputs the message one character at a time onto the screen 
    // and waits for the user to press a key to continue
    void slowText(string message) {
        textOutput = "";
        currentMessage = message;
        timer = currentMessage.Length * 2;
    }
    void slowText(string message, string colour) {
        slowText(message);
        boxAnimator.Play("wiggle_" + colour);
        charAnimator.Play("char_wiggle_" + colour);
    }
}