namespace App;

using Onm;

public class AppMapper
{
	public IOnmPlaylist MapSiiSeasonToOnmPlaylist(Sii.Season season)
	{
		return new OnmPlaylist {
			Id = season.Id,
			Title = season.Title,
			Description = season.Description,
			Image = season.ImageUrl,
			CoverImage = season.CoverImageUrl,
			Creator = season.Studio,
			ReleaseDate = DateOnly.Parse($"{season.ReleaseDate}-01-01"),
			Source = ".",
			Meta = new(),
			Media = season.Episodes.Select(e => new OnmMedia {
				Id = e.Id,
				Title = e.Title,
				Description = season.Description,
				Image = season.CoverImageUrl,
				Creator = season.Studio,
				ReleasseDate = DateOnly.Parse($"{season.ReleaseDate}-01-01"),
				Source = $"{season.Id}_{e.Id}.m3u8",
				Meta = new()
			}).ToList<IOnmMedia>()
		};
	}
}
