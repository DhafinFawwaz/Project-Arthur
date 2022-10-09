using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUI : MonoBehaviour
{
    public Sequence[] AnimationSequence;
    public void Play() => StartCoroutine(PlayAnimation());
    IEnumerator PlayAnimation()
    {
        foreach(Sequence sequence in AnimationSequence)
        {
            switch(sequence.Type)
            {
                case SequenceType.Animation:
                    StartCoroutine(EaseOutRt(sequence.TargetRt, sequence.StartPosition, sequence.EndPosition, sequence.Duration));
                    break;
                
                case SequenceType.Wait:
                    yield return new WaitForSecondsRealtime(sequence.Duration);
                    break;

                case SequenceType.SetButtonBlock:
                    Singleton.Instance.Transition.BlockButton(sequence.IsButtonBlocking);
                    break;

                case SequenceType.SetActive:
                    sequence.Target.SetActive(sequence.IsActivating);
                    break;

                case SequenceType.SFX:
                    Singleton.Instance.Audio.PlaySound(sequence.SFX);
                    break;
            }
        }
    }

    IEnumerator EaseOutRt(RectTransform Rt, Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        float t = 0;
        while (t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            Rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, Ease.OutQuart(t));
            yield return null;
        }
        Rt.anchoredPosition = targetPosition;
    }
    
}
