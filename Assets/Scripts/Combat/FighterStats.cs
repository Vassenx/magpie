using UnityEngine;

public class FighterStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _baseDamage;
    
    public float maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = Mathf.Max(0, value); }
    }

    public float baseDamage
    {
        get { return _baseDamage; }
        set { _baseDamage = Mathf.Max(0, value); }
    }

    private void Start()
    {
        // Hack: Unity doesn't allow properties with built in getters/setters in the inspector
        maxHealth = _maxHealth;
        baseDamage = _baseDamage;
    }
}
