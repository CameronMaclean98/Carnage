using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMana : MonoBehaviour
{
    [SerializeField]
    private int maxMana = 100;

    private int currentMana;

    [SerializeField]
    private Image barImage;

    public event Action<float> OnManaPercentChanged = delegate { };

    private void OnEnable()
    {
        currentMana = maxMana;
    }

    public void ModifyMana(int amount)
    {
        currentMana += amount;

        float currentManaPercent = (float)currentMana / (float)maxMana;
        OnManaPercentChanged(currentManaPercent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            ModifyMana(-20);

        if (currentMana > maxMana)
            currentMana = maxMana;

        if (currentMana < 0)
            currentMana = 0;
    }
}
