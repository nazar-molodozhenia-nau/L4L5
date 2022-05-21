using System.Collections.Generic;

using AutoMapper;

using API_Models;
using BLL_Domains;
using BLL_Services;

namespace API_Controllers {
    public class FileController : IController<FileModel> {

        private readonly IService<FileDomain> _service;
        private readonly IMapper _mapper;

        public FileController(IService<FileDomain> service, IMapper mapper) { _service = service; _mapper = mapper; }

        public void Add(FileModel FileDomain) { _service.Add(_mapper.Map<FileDomain>(FileDomain)); }

        public void Update(FileModel FileDomain) { _service.Update(_mapper.Map<FileDomain>(FileDomain)); }

        public void Remove(FileModel FileDomain) { _service.Remove(_mapper.Map<FileDomain>(FileDomain)); }

        public FileModel GetById(int id) { return _mapper.Map<FileModel>(_service.GetById(id)); }
        
        public void Remove(int id) { _service.Remove(id); }

        public List<FileModel> GetAll() { return _mapper.Map<List<FileModel>>(_service.GetAll()); }

    }
}