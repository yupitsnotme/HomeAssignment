using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class FileTransfersRepository: IFileTransfersRepository
    {
        private FileTransferContext context;
        public FileTransfersRepository(FileTransferContext context)
        {
            this.context = context;
        }

        public FileTransfer GetFileTransfer(int id)
        {
            return context.FileTransfers.SingleOrDefault(f => f.Id == id);
        }

        public IQueryable<FileTransfer> GetFileTransfers()
        {
            return context.FileTransfers;
        }

        public void AddFile(FileTransfer ft)
        {
            context.FileTransfers.Add(ft);
            context.SaveChanges();
        }

    }
}
