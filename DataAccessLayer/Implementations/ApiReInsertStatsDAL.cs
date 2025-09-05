using DataAccessLayer.Interfaces;
using Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementations
{
    public class ApiReInsertStatsDAL : IApiReInsertStatsDAL
    {

        private readonly DbContext _context;

        public ApiReInsertStatsDAL(DbContext context)
        {
            _context = context;
        }

        public async Task Insert(ApiReInsertStats dados)
        {
            // Adiciona a entidade no contexto
            await _context.Set<ApiReInsertStats>().AddAsync(dados);

            // Salva no banco
            await _context.SaveChangesAsync();
        }

        public async Task InserRange(List<ApiReInsertStats> dados)
        {
            // Adiciona a entidade no contexto
            await _context.Set<ApiReInsertStats>().AddRangeAsync(dados);

            // Salva no banco
            await _context.SaveChangesAsync();
        }
    }
}
