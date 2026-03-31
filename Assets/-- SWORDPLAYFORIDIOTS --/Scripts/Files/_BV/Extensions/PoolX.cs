using System.Collections.Generic;
using UnityEngine;

public static class PoolX
{
    public static void AddToPool(GameObject _go, List<GameObject> _pool)
    {
        if (_pool.Contains(_go))
            return;
        _pool.Add(_go);
    }
    
    public static GameObject GetFromPool(GameObject _go, List<GameObject> _pool)
    {
        List<GameObject> available = _pool.FindAll(x=> x.gameObject.name == _go.name);
        if (available.Count == 0 || !GetFromPool(_pool))
        {
            GameObject go = GameObject.Instantiate(_go);
            go.name = _go.name;
            AddToPool(go, _pool);
            return go;
        }
        else
        {
            GameObject go = GetFromPool(_pool);
            go.SetActive(true);
            return go;
        }
    }
    
    public static GameObject GetFromPool(List<GameObject> _pool)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if(!_pool[i].activeSelf)
                return _pool[i];
        }
        return null;
    }
}