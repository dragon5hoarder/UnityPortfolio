using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic stategy, collects the most amount of energy, only kills as a last resort
public class DefaultStrategy : Strategy {

     //takes the current surroundings and returns the intended direction
     public override ActionType strategyAction(Surroundings surr)
     {
          //lists that hold the different desired piece types in the current pieces surroundings
          List<int> advantagePositions = new List<int>();
          List<int> foodPositions = new List<int>();
          List<int> emptyPositions = new List<int>();
          List<int> simplePositions = new List<int>();
          //if there is no desired direction the piece will stay put
          int emptySpace = 4;
          //bool moveToEmpty = false;
          //bool nabbed = false;

          //fills the lists with the surrounding types index
          for (int i = 0; i < 9; i++)
          {
               if (surr.array[i] == PieceType.ADVANTAGE)
                    advantagePositions.Add(i);
               else if (surr.array[i] == PieceType.FOOD)
                    foodPositions.Add(i);
               else if (surr.array[i] == PieceType.EMPTY)
                    emptyPositions.Add(i);
               else if (surr.array[i] == PieceType.SIMPLE)
                    simplePositions.Add(i);
          }

          //if there is an advantage nearby the piece will to to any of them randomly,
          //else it goes to a random food, 
          //else a random empty slot,
          //and if all else fails it will kill a random simple piece
          int randNum;
          if (advantagePositions.Count > 0)
          {
               randNum = Random.Range(0, advantagePositions.Count);
               emptySpace = advantagePositions[randNum];
          }
          else if (foodPositions.Count > 0)
          {
               randNum = Random.Range(0, foodPositions.Count);
               emptySpace = foodPositions[randNum];
          }
          else if (emptyPositions.Count > 0)
          {
               randNum = Random.Range(0, emptyPositions.Count);
               emptySpace = emptyPositions[randNum];
          }
          else if (simplePositions.Count > 0)
          {
               randNum = Random.Range(0, simplePositions.Count);
               emptySpace = simplePositions[randNum];
          }

          return (ActionType)emptySpace;

     }
}
