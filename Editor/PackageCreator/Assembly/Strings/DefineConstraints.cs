namespace PackageCreator {
    public static class DefineConstraints {
        public const string Label = "Define Constraints";
        public const string Tooltip = "Assembly builds only if these constraints are true." +
                                      "“!“ negation and “||“ operator are permitted.\n\n" +
                                      "Note: Assembly Definition Inspector validates these " +
                                      "macro defines. Package Creator doesn't do that, so you " +
                                      "are not restricted in the future versions of Unity.";
    }
}