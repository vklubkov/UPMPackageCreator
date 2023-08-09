using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class SamplesList {
        [SerializeField] private GUIContent _titleGuiContent =
            new GUIContent(Samples.Title.Label, Samples.Title.Tooltip);

        [SerializeField] private GUIContent _displayNameGuiContent =
            new GUIContent(Samples.DisplayName.Label, Samples.DisplayName.Tooltip);

        [SerializeField] private GUIContent _descriptionGuiContent =
            new GUIContent(Samples.Description.Label, Samples.Description.Tooltip);

        [SerializeField] private GUIContent _pathGuiContent =
            new GUIContent(Samples.Path.Label, Samples.Path.Tooltip);

        private ReorderableList _reorderableList;

        public void OnEnable(List<Sample> samples) {
            _reorderableList = new ReorderableList(samples,
                typeof(Sample), true, false, true, true) {
                drawElementCallback = DrawElementCallback,
                elementHeightCallback = ElementHeightCallback
            };

            void DrawElementCallback(Rect rect, int index, bool _, bool __) {
                rect.height = EditorGUIUtility.singleLineHeight;
                var value = (Sample)_reorderableList.list[index];

                DrawDisplayName();
                DrawDescription();
                DrawPath();

                void DrawDisplayName() {
                    rect.y += List.Element.Height.Offset;
                    value.DisplayName = EditorGUI.TextField(rect, _displayNameGuiContent, value.DisplayName);
                }

                void DrawDescription() {
                    rect.y += List.Element.Height.Offset + EditorGUIUtility.singleLineHeight;
                    value.Description = EditorGUI.TextField(rect, _descriptionGuiContent, value.Description);
                }

                void DrawPath() {
                    rect.y += List.Element.Height.Offset + EditorGUIUtility.singleLineHeight;
                    value.Path = EditorGUI.TextField(rect, _pathGuiContent, value.Path);
                }
            }

            float ElementHeightCallback(int _) =>
                EditorGUIUtility.singleLineHeight * 3 + List.Elements.Height.Offset;
        }

        public void OnGui() {
            EditorGUILayout.LabelField(_titleGuiContent, EditorStyles.boldLabel);
            _reorderableList.DoLayoutList();
        }

        public void OnDisable() => _reorderableList = null;
    }
}