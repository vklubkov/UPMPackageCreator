using System.IO;
using System.Linq;
using UnityEditor;

namespace PackageCreator {
    public class PackageSave {
        private readonly PackageData _packageData;
        private readonly DependenciesData _dependenciesData;
        private readonly AssemblyData _runtimeAssemblyData;
        private readonly AssemblyData _editorAssemblyData;
        private readonly AuthorData _authorData;

        public PackageSave(PackageData packageData, DependenciesData dependenciesData,
            AssemblyData runtimeAssemblyData, AssemblyData editorAssemblyData, AuthorData authorData) {
            _packageData = packageData;
            _dependenciesData = dependenciesData;
            _runtimeAssemblyData = runtimeAssemblyData;
            _editorAssemblyData = editorAssemblyData;
            _authorData = authorData;
        }

        public string SavePackageAtPath(string path) {
            var packageFolderPath = Path.Combine(path, _packageData.DirectoryName);
            if (Directory.Exists(packageFolderPath)) {
                EditorUtility.DisplayDialog(
                    Window.Dialog.AlreadyExists.Title,
                    string.Format(Window.Dialog.AlreadyExists.DescriptionFormat, _packageData.DirectoryName, path),
                    Window.Dialog.AlreadyExists.Ok);

                return null;
            }

            Directory.CreateDirectory(packageFolderPath);
            var packageManifestPath = Path.Combine(packageFolderPath, PackagePaths.PackageManifestFileName);
            using (var packageManifest = File.CreateText(packageManifestPath)) {
                var packageManifestContent = PackageJson.ToString(_packageData, _dependenciesData, _authorData);
                packageManifest.Write(packageManifestContent);
            }

            var runtimeFolderPath = SaveAssemblyFile(
                _runtimeAssemblyData, packageFolderPath, AssemblyPaths.RuntimeFolderName);

            var editorAssemblyName = GetEditorAssemblyName(
                _runtimeAssemblyData.AssemblyName,  _editorAssemblyData.AssemblyName);

            _editorAssemblyData.AssemblyName = editorAssemblyName;
            var editorFolderPath = SaveAssemblyFile(
                _editorAssemblyData, packageFolderPath, AssemblyPaths.EditorFolderName);

            CreateMainFolders(packageFolderPath, runtimeFolderPath, editorFolderPath);
            return packageFolderPath;
        }

        private string GetEditorAssemblyName(string runtimeAssemblyName, string editorAssemblyName) {
            if (string.IsNullOrEmpty(editorAssemblyName))
                return editorAssemblyName;

            if (runtimeAssemblyName != editorAssemblyName)
                return editorAssemblyName;

            var fileNameWithoutExtension = editorAssemblyName.EndsWith(AssemblyPaths.AssemblyExtension)
                ? Path.GetFileNameWithoutExtension(editorAssemblyName)
                : editorAssemblyName;

            var editorSuffix = fileNameWithoutExtension.Any(char.IsUpper)
                ? AssemblyPaths.EditorSuffix
                : AssemblyPaths.EditorSuffix.ToLower();

            return fileNameWithoutExtension + editorSuffix;
        }

        private string SaveAssemblyFile(AssemblyData assemblyData, string packageDirectoryPath, string folderName) {
            if (string.IsNullOrEmpty(assemblyData.AssemblyName))
                return null;

            var directoryPath = Path.Combine(packageDirectoryPath, folderName);
            Directory.CreateDirectory(directoryPath);
            var assemblyPath = Path.Combine(directoryPath, assemblyData.AssemblyName);
            if (!assemblyPath.EndsWith(AssemblyPaths.AssemblyExtension))
                assemblyPath = $"{assemblyPath}{AssemblyPaths.AssemblyExtension}";

            using (var assembly = File.CreateText(assemblyPath)) {
                var assemblyContent = AssemblyJson.ToString(assemblyData);
                assembly.Write(assemblyContent);
            }

            return directoryPath;
        }

        private void CreateMainFolders(string packageFolderPath, string runtimeFolderPath, string editorFolderPath) {
            if (string.IsNullOrEmpty(_packageData.MainNamespace))
                return;

            var hasRuntimeFolder = !string.IsNullOrEmpty(runtimeFolderPath);
            if (hasRuntimeFolder)
                CreateMainFolder(runtimeFolderPath);

            var hasEditorFolder = !string.IsNullOrEmpty(editorFolderPath);
            if (hasEditorFolder)
                CreateMainFolder(editorFolderPath);

            if (!hasRuntimeFolder && !hasEditorFolder)
                CreateMainFolder(packageFolderPath);
        }

        private void CreateMainFolder(string path) {
            var packageFolderPath = Path.Combine(path, _packageData.MainNamespace);
            Directory.CreateDirectory(packageFolderPath);
        }
    }
}