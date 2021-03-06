﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public interface IGuessInfo
    {
        string Content {get;}
        IPlayer Contributor { get; }
        GuessType Type { get; }
    }
}
