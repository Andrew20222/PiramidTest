using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorController : MonoBehaviour
{
    [SerializeField] private List<Color> colors;
    private List<Color> _actualColors;
    public event Action<List<Color>> ColorsChangedEvent;

    private void Start()
    {
        _actualColors = new List<Color>();
        for (int i = 0; i < 7; i++)
        {
            var randomIndex = Random.Range(0, colors.Count);
            if (_actualColors.Contains(colors[randomIndex]) == false)
            {
                _actualColors.Add(colors[randomIndex]);
            }
            else
            {
                i--;
            }
        }

        ColorsChangedEvent?.Invoke(new List<Color>(_actualColors));
    }
}