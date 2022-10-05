using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // [Serializable] class Hitbox
    // {
    //     [SerializeField] Transform _hitboxStart;
    //     [SerializeField] Transform _hitboxEnd;
    //     [SerializeField] float _radius = 3;
    //     // [SerializeField] float _distance = 2;
    //     public Transform HitboxStart {get{return _hitboxStart;}}
    //     public Transform HitboxEnd {get{return _hitboxEnd;}}
    //     public float Radius {get{return _radius;}}
    //     // public float Distance {get{return _distance;}}
    // }
    [Serializable] public struct Hitbox
    {
        [SerializeField] Transform _hitboxTrans;
        [SerializeField] float _radius;
        // [SerializeField] float _distance = 2;
        public Transform HitboxTrans {get{return _hitboxTrans;}}
        public float Radius {get{return _radius;} set{_radius = value;}}
    }
    [SerializeField] LayerMask _layerMask;
    [SerializeField] int raycastSize = 5;

    [SerializeField] Hitbox[] _hitboxes;
    public Collider2D[] EnableHitbox()
    {
        Collider2D[] hit = new Collider2D[raycastSize];
        foreach(Hitbox hitbox in _hitboxes)
        {
            if(hitbox.HitboxTrans.gameObject.activeSelf)
            Physics2D.OverlapCircleNonAlloc(hitbox.HitboxTrans.position, hitbox.Radius,
                hit, _layerMask);
        }
        return hit;
    }
}
