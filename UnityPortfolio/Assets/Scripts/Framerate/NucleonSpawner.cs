using UnityEngine;

//spawns nucleons randomly
public class NucleonSpawner : MonoBehaviour {

     public float timeBetweenSpawns;
     public float spawnDistance;
     //holds the nucleon prefabs
     public Nucleon[] nucleonPrefabs;

     float timeSinceLastSpawn;

     //spawns each nucleon in a consistent amount of time
     void FixedUpdate () {
          timeSinceLastSpawn += Time.deltaTime;
          if (timeSinceLastSpawn >= timeBetweenSpawns) {
               timeSinceLastSpawn -= timeBetweenSpawns;
               SpawnNucleon();
          }
     }

     //spawns the nucleon
     void SpawnNucleon() {
          //randomly picks an assigned prefab
          Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
          Nucleon spawn = Instantiate<Nucleon>(prefab);
          spawn.transform.parent = this.transform;
          //randomly places the nucleon along a sphere
          spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
     }
}
