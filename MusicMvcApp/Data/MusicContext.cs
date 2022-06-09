using Microsoft.EntityFrameworkCore;
using MusicMvcApp.Models;

namespace MusicMvcApp.Data
{
    public class MusicContext:DbContext
    {
        //Projenin startup dosyasında database i konfigüre edebilmek için DbContextOptions bileşeni
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {

        }
        //Tablodan veri çekebilmek için DbSet Objesi
        public DbSet<Music> Musics { get; set; }
    }
}
