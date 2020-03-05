using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool 
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake() {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        { 
          Queue<GameObject> objectPool = new Queue<GameObject>();

          for(int i = 0; i < pool.size; i++) {
              GameObject obj = Instantiate(pool.prefab);
              obj.SetActive(false);
              objectPool.Enqueue(obj);
          }  

          poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag) 
    {
        Debug.Log("SpawnFromPool with tag " + tag);

        if(!poolDictionary.ContainsKey(tag))  {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exists.");
            return null;
        }

        if(poolDictionary[tag].Count == 0) {
            GrowQueue(tag);
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);

        // Each Pooled object has the relevant intreface implementing Script located in a child at index 0 called "Body".
        IPooledObject pooledObj = objectToSpawn.transform.GetChild(0).gameObject.GetComponent<IPooledObject>();
        Debug.Log(pooledObj);
        if(pooledObj != null) { 
            Debug.Log("PooledObj not null!");
            pooledObj.OnObjectSpawn(); 
        }

        return objectToSpawn;
    }

    private void GrowQueue(string tag)
    {
        foreach(Pool pool in pools) {
            if(pool.tag == tag) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                poolDictionary[tag].Enqueue(obj);
                return;
            }
        }
    }

    public void AddToPool(string tag, GameObject instance) 
    {
        instance.SetActive(false);
        poolDictionary[tag].Enqueue(instance);
    }
}
