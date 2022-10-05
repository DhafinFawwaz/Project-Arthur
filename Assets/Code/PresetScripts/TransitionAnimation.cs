using UnityEngine;

public class TransitionAnimation : MonoBehaviour
{
    [SerializeField] RectTransform _orangeSquareRT;
    [SerializeField] GameObject _buttonBlocker;
    [SerializeField] float _startYScale = 0;
    [SerializeField] float _endYScale = 1;
    void Start() => InEnd();
    public void OutStart()
    {
        _buttonBlocker.SetActive(true);
        _orangeSquareRT.pivot = new Vector2(0.5f, 1f);
        OutAnimation(0);
    }
    public void OutAnimation(float t)
    {
        float newY = Mathf.Lerp(_startYScale, _endYScale, Ease.OutQuart(t));
        _orangeSquareRT.localScale = new Vector3(1, newY, 1);
    }
    public void OutEnd() => OutAnimation(1);

    public void InStart()
    {
        _orangeSquareRT.pivot = new Vector2(0.5f, 0f);
        InAnimation(0);
    }
    public void InAnimation(float t)
    {
        float newY = Mathf.Lerp(_endYScale, _startYScale, Ease.OutQuart(t));
        _orangeSquareRT.localScale = new Vector3(1, newY, 1);
    }
    public void InEnd()
    {
        _buttonBlocker.SetActive(false);
        InAnimation(1);
    }
}
