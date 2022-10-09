using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Sequence))]
public class SequenceDrawer : PropertyDrawer
{
    int _height = 18;
    int _spacing = 2;
    int _vector2ExtraPush = 0;

    int _colorX = 19;
    int _colorWidthMinus = 38;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var type = property.FindPropertyRelative("Type");
        switch((SequenceType)type.intValue)
        {
            case SequenceType.Animation:
                return (_height + _spacing) * 7 + 6 + _vector2ExtraPush + _vector2ExtraPush;
            case SequenceType.Wait:
                return (_height + _spacing) * 3 + 6;
            case SequenceType.SetButtonBlock:
                return (_height + _spacing) * 3 + 6;
            case SequenceType.SetActive:
                return (_height + _spacing) * 4 + 6;
            case SequenceType.SFX:
                return (_height + _spacing) * 3 + 6;
        }

        return (_height + _spacing) * 6 + 6; //Not gonna happen
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var labelPosition = new Rect(position.x, position.y, position.width, _height);
        EditorGUI.LabelField(labelPosition, label);

        if(EditorGUIUtility.currentViewWidth < 330)//Check if vector2 field pushed down
        {
            _vector2ExtraPush = 20;
        }else _vector2ExtraPush = 0;

        var first = new Rect(position.x, position.y + 20, position.width, _height);
        var second = new Rect(position.x, position.y + 40, position.width, _height);
        var third = new Rect(position.x, position.y + 60, position.width, _height);
        var fourth = new Rect(position.x, position.y + 80 + _vector2ExtraPush, position.width, _height);
        var fifth = new Rect(position.x, position.y + 100 + _vector2ExtraPush + _vector2ExtraPush, position.width, _height);
        var sixth = new Rect(position.x, position.y + 120 + _vector2ExtraPush + _vector2ExtraPush, position.width, _height);

        var type = property.FindPropertyRelative("Type");
        

        switch((SequenceType)type.intValue)
        {
            case SequenceType.Animation:
                EditorGUI.DrawRect(new Rect(
                    _colorX, 
                    position.y, 
                    EditorGUIUtility.currentViewWidth - _colorWidthMinus, 
                    (_height + _spacing) * 7 + 6 + _vector2ExtraPush + _vector2ExtraPush)
                    , new Color(1, 0, 0, 0.05f)
                );
                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(first, property.FindPropertyRelative("Type"));
                EditorGUI.PropertyField(second, property.FindPropertyRelative("TargetRt"));
                EditorGUI.PropertyField(third, property.FindPropertyRelative("StartPosition"));
                EditorGUI.PropertyField(fourth, property.FindPropertyRelative("EndPosition"));
                EditorGUI.PropertyField(fifth, property.FindPropertyRelative("Duration"));
                
                
                if (GUI.Button(new Rect(
                    third.x, 
                    third.y, 
                    95, 
                    20), 
                    "Start Position")) 
                {
                    RectTransform serializedValue = property.FindPropertyRelative("TargetRt").GetSerializedValue<RectTransform>();
                    property.FindPropertyRelative("StartPosition").vector2Value = serializedValue.anchoredPosition;
                }
                if (GUI.Button(new Rect(
                    fourth.x, 
                    fourth.y, 
                    95, 
                    20), 
                    "End Position")) 
                {
                    RectTransform serializedValue = property.FindPropertyRelative("TargetRt").GetSerializedValue<RectTransform>();
                    property.FindPropertyRelative("EndPosition").vector2Value = serializedValue.anchoredPosition;
                }
                if (GUI.Button(new Rect(
                    sixth.x, 
                    sixth.y, 
                    (EditorGUIUtility.currentViewWidth - sixth.x - 50)*0.5f, 
                    20), 
                    "Move To Start Position")) 
                {
                    RectTransform serializedValue = property.FindPropertyRelative("TargetRt").GetSerializedValue<RectTransform>();
                    serializedValue.anchoredPosition = property.FindPropertyRelative("StartPosition").vector2Value;
                }
                if (GUI.Button(new Rect(
                    sixth.x + (EditorGUIUtility.currentViewWidth - sixth.x)*0.5f, 
                    sixth.y, 
                    (EditorGUIUtility.currentViewWidth - sixth.x - 50)*0.5f, 
                    20), 
                    "Move To End Position")) 
                {
                    RectTransform serializedValue = property.FindPropertyRelative("TargetRt").GetSerializedValue<RectTransform>();
                    serializedValue.anchoredPosition = property.FindPropertyRelative("EndPosition").vector2Value;
                }

                break;

            case SequenceType.Wait:
                EditorGUI.DrawRect(new Rect(
                    _colorX, 
                    position.y, 
                    EditorGUIUtility.currentViewWidth - _colorWidthMinus, 
                    (_height + _spacing) * 3 + 6)
                    , new Color(0, 0, 1, 0.05f)
                );
                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(first, property.FindPropertyRelative("Type"));
                EditorGUI.PropertyField(second, property.FindPropertyRelative("Duration"));
                break;

            case SequenceType.SetButtonBlock:
                EditorGUI.DrawRect(new Rect(
                    _colorX, 
                    position.y, 
                    EditorGUIUtility.currentViewWidth - _colorWidthMinus, 
                    (_height + _spacing) * 3 + 6)
                    , new Color(0, 0, 0, 0.05f)
                );
                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(first, property.FindPropertyRelative("Type"));
                EditorGUI.PropertyField(second, property.FindPropertyRelative("IsButtonBlocking"));
                break;

            case SequenceType.SetActive:
                EditorGUI.DrawRect(new Rect(
                    _colorX, 
                    position.y, 
                    EditorGUIUtility.currentViewWidth - _colorWidthMinus, 
                    (_height + _spacing) * 4 + 6)
                    , new Color(0, 1, 0, 0.05f)
                );
                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(first, property.FindPropertyRelative("Type"));
                EditorGUI.PropertyField(second, property.FindPropertyRelative("Target"));
                EditorGUI.PropertyField(third, property.FindPropertyRelative("IsActivating"));
                break;

            case SequenceType.SFX:
                EditorGUI.DrawRect(new Rect(
                    _colorX, 
                    position.y, 
                    EditorGUIUtility.currentViewWidth - _colorWidthMinus, 
                    (_height + _spacing) * 3 + 6)
                    , new Color(1, 1, 0, 0.05f)
                );
                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(first, property.FindPropertyRelative("Type"));
                EditorGUI.PropertyField(second, property.FindPropertyRelative("SFX"));
                break;

        }
        // _currentTime = 0;

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }


    
}

