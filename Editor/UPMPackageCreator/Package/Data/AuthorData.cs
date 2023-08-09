using System;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class AuthorData {
        [field:SerializeField] public string Name { get; set; } = string.Empty;
        [field:SerializeField] public string Email { get; set; } = string.Empty;
        [field:SerializeField] public string Website { get; set; } = string.Empty;

        public bool IsValid => true;
    }
}