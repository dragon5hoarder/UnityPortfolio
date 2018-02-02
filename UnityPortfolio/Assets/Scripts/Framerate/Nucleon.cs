using UnityEngine;

//a particle that propels itself towards the center of a spawner
[RequireComponent(typeof(Rigidbody))]
public class Nucleon : MonoBehaviour {

     public float attractionForce;
     Rigidbody body;

     void Awake() {
          body = GetComponent<Rigidbody>();
     }

     //propels the object towards its parents center
     void FixedUpdate() {
          body.AddForce(transform.localPosition * -attractionForce);
     }
}
