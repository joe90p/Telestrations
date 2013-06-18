using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.Data
{
    public interface IMarkDTO
    {
        IGuessDTO Guess { get; }
        IPlayerDTO Awarder { get; }
        int Score { get; }
        int Id { get; }
    }
}
