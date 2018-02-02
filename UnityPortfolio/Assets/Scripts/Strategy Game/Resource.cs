using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pieces that give agents differing amounts of energy
public class Resource : Piece{

     //amount of energy the resources contain
     public double capacity;

     //rate the resource decays
     public const double _resourceSpoilFactor = 1.2;

     //when the resource is used it is fully consumed
     public void consume()
     {
          Destroy(this.gameObject);
     }

     //the resource exponentially loses energy i
     public sealed override void Age()
     {
          if (capacity < 0.000001)
               capacity = 0.0;
          capacity /= _resourceSpoilFactor;
     }

     public sealed override bool IsViable()
     {
          return !IsFinished() && capacity > 0.0;
     }

     //resources dont move
     public override ActionType TakeTurn(Surroundings surr)
     {
          return ActionType.STAY;
     }

     //gives enrgy when an agent interacts with it
     public override void Interact(Agent other)
     {
          other.energy += capacity;
          finished = true;
     }

     //resources dont interact with each other
     public override void Interact(Resource other)
     {
          
     }

     //overridden by child
     public override PieceType GetType()
     {
          return PieceType.EMPTY;
     }
}
