namespace UPMPackageCreator {
    public static class Dependencies {
        public const string Tab = "Dependencies";

        public static class Unity {
            public static class Version {
                public const string Label = "Unity Version";
                public const string Tooltip = "Minimal supported Unity Version. " +
                                              "Format is “Major.Minor”, e.g. ”2022.3”";
            }

            public static class Release {
                public const string Label = "Unity Release";
                public const string Tooltip = "Minimal supported Release version of Unity. " +
                                              "Format is NfM where N and M are numbers, e.g. ”3f1”";
            }

            public static class Packages {
                public const string Label = "Dependencies";
                public const string Tooltip = "First field is Package Id or git Url. " +
                                              "Second field is Package Version";
            }
        }
    }
}