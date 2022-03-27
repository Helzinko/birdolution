using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private bool isMusic = false;

    private void Start()
    {
        if (isMusic)
        {
            SettingsManager.instance.ChangeMusicVolume(slider.value);
            slider.onValueChanged.AddListener(x => SettingsManager.instance.ChangeMusicVolume(x));
        }
        else
        {
            SettingsManager.instance.ChangeSFXolume(slider.value);
            slider.onValueChanged.AddListener(x => SettingsManager.instance.ChangeSFXolume(x));
        }
    }
}
