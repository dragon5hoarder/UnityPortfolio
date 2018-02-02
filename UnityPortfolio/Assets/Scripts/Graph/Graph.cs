//creates a graph of 10 to 100 cubes, with color that changes as the cubes change location 
using UnityEngine;

public class Graph : MonoBehaviour {

     //slot for the cube prefab
     public Transform pointPrefab;

     //a slider from 10 t0 100 for the amount of cubes displayed
     [Range (10, 100)]
     public int resolution = 10;

     //holds all the cubes for the graph
     Transform[] points;

     //the drop down menu
     public GraphFunctionName function;

     //each functions name
     static GraphFunction[] functions =
     {
          SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction,
          Ripple, Cylinder, Sphere, Torus
     };

     //makes pi readily available, no need to call the function multiple times
     const float pi = Mathf.PI;

     void Awake ()
     {
          //scales all the cubes to fit into the configured space by making them smaller when there are more cubes
          //currently 50 in the game
          float step = 2f / resolution; 
          Vector3 scale = Vector3.one * step;
          //sets the y and z positions of the cubes so there are no compiler errors in the for loop
          Vector3 position;
          position.y = 0f;
          position.z = 0f;
          points = new Transform[resolution * resolution];
          //creates each cube and puts it in the array
          for (int i = 0; i < points.Length; i++)
          {
               Transform point = Instantiate(pointPrefab);
               point.localScale = scale;
               point.SetParent(transform, false);
               points[i] = point;
          }
     }

     void Update ()
     {
          float t = Time.time;
          //sets the function name to what is displayed on the dropdown menu
          GraphFunction f = functions[(int)function];
          //animates the cubes allowing the graph to keep moving along the x and z axis
          //uses uv coordinates for 3d functions
          float step = 2f / resolution;
          for (int i = 0, z = 0; z < resolution; z++)
          {
               float v = (z + 0.5f) * step - 1f;
               for (int x = 0; x < resolution; x++, i++)
               {
                    float u = (x + 0.5f) * step - 1f;
                    //calls the current function
                    points[i].localPosition = f(u, v, t);
               }
          }
     }

     //2d sine function along the x axis
     static Vector3 SineFunction(float x, float z, float t)
     {
          Vector3 p;
          p.x = x;
          p.y = Mathf.Sin(pi * (x + t));
          p.z = z;
          return p;
     }

     //2d sine function along the x axis
     //adds sin(2pi(x + 2t))/2 to make a more wavy function
     static Vector3 MultiSineFunction(float x, float z, float t)
     {
          Vector3 p;
          p.x = x;
          p.y = Mathf.Sin(pi * (x + t));
          p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
          p.y *= 2f / 3f;
          p.z = z;
          return p;
     }

     //3d sine wave
     static Vector3 Sine2DFunction (float x, float z, float t)
     {
          Vector3 p;
          p.x = x;
          p.y = Mathf.Sin(pi * (x + t));
          p.y += Mathf.Sin(pi * (z + t));
          p.y *= 0.5f;
          p.z = z;
          return p;
     }

     //3d sine wave
     //added another sine function with 2t to add some noise to the graph
     static Vector3 MultiSine2DFunction (float x, float z, float t)
     {
          Vector3 p;
          p.x = x;
          p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
          p.y += Mathf.Sin(pi * (x + t));
          p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
          p.y *= 1f / 5.5f;
          p.z = z;
          return p;
     }

     //creates a ripple
     //a sine wave based on the distance from the center
     static Vector3 Ripple (float x, float z, float t)
     {
          Vector3 p;
          //the distance from the center
          float d = Mathf.Sqrt(x * x + z * z);
          p.x = x;
          //sine of the distance
          p.y = Mathf.Sin(pi * (4f * d - t));
          p.y /= 1f + 10f * d;
          p.z = z;
          return p;
     }

     //an animated cylinder 
     static Vector3 Cylinder (float u, float v, float t)
     {
          Vector3 p;
          //the cylinder's radius
          float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
          p.x = r * Mathf.Sin(pi * u);
          p.y = v;
          p.z = r * Mathf.Cos(pi * u);
          return p;
     }

     //an animated sphere
     static Vector3 Sphere(float u, float v, float t)
     {
          Vector3 p;
          //the sphere's radius
          float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
          r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
          float s = r * Mathf.Cos(pi * 0.5f * v);
          p.x = s * Mathf.Sin(pi * u);
          p.y = r * Mathf.Sin(pi * 0.5f * v);
          p.z = s * Mathf.Cos(pi * u);
          return p;
     }

     //an animated torus
     static Vector3 Torus(float u, float v, float t)
     {
          Vector3 p;
          //inner radius
          float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
          //outer radius
          float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
          float s = r2 * Mathf.Cos(pi * v) + r1;
          p.x = s * Mathf.Sin(pi * u);
          p.y = r2 * Mathf.Sin(pi * v);
          p.z = s * Mathf.Cos(pi * u);
          return p;
     }
}
