using UnityEngine;

using System;

using System.Collections;

using System.Collections.Generic;

 

public class GameObjectPoolManager {

 
	/*
private static List _PoolObjectList = new List();

 

public static PoolObjectModel AddNewPoolObject(GameObject prefab, uint initialCapacity)

{

PoolObjectModel newPoolObject = new PoolObjectModel();

 

newPoolObject.Prefab = prefab;

newPoolObject.PrefabName = prefab.name + "(Clone)";

newPoolObject.All = (initialCapacity > 0) ? new ArrayList((int) initialCapacity) : new ArrayList();

newPoolObject.Available = (initialCapacity > 0) ? new Stack((int) initialCapacity) : new Stack();

 

_PoolObjectList.Add(newPoolObject);

 

return newPoolObject;

}

 

public static int numActive (GameObject prefab)

{

PoolObjectModel poolObject = GetPoolObjectByPrefab(prefab);

 

if(poolObject == null)

return 0;

else

return poolObject.All.Count - poolObject.Available.Count;

}

 

public static int numAvailable (GameObject prefab)

{

PoolObjectModel poolObject = GetPoolObjectByPrefab(prefab);

 

if(poolObject == null)

return 0;

else

return poolObject.Available.Count;

}

 

public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)

{

GameObject result;

PoolObjectModel currentPoolObject = new PoolObjectModel();

 

if(DoesPoolObjectExist(prefab))

{

currentPoolObject = GetPoolObjectByPrefab(prefab);

//Debug.Log(“Old Pool ” + currentPoolObject.PrefabName );

}

else

{

currentPoolObject = AddNewPoolObject(prefab, 3);

//Debug.Log(“New Pool ” + currentPoolObject.PrefabName);

}

 

if (currentPoolObject.Available.Count == 0)

{

// create an object and initialize it.

//Debug.Log(“Instantiate new object” + currentPoolObject.PrefabName);

result = GameObject.Instantiate(prefab, position, rotation) as GameObject;

 

currentPoolObject.All.Add(result);

 

}

else

{

//Debug.Log(“Reuse Old object” + currentPoolObject.PrefabName);

result = currentPoolObject.Available.Pop() as GameObject;

 

// get the result’s transform and reuse for efficiency.

// calling gameObject.transform is expensive.

if(result == null)

{

result = GameObject.Instantiate(prefab, position, rotation) as GameObject;

}

 

Transform resultTrans = result.transform;

resultTrans.position = position;

resultTrans.rotation = rotation;

 

SetActive(result, true);

 

//Debug.Log(“Is Active: ” + result.active);

}

 

return result;

}

 

public static bool Destroy(GameObject target)

{

PoolObjectModel currentPoolObject = GetPoolObjectByPrefab(target);

 

if(DoesPoolObjectExist(target))

{

if (!currentPoolObject.Available.Contains(target))

{

currentPoolObject.Available.Push(target);

 

SetActive(target, false);

//Debug.Log(“Is Active: ” + target.active);

return true;

}

}

else

{

Destroy(target);

return true;

}

 

return false;

}

 

public static void DestroyAll()

{

foreach(PoolObjectModel poolObject in _PoolObjectList)

{

for (int i=0; i<poolObject.All.Count; i++)

{

GameObject target = poolObject.All[i] as GameObject;

 

if (target.active)

Destroy(target);

}

}

}

 

// Unspawns all the game objects and clears the pool.

public static void Clear() {

DestroyAll();

foreach(PoolObjectModel poolObject in _PoolObjectList)

{

poolObject.Available.Clear();

poolObject.All.Clear();

}

}

 

public static bool DoesPoolObjectExist(GameObject prefabObject)

{

bool prefabExists = false;

string prefabName;

 

if(prefabObject.name.Contains("(Clone)"))

prefabName = prefabObject.name;

else

prefabName = prefabObject.name + "(Clone)";

 

foreach(PoolObjectModel poolObject in _PoolObjectList)

{

if ( poolObject.PrefabName == prefabName)

prefabExists = true;

}

 

return prefabExists;

}

 

protected static void SetActive(GameObject target, bool active)

{

 

RecursiveDeepActivation(target.transform, active);

 

}

 

public static PoolObjectModel GetPoolObjectByPrefab(GameObject prefabObject)

{

string prefabName;

 

if(prefabObject.name.Contains("(Clone)"))

prefabName = prefabObject.name;

else

prefabName = prefabObject.name + "(Clone)";

 

PoolObjectModel poolObjectToReturn = new PoolObjectModel();

foreach(PoolObjectModel poolObject in _PoolObjectList)

{

if ( poolObject.PrefabName == prefabName)

poolObjectToReturn = poolObject;

}

 

return poolObjectToReturn;

}

 

protected static void RecursiveDeepActivation(Transform gameObjectTransform, bool activate)

{

foreach (Transform t in gameObjectTransform)

{

RecursiveDeepActivation (t, activate);

}

gameObjectTransform.gameObject.active = activate;

}
	 
	 */
}