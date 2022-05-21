using AutoMapper;

using API_Models;
using BLL_Domains;
using DAL_Entities;

namespace BLL_Mappers {
    public class StorageMapper : Profile {

        public StorageMapper() {
            CreateMap<StorageDomain, Storage>().ReverseMap();
            CreateMap<StorageDomain, StorageModel>().ReverseMap();
        }

    }
}