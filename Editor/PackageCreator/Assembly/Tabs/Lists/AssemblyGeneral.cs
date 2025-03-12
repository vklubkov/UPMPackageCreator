using System;
using UnityEditor;
using UnityEngine;

namespace PackageCreator {
    [Serializable]
    public class AssemblyGeneral {
        [SerializeField] private GUIContent _allowUnsafeGuiContent =
            new GUIContent(General.AllowUnsafeCode.Label, General.AllowUnsafeCode.Tooltip);

        [SerializeField] private GUIContent _autoReferencedGuiContent =
            new GUIContent(General.AutoReferenced.Label, General.AutoReferenced.Tooltip);

        [SerializeField] private GUIContent _noEngineReferencesGuiContent =
            new GUIContent(General.NoEngineReferences.Label, General.NoEngineReferences.Tooltip);

        [SerializeField] private GUIContent _overrideReferencesGuiContent =
            new GUIContent(General.OverrideReferences.Label, General.OverrideReferences.Tooltip);

        [SerializeField] private GUIContent _rootNamespaceGuiContent =
            new GUIContent(General.RootNamespace.Label, General.RootNamespace.Tooltip);

        public void OnGui(AssemblyData assemblyData, GUIStyle backgroundStyle) {
            DrawGeneralLabel();
            using (new VerticalLayout(backgroundStyle)) {
                DrawAllowUnsafeCode(assemblyData);
                DrawAutoReferenced(assemblyData);
                DrawNoEngineReferences(assemblyData);
                DrawOverrideReferences(assemblyData);
                DrawRootNamespace(assemblyData);
            }
        }

        private void DrawGeneralLabel() =>
            EditorGUILayout.LabelField(General.Title.Label, EditorStyles.boldLabel);

        private void DrawAllowUnsafeCode(AssemblyData assemblyData) =>
            assemblyData.AllowUnsafeCode =
                EditorGUILayout.Toggle(_allowUnsafeGuiContent, assemblyData.AllowUnsafeCode);

        private void DrawAutoReferenced(AssemblyData assemblyData) =>
            assemblyData.AutoReferenced =
                EditorGUILayout.Toggle(_autoReferencedGuiContent, assemblyData.AutoReferenced);

        private void DrawNoEngineReferences(AssemblyData assemblyData) =>
            assemblyData.NoEngineReferences =
                EditorGUILayout.Toggle(_noEngineReferencesGuiContent, assemblyData.NoEngineReferences);

        private void DrawOverrideReferences(AssemblyData assemblyData) =>
            assemblyData.OverrideReferences =
                EditorGUILayout.Toggle(_overrideReferencesGuiContent, assemblyData.OverrideReferences);

        private void DrawRootNamespace(AssemblyData assemblyData) =>
            assemblyData.RootNamespace =
                EditorGUILayout.TextField(_rootNamespaceGuiContent, assemblyData.RootNamespace);
    }
}