﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.Data
{
    public interface IGuessDTO
    {
        int Id { get; }
        IChainDTO Chain { get; }
    }
}
