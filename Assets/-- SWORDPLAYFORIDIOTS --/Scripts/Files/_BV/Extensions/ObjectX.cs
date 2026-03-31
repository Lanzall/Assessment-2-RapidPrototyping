using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;

public static class ObjectX
{
    /// <summary>
    /// Toggles active or inactive all GameObjects in a list
    /// </summary>
    /// <param name="gos">The list of objects to toggle</param>
    /// <param name="state">The state to toggle the objects (true/false)</param>
    static public void ToggleObjects(List<GameObject> gos, bool state)
    {
        for (int i = 0; i < gos.Count; i++)
            gos[i].SetActive(state);
    }

    /// <summary>
    /// Toggles active or inactive all GameObjects in an array
    /// </summary>
    /// <param name="gos">The array of objects to toggle</param>
    /// /// <param name="state">The state to toggle the objects (true/false)</param>
    static public void ToggleObjects(GameObject[] gos, bool state)
    {
        for (int i = 0; i < gos.Length; i++)
            gos[i].SetActive(state);
    }

    /// <summary>
    /// Scales a GameObject to zero
    /// </summary>
    /// <param name="_go">The GameObject to scale</param>
    static public void ScaleObjectToZero(GameObject _go)
    {
        _go.transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// Scales all GameObjects in a list to zero
    /// </summary>
    /// <param name="gos">The list of objects to scale</param>
    static public void ScaleObjectsToZero(List<GameObject> gos)
    {
        for (int i = 0; i < gos.Count; i++)
            gos[i].transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// Scales all GameObjects in an array to zero
    /// </summary>
    /// <param name="gos">The array of objects to scale</param>
    static public void ScaleObjectsToZero(GameObject[] gos)
    {
        for (int i = 0; i < gos.Length; i++)
            gos[i].transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// Scales all GameObjects in a list to zero
    /// </summary>
    /// <param name="_gos">The list of objects to scale</param>
    static public void ScaleObjectsToZero(List<GameObject> _gos, Vector3 _axis)
    {
        for (int i = 0; i < _gos.Count; i++)
            _gos[i].transform.localScale = _axis;
    }

    /// <summary>
    /// Rotates a GameObject to zero
    /// </summary>
    /// <param name="_go">The GameObject to rotate</param>
    static public void RotateObjectToZero(GameObject _go)
    {
        _go.transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// Rotates all GameObjects in a list to zero
    /// </summary>
    /// <param name="gos">The list of objects to rotate</param>
    static public void RotateObjectsToZero(List<GameObject> gos)
    {
        for (int i = 0; i < gos.Count; i++)
            gos[i].transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// Rotates all GameObjects in an array to zero
    /// </summary>
    /// <param name="gos">The array of objects to rotate</param>
    static public void RotateObjectsToZero(GameObject[] gos)
    {
        for (int i = 0; i < gos.Length; i++)
            gos[i].transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// Fades all GameObjects in a list to zero
    /// </summary>
    /// <param name="gos">The list of objects to fade</param>
    static public void FadeObjectsToZero(List<GameObject> gos, float FadeTime = 0)
    {
        for (int i = 0; i < gos.Count; i++)
        {
            Color temp = gos[i].GetComponent<Renderer>().material.color;
            temp.a = 0;
            gos[i].GetComponent<Renderer>().material.color = temp;
        }
    }

    /// <summary>
    /// Get the count of all inactive objects in a list
    /// </summary>
    /// <param name="gos">The list to find the inactive objects</param>
    static public int GetInactiveCount(List<GameObject> gos)
    {
        return gos.Count(obj => !obj.activeInHierarchy);
    }
    
    /// <summary>
    /// Get the count of all active objects in a list
    /// </summary>
    /// <param name="gos">The list to find the active objects</param>
    static public int GetActiveCount(List<GameObject> gos)
    {
        return gos.Count(obj => obj.activeInHierarchy);
    }
    
    /// <summary>
    /// Gets the next object in a list that is inactive. If all are active will return the last item.
    /// </summary>
    /// <param name="_list">The list of objects to go through</param>
    /// <returns>The next available inactive object</returns>
    static public GameObject GetNextInactive(List<GameObject> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (!_list[i].activeInHierarchy)
                return _list[i];
        }
        return _list[_list.Count - 1];
    }
    
    /// <summary>
    /// Gets the next object in a list that is inactive. If all are active will return the last item.
    /// </summary>
    /// <param name="_array">The list of objects to go through</param>
    /// <returns>The next available inactive object</returns>
    static public GameObject GetNextInactive(GameObject[] _array)
    {
        for (int i = 0; i < _array.Length; i++)
        {
            if (!_array[i].activeInHierarchy)
                return _array[i];
        }
        return _array[_array.Length - 1];
    }

    //
    // Copy component to another GameObject
    // http://answers.unity3d.com/answers/589400/view.html
    static public T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        CopyComponentFields(original, copy);
        return copy as T;
    }
    static public void CopyComponentFields<T>(T original, T copy) where T : Component
    {
        System.Type type = original.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        //Debug.Log("Fields ["+fields.Length+"]");
        foreach (FieldInfo field in fields)
        {
            //Debug.Log("Field ["+field+"]");
            field.SetValue(copy, field.GetValue(original));
        }
    }

    //
    // Copy component to another GameObject
    // http://answers.unity3d.com/answers/589400/view.html
    static public T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (comp == null)
            comp = obj.AddComponent<T>();
        return comp;
    }

    /// <summary>
    /// Sets all children of an objects layers to a new layer
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="layer"></param>
    public static void SetLayerRecursively(this Transform parent, int layer)
    {
        parent.gameObject.layer = layer;

        for (int i = 0, count = parent.childCount; i < count; i++)
        {
            parent.GetChild(i).SetLayerRecursively(layer);
        }
    }

    /// <summary>
    /// Gets the closest object from a list of objects
    /// </summary>
    /// <param name="_this">The object to measure from</param>
    /// <param name="_objectList">The list of objects to check</param>
    /// <returns>The closest object in the object list</returns>
    static public GameObject GetClosest<T>(GameObject _this, List<T> _objectList) where T : Component
    {
        if (_objectList == null || _objectList.Count == 0)
            return null;

        float closestDistance = Mathf.Infinity;
        GameObject closest = null;

        foreach (T item in _objectList)
        {
            float currentDistance = Vector3.Distance(_this.transform.position, item.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closest = item.gameObject;
            }
        }
        return closest;
    }
    

    /// <summary>
    /// Gets the closest object from an array of objects
    /// </summary>
    /// <param name="_this">The object to measure from</param>
    /// <param name="_objectList">The array of objects to check</param>
    /// <returns>The closest object in the object list</returns>
    static public GameObject GetClosest<T>(GameObject _this, T[] _objectArray) where T : Component
    {
        if (_objectArray == null || _objectArray.Length == 0)
            return null;

        float closestDistance = Mathf.Infinity;
        GameObject closest = null;

        foreach (T item in _objectArray)
        {
            float currentDistance = Vector3.Distance(_this.transform.position, item.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closest = item.gameObject;
            }
        }
        return closest;
    }
    
    /// <summary>
    /// Gets the closest object from a list of objects
    /// </summary>
    /// <param name="_this">The object to measure from</param>
    /// <param name="_objectList">The list of objects to check</param>
    /// <returns>The closest object in the object list</returns>
    static public GameObject GetClosest(GameObject _this, List<GameObject> _objectList)
    {
        if (_objectList == null || _objectList.Count == 0)
            return null;
        
        float closestDistance = Mathf.Infinity;
        GameObject closest = null;

        for (int i = 0; i < _objectList.Count; i++)
        {
            float currentDistance = Vector3.Distance(_this.transform.position, _objectList[i].transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closest = _objectList[i];
            }
        }
        return closest;
    }
    
    /// <summary>
    /// Gets the closest object from an array of objects
    /// </summary>
    /// <param name="_this">The object to measure from</param>
    /// <param name="_objectList">The array of objects to check</param>
    /// <returns>The closest object in the object list</returns>
    static public GameObject GetClosest(GameObject _this, GameObject[] _objectList)
    {
        if (_objectList == null || _objectList.Length == 0)
            return null;

        float closestDistance = Mathf.Infinity;
        GameObject closest = null;

        for (int i = 0; i < _objectList.Length; i++)
        {
            float currentDistance = Vector3.Distance(_this.transform.position, _objectList[i].transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closest = _objectList[i];
            }
        }
        return closest;
    }
    
    /// <summary>
    /// Gets the closest object from an array of objects based on a tag
    /// </summary>
    /// <param name="_this">The object to measure from</param>
    /// <param name="_objectTag">The tag of the objects you want to find</param>
    /// <returns>The closest object that matches the tag</returns>
    static public GameObject GetClosest(GameObject _this, string _objectTag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(_objectTag);
        if (objects == null || objects.Length == 0)
            return null;

        float closestDistance = Mathf.Infinity;
        GameObject closest = null;

        for (int i = 0; i < objects.Length; i++)
        {
            float currentDistance = Vector3.Distance(_this.transform.position, objects[i].transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closest = objects[i];
            }
        }
        return closest;
    }
}