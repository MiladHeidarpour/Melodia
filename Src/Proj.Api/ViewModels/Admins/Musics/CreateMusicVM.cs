using Microsoft.AspNetCore.Mvc;
using Proj.Api.ViewModels.Admins.Musics.ArtistMusic;
using System.ComponentModel.DataAnnotations;

namespace Proj.Api.ViewModels.Admins.Musics;

/// <summary>
/// مدل ثبت موزیک
/// </summary>
public class CreateMusicVM
{
    /// <summary>
    /// نام موزیک
    /// </summary>
    [Display(Name = "نام موزیک")]
    [Required(ErrorMessage = "نام موزیک را وارد کنید")]
    [MaxLength(250, ErrorMessage = "نام موزیک نباید بیشتر از 250 کارکتر باشد")]
    public string TrackName { get; set; }


    /// <summary>
    /// آلبوم
    /// </summary>
    [Display(Name = "آلبوم")]
    [Required(ErrorMessage = "آلبوم را وارد کنید")]
    public long AlbumId { get; set; }


    /// <summary>
    /// فایل موزیک
    /// </summary>
    [Display(Name = "فایل موزیک")]
    [Required(ErrorMessage = "فایل موزیک را وارد کنید")]
    public IFormFile TrackFile { get; set; }



    /// <summary>
    ///  مدت زمان
    /// </summary>
    [Display(Name = "مدت زمان")]
    [Required(ErrorMessage = "مدت زمان را وارد کنید")]
    public string TrackTime { get; set; }



    /// <summary>
    ///  تاریخ انتشار
    /// </summary>
    [Display(Name = "تاریخ انتشار")]
    [Required(ErrorMessage = "تاریخ انتشار را وارد کنید")]
    public DateTime ReleaseDate { get; set; }


    /// <summary>
    /// متن 
    /// </summary>
    [Display(Name = "متن")]
    public string? Lyric { get; set; }



    /// <summary>
    ///  اسلاگ
    /// </summary>
    [Display(Name = "اسلاگ")]
    [Required(ErrorMessage = "اسلاگ را وارد کنید")]
    public string Slug { get; set; }


    /// <summary>
    ///  سئو
    /// </summary>
    [Display(Name = "SEO")]
    public SeoDataViewModel SeoData { get; set; }



    /// <summary>
    ///  خواننده
    /// </summary>
    [Display(Name = "خواننده")]
    public AddArtistMusicVM ArtistMusic { get; set; }
}