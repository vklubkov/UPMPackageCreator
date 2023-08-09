using System;
using UnityEditor;
using UnityEngine;

namespace UPMPackageCreator {
    public class ToggleFocusFix : IDisposable {
        public ToggleFocusFix() =>  EditorGUI.BeginChangeCheck();

        public void Dispose() {
            if (EditorGUI.EndChangeCheck()) {
                // This fixes the bug when the text of a focused control
                // e.g., a TextField, remains in other tabs control.
                GUIUtility.keyboardControl = 0;
            }
        }
    }
}