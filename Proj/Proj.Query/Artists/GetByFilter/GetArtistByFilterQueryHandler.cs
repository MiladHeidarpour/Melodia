using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Artists.Dtos;
using Proj.Query.Artists.Mapper;

namespace Proj.Query.Artists.GetByFilter;

internal class GetArtistByFilterQueryHandler : IQueryHandler<GetArtistByFilterQuery, ArtistFilterResult>
{
    private readonly ProjContext _context;

    public GetArtistByFilterQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<ArtistFilterResult> Handle(GetArtistByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.Artists.OrderByDescending(c => c.CreationDate).AsQueryable();
        if (filterParams.ArtistId != null)
        {
            result = result.Where(r => r.Id == filterParams.ArtistId);
        }

        var skip = (filterParams.PageId - 1) * filterParams.Take;

        var model = new ArtistFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(comment => ArtistMapper.Map(comment)).ToListAsync(cancellationToken),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }
}