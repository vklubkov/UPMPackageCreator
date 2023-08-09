using System;
using UnityEditor;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class RecommendedTab {
        [SerializeField] private GUIContent _displayNameGuiContent =
            new GUIContent(Package.DisplayName.Label + Footnote.Two, Package.DisplayName.Tooltip);

        [SerializeField] private GUIContent _descriptionGuiContent =
            new GUIContent(Package.Description.Label + Footnote.Two, Package.Description.Tooltip);

        [SerializeField] private GUIContent _unityVersionGuiContent =
            new GUIContent(Dependencies.Unity.Version.Label + Footnote.Two, Dependencies.Unity.Version.Tooltip);

        [SerializeField] private Vector2 _scrollPosition = Vector2.zero;

        private GUILayoutOption _descriptionTextAreaScrollViewMinHeight;
        private GUILayoutOption _descriptionTextAreaExpandHeight;

        public void OnEnable() {
            _descriptionTextAreaScrollViewMinHeight = GUILayout.MinHeight(200);
            _descriptionTextAreaExpandHeight = GUILayout.ExpandHeight(true);
        }

        public void OnGui(PackageData packageData, DependenciesData dependenciesData) {
            DrawVerticalOffset();
            DrawDisplayName(packageData);
            DrawDescription(packageData);
            DrawUnityVersion(dependenciesData);
            DrawVerticalOffset();
            DrawFootnote();
            DrawVerticalOffset();
        }

        private void DrawVerticalOffset() => GUILayout.Space(15);

        private void DrawDisplayName(PackageData packageData) =>
            packageData.DisplayName = EditorGUILayout.TextField(_displayNameGuiContent, packageData.DisplayName);

        private void DrawDescription(PackageData packageData) {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(_descriptionGuiContent);
            _scrollPosition =
                EditorGUILayout.BeginScrollView(_scrollPosition, _descriptionTextAreaScrollViewMinHeight);

            var textAreaStyle = new GUIStyle(EditorStyles.textArea) { wordWrap = true };
            packageData.Description =
                EditorGUILayout.TextArea(packageData.Description, textAreaStyle, _descriptionTextAreaExpandHeight);

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void DrawUnityVersion(DependenciesData dependenciesData) =>
            dependenciesData.UnityVersion =
                EditorGUILayout.TextField(_unityVersionGuiContent, dependenciesData.UnityVersion);

        private void DrawFootnote() => EditorGUILayout.LabelField(Footnote.LabelTwo);

        public void OnDisable() {
            _descriptionTextAreaScrollViewMinHeight = null;
            _descriptionTextAreaExpandHeight = null;
        }
    }
}