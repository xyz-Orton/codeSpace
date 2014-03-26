using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Entities
{
    public class OrganizationResult
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 节点标签
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 节点标签
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// 父级节点ID
        /// </summary>
        public int pId { get; set; }


        public bool open { get; set; }

        public bool isParent { get; set; }
        /// <summary>
        /// 节点数据
        /// </summary>
        public bool isClass { get; set; }
    }
}
