using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Games.Drones.Models
{
    public class Round
    {
        public int Id { get; set; }
        public int? GameId { get; set; }
        public int? Player1MoveId { get; set; }
        public int? Player2MoveId { get; set; }
        public int? WinnerPlayer { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        [ForeignKey("Player1MoveId")]
        public virtual Move MovePlayer1 { get; set; }
        [ForeignKey("Player2MoveId")]
        public virtual Move MovePlayer2 { get; set; }
    }
}