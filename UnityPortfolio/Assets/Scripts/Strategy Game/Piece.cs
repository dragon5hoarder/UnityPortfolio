using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//parent class for all pieces in strategy game
public abstract class Piece : MonoBehaviour {

     //unused
     public int id;

     //markers for if the piece has gone yet
     public bool finished = false, turned = false;

     protected bool IsFinished()
     {
          return finished;
     }

     //methods used in child classes
     abstract public void Age();
     abstract public bool IsViable();
     abstract public PieceType GetType();

     abstract public ActionType TakeTurn(Surroundings surr);

     abstract public void Interact(Agent other);
     abstract public void Interact(Resource other);
}
