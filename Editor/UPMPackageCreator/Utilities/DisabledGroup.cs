using System;
using UnityEditor;

namespace UPMPackageCreator {
    public class DisabledGroup : IDisposable {
        public DisabledGroup(bool isDisabled) => EditorGUI.BeginDisabledGroup(isDisabled);
        public void Dispose() => EditorGUI.EndDisabledGroup();
    }
}