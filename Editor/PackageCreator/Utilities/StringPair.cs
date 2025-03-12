using System;
using UnityEngine;

namespace PackageCreator {
    [Serializable]
    public class StringPair {
        [field:SerializeField] public string First { get; set; }
        [field:SerializeField] public string Second { get; set; }
    }
}