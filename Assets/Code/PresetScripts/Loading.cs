using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float _progress;
    [SerializeField] Image _loadingFillImg;

    void OnValidate()
    {
        Fill(_progress);
    }

    public void Fill(float progress)
    {
        _loadingFillImg.fillAmount = Mathf.Lerp(0, 1, progress);
    }
}
