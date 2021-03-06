﻿using TouhouCardEngine;

namespace TouhouHeartstone
{
    public abstract class MasterCardDefine : CardDefine
    {
        public override string type { get; set; } = CardDefineType.MASTER;
        public abstract int life { get; set; }
        public abstract int skillID { get; set; }
        public override T getProp<T>(string propName)
        {
            if (propName == nameof(life))
                return (T)(object)life;
            return base.getProp<T>(propName);
        }
        public override void merge(CardDefine newVersion)
        {
            if (newVersion.type != type)
                UberDebug.LogWarning(newVersion + "的类型与" + this + "不同，可能是一次非法的数据合并！");
            if (newVersion is MasterCardDefine master)
            {
                life = master.life;
                skillID = master.skillID;
            }
            else if (newVersion is GeneratedCardDefine generated)
            {
                if (generated.hasProp(nameof(life)))
                    life = generated.getProp<int>(nameof(life));
                if (generated.hasProp(nameof(skillID)))
                    skillID = generated.getProp<int>(nameof(skillID));
            }
        }
        public override string isUsable(CardEngine engine, Player player, Card card)
        {
            return null;
        }
    }
}