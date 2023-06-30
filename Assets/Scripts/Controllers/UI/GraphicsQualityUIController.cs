using UnityEngine;
using TMPro;

public class GraphicQualityController : MonoBehaviour
{

    public TMP_Dropdown graphicsQualityDropdown;
    public int quality;

    // Start is called before the first frame update
    void Start()
    {
        quality = PlayerPrefs.GetInt("qualityNumber", 3);
        graphicsQualityDropdown.value = quality;
        AdjustGraphicsQuality();

    }

    public void AdjustGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsQualityDropdown.value);
        PlayerPrefs.SetInt("qualityNumber", graphicsQualityDropdown.value);
        quality = graphicsQualityDropdown.value;
    }
}
