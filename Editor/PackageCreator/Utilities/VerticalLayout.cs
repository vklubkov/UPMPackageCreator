using System;
using UnityEditor;
using UnityEngine;

namespace PackageCreator {
    public class VerticalLayout : IDisposable {
        public VerticalLayout() => EditorGUILayout.BeginVertical();
        public VerticalLayout(GUIStyle style) => EditorGUILayout.BeginVertical(style);
        public void Dispose() => EditorGUILayout.EndVertical();
    }
}