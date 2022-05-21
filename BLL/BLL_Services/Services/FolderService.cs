using System.Collections.Generic;

using AutoMapper;

using BLL_Domains;
using DAL_Entities;
using DAL_UofW;

namespace BLL_Services {
    public class FolderService : IService<FolderDomain> {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FolderService(IUnitOfWork unitOfWork, IMapper mapper) { _unitOfWork = unitOfWork; _mapper = mapper; }

        public void Add(FolderDomain FolderDomain) { _unitOfWork.Folder.Add(_mapper.Map<Folder>(FolderDomain)); }

        public void Update(FolderDomain FolderDomain) { _unitOfWork.Folder.Update(_mapper.Map<Folder>(FolderDomain)); }

        public void Remove(FolderDomain FolderDomain) { _unitOfWork.Folder.Remove(_mapper.Map<Folder>(FolderDomain)); }

        public FolderDomain GetById(int id) { return _mapper.Map<FolderDomain>(_unitOfWork.Folder.GetById(id)); }

        public void Remove(int id) { _unitOfWork.Folder.Remove(id); }

        public List<FolderDomain> GetAll() { return _mapper.Map<List<FolderDomain>>(_unitOfWork.Folder.GetAll()); }
    }
}