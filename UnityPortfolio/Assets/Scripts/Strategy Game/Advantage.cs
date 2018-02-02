using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//piece that gives the agent piece more energy than food
public class Advantage : Resource {

     const char _advantageID = 'D';

     public const double _advantageMultFactor = 2.0;
     public Text display;

     //contains more energy than food
     private void Awake()
     {
          capacity *= _advantageMultFactor;
     }

     //displays the current energy
     private void Update()
     {
          display.text = capacity.ToString("F1");
     }

     public override PieceType GetType()
     {
          return PieceType.ADVANTAGE;
     }

     //same as parent
     public override void Interact(Agent other)
     {
          other.energy += capacity;
          finished = true;
     }

     public override void Interact(Resource other)
     {
          
     }


}
