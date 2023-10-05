using System.Collections.Generic;

namespace UPMPackageCreator {
        public static class Platforms {
            public static class All {
                // NamedBuildTarget struct is different from what we see in Assembly Definition Inspector
                // RuntimePlatform enum is different from what we see in Assembly Definition Inspector
                // BuildTarget enum is different from what we see in Assembly Definition Inspector (though it is the
                // closest one)
                // So here are custom lists of Platforms that mimic the Assembly Definition Inspector.
                public static readonly IReadOnlyList<string> Keys = new List<string> {
                    "Android",
                    "Editor",
                    "EmbeddedLinux",
                    "GameCoreScarlett",
                    "GameCoreXboxOne",
                    "iOS",
                    "LinuxStandalone64",
                    "CloudRendering",
                    "macOSStandalone",
                    "PS4",
                    "PS5",
                    "QNX",
                    "Stadia",
                    "Switch",
                    "tvOS",
                    "WSA",
                    "WebGL",
                    "WindowsStandalone32",
                    "WindowsStandalone64",
                    "XboxOne"
                };

                public static readonly IReadOnlyList<string> Names = new List<string> {
                    "Android",
                    "Editor",
                    "Embedded Linux",
                    "Game Core - Scarlett",
                    "Game Core - Xbox One",
                    "iOS",
                    "Linux 64-bit",
                    "Linux Headless Simulation",
                    "macOS",
                    "PS4",
                    "PS5",
                    "QNX",
                    "Stadia",
                    "Switch",
                    "tvOS",
                    "Universal Windows Platform",
                    "WebGL",
                    "Windows 32-bit",
                    "Windows 64-bit",
                    "XboxOne"
                };
            }

            public static class Title {
                public const string Label = "Platforms";
                public const string Tooltip = "Here you choose for which platforms this assembly is available";
            }

            public static class Any {
                public const string Label = "Any Platform";
                public const string Tooltip = "When selected, the list below is the exclude platforms list." +
                                              "Otherwise it is the include platforms list.";
            }

            public static class Include {
                public const string Label = "Include Platforms";
            }

            public static class Exclude {
                public const string Label = "Exclude Platforms";
            }

            public static class Button {
                public static class Select {
                    public const string Label = "Select all";
                }

                public static class Deselect {
                    public const string Label = "Deselect all";
                }

                public static class Width {
                    public const float Offset = 8f;
                }
            }
        }

}