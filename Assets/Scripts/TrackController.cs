﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

    public int trackSize = 16; //Number of beats each track keeps at a time.
    public float bpm = 128f;
    KeyCode track1Key= KeyCode.U, track2Key= KeyCode.I, track3Key= KeyCode.O;

    public Context context; 
    static int size = 3;
    private Track[] tracks = new Track[size];


    // Start is called before the first frame update
    void Start () {
        Debug.Log("size" + size);
        tracks[0] = new Track(trackSize, new Action[]{new RocketFireAction()});
        for (int i = 1; i < size; i++) {
            tracks[i] = new Track (trackSize, new Action[]{new TestAction()});
        }
        setTrack(context.mech1, 0);
        setTrack(context.mech2, 1);
        setTrack(context.mech3, 2);
       
    }
    public void setTrack(GameObject mech, int trackNum){
        foreach (Track t in tracks){
            t.removeMech(mech);
        }
        tracks[trackNum].addMech(mech);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(track1Key)){
            setTrack(context.getCurMech(), 0);
        } else if (Input.GetKey(track2Key)){
            setTrack(context.getCurMech(), 1);
        } else if (Input.GetKey(track3Key)){
            setTrack(context.getCurMech(), 2);
        }
    }
    void onBeat(){
        foreach (Track t in tracks){
            t.runBeat();
        }
    }
}