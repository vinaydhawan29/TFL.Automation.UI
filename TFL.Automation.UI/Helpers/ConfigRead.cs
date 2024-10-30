using Newtonsoft.Json.Linq;

namespace TFL.Automation.UI.Helpers
{
    public static class ConfigRead
    {

        public static void Set(string key, string value) => Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process);

        public static string Get(string key, string @default = default) => Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process) ?? Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User) ?? Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine) ?? default;

        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);

        public static void LoadEnvironmentSettingsFile(bool optional = true, bool forceOverride = true, string root = "", string environmentVariableName = "xunit_environment_config")
        {
            // This is used for local test runs and if we require to run tests locally against specific environments
            // Will require xunit_environment_config, or what ever the value of environmentVariableName being passed,
            // to be set on the environment of the machine.

            string filePath = "local.settings.local.json";

            if (!optional && !string.IsNullOrWhiteSpace(root) && !Directory.Exists(root)) throw new DirectoryNotFoundException($"'{root}' not found");

            if (!root.IsNullOrWhiteSpace())
                filePath = Path.Combine(root, filePath);

            if (optional && !File.Exists(filePath)) return;
            if (!optional && !File.Exists(filePath)) throw new FileNotFoundException("File not found.", filePath);

            var json = File.ReadAllText(filePath);

            var values = JObject.Parse(json).Value<JObject>("Values");

            foreach (var item in values)
                if (forceOverride || (!forceOverride && Environment.GetEnvironmentVariable(item.Key, EnvironmentVariableTarget.Process) == null))
                    Set(item.Key, item.Value.ToString());
        }
    }
}
