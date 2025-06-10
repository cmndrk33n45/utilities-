using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class CachingSystem<T> where T : MonoBehaviour
{
    private GameObject prefab;

    private Queue<T> cache;
    public List<T> active;

    private int count;
    private int increment = 25;

    public CachingSystem(GameObject prefab, int increment)
    {
        this.prefab = prefab;
        this.increment = increment;

        cache = new Queue<T>();
        active = new List<T>();

        Expand();
    }

    // add item to cache, remove from acive if applicable
    public void Cache(T item)
    {
        if (active.Contains(item)) active.Remove(item);
        cache.Enqueue(item);
        item.gameObject.SetActive(false);
    }

    //pull inactive projectile from a cache
    public T TakeFromCache()
    {
        if (cache.Count == 0) Expand(); // if all objects are uncached, expand the cache

        T item = cache.Dequeue();
        active.Add(item);
        item.gameObject.SetActive(true);

        return item;
    }

    //expands the cache, dependant on managed object adding itself to cache through manager object.
    public void Expand()
    {
        for (int i = 0; i < increment; i++)
        {
            GameObject.Instantiate(prefab);
        }
    }

    //returns the amount of cached objects
    public int CacheSize()
    {
        return cache.Count;
    }

    //returns the total amount of all managed objects
    public int TotalSize()
    {
        return cache.Count + active.Count;
    }

    //used in the manager class to cull cache overruns.
    public IEnumerator ManageCache()
    {
        while (true)
        {
            if (cache.Count > increment)
            {
                T item = cache.Dequeue();
                GameObject.Destroy(item);
            }
            yield return new WaitForSeconds(3);
        }
    }
}
