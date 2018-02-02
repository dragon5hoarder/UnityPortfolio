using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//basic pieces that give agent pieces energy
public class Food : Resource {

     const char _foodID = 'F';

     public Text display;

     //displays the food's energy
     private void Update()
     {
          display.text = capacity.ToString("F1");
     }

     public override PieceType GetType()
     {
          return PieceType.FOOD;
     }

     //same as parent
     public override void Interact(Resource other)
     {
          
     }

     public override void Interact(Agent other)
     {
          other.energy += capacity;
          finished = true;
     }
}
