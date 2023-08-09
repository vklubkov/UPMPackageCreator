using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace UPMPackageCreator {
    [Serializable]
    public class DependenciesTab {
        [SerializeField] private GUIContent _unityVersionGuiContent =
            new GUIContent(Dependencies.Unity.Version.Label + Footnote.Two, Dependencies.Unity.Version.Tooltip);

        [SerializeField] private GUIContent _unityReleaseGuiContent =
            new GUIContent(Dependencies.Unity.Release.Label, Dependencies.Unity.Release.Tooltip);

        [SerializeField] public GUIContent _packageDependenciesGuiContent =
            new GUIContent(Dependencies.Unity.Packages.Label, Dependencies.Unity.Packages.Tooltip);

        private StringPairList _packageDependencies;

        public void OnEnable(DependenciesData dependenciesData) =>
            _packageDependencies = new StringPairList(_packageDependenciesGuiContent, dependenciesData.Dependencies);

        public void OnGui(DependenciesData dependenciesData) {
            DrawVerticalOffset();
            DrawUnityVersion(dependenciesData);
            DrawUnityRelease(dependenciesData);
            DrawPackageDependencies();
            DrawVerticalOffset();
            DrawFootnote();
            DrawVerticalOffset();
        }

        private void DrawVerticalOffset() => GUILayout.Space(15);

        private void DrawUnityVersion(DependenciesData dependenciesData) =>
            dependenciesData.UnityVersion =
                EditorGUILayout.TextField(_unityVersionGuiContent, dependenciesData.UnityVersion);

        private void DrawUnityRelease(DependenciesData dependenciesData) =>
            dependenciesData.UnityRelease =
                EditorGUILayout.TextField(_unityReleaseGuiContent, dependenciesData.UnityRelease);

        private void DrawPackageDependencies() => _packageDependencies.OnGui();
        private void DrawFootnote() => EditorGUILayout.LabelField(Footnote.LabelTwo);

        public void OnDisable() {
            _packageDependencies.Dispose();
            _packageDependencies = null;
        }
    }
}