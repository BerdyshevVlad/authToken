using AuthToken.ViewModels.Goods.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.ViewModels.Goods
{
    public class GetGoodsView
    {
        public List<GetGoodsViewItem> Goods { get; set; }

        public GetGoodsView()
        {
            Goods = new List<GetGoodsViewItem>();
        }
    }
}
