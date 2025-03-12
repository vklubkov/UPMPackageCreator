using System;
using UnityEditor;
using UnityEngine;

namespace PackageCreator {
    [Serializable]
    public class AssemblyTab {
        [SerializeField] private int _currentTab;

        [SerializeField] private AssemblySubTab _runtimeSubTab = new AssemblySubTab();
        [SerializeField] private AssemblySubTab _editorSubTab = new AssemblySubTab();

        public void OnEnable(AssemblyData runtimeAssemblyData, AssemblyData editorAssemblyData) {
            _runtimeSubTab.OnEnable(runtimeAssemblyData);
            _editorSubTab.OnEnable(editorAssemblyData);
        }

        public void OnGui(AssemblyData runtimeAssemblyData, AssemblyData editorAssemblyData) {
            DrawVerticalOffset();
            DrawTabButtons();
            DrawVerticalOffset();
            DrawTabs(runtimeAssemblyData, editorAssemblyData);
            DrawVerticalOffset();
        }

        private void DrawVerticalOffset() => GUILayout.Space(15);

        private void DrawTabButtons() {
            using (new ToggleFocusFix()) {
                _currentTab = GUILayout.Toolbar(_currentTab, new[] {
                    Assembly.Runtime.SubTab,
                    Assembly.Editor.SubTab
                });
            }
        }

        private void DrawTabs(AssemblyData runtimeAssemblyData, AssemblyData editorAssemblyData) {
            switch (_currentTab) {
                case 0:
                    EditorGUILayout.LabelField(Assembly.Runtime.Label, EditorStyles.boldLabel);
                    _runtimeSubTab.OnGui(runtimeAssemblyData);
                    break;
                case 1:
                    EditorGUILayout.LabelField(Assembly.Editor.Label, EditorStyles.boldLabel);
                    _editorSubTab.OnGui(editorAssemblyData);
                    break;
            }
        }

        public void OnDisable() {
            _runtimeSubTab.OnDisable();
            _editorSubTab.OnDisable();
        }
    }
}