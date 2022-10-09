using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyCore : MonoBehaviour
{
    public EnemyStats Stats{get{return _stats;} set{_stats = value;}}
    EnemyStats _stats;

    public EnemyLocomotion Locomotion{get{return _locomotion;} set{_locomotion = value;}}
    EnemyLocomotion _locomotion;

    public EnemyAttack Attack{get{return _attack;} set{_attack = value;}}
    EnemyAttack _attack;



    public NavMeshAgent Agent{get{return _agent;} set{_agent = value;}}
    NavMeshAgent _agent;

    public Transform PlayerTrans{get{return _playerTrans;} set{_playerTrans = value;}}
    Transform _playerTrans;
    


    public virtual void OnHurt(){}

   
}