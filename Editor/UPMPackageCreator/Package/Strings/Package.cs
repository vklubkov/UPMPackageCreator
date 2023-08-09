namespace UPMPackageCreator {
    public static class Package {
        public const string Tab = "Package";

        public static class Id {
            public const string Label = "Package Id";
            public const string Tooltip = "Package id/name. Usually follows the format: " +
                                          "“com.yourname.packagename“ e.g., “com.vladimirklubkov.packagecreator“";
        }

        public static class Version {
            public const string Label = "Version";
            public const string Tooltip = "Package version. Format is “Major.Minor.Patch“ e.g., “1.0.0“";
        }

        public static class DisplayName {
            public const string Label = "Display Name";
            public const string Tooltip = "Package name displayed in Unity e.g., “Package Creator“";
        }

        public static class Description {
            public const string Label = "Package Description";
            public const string Tooltip = "Package description in free form. Rememeber that " +
                                          "package settings are stored as a JSON file.";
        }

        public static class Category {
            public const string Label = "Category";
            public const string Tooltip = "Undocumented property, serves for categorizing " +
                                          "packages in UPM window. Leave blank if unsure";
        }

        public static class Keywords {
            public const string Label = "Keywords";
            public const string Tooltip = "Keywords used when searching for packages.";
        }

        public static class MainNamespace {
            public const string Label = "Main Namespace";
            public const string Tooltip = "The main namespace of the package. A folder with this name is " +
                                          "created as well. In case the Root Namespace of an assembly is set, " +
                                          "then the order is RootRamespace.MainNamespace";
        }

        public static class License {
            public const string Label = "License";
            public const string Tooltip = "Unity documentation recommends using using the SPDX identifier format, " +
                                          "or writing a string like “See LICENSE.md file”";
        }

        public static class LicensesUrl {
            public const string Label = "Licenses Url";
            public const string Tooltip = "Custom location for the license.";
        }

        public static class ChangelogUrl {
            public const string Label = "Changelog Url";
            public const string Tooltip = "Custom location for the changelog.";
        }

        public static class DocumentationUrl {
            public const string Label = "Documentation Url";
            public const string Tooltip = "Custom location for the documentation";
        }

        public static class VisibilityInEditor {
            public static class Title {
                public const string Label = "Visibility in Editor";
                public const string Tooltip = "Hides assets from Editor e.g., from Project window " +
                                              "and Object Picker. Note: packages are hidden by default.";
            }

            public static class Items {
                public const string DefaultVisibility = "Default Visibility";
                public const string AlwaysHidden = "Always Hidden";
                public const string AlwaysVisible = "Always Visible";
            }
        }

        public static class Type {
            public const string Label = "Type";
            public const string Tooltip = "Provides additional information to Package Manager. Reserved for Unity " +
                                          "internal use. Leave blank unless you know what you are doing.";
        }
    }
}