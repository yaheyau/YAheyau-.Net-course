﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
  internal interface IDataService
  {
    public List<User> GetScoreGame();
    public void WriteResultGame(List<User> scoreGame);
  }
}
