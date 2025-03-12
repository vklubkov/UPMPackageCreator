using System;
using UnityEngine;

namespace PackageCreator {
    [Serializable]
    public class VersionDefine {
        [field:SerializeField] public string Resource { get; set; }
        [field:SerializeField] public string Define { get; set; }
        [field:SerializeField] public string Expression { get; set; }
    }
}