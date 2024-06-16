using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open93AtHome.Modules.Database
{
    public class ClusterEntity
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Indexed]
        [Column("cluster_id")]
        public string ClusterId { get; set; } = string.Empty;

        [Column("cluster_secret")]
        public string ClusterSecret { get; set; } = string.Empty;

        [Column("endpoint")]
        public string Endpoint { get; set; } = string.Empty;

        [Column("down_reason")]
        public string DownReason { get; set; } = string.Empty;

        [Column("cluster_name")]
        public string ClusterName { get; set; } = string.Empty;

        public static ClusterEntity CreateClusterEntity()
        {
            ClusterEntity clusterEntity = new ClusterEntity();
            clusterEntity.ClusterId = Utils.GenerateHexString(24);
            clusterEntity.ClusterSecret = Utils.GenerateHexString(32);
            return clusterEntity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ClusterId, ClusterSecret);
        }

        public override string ToString()
        {
            return $"<{this.GetType().FullName} instance index={this.Id} id={this.ClusterId} secret={this.ClusterSecret} code={this.GetHashCode()}>";
        }
    }
}
