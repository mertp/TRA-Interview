using System.ComponentModel.DataAnnotations;

namespace MusicMvcApp.Models
{
    public class Music
    {
        //Music modeli
        [Key]
        public int Id { get; set; }
        //Zorunlu alanlar için annotionlar
        [Required(ErrorMessage="Şarkı ismi giriniz")]
        public string SongName { get; set; }
        public string SongTime { get; set; }
        //Zorunlu alanlar için annotionlar
        [Required(ErrorMessage = "Artist ismi giriniz")]
        public string Artist { get; set; }
        public string AlbumName { get; set; }
        public int ReleaseDate { get; set; }

    }
}
