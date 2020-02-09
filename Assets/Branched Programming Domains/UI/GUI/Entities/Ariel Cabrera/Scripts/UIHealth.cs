using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    [SerializeField]
    private Image barImage;

    public event Action<float> OnHealthPercentChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        OnHealthPercentChanged(currentHealthPercent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ModifyHealth(-10);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            ModifyHealth(10);

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth <= maxHealth*.66)
            barImage.color = Color.yellow;

        if (currentHealth <= maxHealth*.33)
            barImage.color = Color.red;

        if (currentHealth >= maxHealth * .66)
            barImage.color = Color.green;
    }
}
