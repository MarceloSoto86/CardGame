using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image panelBrightness;

    // Start is called before the first frame update
    void Start()
    {

        slider.value = PlayerPrefs.GetFloat("brightness", 0.5f);

        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, slider.value);


    }

    public void ChangeSlider(float valor)
    {

        sliderValue = valor;
        PlayerPrefs.SetFloat("brightness", sliderValue);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, slider.value);

    }

}

