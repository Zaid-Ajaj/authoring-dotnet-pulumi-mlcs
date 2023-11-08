// *** WARNING: this file was generated by pulumi. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

namespace Pulumi.Random
{
    static class Utilities
    {
        public static string? GetEnv(params string[] names)
        {
            foreach (var n in names)
            {
                var value = global::System.Environment.GetEnvironmentVariable(n);
                if (value != null)
                {
                    return value;
                }
            }
            return null;
        }

        static string[] trueValues = { "1", "t", "T", "true", "TRUE", "True" };
        static string[] falseValues = { "0", "f", "F", "false", "FALSE", "False" };
        public static bool? GetEnvBoolean(params string[] names)
        {
            var s = GetEnv(names);
            if (s != null)
            {
                if (global::System.Array.IndexOf(trueValues, s) != -1)
                {
                    return true;
                }
                if (global::System.Array.IndexOf(falseValues, s) != -1)
                {
                    return false;
                }
            }
            return null;
        }

        public static int? GetEnvInt32(params string[] names) => int.TryParse(GetEnv(names), out int v) ? (int?)v : null;

        public static double? GetEnvDouble(params string[] names) => double.TryParse(GetEnv(names), out double v) ? (double?)v : null;

        [global::System.Obsolete("Please use WithDefaults instead")]
        public static global::Pulumi.InvokeOptions WithVersion(this global::Pulumi.InvokeOptions? options)
        {
            var dst = options ?? new global::Pulumi.InvokeOptions{};
            dst.Version = options?.Version ?? Version;
            return dst;
        }

        public static global::Pulumi.InvokeOptions WithDefaults(this global::Pulumi.InvokeOptions? src)
        {
            var dst = src ?? new global::Pulumi.InvokeOptions{};
            dst.Version = src?.Version ?? Version;
            return dst;
        }

        private readonly static string version;
        public static string Version => version;

        static Utilities()
        {
            version = "4.13.0";
        }
    }

    internal sealed class RandomResourceTypeAttribute : global::Pulumi.ResourceTypeAttribute
    {
        public RandomResourceTypeAttribute(string type) : base(type, Utilities.Version)
        {
        }
    }
}
