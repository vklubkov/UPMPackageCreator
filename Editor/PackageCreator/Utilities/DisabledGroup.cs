using System;
using UnityEditor;

namespace PackageCreator {
    public class DisabledGroup : IDisposable {
        public DisabledGroup(bool isDisabled) => EditorGUI.BeginDisabledGroup(isDisabled);
        public void Dispose() => EditorGUI.EndDisabledGroup();
    }
}