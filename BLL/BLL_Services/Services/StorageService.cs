using System.Collections.Generic;

using AutoMapper;

using BLL_Domains;
using DAL_Entities;
using DAL_UofW;

namespace BLL_Services {
    public class StorageService : IService<StorageDomain> {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StorageService(IUnitOfWork unitOfWork, IMapper mapper) { _unitOfWork = unitOfWork; _mapper = mapper; }

        public void Add(StorageDomain StorageDomain) { _unitOfWork.Storage.Add(_mapper.Map<Storage>(StorageDomain)); }

        public void Update(StorageDomain StorageDomain) { _unitOfWork.Storage.Update(_mapper.Map<Storage>(StorageDomain)); }

        public void Remove(StorageDomain StorageDomain) { _unitOfWork.Storage.Remove(_mapper.Map<Storage>(StorageDomain)); }

        public StorageDomain GetById(int id) { return _mapper.Map<StorageDomain>(_unitOfWork.Storage.GetById(id)); }

        public void Remove(int id) { _unitOfWork.Storage.Remove(id); }

        public List<StorageDomain> GetAll() { return _mapper.Map<List<StorageDomain>>(_unitOfWork.Storage.GetAll()); }
    }
}