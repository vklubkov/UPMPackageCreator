namespace PackageCreator {
    public static class AssemblyJsonKeys {
        public const string AssemblyName = "name";
        public const string RootNamespace = "rootNamespace";
        public const string References = "references";
        public const string IncludePlatforms = "includePlatforms";
        public const string ExcludePlatforms = "excludePlatforms";
        public const string AllowUnsafeCode = "allowUnsafeCode";
        public const string OverrideReferences = "overrideReferences";
        public const string PrecompiledReferences = "precompiledReferences";
        public const string AutoReferenced = "autoReferenced";
        public const string DefineConstraints = "defineConstraints";

        public static class VersionDefines {
            public const string Class = "versionDefines";
            public const string Name = "name";
            public const string Expression = "expression";
            public const string Define = "define";
        }

        public const string NoEngineReferences = "noEngineReferences";
    }
}