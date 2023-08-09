using System;
using UnityEditor;
using UnityEngine;

namespace UPMPackageCreator {
    public class ScrollView : IDisposable {
        public Vector2 Position { get; private set; }
        public ScrollView(Vector2 scrollPosition) => Position = EditorGUILayout.BeginScrollView(scrollPosition);

        public ScrollView(Vector2 scrollPosition, GUILayoutOption guiLayoutOption) =>
            Position = EditorGUILayout.BeginScrollView(scrollPosition, guiLayoutOption);

        public void Dispose() => EditorGUILayout.EndScrollView();
    }
}