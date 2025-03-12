namespace PackageCreator {
    public static class AssemblyDefinitionReferences {
        public static class Title {
            public const string Label = "Assembly Definition References";
            public const string Tooltip = "Other assembly definition files this assembly definition references";
        }

        public static class Guid {
            public static class Use {
                public const string Label = "Use GUIDs";
                public const string Tooltip = "Use GUIDs instead of Assembly names";
            }

            public const string Key = "GUID";
            public const char Separator = ':';
        }
    }
}