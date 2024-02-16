using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.Machine;
using Core.Helpers;
using Core.ViewModels.Auth;
using Core.ViewModels.Machine;
using System.Drawing.Imaging;

namespace PracticeStudy._2.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.Phone))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<PropertyViewModel, Property>();

            CreateMap<CarBrandViewModel, CarBrand>();

            CreateMap<CarYearViewModel, CarYear>();
            
            CreateMap<CarPhotoViewModel, CarPhoto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => MapPhotoName(x.Photo)))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CharacteristicViewModel, Characteristic>();

            CreateMap<CarViewModel, Car>()
                .ForMember(x => x.Characteristics, opt => opt.Ignore())
                .ForMember(x => x.Photos, opt => opt.Ignore())
                .ForMember(x => x.Brand, opt => opt.Ignore())
                .ForMember(x => x.Year, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }

        private static string? MapPhotoName(string base64Photo)
        {
            if (base64Photo == null)
                return null;

            var img = base64Photo.FromBase64StringToImage();
            string randomFilename = Path.GetRandomFileName() + Guid.NewGuid() + ".jpeg";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);

            img.Save(dir, ImageFormat.Jpeg);

            return randomFilename;
        }
    }
}
