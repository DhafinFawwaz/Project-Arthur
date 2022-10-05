using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Damage{get{return _damage;}}
    float _damage = 30;
    [SerializeField] PlayerCore _core;
    BlacksmithManager _bm;

    public bool IsInsideBlacksmith = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("BlackSmith"))
        {
            _bm = col.GetComponent<BlacksmithManager>();
            IsInsideBlacksmith = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("BlackSmith"))
        {
            IsInsideBlacksmith = false;
        }
    }
    void Update()
    {
        if(IsInsideBlacksmith)
        {
            if(_core.Input.BuyInput.WasPressedThisFrame())
            {
                _damage += _bm.BuyLevel();
            }
        }
    }

    public AudioClip SlashSFX;
    public AudioClip KillSFX;
}
