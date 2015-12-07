namespace TexasHoldem.AI.SpartaPlayer.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TexasHoldem.Logic;
    using TexasHoldem.Logic.Players;

    public static class CustomPlayerActions
    {
        public static PlayerAction AgressivePlayerActionPreflop(GetTurnContext context, CardValueType preFlopCards, int raiseAmount, int pushAmount)
        {
            if (preFlopCards != CardValueType.Unplayable)
            {
                if (preFlopCards == CardValueType.NotRecommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind * 11)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount / 2);
                        }
                    }

                    return CheckOrFoldCustomAction(context);
                }
                else if (preFlopCards == CardValueType.Risky)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind * 21)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 21 && context.MoneyToCall > context.SmallBlind * 11)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount / 2);
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (preFlopCards == CardValueType.Recommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * pushAmount);
                        }
                        else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind * 41)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 41 && context.MoneyToCall > context.SmallBlind * 11)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount / 2);
                        }
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }

            return CheckOrFoldCustomAction(context);
        }

        public static PlayerAction PassivePlayerActionPreFlop(GetTurnContext context, CardValueType preFlopCards, int raiseAmount, int pushAmount)
        {
            if (preFlopCards != CardValueType.Unplayable)
            {
                if (preFlopCards == CardValueType.NotRecommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind * 11)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 11 && context.MoneyToCall > context.SmallBlind * 6)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount / 2);
                        }
                    }

                    return CheckOrFoldCustomAction(context);
                }
                else if (preFlopCards == CardValueType.Risky)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind * 21)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 21 && context.MoneyToCall > context.SmallBlind * 11)
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount / 2);
                        }
                    }

                    return CheckOrFoldCustomAction(context);
                }
                else if (preFlopCards == CardValueType.Recommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * pushAmount);
                        }
                        else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind * 41)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 41 && context.MoneyToCall > context.SmallBlind * 11)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount / 2);
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
            }

            return CheckOrFoldCustomAction(context);
        }

        public static PlayerAction AgressivePlayerAction(GetTurnContext context, CardValueType preFlopCards, HandRankType combination, int potMultiplier, int raiseSbMultipliyer)
        {
            if (preFlopCards == CardValueType.Recommended)
            {
                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        if ((context.MoneyToCall > context.CurrentPot / 2 || context.MoneyToCall > context.SmallBlind * 14)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft >= context.SmallBlind * raiseSbMultipliyer)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft >= context.SmallBlind * raiseSbMultipliyer)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else
                    {
                        if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.SmallBlind * 6);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                }
                else
                {
                    if (context.CanCheck && context.MoneyLeft > 0 && context.MyMoneyInTheRound == 0)
                    {
                        return PlayerAction.Raise((context.SmallBlind * raiseSbMultipliyer) - context.SmallBlind);
                    }
                    else if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        return CheckOrFoldCustomAction(context);
                    }
                }
            }
            else if (preFlopCards == CardValueType.NotRecommended || preFlopCards == CardValueType.Risky)
            {
                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.SmallBlind * 10)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }
                    else
                    {
                        if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.SmallBlind * 6);
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }
                }
                else
                {
                    if (context.CanCheck && context.MoneyLeft > 0 && context.MyMoneyInTheRound == 0)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        return CheckOrFoldCustomAction(context);
                    }
                }
            }
            else
            {
                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 || context.MoneyToCall > context.SmallBlind * 14)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft >= context.SmallBlind * raiseSbMultipliyer)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else
                    {
                        if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.SmallBlind * 6);
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }
                }
                else
                {
                    if (context.CanCheck && context.MoneyLeft > 0 && context.MyMoneyInTheRound == 0)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        return CheckOrFoldCustomAction(context);
                    }
                }
            }
        }

        public static PlayerAction PassivePlayerAction(GetTurnContext context, HandRankType combination, int raiseAmount, int pushAmount)
        {
            if (CustomHandStreightChecks.GotStrongHand(combination))
            {
                if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                {
                    if (!context.CanCheck
                        && (context.MoneyToCall > context.CurrentPot / 2 || context.MoneyToCall > context.SmallBlind * 14)
                        && context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                    else if (context.MoneyLeft >= context.SmallBlind * pushAmount)
                    {
                        return PlayerAction.Raise(context.SmallBlind * pushAmount);
                    }
                    else
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                }
                else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                {
                    if (!context.CanCheck
                        && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                        && context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                    else if (context.MoneyLeft >= context.SmallBlind * raiseAmount)
                    {
                        return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                    }
                    else
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                }
                else
                {
                    if (context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.SmallBlind * 6);
                    }
                    else
                    {
                        return PlayerAction.CheckOrCall();
                    }
                }
            }
            else
            {
                if (context.CanCheck && context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise((context.SmallBlind * raiseAmount) - context.SmallBlind);
                }
                else if (context.MoneyToCall <= context.SmallBlind * 2)
                {
                    return PlayerAction.CheckOrCall();
                }

                return CheckOrFoldCustomAction(context);
            }
        }

        public static PlayerAction CheckOrFoldCustomAction(GetTurnContext context)
        {
            if (context.CanCheck)
            {
                return PlayerAction.CheckOrCall();
            }
            else if (!context.CanCheck)
            {
                if (context.CurrentPot < context.SmallBlind * 5)
                {
                    return PlayerAction.CheckOrCall();
                }

                return PlayerAction.Fold();
            }
            else
            {
                return PlayerAction.Fold();
            }
        }
    }
}
