using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue.Editor
{
    public class CharacterEditor : EditorWindow
    {
        Character selectedCharacter = null;

        [MenuItem("Window/Character Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(CharacterEditor), false, "Character Editor");
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            Character character = Selection.activeObject as Character;
            if (character != null)
            {
                selectedCharacter = character;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedCharacter == null)
            {
                EditorGUILayout.LabelField("No character selected");
            }
            else
            {
                EditorGUI.BeginChangeCheck();

                EditorGUILayout.LabelField("Name");
                string newName = EditorGUILayout.TextField(selectedCharacter.mName);
                EditorGUILayout.LabelField("Class");
                string newClass = EditorGUILayout.TextField(selectedCharacter.mClass);
                EditorGUILayout.LabelField("Race");
                string newRace = EditorGUILayout.TextField(selectedCharacter.race);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(selectedCharacter, "Character Edited");

                    selectedCharacter.mName = newName;
                    selectedCharacter.mClass = newClass;
                    selectedCharacter.race = newRace;

                    EditorUtility.SetDirty(selectedCharacter);
                }
            }
        }
    }
}