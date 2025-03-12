using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace PackageCreator {
    public class StringList : IDisposable {
        private ReorderableList _reorderableList;

        public StringList(List<string> strings) : this(null, strings) { }

        public StringList(GUIContent guiContent, List<string> strings) {
            _reorderableList = new ReorderableList(strings,
                typeof(string), true, guiContent != null, true, true) {
                drawHeaderCallback = DrawHeaderCallback,
                drawElementCallback = DrawElementCallback
            };

            void DrawHeaderCallback(Rect rect) {
                if (guiContent != null)
                    EditorGUI.LabelField(rect, guiContent);
            }

            void DrawElementCallback(Rect rect, int index, bool b, bool b1) {
                rect.y += List.Element.Height.Offset;
                rect.height = EditorGUIUtility.singleLineHeight;

                var element = (string)_reorderableList.list[index];
                var elementGuiContent = new GUIContent(string.IsNullOrEmpty(element)
                    ? string.Format(List.Element.Format, index)
                    : element);

                _reorderableList.list[index] = EditorGUI.TextField(rect, elementGuiContent, element);
            }
        }

        public void OnGui() => _reorderableList.DoLayoutList();
        public void Dispose() => _reorderableList = null;
    }
}