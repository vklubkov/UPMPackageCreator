using System;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using PackageManager = UnityEditor.PackageManager.Client;

namespace UPMPackageCreator {
    public class PackageCreatorWindow : EditorWindow {
        // Data
        [SerializeField] private PackageData _packageData = new PackageData();
        [SerializeField] private DependenciesData _dependenciesData = new DependenciesData();
        [SerializeField] private AuthorData _authorData = new AuthorData();
        [SerializeField] private AssemblyData _runtimeAssemblyData = new AssemblyData();
        [SerializeField] private AssemblyData _editorAssemblyData = new AssemblyData();

        // Tabs UI
        [SerializeField] private RequiredTab _requiredTab = new RequiredTab();
        [SerializeField] private RecommendedTab _recommendedTab = new RecommendedTab();
        [SerializeField] private PackageTab _packageTab = new PackageTab();
        [SerializeField] private DependenciesTab _dependenciesTab = new DependenciesTab();
        [SerializeField] private AssemblyTab _assemblyTab = new AssemblyTab();
        [SerializeField] private AuthorTab _authorTab = new AuthorTab();

        // Window UI
        [SerializeField] private int _currentTab;
        [SerializeField] private Vector2 _scrollPosition = Vector2.zero;
        [SerializeField] private PackageType _packageType;

        private enum PackageType {
            Embedded = 0,
            Local,
            InAssetsFolder
        }

        [MenuItem(Menu.Tools + "/" + Menu.PackageName + "/" + Menu.CreateEmbedded)]
        public static void  ShowCreateEmbeddedWindow() =>
            CreateWindow(Window.Title.CreateEmbedded, Window.Size, PackageType.Embedded);

        [MenuItem(Menu.Tools + "/" + Menu.PackageName + "/" + Menu.CreateLocal)]
        public static void  ShowCreateLocalWindow() =>
            CreateWindow(Window.Title.CreateLocal, Window.Size, PackageType.Local);

        [MenuItem(Menu.Tools + "/" + Menu.PackageName + "/" + Menu.CreateInAssetsFolder)]
        public static void  ShowCreateInAssetsFolderWindow() =>
            CreateWindow(Window.Title.CreateInAssetsFolder, Window.Size, PackageType.InAssetsFolder);

        private static void CreateWindow(string title, Vector2 size, PackageType packageType) {
            var window = GetWindow<PackageCreatorWindow>();
            window.Initialize(title, size, packageType);
        }

        private void Initialize(string caption, Vector2 size,PackageType packageType) {
            titleContent = new GUIContent(caption);
            minSize = size;
            _packageType = packageType;
        }

        private void OnEnable() {
            _recommendedTab.OnEnable();
            _packageTab.OnEnable(_packageData);
            _dependenciesTab.OnEnable(_dependenciesData);
            _assemblyTab.OnEnable(_runtimeAssemblyData, _editorAssemblyData);
        }

        private void OnGUI() {
            using (new VerticalLayout(EditorStyles.inspectorDefaultMargins)) {
                DrawTabs();
                DrawButtons();
            }
        }

        private void DrawTabs() {
            using (var scrollView = new ScrollView(_scrollPosition)) {
                _scrollPosition = scrollView.Position;

                using (new ToggleFocusFix()) {
                    _currentTab = GUILayout.Toolbar(_currentTab, new[] {
                        Required.Tab,
                        Recommended.Tab,
                        Package.Tab,
                        Dependencies.Tab,
                        Assembly.Tab,
                        Author.Tab
                    });
                }

                switch (_currentTab) {
                    case 0:
                        _requiredTab.OnGui(_packageData);
                        break;
                    case 1:
                        _recommendedTab.OnGui(_packageData, _dependenciesData);
                        break;
                    case 2:
                        _packageTab.OnGui(_packageData);
                        break;
                    case 3:
                        _dependenciesTab.OnGui(_dependenciesData);
                        break;
                    case 4:
                        _assemblyTab.OnGui(_runtimeAssemblyData, _editorAssemblyData);
                        break;
                    case 5:
                        _authorTab.OnGui(_authorData);
                        break;
                }
            }
        }

        private void DrawButtons() {
            GUILayout.Space(30);

            using (new HorizontalLayout()) {
                var canSave = _packageData.IsValid &&
                              _dependenciesData.IsValid &&
                              _runtimeAssemblyData.IsValid &&
                              _editorAssemblyData.IsValid &&
                              _authorData.IsValid;

                using (new DisabledGroup(!canSave)) {
                    if (GUILayout.Button(Button.Create.Label)) {
                        Save();
                    }
                }

                if (GUILayout.Button(Button.Cancel.Label))
                    Close();
            }

            GUILayout.Space(30);
        }

        private void Save() {
            try {
                var packageSave = new PackageSave(
                    _packageData, _dependenciesData, _runtimeAssemblyData, _editorAssemblyData, _authorData);

                switch (_packageType)
                {
                    case PackageType.Embedded:
                    {
                        var packageFolderPath = packageSave.SavePackageAtPath(PackagePaths.Packages);
                        if (packageFolderPath == null)
                            return;

                        // Note: UPM_HAS_RESOLVE is set through Version Defines feature of the Assembly Definition for
                        // Unity 2020.1.0f1, where Client.Resolve() public method was introduced and above. But the Version
                        // Defines in Unity 2020 are only fully supported starting with Unity 2020.2.4f1. Before that
                        // (i.e. 2020.1.0f1 to 2020.2.3f1) UPM_HAS_RESOLVE macro is not set and Refresh() is called still.
#if UPM_HAS_RESOLVE
                        PackageManager.Resolve();
#else
                        AssetDatabase.Refresh();
#endif
                        break;
                    }
                    case PackageType.Local:
                    {
                        var path = EditorUtility.OpenFolderPanel(Window.Dialog.SelectFolder.Title, PackagePaths.Assets,
                            null);
                        if (string.IsNullOrEmpty(path))
                            return;

                        var packageFolderPath = packageSave.SavePackageAtPath(path);
                        if (packageFolderPath == null)
                            return;

                        var addRequest =
                            PackageManager.Add(PackagePaths.PackageManagerLocalPackagePathPrefix + packageFolderPath);
                        while (!addRequest.IsCompleted) { }

                        if (addRequest.Status == StatusCode.Failure)
                            throw new InvalidOperationException(
                                $"Failed to add the local package at path {packageFolderPath} to PackageManager");

                        break;
                    }
                    case PackageType.InAssetsFolder:
                    {
                        var packageFolderPath = packageSave.SavePackageAtPath(PackagePaths.Assets);
                        if (packageFolderPath == null)
                            return;

                        AssetDatabase.Refresh();
                        break;
                    }
                }
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
        }

        private void OnDisable() {
            _recommendedTab.OnDisable();
            _packageTab.OnDisable();
            _dependenciesTab.OnDisable();
            _assemblyTab.OnDisable();
        }
    }
}