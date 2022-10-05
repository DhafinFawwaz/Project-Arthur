using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{
    public int Value = 1;
    
    public Transform SkinTrans;
    public Transform ShadowTrans;


    public abstract void OnCollected(Collider2D col);
    void OnTriggerEnter2D(Collider2D col)
    {
        GetComponent<Collider2D>().enabled = false;
        OnCollected(col);
    }

    public float NormalizedParabole(float x) => -4*(0.5f - x)*(0.5f - x) + 1;
    
}
