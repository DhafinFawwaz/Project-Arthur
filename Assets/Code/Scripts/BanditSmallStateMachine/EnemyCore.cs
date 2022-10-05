using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyCore : MonoBehaviour
{
    public EnemyStats Stats{get{return _stats;} set{_stats = value;}}
    EnemyStats _stats;

    public NavMeshAgent Agent{get{return _agent;} set{_agent = value;}}
    NavMeshAgent _agent;

    public Transform PlayerTrans{get{return _playerTrans;} set{_playerTrans = value;}}
    Transform _playerTrans;

    [SerializeField] public Rigidbody2D Rb{get{return _rb;} set{_rb = value;}}
    Rigidbody2D _rb;
    float _launchedDuration = 0.2f;

    public void Launch(Vector2 launchDirection, float length)
    => StartCoroutine(Launched(launchDirection, length));
    ushort key;
    IEnumerator Launched(Vector2 launchDirection, float length)
    {
        OnHurt();
        _rb.velocity = Vector2.zero;
        Vector3 startPosition = _rb.position;
        Vector3 targetPosition = _rb.position + launchDirection * length;
        float t = 0;
        key++;
        ushort requirement = key;
        while(t <= 1 && requirement == key)
        {
            t += Time.deltaTime/_launchedDuration;
            _rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, Ease.OutQuad(t)));
            yield return null;
        }
        if(requirement == key)_rb.MovePosition(targetPosition);
    }


    public virtual void OnHurt(){}

   
}