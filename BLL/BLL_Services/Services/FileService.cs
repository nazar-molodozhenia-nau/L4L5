using System.Collections.Generic;

using AutoMapper;

using BLL_Domains;
using DAL_Entities;
using DAL_UofW;

namespace BLL_Services {
    public class FileService : IService<FileDomain> {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileService(IUnitOfWork unitOfWork, IMapper mapper) { _unitOfWork = unitOfWork; _mapper = mapper; }

        public void Add(FileDomain FileDomain) { _unitOfWork.File.Add(_mapper.Map<File>(FileDomain)); }

        public void Update(FileDomain FileDomain) { _unitOfWork.File.Update(_mapper.Map<File>(FileDomain)); }

        public void Remove(FileDomain FileDomain) { _unitOfWork.File.Remove(_mapper.Map<File>(FileDomain)); }

        public FileDomain GetById(int id) { return _mapper.Map<FileDomain>(_unitOfWork.File.GetById(id)); }

        public void Remove(int id) { _unitOfWork.File.Remove(id); }

        public List<FileDomain> GetAll() { return _mapper.Map<List<FileDomain>>(_unitOfWork.File.GetAll()); }
    }
}