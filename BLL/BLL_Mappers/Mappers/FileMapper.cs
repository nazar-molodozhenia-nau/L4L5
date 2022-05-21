using AutoMapper;

using API_Models;
using BLL_Domains;
using DAL_Entities;

namespace BLL_Mappers {
    public class FileMapper : Profile {

        public FileMapper() {
            CreateMap<FileDomain, File>().ReverseMap();
            CreateMap<FileDomain, FileModel>().ReverseMap();
        }

    }
}