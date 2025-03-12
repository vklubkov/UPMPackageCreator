using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using PackageManager = UnityEditor.PackageManager.Client;

namespace PackageCreator {
    [Serializable]
    public class VersionDefineList {
        [SerializeField] private GUIContent _titleGuiContent =
            new GUIContent(VersionDefines.Title.Label, VersionDefines.Title.Tooltip);

        [SerializeField] private List<int> _resourceIndexes = new  List<int>();

        private ReorderableList _reorderableList;

        public void OnEnable(List<VersionDefine> versionDefines) => OnEnable(null, versionDefines);

        public void OnEnable(GUIContent guiContent, List<VersionDefine> versionDefines) {
            var resourcesArray = GetPackages();
            _resourceIndexes = versionDefines.Select(_ => 0).ToList();

            _reorderableList = new ReorderableList(versionDefines,
                typeof(VersionDefine), true, guiContent != null, true, true) {
                drawHeaderCallback = DrawHeaderCallback,
                drawElementCallback = DrawElementCallback,
                elementHeightCallback = ElementHeightCallback,
                onAddCallback = OnAddCallback,
                onRemoveCallback = OnRemoveCallback,
                onReorderCallbackWithDetails = OnReorderCallbackWithDetails
            };

            void DrawHeaderCallback(Rect rect) {
                if (guiContent != null)
                    EditorGUI.LabelField(rect, guiContent);
            }

            void DrawElementCallback(Rect rect, int index, bool _, bool __) {
                rect.height = EditorGUIUtility.singleLineHeight;
                var value = (VersionDefine)_reorderableList.list[index];

                DrawResourcesList();
                DrawDefine();
                DrawExpression();

                void DrawResourcesList() {
                    rect.y += List.Element.Height.Offset;

                    var resourceIndex =
                        EditorGUI.Popup(rect, VersionDefines.Resource.Label, _resourceIndexes[index], resourcesArray);

                    value.Resource = resourcesArray[resourceIndex];
                    _resourceIndexes[index] = resourceIndex;
                }

                void DrawDefine() {
                    rect.y += List.Element.Height.Offset + EditorGUIUtility.singleLineHeight;
                    value.Define = EditorGUI.TextField(rect, VersionDefines.Define.Label, value.Define);
                }

                void DrawExpression() {
                    rect.y += List.Element.Height.Offset + EditorGUIUtility.singleLineHeight;
                    value.Expression =
                        EditorGUI.TextField(rect, VersionDefines.Expression.Label, value.Expression);
                }
            }

            float ElementHeightCallback(int _) =>
                EditorGUIUtility.singleLineHeight * 3 + VersionDefines.ElementHeight.Offset;

            void OnAddCallback(ReorderableList list) {
                _resourceIndexes.Add(0);
                ReorderableList.defaultBehaviours.DoAddButton(list);
            }

            void OnRemoveCallback(ReorderableList list) {
                _resourceIndexes.RemoveAt(list.index);
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }

            void OnReorderCallbackWithDetails(ReorderableList list, int oldIndex, int newIndex) {
                (versionDefines[oldIndex], versionDefines[newIndex]) =
                    (versionDefines[newIndex], versionDefines[oldIndex]);

                (_resourceIndexes[oldIndex], _resourceIndexes[newIndex]) =
                    (_resourceIndexes[newIndex], _resourceIndexes[oldIndex]);
            }
        }

        private string[] GetPackages() {
            var packagesRequest = PackageManager.List(offlineMode:true, includeIndirectDependencies:false);
            while (!packagesRequest.IsCompleted) { }

            var packages = packagesRequest.Status == StatusCode.Success
                ? packagesRequest.Result.ToList()
                : new List<PackageInfo>();

            return new List<string> {
                    VersionDefines.Resources.Select,
                    VersionDefines.Resources.Unity
                }
                .Concat(packages.Select(item => item.name))
                .ToArray();
        }

        public void OnGui(GUIStyle backgroundStyle) {
            using (new VerticalLayout(backgroundStyle)) {
                EditorGUILayout.LabelField(_titleGuiContent, EditorStyles.boldLabel);
                _reorderableList.DoLayoutList();
            }
        }

        public void OnDisable() => _reorderableList = null;
    }
}