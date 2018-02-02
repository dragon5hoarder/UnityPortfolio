using UnityEngine;

//parent class
//creates an object that goes into an object pool
public class PooledObject : MonoBehaviour {

	public ObjectPool Pool { get; set; }

     //used to reference an object in the scene
     //cant save as part of the prefab
     [System.NonSerialized]
     ObjectPool poolInstanceForPrefab;

     //stores the object into the pool
     public void ReturnToPool () {
          if (Pool) {
               Pool.AddObject(this);
          }
          else {
               Destroy(gameObject);
          }
     }

     //returns an unused object from the pool
     public T GetPooledInstance<T> () where T : PooledObject {
          if (!poolInstanceForPrefab) {
               poolInstanceForPrefab = ObjectPool.GetPool(this);
          }
          return (T)poolInstanceForPrefab.GetObject();
     }
}
