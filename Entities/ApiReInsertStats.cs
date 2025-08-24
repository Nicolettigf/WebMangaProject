using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApiReInsertStats
    {
        public int Id { get; set; }
        public string ApiName { get; set; }
        public int IdFromApi { get; set; }
        public string? Type { get; set; }

        public string? Erro { get; set; }
    }
}
