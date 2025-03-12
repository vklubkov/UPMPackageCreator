using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;

namespace PackageCreator {
    [Serializable]
    public class PrecompiledReferencesList {
        [SerializeField] private GUIContent _titleGuiContent =
            new GUIContent(PrecompiledReferences.Label, PrecompiledReferences.Tooltip);

        [SerializeField] private bool _oldOverrideReferences;

        private ReorderableList _reorderableList;

        public void OnEnable(AssemblyData assemblyData) {
            _oldOverrideReferences = assemblyData.OverrideReferences;

            var precompiledAssembliesList = CompilationPipeline.GetPrecompiledAssemblyNames()
                .OrderBy(item => item)
                .ToList();

            precompiledAssembliesList.Insert(0, PrecompiledReferences.Default);
            var precompiledAssembliesArray = precompiledAssembliesList
                .Select(item => new GUIContent(item))
                .ToArray();

            var selectedAssemblyIndex = 0;
            _reorderableList = new ReorderableList(assemblyData.PrecompiledReferences,
                typeof(string), true, false, true, true) {
                drawElementCallback = DrawElementCallback
            };

            void DrawElementCallback(Rect rect, int index, bool _, bool __) {
                rect.y += List.Element.Height.Offset;
                rect.height = EditorGUIUtility.singleLineHeight;

                var popupGUIContent = precompiledAssembliesArray[selectedAssemblyIndex];
                selectedAssemblyIndex =
                    EditorGUI.Popup(rect, popupGUIContent, selectedAssemblyIndex, precompiledAssembliesArray);

                var precompiledAssembly = selectedAssemblyIndex == 0
                    ? string.Empty
                    : precompiledAssembliesList[selectedAssemblyIndex];

                assemblyData.PrecompiledReferences[index] = precompiledAssembly;
            }
        }

        public void OnGui(AssemblyData assemblyData) {
            if (_oldOverrideReferences != assemblyData.OverrideReferences &&
                !assemblyData.OverrideReferences) {
                assemblyData.PrecompiledReferences.Clear();
                _oldOverrideReferences = assemblyData.OverrideReferences;
            }

            if (assemblyData.OverrideReferences) {
                EditorGUILayout.LabelField(_titleGuiContent, EditorStyles.boldLabel);
                _reorderableList.DoLayoutList();
            }
        }

        public void OnDisable() => _reorderableList = null;
    }
}