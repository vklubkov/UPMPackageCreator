namespace PackageCreator {
    public static class Footnote {
        public const string One = "*";
        public const string Two = "**";
        public const string Three = "***";
        public const string Four = "****";

        public const string LabelOne = One + " - required fields";
        public const string LabelTwo = Two + " - recommended fields";
        public const string LabelThree = Three + " - undocumented fields, leave blank if unsure";
        public const string LabelFour = Four + " - reserved for internal use";
    }
}