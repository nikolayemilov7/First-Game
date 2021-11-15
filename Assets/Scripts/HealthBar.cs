using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    /*[SerializeField]
    private Image _filler;
    [SerializeField]
    private Gradient _gradient;*/

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;

        //_filler.color = _gradient.Evaluate(1);
    }

    public void SetHealth(int health)
    {
        _slider.value = health;
        //_filler.color = _gradient.Evaluate(_slider.normalizedValue);
    }

}
