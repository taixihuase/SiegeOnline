//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Cast.cs
//
// 文件功能描述：
//
// 记录施法信息，客户端进行施法操作时，发送技能产生的特定数据给服务端处理后广播技能数据
//
// 创建标识：taixihuase 20150725
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

namespace SiegeOnlineServer.Protocol.Common.Skill
{
    /// <summary>
    /// 类型：类
    /// 名称：Cast
    /// 作者：taixihuase
    /// 作用：施法信息类
    /// 编写日期：2015/7/25
    /// </summary>
    public class Cast
    {
        public int PlayerId { get; set; }

        public int SkillId { get; set; }

        public string SkillName { get; set; }

        public Cast(int player, int id, string name)
        {
            PlayerId = player;
            SkillId = id;
            SkillName = name;
        }
    }
}
