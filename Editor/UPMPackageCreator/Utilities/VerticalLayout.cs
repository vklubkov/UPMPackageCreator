using System;
using UnityEditor;
using UnityEngine;

namespace UPMPackageCreator {
    public class VerticalLayout : IDisposable {
        public VerticalLayout() => EditorGUILayout.BeginVertical();
        public VerticalLayout(GUIStyle style) => EditorGUILayout.BeginVertical(style);
        public void Dispose() => EditorGUILayout.EndVertical();
    }
}