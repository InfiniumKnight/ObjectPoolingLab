using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    private GameObject EmptyHolder;

    private static GameObject GameObjectsEmpty;

    private static Dictionary<GameObject, ObjectPool<GameObject>> ObjectPools;
    private static Dictionary<GameObject, GameObject> CloneToPrefabMap;

    public enum PoolType
    {
        GameObjects
    }
    public static PoolType Poolingtype;

    private void Awake()
    {
        ObjectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        CloneToPrefabMap = new Dictionary<GameObject, GameObject>();

        SetupEmpties();
    }

    private void SetupEmpties()
    {
        EmptyHolder = new GameObject("Object Pools");

        GameObjectsEmpty = new GameObject("GameObjects");
        GameObjectsEmpty.transform.SetParent(EmptyHolder.transform);
    }


    private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObjects)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObject(prefab, pos, rot, poolType),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
            );

        ObjectPools.Add(prefab, pool);
    }

    private static GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObjects)
    {
        prefab.SetActive(false);

        GameObject obj = Instantiate(prefab, pos, rot);

        prefab.SetActive(true);

        GameObject parentObj = SetParentObject(poolType);
        obj.transform.SetParent(parentObj.transform);

        return obj;
    }

    private static void OnGetObject(GameObject obj)
    {

    }

    private static void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private static void OnDestroyObject(GameObject obj)
    {
        if (CloneToPrefabMap.ContainsKey(obj))
        {
            CloneToPrefabMap.Remove(obj);
        }
    }

    private static GameObject SetParentObject(PoolType pooltype)
    {
        switch (pooltype)
        {
            case PoolType.GameObjects:
                return GameObjectsEmpty;
            default:
                return null;
        }
    }

    private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObjects)where T : Object
    {
        if (!ObjectPools.ContainsKey(objectToSpawn))
        {
            CreatePool(objectToSpawn, spawnPos, spawnRot, poolType);
        }

        GameObject obj = ObjectPools[objectToSpawn].Get();

        if (obj != null)
        {
            if (!CloneToPrefabMap.ContainsKey(obj))
            {
                CloneToPrefabMap.Add(obj, objectToSpawn);
            }


            obj.transform.position = spawnPos;
            obj.transform.rotation = spawnRot;

            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }

            T component = obj.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError($"Object {objectToSpawn.name} doesn't have component type {typeof(T)}");
            }

            return component;
        }

        return null;
    }


    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObjects)
    {
        return SpawnObject<GameObject>(objectToSpawn, spawnPos, spawnRot, poolType);
    }

    public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.GameObjects)
    {
        if (CloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
        {
            GameObject parentObject = SetParentObject(poolType);

            if (obj.transform.parent != parentObject.transform)
            {
                obj.transform.SetParent(parentObject.transform);
            }

            if (ObjectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(obj);
            }
        }
        else
        {
            Debug.LogWarning("trying to return an object that is not pool: " + obj.name);
        }
    }
}