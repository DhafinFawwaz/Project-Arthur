using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _swordFastestTimeText;
    [SerializeField] Image _swordFinalScoreImg;
    [SerializeField] Sprite[] _finalScoreSprite = new Sprite[4];//0 = A, 1 = B, 2 = C, 3 = D
    SaveData _data;
    void OnEnable()
    {
        _data = Singleton.Instance.Save.Data;
        if(_data.FastestSwordsmanTime < 999999)//9999999
        {
            _swordFastestTimeText.gameObject.SetActive(true);
            _swordFastestTimeText.text = _data.FastestSwordsmanTimeStr;
            _swordFinalScoreImg.sprite = _finalScoreSprite[_data.RankSwordsman];
        }
        else 
        {
            _swordFastestTimeText.gameObject.SetActive(false);
            _swordFinalScoreImg.gameObject.SetActive(false);
        }

    }

    public void PickSwordsman() => StartCoroutine(PickSwordsmanAnimation());

    IEnumerator PickSwordsmanAnimation()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Singleton.Instance.Transition.SetInDefault();
        Singleton.Instance.Scene.LoadSceneWithTransition("TestLevel");
        Singleton.Instance.Transition
            .AddInStart(EnableGame);
    }
    
    void EnableGame()
    {
        Singleton.Instance.Game.gameObject.SetActive(true);
        // Singleton.Instance.Game.GameAwake();
    }
    void LoadTestLevel() => Singleton.Instance.Scene.LoadScene("TestLevel");
}
