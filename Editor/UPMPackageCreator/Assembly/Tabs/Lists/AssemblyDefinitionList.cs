using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace UPMPackageCreator {
    [Serializable]
    public class AssemblyDefinitionList {
        [SerializeField] private GUIContent _titleGuiContent =
            new GUIContent(AssemblyDefinitionReferences.Title.Label,
                AssemblyDefinitionReferences.Title.Tooltip);

        [SerializeField] private GUIContent _useGuidsGuiContent =
            new GUIContent(AssemblyDefinitionReferences.Guid.Use.Label,
                AssemblyDefinitionReferences.Guid.Use.Tooltip);

        [SerializeField] private bool _useGuids = true;

        private class Dummy : ScriptableObject {
            [SerializeField] private List<AssemblyDefinitionAsset> _list = new List<AssemblyDefinitionAsset>();
            public List<AssemblyDefinitionAsset> List => _list;
            public static string SerializablePropertyName => nameof(_list);
        }

        private Dummy _assemblyDefinitionList;
        private ReorderableList _reorderableList;
        private SerializedObject _serializedObject;
        private SerializedProperty _serializedProperty;

        public void OnEnable(List<string> assemblyDefinitions) {
            _useGuids = assemblyDefinitions.Count == 0 ||
                        assemblyDefinitions.All(item => item.Contains(
                            AssemblyDefinitionReferences.Guid.Key +
                            AssemblyDefinitionReferences.Guid.Separator));

            CreateReorderableList(_useGuids, assemblyDefinitions);
        }

        public void OnGui(List<string> assemblyDefinitions, GUIStyle backgroundStyle) {
            EditorGUILayout.LabelField(_titleGuiContent, EditorStyles.boldLabel);

            using (new VerticalLayout(backgroundStyle)) {
                var isEmptyList = assemblyDefinitions.Count == 0;
                if (isEmptyList)
                    _useGuids = true;

                using (new DisabledGroup(isEmptyList)) {
                    _useGuids = EditorGUILayout.Toggle(_useGuidsGuiContent, _useGuids);
                }
            }

            // A ScriptableObject without an associated asset instantiates in
            // current scene. When the scene changes it becomes null.
            if (_serializedObject.targetObject == null) {
                CleanupReorderableList();
                CreateReorderableList(_useGuids, assemblyDefinitions);
            }

            _serializedObject.Update();
            _reorderableList.DoLayoutList();
            _serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void CreateReorderableList(bool useGuids, List<string> assemblyDefinitions) {
            var assemblyDefinitionAssets = assemblyDefinitions
                .Select(assemblyDefinitionId => {
                    if (string.IsNullOrEmpty(assemblyDefinitionId))
                        return null;

                    var assemblyDefinitionAssetPath = GetAssemblyDefinitionAssetPath(assemblyDefinitionId);
                    if (string.IsNullOrEmpty(assemblyDefinitionAssetPath))
                        return null;

                    return AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assemblyDefinitionAssetPath);
                })
                .ToList();

            _assemblyDefinitionList = ScriptableObject.CreateInstance<Dummy>();
            _assemblyDefinitionList.List.AddRange(assemblyDefinitionAssets);
            _serializedObject = new SerializedObject(_assemblyDefinitionList);
            _serializedProperty = _serializedObject.FindProperty(Dummy.SerializablePropertyName);

            _reorderableList = new ReorderableList(_serializedObject,
                _serializedProperty, true, false, true, true) {
                drawElementCallback = DrawElementCallback,
                onAddCallback = OnAddCallback,
                onRemoveCallback = OnRemoveCallback,
                onReorderCallbackWithDetails = OnReorderCallbackWithDetails
            };

            void DrawElementCallback(Rect rect, int index, bool _, bool __) {
                rect.y += List.Element.Height.Offset;
                rect.height = EditorGUIUtility.singleLineHeight;

                var assemblyDefinitionAsset = _assemblyDefinitionList.List[index];
                assemblyDefinitions[index] = GetAssemblyDefinitionId(useGuids, assemblyDefinitionAsset);

                var element = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                var assemblyName = GetAssemblyName(assemblyDefinitionAsset);

                var elementGuiContent =
                    new GUIContent(string.IsNullOrEmpty(assemblyName)
                        ? string.Format(List.Element.Format, index.ToString())
                        : assemblyName);

                EditorGUI.PropertyField(rect, element, elementGuiContent);
            }

            void OnAddCallback(ReorderableList list) {
                assemblyDefinitionAssets.Add(null);
                assemblyDefinitions.Add(null);
                ReorderableList.defaultBehaviours.DoAddButton(list);
            }

            void OnRemoveCallback(ReorderableList list) {
                assemblyDefinitionAssets.RemoveAt(list.index);
                assemblyDefinitions.RemoveAt(list.index);
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }

            void OnReorderCallbackWithDetails(ReorderableList list, int oldIndex, int newIndex) {
                (assemblyDefinitionAssets[oldIndex], assemblyDefinitionAssets[newIndex]) =
                    (assemblyDefinitionAssets[newIndex], assemblyDefinitionAssets[oldIndex]);

                (assemblyDefinitions[oldIndex], assemblyDefinitions[newIndex]) =
                    (assemblyDefinitions[newIndex], assemblyDefinitions[oldIndex]);
            }
        }

        private string GetAssemblyDefinitionAssetPath(string assemblyDefinitionId) {
            var splitId = assemblyDefinitionId.Split(AssemblyDefinitionReferences.Guid.Separator);
            if (splitId.Length != 2 || splitId[0] != AssemblyDefinitionReferences.Guid.Key)
                return CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(assemblyDefinitionId);

            var guid = splitId[1];
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        private string GetAssemblyDefinitionId(bool useGuids, AssemblyDefinitionAsset assemblyDefinitionAsset) {
            if (!useGuids)
                return GetAssemblyName(assemblyDefinitionAsset);

            if (assemblyDefinitionAsset == null)
                return null;

            var assetPath = AssetDatabase.GetAssetPath(assemblyDefinitionAsset);
            if (string.IsNullOrEmpty(assetPath))
                return null;

            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (string.IsNullOrEmpty(guid))
                return null;

            return AssemblyDefinitionReferences.Guid.Key +
                   AssemblyDefinitionReferences.Guid.Separator +
                   guid; // e.g."GUID:d528c8c98d269ca44a06cd9624a03945"
        }

        private string GetAssemblyName(AssemblyDefinitionAsset assemblyDefinitionAsset) {
            if (assemblyDefinitionAsset == null)
                return null;

            var textJson = JObject.Parse(assemblyDefinitionAsset.text);
            return (string)textJson[AssemblyJsonKeys.AssemblyName];
        }

        public void OnDisable() => CleanupAll();

        private void CleanupAll() {
            CleanupReorderableList();
            CleanupAssemblyDefinitionList();
        }

        private void CleanupReorderableList() {
            if (_serializedObject != null) {
                _serializedObject.Dispose();
                _serializedObject = null;
            }

            if (_serializedProperty != null) {
                _serializedProperty?.Dispose();
                _serializedProperty = null;
            }

            _reorderableList = null;
        }

        private void CleanupAssemblyDefinitionList() {
            if (_assemblyDefinitionList == null)
                return;

            UnityObject.DestroyImmediate(_assemblyDefinitionList);
            _assemblyDefinitionList = null;
        }
    }
}