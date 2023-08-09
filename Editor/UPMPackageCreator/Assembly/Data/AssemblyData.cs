using System;
using System.Collections.Generic;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class AssemblyData {
        [field:SerializeField] public string AssemblyName { get; set; } = string.Empty;
        [field:SerializeField] public bool AllowUnsafeCode  { get; set; }
        [field:SerializeField] public bool OverrideReferences { get; set; }
        [field:SerializeField] public bool AutoReferenced { get; set; } = true;
        [field:SerializeField] public bool NoEngineReferences { get; set; }
        [field:SerializeField] public string RootNamespace { get; set; } = string.Empty;
        [field:SerializeField] public List<string> DefineConstraints { get; set; } = new List<string>();
        [field:SerializeField] public List<string> References { get; set; } = new List<string>();
        [field:SerializeField] public List<string> IncludePlatforms { get; set; } = new List<string>();
        [field:SerializeField] public List<string> ExcludePlatforms { get; set; } = new List<string>();
        [field:SerializeField] public List<VersionDefine> VersionDefines { get; set; } = new List<VersionDefine>();
        [field:SerializeField] public List<string> PrecompiledReferences { get; set; } = new List<string>();

        public bool IsValid => true;
    }
}