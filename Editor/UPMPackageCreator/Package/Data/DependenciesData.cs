using System;
using System.Collections.Generic;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class DependenciesData {
        [field:SerializeField] public string UnityVersion { get; set; } = string.Empty;
        [field:SerializeField] public string UnityRelease { get; set; } = string.Empty;
        [field:SerializeField] public List<StringPair> Dependencies { get; set; } = new List<StringPair>();

        public bool IsValid => true;
    }
}