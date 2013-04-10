﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.GameLogic
{
    public interface IChain
    {
        bool IsAvailableForPlayer(IPlayer player);
        void AddGuess(IGuess guess);
        int Count { get; }
        IGuess Head { get; }
        void Lock(IPlayer player);
        IPlayer LockedBy { get; }
        void Release(IPlayer player);
    }
}