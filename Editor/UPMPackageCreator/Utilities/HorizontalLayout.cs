using System;
using UnityEditor;

namespace UPMPackageCreator {
    public class HorizontalLayout : IDisposable {
        public HorizontalLayout() => EditorGUILayout.BeginHorizontal();
        public void Dispose() => EditorGUILayout.EndHorizontal();
    }
}