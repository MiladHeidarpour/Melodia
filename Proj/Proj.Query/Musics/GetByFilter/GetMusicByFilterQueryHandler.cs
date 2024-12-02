using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Musics.Dtos;
using Proj.Query.Musics.Mapper;

namespace Proj.Query.Musics.GetByFilter;

internal class GetMusicByFilterQueryHandler : IQueryHandler<GetMusicByFilterQuery, MusicFilterResult>
{
    private readonly ProjContext _context;

    public GetMusicByFilterQueryHandler(ProjContext context)
    {
        _context = context;
    }

    public async Task<MusicFilterResult> Handle(GetMusicByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.Musics.OrderByDescending(s => s.CreationDate).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filterParams.TrackName))
        {
            result = result.Where(r => r.TrackName == filterParams.TrackName);
        }
        if (filterParams.AlbumId != null)
        {
            result = result.Where(r => r.AlbumId == filterParams.AlbumId);
        }
        if (filterParams.ReleaseDate != null)
        {
            result = result.Where(r => r.ReleaseDate == filterParams.ReleaseDate);
        }
        if (!string.IsNullOrWhiteSpace(filterParams.Slug))
        {
            result = result.Where(r => r.Slug == filterParams.Slug);
        }

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new MusicFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(s => s.MapListData())
                .ToListAsync(cancellationToken),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }
}