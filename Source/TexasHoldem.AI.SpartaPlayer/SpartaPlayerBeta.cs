namespace TexasHoldem.AI.SpartaPlayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Helpers;
    using Logic;
    using Logic.Cards;
    using Spartalayer.Helpers;
    using TexasHoldem.Logic.Players;

    public class SpartaPlayerBeta : BasePlayer
    {
        public override string Name { get; } = "SpartaPlayerBeta_" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (context.MoneyLeft <= 250)
            {
                // CLOSE to LOOSING The Game - PLay aggresivly to return back the stack
                var preFlopCards = CustomHandEvaluator.PreFlopAggressive(context, this.FirstCard, this.SecondCard);
                var firstCard = this.FirstCard;
                var secondCard = this.SecondCard;
                var communityCards = this.CommunityCards;
                return CustomStackActions.NormalStackMethod(context, preFlopCards, firstCard, secondCard, communityCards);
            }
            else if (context.MoneyLeft > 250 && context.MoneyLeft < 1750)
            {
                var preFlopCards = CustomHandEvaluator.PreFlop(context, this.FirstCard, this.SecondCard);
                var firstCard = this.FirstCard;
                var secondCard = this.SecondCard;
                var communityCards = this.CommunityCards;
                return CustomStackActions.NormalStackMethod(context, preFlopCards, firstCard , secondCard, communityCards);
            }
            else
            {
                // context.MoneyLeft > 1600 we are CHIPLEADERS - FIGHT With AGRESSION!
                var preFlopCards = CustomHandEvaluator.PreFlopAggressive(context, this.FirstCard, this.SecondCard);
                var firstCard = this.FirstCard;
                var secondCard = this.SecondCard;
                var communityCards = this.CommunityCards;
                return CustomStackActions.NormalStackMethod(context, preFlopCards, firstCard, secondCard, communityCards);
            }
        }
    }
}
