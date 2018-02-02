using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//an object pool that holds object then re-instantiates them rather than 
//destroying them and instantiating a new object
//saves memory
public class ObjectPool : MonoBehaviour {

     PooledObject prefab;

     //a list of pooled objects
     List<PooledObject> availableObjects = new List<PooledObject>();

     //returns the last object in available objects,
     //if there are no objects adds one to the pool
     public PooledObject GetObject() {
          PooledObject obj;
          int lastAvailableIndex = availableObjects.Count - 1;
          if (lastAvailableIndex >= 0) {
               obj = availableObjects[lastAvailableIndex];
               availableObjects.RemoveAt(lastAvailableIndex);
               obj.gameObject.SetActive(true);
          }
          else {
               obj = Instantiate<PooledObject>(prefab);
               obj.transform.SetParent(transform, false);
               obj.Pool = this;
          }          
          return obj;
     }

     //adds the selected obeject to the pool
     public void AddObject(PooledObject obj) {
          obj.gameObject.SetActive(false);
          availableObjects.Add(obj);
     }

     //returns the object pool with the prefabs name or creates a new one
     public static ObjectPool GetPool (PooledObject prefab) {
          GameObject obj;
          ObjectPool pool;
          if (Application.isEditor) {
               obj = GameObject.Find(prefab.name + " Pool");
               if (obj) {
                    pool = obj.GetComponent<ObjectPool>();
                    if (pool) {
                         return pool;
                    }
               }
          }
          obj = new GameObject(prefab.name + " Pool");
          //keeps the objects between scenes if there are no pools
          DontDestroyOnLoad(obj);
          pool = obj.AddComponent<ObjectPool>();
          pool.prefab = prefab;
          return pool;
     }
}
