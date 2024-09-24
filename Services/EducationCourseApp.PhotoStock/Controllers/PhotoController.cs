using EducationCourseApp.PhotoStock.Dtos;
using EducationCourseApp.Shared.Controllers;
using EducationCourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.PhotoStock.Controllers;
[Route("api/[controller]/[action]")]
public class PhotoController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> Save(IFormFile photo, CancellationToken cancellationToken)
    {
        if (photo is not null && photo.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos/",photo.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);
            var returnPath = "photos" + photo.FileName;
            var photoDto = new PhotoDto()
            {
                Url = returnPath
            };
            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty!", 400));

    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos/", photoUrl);
        if (!System.IO.File.Exists(path))
        {
            return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found!", 404));
        }
        System.IO.File.Delete(path);
        return CreateActionResultInstance(Response<NoContent>.Success(204));
    }
}