using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListX 
{
    /// <summary>
    /// Gets a set of random items from a list
    /// </summary>
    /// <typeparam name="T">The type of list we are passing in</typeparam>
    /// <param name="list">The list of items</param>
    /// <param name="number">The amount we want returned in the new list</param>
    /// <returns></returns>
    static public List<T> GetRandomItemsFromList<T>(List<T> list, int number)
    {
        // this is the list we're going to remove picked items from
        List<T> tmpList = new List<T>(list);
        // this is the list we're going to move items to
        List<T> newList = new List<T>();

        // make sure tmpList isn't already empty
        while (newList.Count < number && tmpList.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, tmpList.Count);
            newList.Add(tmpList[index]);
            tmpList.RemoveAt(index);
        }

        return newList;
    }

    /// <summary>
    /// Gets a random item from a list
    /// </summary>
    /// <typeparam name="T">The type of list we are passing in</typeparam>
    /// <param name="list">The list of items</param>
    /// <returns></returns>
    static public T GetRandomItemFromList<T>(List<T> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }

    /// <summary>
    /// Finds an element in a string list that matches the _toFind parameter
    /// </summary>
    /// <param name="_list">The list to search through</param>
    /// <param name="_toFind">The string to search for</param>
    /// <returns>Returns an element that contains the string to be found, else return empty string</returns>
    static public string FindElementInList(List<string> _list, string _toFind)
    {
        if (_list.Find(x => x.Contains(_toFind)) != null)
            return _list.Find(x => x.Contains(_toFind));
        else
            return "";
    }

    /// <summary>
    /// Removes all game objects from a list that have a specific script on them
    /// </summary>
    /// <param name="_objectsList">The list of objects</param>
    /// <param name="_scriptType">The script to check</param>
    static public void RemoveGameObjectsWithScript(List<GameObject> _objectsList, System.Type _scriptType)
    {
        _objectsList.RemoveAll(obj => obj != null && obj.GetComponent(_scriptType) != null);
    }

    /// <summary>
    /// Shuffles a list using Unity's Random
    /// </summary>
    /// <typeparam name="T">The data type</typeparam>
    /// <param name="_list">The list to shuffle</param>
    /// <returns></returns>
    static public List<T> ShuffleList<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = UnityEngine.Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }
        return _list;
    }

    /// <summary>
    /// Resizes a list to a specific size
    /// </summary>
    /// <typeparam name="T">The list to resize</typeparam>
    /// <param name="list">The list to resize</param>
    /// <param name="size">How big the list should be</param>
    /// <param name="element"></param>
    /// <returns></returns>
    static public List<T> ResizeList<T>(List<T> list, int size, T element = default(T))
    {
        int count = list.Count;

        if (size < count)
        {
            list.RemoveRange(size, count - size);
        }
        else if (size > count)
        {
            if (size > list.Capacity)   // Optimization
                list.Capacity = size;

            list.AddRange(Enumerable.Repeat(element, size - count));
        }
        return list;
    }

    /// <summary>
    /// Moves an item to the front of a list
    /// https://stackoverflow.com/questions/2575592/moving-a-member-of-a-list-to-the-front-of-the-list
    /// </summary>
    /// <typeparam name="T">The data type of the list</typeparam>
    /// <param name="list">The list</param>
    /// <param name="index">The index of the item to move</param>
    static public void MoveItemAtIndexToFront<T>(List<T> list, int index)
    {
        T item = list[index];
        for (int i = index; i > 0; i--)
            list[i] = list[i - 1];
        list[0] = item;
    }

    /// <summary>
    /// Increments a counter in a list and loops back to the start when it reaches the end
    /// </summary>
    /// <param name="_current">The variable holding the current value</param>
    /// <param name="_list">The list to increment through</param>
    /// <returns></returns>
    static public int IncrementCounter<T>(int _current, List<T> _list)
    {
        return _current == _list.Count - 1 ? 0 : _current + 1;
    }

    /// <summary>
    /// Decrements a counter in a list and loops back to the end when it reaches the start
    /// </summary>
    /// <param name="_current">The variable holding the current value</param>
    /// <param name="_list">The list to increment through</param>
    /// <returns></returns>
    static public int DecrementCounter<T>(int _current, List<T> _list)
    {
        return _current == 0 ? _list.Count - 1 : _current - 1;
    }

    /// <summary>
    /// Decrements a counter in a list and loops back to the end when it reaches the start
    /// </summary>
    /// <param name="_current">The variable holding the current value</param>
    /// <param name="_list">The list to increment through</param>
    /// <returns></returns>
    static public int CycleCounter<T>(int _current, List<T> _list)
    {
        if (_current >= _list.Count)
            _current = 0;
        else if (_current <= -1)
            _current = _list.Count - 1;

        return _current;
    }

    /// <summary>
    /// Gets a random int within the count of a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_list">The list to use as max value</param>
    /// <returns>A random int</returns>
    static public int RandomInt<T>(List<T> _list)
    {
        return UnityEngine.Random.Range(0, _list.Count);
    }

    /// <summary>
    /// Destroys an element in a list and removes it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_objects"></param>
    /// <param name="_object"></param>
    static public void DestroyObject<T>(List<T> _objects, T _object)
    {
        for(int i=0; i < _objects.Count; i++)
        {
            //if (_objects[i] == _object)
        }
    }

    /// <summary>
    /// Destroys all objects in a list and then clears the list
    /// </summary>
    /// <param name="_objects">The list of objects to Destroy</param>
    static public void DestroyList(List<GameObject> _objects)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            if (Application.isEditor && !Application.isPlaying)
                UnityEngine.Object.DestroyImmediate(_objects[i]);
            else
                UnityEngine.Object.Destroy(_objects[i]);
        }
        _objects.Clear();
    }
}