using UnityEngine;

//struct used to create random variables
[System.Serializable]
public struct FloatRange
{

     public float min, max;

     //returns a random number between the max and min of the variable
     public float RandomInRange
     {
          get
          {
               return Random.Range(min, max);
          }
     }
}

//spawns objects and sprays them out like a hose
public class StuffSpawner : MonoBehaviour {

     //variables that will hold random numbers
     public FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;

     //holds various shapes to be sprayed
     public Stuff[] stuffPrefabs;

     float timeSinceLastSpawn;

     public float velocity;

     //random delay between each objects spawn
     float currentSpawnDelay;

     //the objects color
     public Material stuffMaterial;

     //spits out another object at random increments
     void FixedUpdate () {
          timeSinceLastSpawn += Time.deltaTime;
          if (timeSinceLastSpawn >= currentSpawnDelay) {
               timeSinceLastSpawn -= currentSpawnDelay;
               currentSpawnDelay = timeBetweenSpawns.RandomInRange;
               SpawnStuff();
          }
     }

     //spawns the object randomly
     void SpawnStuff() {
          //random object
          Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
          //pulls the object from its pool
          Stuff spawn = prefab.GetPooledInstance<Stuff>();

          //gives it a random transform
          spawn.transform.localPosition = transform.position;
          spawn.transform.localScale = Vector3.one * scale.RandomInRange;
          spawn.transform.localRotation = Random.rotation;

          //random velocity
          spawn.Body.velocity = transform.up * velocity +
               Random.onUnitSphere * randomVelocity.RandomInRange;
          //random spin
          spawn.Body.angularVelocity =
               Random.onUnitSphere * angularVelocity.RandomInRange;

          spawn.SetMaterial(stuffMaterial);
     }
}
