using System.Collections.Generic;

using AutoMapper;

using API_Models;
using BLL_Domains;
using BLL_Services;

namespace API_Controllers {
    public class FolderController : IController<FolderModel> {

        private readonly IService<FolderDomain> _service;
        private readonly IMapper _mapper;

        public FolderController(IService<FolderDomain> service, IMapper mapper) { _service = service; _mapper = mapper; }

        public void Add(FolderModel FolderDomain) { _service.Add(_mapper.Map<FolderDomain>(FolderDomain)); }

        public void Update(FolderModel FolderDomain) { _service.Update(_mapper.Map<FolderDomain>(FolderDomain)); }

        public void Remove(FolderModel FolderDomain) { _service.Remove(_mapper.Map<FolderDomain>(FolderDomain)); }

        public FolderModel GetById(int id) { return _mapper.Map<FolderModel>(_service.GetById(id)); }
        
        public void Remove(int id) { _service.Remove(id); }

        public List<FolderModel> GetAll() { return _mapper.Map<List<FolderModel>>(_service.GetAll()); }

    }
}