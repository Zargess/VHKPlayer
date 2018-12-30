using System.Threading.Tasks;
using Autofac;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IComponentContext _context;

        public QueryProcessor(IComponentContext context)
        {
            this._context = context;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _context.Resolve(handlerType);

            return handler.Handle((dynamic) query);
        }

        public async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            return await Task.Factory.StartNew(() => Process(query)).ConfigureAwait(false);
        }
    }
}