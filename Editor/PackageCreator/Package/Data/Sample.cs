using System;
using UnityEngine;

namespace PackageCreator {
    [Serializable]
    public class Sample {
        [field:SerializeField] public string DisplayName { get; set; } = string.Empty;
        [field:SerializeField] public string Description { get; set; } = string.Empty;
        [field:SerializeField] public string Path { get; set; } = string.Empty;
    }
}