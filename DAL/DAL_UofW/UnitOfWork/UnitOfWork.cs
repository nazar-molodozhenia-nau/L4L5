using DAL_Database;
using DAL_Entities;
using DAL_Repositories;

using System;

namespace DAL_UofW {
    public class UnitOfWork : IUnitOfWork {

        private readonly ApplicationDbContext _context;

        private IRepository<Storage> _storageRepository;
        private IRepository<Folder> _folderRepository;
        private IRepository<File> _fileRepository;

        public UnitOfWork(ApplicationDbContext context) { _context = context; }

        public IRepository<Storage> Storage {
            get {
                if(_storageRepository == null) { _storageRepository = new Repository<Storage>(_context); }
                return _storageRepository;
            }
        }

        public IRepository<Folder> Folder {
            get {
                if(_folderRepository == null) { _folderRepository = new Repository<Folder>(_context); }
                return _folderRepository;
            }
        }

        public IRepository<File> File {
            get {
                if(_fileRepository == null) { _fileRepository = new Repository<File>(_context); }
                return _fileRepository;
            }
        }

        public void Save() { _context.SaveChanges(); }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if(!disposed) { if(disposing) { _context.Dispose(); } }
            disposed = true;
        }

        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

    }
}