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

    public class SpartaPlayer : BasePlayer
    {
        public override string Name { get; } = "SpartaPlayer_" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (context.MoneyLeft <= 400)
            {
                // CLOSE to LOOSING The Game - PLay aggresivly to return back the stack
                var preFlopCards = CustomHandEvaluator.PreFlopAggressive(context, this.FirstCard, this.SecondCard);
                var firstCard = this.FirstCard;
                var secondCard = this.SecondCard;
                var communityCards = this.CommunityCards;
                return CustomStackActions.BigStackMethod(context, preFlopCards, firstCard, secondCard, communityCards);
            }
            else if (context.MoneyLeft > 400 && context.MoneyLeft < 1600)
            {
                var preFlopCards = CustomHandEvaluator.PreFlopAggressive(context, this.FirstCard, this.SecondCard);
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
                return CustomStackActions.BigStackMethod(context, preFlopCards, firstCard, secondCard, communityCards);
            }
        }
    }
}
