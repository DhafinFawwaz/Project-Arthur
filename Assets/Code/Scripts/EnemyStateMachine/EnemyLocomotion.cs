using System.Collections;
using UnityEngine;

public class EnemyLocomotion : MonoBehaviour
{
    float _launchedDuration = 0.2f;

    public Rigidbody2D Rb{get{return _rb;} set{_rb = value;}}
    Rigidbody2D _rb;

    protected EnemyCore Core{set{_core = value;}}
    EnemyCore _core;


    public void Launch(Vector2 launchDirection, float length)
    => StartCoroutine(Launched(launchDirection, length));
    ushort key;
    IEnumerator Launched(Vector2 launchDirection, float length)
    {
        _rb.velocity = Vector2.zero;
        Vector3 startPosition = _rb.position;
        Vector3 targetPosition = _rb.position + launchDirection * length;
        float t = 0;
        key++;
        ushort requirement = key;
        while(t <= 1 && requirement == key)
        {
            t += Time.deltaTime/_launchedDuration;
            _core.Agent.Warp(Vector3.Lerp(startPosition, targetPosition, Ease.OutQuart(t)));
            yield return null;
        }
        if(requirement == key)
        {
            _core.Agent.Warp(targetPosition);
        }
    }
}