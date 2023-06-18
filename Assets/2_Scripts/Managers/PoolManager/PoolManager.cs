﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
    {
        [SerializeField] private bool dontDestroyOnLoad;
        
        // List to hold all the pools for the game
        public List<ObjectPool> pools;

        private Dictionary<Pools.Types, ObjectPool> _poolTypeObjectPoolDictionary =
            new Dictionary<Pools.Types, ObjectPool>();

        public static PoolManager Instance;

        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                if(dontDestroyOnLoad) DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // Initialize all the pools
            for (int i = 0; i < pools.Count; i++)
            {
                var pool = pools[i];
                pool.InitializePool();
                if(!_poolTypeObjectPoolDictionary.ContainsKey(pool.poolType))
                    _poolTypeObjectPoolDictionary[pool.poolType] = pool;
                else
                    Debug.LogError($"Pool Type {pool.poolType} Already Exists!");
            }
        }

        /// <summary>
        /// Spawn the next gameobject at its current place 
        /// </summary>
        /// <param name="poolName">Name of the pool to get the gameobject</param>
        /// <returns>Spawned gameobject</returns>
        public GameObject Spawn(Pools.Types poolType, Transform parent = null)
        {
            return Spawn(poolType, null, null, parent);
        }

        public GameObject Spawn(Pools.Types poolType, Vector3? position, Quaternion? rotation, Transform parent = null)
        {
            // Get the pool with the given pool name
            ObjectPool pool = GetObjectPool(poolType);

            if (pool == null)
            {
                //Debug.LogErrorFormat("Cannot find the object pool with name %s", poolName);

                return null;
            }

            // Get the next object from the pool
            GameObject clone = pool.GetNextObject();

            if (clone == null)
            {
                //Debug.LogError("Scene contains maximum number of instances.");

                return null;
            }

            if (parent != null)
            {
                clone.transform.SetParent(parent);
            }

            if (position != null)
            {
                clone.transform.position = (Vector3)position;
            }

            if (rotation != null)
            {
                clone.transform.rotation = (Quaternion)rotation;
            }

            // Spawn the gameobject
            clone.SetActive(true);

            //pool.activeList.Add(clone);

            //pool.passiveList.RemoveAt(pool.passiveList.Count - 1);

            return clone;
        }

        /// <summary>
        /// Spawn the next gameobject from the given pool to the random location between two
        /// vectors and given rotation
        /// </summary>
        /// <param name="poolName">Name of the pool</param>
        /// <param name="minVector">Minimum vector position for the spawned gameobject</param>
        /// <param name="maxVector">Maximum vector position for the spawned gameobject</param>
        /// <param name="rotation">Rotation of the spawned gameobject</param>
        /// <returns>Spawned gameobject</returns>
        public GameObject Spawn(Pools.Types poolType, Vector3 minVector, Vector3 maxVector, Quaternion rotation)
        {
            // Determine the random position
            float x = Random.Range(minVector.x, maxVector.x);
            float y = Random.Range(minVector.y, maxVector.y);
            float z = Random.Range(minVector.z, maxVector.z);
            Vector3 newPosition = new Vector3(x, y, z);

            // Spawn the next gameobject
            return Spawn(poolType, newPosition, rotation);
        }

        /// <summary>
        /// Despawn the given gameobject from the scene
        /// </summary>
        /// <param name="obj">Gameobject to despawn</param>
        public void Despawn(Pools.Types poolType, GameObject obj)
        {
            ObjectPool poolObject = GetObjectPool(poolType);

            obj.transform.SetParent(poolObject.pool.transform);

            //pool.activeList.Remove(obj);

            if (!poolObject.passiveObjectsDictionary.ContainsKey(obj.GetInstanceID()))
            {
                poolObject.passiveObjectsDictionary.Add(obj.GetInstanceID(), obj);
            }

            obj.SetActive(false);
        }

        /// <summary>
        /// Get the object pool reference from the pool list with the given pool name
        /// </summary>
        /// <param name="poolName">Name of the pool</param>
        /// <returns>ObjectPool object with the given name</returns>
        public ObjectPool GetObjectPool(Pools.Types poolType)
        {
            _poolTypeObjectPoolDictionary.TryGetValue(poolType, out var objPool);
            return objPool;
        }
    }
