using Autofac;
using System.Threading.Tasks;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IComponentContext context;

        public QueryProcessor(IComponentContext context)
        {
            this.context = context;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = context.Resolve(handlerType);

            return handler.Handle((dynamic)query);
        }

        public async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            return await Task.Factory.StartNew(() => Process(query)).ConfigureAwait(false);
        }
    }
}
