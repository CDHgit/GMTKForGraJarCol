﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SongTimer : MonoBehaviour
{
    private AudioSource song;
    public float bpm = 130f; //bpm of the song
    public float beatsPerAction = 2;
    public float startOffset = 0; //number of seconds that pass before the first beat
    List<GameObject> observers = new List<GameObject>(); //list of all objects that want to be notified when the beat falls
    static string[] observedObjects = { "ContextManager", "TrackController", "Mech1", "Mech2", "Mech3" };

    // TODO: prox mines glow on the beat??

    public float secPerBeat; //number of seconds that pass between beats
    float songPos; //number of seconds since the first beat
    float songStart; //time when the song start was recorded
    int lastBeat=0; //the last beat number
    void Start()
    {
        secPerBeat = 60f/bpm*beatsPerAction;
        songStart = (float)AudioSettings.dspTime;

        song = gameObject.GetComponent<AudioSource>();
        song.Play();

        foreach(string s in observedObjects) {
            observers.Add(GameObject.Find(s));
        }

    }

	void Update ()
	{
        
        songPos = (float)(AudioSettings.dspTime - songStart - startOffset);
		if (songPos/secPerBeat > lastBeat){
            foreach (GameObject o in observers) {
                o.SendMessage("onBeat", lastBeat);
            }
            lastBeat++;
        }
		
	
    }
}
