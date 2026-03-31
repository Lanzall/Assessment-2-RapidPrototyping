using System;
using System.Collections.Generic;

public static class ArrayX 
{
    static public void SetIntArrayValues(int[] _array, int _value)
    {
        for (int i = 0; i < _array.Length; i++)
            _array[i] = _value;
    }
    static public int GetIntArrayTotal(int[] _array)
    {
        int val = 0;
        for (int i = 0; i < _array.Length; i++)
            val += _array[i];

        return val;
    }

    static public T[] ResizeArray<T>(T[] original, int newLength)
    {
        if (original == null || newLength == 0)
            return null;
        var newArray = new T[newLength];
        for (int i = 0; i < Math.Min(newLength, original.Length); i++)
            newArray[i] = original[i];
        return newArray;
    }
    static public T[] ExpandArray<T>(T[] original, int newLength)
    {
        if (original == null)
            return null;
        if (newLength > original.Length)
            return ResizeArray(original, newLength);
        else
            return original;
    }
    //
    // Double index
    static public T[,] ResizeArray<T>(T[,] original, int rows, int cols)
    {
        if (original == null || rows == 0 || cols == 0)
            return null;
        var newArray = new T[rows, cols];
        int minRows = Math.Min(rows, original.GetLength(0));
        int minCols = Math.Min(cols, original.GetLength(1));
        for (int i = 0; i < minRows; i++)
            for (int j = 0; j < minCols; j++)
                newArray[i, j] = original[i, j];
        return newArray;
    }
    static public T[,] ExpandArray<T>(T[,] original, int rows, int cols)
    {
        if (original == null)
            return null;
        if (rows > original.GetLength(0) || cols > original.GetLength(1))
            return ResizeArray(original, rows, cols);
        else
            return original;
    }

    /// <summary>
    /// Gets a random item from an array
    /// </summary>
    /// <typeparam name="T">The type of array we are passing in</typeparam>
    /// <param name="_array">The array of items</param>
    /// <returns></returns>
    static public T GetRandomItemFromArray<T>(T[] _array)
    {
        int index = UnityEngine.Random.Range(0, _array.Length);
        return _array[index];
    }
    
    /// <summary>
    /// Increments a counter in an array and loops back to the start when it reaches the end
    /// </summary>
    /// <param name="_current">The variable holding the current value</param>
    /// <param name="_array">The array to increment through</param>
    /// <returns></returns>
    static public int IncrementCounter<T>(int _current, T[] _array)
    {
        return _current == _array.Length - 1 ? 0 : _current + 1;
    }

    /// <summary>
    /// Decrements a counter in an array and loops back to the end when it reaches the start
    /// </summary>
    /// <param name="_current">The variable holding the current value</param>
    /// <param name="_array">The array to decrement through</param>
    /// <returns></returns>
    static public int DecrementCounter<T>(int _current, T[] _array)
    {
        return _current == 0 ? _array.Length - 1 : _current - 1;
    }
}
