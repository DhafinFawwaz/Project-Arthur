using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] RectTransform _mapRT;
    [SerializeField] RectTransform _playerPinRt;
    Vector2 _mapSize;
    Vector2 _worldSize = new Vector2(92, 64);
    Transform _playerTrans;
    
    //46 x 32 -> 184 x 128, but 46 x 32 is after the grid scaled to 2, so its 92 x 64 -> 184 x 128

    public void OnSceneLoaded() //void Start()
    {
        _playerTrans = Singleton.Instance.Game.Core.transform;
        _mapSize = _mapRT.sizeDelta;
    }

    public void GameUpdate() //void Update()
    {
        float xPos = Global.Remap(_playerTrans.position.x, 
            -_worldSize.x/2, _worldSize.x/2, 
            -_mapSize.x/2, _mapSize.x/2
        );
        float yPos = Global.Remap(_playerTrans.position.y, 
            -_worldSize.y/2, _worldSize.y/2, 
            -_mapSize.y/2, _mapSize.y/2
        );

        _playerPinRt.anchoredPosition = new Vector2(xPos, yPos);
    }

}
