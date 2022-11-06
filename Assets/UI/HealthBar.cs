using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    private void Start()
    {
        slider.value = 1;
        slider.minValue = 0;
    }

    private void OnHealthChange(Fighter fighter, float newHealth, float totalHealth)
    {
        if (fighter.transform != transform.parent.parent)
            return;
        
        slider.value = (newHealth/totalHealth);
    }
    
    private void OnEnable() => Fighter.HealthChangeEvent += OnHealthChange;

    private void OnDisable() => Fighter.HealthChangeEvent -= OnHealthChange;
    
    protected void OnDestroy() => Fighter.HealthChangeEvent -= OnHealthChange;
}
