﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechInfo : MonoBehaviour
{
    public int health;
    public Slider healthSlider;
    public float antivirusProgress;
    //public Slider antivirusSlider;
    private Context context;
    public float antivirusGoal;
    public int mechNumber;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SendMessage("setMechNum1", mechNumber);
        context = GameObject.Find("ContextManager").GetComponent<Context>();
        this.gameObject.tag = "Destructable";

    }
    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
        GameObject curMech = context.getCurMech();
        if (curMech == this.gameObject)
        {
            antivirusProgress = antivirusProgress+Time.deltaTime;
        }
        //antivirusSlider.value = antivirusProgress;
        //print(Time.timeSinceLevelLoad);
        //print(antivirusProgress);

    }

    public void changeHealth(int deltaHealth)
    {
        health += deltaHealth;
    }

    void changeAntivirus(int antivirusDelta)
    {
        antivirusProgress += antivirusDelta;
    }

    void setMechNumber(int mechNumberValue)
    {
        mechNumber = mechNumberValue;
    }
}
