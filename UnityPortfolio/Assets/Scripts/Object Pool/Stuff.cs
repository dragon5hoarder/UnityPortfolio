using UnityEngine;

//child of PooledObject
//sets the mesh and 3d components of the pooled object
[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject {

     public Rigidbody Body { get; private set; }

     //holds each mesh the prefab holds
     MeshRenderer[] meshRenderers;

     //sets the color for each mesh the prefab holds
     public void SetMaterial(Material m) {
          for (int i = 0; i < meshRenderers.Length; i++) {
               meshRenderers[i].material = m;
          }
     }

     //sets the prefabs components
     void Awake() {
          Body = GetComponent<Rigidbody>();
          meshRenderers = GetComponentsInChildren<MeshRenderer>();       
     }

     //destroys the objects when they enter the kill square
     void OnTriggerEnter (Collider enteredCollider) {
          if (enteredCollider.CompareTag("Kill Zone")) {
               ReturnToPool();
          }
     }

     /*void OnLevelWasLoaded() {
          ReturnToPool();
     }*/
    
}
