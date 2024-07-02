using Game.Log;

namespace Game
{

    /// <summary>
    /// x.x.x_tag
    /// </summary>
    public class Version : System.IEquatable<Version>
    {
        public static string DefaultVersion = "0.0.0";

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Patch { get; private set; }
        public string Tag { get; private set; }

        public string SimpleVersionName => $"{Major}.{Minor}.{Patch}";
        public string VersionName => string.IsNullOrEmpty(Tag) ? SimpleVersionName : $"{SimpleVersionName}_{Tag}";

        public Version(int major, int minor, int patch, string tag = null)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Tag = tag;
        }

        public Version(string version)
        {
            // tag
            var tagIndex = version.IndexOf('_');
            if (tagIndex != -1)
            {
                Tag = version.Substring(tagIndex + 1);
                version = version.Substring(0, tagIndex);
            }

            var versionArray = version.Split('.');
            if (versionArray.Length < 3)
            {
                GameLog.Error($"版本号格式错误，应该是x.x.x，错误版本号：'{version}'，已替换为 '{DefaultVersion}'");
                versionArray = DefaultVersion.Split('.');
            }
            try
            {
                Major = int.Parse(versionArray[0]);
                Minor = int.Parse(versionArray[1]);
                Patch = int.Parse(versionArray[2]);
            }
            catch (System.Exception)
            {
                GameLog.Error($"版本号格式错误，应该是x.x.x，全数字加小数点，错误版本号：'{version}'，已替换为 '{DefaultVersion}'");
                versionArray = DefaultVersion.Split('.');
                Major = int.Parse(versionArray[0]);
                Minor = int.Parse(versionArray[1]);
                Patch = int.Parse(versionArray[2]);
            }
        }

        public override string ToString() => VersionName;

        public static bool operator >(Version a, Version b)
        {
            if (a.Major > b.Major)
            {
                return true;
            }
            if (a.Minor > b.Minor)
            {
                return true;
            }
            if (a.Patch > b.Patch)
            {
                return true;
            }
            return false;
        }

        public static bool operator <(Version a, Version b)
        {
            if (a.Major < b.Major)
            {
                return true;
            }
            if (a.Minor < b.Minor)
            {
                return true;
            }
            if (a.Patch < b.Patch)
            {
                return true;
            }
            return false;
        }

        public static bool operator ==(Version a, Version b) => a.Major == b.Major && a.Minor == b.Minor && a.Patch == b.Patch && a.Tag == b.Tag;
        public static bool operator !=(Version a, Version b) => !(a == b);

        public bool Equals(Version other)
        {
            return other is not null && this == other;
        }
        public override bool Equals(object obj)
        {
            return obj is Version version && Equals(version);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Major, Minor, Patch, Tag);
        }
    }
}
