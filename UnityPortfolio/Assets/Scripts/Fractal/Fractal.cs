using System.Collections;
using UnityEngine;

//creates a fractal of basic unity shapes, giving it random amounts of arms, color, rotation and skew
public class Fractal : MonoBehaviour {

     //slots for the shapes and material
     public Mesh[] meshes;
     public Material material;

     //amount of children the parent shape has
     public int maxDepth;
     private int depth;

     //how much smaller each child is
     public float childScale;

     //allows the vector3 to be loaded only once
     private static Vector3[] childDirections = {
          Vector3.up,
          Vector3.right,
          Vector3.left,
          Vector3.forward,
          Vector3.back
     };

     //allows the quaternion to be loaded only once
     private static Quaternion[] childOrientations = {
          Quaternion.identity,
          Quaternion.Euler(0f, 0f, -90f),
          Quaternion.Euler(0f, 0f, 90f),
          Quaternion.Euler(90f, 0f, 0f),
          Quaternion.Euler(-90f, 0f, 0f)
     };

     //holds 2 materials for each generation
     private Material[,] materials;

     //percentage of limbs spawned
     public float spawnProbability;

     //rotation speed
     public float maxRotationSpeed;
     private float rotationSpeed;

     //amount each arm is skewed
     public float maxTwist;

     private void Start(){
          //randomizes the arms rotation speed and skew
          rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
          transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
          if (materials == null) {
               InitializeMaterials();
          }
          //randomizes which mesh the child will have out of the chosen meshes
          gameObject.AddComponent<MeshFilter>().mesh =
               meshes[Random.Range(0, meshes.Length)];
          //randomizes the color between that generations 2 colors
          gameObject.AddComponent<MeshRenderer>().material =
               materials[depth, Random.Range(0, 2)];
          //keeps popping out children until the max amount is reached
          if (depth < maxDepth) {
               StartCoroutine(CreateChildren());
               
          }
     }

     //initializes each generations color
     private void InitializeMaterials () {
          materials = new Material[maxDepth + 1, 2];// the root parent is 0, the last child is maxdepth + 1
          //individually selects each color from a gradient and assigns it to each generation
          for (int i = 0; i <= maxDepth; i++) {
               //t is the distance between the first color and the second, t goes from 0 to 1 to create equal spacing between the colors
               float t = i / (maxDepth - 1f);
               t *= t;
               materials[i, 0] = new Material(material);
               materials[i, 0].color = Color.Lerp(Color.black, Color.yellow, t);
               materials[i, 1] = new Material(material);
               materials[i, 1].color = Color.Lerp(Color.white, Color.green, t);
          }
          //sets the colors for the final generation
          materials[maxDepth, 0].color = Color.blue;
          materials[maxDepth, 1].color = Color.cyan;
     }

     //an iterator that allows each child to be created gradually rather than all at once
     private IEnumerator CreateChildren () {
          for (int i = 0; i < childDirections.Length; i++) { 
               //creates each child at random intervals
               if (Random.value < spawnProbability) { 
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                    new GameObject("Fractal Child").AddComponent<Fractal>().
                         Initialize(this, i);
               }
          }
     }

     //makes sure each child shares the same variables as its parent
     private void Initialize (Fractal parent, int childIndex) {
          meshes = parent.meshes;
          materials = parent.materials;
          maxDepth = parent.maxDepth;
          depth = parent.depth + 1;
          childScale = parent.childScale;
          transform.parent = parent.transform;
          transform.localScale = Vector3.one * childScale;
          transform.localPosition =
               childDirections[childIndex] * (0.5f + 0.5f * childScale);
          transform.localRotation = childOrientations[childIndex];
          spawnProbability = parent.spawnProbability;
          maxRotationSpeed = parent.maxRotationSpeed;
          maxTwist = parent.maxTwist;
     }

     //makes each shape rotate along its y axis
     private void Update() {
          transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
     }

}
