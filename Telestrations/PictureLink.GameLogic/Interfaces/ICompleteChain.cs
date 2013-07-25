﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLink.Data;

namespace PictureLink.GameLogic
{
    public interface ICompleteChain : IChainDTO
    {
        bool HasMarksAssigned(int playerId);
    }
}
