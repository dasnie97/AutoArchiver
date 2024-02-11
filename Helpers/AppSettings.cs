using System.Collections.Generic;

namespace AutoArchiver.Helpers;

public class Config
{
    public Directories Directories { get; set; }
    public List<Extension> Extensions { get; set; }
    public int ArchiveTime { get; set; }
    public List<string> ArchivePathOptions { get; set; }
    public string ArchivePath { get; set; }
}

public class Directories
{
    public List<string> Input { get; set; }
}

public class Extension
{
    public string Name { get; set; }
    public bool? IsActive { get; set; }
}

public class MinimumLevel
{
    public string Default { get; set; }
    public Override Override { get; set; }
}

public class Override
{
    public string Microsoft { get; set; }
    public string System { get; set; }
}

public class AppSettings
{
    public Serilog Serilog { get; set; }
    public Config Config { get; set; }
}

public class Serilog
{
    public MinimumLevel MinimumLevel { get; set; }
}
