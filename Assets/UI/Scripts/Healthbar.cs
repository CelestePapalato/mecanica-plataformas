using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] 
    private HealthComponent _healthComponent;
    private Slider _slider;

    void Awake()
    {
        if (!_healthComponent)
        {
            Destroy(gameObject);
        }
        _slider = GetComponent<Slider>();
        _slider.maxValue = _healthComponent.MaxHealth;
        _slider.value = _healthComponent.Health;
        _healthComponent.HealthUpdate += UpdateSlider;
    }

    void UpdateSlider(int value)
    {
        _slider.value = value;
    }
}
