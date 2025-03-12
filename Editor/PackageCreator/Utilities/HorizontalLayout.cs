using System;
using UnityEditor;

namespace PackageCreator {
    public class HorizontalLayout : IDisposable {
        public HorizontalLayout() => EditorGUILayout.BeginHorizontal();
        public void Dispose() => EditorGUILayout.EndHorizontal();
    }
}