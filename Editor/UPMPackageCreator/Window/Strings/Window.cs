using UnityEngine;

namespace UPMPackageCreator {
    public static class Window {
        public static class Title {
            public const string CreateEmbedded = Menu.CreateEmbedded;
            public const string CreateLocal = Menu.CreateLocal;
            public const string CreateInAssetsFolder = Menu.CreateInAssetsFolder;
        }

        public static class Dialog {
            public static class SelectFolder {
                public const string Title = "Select folder...";
            }

            public static class AlreadyExists {
                public const string Title = "Error saving package";
                public const string DescriptionFormat = "A folder with the name \"{0}\" already exists at {1}";
                public const string Ok = "OK";
            }
        }

        public static readonly Vector2 Size = new Vector2(640, 480);
    }
}