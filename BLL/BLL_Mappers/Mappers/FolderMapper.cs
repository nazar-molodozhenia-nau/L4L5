using AutoMapper;

using API_Models;
using BLL_Domains;
using DAL_Entities;

namespace BLL_Mappers {
    public class FolderMapper : Profile {

        public FolderMapper() {
            CreateMap<FolderDomain, Folder>().ReverseMap();
            CreateMap<FolderDomain, FolderModel>().ReverseMap();
        }

    }
}