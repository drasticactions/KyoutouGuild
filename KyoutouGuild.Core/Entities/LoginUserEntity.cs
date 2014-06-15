using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KyoutouGuild.Core.Entities
{
    public class LoginUserEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("battle_num")]
        public int BattleNum { get; set; }

        [JsonProperty("profile_entered")]
        public bool ProfileEntered { get; set; }

        [JsonProperty("push_notification_global")]
        public bool PushNotificationGlobal { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("last_playtitle_id")]
        public int LastPlaytitleId { get; set; }

        [JsonProperty("psn_onlineid")]
        public string PsnOnlineid { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("user_badge_ids")]
        public int[] UserBadgeIds { get; set; }

        [JsonProperty("favorite_badge_id")]
        public int FavoriteBadgeId { get; set; }

        [JsonProperty("bgimage_playtitle_id")]
        public object BgimagePlaytitleId { get; set; }

        [JsonProperty("user_trophy_ids")]
        public object[] UserTrophyIds { get; set; }

        [JsonProperty("friend_num")]
        public int FriendNum { get; set; }

        [JsonProperty("playtitles")]
        public object[] Playtitles { get; set; }
    }
}
