namespace BusinessLogicalLayer.Interfaces
{
    public interface IJikanApi
    {
        /// <summary>
        /// Consome mangas da API exerna kitsu, e insere na DB(DAL)
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        Task ConsumeManga();
        Task ConsumeMissingMangas();
        Task ConsumeAnime();
        Task ConsumeMissingAnime();
        Task ConsumeGenre();
    }
}
