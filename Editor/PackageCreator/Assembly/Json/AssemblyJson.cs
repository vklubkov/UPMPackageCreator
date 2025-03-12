using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PackageCreator {
    public static class AssemblyJson {
        public static string ToString(AssemblyData assemblyData) {
            var assemblyJson = new JObject();
            AddAssemblyName(assemblyJson, assemblyData);
            AddRootNamespace(assemblyJson, assemblyData);
            AddAssemblyReferences(assemblyJson, assemblyData);
            AddIncludePlatforms(assemblyJson, assemblyData);
            AddExcludePlatforms(assemblyJson, assemblyData);
            AddAllowUnsafeCode(assemblyJson, assemblyData);
            AddOverrideReferences(assemblyJson, assemblyData);
            AddPrecompiledReferences(assemblyJson, assemblyData);
            AddAutoReferenced(assemblyJson, assemblyData);
            AddDefineConstraints(assemblyJson, assemblyData);
            AddVersionDefines(assemblyJson, assemblyData);
            AddNoEngineReferences(assemblyJson, assemblyData);
            return assemblyJson.ToString(Formatting.Indented);
        }

        private static void AddAssemblyName(JObject parent, AssemblyData assemblyData) =>
            parent.Add(AssemblyJsonKeys.AssemblyName, assemblyData.AssemblyName);

        private static void AddRootNamespace(JObject parent, AssemblyData assemblyData) =>
            parent.Add(AssemblyJsonKeys.RootNamespace, assemblyData.RootNamespace);

        private static void AddAssemblyReferences(JObject parent, AssemblyData assemblyData) {
            var references = new JArray();
            foreach (var reference in assemblyData.References)
                 references.Add(reference);

            parent.Add(AssemblyJsonKeys.References, references);
        }

        private static void AddIncludePlatforms(JObject parent, AssemblyData assemblyData) {
            var includePlatforms = new JArray();
            foreach (var platform in assemblyData.IncludePlatforms)
                includePlatforms.Add(platform);

            parent.Add(AssemblyJsonKeys.IncludePlatforms, includePlatforms);
        }

        private static void AddExcludePlatforms(JObject parent, AssemblyData assemblyData) {
            var excludePlatforms = new JArray();
            foreach (var platform in assemblyData.ExcludePlatforms)
                excludePlatforms.Add(platform);

            parent.Add(AssemblyJsonKeys.ExcludePlatforms, excludePlatforms);
        }

        private static void AddAllowUnsafeCode(JObject parent, AssemblyData assemblyData) =>
            parent.Add(AssemblyJsonKeys.AllowUnsafeCode, assemblyData.AllowUnsafeCode);

        private static void AddOverrideReferences(JObject parent, AssemblyData assemblyData) =>
            parent.Add(AssemblyJsonKeys.OverrideReferences, assemblyData.OverrideReferences);

        private static void AddPrecompiledReferences(JObject parent, AssemblyData assemblyData) {
            var precompiledReferences = new JArray();
            foreach (var precompiledReference in assemblyData.PrecompiledReferences)
                precompiledReferences.Add(precompiledReference);

            parent.Add(AssemblyJsonKeys.PrecompiledReferences, precompiledReferences);
        }

        private static void AddAutoReferenced(JObject parent, AssemblyData assemblyData) =>
            parent.Add(AssemblyJsonKeys.AutoReferenced, assemblyData.AutoReferenced);

        private static void AddDefineConstraints(JObject parent, AssemblyData assemblyData) {
            var defineConstraints = new JArray();
            foreach (var constraint in assemblyData.DefineConstraints)
                defineConstraints.Add(constraint);

            parent.Add(AssemblyJsonKeys.DefineConstraints, defineConstraints);
        }

        private static void AddVersionDefines(JObject parent, AssemblyData assemblyData) {
            var versionDefines = new JArray();
            foreach (var versionDefine in assemblyData.VersionDefines) {
                var versionDefineObject = new JObject {
                    { AssemblyJsonKeys.VersionDefines.Name, versionDefine.Resource },
                    { AssemblyJsonKeys.VersionDefines.Expression, versionDefine.Expression },
                    { AssemblyJsonKeys.VersionDefines.Define, versionDefine.Define }
                };

                versionDefines.Add(versionDefineObject);
            }
            parent.Add(AssemblyJsonKeys.VersionDefines.Class, versionDefines);
        }

        private static void AddNoEngineReferences(JObject parent, AssemblyData assemblyData) =>
            parent.Add(AssemblyJsonKeys.NoEngineReferences, assemblyData.NoEngineReferences);
    }
}