using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Filters;
using ShopApi.Helpers;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [Authorize]
    [Route("api/user/{userId}/photos")]
    [ApiController]
    public class PhotoController : BaseController
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotoController(IGenericUnitOfWork uow, IMapper mapper,
        IOptions<CloudinarySettings> cloudinaryConfig) : base(uow, mapper)
        {
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _uow.Repository<SystemVariable>().GetByID(x => x.Key == "ApiSecret").Value
            );
            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _uow.Repository<Photo>().GetByIDAsync(x => x.Id == id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await _uow.Repository<User>().GetByIDAsync(x => x.Id == userId, x => x.Photos);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.UserId = userId;

            if (!userFromRepo.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }

            _uow.Repository<Photo>().InsertAsync(photo);

            if (await _uow.SaveChangesAsync())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Nie udało sie dodać zdjęcia");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var photoFromRepo = await _uow.Repository<Photo>().GetByIDAsync(x => x.Id == id && x.UserId == userId);

            if (photoFromRepo == null)
            {
                return Unauthorized();
            }

            if (photoFromRepo.IsMain)
            {
                return BadRequest("To zdjecie jest głównym zdjęciem");
            }

            var photos = await _uow.Repository<Photo>().Get(x => x.UserId == userId);

            photos.ToList().ForEach(x => x.IsMain = false);

            photoFromRepo.IsMain = true;

            if (await _uow.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Nie udało sie ustawić zdjęcia głównego");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var photoFromRepo = await _uow.Repository<Photo>().GetByIDAsync(x => x.Id == id && x.UserId == userId);

            if (photoFromRepo == null)
            {
                return Unauthorized();
            }

            if (photoFromRepo.IsMain)
            {
                return BadRequest("Nie można usunąć zdjęcia głównego");
            }

            if (photoFromRepo.PublicId != null)
            {
                var deleteParam = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deleteParam);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    _uow.Repository<Photo>().Delete(id);

                }
            }
            else
            {
                _uow.Repository<Photo>().Delete(id);
            }


            if (await _uow.SaveChangesAsync())
            {
                return Ok();
            }

            return BadRequest("Nie udało sie usunąć zdjęcia");
        }

    }
}