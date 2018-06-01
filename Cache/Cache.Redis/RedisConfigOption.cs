using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConsole
{
    public class RedisConfigOption
    {
        public string[] ReadWriteHosts { get; set; }

        public string[] ReadOnlyHosts { get; set; }

        public int MaxWritePoolSize { get; set; }

        public int MaxReadPoolSize { get; set; }

        public bool AutoStart { get; set; }
    }
}
