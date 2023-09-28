using System.Collections.Generic;

namespace Elite3E.Infrastructure.Database.DataCleanup
{
   public static class CleanupCommands
   {
       public static IEnumerable<string> EntityCommands
       {
           get;
           private set;
       }
           = new[]
           {
               "delete from tablename where Id = @Id"
           };
   }
}
