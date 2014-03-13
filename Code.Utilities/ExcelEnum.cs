using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Utilities
{
    /// <summary>
    /// Excel单元格样式类型
    /// </summary>
    public enum CellStyleType
    {
        /// <summary>
        /// 表标题
        /// </summary>
        Title = 0,

        /// <summary>
        /// 表头
        /// </summary>
        TH = 1, 

        /// <summary>
        /// 表内容单元格
        /// </summary>
        TD = 2, 

        /// <summary>
        /// 总计
        /// </summary>
        SumTotal = 3,
   
        /// <summary>
        /// 合计
        /// </summary>
        Total = 4,  

        /// <summary>
        /// 小计
        /// </summary>
        SubTotal = 5    
    }
}
