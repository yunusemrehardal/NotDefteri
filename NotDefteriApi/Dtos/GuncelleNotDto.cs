using System.ComponentModel.DataAnnotations;

namespace NotDefteriApi.Dtos
{
    public class GuncelleNotDto
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Baslik { get; set; } = null!;

        public string? Icerik { get; set; } = string.Empty;
    }
}
