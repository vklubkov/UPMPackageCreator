using System;
using System.Collections.Generic;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class PackageData {
        [field:SerializeField] public string DirectoryName { get; set; } = string.Empty;
        [field:SerializeField] public string PackageId { get; set; } = string.Empty;
        [field:SerializeField] public string DisplayName { get; set; } = string.Empty;
        [field:SerializeField] public string Version { get; set; } = string.Empty;
        [field:SerializeField] public string Description { get; set; } = string.Empty;
        [field:SerializeField] public string Category { get; set; } = string.Empty;
        [field:SerializeField] public string Type { get; set; } = string.Empty;
        [field:SerializeField] public string MainNamespace { get; set; } = string.Empty;
        [field:SerializeField] public List<string> Keywords { get; set; } = new List<string>();
        [field:SerializeField] public string License { get; set; } = string.Empty;
        [field:SerializeField] public string LicensesUrl { get; set; } = string.Empty;
        [field:SerializeField] public string ChangelogUrl { get; set; } = string.Empty;
        [field:SerializeField] public string DocumentationUrl { get; set; } = string.Empty;
        [field:SerializeField] public bool? HideInEditor { get; set; } = null;
        [field:SerializeField] public List<Sample> Samples { get; set; } = new List<Sample>();

        public bool IsValid => !string.IsNullOrEmpty(DirectoryName) &&
                               !string.IsNullOrEmpty(PackageId) &&
                               !string.IsNullOrEmpty(Version);
    }
}