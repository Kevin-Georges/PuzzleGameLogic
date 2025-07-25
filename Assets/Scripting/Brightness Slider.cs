using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public TextMeshProUGUI brightnessPercentage;

    private SpriteRenderer[] spriteRenderers;

    public AudioMixer mainMixer;


    private void Start()
    {
        spriteRenderers = FindObjectsOfType<SpriteRenderer>();
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
  
    public void SetFullScreen(bool fulscreen)
    {
        Debug.Log("the game is on fullscreen");
        Screen.fullScreen = fulscreen;
    }

    public void SetQuality(int qualityIndex){
    
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("game quality: " + qualityIndex);
    }

    public void AdjustBrightness(float BrightnessValue)
    {

        brightnessPercentage.text = (BrightnessValue * 100).ToString("F0") + "%";

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = new Color(BrightnessValue, BrightnessValue, BrightnessValue, spriteRenderer.color.a);
        }
    }
 
}
