using System;
using UnityEditor;
using UnityEngine;

namespace PackageCreator {
    [Serializable]
    public class RequiredTab {
        [SerializeField] private GUIContent _directoryNameGuiContent =
            new GUIContent(Folder.Label + Footnote.One, Folder.Tooltip);

        [SerializeField] private GUIContent _packageIdGuiContent =
            new GUIContent(Package.Id.Label + Footnote.One, Package.Id.Tooltip);

        [SerializeField] private GUIContent _packageVersionGuiContent =
            new GUIContent(Package.Version.Label + Footnote.One, Package.Version.Tooltip);

        public void OnGui(PackageData packageData) {
            DrawVerticalOffset();
            DrawDirectoryName(packageData);
            DrawPackageId(packageData);
            DrawVersion(packageData);
            DrawVerticalOffset();
            DrawFootnote();
            DrawVerticalOffset();
        }

        private void DrawVerticalOffset() => GUILayout.Space(15);

        private void DrawDirectoryName(PackageData packageData) =>
            packageData.DirectoryName = EditorGUILayout.TextField(_directoryNameGuiContent, packageData.DirectoryName);

        private void DrawPackageId(PackageData packageData) =>
            packageData.PackageId = EditorGUILayout.TextField(_packageIdGuiContent, packageData.PackageId);

        private void DrawVersion(PackageData packageData) =>
            packageData.Version = EditorGUILayout.TextField(_packageVersionGuiContent, packageData.Version);

        private void DrawFootnote() => EditorGUILayout.LabelField(Footnote.LabelOne);
    }
}