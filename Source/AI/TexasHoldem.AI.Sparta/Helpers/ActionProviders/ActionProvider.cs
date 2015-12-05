using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    internal abstract class ActionProvider
    {
        internal abstract PlayerAction GetAction();
    }
}
