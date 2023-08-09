using System;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace UPMPackageCreator {
    [Serializable]
    public class AssemblySubTab {
        [SerializeField] private GUIContent _assemblyName = new GUIContent(Assembly.Name.Label, Assembly.Name.Tooltip);

        [SerializeField] private AssemblyGeneral _general = new AssemblyGeneral();
        [SerializeField] private AssemblyDefineConstraints _defineConstraints = new AssemblyDefineConstraints();
        [SerializeField] private AssemblyDefinitionList _references = new AssemblyDefinitionList();
        [SerializeField] private PrecompiledReferencesList _precompiledReferences = new PrecompiledReferencesList();
        [SerializeField] private PlatformList _platforms = new PlatformList();
        [SerializeField] private VersionDefineList _versionDefines = new VersionDefineList();

        private Texture _backgroundTexture;
        private GUIStyle _backgroundStyle;

        public void OnEnable(AssemblyData assemblyData) {
            _defineConstraints.OnEnable(assemblyData);
            _references.OnEnable(assemblyData.References);
            _precompiledReferences.OnEnable(assemblyData);
            _platforms.OnEnable(assemblyData.IncludePlatforms);
            _versionDefines.OnEnable(assemblyData.VersionDefines);
        }

        public void OnGui(AssemblyData assemblyData) {
            if (_backgroundTexture == null || _backgroundStyle == null) {
                CleanupBackgroundTexture();
                CreateBackgroundStyle();
            }

            assemblyData.AssemblyName = EditorGUILayout.TextField(_assemblyName, assemblyData.AssemblyName);
            using (new DisabledGroup(string.IsNullOrEmpty(assemblyData.AssemblyName))) {
                _general.OnGui(assemblyData, _backgroundStyle);
                DrawSmallVerticalSpace();
                _defineConstraints.OnGui();
                _references.OnGui(assemblyData.References, _backgroundStyle);
                _precompiledReferences.OnGui(assemblyData);
                _platforms.OnGui(assemblyData.IncludePlatforms, assemblyData.ExcludePlatforms);
                _versionDefines.OnGui(_backgroundStyle);
            }
        }

        private void CreateBackgroundStyle() {
            var style = new GUIStyle(EditorStyles.label);
            var texture = new Texture2D(1, 1);
            var fillColor = new Color(0f, 0f, 0f, 0.1f);
            texture.SetPixels(new[] { fillColor }, 0 );
            texture.Apply();
            style.normal.background = texture;
            _backgroundTexture = texture;
            _backgroundStyle = style;
        }

        private void DrawSmallVerticalSpace() => GUILayout.Space(5);

        public void OnDisable() {
            _defineConstraints.OnDisable();
            _references.OnDisable();
            _precompiledReferences.OnDisable();
            _platforms.OnDisable();
            _versionDefines.OnDisable();

            CleanupBackgroundTexture();
        }

        private void CleanupBackgroundTexture() {
            if (_backgroundTexture != null)
                UnityObject.DestroyImmediate(_backgroundTexture);
        }
    }
}