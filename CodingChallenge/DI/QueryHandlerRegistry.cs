using Lamar;
namespace CodingChallenge.DI
{
	public class QueryHandlerRegistry : ServiceRegistry
	{
		public QueryHandlerRegistry()
		{
			Scan(a =>
			{
				a.Assembly("Business"); //Business project
				a.Include(t => t.Name.EndsWith("QueryHandler")); //Finding all the files that ends with query handler
				a.WithDefaultConventions();
			});
		}
	}
}
