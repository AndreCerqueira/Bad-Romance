using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI  healthLabel;
    public PlayerStats stats;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Static methods

    static public void SetLabelText(TextMeshProUGUI label, string labelText) => label.text = labelText;

    static public void SetSliderValue(Slider slider, float value) => slider.value = value;

    static public float GetSliderValue(Slider slider) => slider.value;



    public void UpdateHealthSlider() => StartCoroutine(UpdateHealth());


    private IEnumerator UpdateHealth()
    {
        // log
        // SetLabelText(healthLabel, $"Health: {stats.currentHealth}/{stats.maxHealth}");

        //float currentValue = GetSliderValue(healthSlider);
        //float targetValue = stats.currentHealth / stats.maxHealth;

        
        // update new health bar, for each 30 health points missing remove one heart
        int healthPoints = (int)stats.currentHealth;
        int healthPointsMissing = (int)stats.maxHealth - healthPoints;
        int heartsMissing = healthPointsMissing / 30;

        //log
        Debug.Log($"Health points: {healthPoints}");
        Debug.Log($"Health points missing: {healthPointsMissing}");
        Debug.Log($"Hearts missing: {heartsMissing}");

        // disable hearts
        if (heartsMissing >= 1)
        {
            life1.SetActive(false);
        }
        if (heartsMissing >= 2)
        {
            life2.SetActive(false);
        }
        if (heartsMissing >= 3)
        {
            life3.SetActive(false);
        }
            yield return new WaitForFixedUpdate();

/*
        while (currentValue != targetValue)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, 0.25f);
            SetSliderValue(healthSlider, currentValue);
            yield return new WaitForFixedUpdate();
        } 
*/

    }

}
