using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//piece that only collects energy
public class Simple : Agent {

     public const char _simpleID = 'S';

     public Text display;

     //displays the energy
     private void Update()
     {
          display.text = energy.ToString("F1");
     }

     public override PieceType GetType()
     {
          return PieceType.SIMPLE;
     }

     //takes the pieces surroundings and returns the pieces intended direction
     public override ActionType TakeTurn(Surroundings surr)
     {
          //lists holding the pieces surrounding resources or empty spaces
          List<int> resourcePositions = new List<int>();
          List<int> emptyPositions = new List<int>();
          //the piece will stay in its current location if no direction is viable
          int emptySpace = 4;

          //fills the lists with the index of each respective surrounding
          for(int i = 0; i < 9; i++)
          {
               if (surr.array[i] == PieceType.FOOD || surr.array[i] == PieceType.ADVANTAGE)
                    resourcePositions.Add(i);
               else if (surr.array[i] == PieceType.EMPTY)
                    emptyPositions.Add(i);

          }
          //if there are resources nearby the piece will go towards one of them randomly
          //otherwise it will go to a random empty space
          //if neither of those options are viable it will stay in its current location
          int randNum;
          if (resourcePositions.Count > 0)
          {
               randNum = Random.Range(0, resourcePositions.Count);
               emptySpace = resourcePositions[randNum];
          }
          else if(emptyPositions.Count > 0)
          {
               randNum = Random.Range(0, emptyPositions.Count);
               emptySpace = emptyPositions[randNum];
          }

          return (ActionType)emptySpace;
     }

     //same as parent
     public override void Interact(Agent other)
     {
          base.Interact(other);
     }

     public override void Interact(Resource other)
     {
          base.Interact(other);
     }
}
