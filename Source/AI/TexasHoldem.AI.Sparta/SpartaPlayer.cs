using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.Sparta
{
    public class SpartaPlayer : BasePlayer
    {

        public override string Name { get; } = "Sparta" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {




            return null;
        }
    }
}
