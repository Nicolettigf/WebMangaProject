namespace Entities.Common
{
    public class ApiConsumeStats
    {
        public int Id { get; set; }
        public string ApiName { get; set; }
        public string ApiURLBase { get; set; }
        public int? PagesConsumedAnime { get; set; }
        public int? PagesConsumedManga { get; set; }
        public int? UnitarioAnime { get; set; }
        public int? UnitarioManga { get; set; }

        public static List<int> GetMissingUnitarios(List<int> existingIds, int limite)
        {
            if (existingIds == null || existingIds.Count == 0)
                return new List<int>(); // se não há ids, retorna vazio

            existingIds.Sort(); // garante que a lista esteja ordenada

            var missingIds = new List<int>();
            int current = existingIds.First(); // começa do primeiro ID existente

            foreach (var id in existingIds)
            {
                if (id > current)
                {
                    missingIds.AddRange(Enumerable.Range(current, id - current));
                }
                current = id + 1;
            }

            if (current <= limite)
            {
                missingIds.AddRange(Enumerable.Range(current, limite - current + 1));
            }

            return missingIds;
        }
    }

    public enum ConsumoTipo
    {
        Unitario,
        Pagina
    }
}
