using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    private Image barImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    private void Awake()
    {
        GetComponentInParent<UIMana>().OnManaPercentChanged += HandleManaChanged;
    }

    private void HandleManaChanged(float percent)
    {
        StartCoroutine(ChangeToPercent(percent));
    }

    private IEnumerator ChangeToPercent(float percent)
    {
        float preChangePercent = barImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            barImage.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsed / updateSpeedSeconds);
            yield return null;
        }

        barImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
