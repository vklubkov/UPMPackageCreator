using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UPMPackageCreator {
    public class StringPairList : IDisposable {
        private ReorderableList _reorderableList;

        public StringPairList(GUIContent guiContent, List<StringPair> stringPairs) {
            _reorderableList = new ReorderableList(stringPairs,
                typeof(StringPair), true, guiContent != null, true, true) {
                drawHeaderCallback = DrawHeaderCallback,
                drawElementCallback = DrawElementCallback
            };

            void DrawHeaderCallback(Rect rect) {
                if (guiContent != null)
                    EditorGUI.LabelField(rect, guiContent);
            }

            void DrawElementCallback(Rect rect, int index, bool b, bool b1) {
                rect.y += List.Element.Height.Offset;
                var halfWidth = rect.width / 2;
                var value = (StringPair)_reorderableList.list[index];

                stringPairs[index].First =
                    EditorGUI.TextField(
                        new Rect(rect.x, rect.y, halfWidth, EditorGUIUtility.singleLineHeight), value.First);

                stringPairs[index].Second =
                    EditorGUI.TextField(
                        new Rect(rect.x + halfWidth, rect.y, rect.width - halfWidth, EditorGUIUtility.singleLineHeight),
                        value.Second);
            }
        }

        public void OnGui() => _reorderableList.DoLayoutList();
        public void Dispose() => _reorderableList = null;
    }
}