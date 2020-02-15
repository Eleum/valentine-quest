using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valentine.Domain;

namespace Valentine.Application.Interfaces
{
    public interface IValentineDbContext
    {
        DbSet<Image> Images { get; set; }

        Task<int> SaveChangesAsync();
    }
}
