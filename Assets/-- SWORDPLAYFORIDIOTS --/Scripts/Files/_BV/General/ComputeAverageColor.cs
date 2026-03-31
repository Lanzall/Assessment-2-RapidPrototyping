using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeAverageColor
{
	float addInterval = 0.1f;

	Color[] _colors;
	Color _averageColor;
	int _index;
	float _addTime;

	public Color color { get { return _averageColor; } }

	public ComputeAverageColor(int count)
	{
		_colors = new Color[count];
		Reset();
	}

	void CalculateAverage()
	{
		Vector3 sum = Vector3.zero;
		for (int i = 0 ; i < _colors.Length ; ++i)
		{
			sum.x += _colors[i].r;
			sum.y += _colors[i].g;
			sum.z += _colors[i].b;
		}
		_averageColor = new Color(sum.x / _colors.Length, sum.y / _colors.Length, sum.z / _colors.Length);
	}

	public void Reset()
	{
		_index = 0;
		for (int i = 0 ; i < _colors.Length ; ++i)
			_colors[i] = Color.white;
		CalculateAverage();
	}

	public void AddColor(Color c)
	{
		float now = Time.time;
		if ((now - _addTime) >= addInterval)
		{
			_addTime = now;
			_colors[_index] = c;
			CalculateAverage();
			_index = (_index+1) % _colors.Length;
		}
	}

}
