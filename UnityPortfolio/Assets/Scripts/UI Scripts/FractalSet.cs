using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FractalSet : MonoBehaviour {


     public Slider s1, s2, s3, s4, s5;
     public Transform fractal;
     Transform instFractal;

     public void SetValue()
     {
          if (instFractal != null)
               Destroy(instFractal.gameObject);
          instFractal = Instantiate(fractal);
          instFractal.GetComponent<Fractal>().maxDepth = (int)s1.value;
          instFractal.GetComponent<Fractal>().childScale = s2.value;
          instFractal.GetComponent<Fractal>().spawnProbability = s3.value;
          instFractal.GetComponent<Fractal>().maxRotationSpeed = s4.value;
          instFractal.GetComponent<Fractal>().maxTwist = s5.value;
          

     }
}
