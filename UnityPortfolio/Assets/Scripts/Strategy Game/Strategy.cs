using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class of each startegy
public abstract class Strategy : MonoBehaviour {

     abstract public ActionType strategyAction(Surroundings surr);
}
