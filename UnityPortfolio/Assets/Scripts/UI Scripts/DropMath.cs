using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropMath : MonoBehaviour {

     public Dropdown drop;
     public Graph graph;

     public void ChangeGraph()
     {
          graph.function = (GraphFunctionName)drop.value;
     }
}
