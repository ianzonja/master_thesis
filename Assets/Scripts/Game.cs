using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Game
{
    public Guid GameId { get; set; }

    public PlayerData[] Players = new PlayerData[4];
    public string HostUsername { get; set; }
    public RoundPoint[] Scoreboard { get; set; }
    public string DealerPlayfabId { get; set; }
    public Message[] Chat { get; set; }

    public CardData[] CollectedCards { get; set; }

    public CardData[] ShuffledCards { get; set; }

    public string Status { get; set; }

    public string RoomId { get; set; }

    public string PlayerIdToPlay { get; set; }
}
