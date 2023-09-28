using System.Collections.Generic;

namespace Elite3E.Infrastructure.Database.DataCreation
{
    public static class PopulateCommands
    {
        public static IEnumerable<string> EntityCommands
        {
            get;
            private set;
        }
            = new[]
            {
                "insert into tablename columns values"
            };
    }
}
