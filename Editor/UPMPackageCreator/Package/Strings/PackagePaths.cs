using System.IO;
using UnityEngine;

namespace UPMPackageCreator {
    public static class PackagePaths {
        private const string PackagesFolderName = "Packages";

        public static string Packages {
            get {
                var assetsPath = Assets;
                var dirInfo = new DirectoryInfo(assetsPath);
                // ReSharper disable once PossibleNullReferenceException
                // because it is better to throw early in Editor
                var parent = dirInfo.Parent.FullName;
                return Path.Combine(parent, PackagesFolderName);
            }
        }

        public static string Assets => Application.dataPath;

        public const string PackageManifestFileName = "package.json";

        public const string PackageManagerLocalPackagePathPrefix = "file:";
    }
}