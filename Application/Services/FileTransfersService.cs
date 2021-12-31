using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class FileTransfersService : IFileTransfersService
    {
        private IFileTransfersRepository fileTransfersRepository;
        public FileTransfersService(IFileTransfersRepository fileTransfersRepository)
        {
            this.fileTransfersRepository = fileTransfersRepository;
        }

        public FileTransferViewModel GetFileTransfer(int id)
        {
            var fileTransfer = fileTransfersRepository.GetFileTransfer(id);
            var result = new FileTransferViewModel()
            {
                Id = fileTransfer.Id,
                Email = fileTransfer.Email,
                ToEmail = fileTransfer.ToEmail,
                Title = fileTransfer.Title,
                Message = fileTransfer.Message,
                Password = fileTransfer.Password
            };
            return result;
        }

        public IQueryable<FileTransferViewModel> GetFileTransfers()
        {
            var list = from f in fileTransfersRepository.GetFileTransfers()
                       select new FileTransferViewModel()
                       {
                           Id = f.Id,
                           Email = f.Email,
                           ToEmail = f.ToEmail,
                           Title = f.Title,
                           Message = f.Message,
                           Password = f.Password
                       };
            return list;
        }
    }
}
