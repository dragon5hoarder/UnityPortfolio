using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pieces that interact with other pieces
public class Agent : Piece {

     //amount of energy each unit has
     public double energy;

     //rate unit's energy decreases
     const double _agentFatigueRate = 0.3;

     //variables for using Vector3.Lerp, allowing the piece to move after taking an action
     public float movementTime = 1f;//moves for only 1 second
     bool isMoving;
     Vector3 startPos, endPos;
     float timeStartedMovement;

     //sets variables used to determine the pieces movement
     public void startMovement(Vector3 end)
     {
          isMoving = true;
          timeStartedMovement = Time.time;

          startPos = transform.localPosition;
          endPos = end;
     } 

     private void FixedUpdate()
     {
          //moves the piece after taking an action
          if (isMoving)
          {
               float timeSinceStarted = Time.time - timeStartedMovement;// amount of time since startMovement was called
               float percentageComplete = timeSinceStarted / movementTime;//creates a percentage each frame between 0 and 1 second

               transform.localPosition = Vector3.Lerp(startPos, endPos, percentageComplete);
               if (percentageComplete >= 1.0f)
                    isMoving = false;
          }
     }

     //ages the piece
     public override void Age()
     {
          energy -= _agentFatigueRate;
     }

     //piece is viable if it wasnt destroyed and still has energy
     public override bool IsViable()
     {
          return !IsFinished() && energy > 0.0;
     }

     //when two agents interact with each other, they subtract energy from each other
     public override void Interact(Agent other)
     {
          //equal energy means they both are destroyed
          if (energy == other.energy)
          {
               finished = true;
               other.finished = true;
          }
          //only need to subtract energy from the larger one since the smaller one will be destroyed
          else if (energy > other.energy)
          {
               energy -= other.energy;
               other.finished = true;
          }
          else
          {
               other.energy -= energy;
               finished = true;
          }
     }

     //resources have the ineract method inside their class as well
     public override void Interact(Resource other)
     {
          var t = this;
          other.Interact(t);
     }

     //this method is overridden in the children
     public override PieceType GetType()
     {
          return PieceType.EMPTY;
     }

     //this method is overridden in the children
     public override ActionType TakeTurn(Surroundings surr)
     {
          return ActionType.STAY;
     }
}
