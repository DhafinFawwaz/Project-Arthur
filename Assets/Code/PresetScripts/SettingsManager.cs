using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Linq;
public class SettingsManager : MonoBehaviour
{
    [SerializeField] SliderUI _musicSlider;
    [SerializeField] SliderUI _soundSlider;

    Resolution[] _resolutions;
    [SerializeField] TMP_Dropdown _resolutionDropdown;
    [SerializeField] Toggle _fullscreenToggle;
    void Start()
    {
        _musicSlider.SetValueInstant(Singleton.Instance.Audio.GetMusicVolume());
        _soundSlider.SetValueInstant(Singleton.Instance.Audio.GetSoundVolume());
        
        _musicSlider.OnVariableChanged += OnMusicValueChanged;
        _soundSlider.OnVariableChanged += OnSoundValueChanged;

        _resolutions = Singleton.Instance.Resolution.GetResolutions();
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;
        for(int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " × " + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {currentResolutionIndex = i;}
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        _fullscreenToggle.isOn = Singleton.Instance.Resolution.GetIsFullscreen();
    }

    void OnMusicValueChanged(float newVal)
        => Singleton.Instance.Audio.OnMusicValueChanged(newVal);

    void OnSoundValueChanged(float newVal)
        => Singleton.Instance.Audio.OnSoundValueChanged(newVal);

    public void SetFullScreen(bool isFullScreen)
        => Singleton.Instance.Resolution.SetFullScreen(isFullScreen);
    public void OnResolutionValueChanged(int resolutionIndex)
        => Singleton.Instance.Resolution.SetResolution(resolutionIndex);
}
