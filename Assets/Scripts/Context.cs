﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Context : MonoBehaviour {
    public float timeToWin = 50;
    private readonly string[] mechs = { "Mech1", "Mech2", "Mech3" };
    public int dead = 0, goal = 0;
    public int goalThreshold = 1;
    public Slider slider;
    public Sprite winSprite, lossSprite;
    public int deadThreshold = 1;
    public int switchCooldownBeats = 8;
    int beatsToReady = 0; //beats left in cooldown
    int curMechIdx = 0;
    KeyCode mech1Key = KeyCode.J, mech2Key = KeyCode.K, mech3Key = KeyCode.L; //Private key codes JKL
    public List<GameObject> mechList; // Mech list used to update the active
    private Sprite[] mechSwitchUISpriteOnList = new Sprite[3];
    private Sprite mechSwitchUISpriteOff;
    private GameObject[] mechSwitchUIGOs = new GameObject[3];
    MechControls mechKeyControlsScript;
    public bool[] mechsEnabled = new bool[3] { true, true, true };
    // Start is called before the first frame update
    void Start () {
        //Create mech arrow array for each mech in order Blue, Yellow, Green
        mechSwitchUISpriteOnList[0] = Resources.Load<Sprite>("Images/BlueLED");
        mechSwitchUISpriteOnList[1] = Resources.Load<Sprite>("Images/YellowLED");
        mechSwitchUISpriteOnList[2] = Resources.Load<Sprite>("Images/GreenLED");
        mechSwitchUISpriteOff = Resources.Load<Sprite>("Images/OffLED");
        mechSwitchUIGOs[0] = Resources.Load<GameObject>("BlueLED");
        mechSwitchUIGOs[1] = Resources.Load<GameObject>("YellowLED");
        mechSwitchUIGOs[2] = Resources.Load<GameObject>("GreenLED");
        // Create a list of mechs to iterate through later for easier updating
        mechList = new List<GameObject> ();
        int i = 0;
        foreach (string s in mechs) {

            mechList.Add (GameObject.Find (s));
            mechList[i].GetComponent<MechInfo> ().mechNumber = i;
            ++i;
        }
        
        //Initial Mech
        switchMech (1);

        // This doesn't work right now might need to trigger it or have a 3 state maybe
        // switchMech(0);
    }

    // Update is called once per frame
    void Update () {
       
        if (doneStart == -1) {
            Debug.Log("In donestart");
            slider.value=100f*Time.timeSinceLevelLoad/timeToWin;
            int oldBeatsToReady = beatsToReady;
            // for (int i = 0; i < 3 && !mechList[(curMechIdx) % 3].GetComponent<MechControls> ().getMechEnabledStatus (); i++) {
            //     beatsToReady = 0;
            //     switchMech ((curMechIdx + 1) % 3);
            // }
            beatsToReady = oldBeatsToReady;
            Debug.Log("In about to if");
            if (Input.GetKeyDown (mech1Key)) {
                switchMech (0);
                Debug.Log("In switchKey" + mech1Key);
            } else if (Input.GetKeyDown (mech2Key)) {
                switchMech (1);
            } else if (Input.GetKeyDown (mech3Key)) {
                Debug.Log("In switchKey" + mech3Key);
                switchMech (2);

            }
            if (Time.timeSinceLevelLoad > timeToWin) {
                win ();
            }
            //Debug.Log(dead + " DEAD "+ deadThreshold);
            if (dead >= deadThreshold) {
                lose ();
            }
        }
        else if (Time.unscaledTime - doneStart > 4) {
            SceneManager.LoadScene("Level 2");
            SceneManager.LoadScene ("Menu");
            doneStart = -1;
            Time.timeScale=1;
        }
        //Debug.Log("AWEFAWEFAWEFAWE AW W FEW W W EW F A "  + (Time.unscaledTime - doneStart));
    }
    public float calculteScore () {
        float ret = 0;
        foreach (GameObject o in mechList) {
            ret += o.GetComponent<MechInfo> ().health;
        }
        return ret;
    }
    float doneStart = -1;
    public void win () {
        foreach (GameObject o in mechList) {
            o.GetComponent<MechControls> ().setMechEnabledStatus (false);
        }
        try {
            GetComponent<SpriteRenderer> ().sprite = winSprite;
            GetComponent<SpriteRenderer> ().enabled = (true);
            Time.timeScale = 0;
        } catch (MissingComponentException e) { Debug.LogError ("Failed to lose because no sprite renderer"); }
        doneStart = Time.unscaledTime;
    }
    public void lose () {
        foreach (GameObject o in mechList) {
            o.GetComponent<MechControls> ().setMechEnabledStatus (false);
        }
        try {
            GetComponent<SpriteRenderer> ().sprite = lossSprite;
            GetComponent<SpriteRenderer> ().enabled = (true);
            Time.timeScale = 0;
        } catch (MissingComponentException e) { Debug.LogError ("Failed to lose because no sprite renderer"); }
        doneStart = Time.unscaledTime;
    }
    public GameObject getCurMech () {
        return mechList[curMechIdx];
    }
    /**
     * Switch which mech is being controlled, checks if mech is enabled
     */
    private void switchMech (int mechNum) {
        Debug.Assert (mechNum >= 0 && mechNum < 3, "Mechnum should be in the range [1,3] but was: " + mechNum);
        Debug.Log ("Switching mech to " + mechNum);

        //Set the active mech to true and the others to false
        if (beatsToReady <= 0 && curMechIdx != mechNum && mechsEnabled[mechNum]) {
            beatsToReady = switchCooldownBeats;
            mechList[curMechIdx].SendMessage ("setActive", false);
            // mechSwitchUIGOs[curMechIdx].GetComponent<SpriteRenderer>().sprite = mechSwitchUISpriteOff;
            curMechIdx = mechNum;
            mechList[curMechIdx].SendMessage ("setActive", true);
            // mechSwitchUIGOs[curMechIdx].GetComponent<SpriteRenderer>().sprite = mechSwitchUISpriteOnList[curMechIdx];
        } else {
            print ("Mech Switch Failed");
        }

    }
    public void onBeat (int beatNum) {
        if (beatsToReady > 0) {
            beatsToReady--;
        }
    }
    public bool mechIsEnabled (int mechNum) {
        return mechsEnabled[mechNum];
    }
}