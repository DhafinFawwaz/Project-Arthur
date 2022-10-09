using UnityEngine;
[System.Serializable]
public class Sequence
{
    public string AtTime;
    public SequenceType Type;
    
    public RectTransform TargetRt;
    public Vector2 CurrentTargetRtAnchoredPosition{
        get{return TargetRt.anchoredPosition;}
    }
    
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public float Duration;

    public bool IsButtonBlocking;

    public GameObject Target;
    public bool IsActivating;

    public AudioClip SFX;


}
