using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSet : MonoBehaviour {


     public Slider width, height;
     public Transform game;

     public void SetValue()
     {
          
          
          game.GetComponent<Game>().width = (int)width.value;
          game.GetComponent<Game>().height = (int)height.value;
          game.GetComponent<Game>().NewGame();
          

     }
}
