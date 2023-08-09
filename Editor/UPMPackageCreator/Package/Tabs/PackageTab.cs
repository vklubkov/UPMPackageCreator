using System;
using UnityEditor;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class PackageTab {
        [SerializeField] private GUIContent _directoryNameGuiContent =
            new GUIContent(Folder.Label + Footnote.One, Folder.Tooltip);

        [SerializeField] private GUIContent _packageIdGuiContent =
            new GUIContent(Package.Id.Label + Footnote.One, Package.Id.Tooltip);

        [SerializeField] private GUIContent _versionGuiContent =
            new GUIContent(Package.Version.Label + Footnote.One, Package.Version.Tooltip);

        [SerializeField] private GUIContent _displayNameGuiContent =
            new GUIContent(Package.DisplayName.Label + Footnote.Two, Package.DisplayName.Tooltip);

        [SerializeField] private GUIContent _descriptionGuiContent =
            new GUIContent(Package.Description.Label + Footnote.Two, Package.Description.Tooltip);

        [SerializeField] public GUIContent _categoryGuiContent =
            new GUIContent(Package.Category.Label + Footnote.Three, Package.Category.Tooltip);

        [SerializeField] public GUIContent _mainNamespaceGuiContent =
            new GUIContent(Package.MainNamespace.Label, Package.MainNamespace.Tooltip);

        [SerializeField] public GUIContent _keywordsGuiContent =
            new GUIContent(Package.Keywords.Label, Package.Keywords.Tooltip);

        [SerializeField] public GUIContent _licenseGuiContent =
            new GUIContent(Package.License.Label, Package.License.Tooltip);

        [SerializeField] public GUIContent _licenseUrlGuiContent =
            new GUIContent(Package.LicensesUrl.Label, Package.LicensesUrl.Tooltip);

        [SerializeField] public GUIContent _changelogGuiContent =
            new GUIContent(Package.ChangelogUrl.Label, Package.ChangelogUrl.Tooltip);

        [SerializeField] public GUIContent _documentationUrlGuiContent =
            new GUIContent(Package.DocumentationUrl.Label, Package.DocumentationUrl.Tooltip);

        [SerializeField] public GUIContent _visibilityInEditorGuiContent =
            new GUIContent(Package.VisibilityInEditor.Title.Label, Package.VisibilityInEditor.Title.Tooltip);

        [SerializeField] public GUIContent _typeGuiContent =
            new GUIContent(Package.Type.Label + Footnote.Four, Package.Type.Tooltip);

        [SerializeField] private SamplesList _samplesList = new SamplesList();

        [SerializeField] private Vector2 _scrollPosition = Vector2.zero;

        private static readonly string[] _visibilityInEditorItems = {
            Package.VisibilityInEditor.Items.DefaultVisibility,
            Package.VisibilityInEditor.Items.AlwaysHidden,
            Package.VisibilityInEditor.Items.AlwaysVisible
        };

        private const int _defaultVisibilityIndex = 0;
        private const int _alwaysHiddenIndex = 1;
        private const int _alwaysVisibleIndex = 2;

        private GUILayoutOption _descriptionTextAreaScrollViewMinHeight;
        private GUILayoutOption _descriptionTextAreaExpandHeight;

        private StringList _stringList;

        public void OnEnable(PackageData packageData) {
            _descriptionTextAreaScrollViewMinHeight = GUILayout.MinHeight(200);
            _descriptionTextAreaExpandHeight = GUILayout.ExpandHeight(true);
            _stringList = new StringList(_keywordsGuiContent, packageData.Keywords);
            _samplesList.OnEnable(packageData.Samples);
        }

        public void OnGui(PackageData packageData) {
            DrawVerticalOffset();
            DrawDirectoryName(packageData);
            DrawPackageId(packageData);
            DrawVersion(packageData);
            DrawVerticalOffset();
            DrawDisplayName(packageData);
            DrawDescription(packageData);
            DrawVerticalOffset();
            DrawCategory(packageData);
            DrawType(packageData);
            DrawVerticalOffset();
            DrawMainNamespace(packageData);
            DrawKeywords();
            DrawLicense(packageData);
            DrawLicensesUrl(packageData);
            DrawChangelogUrl(packageData);
            DrawDocumentationUrl(packageData);
            DrawHideInEditor(packageData);
            DrawSamples();
            DrawVerticalOffset();
            DrawFootnotes();
            DrawVerticalOffset();
        }

        private void DrawVerticalOffset() => GUILayout.Space(15);

        private void DrawDirectoryName(PackageData packageData) =>
            packageData.DirectoryName = EditorGUILayout.TextField(_directoryNameGuiContent, packageData.DirectoryName);

        private void DrawPackageId(PackageData packageData) =>
            packageData.PackageId = EditorGUILayout.TextField(_packageIdGuiContent, packageData.PackageId);

        private void DrawVersion(PackageData packageData) =>
            packageData.Version = EditorGUILayout.TextField(_versionGuiContent, packageData.Version);

        private void DrawDisplayName(PackageData packageData) =>
            packageData.DisplayName = EditorGUILayout.TextField(_displayNameGuiContent, packageData.DisplayName);

        private void DrawDescription(PackageData packageData) {
            using (new VerticalLayout()) {
                EditorGUILayout.LabelField(_descriptionGuiContent);

                using (var scrollView = new ScrollView(_scrollPosition, _descriptionTextAreaScrollViewMinHeight)) {
                    _scrollPosition = scrollView.Position;
                    var textAreaStyle = new GUIStyle(EditorStyles.textArea) { wordWrap = true };
                    packageData.Description = EditorGUILayout.TextArea(
                        packageData.Description, textAreaStyle, _descriptionTextAreaExpandHeight);
                }
            }
        }

        private void DrawCategory(PackageData packageData) =>
            packageData.Category = EditorGUILayout.TextField(_categoryGuiContent, packageData.Category);

        private void DrawType(PackageData packageData) =>
            packageData.Type = EditorGUILayout.TextField(_typeGuiContent, packageData.Type);

        private void DrawMainNamespace(PackageData packageData) =>
            packageData.MainNamespace = EditorGUILayout.TextField(_mainNamespaceGuiContent, packageData.MainNamespace);

        private void DrawKeywords() => _stringList.OnGui();

        private void DrawLicense(PackageData packageData) =>
            packageData.License = EditorGUILayout.TextField(_licenseGuiContent, packageData.License);

        private void DrawLicensesUrl(PackageData packageData) =>
            packageData.LicensesUrl = EditorGUILayout.TextField(_licenseUrlGuiContent, packageData.LicensesUrl);

        private void DrawChangelogUrl(PackageData packageData) =>
            packageData.ChangelogUrl = EditorGUILayout.TextField(_changelogGuiContent, packageData.ChangelogUrl);

        private void DrawDocumentationUrl(PackageData packageData) =>
            packageData.DocumentationUrl =
                EditorGUILayout.TextField(_documentationUrlGuiContent, packageData.DocumentationUrl);

        private void DrawHideInEditor(PackageData packageData) {
            var index = packageData.HideInEditor.HasValue
                ? (packageData.HideInEditor.Value ? _alwaysVisibleIndex : _alwaysHiddenIndex)
                : _defaultVisibilityIndex;

            index = EditorGUILayout.Popup(_visibilityInEditorGuiContent, index, _visibilityInEditorItems);

            packageData.HideInEditor = index switch {
                _defaultVisibilityIndex => null,
                _alwaysHiddenIndex => false,
                _alwaysVisibleIndex => true,
                _ => packageData.HideInEditor
            };
        }

        private void DrawSamples() => _samplesList.OnGui();

        private void DrawFootnotes() {
            EditorGUILayout.LabelField(Footnote.LabelOne);
            EditorGUILayout.LabelField(Footnote.LabelTwo);
            EditorGUILayout.LabelField(Footnote.LabelThree);
            EditorGUILayout.LabelField(Footnote.LabelFour);
        }

        public void OnDisable() {
            _samplesList.OnDisable();

            _descriptionTextAreaScrollViewMinHeight = null;
            _descriptionTextAreaExpandHeight = null;

            _stringList.Dispose();
            _stringList = null;
        }
    }
}