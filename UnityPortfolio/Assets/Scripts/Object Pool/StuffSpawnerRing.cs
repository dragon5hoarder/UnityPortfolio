using UnityEngine;

//craetes a ring of spawners that spit out objects
public class StuffSpawnerRing : MonoBehaviour {

     public int numberOfSpawners;

     public float radius, tiltAngle;

     //holds the spawner
     public StuffSpawner spawnerPrefab;

     //holds the various colors each spawner will have
     public Material[] stuffMaterials;

     //creates spawners based on numberOfSpawners
     void Awake() {
          for (int i = 0; i < numberOfSpawners; i++) {
               CreateSpawner(i);
          }
     }

     //spawns the spawner based on its index
     void CreateSpawner (int index) {
          Transform rotater = new GameObject("Rotater").transform;
          rotater.SetParent(transform, false);
          //evenly spaces out spawners in a circle
          rotater.localRotation =
               Quaternion.Euler(0f, index * 360f / numberOfSpawners, 0f);

          //instantiates the spawner pointing it towards the center
          StuffSpawner spawner = Instantiate<StuffSpawner>(spawnerPrefab);
          spawner.transform.SetParent(rotater, false);
          spawner.transform.localPosition = new Vector3(0f, 0f, radius);
          spawner.transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);

          //gives the spawner the color its objects will have
          spawner.stuffMaterial = stuffMaterials[index % stuffMaterials.Length];
     }
}
