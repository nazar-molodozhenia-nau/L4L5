using System.Collections.Generic;

using AutoMapper;

using API_Models;
using BLL_Domains;
using BLL_Services;

namespace API_Controllers {
    public class StorageController : IController<StorageModel> {

        private readonly IService<StorageDomain> _service;
        private readonly IMapper _mapper;

        public StorageController(IService<StorageDomain> service, IMapper mapper) { _service = service; _mapper = mapper; }

        public void Add(StorageModel StorageDomain) { _service.Add(_mapper.Map<StorageDomain>(StorageDomain)); }

        public void Update(StorageModel StorageDomain) { _service.Update(_mapper.Map<StorageDomain>(StorageDomain)); }

        public void Remove(StorageModel StorageDomain) { _service.Remove(_mapper.Map<StorageDomain>(StorageDomain)); }

        public StorageModel GetById(int id) { return _mapper.Map<StorageModel>(_service.GetById(id)); }
        
        public void Remove(int id) { _service.Remove(id); }

        public List<StorageModel> GetAll() { return _mapper.Map<List<StorageModel>>(_service.GetAll()); }

    }
}