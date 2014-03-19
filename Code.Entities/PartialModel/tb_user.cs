using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Code.Entities
{
    public partial class tb_user
    {
        [ResultColumn]
        public string OrgName { get; set; }
    }
}
