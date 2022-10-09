using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationUI))]
public class AnimationUIDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        AnimationUI animationUI = (AnimationUI)target;

        if(animationUI.AnimationSequence == null) //Prevent error when adding component
        {
            DrawDefaultInspector();
            return;
        }

        float _currentTime = 0;
        foreach(Sequence sequence in animationUI.AnimationSequence)
        {
            sequence.AtTime = "At "+_currentTime.ToString() + " seconds";
            if(sequence.Type == SequenceType.Wait)
                _currentTime += sequence.Duration;
        }

        DrawDefaultInspector();
    }

}
