using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    interface IFileTransfersService
    {
        public IQueryable<FileTransferViewModel> GetFileTransfers();
        public FileTransferViewModel GetFileTransfer(int id);
        
    }
}
