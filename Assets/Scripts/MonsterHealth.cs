using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterHealth : MonoBehaviour 
{
    public float startingHealth = 100f;
    public Slider slider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;

    private float currentHealth;

    void Start()
    {
        slider.maxValue = startingHealth;
        currentHealth = startingHealth;

        SetHealthUI();
    }

    void SetHealthUI()
    {
        slider.value = currentHealth;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        SetHealthUI();
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
