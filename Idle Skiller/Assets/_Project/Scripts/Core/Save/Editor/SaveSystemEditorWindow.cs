#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IdleSkiller.Core.Save
{
    public class SaveSystemEditorWindow : EditorWindow
    {
        private ISaveSystem saveSystem;
        private SaveData data;

        [MenuItem("IdleSkiller/Save System Debug")]
        public static void ShowWindow()
        {
            GetWindow<SaveSystemEditorWindow>("Save Debug");
        }

        private void OnEnable()
        {
            saveSystem = new SaveSystem();
            data = saveSystem.Load();
        }

        private void OnGUI()
        {
            data.Gold = EditorGUILayout.IntField("Gold", data.Gold);
            data.Gems = EditorGUILayout.IntField("Gems", data.Gems);
            data.Fame = EditorGUILayout.IntField("Fame", data.Fame);
            data.Workers = EditorGUILayout.IntField("Workers", data.Workers);
            data.Guild = EditorGUILayout.TextField("Guild", data.Guild);

            var inventoryString = string.Join(",", data.Inventory);
            inventoryString = EditorGUILayout.TextField("Inventory", inventoryString);
            data.Inventory = new List<string>(inventoryString.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries));

            if (GUILayout.Button("Save"))
            {
                saveSystem.Save(data);
            }

            if (GUILayout.Button("Load"))
            {
                data = saveSystem.Load();
            }
        }
    }
}
#endif
