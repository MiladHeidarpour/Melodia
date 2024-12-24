using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.MusicAlbums.Dtos;
using Proj.Query.MusicAlbums.Mapper;

namespace Proj.Query.MusicAlbums.GetByFilter;

internal class GetMusicAlbumFilterQueryHandler : IQueryHandler<GetMusicAlbumByFilterQuery, MusicAlbumFilterResult>
{
    private readonly ProjContext _context;

    public GetMusicAlbumFilterQueryHandler(ProjContext context)
    {
        _context = context;
    }
    public async Task<MusicAlbumFilterResult> Handle(GetMusicAlbumByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.MusicAlbums.OrderByDescending(s => s.CreationDate).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filterParams.AlbumName))
        {
            result = result.Where(r => r.AlbumName == filterParams.AlbumName);
        }
        if (filterParams.AlbumType != null)
        {
            result = result.Where(r => r.AlbumType == filterParams.AlbumType);
        }
        if (filterParams.ReleaseDate != null)
        {
            result = result.Where(r => r.CreationDate == filterParams.ReleaseDate);
        }
        if (!string.IsNullOrWhiteSpace(filterParams.Slug))
        {
            result = result.Where(r => r.Slug == filterParams.Slug);
        }

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new MusicAlbumFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(s => s.MapListData())
                .ToListAsync(cancellationToken),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }
}