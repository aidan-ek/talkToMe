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
    public Animator backgroundAnimator;
    public SpriteRenderer boxRender;
    public SpriteRenderer charRender;
    public SpriteRenderer backgroundRender;

    // what to actually display every frame
    string textOutput = " ";
    static string currentMessage = "<Press space or click to progress the story>";
    int timer = currentMessage.Length * 2;
    int listCounter = 0;
    
    string[,] messageList = new string[40,2] {
        {"Yo what’s up!?", "green"},
        {"What have you been doing?", "green"},
        {"Where have you been?", "green"},

        {"Wanna go out today?", "green"},

        {"Don't look at me like that. I’m just trying to cheer you up!", "green"},
        {"...", "green_dark"},
        {"Why are you like this?", "green_dark"},

        // I'm Sorry x3 are the options

        {"Whatever man.", "green_dark"},
        {"*The green square walks away*", "green_dark"},
        {"...", "none"},
        
        {"500", "delay"},

        {"No one is around anymore.", "none"},
        {"The silence is deafening, yet you can't work up the energy to move yourself or even make a sound.", "none"},
        {"With every second that passes, every blink, time slows down.", "blink"},
        {"Each time your eyes close, it becomes harder to reopen them.", "blink"},

        {"1000", "delay"},

        {"Hey sleepy. What're you up to?", "blue"},
        {"...nothing? C'mon don't give me that crap. Let's get messed up.", "blue"},
        {"Well? Are you gonna say something?", "blue"},
        {"...", "blue"},
        {"Am I that lame?", "blue_dark"},
        
        {"200", "delay"},

        {"I don't even know why I bothered.", "blue_dark"},
        {"The triangle walks away, glaring.", "none"},

        {"200", "delay"},

        {"p̷̸̷̨͙͙͇ͨ̌ͣ͋͟͢͠D҉̣͍̓̎͗͜͜Z҉͆͢͠͠N̵҉̾͟͞͡G̸҉̜̜̱̄ͩ͆͜͝͞Z҉͆͢͠͠p̷̸̷̨͙͙͇ͨ̌ͣ͋͟͢͠u̶͖̖͆̊̈́͡͡D҉̣͍̓̎͗͜͜v҉̨̊͢͠͠I҉̡̯̺̜̅́͋̃͢͜V̶̝̐̀͟͟͝G̸҉̜̜̱̄ͩ͆͜͝͞a҉͖̟̜̞̂̃̑̽͢͢͠͡U̵̶̸̹̮̹̲̻͙̎ͪͣͦ͜͡͞͡͡F̶̵͖͚̯̮̤̫̿̆͌͋͢͟͡͡B̵̴҉̞̠̘̩͍̱́͊͗͜͠͠͠͠v", "pink_dark"},
        {"H̴̶̵e̵̡̫̫͍͕̎ͭ̐͟͟͝͞ȳ̸̵̩̜͔͍̔́͟͟͢͡, ȳ̸̵̩̜͔͍̔́͟͟͢͡ou̶͖̖͆̊̈́͡͡ al̶rig̷̵̸̡̼̱͎͎̞ͤͬ̅͢͟͞h̷̶̘̘̬ͭ̏͞͡t?", "pink_dark"},
        {"Can you hear me? Hello?", "pink_dark"},
        {"You alright?", "pink"},
        {"I was listening to your conversation earlier. Er I mean.. I wasn't eavesdropping, I just overheard.", "pink"},
        
        {"Look, I don't know what you're going through but..", "pink"},
        {"*A crumpled piece of paper falls into your lap*", "pink"},
        {"Everyone needs someone to talk to.", "pink"},
        {"Don't feel like talking much eh? That's alright.", "pink"},
        {"I hope to see you around again.", "pink"},
        {"The creature floats away.", "none"},

        {"200", "delay"},
        
        {"You gaze at the paper ball in your lap.", "none"},
        {"As it unfolds, it reveals itself to be a poem.", "none"},
        {"The sheet has clearly seen better days, but the text is still legible.", "none"},

    };   

    // Update is called once per frame
    void Update()
    {   
        Debug.Log(listCounter);
        // checks every frame if text buffer update is needed, and then
        // checks if the user is pressing space
        if (timer > 0) {
            bufferText();
        } else {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) || textOutput.Equals("")) {
                if (listCounter == 40) {
                    displayPoem();
                    listCounter++;
                } else if (listCounter == 41) {
                    backgroundAnimator.Play("endScreen");
                    listCounter++;
                } else if (listCounter == 42) {
                    Application.Quit();
                } else {
                    slowText(messageList[listCounter, 0], messageList[listCounter,1]);
                    listCounter++;
                }
            }
        }
    }
        


    // called every frame to update any slowText messages
    void bufferText() {
        timer--;
        if (timer % 2 == 0 && !currentMessage.Equals("")) {
            textOutput = textOutput + currentMessage.Substring(0,1);
            currentMessage = currentMessage.Remove(0,1);
        } 
        textBox.text = textOutput;
    }

    // outputs the message one character at a time onto the screen 
    // and waits for the user to press space or m1 to continue
    void slowText(string message) {
        textOutput = "";
        currentMessage = message;
        timer = currentMessage.Length * 2;
    }
    void slowText(string message, string colour) {

        if (colour.Equals("blink")) {
            // shows the blink animation
            backgroundRender.enabled = true;
            backgroundAnimator.Play("blink");
            boxAnimator.Play("wiggle_" + colour);
            slowText(message);
        }
        else if (colour.Equals("delay")) {
            // hides the textbox and waits a specified amount of time
            boxRender.enabled = false;
            timer = int.Parse(message);
            currentMessage = "";
            textOutput = "";   
        } 
        else {
            // shows all elements except for background blinking animation
            charRender.enabled = true;
            backgroundRender.enabled = false;
            boxRender.enabled = true;
            charAnimator.Play("char_wiggle_" + colour);
            boxAnimator.Play("wiggle_" + colour);
            slowText(message);
        }
        
        if (colour.Equals("none")) {
            // hides the character
            charRender.enabled = false;
        }
        
    }

    void displayPoem() {
        backgroundRender.enabled = true;
        boxRender.enabled = false;
        charRender.enabled = false;
        currentMessage = "";
        timer = 200;
        textOutput = " ";
        backgroundAnimator.Play("poem");
    }
}