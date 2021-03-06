﻿using NUnit.Framework;
using UnityEngine;
using System.Linq;
using TouhouHeartstone;
using TouhouCardEngine;
using TouhouHeartstone.Builtin;
using System;
namespace Tests
{
    public class KeywordsTests
    {
        /// <summary>
        /// 突袭的测试
        /// </summary>
        [Test]
        public void RushTest()
        {
            THHGame game = GameInitWithoutPlayer<DefaultServant, RushServant>(29, 1);

            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[1], 0);
            game.sortedPlayers[1].cmdTurnEnd(game);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[0], 0);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[1], 1);

            Assert.True(game.sortedPlayers[0].field[0].isRush(game));   //是否为突袭随从
            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[1], game.sortedPlayers[1].master);
            Assert.AreEqual(30, game.sortedPlayers[1].master.getCurrentLife(game));     //普通随从没准备好无法攻击
            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[0], game.sortedPlayers[1].master);
            Assert.AreEqual(30, game.sortedPlayers[1].master.getCurrentLife(game));     //突袭的随从没准备好无法攻击master
            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[0], game.sortedPlayers[1].field[0]);
            Assert.AreEqual(6, game.sortedPlayers[1].field[0].getCurrentLife(game));    //突袭的随从可以攻击敌方随从
        }

        /// <summary>
        /// 圣盾的测试
        /// </summary>
        [Test]
        public void ShieldTest()
        {
            THHGame game = GameInitWithoutPlayer<DefaultServant, ShieldServant>(29, 1);
            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[1], 0);
            game.sortedPlayers[1].cmdTurnEnd(game);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[1], 0);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[0], 1);
            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdTurnEnd(game);

            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[0], game.sortedPlayers[1].field[0]);
            Assert.AreEqual(6, game.sortedPlayers[0].field[0].getCurrentLife(game));    //没圣盾的随从攻击后受到反伤
            Assert.True(game.sortedPlayers[0].field[1].isShield(game));                 //圣盾随从带有圣盾
            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[1], game.sortedPlayers[1].field[0]);
            Debug.Log(game.sortedPlayers[0].field[1].getCurrentLife(game));
            Assert.AreEqual(3, game.sortedPlayers[0].field[1].getCurrentLife(game));    //圣盾随从攻击后不受伤
            Assert.False(game.sortedPlayers[0].field[1].isShield(game));                //圣盾随从攻击后圣盾消失
        }

        /// <summary>
        /// 潜行的测试
        /// </summary>
        [Test]
        public void StealthTest()
        {
            THHGame game = GameInitWithoutPlayer<DefaultServant, StealthServant>(29, 1);

            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[1], 0);
            game.sortedPlayers[1].cmdTurnEnd(game);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[1], 0);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[0], 1);
            Assert.True(game.sortedPlayers[0].field[1].isStealth(game));
            game.sortedPlayers[0].cmdTurnEnd(game);


            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[0], game.sortedPlayers[0].field[1]);
            //game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[0], game.sortedPlayers[1].master);

            Assert.AreEqual(3, game.sortedPlayers[0].field[1].getCurrentLife(game));  //潜行的随从不会被攻击，因此血量不变
            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[0], game.sortedPlayers[0].field[0]);
            Assert.AreEqual(6, game.sortedPlayers[1].field[0].getCurrentLife(game));  //非潜行随从可以被攻击
            game.sortedPlayers[1].cmdTurnEnd(game);
            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[1], game.sortedPlayers[1].field[0]);
            Assert.False(game.sortedPlayers[0].field[1].isStealth(game));   //潜行随从攻击后变为非潜行状态
            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[0], game.sortedPlayers[0].field[1]);
            Assert.AreEqual(1, game.sortedPlayers[0].field[1].getCurrentLife(game));    //变为非潜行状态后可以被攻击
            //game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[0], 1);
            //Assert.True(game.sortedPlayers[1].field[1].isStealth(game));
            //game.sortedPlayers[1].cmdTurnEnd(game);
            //Assert.AreEqual(29, game.sortedPlayers[0].master.getCurrentLife(game));     //潜行随从在回合结束时对对方master造成伤害
            //Assert.False(game.sortedPlayers[1].field[1].isStealth(game));               //潜行消失
        }

        /// <summary>
        /// 吸血测试
        /// </summary>
        [Test]
        public void DrainTest()
        {
            THHGame game = GameInitWithoutPlayer<DefaultServant, DrainServant>(29, 1);
            AfterXRound(game, 1);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[1], 0);
            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[1], 0);
            game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[0], 1);
            game.sortedPlayers[1].cmdTurnEnd(game);
            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[0], game.sortedPlayers[1].master);
            Assert.AreEqual(29, game.sortedPlayers[1].master.getCurrentLife(game));  //被攻击后血量减少1
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[0], 1);
            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[0], game.sortedPlayers[0].master);
            Assert.AreEqual(29, game.sortedPlayers[1].master.getCurrentLife(game));  //不会吸血的随从攻击后，master没有回血
            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[1], game.sortedPlayers[0].field[1]);
            Assert.AreEqual(30, game.sortedPlayers[1].master.getCurrentLife(game));  //会吸血的随从攻击后，master回血
            Assert.AreEqual(30, game.sortedPlayers[0].master.getCurrentLife(game));  //攻击吸血随从，敌方master回血

        }

        /// <summary>
        /// 剧毒测试
        /// </summary>
        [Test]
        public void PoisonousTest()
        {
            THHGame game = GameInitWithoutPlayer<DefaultServant, PoisonousServant>(29, 1);
            AfterXRound(game, 1);
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[1], 0);
            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[1], 0);
            game.sortedPlayers[1].cmdUse(game, game.sortedPlayers[1].hand[0], 1);
            game.sortedPlayers[1].cmdTurnEnd(game);
            game.sortedPlayers[0].cmdAttack(game, game.sortedPlayers[0].field[0], game.sortedPlayers[1].field[1]);
            Assert.AreEqual(0, game.sortedPlayers[0].field.count);      //攻击剧毒随从，自身死亡
            game.sortedPlayers[0].cmdUse(game, game.sortedPlayers[0].hand[1], 0);
            game.sortedPlayers[0].cmdTurnEnd(game);
            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[0], game.sortedPlayers[0].field[0]);
            Assert.AreEqual(6, game.sortedPlayers[0].field[0].getCurrentLife(game));    //没有剧毒的随从攻击，受到伤害，不会即死
            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[1], game.sortedPlayers[0].field[0]);
            Assert.AreEqual(0, game.sortedPlayers[0].field.count);      //有剧毒的随从攻击敌方随从，随从死亡
            game.sortedPlayers[1].cmdTurnEnd(game);
            game.sortedPlayers[0].cmdTurnEnd(game);
            Assert.True(game.sortedPlayers[1].field[1].isPoisonous(game));
            game.sortedPlayers[1].cmdAttack(game, game.sortedPlayers[1].field[1], game.sortedPlayers[0].master);
            Assert.AreEqual(29, game.sortedPlayers[0].master.getCurrentLife(game));     //剧毒随从攻击master，master受到伤害，不会即死

        }

        /// <summary>
        /// 魔免测试
        /// </summary>
        [Test]
        public void ElusiveTest()
        {
            THHGame game = TestGameflow.initGameWithoutPlayers(null, new GameOption()
            {
                shuffle = false
            });
            THHPlayer defaultPlayer = game.createPlayer(0, "玩家0", game.getCardDefine<TestMaster2>(), Enumerable.Repeat(game.getCardDefine<DefaultServant>() as CardDefine, 28)
            .Concat(Enumerable.Repeat(game.getCardDefine<TestSpellCard>(), 2)));
            THHPlayer elusivePlayer = game.createPlayer(1, "玩家1", game.getCardDefine<TestMaster2>(), Enumerable.Repeat(game.getCardDefine<DefaultServant>() as CardDefine, 29)
            .Concat(Enumerable.Repeat(game.getCardDefine<ElusiveServant>(), 1)));

            game.skipTurnUntil(() => defaultPlayer.gem >= game.getCardDefine<DefaultServant>().cost && game.currentPlayer == defaultPlayer);
            defaultPlayer.cmdUse(game, defaultPlayer.hand.First(c => c.define.id == DefaultServant.ID), 0);

            game.skipTurnUntil(() => elusivePlayer.gem >= game.getCardDefine<ElusiveServant>().cost && game.currentPlayer == elusivePlayer);
            elusivePlayer.cmdUse(game, elusivePlayer.hand.First(c => c.define.id == ElusiveServant.ID), 0);

            game.skipTurnUntil(() => defaultPlayer.gem >= game.getCardDefine<TestSpellCard>().cost && game.currentPlayer == defaultPlayer);
            defaultPlayer.cmdUse(game, defaultPlayer.hand.First(c => c.define.id == TestSpellCard.ID), 0, defaultPlayer.field[0]);
            Assert.AreEqual(6, defaultPlayer.field[0].getCurrentLife(game));//没有魔免的可以被法术指定
            defaultPlayer.cmdUse(game, defaultPlayer.hand.First(c => c.define.id == TestSpellCard.ID), 0, elusivePlayer.field[0]);
            Assert.AreEqual(3, elusivePlayer.field[0].getCurrentLife(game));//魔免无法被法术指定

            game.skipTurnUntil(() => elusivePlayer.gem >= game.getCardDefine<TestDamageSkill>().cost && game.currentPlayer == elusivePlayer);
            elusivePlayer.cmdUse(game, elusivePlayer.skill, 0, defaultPlayer.field[0]);
            Assert.AreEqual(5, defaultPlayer.field[0].getCurrentLife(game));//没有魔免的可以被技能指定
            elusivePlayer.cmdTurnEnd(game);

            game.skipTurnUntil(() => elusivePlayer.gem >= game.getCardDefine<TestDamageSkill>().cost && game.currentPlayer == elusivePlayer);
            elusivePlayer.cmdUse(game, elusivePlayer.skill, 0, elusivePlayer.field[0]);
            Assert.AreEqual(3, elusivePlayer.field[0].getCurrentLife(game));//魔免无法被技能指定

            game.Dispose();
        }
        /// <summary>
        /// 冰冻测试：在测试类中创建一个冰环法术，对方拍一个默认随从，我方放一个冰环，预期下一个回合对方的随从无法进行攻击，再下一个回合解冻可以进行攻击。
        /// </summary>
        [Test]
        public void freezeTest()
        {
            THHGame game = TestGameflow.initGameWithoutPlayers(null, new GameOption()
            {
                shuffle = false
            });
            THHPlayer defaultPlayer = game.createPlayer(0, "玩家0", game.getCardDefine<TestMaster2>(), Enumerable.Repeat(game.getCardDefine<DefaultServant>() as CardDefine, 30));
            THHPlayer elusivePlayer = game.createPlayer(1, "玩家1", game.getCardDefine<TestMaster2>(), Enumerable.Repeat(game.getCardDefine<DefaultServant>() as CardDefine, 28)
            .Concat(Enumerable.Repeat(game.getCardDefine<TestFreeze>(), 2)));

            game.skipTurnUntil(() => defaultPlayer.gem >= game.getCardDefine<DefaultServant>().cost && game.currentPlayer == defaultPlayer);
            defaultPlayer.cmdUse(game, defaultPlayer.hand.First(c => c.define.id == DefaultServant.ID), 0);
            //玩家0拍白板一张

            game.skipTurnUntil(() => elusivePlayer.gem >= game.getCardDefine<TestFreeze>().cost && game.currentPlayer == elusivePlayer);
            elusivePlayer.cmdUse(game, elusivePlayer.hand.First(c => c.define.id == TestFreeze.ID), 0, defaultPlayer.field);
            //玩家1释放冰环

            game.skipTurnUntil(() => defaultPlayer.gem >= game.getCardDefine<DefaultServant>().cost && game.currentPlayer == defaultPlayer);
            defaultPlayer.cmdAttack(game, defaultPlayer.field[0], elusivePlayer.master);//被冻结的随从无法攻击
            defaultPlayer.cmdTurnEnd(game);//回合结束，随从解冻

            elusivePlayer.cmdTurnEnd(game);

            game.skipTurnUntil(() => defaultPlayer.gem >= game.getCardDefine<DefaultServant>().cost && game.currentPlayer == defaultPlayer);
            defaultPlayer.cmdAttack(game, defaultPlayer.field[0], elusivePlayer.master);//已解除冰冻的随从可以攻击
            Assert.AreEqual(29, elusivePlayer.master.getCurrentLife(game));

            game.Dispose();
        }
        /// <summary>
        /// 需要写一个愤怒的女祭司随从，给它剧毒属性，然后预期在回合结束的时候她只需要刮一刀就可以刮死对面不止1血的怪
        /// </summary>
        [Test]
        public void poisiousTest_ServantEffect()
        {
            THHGame game = TestGameflow.initGameWithoutPlayers(null, new GameOption()
            {
                shuffle = false
            });
            THHPlayer defaultPlayer = game.createPlayer(0, "玩家0", game.getCardDefine<TestMaster2>(), Enumerable.Repeat(game.getCardDefine<DefaultServant>() as CardDefine, 30));
            THHPlayer elusivePlayer = game.createPlayer(1, "玩家1", game.getCardDefine<TestMaster2>(), Enumerable.Repeat(game.getCardDefine<DefaultServant>() as CardDefine, 28)
            .Concat(Enumerable.Repeat(game.getCardDefine<Priestess>(), 2)));

            game.skipTurnUntil(() => defaultPlayer.gem >= game.getCardDefine<DefaultServant>().cost && game.currentPlayer == defaultPlayer);
            defaultPlayer.cmdUse(game, defaultPlayer.hand.First(c => c.define.id == DefaultServant.ID), 0);
            //玩家0拍白板一张

            game.skipTurnUntil(() => elusivePlayer.gem >= game.getCardDefine<Priestess>().cost && game.currentPlayer == elusivePlayer);
            elusivePlayer.cmdUse(game, elusivePlayer.hand.First(c => c.define.id == Priestess.ID), 0);
            //玩家1拍女祭司

            game.skipTurnUntil(() => defaultPlayer.gem >= game.getCardDefine<DefaultServant>().cost && game.currentPlayer == defaultPlayer);
            defaultPlayer.cmdUse(game, defaultPlayer.hand.First(c => c.define.id == DefaultServant.ID), 1);

            game.Dispose();
        }
        /// <summary>
        /// 从player[0]开始经过x回合
        /// </summary>
        /// <param name="game"></param>
        /// <param name="x"></param>
        public void AfterXRound(THHGame game, int x)
        {
            for (int i = 0; i < x; i++)
            {
                game.sortedPlayers[0].cmdTurnEnd(game);
                game.sortedPlayers[1].cmdTurnEnd(game);
            }
        }

        /// <summary>
        /// 初始化对局
        /// </summary>
        /// <typeparam name="T1">卡牌1</typeparam>
        /// <typeparam name="T2">卡牌2</typeparam>
        /// <param name="card1Count"></param>
        /// <param name="card2Count"></param>
        /// <returns></returns>
        public THHGame GameInitWithoutPlayer<T1, T2>(int card1Count, int card2Count) where T1 : CardDefine where T2 : CardDefine
        {

            THHGame game = TestGameflow.initGameWithoutPlayers(null, new GameOption()
            {
                shuffle = false
            });
            game.createPlayer(0, "玩家0", game.getCardDefine<Reimu>(), Enumerable.Repeat(game.getCardDefine<T1>() as CardDefine, 29)
            .Concat(Enumerable.Repeat(game.getCardDefine<T2>(), 1)));
            game.createPlayer(1, "玩家1", game.getCardDefine<Reimu>(), Enumerable.Repeat(game.getCardDefine<T1>() as CardDefine, 29)
            .Concat(Enumerable.Repeat(game.getCardDefine<T2>(), 1)));
            game.run();
            game.sortedPlayers[0].cmdInitReplace(game);
            game.sortedPlayers[1].cmdInitReplace(game);
            return game;
        }
    }
}
