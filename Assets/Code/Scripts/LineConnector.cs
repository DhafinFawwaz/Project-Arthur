using UnityEngine;

[ExecuteInEditMode]
public class LineConnector : MonoBehaviour
{
    [SerializeField] LineRenderer _line;
    [SerializeField] Transform _targetPosition;

    void Start() => _line = GetComponent<LineRenderer>();

    void Update()
    {
        if(_line == null || _targetPosition == null)return;
        _line.SetPosition(1, _targetPosition.position);
        _line.SetPosition(0, transform.position);
    }
}
