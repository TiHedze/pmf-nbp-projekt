namespace Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Neo4jSettings
    {
        public const string Key = nameof(Neo4jSettings);
        public string ConnectionString { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
