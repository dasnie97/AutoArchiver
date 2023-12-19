using System.Collections.Generic;

namespace AutoArchiver.Helpers;

public class DirectoryConfig
{
    public List<string> InputDirectories { get; set; }
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
    public DirectoryConfig DirectoryConfig { get; set; }
}

public class Serilog
{
    public MinimumLevel MinimumLevel { get; set; }
}
