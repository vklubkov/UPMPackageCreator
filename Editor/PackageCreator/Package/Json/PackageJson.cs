using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PackageCreator {
    public static class PackageJson {
        public static string ToString(
            PackageData packageData, DependenciesData dependenciesData, AuthorData authorData) {
            var packageJson = new JObject();

            // Required properties
            AddPackageId(packageJson, packageData);
            AddVersion(packageJson, packageData);

            // Undocumented properties
            AddCategory(packageJson, packageData);

            // Recommended properties
            AddDisplayName(packageJson, packageData);
            AddDescription(packageJson, packageData);
            AddUnityVersion(packageJson, dependenciesData);

            // Other properties
            AddUnityRelease(packageJson, dependenciesData);
            AddKeywords(packageJson, packageData);
            AddDependencies(packageJson, dependenciesData);
            AddLicense(packageJson, packageData);
            AddLicensesUrl(packageJson, packageData);
            AddChangelogUrl(packageJson, packageData);
            AddDocumentationUrl(packageJson, packageData);
            AddHideInEditor(packageJson, packageData);
            AddType(packageJson, packageData);
            AddSamples(packageJson, packageData);
            AddAuthor(packageJson, authorData);

            return packageJson.ToString(Formatting.Indented);
        }

        private static void AddPackageId(JObject parent, PackageData packageData) =>
            parent.Add(PackageJsonKeys.PackageId, packageData.PackageId);

        private static void AddVersion(JObject parent, PackageData packageData) =>
            parent.Add(PackageJsonKeys.Version, packageData.Version);

        private static void AddDisplayName(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.DisplayName))
                parent.Add(PackageJsonKeys.DisplayName, packageData.DisplayName);
        }

        private static void AddDescription(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.Description))
                parent.Add(PackageJsonKeys.Description, packageData.Description);
        }

        private static void AddUnityVersion(JObject parent, DependenciesData dependenciesData) {
            if (!string.IsNullOrEmpty(dependenciesData.UnityVersion))
                parent.Add(PackageJsonKeys.UnityVersion, dependenciesData.UnityVersion);
        }

        private static void AddUnityRelease(JObject parent, DependenciesData dependenciesData) {
            if (!string.IsNullOrEmpty(dependenciesData.UnityRelease))
                parent.Add(PackageJsonKeys.UnityRelease, dependenciesData.UnityRelease);
        }

        private static void AddCategory(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.Category))
                parent.Add(PackageJsonKeys.Category, packageData.Category);
        }

        private static void AddKeywords(JObject parent, PackageData packageData) {
            if (packageData.Keywords.Count == 0)
                return;

            var keywords = new JArray();
            foreach (var keyword in packageData.Keywords)
                keywords.Add(keyword);

            parent.Add(PackageJsonKeys.Keywords, keywords);
        }

        private static void AddDependencies(JObject parent, DependenciesData dependenciesData) {
            if (dependenciesData.Dependencies.Count == 0)
                return;

            var dependencies = new JObject();
            foreach (var dependency in dependenciesData.Dependencies)
                dependencies.Add(dependency.First, dependency.Second ?? string.Empty);

            parent.Add(PackageJsonKeys.Dependencies, dependencies);
        }

        private static void AddLicense(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.License))
                parent.Add(PackageJsonKeys.License, packageData.License);
        }

        private static void AddLicensesUrl(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.LicensesUrl))
                parent.Add(PackageJsonKeys.LicensesUrl, packageData.LicensesUrl);
        }

        private static void AddChangelogUrl(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.ChangelogUrl))
                parent.Add(PackageJsonKeys.ChangelogUrl, packageData.ChangelogUrl);
        }

        private static void AddDocumentationUrl(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.DocumentationUrl))
                parent.Add(PackageJsonKeys.DocumentationUrl, packageData.DocumentationUrl);
        }

        private static void AddHideInEditor(JObject parent, PackageData packageData) {
            if (packageData.HideInEditor.HasValue)
                parent.Add(PackageJsonKeys.HideInEditor, packageData.HideInEditor.Value);
        }

        private static void AddType(JObject parent, PackageData packageData) {
            if (!string.IsNullOrEmpty(packageData.Type))
                parent.Add(PackageJsonKeys.Type, packageData.Type);
        }

        private static void AddSamples(JObject parent, PackageData packageData) {
            if (packageData.Samples.Count == 0)
                return;

            var samples = new JArray();
            foreach (var sample in packageData.Samples) {
                var sampleJson = new JObject {
                    { PackageJsonKeys.Samples.DisplayName, sample.DisplayName },
                    { PackageJsonKeys.Samples.Description, sample.Description },
                    { PackageJsonKeys.Samples.Path, sample.Path }
                };

                samples.Add(sampleJson);
            }

            parent.Add(PackageJsonKeys.Samples.Class, samples);
        }

        private static void AddAuthor(JObject parent, AuthorData authorData) {
            var hasName = !string.IsNullOrEmpty(authorData.Name);
            var hasEmail = !string.IsNullOrEmpty(authorData.Email);
            var hasWebsite = !string.IsNullOrEmpty(authorData.Website);
            if (!hasName && !hasEmail && !hasWebsite)
                return;

            var author = new JObject();
            if (hasName)
                author.Add(PackageJsonKeys.Author.Name, authorData.Name);

            if (hasEmail)
                author.Add(PackageJsonKeys.Author.Email, authorData.Email);

            if (hasWebsite)
                author.Add(PackageJsonKeys.Author.Website, authorData.Website);

            parent.Add(PackageJsonKeys.Author.Class, author);
        }
    }
}