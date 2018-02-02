using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//piece that collects energy and can kill simple pieces if aggressive
public class Strategic : Agent {

     public const char _strategicID = 'T';

     //holds the type of strategy the piece uses
     public GameObject strategyObj;
     Strategy strategy;

     public Text display;

     //displays the pieces energy
     private void Update()
     {
          display.text = energy.ToString("F1");
     }

     public override PieceType GetType()
     {
          return PieceType.STRATEGIC;
     }

     //takes the pieces surrounding and has the current strategy choose a direction for it to take
     public override ActionType TakeTurn(Surroundings surr)
     {
          strategy = strategyObj.GetComponent<Strategy>();
          return strategy.strategyAction(surr);
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
