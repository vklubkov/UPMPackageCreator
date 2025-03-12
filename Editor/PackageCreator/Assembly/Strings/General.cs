namespace PackageCreator {
    public static class General {
        public static class Title {
            public const string Label = "General";
        }

        public static class AllowUnsafeCode {
            public const string Label = "Allow Unsafe Code";
            public const string Tooltip = "Allows the use of unsafe code in this assembly";
        }

        public static class AutoReferenced {
            public const string Label = "Auto Referenced";
            public const string Tooltip = "Makes this assembly auto-referenced by Unity project";
        }

        public static class NoEngineReferences {
            public const string Label = "No Engine References";
            public const string Tooltip = "Removes UnityEngine/UnityEditor references";
        }

        public static class OverrideReferences {
            public const string Label = "Override References";
            public const string Tooltip = "Override Unity assemblies referenced by this one";
        }

        public static class RootNamespace {
            public const string Label = "Root Namespace";
            public const string Tooltip = "Root namespace of the package name, e.g. PackageName";
        }
    }
}