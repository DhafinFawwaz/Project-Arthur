using System;
using UnityEngine;

public class EnemyBanditSmallAttack : MonoBehaviour
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
        [SerializeField] Transform _hitboxStart;
        [SerializeField] Transform _hitboxEnd;
        [SerializeField] float _radius;
        // [SerializeField] float _distance = 2;
        public Transform HitboxStart {get{return _hitboxStart;}}
        public Transform HitboxEnd {get{return _hitboxEnd;}}
        public float Radius {get{return _radius;} set{_radius = value;}}
    }
    [SerializeField] LayerMask _layerMask;
    [SerializeField] int raycastSize = 5;

    [SerializeField] Hitbox[] _hitboxes;
    public RaycastHit2D[] EnableHitbox()
    {
        RaycastHit2D[] hit = new RaycastHit2D[raycastSize];
        foreach(Hitbox hitbox in _hitboxes)
        {
            // if(hitbox == null)break;
            Vector2 direction = hitbox.HitboxEnd.position - hitbox.HitboxStart.position;
            Physics2D.CircleCastNonAlloc(hitbox.HitboxStart.position, hitbox.Radius,
                direction.normalized, hit, direction.magnitude, _layerMask);
            // _hit = Physics2D.CircleCastAll(hitbox.HitboxStart.position, hitbox.Radius,
            //     direction.normalized, direction.magnitude, _layerMask);
        }
        return hit;
    }
}
