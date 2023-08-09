using System;
using UnityEditor;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class AssemblyDefineConstraints {
        [SerializeField] private GUIContent _titleGuiContent =
            new GUIContent(DefineConstraints.Label, DefineConstraints.Tooltip);

        private StringList _defineConstraints;

        public void OnEnable(AssemblyData assemblyData) =>
            _defineConstraints = new StringList(assemblyData.DefineConstraints);

        public void OnGui() {
            EditorGUILayout.LabelField(_titleGuiContent, EditorStyles.boldLabel);
            _defineConstraints.OnGui();
        }

        public void OnDisable() {
            _defineConstraints.Dispose();
            _defineConstraints = null;
        }
    }
}