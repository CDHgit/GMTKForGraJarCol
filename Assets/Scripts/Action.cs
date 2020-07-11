using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action {
    public abstract void performAction (GameObject o);
    
}
public class TestAction : Action {
    
    public override void performAction (GameObject o) {
        // Debug.Log ("hello"); 
    }
}
public class DashAction : Action {
    public override void performAction (GameObject o) {
        o.SendMessage("startDash", HelperFunctions.getAngleToMouse(o));
    }

}
public class RocketFireAction : Action { 
    public override void performAction (GameObject o) {
        o.SendMessage("fireRocket", HelperFunctions.getAngleToMouse(o));
    }
}
public class LaserAction : Action {
    public override void performAction (GameObject o) {
        o.SendMessage("shootAt");
    }

}