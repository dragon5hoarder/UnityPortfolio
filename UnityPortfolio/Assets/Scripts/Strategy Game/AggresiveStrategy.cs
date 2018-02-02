using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a strategy that has the piece kill an agent piece first then go about its business
public class AggresiveStrategy : Strategy {

     //if the piece has too much energy it will become aggressive
     public const double _defaultAggressionThreshold = Game._startingAgentEnergy * 0.75;
     double agentEnergy = Game._startingAgentEnergy;

     //takes the pieces current surroundings and chooses a direction to take
     public override ActionType strategyAction(Surroundings surr)
     {
          //lists that hold different piece types based on strategy's preferences
          List<int> agentPositions = new List<int>();
          List<int> advantagePositions = new List<int>();
          List<int> foodPositions = new List<int>();
          List<int> emptyPositions = new List<int>();
          //if no direction is chosen the piece will stay still
          int emptySpace = 4;
          //bool moveToEmpty = false;
          //bool nabbed = false;

          //fills the lists with the surrounding's index
          for (int i = 0; i < 9; i++)
          {
               //will only attack another agent if aggressive
               if (agentEnergy >= _defaultAggressionThreshold && (surr.array[i] == 
                    PieceType.SIMPLE || surr.array[i] == PieceType.STRATEGIC))
                    agentPositions.Add(i);
               if (surr.array[i] == PieceType.ADVANTAGE)
                    advantagePositions.Add(i);
               else if (surr.array[i] == PieceType.FOOD)
                    foodPositions.Add(i);
               else if (surr.array[i] == PieceType.EMPTY)
                    emptyPositions.Add(i);
          }

          //will attempt to kill a random piece first,
          //else it will take a random advantage piece,
          //else a random food piece,
          //and lastly it will move into a random empty space
          int randNum;
          if (agentPositions.Count > 0)
          {
               randNum = Random.Range(0, agentPositions.Count);
               emptySpace = agentPositions[randNum];
          }
          else if (advantagePositions.Count > 0)
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

          return (ActionType)emptySpace;

     }
}
