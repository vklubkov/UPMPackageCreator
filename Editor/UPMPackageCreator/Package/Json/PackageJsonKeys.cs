namespace UPMPackageCreator {
    public static class PackageJsonKeys {
        public const string PackageId = "name";
        public const string Version = "version";
        public const string DisplayName = "displayName";
        public const string Description = "description";
        public const string UnityVersion = "unity";
        public const string UnityRelease = "unityRelease";
        public const string Category = "category";
        public const string Keywords = "keywords";
        public const string Dependencies = "dependencies";

        public static class Author {
            public const string Class = "author";
            public const string Name = "name";
            public const string Email = "email";
            public const string Website = "url";
        }

        public const string License = "license";
        public const string LicensesUrl = "licensesUrl";
        public const string ChangelogUrl = "changelogUrl";
        public const string DocumentationUrl = "documentationUrl";
        public const string HideInEditor = "hideInEditor";
        public const string Type = "type";

        public static class Samples {
            public const string Class = "samples";
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public const string DisplayName = "displayName";
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public const string Description = "description";
            public const string Path = "path";
        }
    }
}